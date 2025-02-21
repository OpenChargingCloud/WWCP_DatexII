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

using System.Xml.Serialization;

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// A site where vehicles can be supplied with energy, including all buildings, stations, parking spaces and other associated services.
    /// </summary>
    [XmlType("EnergyInfrastructureSite", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyInfrastructureSite(String                                Id,
                                          String                                Version,

                                          MultilingualString?                   Name                     = null,
                                          IEnumerable<MultilingualString>?      Alias                    = null,
                                          String?                               ExternalIdentifier       = null,
                                          DateTime?                             LastUpdated              = null,
                                          MultilingualString?                   Description              = null,
                                          IEnumerable<Accessibilities>?         Accessibility            = null,
                                          IEnumerable<MultilingualString>?      AdditionalInformation    = null,
                                          IEnumerable<URL>?                     InformationWebsites      = null,
                                          IEnumerable<URL>?                     PhotoURLs                = null,
                                          IEnumerable<Image>?                   Photos                   = null,
                                          AOperatingHours?                      OperatingHours           = null,
                                          LocationReference?                    LocationReference        = null,
                                          AOrganisation?                        Owner                    = null,
                                          AOrganisation?                        Operator                 = null,
                                          AOrganisation?                        Helpdesk                 = null,
                                          IEnumerable<VehicleCharacteristics>?  ApplicableForVehicles    = null,
                                          Dimension?                            Dimension                = null,
                                          Amenities?                            Amenities                = null,

                                          IEnumerable<ASupplementalFacility>?   SupplementalFacilities   = null,
                                          IEnumerable<DedicatedParkingSpaces>?  DedicatedParkingSpaces   = null)

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
                   DedicatedParkingSpaces)

    {

        /// <summary>
        /// Specifies the type of the site.
        /// </summary>
        [XmlElement("typeOfSite", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public EnergyInfrastructureSiteTypes?            TypeOfSite { get; set; }

        /// <summary>
        /// The brand of the site.
        /// </summary>
        [XmlElement("brand", Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?                       Brand { get; set; }

        /// <summary>
        /// Limitation to a set of users (exclusive).
        /// </summary>
        [XmlElement("exclusiveUsers", Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<UserTypes>                    ExclusiveUsers { get; set; }

        /// <summary>
        /// Users that are preferred at this site (but not exclusive).
        /// </summary>
        [XmlElement("preferredUsers", Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<UserTypes>                    PreferredUsers { get; set; }

        /// <summary>
        /// Specifies the type of service available at an EnergyInfrastructureSite.
        /// </summary>
        [XmlElement("serviceType", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<ServiceType>                  ServiceTypes { get; set; }

        /// <summary>
        /// Possibility to specify the location of the site's entrance.
        /// </summary>
        [XmlElement("entrance", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public IEnumerable<ALocation>                    Entrances { get; set; }

        /// <summary>
        /// Possibility to specify the location of the site's exit.
        /// </summary>
        [XmlElement("exit", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public IEnumerable<ALocation>                    Exits { get; set; }

        /// <summary>
        /// Specifications of charging stations on the site.
        /// </summary>
        [XmlElement("energyInfrastructureStation", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyInfrastructureStation>  EnergyInfrastructureStations { get; set; }

        ///// <summary>
        ///// Optional extension element for additional EnergyInfrastructureSite information.
        ///// </summary>
        //[XmlElement("_energyInfrastructureSiteExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? EnergyInfrastructureSiteExtension { get; set; }

    }


}
