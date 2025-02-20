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
    /// One type of supplemental equipment, which is available on some site, for example on a rest area.
    /// </summary>
    [XmlType("SupplementalEquipment", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class SupplementalEquipment(String                                Id,
                                       String                                Version,
                                       EquipmentTypes                        EquipmentType,

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
                                       LocationReference?                    LocationReference       = null,
                                       AOrganisation?                        Owner                   = null,
                                       AOrganisation?                        Operator                = null,
                                       AOrganisation?                        Helpdesk                = null,
                                       IEnumerable<VehicleCharacteristics>?  ApplicableForVehicles   = null,
                                       Dimension?                            Dimension               = null,
                                       Amenities?                            Amenities               = null,

                                       Availabilities?                       Availability            = null,
                                       UInt64?                               Quantity                = null,
                                       Boolean?                              RegularlyCleaned        = null,
                                       IEnumerable<UserTypes>?               ApplicableForUser       = null,
                                       Boolean?                              Nearby                  = null)

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
                                Nearby)

    {
        /// <summary>
        /// One type of equipment.
        /// </summary>
        [XmlElement("equipmentType", Namespace = "http://datex2.eu/schema/3/facilities")]
        public EquipmentTypes  EquipmentType                     { get; set; } = EquipmentType;

        ///// <summary>
        ///// Optional extension element for additional supplemental equipment information.
        ///// </summary>
        //[XmlElement("_supplementalEquipmentExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType?  SupplementalEquipmentExtension    { get; set; }

    }

}
