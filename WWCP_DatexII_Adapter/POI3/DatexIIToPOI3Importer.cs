/*
 * Copyright (c) 2014-2026 GraphDefined GmbH <achim.friedland@graphdefined.com>
 * This file is part of WWCP DatexII <https://github.com/OpenChargingCloud/WWCP_DatexII>
 *
 * Licensed under the Affero GPL license, Version 3.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.gnu.org/licenses/agpl.html
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System.Xml.Linq;
using System.Threading.Channels;
using System.Collections.Concurrent;

using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.D2Payload;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

using POI = cloud.charging.open.protocols.WWCP.POI;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.POI3
{

    /// <summary>
    /// Signs a change set of the import with the keys of the operators it changes, e.g. of a hub
    /// that received their data and is trusted by them.
    /// </summary>
    /// <param name="ChangeSet">A change set of the import, unsigned.</param>
    public delegate POI.RoamingNetworkChangeSet SignPOI3ChangeSetDelegate(POI.RoamingNetworkChangeSet ChangeSet);


    /// <summary>
    /// The consumer side: DATEX II Energy Infrastructure publications, e.g. from a national access point, are
    /// committed to a POI3 history.
    /// </summary>
    /// <remarks>
    /// The sites of a table publication become, per operator, one change set against the head: every charging pool as
    /// DATEX II tells it, onto the one there is, with what only POI3 knows kept, and the energy rates of its charging points
    /// as charging tariffs. Charging pools of that operator the table does not have are removed, unless told otherwise.
    /// The change set is signed by the given delegate, as the operator owns its data, and committed; nothing changed,
    /// nothing is committed. The status of a refill point goes into the runtime status of its EVSE: the refill points of
    /// the last table tell which EVSE that is. The operators must be in the roaming network already: adding them is up to
    /// its operator. The work is done one after the other, in the order it came.
    /// </remarks>
    public sealed class DatexIIToPOI3Importer : IAsyncDisposable
    {

        #region Data

        private readonly Channel<Func<Task>>                        work                 = Channel.CreateUnbounded<Func<Task>>(new UnboundedChannelOptions { SingleReader = true });
        private readonly ConcurrentDictionary<String, POI.EVSE_Id>  evseIds              = new();
        private readonly ConcurrentDictionary<String, Byte>         otherRefillPointIds  = new();
        private readonly ConcurrentDictionary<String, POI.ChargingStation_Id>  stationIds      = new();
        private readonly ConcurrentDictionary<String, POI.ChargingPool_Id>     poolIdsOfSites  = new();
        private readonly Task                                       worker;

        #endregion

        #region Properties

        /// <summary>
        /// The history imported into.
        /// </summary>
        public POI.RoamingNetworkHistory               History                     { get; }

        /// <summary>
        /// Signs the change sets of the import.
        /// </summary>
        public SignPOI3ChangeSetDelegate               Sign                        { get; }

        /// <summary>
        /// The language of texts DATEX II gives without one.
        /// </summary>
        public Languages                               Language                    { get; }

        /// <summary>
        /// An optional additional verifier of change set signatures, given to the history when committing.
        /// </summary>
        public POI.VerifyChangeSetSignatureDelegate?   VerifyChangeSetSignature    { get; }

        #endregion

        #region Events

        /// <summary>
        /// Something could not be mapped or committed.
        /// </summary>
        public event OnPOI3DatexIIErrorDelegate?  OnError;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Import DATEX II Energy Infrastructure publications into the given history.
        /// </summary>
        /// <param name="History">A history of a roaming network.</param>
        /// <param name="Sign">Signs the change sets of the import with the keys of the operators.</param>
        /// <param name="Language">The language of texts DATEX II gives without one; English by default.</param>
        /// <param name="VerifyChangeSetSignature">An optional additional verifier of change set signatures.</param>
        public DatexIIToPOI3Importer(POI.RoamingNetworkHistory               History,
                                     SignPOI3ChangeSetDelegate               Sign,
                                     Languages?                              Language                   = null,
                                     POI.VerifyChangeSetSignatureDelegate?   VerifyChangeSetSignature   = null)
        {

            this.History                   = History;
            this.Sign                      = Sign;
            this.Language                  = Language ?? Languages.en;
            this.VerifyChangeSetSignature  = VerifyChangeSetSignature;

            this.worker                    = Task.Run(WorkAsync);

        }

        #endregion


        #region ImportAsync(TablePublication,  RemoveMissingSites = true)

        /// <summary>
        /// Import the sites of the given table publication.
        /// </summary>
        /// <param name="TablePublication">A table publication.</param>
        /// <param name="RemoveMissingSites">Whether charging pools of an operator of the table, which the table does not have, are removed: for full tables.</param>
        public Task ImportAsync(EnergyInfrastructureTablePublication  TablePublication,
                                Boolean                               RemoveMissingSites   = true)

            => Enqueue(() => ImportTable(TablePublication, RemoveMissingSites));

        #endregion

        #region ImportAsync(StatusPublication)

        /// <summary>
        /// Import the refill point status of the given status publication.
        /// </summary>
        /// <param name="StatusPublication">A status publication.</param>
        public Task ImportAsync(EnergyInfrastructureStatusPublication StatusPublication)

            => Enqueue(() => ImportStatus(StatusPublication));

        #endregion

        #region ImportAsync(XML)

        /// <summary>
        /// Import the given DATEX II payload: a table or a status publication.
        /// </summary>
        /// <param name="XML">The XML of a d2:payload.</param>
        public Task ImportAsync(XElement XML)
        {

            if (!Payload.TryParseXML(XML, out var payload, out var errorResponse))
            {
                RaiseError($"The DATEX II payload can not be parsed: {errorResponse}", []);
                return Task.CompletedTask;
            }

            switch (payload)
            {

                case EnergyInfrastructureTablePublication  tablePublication:
                    return ImportAsync(tablePublication);

                case EnergyInfrastructureStatusPublication statusPublication:
                    return ImportAsync(statusPublication);

                default:
                    RaiseError($"A DATEX II payload of the type '{payload.GetType().Name}' can not be imported!", []);
                    return Task.CompletedTask;

            }

        }

        #endregion

        #region DisposeAsync()

        /// <summary>
        /// Stop importing, after what is waiting.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            work.Writer.TryComplete();
            await worker;
        }

        #endregion


        #region (private) WorkAsync() / Enqueue(Job)

        private async Task WorkAsync()
        {
            await foreach (var job in work.Reader.ReadAllAsync())
            {
                try
                {
                    await job();
                }
                catch (Exception e)
                {
                    RaiseError($"Importing failed: {e.Message}", []);
                }
            }
        }

        private Task Enqueue(Action Job)
        {

            var done = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

            if (!work.Writer.TryWrite(() => {
                    try
                    {
                        Job();
                        done.TrySetResult();
                    }
                    catch (Exception e)
                    {
                        done.TrySetException(e);
                    }
                    return Task.CompletedTask;
                }))
            {
                done.TrySetException(new ObjectDisposedException(nameof(DatexIIToPOI3Importer)));
            }

            return done.Task;

        }

        #endregion


        #region (private) ImportTable(TablePublication, RemoveMissingSites)

        private void ImportTable(EnergyInfrastructureTablePublication  TablePublication,
                                 Boolean                               RemoveMissingSites)
        {

            var sitesByOperator  = new Dictionary<POI.ChargingStationOperator_Id, List<EnergyInfrastructureSite>>();
            var takenPoolIds     = new Dictionary<POI.ChargingPool_Id,    String>();
            var takenStationIds  = new Dictionary<POI.ChargingStation_Id, String>();
            var takenEVSEIds     = new Dictionary<POI.EVSE_Id,            String>();

            foreach (var table in TablePublication.EnergyInfrastructureTables)
            {
                foreach (var site in table.EnergyInfrastructureSites)
                {

                    var operatorId = POI3Mapper.OperatorIdOf(site);

                    if (!operatorId.HasValue)
                    {
                        RaiseError($"The operator of the site '{site.Id}' is unknown: it has neither an operator identification nor an EVSE identification!", []);
                        continue;
                    }

                    if (!sitesByOperator.TryGetValue(operatorId.Value, out var sites))
                        sitesByOperator[operatorId.Value] = sites = [];

                    sites.Add(site);

                    // Which EVSE a refill point status is about. A facility left out, as another one became the same
                    // POI3 identification, has none: its status is not taken for that of the other.
                    foreach (var chargingPoint in site.EnergyInfrastructureStations.SelectMany(station => station.RefillPoints).OfType<ElectricChargingPoint>())
                        if (POI3Mapper.ToPOI3EVSEId(chargingPoint, operatorId.Value) is POI.EVSE_Id evseId &&
                            POI3Mapper.IsFirst(takenEVSEIds, evseId, chargingPoint.Id, "charging points", "EVSE", []))
                            evseIds[chargingPoint.Id] = evseId;

                    // Which charging pool a site status is about.
                    if (POI3Mapper.ToPOI3PoolId(site.Id, operatorId.Value) is POI.ChargingPool_Id poolIdOfSite &&
                        POI3Mapper.IsFirst(takenPoolIds, poolIdOfSite, site.Id, "sites", "charging pool", []))
                        poolIdsOfSites[site.Id] = poolIdOfSite;

                    // Which charging station a station status is about.
                    foreach (var station in site.EnergyInfrastructureStations)
                        if (POI3Mapper.ToPOI3StationId(station.Id, operatorId.Value) is POI.ChargingStation_Id stationId &&
                            POI3Mapper.IsFirst(takenStationIds, stationId, station.Id, "stations", "charging station", []))
                            stationIds[station.Id] = stationId;

                    // And which refill points are no EVSEs, e.g. diesel ones: their statuses are skipped.
                    foreach (var otherRefillPoint in site.EnergyInfrastructureStations.SelectMany(station => station.RefillPoints).OfType<OtherRefillPoint>())
                        otherRefillPointIds[otherRefillPoint.Id] = 0;

                }
            }

            var tableIds = String.Join(", ", TablePublication.EnergyInfrastructureTables.Select(table => $"{table.Id} v{table.Version}"));

            foreach (var (operatorId, sites) in sitesByOperator)
            {

                ChangeOperator(operatorId,
                               $"DATEX II table {tableIds}",
                               TablePublication.PublicationTime,
                               (head, operatorJSON, warnings) => {

                                   var pools    = POI3Mapper.ArrayOf(operatorJSON, "chargingPools");

                                   head.TryGetChargingStationOperatorById(operatorId, out var chargingStationOperator);
                                   var poolIds  = new HashSet<String>();
                                   var siteOfPool = new Dictionary<String, String>();

                                   foreach (var site in sites)
                                   {

                                       var poolId    = POI3Mapper.ToPOI3PoolId(site.Id, operatorId)?.ToString();

                                       if (poolId is not null && !POI3Mapper.IsFirst(siteOfPool, poolId, site.Id, "sites", "charging pool", warnings))
                                           continue;

                                       var existing  = pools.OfType<JObject>().FirstOrDefault(pool => pool["@id"]?.Value<String>() == poolId);
                                       // The existing charging pool as it is at runtime, with what its templates give it, and as DATEX II
                                       // tells it: what DATEX II tells alike is kept, what the templates give stays in the templates.
                                       var effective  = chargingStationOperator is not null &&
                                                        POI.ChargingPool_Id.TryParse(poolId ?? "", out var chargingPoolId) &&
                                                        chargingStationOperator.TryGetEffectiveChargingPool(chargingPoolId, out var effectiveChargingPool)
                                                            ? effectiveChargingPool
                                                            : null;

                                       var unchanged  = effective?.ToDatexII(head, Language, [])?.ToPOI3(operatorId, null, Language, []);

                                       var desired    = site.ToPOI3(operatorId, existing, Language, warnings, unchanged, effective?.ToJSON());

                                       if (desired is null || poolId is null)
                                           continue;

                                       poolIds.Add(poolId);

                                       if (existing is not null)
                                           existing.Replace(desired);
                                       else
                                           pools.Add(desired);

                                   }

                                   if (RemoveMissingSites)
                                       foreach (var pool in pools.OfType<JObject>().Where(pool => !poolIds.Contains(pool["@id"]?.Value<String>() ?? "")).ToArray())
                                           pool.Remove();

                                   var tariffs  = POI3Mapper.ArrayOf(operatorJSON, "chargingTariffs");

                                   foreach (var rate in sites.SelectMany(POI3Mapper.RatesOf).GroupBy(rate => rate.Id).Select(rates => rates.First()))
                                   {

                                       var chargingTariff  = rate.ToPOI3(operatorId, Language, warnings);

                                       if (chargingTariff is null)
                                           continue;

                                       var existing        = tariffs.OfType<JObject>().FirstOrDefault(tariff => tariff["@id"]?.Value<String>() == chargingTariff.Id.ToString());
                                       var desired         = chargingTariff.ToJSON();

                                       if (existing is not null)
                                       {
                                           // What only POI3 knows is kept.
                                           var merged = (JObject) existing.DeepClone();
                                           foreach (var property in desired.Properties().Where(property => property.Name is not "created" and not "lastChange"))
                                               merged[property.Name] = property.Value.DeepClone();
                                           POI3Mapper.Overlay(merged, desired, [ "name", "description" ]);
                                           // The payment only when the rate tells one.
                                           if (rate.Payment is not null)
                                               POI3Mapper.Overlay(merged, desired, [ "paymentMeans", "paymentBrands" ]);
                                           // The validity only when the rate tells one.
                                           if (rate.OverallPeriod is not null)
                                               POI3Mapper.Overlay(merged, desired, [ "validFrom", "validUntil" ]);
                                           existing.Replace(merged);
                                       }
                                       else
                                           tariffs.Add(desired);

                                   }

                                   return true;

                               });

            }

        }

        #endregion

        #region (private) ImportStatus(StatusPublication)

        private void ImportStatus(EnergyInfrastructureStatusPublication StatusPublication)
        {

            foreach (var siteStatus in StatusPublication.EnergyInfrastructureSiteStatus)
            {

                // The operation status of the site: the admin status of the charging pool.
                if ((poolIdsOfSites.TryGetValue(siteStatus.Reference.Id, out var knownPool)
                         ? knownPool
                         : POI.ChargingPool_Id.TryParse(siteStatus.Reference.Id)) is POI.ChargingPool_Id chargingPoolId)
                {
                    ImportAdminStatus(nameof(POI.ChargingPool),
                                      chargingPoolId.ToString(),
                                      siteStatus.OperationStatus,
                                      null,
                                      siteStatus.LastUpdated ?? StatusPublication.PublicationTime);
                }

                foreach (var stationStatus in siteStatus.EnergyInfrastructureStationStatus)
                {

                    // The energy rate updates of the station: the price status of the charging station.
                    if ((stationIds.TryGetValue(stationStatus.Reference.Id, out var knownStation)
                             ? knownStation
                             : POI.ChargingStation_Id.TryParse(stationStatus.Reference.Id)) is POI.ChargingStation_Id chargingStationId)
                    {

                        ImportAdminStatus(nameof(POI.ChargingStation),
                                          chargingStationId.ToString(),
                                          stationStatus.OperationStatus,
                                          null,
                                          stationStatus.LastUpdated ?? StatusPublication.PublicationTime);

                        ImportPriceStatus(nameof(POI.ChargingStation),
                                          chargingStationId.ToString(),
                                          chargingStationId.OperatorId,
                                          stationStatus.EnergyRateUpdates,
                                          stationStatus.LastUpdated ?? StatusPublication.PublicationTime);

                    }

                    foreach (var refillPointStatus in stationStatus.RefillPointStatus)
                    {

                        if (otherRefillPointIds.ContainsKey(refillPointStatus.Reference.Id))
                            continue;

                        var evseId = evseIds.TryGetValue(refillPointStatus.Reference.Id, out var known)
                                         ? known
                                         : POI.EVSE_Id.TryParse(refillPointStatus.Reference.Id);

                        if (!evseId.HasValue)
                        {
                            RaiseError($"The refill point '{refillPointStatus.Reference.Id}' is unknown: import its table first!", []);
                            continue;
                        }

                        ImportStatus(evseId.Value,
                                     refillPointStatus.Status.ToPOI3Status(),
                                     refillPointStatus.LastUpdated ?? StatusPublication.PublicationTime);

                        ImportAdminStatus(nameof(POI.EVSE),
                                          evseId.Value.ToString(),
                                          refillPointStatus.OperationStatus,
                                          refillPointStatus.Status.ToString(),
                                          refillPointStatus.LastUpdated ?? StatusPublication.PublicationTime);

                        if (refillPointStatus is ElectricChargingPointStatus electricChargingPointStatus)
                            ImportEnergyStatus(evseId.Value,
                                               electricChargingPointStatus,
                                               refillPointStatus.LastUpdated ?? StatusPublication.PublicationTime);

                        ImportPlannedStatus(evseId.Value,
                                            refillPointStatus);

                        ImportPriceStatus(nameof(POI.EVSE),
                                          evseId.Value.ToString(),
                                          evseId.Value.OperatorId,
                                          refillPointStatus.EnergyRateUpdates,
                                          refillPointStatus.LastUpdated ?? StatusPublication.PublicationTime);

                    }
                }
            }

        }

        private void ImportStatus(POI.EVSE_Id         EVSEId,
                                  POI.EVSEStatusType  Status,
                                  DateTimeOffset      Timestamp)

            => TakeCurrent(POI.POIRuntimeStatusKind.Status,
                           nameof(POI.EVSE),
                           EVSEId.ToString(),
                           Status.ToString(),
                           Timestamp,
                           "status");

        /// <summary>
        /// The operation status of a site, station or refill point as the admin status of the charging pool, charging
        /// station or EVSE: in operation is operational. Not in operation is out of service, unless the current admin
        /// status already tells why it is not operational; for an EVSE, a refill point status planned, blocked or for
        /// internal use tells that admin status.
        /// </summary>
        private void ImportAdminStatus(String            EntityType,
                                       String            EntityId,
                                       OperationStatus?  OperationStatus,
                                       String?           RefillPointStatus,
                                       DateTimeOffset    Timestamp)
        {

            if (!OperationStatus.HasValue)
                return;

            var hasCurrent  = History.RuntimeStatus.TryGet(POI.POIRuntimeStatusKind.AdminStatus, EntityType, EntityId, out var current);
            var isOperational = !hasCurrent || current!.Value.ToLowerInvariant() is "operational" or "unknown" or "unspecified";

            var value = OperationStatus.Value.ToString() switch {
                            "inOperation"     => "operational",
                            "notInOperation"  => RefillPointStatus switch {
                                                     "planned"      => "planned",
                                                     "blocked"      => "blocked",
                                                     "internalUse"  => "internalUse",
                                                     _              => isOperational ? "outOfService" : null
                                                 },
                            _                 => null
                        };

            if (value is null ||
                (hasCurrent && String.Equals(current!.Value, value, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            TakeCurrent(POI.POIRuntimeStatusKind.AdminStatus,
                        EntityType,
                        EntityId,
                        value,
                        Timestamp,
                        "admin status");

        }

        /// <summary>
        /// The current charging power, voltage and remaining charging time of an electric charging point as the energy
        /// status of its EVSE. DATEX II does not tell the power still available: none while it charges, else the
        /// maximum power of the EVSE.
        /// </summary>
        private void ImportEnergyStatus(POI.EVSE_Id                  EVSEId,
                                        ElectricChargingPointStatus  Status,
                                        DateTimeOffset               Timestamp)
        {

            if (!Status.CurrentChargingPower.HasValue)
                return;

            var used      = Status.CurrentChargingPower.Value;
            var maxPower  = History.Head.RoamingNetwork.GetChargingStationOperatorById(EVSEId.OperatorId) is POI.ChargingStationOperator chargingStationOperator &&
                            chargingStationOperator.TryGetEVSE(EVSEId, out var evse)
                                ? evse.MaxPower
                                : null;

            var energyStatus = new POI.EnergyInfo(used,
                                                  used.Value > 0
                                                      ? Watt.FromW(0)
                                                      : maxPower ?? Watt.FromW(0),
                                                  CurrentVoltage:         Status.CurrentVoltage,
                                                  RemainingChargingTime:  Status.RemainingChargingTime);

            TakeCurrent(POI.POIRuntimeStatusKind.EnergyStatus,
                        nameof(POI.EVSE),
                        EVSEId.ToString(),
                        POI.POIEnergyStatus.ToText(energyStatus),
                        Timestamp,
                        "energy status");

        }

        /// <summary>
        /// Take the given value as the current runtime status of the given kind. What a publication tells is the current
        /// one: a later one here, e.g. the default status of an EVSE just imported, is followed right after.
        /// </summary>
        private void TakeCurrent(POI.POIRuntimeStatusKind  Kind,
                                 String                    EntityType,
                                 String                    EntityId,
                                 String                    Value,
                                 DateTimeOffset            Timestamp,
                                 String                    Description)
        {

            if (History.RuntimeStatus.TryGet(Kind, EntityType, EntityId, out var current) &&
                current.Timestamp >= Timestamp &&
                !String.Equals(current.Value, Value, StringComparison.OrdinalIgnoreCase))
            {
                Timestamp = current.Timestamp.AddMilliseconds(1);
            }

            var result = History.RuntimeStatus.TryUpdate(Kind,
                                                         EntityType,
                                                         EntityId,
                                                         Value,
                                                         Timestamp,
                                                         out _,
                                                         out var errorResponse,
                                                         DataSource: "DATEX II");

            if (result == POI.POIStatusUpdateResult.Refused)
                RaiseError($"The {Description} of the {EntityType} '{EntityId}' can not be taken: {errorResponse}", []);

        }

        /// <summary>
        /// Plan the planned statuses of the given refill point status, e.g. reservations, as statuses of the EVSE from
        /// their start. When one ends without another starting then, the EVSE is planned to have its current status
        /// again. Planned statuses already started are not taken: the current status tells.
        /// </summary>
        private void ImportPlannedStatus(POI.EVSE_Id        EVSEId,
                                         RefillPointStatus   RefillPointStatus)
        {

            var now      = Timestamp.Now;
            var planned  = RefillPointStatus.PlannedRefillPointStatus.
                               Where  (status => status.OverallPeriod.OverallStartTime > now).
                               OrderBy(status => status.OverallPeriod.OverallStartTime).
                               ToArray();

            foreach (var status in planned)
            {

                Plan(EVSEId, status.Status.ToPOI3Status(), status.OverallPeriod.OverallStartTime);

                if (status.OverallPeriod.OverallEndTime is DateTimeOffset end &&
                    end > status.OverallPeriod.OverallStartTime &&
                    !planned.Any(next => next.OverallPeriod.OverallStartTime == end))
                {
                    Plan(EVSEId, RefillPointStatus.Status.ToPOI3Status(), end);
                }

            }

        }

        /// <summary>
        /// The given energy rate updates of a refill point or station as the price status of the EVSE or charging station:
        /// for every rate, its new prices as the new prices of that charging tariff. None, after there were some, are no
        /// new prices.
        /// </summary>
        private void ImportPriceStatus(String                         EntityType,
                                       String                         EntityId,
                                       POI.ChargingStationOperator_Id  OperatorId,
                                       IEnumerable<EnergyRateUpdate>  EnergyRateUpdates,
                                       DateTimeOffset                 Timestamp)
        {

            var updates   = EnergyRateUpdates.ToArray();

            var warnings  = new List<String>();
            var prices    = new List<POI.ChargingTariffPrices>();

            foreach (var update in updates)
            {

                var tariffId = POI3Mapper.ToPOI3TariffId(update.EnergyRateReference.Id, OperatorId);

                if (!tariffId.HasValue)
                {
                    warnings.Add($"The energy rate '{update.EnergyRateReference.Id}' of an energy rate update is no charging tariff identification!");
                    continue;
                }

                var elements = POI3Mapper.ElementsOf(update.EnergyPrices, update.EnergyRateReference.Id, warnings);

                if (elements.Count > 0 && !prices.Any(price => price.ChargingTariffId == tariffId.Value))
                    prices.Add(new POI.ChargingTariffPrices(tariffId.Value, elements));

            }

            if (warnings.Count > 0)
                RaiseError($"The new prices of the {EntityType} '{EntityId}' are imported partly.", warnings);

            // No new prices, and none before: nothing to tell.
            if (prices.Count == 0 &&
                !History.RuntimeStatus.TryGet(POI.POIRuntimeStatusKind.PriceStatus, EntityType, EntityId, out _))
            {
                return;
            }

            var result = History.RuntimeStatus.TryUpdate(POI.POIRuntimeStatusKind.PriceStatus,
                                                         EntityType,
                                                         EntityId,
                                                         new POI.ChargingPrices(prices).Text,
                                                         updates.Length > 0
                                                             ? updates.Max(update => update.LastUpdated)
                                                             : Timestamp,
                                                         out _,
                                                         out var errorResponse,
                                                         DataSource: "DATEX II");

            if (result == POI.POIStatusUpdateResult.Refused)
                RaiseError($"The new prices of the {EntityType} '{EntityId}' can not be taken: {errorResponse}", []);

        }

        private void Plan(POI.EVSE_Id         EVSEId,
                          POI.EVSEStatusType  Status,
                          DateTimeOffset      At)
        {

            var result = History.RuntimeStatus.TryUpdate(POI.POIRuntimeStatusKind.Status,
                                                         nameof(POI.EVSE),
                                                         EVSEId.ToString(),
                                                         Status.ToString(),
                                                         At,
                                                         out _,
                                                         out var errorResponse,
                                                         DataSource: "DATEX II");

            if (result == POI.POIStatusUpdateResult.Refused)
                RaiseError($"The planned status of the EVSE '{EVSEId}' can not be taken: {errorResponse}", []);

        }

        #endregion

        #region (private) ChangeOperator(OperatorId, Description, CreatedAt, Change)

        /// <summary>
        /// Change the JSON of the given operator in the head, which the change is given as well, and commit the difference, signed.
        /// </summary>
        private void ChangeOperator(POI.ChargingStationOperator_Id                                     OperatorId,
                                    String                                                             Description,
                                    DateTimeOffset                                                     CreatedAt,
                                    Func<POI.RoamingNetwork, JObject, ICollection<String>, Boolean>    Change)
        {

            var head          = History.Head.RoamingNetwork;
            var networkJSON   = head.ToJSON(IncludeETags: false);   // changed, so without the ETags of the head
            var operatorJSON  = (networkJSON["chargingStationOperators"] as JArray ?? []).
                                    OfType<JObject>().
                                    FirstOrDefault(op => op["@id"]?.Value<String>() == OperatorId.ToString());

            if (operatorJSON is null)
            {
                RaiseError($"The operator '{OperatorId}' is not in the roaming network: adding it is up to its operator!", []);
                return;
            }

            var warnings = new List<String>();

            if (!Change(head, operatorJSON, warnings))
            {
                if (warnings.Count > 0)
                    RaiseError($"{Description} can not be imported!", warnings);
                return;
            }

            if (!POI.RoamingNetwork.TryParse(networkJSON, out var desired, out var parseError))
            {
                RaiseError($"{Description} is no valid roaming network data: {parseError}", warnings);
                return;
            }

            // The difference: a merge whose left side is its base.
            if (!POI.RoamingNetwork.TryMerge(head, head, desired, POI.ChangeSet_Id.NewRandom(), out _, out var difference, out var conflicts))
            {
                RaiseError($"{Description} can not be imported: {String.Join(" ", conflicts.Select(conflict => conflict.Description))}", warnings);
                return;
            }

            if (difference.Changes.Length == 0)
            {
                if (warnings.Count > 0)
                    RaiseError($"{Description} was imported partly.", warnings);
                return;
            }

            var changeSet  = head.CreateChangeSet(difference.Changes,
                                                  new ImmutableI18NString(I18NString.Create(Languages.en, Description)),
                                                  CreatedAt: CreatedAt);

            var result     = History.Commit(Sign(changeSet), VerifyChangeSetSignature);

            if (!result.IsCommitted)
                RaiseError($"{Description} can not be committed: {result.ErrorResponse}", warnings);

            else if (warnings.Count > 0)
                RaiseError($"{Description} was imported partly.", warnings);

        }

        #endregion

        #region (private) RaiseError(Description, Warnings)

        private void RaiseError(String               Description,
                                IEnumerable<String>  Warnings)
        {
            try
            {
                OnError?.Invoke(this, Description, Warnings);
            }
            catch
            { }
        }

        #endregion

    }

}
