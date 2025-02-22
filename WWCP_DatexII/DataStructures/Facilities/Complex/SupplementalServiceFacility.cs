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
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;
using System.Xml.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// One type of supplemental service facility. You can specify the number of this service facility type (e.g. 5 restaurants)
    /// as well as the number of subitems (e.g. 200 restaurant places).
    /// </summary>
    [XmlType("SupplementalServiceFacility", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class SupplementalServiceFacility(String                                Id,
                                             String                                Version,
                                             ServiceFacilityTypes                  ServiceFacilityType,

                                             MultilingualString?                   Name                         = null,
                                             IEnumerable<MultilingualString>?      Alias                        = null,
                                             String?                               ExternalIdentifier           = null,
                                             DateTime?                             LastUpdated                  = null,
                                             MultilingualString?                   Description                  = null,
                                             IEnumerable<Accessibilities>?         Accessibility                = null,
                                             IEnumerable<MultilingualString>?      AdditionalInformation        = null,
                                             IEnumerable<URL>?                     InformationWebsites          = null,
                                             IEnumerable<URL>?                     PhotoURLs                    = null,
                                             IEnumerable<Image>?                   Photos                       = null,
                                             AOperatingHours?                      OperatingHours               = null,
                                             ALocationReference?                   LocationReference            = null,
                                             AOrganisation?                        Owner                        = null,
                                             AOrganisation?                        Operator                     = null,
                                             AOrganisation?                        Helpdesk                     = null,
                                             IEnumerable<VehicleCharacteristics>?  ApplicableForVehicles        = null,
                                             Dimension?                            Dimension                    = null,
                                             Amenities?                            Amenities                    = null,

                                             Availabilities?                       Availability                 = null,
                                             UInt64?                               Quantity                     = null,
                                             Boolean?                              RegularlyCleaned             = null,
                                             IEnumerable<UserTypes>?               ApplicableForUser            = null,
                                             Boolean?                              Nearby                       = null,

                                             UInt64?                               NumberOfSubitems             = null,
                                             UInt64?                               DistanceFromOriginFacility   = null)

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
        /// One type of service.
        /// </summary>
        [XmlElement("serviceFacilityType",                    Namespace = "http://datex2.eu/schema/3/facilities")]
        public ServiceFacilityTypes  ServiceFacilityType                     { get; set; } = ServiceFacilityType;

        /// <summary>
        /// The quantity of subitems to this service facility type, e.g. the total number of restaurant places or fuel dispensers etc.
        /// </summary>
        [XmlElement("numberOfSubitems",                       Namespace = "http://datex2.eu/schema/3/common")]
        public UInt64?               NumberOfSubitems                        { get; set; } = NumberOfSubitems;

        /// <summary>
        /// Approximate distance (in metres) between this supplemental facility and some origin facility to which it is clearly related 
        /// (typically a larger facility, e.g. a parking site).
        /// </summary>
        [XmlElement("distanceFromOriginFacility",             Namespace = "http://datex2.eu/schema/3/common")]
        public UInt64?               DistanceFromOriginFacility              { get; set; } = DistanceFromOriginFacility;

        /// <summary>
        /// Optional extension element for additional supplemental service facility information.
        /// </summary>
        [XmlElement("_supplementalServiceFacilityExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?             SupplementalServiceFacilityExtension    { get; set; }

    }

}
