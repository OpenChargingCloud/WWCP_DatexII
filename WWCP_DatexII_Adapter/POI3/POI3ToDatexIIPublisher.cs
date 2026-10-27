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

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

using POI = cloud.charging.open.protocols.WWCP.POI;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.POI3
{

    /// <summary>
    /// Something could not be mapped, published or imported.
    /// </summary>
    /// <param name="Sender">The publisher or importer.</param>
    /// <param name="Description">What went wrong.</param>
    /// <param name="Warnings">What could not be mapped, if anything.</param>
    public delegate void OnPOI3DatexIIErrorDelegate(Object               Sender,
                                                    String               Description,
                                                    IEnumerable<String>  Warnings);

    /// <summary>
    /// A new table publication, e.g. to push it to a national access point.
    /// </summary>
    /// <param name="Sender">The publisher.</param>
    /// <param name="Publication">The table publication.</param>
    public delegate Task OnTablePublicationDelegate(POI3ToDatexIIPublisher                Sender,
                                                    EnergyInfrastructureTablePublication  Publication);

    /// <summary>
    /// A new status publication, e.g. to push it to a national access point.
    /// </summary>
    /// <param name="Sender">The publisher.</param>
    /// <param name="Publication">The status publication.</param>
    /// <param name="IsDelta">Whether it has only what changed, rather than the status of every refill point (as Publication.IsDelta).</param>
    public delegate Task OnStatusPublicationDelegate(POI3ToDatexIIPublisher                 Sender,
                                                     EnergyInfrastructureStatusPublication  Publication,
                                                     Boolean                                IsDelta);


    /// <summary>
    /// The CPO side: a POI3 history becomes DATEX II Energy Infrastructure table and status publications, e.g. for
    /// the national access point a CPO has to publish to under AFIR.
    /// </summary>
    /// <remarks>
    /// At the start, the head becomes the current table publication, and the status of every EVSE the current status
    /// publication. Every new commit maps the head again: a table with other content gets the revision of the commit as
    /// its version, is published, and so is the full status, whose references carry the new versions. Every change of
    /// the status or admin status of an EVSE renews the current status publication, and publishes a delta with that EVSE.
    /// Serving the current publications, e.g. over HTTP, and pushing them is up to the subscribers of the events.
    /// The work is done one after the other, in the order it came.
    /// </remarks>
    public sealed class POI3ToDatexIIPublisher : IAsyncDisposable
    {

        #region Data

        private readonly Channel<Func<Task>>  work         = Channel.CreateUnbounded<Func<Task>>(new UnboundedChannelOptions { SingleReader = true });
        private          XElement?            publishedSites;
        private          Task?                worker;

        /// <summary>
        /// When a status delta of an EVSE was published last, and the EVSEs whose energy status delta is planned.
        /// </summary>
        private readonly ConcurrentDictionary<POI.EVSE_Id, DateTimeOffset>  lastDeltas            = new ();

        /// <summary>
        /// The schedule of the next change of an exceptional closing or opening: its generation, and until when changes
        /// are published.
        /// </summary>
        private          Int32                                              openingGeneration;
        private          DateTimeOffset                                     openingCheckedUntil   = Timestamp.Now;

        /// <summary>
        /// The longest the publisher waits for the next change of an exceptional closing or opening at once.
        /// </summary>
        private static readonly TimeSpan                                    maxOpeningWait        = TimeSpan.FromHours(12);
        private readonly ConcurrentDictionary<POI.EVSE_Id, Byte>            plannedEnergyDeltas   = new ();

        /// <summary>
        /// The EVSEs of the next grouped status delta, whether it is planned, and when the last one was published; how
        /// many deltas were published since the last full status, and the schedule of the next full status. Only the
        /// worker touches them.
        /// </summary>
        private readonly HashSet<POI.EVSE_Id>                               pendingDeltaEVSEs     = [];
        private          Boolean                                            deltaPlanned;
        private          DateTimeOffset                                     lastGroupedDelta      = DateTimeOffset.MinValue;
        private          UInt32                                             deltasSinceSnapshot;
        private          Int32                                              snapshotGeneration;

        /// <summary>
        /// The default least time between two status deltas of an EVSE for a changed energy status: one minute.
        /// </summary>
        public static readonly TimeSpan DefaultEnergyStatusDeltaInterval = TimeSpan.FromMinutes(1);

        /// <summary>
        /// The Mobilithek profile: status deltas at most once a second, the changes of a second grouped.
        /// </summary>
        public static readonly TimeSpan MobilithekStatusDeltaInterval    = TimeSpan.FromSeconds(1);

        /// <summary>
        /// The Mobilithek profile: a full status at least every six hours...
        /// </summary>
        public static readonly TimeSpan MobilithekSnapshotInterval       = TimeSpan.FromHours(6);

        /// <summary>
        /// ...and after at most 3500 status deltas.
        /// </summary>
        public const           UInt32   MobilithekMaxDeltasPerSnapshot   = 3500;

        #endregion

        #region Properties

        /// <summary>
        /// The history published.
        /// </summary>
        public POI.RoamingNetworkHistory                       History                     { get; }

        /// <summary>
        /// The creator of the publications, e.g. the national access point identification of the CPO.
        /// </summary>
        public InternationalIdentifier                         PublicationCreator          { get; }

        /// <summary>
        /// The language of the publications.
        /// </summary>
        public Languages                                       Language                    { get; }

        /// <summary>
        /// The identification of the table.
        /// </summary>
        public String                                          TableId                     { get; }

        /// <summary>
        /// The operators published; all, unless given.
        /// </summary>
        public IReadOnlySet<POI.ChargingStationOperator_Id>?   OperatorIds                 { get; }

        /// <summary>
        /// Tells the station identification of the Bundesnetzagentur of a charging station.
        /// </summary>
        public StationIdBNetzADelegate?                        StationIdBNetzA             { get; }

        /// <summary>
        /// The least time between two status deltas of an EVSE for a changed energy status: its charging power,
        /// voltage and remaining charging time change often, e.g. every few seconds while charging. A change of its
        /// status is published at once, and with it the energy status as it is then. Zero publishes every change.
        /// </summary>
        public TimeSpan                                        EnergyStatusDeltaInterval   { get; }

        /// <summary>
        /// The least time between two status deltas: the changes until then are grouped into one delta. Zero, the
        /// default, publishes every delta at once; the Mobilithek profile wants one second (MobilithekStatusDeltaInterval).
        /// </summary>
        public TimeSpan                                        StatusDeltaInterval         { get; }

        /// <summary>
        /// The longest time between two full status publications: when it is over, the full status is published again,
        /// though nothing changed. None by default; the Mobilithek profile wants six hours (MobilithekSnapshotInterval).
        /// </summary>
        public TimeSpan?                                       SnapshotInterval            { get; }

        /// <summary>
        /// The most status deltas between two full status publications: after as many, the full status is published
        /// again. None by default; the Mobilithek profile wants 3500 (MobilithekMaxDeltasPerSnapshot).
        /// </summary>
        public UInt32?                                         MaxDeltasPerSnapshot        { get; }

        /// <summary>
        /// The current table publication, once started.
        /// </summary>
        public EnergyInfrastructureTablePublication?           CurrentTablePublication     { get; private set; }

        /// <summary>
        /// The current status publication with the status of every EVSE, once started.
        /// </summary>
        public EnergyInfrastructureStatusPublication?          CurrentStatusPublication    { get; private set; }

        /// <summary>
        /// The version of the current table: the revision of the commit it was made of.
        /// </summary>
        public String?                                         TableVersion                { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// A new table publication.
        /// </summary>
        public event OnTablePublicationDelegate?   OnTablePublication;

        /// <summary>
        /// A new status publication: a full one or a delta.
        /// </summary>
        public event OnStatusPublicationDelegate?  OnStatusPublication;

        /// <summary>
        /// Something could not be mapped or published.
        /// </summary>
        public event OnPOI3DatexIIErrorDelegate?   OnError;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Publish the given history as DATEX II Energy Infrastructure publications.
        /// </summary>
        /// <param name="History">A history of a roaming network.</param>
        /// <param name="PublicationCreator">The creator of the publications, e.g. the national access point identification of the CPO.</param>
        /// <param name="Language">The language of the publications; English by default.</param>
        /// <param name="TableId">The identification of the table; the roaming network identification by default.</param>
        /// <param name="OperatorIds">The operators to publish; all, unless given.</param>
        /// <param name="StationIdBNetzA">Tells the station identification of the Bundesnetzagentur of a charging station.</param>
        /// <param name="EnergyStatusDeltaInterval">The least time between two status deltas of an EVSE for a changed energy status; one minute by default, zero for every change.</param>
        /// <param name="StatusDeltaInterval">The least time between two status deltas, the changes until then grouped; zero by default, for every delta at once.</param>
        /// <param name="SnapshotInterval">The longest time between two full status publications; none by default.</param>
        /// <param name="MaxDeltasPerSnapshot">The most status deltas between two full status publications; none by default.</param>
        public POI3ToDatexIIPublisher(POI.RoamingNetworkHistory                      History,
                                      InternationalIdentifier                        PublicationCreator,
                                      Languages?                                     Language                    = null,
                                      String?                                        TableId                     = null,
                                      IEnumerable<POI.ChargingStationOperator_Id>?   OperatorIds                 = null,
                                      StationIdBNetzADelegate?                       StationIdBNetzA             = null,
                                      TimeSpan?                                      EnergyStatusDeltaInterval   = null,
                                      TimeSpan?                                      StatusDeltaInterval         = null,
                                      TimeSpan?                                      SnapshotInterval            = null,
                                      UInt32?                                        MaxDeltasPerSnapshot        = null)
        {

            if (EnergyStatusDeltaInterval < TimeSpan.Zero)
                throw new ArgumentException("The energy status delta interval must not be negative!", nameof(EnergyStatusDeltaInterval));

            if (StatusDeltaInterval < TimeSpan.Zero)
                throw new ArgumentException("The status delta interval must not be negative!", nameof(StatusDeltaInterval));

            if (SnapshotInterval <= TimeSpan.Zero)
                throw new ArgumentException("The snapshot interval must be positive!", nameof(SnapshotInterval));

            if (MaxDeltasPerSnapshot == 0)
                throw new ArgumentException("The most status deltas per snapshot must be positive!", nameof(MaxDeltasPerSnapshot));

            this.History             = History;
            this.PublicationCreator  = PublicationCreator;
            this.Language            = Language ?? Languages.en;
            this.TableId             = TableId  ?? History.RoamingNetworkId.ToString();
            this.OperatorIds         = OperatorIds?.ToHashSet();
            this.StationIdBNetzA     = StationIdBNetzA;
            this.EnergyStatusDeltaInterval = EnergyStatusDeltaInterval ?? DefaultEnergyStatusDeltaInterval;
            this.StatusDeltaInterval       = StatusDeltaInterval       ?? TimeSpan.Zero;
            this.SnapshotInterval          = SnapshotInterval;
            this.MaxDeltasPerSnapshot      = MaxDeltasPerSnapshot;

        }

        #endregion


        #region StartAsync()

        /// <summary>
        /// Publish the head, and every change from now on; returns once the head is published.
        /// </summary>
        public async Task StartAsync()
        {

            if (worker is not null)
                throw new InvalidOperationException("The publisher has been started already!");

            History.OnCommitAnnouncement           += Announced;
            History.RuntimeStatus.OnStatusUpdate   += StatusUpdated;
            History.RuntimeStatus.OnStatusPlanned  += StatusPlanned;

            worker = Task.Run(WorkAsync);

            var published = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

            work.Writer.TryWrite(async () => {
                try
                {
                    await PublishTableAsync();
                }
                finally
                {
                    published.TrySetResult();
                }
            });

            await published.Task;

        }

        #endregion

        #region DisposeAsync()

        /// <summary>
        /// Stop publishing, after what is waiting.
        /// </summary>
        public async ValueTask DisposeAsync()
        {

            History.OnCommitAnnouncement           -= Announced;
            History.RuntimeStatus.OnStatusUpdate   -= StatusUpdated;
            History.RuntimeStatus.OnStatusPlanned  -= StatusPlanned;

            Interlocked.Increment(ref openingGeneration);
            Interlocked.Increment(ref snapshotGeneration);

            work.Writer.TryComplete();

            if (worker is not null)
                await worker;

        }

        #endregion


        #region (private) Announced(Announcement) / StatusUpdated(StatusUpdate)

        private void Announced(POI.RoamingNetworkCommitAnnouncement Announcement)

            => work.Writer.TryWrite(PublishTableAsync);

        // Raised while the status schedule of the EVSE is locked: only queued here.
        private void StatusUpdated(POI.POIStatusUpdate StatusUpdate)
        {

            // New prices of a charging pool, station or EVSE: a delta of its EVSEs at once.
            if (StatusUpdate.Kind == POI.POIRuntimeStatusKind.PriceStatus)
            {
                work.Writer.TryWrite(() => PublishStatusAsync(EVSEIdsOf(StatusUpdate.EntityType, StatusUpdate.EntityId)));
                return;
            }

            if (StatusUpdate.EntityType != nameof(POI.EVSE) ||
                POI.EVSE_Id.TryParse(StatusUpdate.EntityId) is not POI.EVSE_Id evseId ||
                (OperatorIds is not null && !OperatorIds.Contains(evseId.OperatorId)))
            {
                return;
            }

            if (StatusUpdate.Kind is POI.POIRuntimeStatusKind.Status or POI.POIRuntimeStatusKind.AdminStatus)
                work.Writer.TryWrite(() => PublishStatusAsync(evseId));

            else if (StatusUpdate.Kind == POI.POIRuntimeStatusKind.EnergyStatus)
                PlanEnergyStatusDelta(evseId);

        }

        // A planned status or admin status of an EVSE, e.g. a reservation: its planned statuses changed.
        private void StatusPlanned(POI.POIStatusUpdate StatusUpdate)
        {
            if (StatusUpdate.EntityType == nameof(POI.EVSE) &&
                StatusUpdate.Kind is POI.POIRuntimeStatusKind.Status or POI.POIRuntimeStatusKind.AdminStatus &&
                POI.EVSE_Id.TryParse(StatusUpdate.EntityId) is POI.EVSE_Id evseId &&
                (OperatorIds is null || OperatorIds.Contains(evseId.OperatorId)))
            {
                work.Writer.TryWrite(() => PublishStatusAsync(evseId));
            }
        }

        /// <summary>
        /// Plan a status delta of the given EVSE for its changed energy status: at once, when its last delta is at least
        /// the energy status delta interval ago, else when it will be. One planned delta publishes the energy status as
        /// it is then, so further changes until then need none of their own.
        /// </summary>
        private void PlanEnergyStatusDelta(POI.EVSE_Id EVSEId)
        {

            if (!plannedEnergyDeltas.TryAdd(EVSEId, 0))
                return;

            var wait = lastDeltas.TryGetValue(EVSEId, out var lastDelta)
                           ? lastDelta + EnergyStatusDeltaInterval - Timestamp.Now
                           : TimeSpan.Zero;

            void Publish()
                => work.Writer.TryWrite(() => {
                       plannedEnergyDeltas.TryRemove(EVSEId, out _);
                       return PublishStatusAsync(EVSEId);
                   });

            if (wait <= TimeSpan.Zero)
                Publish();
            else
                _ = Task.Delay(wait).ContinueWith(_ => Publish(), TaskScheduler.Default);

        }

        #endregion

        #region (private) WorkAsync()

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
                    RaiseError($"Publishing failed: {e.Message}", []);
                }
            }
        }

        #endregion

        #region (private) PublishTableAsync()

        /// <summary>
        /// Map the head; when the sites changed, publish them as a new version of the table, and the full status.
        /// </summary>
        private async Task PublishTableAsync()
        {

            var head      = History.Head;
            var warnings  = new List<String>();

            var table     = POI3Mapper.ToTablePublication(head.RoamingNetwork,
                                                          PublicationCreator,
                                                          Language,
                                                          TableId,
                                                          head.Commit.Revision.ToString(),
                                                          warnings,
                                                          OperatorIds,
                                                          StationIdBNetzA: StationIdBNetzA);

            if (warnings.Count > 0)
                RaiseError($"Revision {head.Commit.Revision} is published partly.", warnings);

            // The sites, without the version of the table: a commit of something not published changes nothing here.
            var sites     = new XElement("sites", table.EnergyInfrastructureTables.SelectMany(table => table.EnergyInfrastructureSites).Select(site => site.ToXML()));

            if (publishedSites is not null && XNode.DeepEquals(publishedSites, sites))
                return;

            publishedSites           = sites;
            TableVersion             = head.Commit.Revision.ToString();
            CurrentTablePublication  = table;

            await Raise(OnTablePublication, handler => handler(this, table));

            // The full status tells every opening change until now.
            openingCheckedUntil      = Timestamp.Now;

            CurrentStatusPublication = POI3Mapper.ToStatusPublication(head.RoamingNetwork,
                                                                      PublicationCreator,
                                                                      Language,
                                                                      TableId,
                                                                      TableVersion,
                                                                      OperatorIds);

            await Raise(OnStatusPublication, handler => handler(this, CurrentStatusPublication, false));

            SnapshotPublished();

            ScheduleOpeningChange();

        }

        #endregion

        #region (private) ScheduleOpeningChange() / PublishOpeningChangesAsync()

        /// <summary>
        /// Wake up when the next exceptional closing or opening of a published charging pool begins or ends, so that the
        /// opening status of its site changes in time: of its opening times as it is at runtime, also those its templates
        /// give it. The next one after the time opening changes were published until, not after now: one in between, e.g.
        /// as the delay ended a little early, would neither be published nor be waited for.
        /// </summary>
        private void ScheduleOpeningChange()
        {

            var from        = openingCheckedUntil;
            var generation  = Interlocked.Increment(ref openingGeneration);

            var next        = History.Head.RoamingNetwork.ChargingStationOperators.
                                  Where     (chargingStationOperator => OperatorIds is null || OperatorIds.Contains(chargingStationOperator.Id)).
                                  SelectMany(chargingStationOperator => chargingStationOperator.EffectiveChargingPools).
                                  Select    (chargingPool            => POI3Mapper.NextOpeningChangeOf(chargingPool, from)).
                                  Where     (timestamp               => timestamp.HasValue).
                                  Min();

            if (!next.HasValue)
                return;

            // A millisecond late rather than early; at once when it passed already.
            var wait = next.Value - Timestamp.Now + TimeSpan.FromMilliseconds(1);

            if (wait < TimeSpan.Zero)
                wait = TimeSpan.Zero;

            if (wait > maxOpeningWait)
                wait = maxOpeningWait;

            _ = Task.Delay(wait).ContinueWith(_ => {
                    if (generation == Volatile.Read(ref openingGeneration))
                        work.Writer.TryWrite(PublishOpeningChangesAsync);
                }, TaskScheduler.Default);

        }

        /// <summary>
        /// Publish a delta of the EVSEs of the charging pools whose exceptional closing or opening began or ended since
        /// the last time, and wait for the next change: also when publishing it failed, so that a failure does not end
        /// the opening changes to come.
        /// </summary>
        private async Task PublishOpeningChangesAsync()
        {

            var since    = openingCheckedUntil;
            var now      = Timestamp.Now;

            openingCheckedUntil = now;

            try
            {

                var evseIds  = History.Head.RoamingNetwork.ChargingStationOperators.
                                   Where     (chargingStationOperator => OperatorIds is null || OperatorIds.Contains(chargingStationOperator.Id)).
                                   SelectMany(chargingStationOperator => chargingStationOperator.EffectiveChargingPools).
                                   Where     (chargingPool            => chargingPool.OpeningTimes is not null &&
                                                                         chargingPool.OpeningTimes.ExceptionalClosings.Concat(chargingPool.OpeningTimes.ExceptionalOpenings).
                                                                             SelectMany(period => new[] { period.Begin, period.End }).
                                                                             Any(timestamp => timestamp > since && timestamp <= now)).
                                   SelectMany(chargingPool            => chargingPool.ChargingStations.SelectMany(chargingStation => chargingStation.EVSEs)).
                                   Select    (evse                    => evse.Id).
                                   ToArray();

                await PublishStatusAsync(evseIds);

            }
            finally
            {
                ScheduleOpeningChange();
            }

        }

        #endregion

        #region (private) EVSEIdsOf(EntityType, EntityId)

        /// <summary>
        /// The published EVSEs of the given charging pool, charging station or EVSE in the head.
        /// </summary>
        private IReadOnlyList<POI.EVSE_Id> EVSEIdsOf(String  EntityType,
                                                    String  EntityId)
        {

            var head = History.Head.RoamingNetwork;

            IEnumerable<POI.EVSE_Id> evseIds = EntityType switch {

                nameof(POI.EVSE)             => POI.EVSE_Id.TryParse(EntityId) is POI.EVSE_Id evseId
                                                    ? [ evseId ]
                                                    : [],

                nameof(POI.ChargingStation)  => POI.ChargingStation_Id.TryParse(EntityId) is POI.ChargingStation_Id stationId &&
                                                head.TryGetChargingStationOperatorById(stationId.OperatorId, out var stationOperator) &&
                                                stationOperator.TryGetChargingStation(stationId, out var station)
                                                    ? station.EVSEs.Select(evse => evse.Id)
                                                    : [],

                nameof(POI.ChargingPool)     => POI.ChargingPool_Id.TryParse(EntityId) is POI.ChargingPool_Id poolId &&
                                                head.TryGetChargingStationOperatorById(poolId.OperatorId, out var poolOperator) &&
                                                poolOperator.TryGetChargingPool(poolId, out var pool)
                                                    ? pool.ChargingStations.SelectMany(chargingStation => chargingStation.EVSEs).Select(evse => evse.Id)
                                                    : [],

                _                            => []

            };

            return evseIds.Where(evseId => OperatorIds is null || OperatorIds.Contains(evseId.OperatorId)).ToArray();

        }

        #endregion

        #region (private) PublishStatusAsync(EVSEId)

        /// <summary>
        /// Renew the full status, and publish the status of the given EVSE as a delta.
        /// </summary>
        private Task PublishStatusAsync(POI.EVSE_Id EVSEId)

            => PublishStatusAsync([ EVSEId ]);

        /// <summary>
        /// Renew the full status, and publish the status of the given EVSEs as one delta: at once, or grouped with the
        /// others until the status delta interval since the last delta is over.
        /// </summary>
        private async Task PublishStatusAsync(IReadOnlyList<POI.EVSE_Id> EVSEIds)
        {

            if (TableVersion is null || EVSEIds.Count == 0)
                return;

            CurrentStatusPublication = POI3Mapper.ToStatusPublication(History.Head.RoamingNetwork,
                                                                      PublicationCreator,
                                                                      Language,
                                                                      TableId,
                                                                      TableVersion,
                                                                      OperatorIds);

            if (StatusDeltaInterval == TimeSpan.Zero)
            {
                await PublishDeltaAsync(EVSEIds);
                return;
            }

            pendingDeltaEVSEs.UnionWith(EVSEIds);

            if (deltaPlanned)
                return;

            var wait = lastGroupedDelta + StatusDeltaInterval - Timestamp.Now;

            if (wait <= TimeSpan.Zero)
            {
                await PublishPendingDeltaAsync();
                return;
            }

            deltaPlanned = true;

            _ = Task.Delay(wait).ContinueWith(_ => work.Writer.TryWrite(PublishPendingDeltaAsync), TaskScheduler.Default);

        }

        /// <summary>
        /// Publish the status of the EVSEs grouped since the last delta as one delta, as it is now.
        /// </summary>
        private Task PublishPendingDeltaAsync()
        {

            deltaPlanned = false;

            var evseIds = pendingDeltaEVSEs.ToArray();
            pendingDeltaEVSEs.Clear();

            return PublishDeltaAsync(evseIds);

        }

        /// <summary>
        /// Publish the status of the given EVSEs as one delta; after the most deltas per snapshot, the full status.
        /// </summary>
        private async Task PublishDeltaAsync(IReadOnlyList<POI.EVSE_Id> EVSEIds)
        {

            if (TableVersion is null || EVSEIds.Count == 0)
                return;

            var head  = History.Head.RoamingNetwork;

            var delta = POI3Mapper.ToStatusPublication(head,
                                                       PublicationCreator,
                                                       Language,
                                                       TableId,
                                                       TableVersion,
                                                       OperatorIds,
                                                       EVSEIds).
                            // Its Mobilithek JSON is a deltaPush.
                            AsDelta();

            if (delta.EnergyInfrastructureSiteStatus.Any())
            {

                var now = Timestamp.Now;

                foreach (var evseId in EVSEIds)
                    lastDeltas[evseId] = now;

                lastGroupedDelta = now;

                await Raise(OnStatusPublication, handler => handler(this, delta, true));

                if (MaxDeltasPerSnapshot.HasValue && ++deltasSinceSnapshot >= MaxDeltasPerSnapshot.Value)
                    await PublishSnapshotAsync();

            }

        }

        #endregion

        #region (private) PublishSnapshotAsync() / SnapshotPublished()

        /// <summary>
        /// Publish the full status again, as it is now: it tells nothing the deltas did not.
        /// </summary>
        private async Task PublishSnapshotAsync()
        {

            if (TableVersion is null)
                return;

            CurrentStatusPublication = POI3Mapper.ToStatusPublication(History.Head.RoamingNetwork,
                                                                      PublicationCreator,
                                                                      Language,
                                                                      TableId,
                                                                      TableVersion,
                                                                      OperatorIds);

            await Raise(OnStatusPublication, handler => handler(this, CurrentStatusPublication, false));

            SnapshotPublished();

        }

        /// <summary>
        /// The full status was published: count the deltas anew, and publish it again when the snapshot interval is over.
        /// </summary>
        private void SnapshotPublished()
        {

            deltasSinceSnapshot = 0;

            var generation = Interlocked.Increment(ref snapshotGeneration);

            if (SnapshotInterval is TimeSpan interval)
                _ = Task.Delay(interval).ContinueWith(_ => {
                        if (generation == Volatile.Read(ref snapshotGeneration))
                            work.Writer.TryWrite(PublishSnapshotAsync);
                    }, TaskScheduler.Default);

        }

        #endregion

        #region (private) Raise(Event, Invoke)

        private async Task Raise<TDelegate>(TDelegate?             Event,
                                            Func<TDelegate, Task>  Invoke)

            where TDelegate : Delegate

        {

            if (Event is null)
                return;

            foreach (var handler in Event.GetInvocationList().Cast<TDelegate>())
            {
                try
                {
                    await Invoke(handler);
                }
                catch (Exception e)
                {
                    RaiseError($"A subscriber of the publications failed: {e.Message}", []);
                }
            }

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
