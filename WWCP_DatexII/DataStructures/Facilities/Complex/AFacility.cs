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
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Provides information about a facility, which may represent any kind of site, building, or structure, 
    /// and may be a composite facility that includes supplemental facilities.
    /// </summary>
    [XmlType("Facility", Namespace = "http://datex2.eu/schema/3/facilities")]
    public abstract class Facility(String                                Id,
                                   String                                Version,

                                   MultilingualString?                   Name                      = null,
                                   IEnumerable<MultilingualString>?      Alias                     = null,
                                   String?                               ExternalIdentifier        = null,
                                   DateTimeOffset?                       LastUpdated               = null,
                                   MultilingualString?                   Description               = null,
                                   IEnumerable<Accessibility>?           Accessibility             = null,
                                   IEnumerable<MultilingualString>?      AdditionalInformation     = null,
                                   IEnumerable<URL>?                     InformationWebsites       = null,
                                   IEnumerable<URL>?                     PhotoURLs                 = null,
                                   IEnumerable<Image>?                   Photos                    = null,
                                   AOperatingHours?                      OperatingHours            = null,
                                   ALocationReference?                   LocationReference         = null,
                                   AOrganisation?                        Owner                     = null,
                                   AOrganisation?                        Operator                  = null,
                                   AOrganisation?                        Helpdesk                  = null,
                                   IEnumerable<VehicleCharacteristics>?  ApplicableForVehicles     = null,
                                   Dimension?                            Dimension                 = null,
                                   Amenities?                            Amenities                 = null,

                                   IEnumerable<ASupplementalFacility>?   SupplementalFacilities    = null,
                                   IEnumerable<DedicatedParkingSpaces>?  DedicatedParkingSpaces    = null,

                                   XElement?                             FacilityObjectExtension   = null,
                                   XElement?                             FacilityExtension         = null)

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
                          Amenities,

                          FacilityObjectExtension)

    {

        #region Properties

        /// <summary>
        /// Supplemental facilities, e.g. additional service or equipment available on the site.
        /// </summary>
        [XmlElement("supplementalFacility",    Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<ASupplementalFacility>   SupplementalFacilities    { get; } = SupplementalFacilities?.Distinct() ?? [];

        /// <summary>
        /// Dedicated parking spaces associated with the facility.
        /// </summary>
        [XmlElement("dedicatedParkingSpaces",  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<DedicatedParkingSpaces>  DedicatedParkingSpaces    { get; } = DedicatedParkingSpaces?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional facility information.
        /// </summary>
        [XmlElement("_facilityExtension",      Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                            FacilityExtension         { get; } = FacilityExtension;

        #endregion

    }

}
