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

using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Aegir;
using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationExtension;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

using POI        = cloud.charging.open.protocols.WWCP.POI;
using Address    = org.GraphDefined.Vanaheimr.Illias.Address;
using DXAddress  = cloud.charging.open.protocols.DatexII.v3.LocationExtension.Address;
using DXImage    = cloud.charging.open.protocols.DatexII.v3.Facilities.Image;
using DXTimeZone = cloud.charging.open.protocols.DatexII.v3.Facilities.TimeZone;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.POI3
{

    /// <summary>
    /// Tells the station identification of the Bundesnetzagentur of a charging station, which POI3 does not know.
    /// </summary>
    /// <param name="ChargingStation">A charging station.</param>
    public delegate String? StationIdBNetzADelegate(POI.ChargingStation ChargingStation);


    /// <summary>
    /// The mapping between the charging infrastructure and tariffs of a POI3 roaming network and DATEX II v3
    /// Energy Infrastructure: charging pools and sites, charging stations and stations, EVSEs and electric charging
    /// points, charging connectors and connectors, charging tariffs and energy rates, and the status of EVSEs and
    /// refill points. See README.md for what does not map.
    /// </summary>
    public static class POI3Mapper
    {

        #region Identifications and versions

        /// <summary>
        /// The version of a facility object: the milliseconds since 1970 of its last change, so a later change has a
        /// greater version, and the status refers to the version of the table it belongs to.
        /// </summary>
        /// <param name="LastChange">The last change of an entity.</param>
        public static String VersionOf(DateTimeOffset LastChange)

            => LastChange.ToUnixTimeMilliseconds().ToString();

        /// <summary>
        /// A suffix of a POI3 identification made of the given DATEX II identification: its letters and digits,
        /// at most 30 of them, e.g. of a GUID.
        /// </summary>
        /// <param name="Id">A DATEX II identification.</param>
        public static String ToPOI3Suffix(String Id)
        {

            var suffix = new String(Id.Where(Char.IsAsciiLetterOrDigit).ToArray());

            return suffix.Length > 30
                       ? suffix[..30]
                       : suffix;

        }

        /// <summary>
        /// The charging pool of the given site of the given operator: its identification, when it is one of the
        /// operator, else one made of its letters and digits.
        /// </summary>
        public static POI.ChargingPool_Id? ToPOI3PoolId(String                          SiteId,
                                                       POI.ChargingStationOperator_Id  OperatorId)
        {

            var poolId = POI.ChargingPool_Id.TryParse(SiteId);

            if (poolId.HasValue && poolId.Value.OperatorId == OperatorId)
                return poolId;

            var suffix = ToPOI3Suffix(SiteId);

            return suffix.Length > 0
                       ? POI.ChargingPool_Id.TryParse(OperatorId, suffix)
                       : null;

        }

        /// <summary>
        /// The charging station of the given station of the given operator: its identification, when it is one of the
        /// operator, else one made of its letters and digits.
        /// </summary>
        public static POI.ChargingStation_Id? ToPOI3StationId(String                          StationId,
                                                             POI.ChargingStationOperator_Id  OperatorId)
        {

            var stationId = POI.ChargingStation_Id.TryParse(StationId);

            if (stationId.HasValue && stationId.Value.OperatorId == OperatorId)
                return stationId;

            var suffix = ToPOI3Suffix(StationId);

            return suffix.Length > 0
                       ? POI.ChargingStation_Id.TryParse(OperatorId, suffix)
                       : null;

        }

        /// <summary>
        /// The EVSE of the given electric charging point of the given operator: its evseId, else its identification,
        /// when one is an EVSE identification of the operator, else one made of the letters and digits of its identification.
        /// </summary>
        public static POI.EVSE_Id? ToPOI3EVSEId(ElectricChargingPoint           ChargingPoint,
                                               POI.ChargingStationOperator_Id  OperatorId)
        {

            foreach (var text in new[] { ChargingPoint.EVSEId, ChargingPoint.Id })
            {
                if (text is not null &&
                    POI.EVSE_Id.TryParse(text) is POI.EVSE_Id evseId &&
                    evseId.OperatorId == OperatorId)
                {
                    return evseId;
                }
            }

            var suffix = ToPOI3Suffix(ChargingPoint.Id);

            return suffix.Length > 0
                       ? POI.EVSE_Id.TryParse(OperatorId, suffix)
                       : null;

        }

        /// <summary>
        /// The charging tariff of the given energy rate of the given operator: its identification, when it is one of
        /// the operator, else one with it as its suffix.
        /// </summary>
        public static POI.ChargingTariff_Id? ToPOI3TariffId(String                          RateId,
                                                           POI.ChargingStationOperator_Id  OperatorId)
        {

            var tariffId = POI.ChargingTariff_Id.TryParse(RateId);

            if (tariffId.HasValue && tariffId.Value.OperatorId == OperatorId)
                return tariffId;

            return POI.ChargingTariff_Id.TryParse(OperatorId, RateId);

        }

        /// <summary>
        /// The operator of the given site: the operator identification of its operator organisation, else the operator
        /// of the first EVSE identification of its charging points.
        /// </summary>
        public static POI.ChargingStationOperator_Id? OperatorIdOf(EnergyInfrastructureSite Site)
        {

            if (Site.Operator is OrganisationSpecification organisation &&
                POI.ChargingStationOperator_Id.TryParse(organisation.OperatorId ?? "") is POI.ChargingStationOperator_Id operatorId)
            {
                return operatorId;
            }

            foreach (var chargingPoint in Site.EnergyInfrastructureStations.SelectMany(station => station.RefillPoints).OfType<ElectricChargingPoint>())
            {
                if (POI.EVSE_Id.TryParse(chargingPoint.EVSEId ?? chargingPoint.Id) is POI.EVSE_Id evseId)
                    return evseId.OperatorId;
            }

            return null;

        }

        #endregion

        #region Status

        /// <summary>
        /// The refill point status of an EVSE: its admin status where it is not in operation, its status otherwise.
        /// </summary>
        public static RefillPointStatusEnum ToRefillPointStatus(POI.EVSEStatusType       Status,
                                                                POI.EVSEAdminStatusType  AdminStatus)

            => AdminStatus.ToString().ToLowerInvariant() switch {
                   "planned"       => RefillPointStatusEnum.Parse("planned"),
                   "indeployment"  => RefillPointStatusEnum.Parse("planned"),
                   "deleted"       => RefillPointStatusEnum.Parse("removed"),
                   "outofservice"  => RefillPointStatusEnum.Parse("inoperative"),
                   "blocked"       => RefillPointStatusEnum.Parse("blocked"),
                   "internaluse"   => RefillPointStatusEnum.Parse("internalUse"),
                   _               => ToRefillPointStatus(Status)
               };

        /// <summary>
        /// The refill point status of the given EVSE status.
        /// </summary>
        public static RefillPointStatusEnum ToRefillPointStatus(this POI.EVSEStatusType Status)

            => RefillPointStatusEnum.Parse(Status.ToString().ToLowerInvariant() switch {
                   "available"         => "available",
                   "waitingforplugin"  => "available",
                   "reserved"          => "reserved",
                   "charging"          => "charging",
                   "pluggedin"         => "occupied",
                   "occupied"          => "occupied",
                   "blocked"           => "blocked",
                   "offline"           => "unavailable",
                   "unavailable"       => "unavailable",
                   "inoperative"       => "inoperative",
                   "outofservice"      => "inoperative",
                   "doornotclosed"     => "inoperative",
                   "error"             => "faulted",
                   "faulted"           => "faulted",
                   "outoforder"        => "outOfOrder",
                   "indeployment"      => "planned",
                   "removed"           => "removed",
                   _                   => "unknown"
               });

        /// <summary>
        /// The EVSE status of the given refill point status.
        /// </summary>
        public static POI.EVSEStatusType ToPOI3Status(this RefillPointStatusEnum Status)

            => POI.EVSEStatusType.Parse(Status.ToString() switch {
                   "available"    => "available",
                   "blocked"      => "blocked",
                   "charging"     => "charging",
                   "faulted"      => "faulted",
                   "inoperative"  => "inoperative",
                   "occupied"     => "occupied",
                   "outOfOrder"   => "outOfOrder",
                   "outOfStock"   => "unavailable",
                   "planned"      => "inDeployment",
                   "removed"      => "removed",
                   "reserved"     => "reserved",
                   "unavailable"  => "unavailable",
                   _              => "unknown"
               });

        /// <summary>
        /// The operation status of the given admin status, if it tells one.
        /// </summary>
        public static OperationStatus? ToOperationStatus(String AdminStatus)

            => AdminStatus.ToLowerInvariant() switch {
                   "operational"   => OperationStatus.Parse("inOperation"),
                   "planned"       => OperationStatus.Parse("notInOperation"),
                   "indeployment"  => OperationStatus.Parse("notInOperation"),
                   "outofservice"  => OperationStatus.Parse("notInOperation"),
                   "blocked"       => OperationStatus.Parse("notInOperation"),
                   "deleted"       => OperationStatus.Parse("notInOperation"),
                   _               => null
               };

        #endregion


        #region ToTablePublication(RoamingNetwork, PublicationCreator, Language, TableId, TableVersion, Warnings, ...)

        /// <summary>
        /// A table publication of the charging pools of the given roaming network, one site each.
        /// </summary>
        /// <param name="RoamingNetwork">A roaming network.</param>
        /// <param name="PublicationCreator">The creator of the publication, e.g. the national access point identification of the CPO.</param>
        /// <param name="Language">The language of the publication.</param>
        /// <param name="TableId">The identification of the table.</param>
        /// <param name="TableVersion">The version of the table, e.g. the revision of the commit.</param>
        /// <param name="Warnings">Where to tell what could not be mapped.</param>
        /// <param name="OperatorIds">The operators to publish; all, unless given.</param>
        /// <param name="PublicationTime">The time of the publication; now, unless given.</param>
        /// <param name="StationIdBNetzA">Tells the station identification of the Bundesnetzagentur of a charging station.</param>
        public static EnergyInfrastructureTablePublication ToTablePublication(POI.RoamingNetwork                            RoamingNetwork,
                                                                              InternationalIdentifier                       PublicationCreator,
                                                                              Languages                                     Language,
                                                                              String                                        TableId,
                                                                              String                                        TableVersion,
                                                                              ICollection<String>                           Warnings,
                                                                              IEnumerable<POI.ChargingStationOperator_Id>?  OperatorIds       = null,
                                                                              DateTimeOffset?                               PublicationTime   = null,
                                                                              StationIdBNetzADelegate?                      StationIdBNetzA   = null)
        {

            var operatorIds  = OperatorIds?.ToHashSet();
            var sites        = new List<EnergyInfrastructureSite>();

            foreach (var chargingStationOperator in RoamingNetwork.ChargingStationOperators.Where(op => operatorIds is null || operatorIds.Contains(op.Id)))
            {
                foreach (var chargingPool in chargingStationOperator.ChargingPools)
                {

                    var site = chargingPool.ToDatexII(RoamingNetwork, Language, Warnings, StationIdBNetzA);

                    if (site is not null)
                        sites.Add(site);

                }
            }

            return new EnergyInfrastructureTablePublication(
                       PublicationTime ?? Timestamp.Now,
                       PublicationCreator,
                       Language,
                       new HeaderInformation(
                           InformationStatus.Parse("real"),
                           Confidentiality.  Parse("noRestriction"),
                           [ InformationDeliveryService.Parse("anyGeneralDeliveryService") ]
                       ),
                       [
                           new EnergyInfrastructureTable(
                               TableId,
                               TableVersion,
                               sites
                           )
                       ]
                   );

        }

        #endregion

        #region ToDatexII(ChargingPool, RoamingNetwork, Language, Warnings, StationIdBNetzA = null)

        /// <summary>
        /// The site of the given charging pool with its stations, or null when it has no geo location.
        /// </summary>
        /// <param name="ChargingPool">A charging pool.</param>
        /// <param name="RoamingNetwork">The roaming network of the charging pool: for its operator, brands, groups and tariffs.</param>
        /// <param name="Language">The language of texts POI3 has in no language.</param>
        /// <param name="Warnings">Where to tell what could not be mapped.</param>
        /// <param name="StationIdBNetzA">Tells the station identification of the Bundesnetzagentur of a charging station.</param>
        public static EnergyInfrastructureSite? ToDatexII(this POI.ChargingPool     ChargingPool,
                                                          POI.RoamingNetwork        RoamingNetwork,
                                                          Languages                 Language,
                                                          ICollection<String>       Warnings,
                                                          StationIdBNetzADelegate?  StationIdBNetzA   = null)
        {

            if (!RoamingNetwork.TryGetChargingStationOperatorById(ChargingPool.Id.OperatorId, out var chargingStationOperator))
            {
                Warnings.Add($"The operator of the charging pool '{ChargingPool.Id}' is unknown!");
                return null;
            }

            if (!ChargingPool.GeoLocation.HasValue)
            {
                Warnings.Add($"The charging pool '{ChargingPool.Id}' has no geo location, which a site needs to be found!");
                return null;
            }

            var stations   = ChargingPool.ChargingStations.
                                 Select(chargingStation => chargingStation.ToDatexII(ChargingPool, chargingStationOperator, RoamingNetwork, Language, Warnings, StationIdBNetzA)).
                                 ToArray();

            var brand      = ChargingPool.BrandIds.
                                 Select(brandId => RoamingNetwork.TryGetBrandById(brandId, out var found) ? found : null).
                                 FirstOrDefault(found => found is not null);

            var helpdesk   = ChargingPool.HotlinePhoneNumber ?? chargingStationOperator.HotlinePhoneNumber;

            if (ChargingPool.Accessibility.HasValue)
                Warnings.Add($"The accessibility '{ChargingPool.Accessibility}' of the charging pool '{ChargingPool.Id}' has no counterpart!");

            return new EnergyInfrastructureSite(

                       Id:                            ChargingPool.Id.ToString(),
                       Version:                       VersionOf(ChargingPool.LastChangeDate),
                       Name:                          ToMultilingualString(ChargingPool.Name),
                       LastUpdated:                   ChargingPool.LastChangeDate,
                       Description:                   ToMultilingualString(ChargingPool.Description),
                       AdditionalInformation:         ToMultilingualString(ChargingPool.ArrivalInstructions) is MultilingualString arrivalInstructions
                                                          ? [ arrivalInstructions ]
                                                          : null,
                       PhotoURLs:                     ChargingPool.Images.Select(image => image.URL),
                       OperatingHours:                ToDatexII(ChargingPool.OpeningTimes, ChargingPool.Id.ToString(), VersionOf(ChargingPool.LastChangeDate), ChargingPool.Created, Warnings),
                       LocationReference:             new PointLocation(
                                                          PointByCoordinates:     new PointByCoordinates(ToDatexII(ChargingPool.GeoLocation.Value)),
                                                          CoordinatesForDisplay:  ToDatexII(ChargingPool.GeoLocation.Value),
                                                          FacilityLocation:       new FacilityLocation(
                                                                                      TimeZone:  ToDatexII(ChargingPool.TimeZone ?? ChargingPool.Address?.TimeZone, ChargingPool.Id.ToString(), Warnings),
                                                                                      Address:   ChargingPool.Address?.ToDatexII()
                                                                                  )
                                                      ),
                       Operator:                      chargingStationOperator.ToDatexII(Language),
                       Helpdesk:                      helpdesk.HasValue
                                                          ? new OrganisationSpecification(
                                                                Id:                 $"{chargingStationOperator.Id}*HELPDESK",
                                                                Version:            VersionOf(chargingStationOperator.LastChangeDate),
                                                                Name:               new MultilingualString(new MultilingualStringValue(Language, "Hotline")),
                                                                OrganisationUnits:  [
                                                                                        new OrganisationUnit(
                                                                                            ContactInformation: [ new ContactInformation(TelephoneNumber: helpdesk.Value.ToString()) ]
                                                                                        )
                                                                                    ]
                                                            )
                                                          : null,
                       SupplementalFacilities:        ToDatexII(ChargingPool.Facilities, ChargingPool.Id.ToString(), VersionOf(ChargingPool.LastChangeDate), Warnings),
                       TypeOfSite:                    ChargingPool.ParkingType.HasValue
                                                          ? ToTypeOfSite(ChargingPool.ParkingType.Value)
                                                          : null,
                       Brand:                         brand is not null
                                                          ? ToMultilingualString(brand.Name)
                                                          : null,
                       ServiceTypes:                  [ new Service(ServiceType.Parse("unattended")) ],
                       EnergyInfrastructureStations:  stations

                   );

        }

        #endregion

        #region ToDatexII(ChargingStation, ChargingPool, Operator, RoamingNetwork, Language, Warnings, StationIdBNetzA = null)

        /// <summary>
        /// The station of the given charging station with its electric charging points.
        /// </summary>
        public static EnergyInfrastructureStation ToDatexII(this POI.ChargingStation          ChargingStation,
                                                            POI.ChargingPool                  ChargingPool,
                                                            POI.ChargingStationOperator       Operator,
                                                            POI.RoamingNetwork                RoamingNetwork,
                                                            Languages                         Language,
                                                            ICollection<String>               Warnings,
                                                            StationIdBNetzADelegate?          StationIdBNetzA   = null)
        {

            var chargingPoints     = ChargingStation.EVSEs.
                                         Select(evse => evse.ToDatexII(ChargingStation, ChargingPool, Operator, RoamingNetwork, Language, Warnings)).
                                         ToArray();

            // Its own, else the sum of its EVSEs.
            var totalMaximumPower  = ChargingStation.MaxPower ??
                                     Watt.FromW(ChargingStation.EVSEs.Sum(evse => evse.MaxPower?.Value ?? 0));

            var stationIdBNetzA    = StationIdBNetzA?.Invoke(ChargingStation);

            if (stationIdBNetzA is null)
                Warnings.Add($"The charging station '{ChargingStation.Id}' has no station identification of the Bundesnetzagentur!");

            var languages          = ChargingPool.LocationLanguages.Any()
                                         ? ChargingPool.LocationLanguages
                                         : [ Language ];

            return new EnergyInfrastructureStation(

                       Id:                                      ChargingStation.Id.ToString(),
                       Version:                                 VersionOf(ChargingStation.LastChangeDate),
                       StationIdBNetzA:                         stationIdBNetzA ?? "",
                       TotalMaximumPower:                       totalMaximumPower,
                       AuthenticationAndIdentificationMethods:  AuthenticationMethodsOf(ChargingPool),
                       NumberOfRefillPoints:                    (UInt16) chargingPoints.Length,
                       UserInterfaceLanguages:                  languages,
                       ServiceTypes:                            [ new Service(ServiceType.Parse("unattended")) ],
                       Name:                                    ToMultilingualString(ChargingStation.Name),
                       LastUpdated:                             ChargingStation.LastChangeDate,
                       Description:                             ToMultilingualString(ChargingStation.Description),
                       PhotoURLs:                               ChargingStation.Images.Select(image => image.URL),
                       LocationReference:                       ChargingStation.GeoLocation.HasValue
                                                                    ? new PointLocation(
                                                                          PointByCoordinates:     new PointByCoordinates(ToDatexII(ChargingStation.GeoLocation.Value)),
                                                                          CoordinatesForDisplay:  ToDatexII(ChargingStation.GeoLocation.Value)
                                                                      )
                                                                    : null,
                       RefillPoints:                            chargingPoints

                   );

        }

        #endregion

        #region ToDatexII(EVSE, ChargingStation, ChargingPool, Operator, RoamingNetwork, Language, Warnings)

        /// <summary>
        /// The electric charging point of the given EVSE, with its connectors, energy mix and tariffs.
        /// </summary>
        public static ElectricChargingPoint ToDatexII(this POI.EVSE                    EVSE,
                                                      POI.ChargingStation              ChargingStation,
                                                      POI.ChargingPool                 ChargingPool,
                                                      POI.ChargingStationOperator      Operator,
                                                      POI.RoamingNetwork               RoamingNetwork,
                                                      Languages                        Language,
                                                      ICollection<String>              Warnings)
        {

            var connectors      = EVSE.ChargingConnectors.
                                      Select(chargingConnector => chargingConnector.ToDatexII(EVSE, Warnings)).
                                      ToArray();

            var energyRates     = TariffIdsOf(EVSE, ChargingStation, ChargingPool, Operator).
                                      Select(tariffId => Operator.ChargingTariffs.FirstOrDefault(tariff => tariff.Id == tariffId)).
                                      Where (tariff   => tariff is not null).
                                      Select(tariff   => tariff!.ToDatexII(Language, Warnings)).
                                      Where (rate     => rate is not null).
                                      Cast<EnergyRate>().
                                      ToArray();

            var electricEnergy  = ChargingPool.EnergyMix is not null || energyRates.Length > 0
                                      ? new ElectricEnergy(
                                            EnergyProductName:          ChargingPool.EnergyMix is not null
                                                                            ? ToMultilingualString(ChargingPool.EnergyMix.ProductName)
                                                                            : null,
                                            IsGreenEnergy:              ChargingPool.EnergyMix is not null
                                                                            ? IsGreen(ChargingPool.EnergyMix)
                                                                            : null,
                                            ElectricEnergySourceRatios: ChargingPool.EnergyMix?.EnergySources.
                                                                            Select(source => ToDatexII(source)).
                                                                            Where (ratio  => ratio is not null).
                                                                            Cast<ElectricEnergySourceRatio>(),
                                            EnergyRates:                energyRates
                                        )
                                      : null;

            return new ElectricChargingPoint(

                       Id:                              EVSE.Id.ToString(),
                       Version:                         VersionOf(EVSE.LastChangeDate),
                       DeliveryUnit:                    DeliveryUnit.Parse("kWh"),
                       Name:                            ToMultilingualString(EVSE.Name),
                       LastUpdated:                     EVSE.LastChangeDate,
                       Description:                     ToMultilingualString(EVSE.Description),
                       PhotoURLs:                       EVSE.Images.Select(image => image.URL),
                       LocationReference:               EVSE.GeoLocation.HasValue
                                                            ? new PointLocation(
                                                                  PointByCoordinates:     new PointByCoordinates(ToDatexII(EVSE.GeoLocation.Value)),
                                                                  CoordinatesForDisplay:  ToDatexII(EVSE.GeoLocation.Value)
                                                              )
                                                            : null,
                       Reservation:                     ChargingStation.Features.Contains(POI.ChargingStationFeature.Reservable)
                                                            ? ReservationType.Parse("optional")
                                                            : null,
                       EVSEId:                          EVSE.Id.ToString(),
                       UsageType:                       [ ChargingPointUsage.Parse("electricVehicle") ],
                       VehicleToGridCommunicationType:  ChargingPool.AuthenticationModes.Any(mode => mode.Type is POI.AuthenticationMode.ISO15118_PLCType or POI.AuthenticationMode.ISO15118_AirType)
                                                            ? [ VehicleToGridCommunication.Parse("iso15118") ]
                                                            : null,
                       NumberOfConnectors:              (Byte) connectors.Length,
                       AvailableVoltage:                EVSE.MaxVoltage.HasValue ? [ EVSE.MaxVoltage.Value ] : null,
                       AvailableChargingPower:          EVSE.MaxPower.  HasValue ? [ EVSE.MaxPower.  Value ] : null,
                       Connector:                       connectors,
                       ElectricEnergy:                  electricEnergy is not null ? [ electricEnergy ] : null

                   );

        }

        #endregion

        #region ToDatexII(ChargingConnector, EVSE, Warnings)

        /// <summary>
        /// The connector of the given charging connector: its plug type, and the charging mode, maximum power, voltage
        /// and current of its EVSE.
        /// </summary>
        public static Connector ToDatexII(this POI.ChargingConnector  ChargingConnector,
                                          POI.EVSE                    EVSE,
                                          ICollection<String>         Warnings)
        {

            var connectorType = ToConnectorType(ChargingConnector.Type);

            if (!connectorType.HasValue)
                Warnings.Add($"The plug type '{ChargingConnector.Type}' of the EVSE '{EVSE.Id}' has no counterpart and is published as 'other'!");

            return new Connector(
                       ConnectorType:     connectorType ?? EnergyInfrastructure.ConnectorType.Parse("other"),
                       MaxPowerAtSocket:  EVSE.MaxPower ?? Watt.FromW(0),
                       OtherConnector:    connectorType.HasValue ? null : ChargingConnector.Type.ToString(),
                       ChargingMode:      ChargingModeOf(EVSE, ChargingConnector.Type),
                       Voltage:           EVSE.MaxVoltage,
                       MaximumCurrent:    EVSE.MaxCurrent
                   );

        }

        #endregion

        #region ToDatexII(ChargingTariff, Language, Warnings)

        /// <summary>
        /// The ad hoc energy rate of the given charging tariff, or null when none of its prices has a counterpart.
        /// A price per hour becomes a price per minute; time of day, weekdays and dates of an element become the
        /// overall period of its prices.
        /// </summary>
        public static EnergyRate? ToDatexII(this POI.ChargingTariff  ChargingTariff,
                                            Languages                Language,
                                            ICollection<String>      Warnings)
        {

            var prices = new List<EnergyPrice>();

            foreach (var element in ChargingTariff.Elements)
            {

                var overallPeriod = element.Restrictions is not null
                                        ? ToDatexII(element.Restrictions, ChargingTariff.Created, ChargingTariff.Id, Warnings)
                                        : null;

                foreach (var priceComponent in element.PriceComponents)
                {

                    (String Type, Decimal Price)? price = priceComponent.Type switch {
                        POI.ChargingDimensionTypes.ENERGY  => ("pricePerKWh",    priceComponent.Price),
                        POI.ChargingDimensionTypes.TIME    => ("pricePerMinute", priceComponent.Price / 60),
                        POI.ChargingDimensionTypes.FLAT    => ("basePrice",      priceComponent.Price),
                        _                                  => null
                    };

                    if (!price.HasValue)
                    {
                        Warnings.Add($"The price component '{priceComponent.Type}' of the charging tariff '{ChargingTariff.Id}' has no counterpart and is left out!");
                        continue;
                    }

                    if (priceComponent.StepSize.HasValue && priceComponent.StepSize.Value > 1)
                        Warnings.Add($"The step size of the price component '{priceComponent.Type}' of the charging tariff '{ChargingTariff.Id}' has no counterpart!");

                    prices.Add(new EnergyPrice(
                                   PriceType:      PriceType.Parse(price.Value.Type),
                                   Value:          new AmountOfMoney(price.Value.Price),
                                   TaxIncluded:    priceComponent.VAT.HasValue ? false : null,
                                   TaxRate:        priceComponent.VAT.HasValue ? Percentage.Parse(priceComponent.VAT.Value) : null,
                                   OverallPeriod:  overallPeriod
                               ));

                }

            }

            if (prices.Count == 0)
            {
                Warnings.Add($"The charging tariff '{ChargingTariff.Id}' has no price with a counterpart!");
                return null;
            }

            return new EnergyRate(
                       Id:                     ChargingTariff.Id.ToString(),
                       LastUpdated:            ChargingTariff.LastChangeDate,
                       RatePolicy:             RatePolicy.Parse("adHoc"),
                       ApplicableCurrency:     [ ChargingTariff.Currency ],
                       RateName:               ToMultilingualString(ChargingTariff.Name),
                       AdditionalInformation:  ToMultilingualString(ChargingTariff.Description),
                       EnergyPrices:           prices
                   );

        }

        #endregion


        #region ToStatusPublication(RoamingNetwork, PublicationCreator, Language, TableId, TableVersion, ...)

        /// <summary>
        /// A status publication of the EVSEs of the given roaming network, of the given ones only, if given.
        /// </summary>
        /// <param name="RoamingNetwork">A roaming network.</param>
        /// <param name="PublicationCreator">The creator of the publication.</param>
        /// <param name="Language">The language of the publication.</param>
        /// <param name="TableId">The identification of the table the status belongs to.</param>
        /// <param name="TableVersion">The version of the table the status belongs to.</param>
        /// <param name="OperatorIds">The operators to publish; all, unless given.</param>
        /// <param name="EVSEIds">The EVSEs to publish, e.g. those whose status changed; all, unless given.</param>
        /// <param name="PublicationTime">The time of the publication; now, unless given.</param>
        public static EnergyInfrastructureStatusPublication ToStatusPublication(POI.RoamingNetwork                            RoamingNetwork,
                                                                                InternationalIdentifier                       PublicationCreator,
                                                                                Languages                                     Language,
                                                                                String                                        TableId,
                                                                                String                                        TableVersion,
                                                                                IEnumerable<POI.ChargingStationOperator_Id>?  OperatorIds       = null,
                                                                                IEnumerable<POI.EVSE_Id>?                     EVSEIds           = null,
                                                                                DateTimeOffset?                               PublicationTime   = null)
        {

            var operatorIds  = OperatorIds?.ToHashSet();
            var evseIds      = EVSEIds?.    ToHashSet();
            var siteStatus   = new List<EnergyInfrastructureSiteStatus>();

            foreach (var chargingStationOperator in RoamingNetwork.ChargingStationOperators.Where(op => operatorIds is null || operatorIds.Contains(op.Id)))
            {
                foreach (var chargingPool in chargingStationOperator.ChargingPools)
                {

                    var status = chargingPool.ToSiteStatus(evseIds);

                    if (status is not null)
                        siteStatus.Add(status);

                }
            }

            return new EnergyInfrastructureStatusPublication(
                       PublicationTime ?? Timestamp.Now,
                       PublicationCreator,
                       Language,
                       [ new EnergyInfrastructureTableVersionedReference(TableId, TableVersion) ],
                       new HeaderInformation(
                           InformationStatus.Parse("real"),
                           Confidentiality.  Parse("noRestriction"),
                           [ InformationDeliveryService.Parse("anyGeneralDeliveryService") ]
                       ),
                       siteStatus
                   );

        }

        #endregion

        #region ToSiteStatus(ChargingPool, EVSEIds = null)

        /// <summary>
        /// The site status of the given charging pool: the status of its EVSEs, of the given ones only, if given;
        /// null when none of them is there.
        /// </summary>
        public static EnergyInfrastructureSiteStatus? ToSiteStatus(this POI.ChargingPool       ChargingPool,
                                                                   IReadOnlySet<POI.EVSE_Id>?  EVSEIds   = null)
        {

            var stationStatus = new List<EnergyInfrastructureStationStatus>();

            foreach (var chargingStation in ChargingPool.ChargingStations)
            {

                var pointStatus = ChargingStation_RefillPointStatus(chargingStation, EVSEIds);

                if (pointStatus.Length == 0)
                    continue;

                stationStatus.Add(new EnergyInfrastructureStationStatus(
                                      Reference:          new FacilityObjectVersionedReference(chargingStation.Id.ToString(), VersionOf(chargingStation.LastChangeDate)),
                                      LastUpdated:        pointStatus.Max(status => status.LastUpdated),
                                      IsAvailable:        EVSEIds is null
                                                              ? pointStatus.Any(status => status.Status.ToString() == "available")
                                                              : null,
                                      RefillPointStatus:  pointStatus
                                  ));

            }

            return stationStatus.Count > 0
                       ? new EnergyInfrastructureSiteStatus(
                             Reference:                          new FacilityObjectVersionedReference(ChargingPool.Id.ToString(), VersionOf(ChargingPool.LastChangeDate)),
                             LastUpdated:                        stationStatus.Max(status => status.LastUpdated),
                             OperationStatus:                    ToOperationStatus(ChargingPool.AdminStatus.Value.ToString()),
                             EnergyInfrastructureStationStatus:  stationStatus
                         )
                       : null;

        }

        private static ElectricChargingPointStatus[] ChargingStation_RefillPointStatus(POI.ChargingStation         ChargingStation,
                                                                                      IReadOnlySet<POI.EVSE_Id>?  EVSEIds)

            => ChargingStation.EVSEs.
                   Where (evse => EVSEIds is null || EVSEIds.Contains(evse.Id)).
                   Select(evse => new ElectricChargingPointStatus(
                                      Status:           ToRefillPointStatus(evse.Status.Value, evse.AdminStatus.Value),
                                      Reference:        new FacilityObjectVersionedReference(evse.Id.ToString(), VersionOf(evse.LastChangeDate)),
                                      LastUpdated:      new[] { evse.Status.Timestamp, evse.AdminStatus.Timestamp }.Max(),
                                      OperationStatus:  ToOperationStatus(evse.AdminStatus.Value.ToString())
                                  )).
                   ToArray();

        #endregion


        #region ToPOI3(Site, OperatorId, Existing, Language, Warnings)

        /// <summary>
        /// The charging pool of the given site, as JSON: the existing one, if any, with what DATEX II tells about it.
        /// What only POI3 knows is kept. Stations and charging points are matched by their identifications: new ones
        /// are added, ones gone are removed.
        /// </summary>
        /// <param name="Site">A site.</param>
        /// <param name="OperatorId">The operator of the site.</param>
        /// <param name="Existing">The JSON of the existing charging pool, if any.</param>
        /// <param name="Language">The language of texts DATEX II gives without one.</param>
        /// <param name="Warnings">Where to tell what could not be mapped.</param>
        public static JObject? ToPOI3(this EnergyInfrastructureSite      Site,
                                      POI.ChargingStationOperator_Id     OperatorId,
                                      JObject?                           Existing,
                                      Languages                          Language,
                                      ICollection<String>                Warnings)
        {

            var poolId = ToPOI3PoolId(Site.Id, OperatorId);

            if (!poolId.HasValue)
            {
                Warnings.Add($"The site identification '{Site.Id}' is no charging pool identification!");
                return null;
            }

            #region The charging pool as DATEX II tells it

            var location         = Site.LocationReference as ALocation;
            var coordinates      = location?.CoordinatesForDisplay ??
                                   (Site.LocationReference as PointLocation)?.PointByCoordinates?.PointCoordinates;
            var address          = location?.FacilityLocation?.Address;

            var authentication   = Site.EnergyInfrastructureStations.
                                       SelectMany(station => station.AuthenticationAndIdentificationMethods).
                                       Distinct().
                                       ToArray();

            var mapped           = new POI.ChargingPool(
                                       Id:                   poolId.Value,
                                       Name:                 ToI18NString(Site.Name, Language),
                                       Description:          ToI18NString(Site.Description, Language),
                                       Address:              address?.ToPOI3(Language),
                                       GeoLocation:          coordinates is not null
                                                                 ? GeoCoordinate.Create(Latitude.Parse(coordinates.Latitude), Longitude.Parse(coordinates.Longitude))
                                                                 : null,
                                       ParkingType:          Site.TypeOfSite.HasValue
                                                                 ? ToParkingType(Site.TypeOfSite.Value)
                                                                 : null,
                                       HotlinePhoneNumber:   HotlineOf(Site.Helpdesk),
                                       OpeningTimes:         ToPOI3(Site.OperatingHours, Warnings),
                                       EnergyMix:            Site.EnergyInfrastructureStations.
                                                                 SelectMany(station => station.RefillPoints).
                                                                 OfType<ElectricChargingPoint>().
                                                                 SelectMany(point => point.ElectricEnergy).
                                                                 Select(energy => energy.ToPOI3(Language)).
                                                                 FirstOrDefault(mix => mix is not null),
                                       Images:               Site.PhotoURLs.Select(ToPOI3Image),
                                       Facilities:           Site.SupplementalFacilities.
                                                                 Select(ToPOI3Facility).
                                                                 Where (facility => facility.HasValue).
                                                                 Select(facility => facility!.Value).
                                                                 Distinct(),
                                       PaymentOptions:       PaymentOptionsOf(authentication),
                                       AuthenticationModes:  AuthenticationModesOf(authentication),
                                       LocationLanguages:    Site.EnergyInfrastructureStations.
                                                                 SelectMany(station => station.UserInterfaceLanguages).
                                                                 Distinct(),
                                       ArrivalInstructions:  Site.AdditionalInformation.Any()
                                                                 ? ToI18NString(Site.AdditionalInformation.First(), Language)
                                                                 : null,
                                       LastChange:           Site.LastUpdated
                                   ).ToJSON();

            var mappedStations   = new List<JObject>();

            foreach (var station in Site.EnergyInfrastructureStations)
            {
                var chargingStation = station.ToPOI3(OperatorId, Language, Warnings);
                if (chargingStation is not null)
                    mappedStations.Add(chargingStation.ToJSON());
            }

            #endregion

            var desired   = Existing is not null
                                ? (JObject) Existing.DeepClone()
                                : new JObject(new JProperty("@id", poolId.Value.ToString()));

            Overlay(desired, mapped, PoolProperties);

            #region The charging stations: matched by their identification

            var stations  = ArrayOf(desired, "chargingStations");
            var wanted    = mappedStations.ToDictionary(station => station["@id"]?.Value<String>() ?? "");

            foreach (var station in stations.OfType<JObject>().ToArray())
            {

                var stationId = station["@id"]?.Value<String>() ?? "";

                if (!wanted.TryGetValue(stationId, out var mappedStation))
                {
                    // Gone at DATEX II.
                    station.Remove();
                    continue;
                }

                Overlay(station, mappedStation, StationProperties);

                #region The EVSEs of the charging station: matched by their identification

                var evses        = ArrayOf(station, "EVSEs");
                var wantedEVSEs  = (mappedStation["EVSEs"] as JArray ?? []).OfType<JObject>().ToDictionary(evse => evse["@id"]?.Value<String>() ?? "");

                foreach (var evse in evses.OfType<JObject>().ToArray())
                {

                    var evseId = evse["@id"]?.Value<String>() ?? "";

                    if (wantedEVSEs.Remove(evseId, out var mappedEVSE))
                        Overlay(evse, mappedEVSE, EVSEProperties);

                    // Gone at DATEX II.
                    else
                        evse.Remove();

                }

                foreach (var newEVSE in wantedEVSEs.Values)
                    evses.Add(newEVSE);

                #endregion

                wanted.Remove(stationId);

            }

            foreach (var newStation in wanted.Values)
                stations.Add(newStation);

            #endregion

            return desired;

        }

        #endregion

        #region ToPOI3(Station, OperatorId, Language, Warnings)

        /// <summary>
        /// The charging station of the given station, with its EVSEs.
        /// </summary>
        public static POI.ChargingStation? ToPOI3(this EnergyInfrastructureStation  Station,
                                                  POI.ChargingStationOperator_Id    OperatorId,
                                                  Languages                         Language,
                                                  ICollection<String>               Warnings)
        {

            var stationId = ToPOI3StationId(Station.Id, OperatorId);

            if (!stationId.HasValue)
            {
                Warnings.Add($"The station identification '{Station.Id}' is no charging station identification!");
                return null;
            }

            var evses = new List<POI.EVSE>();

            foreach (var refillPoint in Station.RefillPoints)
            {

                if (refillPoint is not ElectricChargingPoint chargingPoint)
                {
                    Warnings.Add($"The refill point '{refillPoint.Id}' of the station '{Station.Id}' is no electric charging point!");
                    continue;
                }

                var evse = chargingPoint.ToPOI3(OperatorId, Language, Warnings);

                if (evse is not null)
                    evses.Add(evse);

            }

            var coordinates = (Station.LocationReference as ALocation)?.CoordinatesForDisplay;

            return new POI.ChargingStation(
                       Id:           stationId.Value,
                       Name:         ToI18NString(Station.Name,        Language),
                       Description:  ToI18NString(Station.Description, Language),
                       GeoLocation:  coordinates is not null
                                         ? GeoCoordinate.Create(Latitude.Parse(coordinates.Latitude), Longitude.Parse(coordinates.Longitude))
                                         : null,
                       EVSEs:        evses,
                       Images:       Station.PhotoURLs.Select(ToPOI3Image),
                       Features:     Station.RefillPoints.Any(point => point.Reservation.HasValue &&
                                                                      point.Reservation.Value.ToString() is "optional" or "mandatory" or "partly")
                                         ? [ POI.ChargingStationFeature.Reservable ]
                                         : null,
                       // Only a total maximum power of its own: the sum of the charging points is what POI3 tells anyway.
                       MaxPower:     Station.TotalMaximumPower.Value > 0 &&
                                     Station.TotalMaximumPower.Value != evses.Sum(evse => evse.MaxPower?.Value ?? 0)
                                         ? Station.TotalMaximumPower
                                         : null,
                       LastChange:   Station.LastUpdated
                   );

        }

        #endregion

        #region ToPOI3(ChargingPoint, OperatorId, Language, Warnings)

        /// <summary>
        /// The EVSE of the given electric charging point: its maximum voltage and power from its available ones or its
        /// connectors, its current types and charging modes from the charging modes of its connectors, and its charging
        /// tariffs from its energy rates.
        /// </summary>
        public static POI.EVSE? ToPOI3(this ElectricChargingPoint      ChargingPoint,
                                       POI.ChargingStationOperator_Id  OperatorId,
                                       Languages                       Language,
                                       ICollection<String>             Warnings)
        {

            var evseId = ToPOI3EVSEId(ChargingPoint, OperatorId);

            if (!evseId.HasValue)
            {
                Warnings.Add($"The charging point '{ChargingPoint.Id}' has no EVSE identification of the operator '{OperatorId}'!");
                return null;
            }

            var connectors     = new List<POI.ChargingConnector>();
            var currentTypes   = POI.CurrentTypes.Unspecified;
            var chargingModes  = POI.ChargingModes.Unspecified;
            var number         = 1;

            foreach (var connector in ChargingPoint.Connector)
            {

                var type = ToPOI3(connector.ConnectorType);

                if (!type.HasValue)
                    Warnings.Add($"The connector type '{connector.ConnectorType}' of the charging point '{ChargingPoint.Id}' has no counterpart and is left out!");

                else
                    connectors.Add(new POI.ChargingConnector(POI.ChargingConnector_Id.Parse((number++).ToString()), type.Value));

                (currentTypes, chargingModes) = connector.ChargingMode?.ToString() switch {
                    "mode1AC1p"  => (currentTypes | POI.CurrentTypes.AC_OnePhase,    chargingModes | POI.ChargingModes.Mode_1),
                    "mode1AC3p"  => (currentTypes | POI.CurrentTypes.AC_ThreePhases, chargingModes | POI.ChargingModes.Mode_1),
                    "mode2AC1p"  => (currentTypes | POI.CurrentTypes.AC_OnePhase,    chargingModes | POI.ChargingModes.Mode_2),
                    "mode2AC3p"  => (currentTypes | POI.CurrentTypes.AC_ThreePhases, chargingModes | POI.ChargingModes.Mode_2),
                    "mode3AC3p"  => (currentTypes | POI.CurrentTypes.AC_ThreePhases, chargingModes | POI.ChargingModes.Mode_3),
                    "mode4DC"    => (currentTypes | POI.CurrentTypes.DC,             chargingModes | POI.ChargingModes.Mode_4),
                    "ccs"        => (currentTypes | POI.CurrentTypes.DC,             chargingModes | POI.ChargingModes.Mode_4),
                    _            => (currentTypes, chargingModes)
                };

            }

            var tariffIds = ChargingPoint.ElectricEnergy.
                                SelectMany(energy => energy.EnergyRates.Select(rate => rate.Id).Concat(energy.EnergyRateByReference.Select(reference => reference.Id))).
                                Select    (rateId => ToPOI3TariffId(rateId, OperatorId)).
                                Where     (tariffId => tariffId.HasValue).
                                Select    (tariffId => tariffId!.Value).
                                Distinct();

            var coordinates = (ChargingPoint.LocationReference as ALocation)?.CoordinatesForDisplay;

            return new POI.EVSE(
                       Id:                  evseId.Value,
                       Name:                ToI18NString(ChargingPoint.Name,        Language),
                       Description:         ToI18NString(ChargingPoint.Description, Language),
                       CurrentTypes:        currentTypes  != POI.CurrentTypes. Unspecified ? currentTypes  : null,
                       ChargingModes:       chargingModes != POI.ChargingModes.Unspecified ? chargingModes : null,
                       MaxVoltage:          ChargingPoint.AvailableVoltage.Any()
                                                ? ChargingPoint.AvailableVoltage.Max()
                                                : ChargingPoint.Connector.Select(connector => connector.Voltage).Where(voltage => voltage.HasValue).Max(),
                       MaxCurrent:          ChargingPoint.Connector.Select(connector => connector.MaximumCurrent).Where(current => current.HasValue).Max(),
                       MaxPower:            ChargingPoint.AvailableChargingPower.Any()
                                                ? ChargingPoint.AvailableChargingPower.Max()
                                                : ChargingPoint.Connector.Any()
                                                      ? ChargingPoint.Connector.Max(connector => connector.MaxPowerAtSocket)
                                                      : null,
                       ChargingConnectors:  connectors,
                       ChargingTariffIds:   tariffIds,
                       Images:              ChargingPoint.PhotoURLs.Select(ToPOI3Image),
                       GeoLocation:         coordinates is not null
                                                ? GeoCoordinate.Create(Latitude.Parse(coordinates.Latitude), Longitude.Parse(coordinates.Longitude))
                                                : null,
                       LastChange:          ChargingPoint.LastUpdated
                   );

        }

        #endregion

        #region ToPOI3(EnergyRate, OperatorId, Language, Warnings)

        /// <summary>
        /// The charging tariff of the given energy rate: a price per minute becomes a price per hour. Its prices with
        /// the same overall period make one element, whose restrictions are the dates, times of day and weekdays of it.
        /// </summary>
        public static POI.ChargingTariff? ToPOI3(this EnergyRate                 EnergyRate,
                                                 POI.ChargingStationOperator_Id  OperatorId,
                                                 Languages                       Language,
                                                 ICollection<String>             Warnings)
        {

            var tariffId = ToPOI3TariffId(EnergyRate.Id, OperatorId);

            if (!tariffId.HasValue)
            {
                Warnings.Add($"The energy rate identification '{EnergyRate.Id}' is no charging tariff identification!");
                return null;
            }

            if (EnergyRate.RatePolicy.ToString() != "adHoc")
                Warnings.Add($"The energy rate '{EnergyRate.Id}' is a '{EnergyRate.RatePolicy}' rate; POI3 charging tariffs do not tell!");

            var elements = new List<POI.ChargingTariffElement>();

            foreach (var group in GroupByPeriod(EnergyRate.EnergyPrices))
            {

                var components = new List<POI.ChargingPriceComponent>();

                foreach (var price in group)
                {

                    var component = price.PriceType.ToString() switch {
                        "pricePerKWh"     => new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.ENERGY, price.Value.Value,      VATOf(price), 1),
                        "pricePerMinute"  => new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.TIME,   price.Value.Value * 60, VATOf(price), 1),
                        "basePrice"       => new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.FLAT,   price.Value.Value,      VATOf(price)),
                        "flatRate"        => new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.FLAT,   price.Value.Value,      VATOf(price)),
                        "free"            => new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.FLAT,   0),
                        _                 => null
                    };

                    if (component is null)
                        Warnings.Add($"The price type '{price.PriceType}' of the energy rate '{EnergyRate.Id}' has no counterpart and is left out!");

                    else
                        components.Add(component);

                }

                if (components.Count > 0)
                    elements.Add(new POI.ChargingTariffElement(components, ToPOI3(group.First().OverallPeriod, Warnings)));

            }

            if (elements.Count == 0)
            {
                Warnings.Add($"The energy rate '{EnergyRate.Id}' has no price with a counterpart!");
                return null;
            }

            var currency = EnergyRate.ApplicableCurrency.First();

            if (EnergyRate.ApplicableCurrency.Count() > 1)
                Warnings.Add($"The energy rate '{EnergyRate.Id}' has more than one currency; only '{currency}' is taken!");

            return new POI.ChargingTariff(
                       Id:          tariffId.Value,
                       Currency:    currency,
                       Elements:    elements,
                       Name:        ToI18NString(EnergyRate.RateName,              Language),
                       Description: ToI18NString(EnergyRate.AdditionalInformation, Language),
                       LastChange:  EnergyRate.LastUpdated
                   );

        }

        #endregion

        #region RatesOf(Site)

        /// <summary>
        /// The energy rates of the given site, each once.
        /// </summary>
        public static IEnumerable<EnergyRate> RatesOf(EnergyInfrastructureSite Site)

            => Site.EnergyInfrastructureStations.
                    SelectMany(station => station.ElectricEnergy.
                                              Concat(station.RefillPoints.OfType<ElectricChargingPoint>().SelectMany(point => point.ElectricEnergy))).
                    SelectMany(energy  => energy.EnergyRates).
                    GroupBy   (rate    => rate.Id).
                    Select    (rates   => rates.First());

        #endregion


        #region (internal) PoolProperties / StationProperties / EVSEProperties

        /// <summary>
        /// The properties of a charging pool DATEX II tells: imported onto an existing charging pool, the others are kept.
        /// </summary>
        internal static readonly String[] PoolProperties = [
            "name", "description", "address", "geoLocation", "parkingType", "hotline", "openingTimes", "energyMix", "images",
            "facilities", "paymentOptions", "authenticationModes", "locationLanguages", "arrivalInstructions"
        ];

        /// <summary>
        /// The properties of a charging station DATEX II tells: imported onto an existing charging station, the others are kept.
        /// </summary>
        internal static readonly String[] StationProperties = [
            "name", "description", "geoLocation", "images", "features", "maxPower"
        ];

        /// <summary>
        /// The properties of an EVSE DATEX II tells: imported onto an existing EVSE, the others are kept.
        /// </summary>
        internal static readonly String[] EVSEProperties = [
            "name", "description", "currentTypes", "chargingModes", "maxVoltage", "maxCurrent", "maxPower", "chargingConnectors",
            "chargingTariffIds", "images", "geoLocation"
        ];

        /// <summary>
        /// The array of the given property, made when there is none. Not one assigned again: a token with a parent
        /// is copied when it is assigned, and changes to it would go nowhere.
        /// </summary>
        internal static JArray ArrayOf(JObject  Parent,
                                       String   Property)
        {

            if (Parent[Property] is JArray array)
                return array;

            array = [];
            Parent[Property] = array;
            return array;

        }

        /// <summary>
        /// Set the given properties of the target to those of the source; one the source does not have is removed.
        /// </summary>
        internal static void Overlay(JObject              Target,
                                     JObject              Source,
                                     IEnumerable<String>  Properties)
        {
            foreach (var property in Properties)
            {
                if (Source[property] is JToken value)
                    Target[property] = value.DeepClone();
                else
                    Target.Remove(property);
            }
        }

        #endregion

        #region (private) Texts, coordinates, addresses, images

        private static MultilingualString? ToMultilingualString(IEnumerable<I18NPair>? Text)
        {

            var values = Text?.Where (pair => pair.Text.IsNotNullOrEmpty()).
                               Select(pair => new MultilingualStringValue(pair.Language, pair.Text)).
                               ToArray() ?? [];

            return values.Length > 0
                       ? new MultilingualString(values)
                       : null;

        }

        private static I18NString? ToI18NString(MultilingualString?  Text,
                                                Languages            Language)
        {

            if (Text is null)
                return null;

            var text = I18NString.Empty;

            foreach (var value in Text.Values.Where(value => value.Text.IsNotNullOrEmpty()))
                text.Set(value.Language == Languages.unknown ? Language : value.Language, value.Text);

            return text.IsNullOrEmpty() ? null : text;

        }

        private static PointCoordinates ToDatexII(GeoCoordinate GeoCoordinate)

            => new (GeoCoordinate.Latitude. Value,
                    GeoCoordinate.Longitude.Value);

        private static DXAddress ToDatexII(this Address Address)
        {

            var lines = new List<AddressLine>();

            if (Address.Street.IsNotNullOrEmpty())
                lines.Add(new AddressLine(0, new MultilingualString(new MultilingualStringValue(Languages.unknown, Address.Street)),       AddressLineType.Parse("street")));

            if (Address.HouseNumber.IsNotNullOrEmpty())
                lines.Add(new AddressLine(1, new MultilingualString(new MultilingualStringValue(Languages.unknown, Address.HouseNumber!)), AddressLineType.Parse("houseNumber")));

            if (Address.Region.IsNotNullOrEmpty())
                lines.Add(new AddressLine(2, new MultilingualString(new MultilingualStringValue(Languages.unknown, Address.Region!)),      AddressLineType.Parse("region")));

            return new DXAddress(
                       Postcode:      Address.PostalCode.IsNotNullOrEmpty() ? Address.PostalCode : null,
                       City:          ToMultilingualString(Address.City),
                       CountryCode:   Address.Country,
                       AddressLines:  lines
                   );

        }

        private static Address? ToPOI3(this DXAddress  Address,
                                       Languages       Language)
        {

            String? LineOf(String Type)
                => Address.AddressLines.
                       Where  (line => line.Type.ToString() == Type).
                       OrderBy(line => line.Order).
                       Select (line => line.Text.Values.FirstOrDefault()?.Text).
                       FirstOrDefault();

            if (Address.CountryCode is null)
                return null;

            return new Address(
                       Street:       LineOf("street") ?? "",
                       PostalCode:   Address.Postcode ?? "",
                       City:         ToI18NString(Address.City, Language) ?? I18NString.Empty,
                       Country:      Address.CountryCode,
                       HouseNumber:  LineOf("houseNumber"),
                       Region:       LineOf("region")
                   );

        }

        private static DXTimeZone? ToDatexII(Time_Zone?           TimeZone,
                                             String               Entity,
                                             ICollection<String>  Warnings)
        {

            if (!TimeZone.HasValue)
                return null;

            try
            {
                // DATEX II knows only an offset to UTC: the standard one of the time zone, without daylight saving time.
                return DXTimeZone.FromOffset(TimeZoneInfo.FindSystemTimeZoneById(TimeZone.Value.ToString()).BaseUtcOffset);
            }
            catch (Exception)
            {
                Warnings.Add($"The time zone '{TimeZone}' of '{Entity}' is unknown here and left out!");
                return null;
            }

        }

        private static POI.Image ToPOI3Image(URL URL)

            => new (URL,
                    URL.ToString()[(URL.ToString().LastIndexOf('.') + 1)..].ToLowerInvariant() switch {
                        "png"   => HTTPContentType.Image.PNG,
                        "gif"   => HTTPContentType.Image.GIF,
                        "svg"   => HTTPContentType.Image.SVG,
                        "webp"  => HTTPContentType.Image.WEBP,
                        _       => HTTPContentType.Image.JPEG
                    },
                    POI.ImageCategory.LOCATION);

        private static PhoneNumber? HotlineOf(AOrganisation? Helpdesk)

            => Helpdesk is OrganisationSpecification organisation
                   ? organisation.OrganisationUnits.
                         SelectMany(unit => unit.ContactInformation).
                         Select    (contact => contact.TelephoneNumber).
                         Where     (number  => number is not null).
                         Select    (number  => PhoneNumber.TryParse(number!)).
                         FirstOrDefault(number => number.HasValue)
                   : null;

        private static OrganisationSpecification ToDatexII(this POI.ChargingStationOperator  Operator,
                                                           Languages                         Language)

            => new (
                   Id:                        Operator.Id.ToString(),
                   Version:                   VersionOf(Operator.LastChangeDate),
                   Name:                      ToMultilingualString(Operator.Name) ??
                                              new MultilingualString(new MultilingualStringValue(Language, Operator.Id.ToString())),
                   OperatorId:                Operator.Id.ToString(),
                   LastUpdated:               Operator.LastChangeDate,
                   Description:               ToMultilingualString(Operator.Description),
                   LinkToGeneralInformation:  Operator.Homepage,
                   LinkToLogo:                Operator.Logo,
                   OrganisationUnits:         Operator.EMailAddress.HasValue || Operator.Telephone.HasValue
                                                  ? [
                                                        new OrganisationUnit(
                                                            ContactInformation: [ new ContactInformation(TelephoneNumber: Operator.Telephone?.ToString(),
                                                                                                         EMail:           Operator.EMailAddress?.ToString()) ]
                                                        )
                                                    ]
                                                  : null
               );

        #endregion

        #region (private) Opening times

        /// <summary>
        /// Open around the clock, or the regular openings as periods: one for each time of day, on the weekdays it is
        /// open then. The overall period starts with the creation of the charging pool.
        /// </summary>
        private static AOperatingHours? ToDatexII(ImmutableOpeningTimes?  OpeningTimes,
                                                  String                  Id,
                                                  String                  Version,
                                                  DateTimeOffset          Created,
                                                  ICollection<String>     Warnings)
        {

            if (OpeningTimes is null)
                return null;

            if (OpeningTimes.IsOpen24Hours)
                return new OpenAllHours();

            if (OpeningTimes.ExceptionalOpenings.Length > 0)
                Warnings.Add($"The exceptional openings of '{Id}' are left out!");

            var periods = OpeningTimes.RegularOpenings.
                              SelectMany(day => day.Value).
                              GroupBy   (hours => (hours.PeriodBegin, hours.PeriodEnd)).
                              Select    (group => new Period(
                                                      RecurringTimePeriodOfDay:     [ new TimePeriodOfDay(Time.FromHourMinSec(group.Key.PeriodBegin.Hour, group.Key.PeriodBegin.Minute, 0)!.Value,
                                                                                                          Time.FromHourMinSec(group.Key.PeriodEnd.  Hour, group.Key.PeriodEnd.  Minute, 0)!.Value) ],
                                                      RecurringDayWeekMonthPeriod:  [ new DayWeekMonth(group.Select(hours => ToDay(hours.DayOfWeek)).Distinct()) ]
                                                  )).
                              ToArray();

            return periods.Length > 0
                       ? new OperatingHoursSpecification(
                             Id:             $"{Id}*OPENINGTIMES",
                             Version:        Version,
                             OverallPeriod:  new OverallPeriod(Created, ValidPeriod: periods)
                         )
                       : null;

        }

        private static ImmutableOpeningTimes? ToPOI3(AOperatingHours?     OperatingHours,
                                                     ICollection<String>  Warnings)
        {

            switch (OperatingHours)
            {

                case OpenAllHours:
                    return new ImmutableOpeningTimes(OpeningTimes.Open24Hours);

                case OperatingHoursSpecification specification:

                    var openingTimes = new OpeningTimes();

                    foreach (var period in specification.OverallPeriod.ValidPeriod)
                    {

                        var days = period.RecurringDayWeekMonthPeriod.SelectMany(dayWeekMonth => dayWeekMonth.ApplicableDays).ToArray();

                        if (days.Length == 0)
                            days = [.. Enum.GetValues<DayOfWeek>().Select(ToDay)];

                        foreach (var timeOfDay in period.RecurringTimePeriodOfDay)
                            foreach (var day in days)
                                openingTimes.AddRegularOpening(ToDayOfWeek(day),
                                                               new HourMin((Byte) timeOfDay.StartTimeOfPeriod.Hour, timeOfDay.StartTimeOfPeriod.Minute),
                                                               new HourMin((Byte) timeOfDay.EndTimeOfPeriod.  Hour, timeOfDay.EndTimeOfPeriod.  Minute));

                    }

                    if (specification.OverallPeriod.ExceptionPeriod.Any())
                        Warnings.Add($"The exception periods of the operating hours '{specification.Id}' are left out!");

                    return new ImmutableOpeningTimes(openingTimes);

                case null:
                    return null;

                default:
                    Warnings.Add($"Operating hours of the type '{OperatingHours.GetType().Name}' have no counterpart and are left out!");
                    return null;

            }

        }

        private static Day ToDay(DayOfWeek DayOfWeek)

            => Day.Parse(DayOfWeek.ToString().ToLowerInvariant());

        private static DayOfWeek ToDayOfWeek(Day Day)

            => Enum.Parse<DayOfWeek>(Day.ToString(), ignoreCase: true);

        #endregion

        #region (private) Tariffs

        /// <summary>
        /// The tariffs of an EVSE: its own, and those of the EVSE, charging station and charging pool groups it belongs to.
        /// </summary>
        private static IEnumerable<POI.ChargingTariff_Id> TariffIdsOf(POI.EVSE                     EVSE,
                                                                      POI.ChargingStation          ChargingStation,
                                                                      POI.ChargingPool             ChargingPool,
                                                                      POI.ChargingStationOperator  Operator)
        {

            var tariffIds = new List<POI.ChargingTariff_Id>(EVSE.ChargingTariffIds);

            tariffIds.AddRange(Operator.EVSEGroups.           Where(group => group.ChargingTariffId.HasValue && group.EVSEIds.           Contains(EVSE.Id)).           Select(group => group.ChargingTariffId!.Value));
            tariffIds.AddRange(Operator.ChargingStationGroups.Where(group => group.ChargingTariffId.HasValue && group.ChargingStationIds.Contains(ChargingStation.Id)).Select(group => group.ChargingTariffId!.Value));
            tariffIds.AddRange(Operator.ChargingPoolGroups.   Where(group => group.ChargingTariffId.HasValue && group.ChargingPoolIds.   Contains(ChargingPool.Id)).   Select(group => group.ChargingTariffId!.Value));

            return tariffIds.Distinct();

        }

        /// <summary>
        /// The dates, times of day and weekdays of the given restrictions as an overall period: from the start date, else
        /// from the creation of the tariff. Energy, power, current and duration restrictions have no counterpart.
        /// </summary>
        private static OverallPeriod? ToDatexII(POI.ChargingTariffRestrictions  Restrictions,
                                                DateTimeOffset                  Created,
                                                POI.ChargingTariff_Id           TariffId,
                                                ICollection<String>             Warnings)
        {

            if (Restrictions.MinEnergy.  HasValue || Restrictions.MaxEnergy.  HasValue ||
                Restrictions.MinPower.   HasValue || Restrictions.MaxPower.   HasValue ||
                Restrictions.MinCurrent. HasValue || Restrictions.MaxCurrent. HasValue ||
                Restrictions.MinDuration.HasValue || Restrictions.MaxDuration.HasValue)
            {
                Warnings.Add($"The energy, power, current and duration restrictions of the charging tariff '{TariffId}' have no counterpart and are left out!");
            }

            var timeOfDay  = Restrictions.StartTime.HasValue || Restrictions.EndTime.HasValue
                                 ? new TimePeriodOfDay(Time.FromHourMinSec((Int16) (Restrictions.StartTime?.Hour ?? 0),  (Byte) (Restrictions.StartTime?.Minute ?? 0),  0)!.Value,
                                                       Time.FromHourMinSec((Int16) (Restrictions.EndTime?.  Hour ?? 23), (Byte) (Restrictions.EndTime?.  Minute ?? 59), Restrictions.EndTime.HasValue ? (Byte) 0 : (Byte) 59)!.Value)
                                 : null;

            var days       = Restrictions.DaysOfWeek.Any()
                                 ? new DayWeekMonth(Restrictions.DaysOfWeek.Select(ToDay))
                                 : null;

            if (!Restrictions.StartDate.HasValue && !Restrictions.EndDate.HasValue && timeOfDay is null && days is null)
                return null;

            return new OverallPeriod(
                       OverallStartTime:  Restrictions.StartDate.HasValue
                                              ? new DateTimeOffset(Restrictions.StartDate.Value.ToDateTime(TimeOnly.MinValue), TimeSpan.Zero)
                                              : Created,
                       OverallEndTime:    Restrictions.EndDate.HasValue
                                              ? new DateTimeOffset(Restrictions.EndDate.Value.ToDateTime(TimeOnly.MinValue), TimeSpan.Zero)
                                              : null,
                       ValidPeriod:       timeOfDay is not null || days is not null
                                              ? [
                                                    new Period(
                                                        RecurringTimePeriodOfDay:     timeOfDay is not null ? [ timeOfDay ] : null,
                                                        RecurringDayWeekMonthPeriod:  days      is not null ? [ days ]      : null
                                                    )
                                                ]
                                              : null
                   );

        }

        /// <summary>
        /// The restrictions of the given overall period of a price: its start as the start date, its end as the end
        /// date, and the times of day and weekdays of its first valid period.
        /// </summary>
        private static POI.ChargingTariffRestrictions? ToPOI3(OverallPeriod?       OverallPeriod,
                                                              ICollection<String>  Warnings)
        {

            if (OverallPeriod is null)
                return null;

            var period     = OverallPeriod.ValidPeriod.FirstOrDefault();
            var timeOfDay  = period?.RecurringTimePeriodOfDay.FirstOrDefault();

            if (OverallPeriod.ValidPeriod.Count() > 1 || OverallPeriod.ExceptionPeriod.Any() || period?.RecurringTimePeriodOfDay.Count() > 1)
                Warnings.Add("Only the first valid period and time of day of a price are taken!");

            return new POI.ChargingTariffRestrictions(
                       StartTime:   timeOfDay is not null ? new TimeOnly(timeOfDay.StartTimeOfPeriod.Hour, timeOfDay.StartTimeOfPeriod.Minute) : null,
                       EndTime:     timeOfDay is not null ? new TimeOnly(timeOfDay.EndTimeOfPeriod.  Hour, timeOfDay.EndTimeOfPeriod.  Minute) : null,
                       StartDate:   DateOnly.FromDateTime(OverallPeriod.OverallStartTime.UtcDateTime),
                       EndDate:     OverallPeriod.OverallEndTime.HasValue
                                        ? DateOnly.FromDateTime(OverallPeriod.OverallEndTime.Value.UtcDateTime)
                                        : null,
                       DaysOfWeek:  period?.RecurringDayWeekMonthPeriod.SelectMany(dayWeekMonth => dayWeekMonth.ApplicableDays).Select(ToDayOfWeek)
                   );

        }

        /// <summary>
        /// The prices with the same overall period, in their order.
        /// </summary>
        private static IEnumerable<IReadOnlyList<EnergyPrice>> GroupByPeriod(IEnumerable<EnergyPrice> Prices)
        {

            var groups = new List<List<EnergyPrice>>();

            foreach (var price in Prices)
            {

                var group = groups.FirstOrDefault(group => SamePeriod(group[0].OverallPeriod, price.OverallPeriod));

                if (group is null)
                    groups.Add([ price ]);
                else
                    group.Add(price);

            }

            return groups;

        }

        private static Boolean SamePeriod(OverallPeriod? Period1, OverallPeriod? Period2)

            => Period1 is null || Period2 is null
                   ? Period1 is null && Period2 is null
                   : XNode.DeepEquals(Period1.ToXML(), Period2.ToXML());

        private static Decimal? VATOf(EnergyPrice Price)

            => Price.TaxRate?.Value;

        #endregion

        #region (private) Energy mix

        private static Boolean IsGreen(POI.EnergyMix EnergyMix)

            => EnergyMix.EnergySources.Any() &&
               EnergyMix.EnergySources.All(source => source.Value.ToString() is "Sun" or "Wind" or "Geothermal" or "Biogas" or "OtherGreen");

        private static ElectricEnergySourceRatio? ToDatexII(PercentageOf<POI.EnergySourceCategory> Source)
        {

            var energySource = Source.Value.ToString() switch {
                                   "Gas"            => "gas",
                                   "Coal"           => "coal",
                                   "StoneCoal"      => "coal",
                                   "Oil"            => "generalFossil",
                                   "OtherFossil"    => "generalFossil",
                                   "NuclearFision"  => "nuclear",
                                   "NuclearFusion"  => "nuclear",
                                   "Sun"            => "solar",
                                   "Wind"           => "wind",
                                   "Geothermal"     => "generalGreen",
                                   "Biogas"         => "biogas",
                                   "OtherGreen"     => "generalGreen",
                                   _                => "other"
                               };

            return new ElectricEnergySourceRatio(
                       ElectricEnergySourceType.Parse(energySource),
                       PercentageDouble.Parse(Source.Percent),
                       energySource == "other" ? Source.Value.ToString() : null
                   );

        }

        private static POI.EnergyMix? ToPOI3(this ElectricEnergy  ElectricEnergy,
                                             Languages            Language)
        {

            var sources = ElectricEnergy.ElectricEnergySourceRatios.
                              Select(ratio => (Category: ratio.EnergySource.ToString() switch {
                                                             "biogas"         => "Biogas",
                                                             "coal"           => "Coal",
                                                             "gas"            => "Gas",
                                                             "nuclear"        => "NuclearFision",
                                                             "solar"          => "Sun",
                                                             "water"          => "OtherGreen",
                                                             "wind"           => "Wind",
                                                             "generalGreen"   => "OtherGreen",
                                                             "generalFossil"  => "OtherFossil",
                                                             _                => null
                                                         },
                                               Percent: (Single) ratio.SourceRatioValue.Value)).
                              Where (source => source.Category is not null).
                              Select(source => new PercentageOf<POI.EnergySourceCategory>(POI.EnergySourceCategory.Parse(source.Category!), source.Percent)).
                              ToArray();

            if (sources.Length == 0 && ElectricEnergy.EnergyProductName is null)
                return null;

            return new POI.EnergyMix(
                       sources,
                       [],
                       new ImmutableI18NString(I18NString.Empty),
                       new ImmutableI18NString(ToI18NString(ElectricEnergy.EnergyProductName, Language) ?? I18NString.Empty)
                   );

        }

        #endregion

        #region (private) Parking, facilities, authentication, connectors, charging modes

        private static EnergyInfrastructureSiteType? ToTypeOfSite(POI.ParkingType ParkingType)

            => ParkingType.ToString() switch {
                   "ON_STREET"           => EnergyInfrastructureSiteType.Parse("onstreet"),
                   "PARKING_GARAGE"      => EnergyInfrastructureSiteType.Parse("inBuilding"),
                   "UNDERGROUND_GARAGE"  => EnergyInfrastructureSiteType.Parse("inBuilding"),
                   "PARKING_LOT"         => EnergyInfrastructureSiteType.Parse("openSpace"),
                   "ALONG_MOTORWAY"      => EnergyInfrastructureSiteType.Parse("openSpace"),
                   "ON_DRIVEWAY"         => EnergyInfrastructureSiteType.Parse("other"),
                   _                     => null
               };

        private static POI.ParkingType? ToParkingType(EnergyInfrastructureSiteType TypeOfSite)

            => TypeOfSite.ToString() switch {
                   "onstreet"    => POI.ParkingType.ON_STREET,
                   "inBuilding"  => POI.ParkingType.PARKING_GARAGE,
                   "openSpace"   => POI.ParkingType.PARKING_LOT,
                   _             => null
               };

        private static readonly (String Facility, String? Service, String? Equipment)[] Facilities = [
            ("HOTEL",         "hotel",        null),
            ("RESTAURANT",    "restaurant",   null),
            ("CAFE",          "cafe",         null),
            ("MALL",          "shop",         null),
            ("SUPERMARKET",   "foodShopping", null),
            ("BIKE_SHARING",  "bikeSharing",  null),
            ("FUEL_STATION",  "petrolStation", null),
            ("WIFI",          null,           "internetWireless")
        ];

        private static IEnumerable<ASupplementalFacility> ToDatexII(IEnumerable<POI.Facility>  Facilities,
                                                                    String                     PoolId,
                                                                    String                     Version,
                                                                    ICollection<String>        Warnings)
        {

            var supplementalFacilities = new List<ASupplementalFacility>();

            foreach (var facility in Facilities)
            {

                var mapping = POI3Mapper.Facilities.FirstOrDefault(mapping => mapping.Facility == facility.ToString());

                if (mapping.Service is not null)
                    supplementalFacilities.Add(new SupplementalServiceFacility($"{PoolId}*{facility}", Version, ServiceFacilityType.Parse(mapping.Service)));

                else if (mapping.Equipment is not null)
                    supplementalFacilities.Add(new SupplementalEquipment      ($"{PoolId}*{facility}", Version, EquipmentType.      Parse(mapping.Equipment)));

                else
                    Warnings.Add($"The facility '{facility}' of the charging pool '{PoolId}' has no counterpart and is left out!");

            }

            return supplementalFacilities;

        }

        private static POI.Facility? ToPOI3Facility(ASupplementalFacility SupplementalFacility)
        {

            var type   = SupplementalFacility switch {
                             SupplementalServiceFacility service    => service.  ServiceFacilityType.ToString() switch {
                                                                           "restaurantSelfService"    => "restaurant",
                                                                           "motorwayRestaurant"       => "restaurant",
                                                                           "motorwayRestaurantSmall"  => "restaurant",
                                                                           "snackBar"                 => "cafe",
                                                                           var other                  => other
                                                                       },
                             SupplementalEquipment       equipment  => equipment.EquipmentType.ToString(),
                             _                                      => null
                         };

            var facility = Facilities.FirstOrDefault(mapping => mapping.Service == type || mapping.Equipment == type).Facility;

            return facility is not null
                       ? POI.Facility.TryParse(facility)
                       : null;

        }

        private static readonly (String Mode, String Method)[] AuthenticationMethods = [
            (POI.AuthenticationMode.RFIDType,                      "rfid"),
            (POI.AuthenticationMode.NFCType,                       "nfc"),
            (POI.AuthenticationMode.CreditCardType,                "creditCard"),
            (POI.AuthenticationMode.DebitCardType,                 "debitCard"),
            (POI.AuthenticationMode.PrepaidCardType,               "prepaidCard"),
            (POI.AuthenticationMode.PINPADType,                    "pinpad"),
            (POI.AuthenticationMode.REMOTEType,                    "apps"),
            (POI.AuthenticationMode.ISO15118_PLCType,              "plc"),
            (POI.AuthenticationMode.ISO15118_AirType,              "overTheAir"),
            (POI.AuthenticationMode.SMSType,                       "phoneSMS"),
            (POI.AuthenticationMode.PhoneCallType,                 "phoneDialog"),
            (POI.AuthenticationMode.DirectPaymentType,             "website"),
            (POI.AuthenticationMode.FreeChargingType,              "unlimitedAccess"),
            (POI.AuthenticationMode.NoAuthenticationRequiredType,  "unlimitedAccess")
        ];

        /// <summary>
        /// The authentication and identification methods of the charging pool: from its authentication modes, and its
        /// payment options cash, credit and debit card.
        /// </summary>
        private static IEnumerable<AuthenticationAndIdentificationType> AuthenticationMethodsOf(POI.ChargingPool ChargingPool)
        {

            var methods = ChargingPool.AuthenticationModes.
                              Select(mode    => AuthenticationMethods.FirstOrDefault(mapping => mapping.Mode == mode.Type).Method).
                              Where (method  => method is not null).
                              ToList();

            if (ChargingPool.PaymentOptions.Contains(POI.PaymentOptions.Cash))        methods.Add("cashPayment");
            if (ChargingPool.PaymentOptions.Contains(POI.PaymentOptions.CreditCard))  methods.Add("creditCard");
            if (ChargingPool.PaymentOptions.Contains(POI.PaymentOptions.DebitCard))   methods.Add("debitCard");

            return methods.Distinct().Select(method => AuthenticationAndIdentificationType.Parse(method!));

        }

        private static IEnumerable<POI.AuthenticationMode> AuthenticationModesOf(IEnumerable<AuthenticationAndIdentificationType> Methods)
        {

            var modes = new List<POI.AuthenticationMode>();

            foreach (var method in Methods.Select(method => method.ToString()).Distinct())
            {

                switch (method)
                {

                    case "rfid":
                    case "activeRFIDChip":
                    case "mifareClassic":
                    case "mifareDesfire":
                    case "calypso":
                        if (!modes.Any(mode => mode.Type == POI.AuthenticationMode.RFIDType))
                            modes.Add(POI.AuthenticationMode.RFID());
                        break;

                    case "nfc":            modes.Add(POI.AuthenticationMode.NFC);           break;
                    case "creditCard":     modes.Add(POI.AuthenticationMode.CreditCard);    break;
                    case "debitCard":      modes.Add(POI.AuthenticationMode.DebitCard);     break;
                    case "prepaidCard":    modes.Add(POI.AuthenticationMode.PrepaidCard);   break;
                    case "pinpad":         modes.Add(POI.AuthenticationMode.PINPAD);        break;
                    case "apps":           modes.Add(POI.AuthenticationMode.REMOTE);        break;
                    case "plc":            modes.Add(POI.AuthenticationMode.ISO15118_PLC);  break;
                    case "overTheAir":     modes.Add(POI.AuthenticationMode.ISO15118_Air);  break;

                }

            }

            return modes;

        }

        private static IEnumerable<POI.PaymentOptions> PaymentOptionsOf(IEnumerable<AuthenticationAndIdentificationType> Methods)
        {

            var options = new List<POI.PaymentOptions>();
            var methods = Methods.Select(method => method.ToString()).ToHashSet();

            if (methods.Contains("cashPayment"))  options.Add(POI.PaymentOptions.Cash);
            if (methods.Contains("creditCard"))   options.Add(POI.PaymentOptions.CreditCard);
            if (methods.Contains("debitCard"))    options.Add(POI.PaymentOptions.DebitCard);

            return options;

        }

        private static readonly (String POI3, String DatexII)[] ConnectorTypes = [
            ("CHADEMO",                "chademo"),
            ("DOMESTIC_A",             "domesticA"),
            ("DOMESTIC_B",             "domesticB"),
            ("DOMESTIC_C",             "domesticC"),
            ("DOMESTIC_D",             "domesticD"),
            ("DOMESTIC_E",             "domesticE"),
            ("DOMESTIC_F",             "domesticF"),
            ("DOMESTIC_G",             "domesticG"),
            ("DOMESTIC_H",             "domesticH"),
            ("DOMESTIC_I",             "domesticI"),
            ("DOMESTIC_J",             "domesticJ"),
            ("DOMESTIC_K",             "domesticK"),
            ("DOMESTIC_L",             "domesticL"),
            ("DOMESTIC_M",             "domesticM"),
            ("DOMESTIC_N",             "domesticN"),
            ("DOMESTIC_O",             "domesticO"),
            ("IEC_60309_2_single_16",  "iec60309x2single16"),
            ("IEC_60309_2_three_16",   "iec60309x2three16"),
            ("IEC_60309_2_three_32",   "iec60309x2three32"),
            ("IEC_60309_2_three_64",   "iec60309x2three64"),
            ("IEC_62196_T1",           "iec62196T1"),
            ("IEC_62196_T1_COMBO",     "iec62196T1COMBO"),
            ("IEC_62196_T2",           "iec62196T2"),
            ("IEC_62196_T2_COMBO",     "iec62196T2COMBO"),
            ("IEC_62196_T3A",          "iec62196T3A"),
            ("IEC_62196_T3C",          "iec62196T3C"),
            ("PANTOGRAPH_BOTTOM_UP",   "pantographBottomUp"),
            ("PANTOGRAPH_TOP_DOWN",    "pantographTopDown"),
            ("TESLA_R",                "teslaR"),
            ("TESLA_S",                "teslaS")
        ];

        /// <summary>
        /// The connector type of the given plug type, if there is one.
        /// </summary>
        public static ConnectorType? ToConnectorType(POI.ChargingConnectorType Type)
        {

            var datexII = ConnectorTypes.FirstOrDefault(mapping => String.Equals(mapping.POI3, Type.ToString(), StringComparison.OrdinalIgnoreCase)).DatexII;

            return datexII is not null
                       ? EnergyInfrastructure.ConnectorType.Parse(datexII)
                       : null;

        }

        /// <summary>
        /// The plug type of the given connector type, if there is one.
        /// </summary>
        public static POI.ChargingConnectorType? ToPOI3(ConnectorType Type)
        {

            var poi3 = ConnectorTypes.FirstOrDefault(mapping => mapping.DatexII == Type.ToString()).POI3;

            return poi3 is not null
                       ? POI.ChargingConnectorType.Parse(poi3)
                       : null;

        }

        /// <summary>
        /// The charging mode of a connector of the given EVSE: CCS for a combo plug, mode 4 for direct current, mode 3
        /// for three phases, mode 2 for one phase where the EVSE says mode 2. DATEX II has no mode 3 with one phase.
        /// </summary>
        private static ChargingMode? ChargingModeOf(POI.EVSE                   EVSE,
                                                    POI.ChargingConnectorType  Type)
        {

            var currentTypes   = EVSE.CurrentTypes;
            var chargingModes  = EVSE.ChargingModes;

            if (Type.ToString() is "IEC_62196_T1_COMBO" or "IEC_62196_T2_COMBO")
                return ChargingMode.Parse("ccs");

            if (currentTypes.HasFlag(POI.CurrentTypes.DC))
                return ChargingMode.Parse("mode4DC");

            if (currentTypes.HasFlag(POI.CurrentTypes.AC_ThreePhases))
                return ChargingMode.Parse(chargingModes.HasFlag(POI.ChargingModes.Mode_2) ? "mode2AC3p"
                                        : chargingModes.HasFlag(POI.ChargingModes.Mode_1) ? "mode1AC3p"
                                        : "mode3AC3p");

            if (currentTypes.HasFlag(POI.CurrentTypes.AC_OnePhase))
                return chargingModes.HasFlag(POI.ChargingModes.Mode_2) ? ChargingMode.Parse("mode2AC1p")
                     : chargingModes.HasFlag(POI.ChargingModes.Mode_1) ? ChargingMode.Parse("mode1AC1p")
                     : null;

            return null;

        }

        #endregion

    }

}
