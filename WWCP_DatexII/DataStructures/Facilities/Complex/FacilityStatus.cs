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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Dynamic status information for a facility.
    /// </summary>
    [XmlType("FacilityStatus", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class FacilityStatus(FacilityObjectVersionedReference          Reference,
                                DateTime?                                 LastUpdated                    = null,
                                OpeningStatus?                            OpeningStatus                  = null,
                                OperationStatus?                          OperationStatus                = null,
                                Boolean?                                  RegularOperatingHoursInForce   = null,
                                MultilingualString?                       StatusDescription              = null,
                                AOperatingHours?                          NewOperatingHours              = null,
                                Fault?                                    Fault                          = null,

                                IEnumerable<SupplementalFacilityStatus>?  SupplementalFacilityStatuses   = null)

        : FacilityObjectStatus(Reference,
                               LastUpdated,
                               OpeningStatus,
                               OperationStatus,
                               RegularOperatingHoursInForce,
                               StatusDescription,
                               NewOperatingHours,
                               Fault)

    {

        /// <summary>
        /// Dynamic status information for supplemental facilities associated with this facility.
        /// </summary>
        [XmlElement("supplementalFacilityStatus",  Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<SupplementalFacilityStatus>  SupplementalFacilityStatuses    { get; set; } = SupplementalFacilityStatuses?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional facility status information.
        /// </summary>
        [XmlElement("_facilityStatusExtension",    Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                FacilityStatusExtension         { get; set; }

    }

}
