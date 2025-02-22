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
    /// Information on current status and availability of supplemental facilities 
    /// (for example number of free electric charging stations).
    /// </summary>
    [XmlType("SupplementalFacilityStatus", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class SupplementalFacilityStatus(FacilityObjectVersionedReference  Reference,

                                            DateTime?                         LastUpdated                    = null,
                                            OpeningStatus?                    OpeningStatus                  = null,
                                            OperationStatus?                  OperationStatus                = null,
                                            Boolean?                          RegularOperatingHoursInForce   = null,
                                            MultilingualString?               StatusDescription              = null,
                                            AOperatingHours?                  NewOperatingHours              = null,
                                            Fault?                            Fault                          = null,

                                            UInt16?                           QuantityOverride               = null,
                                            UInt16?                           NumberOfSubitemsOverride       = null,
                                            UInt16?                           VacantSubitems                 = null,
                                            OperationStatus?                  EquipmentOperationStatus       = null)

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
        /// Overrides the static quantity information (for example because of long- or midterm closures, such as renovation).
        /// </summary>
        [XmlElement("quantityOverride",                      Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16?           QuantityOverride                       { get; set; } = QuantityOverride;

        /// <summary>
        /// Overrides the static value 'numberOfSubitems' (for example because of long- or midterm closures, such as renovation).
        /// </summary>
        [XmlElement("numberOfSubitemsOverride",              Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16?           NumberOfSubitemsOverride               { get; set; } = NumberOfSubitemsOverride;

        /// <summary>
        /// Sets the number of currently vacant elements of either equipment (e.g. free toilets) 
        /// or service facility subitems (e.g. free restaurant places).
        /// </summary>
        [XmlElement("vacantSubitems",                        Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16?           VacantSubitems                         { get; set; } = VacantSubitems;

        /// <summary>
        /// Specifies whether this supplemental equipment is available / in operation or not.
        /// </summary>
        [XmlElement("equipmentOperationStatus",              Namespace = "http://datex2.eu/schema/3/facilities")]
        public OperationStatus?  EquipmentOperationStatus               { get; set; } = EquipmentOperationStatus;

        /// <summary>
        /// Optional extension element for additional supplemental facility status information.
        /// </summary>
        [XmlElement("_supplementalFacilityStatusExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?         SupplementalFacilityStatusExtension    { get; set; }

    }

}
