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

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;
using System.Xml.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Dedicated parking spaces directly belonging to a facility (which is usually not a parking site itself).
    /// </summary>
    [XmlType("DedicatedParkingSpaces", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class DedicatedParkingSpaces(String                                Id,
                                        String                                Version,
                                        UInt16                                NumberOfSpaces,

                                        MultilingualString?                   Name                    = null,
                                        IEnumerable<MultilingualString>?      Alias                   = null,
                                        String?                               ExternalIdentifier      = null,
                                        DateTime?                             LastUpdated             = null,
                                        MultilingualString?                   Description             = null,
                                        IEnumerable<Accessibilities>?         Accessibility           = null,
                                        IEnumerable<MultilingualString>?      AdditionalInformation   = null,
                                        IEnumerable<URL>?                     InformationWebsites     = null,
                                        IEnumerable<URL>?                     PhotoURLs               = null,
                                        IEnumerable<Image>?                   Photos                  = null,
                                        AOperatingHours?                      OperatingHours          = null,
                                        ALocationReference?                   LocationReference       = null,
                                        AOrganisation?                        Owner                   = null,
                                        AOrganisation?                        Operator                = null,
                                        AOrganisation?                        Helpdesk                = null,
                                        IEnumerable<VehicleCharacteristics>?  ApplicableForVehicles   = null,
                                        Dimension?                            Dimension               = null,
                                        Amenities?                            Amenities               = null,

                                        IEnumerable<UserTypes>?               UserSpecific            = null)

        : AFacilityObject(Id,
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
                          Amenities)

    {

        /// <summary>
        /// The number of parking spaces.
        /// </summary>
        [XmlElement(ElementName = "numberOfSpaces",  Namespace = "http://datex2.eu/schema/3/facilities")]
        public UInt16                  NumberOfSpaces                     { get; set; } = NumberOfSpaces;

        /// <summary>
        /// The indicated parking spaces are specifically for the given set of users.
        /// </summary>
        [XmlElement(ElementName = "userSpecific",    Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<UserTypes>  UserSpecific                       { get; set; } = UserSpecific ?? [];

        /// <summary>
        /// Optional extension element for additional dedicated parking spaces information.
        /// </summary>
        [XmlElement("_dedicatedParkingSpacesExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?               DedicatedParkingSpacesExtension    { get; set; }

    }

}
