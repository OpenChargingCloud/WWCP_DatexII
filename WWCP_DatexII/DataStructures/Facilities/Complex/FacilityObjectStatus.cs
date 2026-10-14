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
using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Dynamic status information for a facility object.
    /// </summary>
    [XmlType("FacilityObjectStatus", Namespace = "http://datex2.eu/schema/3/afirFacilities")]
    public class FacilityObjectStatus(FacilityObjectVersionedReference  Reference,
                                      DateTimeOffset?                   LastUpdated                     = null,
                                      OpeningStatus?                    OpeningStatus                   = null,
                                      OperationStatus?                  OperationStatus                 = null,
                                      Boolean?                          RegularOperatingHoursInForce    = null,
                                      MultilingualString?               StatusDescription               = null,
                                      AOperatingHours?                  NewOperatingHours               = null,
                                      Fault?                            Fault                           = null,
                                      XElement?                         FacilityObjectStatusExtension   = null)

    {

        #region Properties

        /// <summary>
        /// Reference to the corresponding static facility object.
        /// </summary>
        [XmlElement("reference",                       Namespace = "http://datex2.eu/schema/3/afirFacilities")]
        public FacilityObjectVersionedReference  Reference                        { get; } = Reference;

        /// <summary>
        /// Information on the time when the information was last updated.
        /// </summary>
        [XmlElement("lastUpdated",                     Namespace = "http://datex2.eu/schema/3/common")]
        public DateTimeOffset?                   LastUpdated                      { get; } = LastUpdated;

        /// <summary>
        /// The opening status of this facility (open or closed).
        /// </summary>
        [XmlElement("openingStatus",                   Namespace = "http://datex2.eu/schema/3/afirFacilities")]
        public OpeningStatus?                    OpeningStatus                    { get; } = OpeningStatus;

        /// <summary>
        /// The operation status of this facility.
        /// </summary>
        [XmlElement("operationStatus",                 Namespace = "http://datex2.eu/schema/3/afirFacilities")]
        public OperationStatus?                  OperationStatus                  { get; } = OperationStatus;

        /// <summary>
        /// If true, regular operating hours are in force (can be open or closed).
        /// </summary>
        [XmlElement("regularOperatingHoursInForce",    Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?                          RegularOperatingHoursInForce     { get; } = RegularOperatingHoursInForce;

        /// <summary>
        /// A description for the status of this facility.
        /// </summary>
        [XmlElement("statusDescription",               Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?               StatusDescription                { get; } = StatusDescription;

        /// <summary>
        /// Overrides the operating hours information specified in the static part either with a new reference or with a new defined version.
        /// </summary>
        [XmlElement("newOperatingHours",               Namespace = "http://datex2.eu/schema/3/afirFacilities")]
        public AOperatingHours?                  NewOperatingHours                { get; } = NewOperatingHours;

        /// <summary>
        /// Fault information, if applicable.
        /// </summary>
        [XmlElement("fault",                           Namespace = "http://datex2.eu/schema/3/common")]
        public Fault?                            Fault                            { get; } = Fault;

        /// <summary>
        /// Optional extension element for additional dynamic status information.
        /// </summary>
        [XmlElement("_facilityObjectStatusExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                         FacilityObjectStatusExtension    { get; } = FacilityObjectStatusExtension;

        #endregion


        #region (protected) ToXMLElements()

        /// <summary>
        /// The particles of a facility object status, in schema order.
        /// </summary>
        protected virtual IEnumerable<Object?> ToXMLElements()
        {

            yield return Reference.ToXML();

            if (LastUpdated.HasValue)
                yield return new XElement(DatexIINS.Facilities + "lastUpdated",                    LastUpdated.Value.ToISO8601WithOffset());

            if (OpeningStatus.HasValue)
                yield return new XElement(DatexIINS.Facilities + "openingStatus",                  OpeningStatus.  Value.ToString());

            if (OperationStatus.HasValue)
                yield return new XElement(DatexIINS.Facilities + "operationStatus",                OperationStatus.Value.ToString());

            if (RegularOperatingHoursInForce.HasValue)
                yield return new XElement(DatexIINS.Facilities + "regularOperatingHoursInForce",   RegularOperatingHoursInForce.Value ? "true" : "false");

            if (StatusDescription is not null)
                yield return StatusDescription.ToXML(DatexIINS.Facilities + "statusDescription");

            if (NewOperatingHours is not null)
                yield return NewOperatingHours.ToXML(DatexIINS.Facilities + "newOperatingHours");

            if (Fault is not null)
                yield return Fault.ToXML(DatexIINS.Facilities + "fault");

            if (FacilityObjectStatusExtension is not null)
                yield return new XElement(DatexIINS.Facilities + "_facilityObjectStatusExtension", FacilityObjectStatusExtension);

        }

        #endregion

    }

}
