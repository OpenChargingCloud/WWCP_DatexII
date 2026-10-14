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
using System.Diagnostics.CodeAnalysis;

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Information on current status and availability of supplemental facilities 
    /// (for example number of free electric charging stations).
    /// </summary>
    [XmlType("SupplementalFacilityStatus", Namespace = "http://datex2.eu/schema/3/afirFacilities")]
    public class SupplementalFacilityStatus(FacilityObjectVersionedReference  Reference,

                                            UInt16?                           QuantityOverride                      = null,
                                            UInt16?                           NumberOfSubitemsOverride              = null,
                                            UInt16?                           VacantSubitems                        = null,
                                            OperationStatus?                  EquipmentOperationStatus              = null,
                                            XElement?                         SupplementalFacilityStatusExtension   = null,

                                            DateTimeOffset?                   LastUpdated                           = null,
                                            OpeningStatus?                    OpeningStatus                         = null,
                                            OperationStatus?                  OperationStatus                       = null,
                                            Boolean?                          RegularOperatingHoursInForce          = null,
                                            MultilingualString?               StatusDescription                     = null,
                                            AOperatingHours?                  NewOperatingHours                     = null,
                                            Fault?                            Fault                                 = null,
                                            XElement?                         FacilityObjectStatusExtension         = null)

        : FacilityObjectStatus(Reference,
                               LastUpdated,
                               OpeningStatus,
                               OperationStatus,
                               RegularOperatingHoursInForce,
                               StatusDescription,
                               NewOperatingHours,
                               Fault,
                               FacilityObjectStatusExtension)

    {

        #region Properties

        /// <summary>
        /// Overrides the static quantity information (for example because of long- or midterm closures, such as renovation).
        /// </summary>
        [XmlElement("quantityOverride",                      Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16?           QuantityOverride                       { get; } = QuantityOverride;

        /// <summary>
        /// Overrides the static value 'numberOfSubitems' (for example because of long- or midterm closures, such as renovation).
        /// </summary>
        [XmlElement("numberOfSubitemsOverride",              Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16?           NumberOfSubitemsOverride               { get; } = NumberOfSubitemsOverride;

        /// <summary>
        /// Sets the number of currently vacant elements of either equipment (e.g. free toilets) 
        /// or service facility subitems (e.g. free restaurant places).
        /// </summary>
        [XmlElement("vacantSubitems",                        Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16?           VacantSubitems                         { get; } = VacantSubitems;

        /// <summary>
        /// Specifies whether this supplemental equipment is available / in operation or not.
        /// </summary>
        [XmlElement("equipmentOperationStatus",              Namespace = "http://datex2.eu/schema/3/afirFacilities")]
        public OperationStatus?  EquipmentOperationStatus               { get; } = EquipmentOperationStatus;

        /// <summary>
        /// Optional extension element for additional supplemental facility status information.
        /// </summary>
        [XmlElement("_supplementalFacilityStatusExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?         SupplementalFacilityStatusExtension    { get; } = SupplementalFacilityStatusExtension;

        #endregion


        #region TryParseXML(XML, out SupplementalFacilityStatus, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a supplemental facility status.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="SupplementalFacilityStatus">The parsed supplemental facility status.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                              XML,
                                          [NotNullWhen(true)]  out SupplementalFacilityStatus?  SupplementalFacilityStatus,
                                          [NotNullWhen(false)] out String?                      ErrorResponse)
        {

            SupplementalFacilityStatus = null;

            if (!FacilityObjectStatusParts.TryParseXML(XML, out var parts, out ErrorResponse))
                return false;

            if (!XML.TryParseOptionalValue(DatexIINS.Facilities + "quantityOverride",          "quantity override",            DatexIIXMLExtensions.TryParseUInt16,  out UInt16?           quantityOverride,         out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "numberOfSubitemsOverride",  "number of subitems override",  DatexIIXMLExtensions.TryParseUInt16,  out UInt16?           numberOfSubitemsOverride, out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "vacantSubitems",            "vacant subitems",              DatexIIXMLExtensions.TryParseUInt16,  out UInt16?           vacantSubitems,           out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "equipmentOperationStatus",  "equipment operation status",   Facilities.OperationStatus.TryParse,  out OperationStatus?  equipmentOperationStatus, out ErrorResponse))
            {
                return false;
            }

            SupplementalFacilityStatus = new SupplementalFacilityStatus(
                                             parts.Reference,
                                             quantityOverride,
                                             numberOfSubitemsOverride,
                                             vacantSubitems,
                                             equipmentOperationStatus,
                                             XML.Element(DatexIINS.Facilities + "_supplementalFacilityStatusExtension"),
                                             parts.LastUpdated,
                                             parts.OpeningStatus,
                                             parts.OperationStatus,
                                             parts.RegularOperatingHoursInForce,
                                             parts.StatusDescription,
                                             parts.NewOperatingHours,
                                             parts.Fault,
                                             parts.FacilityObjectStatusExtension
                                         );

            return true;

        }

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.Facilities + "supplementalFacilityStatus",

                   ToXMLElements(),

                   QuantityOverride.HasValue
                       ? new XElement(DatexIINS.Facilities + "quantityOverride",           QuantityOverride.        Value)
                       : null,

                   NumberOfSubitemsOverride.HasValue
                       ? new XElement(DatexIINS.Facilities + "numberOfSubitemsOverride",   NumberOfSubitemsOverride.Value)
                       : null,

                   VacantSubitems.HasValue
                       ? new XElement(DatexIINS.Facilities + "vacantSubitems",             VacantSubitems.          Value)
                       : null,

                   EquipmentOperationStatus.HasValue
                       ? new XElement(DatexIINS.Facilities + "equipmentOperationStatus",   EquipmentOperationStatus.Value.ToString())
                       : null,

                   SupplementalFacilityStatusExtension is not null
                       ? new XElement(DatexIINS.Facilities + "_supplementalFacilityStatusExtension", SupplementalFacilityStatusExtension)
                       : null

               );

        #endregion

    }

}
