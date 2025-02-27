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

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A specialization of the SupplementalFacility class adding an ElectricChargingPoint.
    /// </summary>
    [XmlType("ElectricChargingEquipment", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class ElectricChargingEquipment(String                                Id,
                                           String                                Version,
                                           ElectricChargingPoint                 ElectricChargingPoint,

                                           MultilingualString?                   Name                                 = null,
                                           IEnumerable<MultilingualString>?      Alias                                = null,
                                           String?                               ExternalIdentifier                   = null,
                                           DateTime?                             LastUpdated                          = null,
                                           MultilingualString?                   Description                          = null,
                                           IEnumerable<Accessibility>?           Accessibility                        = null,
                                           IEnumerable<MultilingualString>?      AdditionalInformation                = null,
                                           IEnumerable<URL>?                     InformationWebsites                  = null,
                                           IEnumerable<URL>?                     PhotoURLs                            = null,
                                           IEnumerable<Image>?                   Photos                               = null,
                                           AOperatingHours?                      OperatingHours                       = null,
                                           ALocationReference?                   LocationReference                    = null,
                                           AOrganisation?                        Owner                                = null,
                                           AOrganisation?                        Operator                             = null,
                                           AOrganisation?                        Helpdesk                             = null,
                                           IEnumerable<VehicleCharacteristics>?  ApplicableForVehicles                = null,
                                           Dimension?                            Dimension                            = null,
                                           Amenities?                            Amenities                            = null,

                                           Availability?                         Availability                         = null,
                                           UInt64?                               Quantity                             = null,
                                           Boolean?                              RegularlyCleaned                     = null,
                                           IEnumerable<UserType>?                ApplicableForUser                    = null,
                                           Boolean?                              Nearby                               = null,

                                           XElement?                             FacilityObjectExtension              = null,
                                           XElement?                             SupplementalFacilityExtension        = null,
                                           XElement?                             ElectricChargingEquipmentExtension   = null)

        : ASupplementalFacility(Id,
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

                                Availability,
                                Quantity,
                                RegularlyCleaned,
                                ApplicableForUser,
                                Nearby,

                                FacilityObjectExtension,
                                SupplementalFacilityExtension)

    {

        #region Properties

        /// <summary>
        /// The electric charging point.
        /// </summary>
        [XmlElement("electricChargingPoint",                Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public ElectricChargingPoint  ElectricChargingPoint                 { get; } = ElectricChargingPoint;

        /// <summary>
        /// Optional extension element for additional electric charging equipment information.
        /// </summary>
        [XmlElement("_electricChargingEquipmentExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?              ElectricChargingEquipmentExtension    { get; } = ElectricChargingEquipmentExtension;

        #endregion

    }

}
