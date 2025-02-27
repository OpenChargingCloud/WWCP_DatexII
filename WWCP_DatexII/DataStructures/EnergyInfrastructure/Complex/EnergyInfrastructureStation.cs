/*
 * Copyright (c) 2014-2025 GraphDefined GmbH <achim.friedland@graphdefined.com>
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
using System.Xml.Serialization;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A collection of associated refill points (which can be of different type). An example would be a fuel dispenser that serves different types of fuel.
    /// Often the vehicle space of the station is shared between the different refill points.
    /// </summary>
    [XmlType("EnergyInfrastructureStation", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyInfrastructureStation(String                                            Id,
                                             String                                            Version,
                                             String                                            StationIdBNetzA,
                                             Watt                                              TotalMaximumPower,
                                             IEnumerable<AuthenticationAndIdentificationType>  AuthenticationAndIdentificationMethods,
                                             UInt16                                            NumberOfRefillPoints,
                                             IEnumerable<Languages>                            UserInterfaceLanguages,
                                             IEnumerable<Service>                              ServiceTypes,

                                             MultilingualString?                               Name                                   = null,
                                             IEnumerable<MultilingualString>?                  Alias                                  = null,
                                             String?                                           ExternalIdentifier                     = null,
                                             DateTime?                                         LastUpdated                            = null,
                                             MultilingualString?                               Description                            = null,
                                             IEnumerable<Accessibility>?                       Accessibility                          = null,
                                             IEnumerable<MultilingualString>?                  AdditionalInformation                  = null,
                                             IEnumerable<URL>?                                 InformationWebsites                    = null,
                                             IEnumerable<URL>?                                 PhotoURLs                              = null,
                                             IEnumerable<Image>?                               Photos                                 = null,
                                             AOperatingHours?                                  OperatingHours                         = null,
                                             ALocationReference?                               LocationReference                      = null,
                                             AOrganisation?                                    Owner                                  = null,
                                             AOrganisation?                                    Operator                               = null,
                                             AOrganisation?                                    Helpdesk                               = null,
                                             IEnumerable<VehicleCharacteristics>?              ApplicableForVehicles                  = null,
                                             Dimension?                                        Dimension                              = null,
                                             Amenities?                                        Amenities                              = null,

                                             IEnumerable<ASupplementalFacility>?               SupplementalFacilities                 = null,
                                             IEnumerable<DedicatedParkingSpaces>?              DedicatedParkingSpaces                 = null,

                                             IEnumerable<VersionedReference>?                  RefillPointByReference                 = null,
                                             AOrganisation?                                    EnergyDistributor                      = null,
                                             IEnumerable<AOrganisation>?                       MobilityServiceProviders               = null,
                                             IEnumerable<AOrganisation>?                       RoamingPlatforms                       = null,
                                             IEnumerable<RefillPoint>?                         RefillPoints                           = null,
                                             IEnumerable<ElectricEnergy>?                      ElectricEnergy                         = null,

                                             XElement?                                         FacilityObjectExtension                = null,
                                             XElement?                                         FacilityExtension                      = null,
                                             XElement?                                         EnergyInfrastructureStationExtension   = null)

        : Facility(Id,
                   Version,

                   Name,
                   Alias,
                   ExternalIdentifier,
                   LastUpdated,
                   Description,
                   Accessibility,
                   AdditionalInformation,
                   InformationWebsites,
                   PhotoURLs,
                   Photos,
                   OperatingHours,
                   LocationReference,
                   Owner,
                   Operator,
                   Helpdesk,
                   ApplicableForVehicles,
                   Dimension,
                   Amenities,

                   SupplementalFacilities,
                   DedicatedParkingSpaces,

                   FacilityObjectExtension,
                   FacilityExtension)

    {

        #region Properties

        /// <summary>
        /// Ladeeinrichtungs-ID der Bundesnetzagentur.
        /// (See https://www.bundesnetzagentur.de/DE/Fachthemen/ElektrizitaetundGas/E-Mobilitaet/AnzeigeNEU/start.html)
        /// </summary>
        [XmlElement("stationIdBNetzA",                         Namespace = "http://datex2.eu/schema/3/common")]
        public String                                            StationIdBNetzA                           { get; } = StationIdBNetzA;

        /// <summary>
        /// The total maximum power of the station.
        /// </summary>
        [XmlElement("totalMaximumPower",                       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Watt                                              TotalMaximumPower                         { get; } = TotalMaximumPower;

        /// <summary>
        /// Information on what methods of identification and/or authentication are accepted.
        /// </summary>
        [XmlElement("authenticationAndIdentificationMethods",  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<AuthenticationAndIdentificationType>  AuthenticationAndIdentificationMethods    { get; } = AuthenticationAndIdentificationMethods.Distinct();

        /// <summary>
        /// Information on the number of refill points at the station.
        /// </summary>
        [XmlElement("numberOfRefillPoints",                    Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16                                            NumberOfRefillPoints                      { get; } = NumberOfRefillPoints;

        /// <summary>
        /// Refill points can be attached to an EnergyInfrastructureStation instance either directly or by reference.
        /// </summary>
        [XmlElement("refillPointByReference",                  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<VersionedReference>                   RefillPointByReference                    { get; } = RefillPointByReference?.               Distinct() ?? [];

        /// <summary>
        /// Languages in which a user interface is available.
        /// </summary>
        [XmlElement("userInterfaceLanguage",                   Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Languages>                            UserInterfaceLanguages                    { get; } = UserInterfaceLanguages?.               Distinct() ?? [];

        /// <summary>
        /// The distributor of the energy.
        /// </summary>
        [XmlElement("energyDistributor",                       Namespace = "http://datex2.eu/schema/3/facilities")]
        public AOrganisation?                                    EnergyDistributor                         { get; } = EnergyDistributor;

        /// <summary>
        /// Mobility service providers offering contract-based recharging.
        /// </summary>
        [XmlElement("mobilityServiceProvider",                 Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<AOrganisation>                        MobilityServiceProviders                  { get; } = MobilityServiceProviders?.             Distinct() ?? [];

        /// <summary>
        /// Supported roaming platforms.
        /// </summary>
        [XmlElement("roamingPlatform",                         Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<AOrganisation>                        RoamingPlatforms                          { get; } = RoamingPlatforms?.                     Distinct() ?? [];

        /// <summary>
        /// Specify the type of service available at the EnergyInfrastructureStation.
        /// </summary>
        [XmlElement("serviceType",                             Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<Service>                              ServiceTypes                              { get; } = ServiceTypes.                          Distinct();

        /// <summary>
        /// Specifications of refill points at a charging station.
        /// Note that the refillPointIndex must be unique within the complete site!
        /// </summary>
        [XmlElement("refillPoint",                             Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<RefillPoint>                          RefillPoints                              { get; } = RefillPoints?.                         Distinct() ?? [];

        /// <summary>
        /// Electric energy definitions applicable at the station.
        /// </summary>
        [XmlElement("electricEnergy",                          Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<ElectricEnergy>                       ElectricEnergy                            { get; } = ElectricEnergy?.                       Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional EnergyInfrastructureStation information.
        /// </summary>
        [XmlElement("_energyInfrastructureStationExtension",   Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                         EnergyInfrastructureStationExtension      { get; } = EnergyInfrastructureStationExtension;

        #endregion

    }

}
