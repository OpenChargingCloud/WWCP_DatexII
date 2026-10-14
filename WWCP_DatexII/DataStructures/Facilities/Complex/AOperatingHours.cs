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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Operating hours, either by reference or by specification.
    /// </summary>
    [XmlType("OperatingHours", Namespace = "http://datex2.eu/schema/3/facilities")]
    public abstract class AOperatingHours(ClosureInformation?  ClosureInformation        = null,
                                          XElement?            OperatingHoursExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Optional closure information.
        /// </summary>
        [XmlElement("closureInformation", Namespace = "http://datex2.eu/schema/3/facilities")]
        public ClosureInformation?  ClosureInformation         { get; } = ClosureInformation;

        /// <summary>
        /// Optional extension element for additional operating hours information.
        /// </summary>
        [XmlElement("_operatingHoursExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            OperatingHoursExtension    { get; } = OperatingHoursExtension;

        #endregion


        #region TryParseXML(XML, out OperatingHours, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of operating hours, by its xsi:type.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="OperatingHours">The parsed operating hours.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                   XML,
                                          [NotNullWhen(true)]  out AOperatingHours?  OperatingHours,
                                          [NotNullWhen(false)] out String?           ErrorResponse)
        {

            OperatingHours = null;

            switch (XML.XSIType())
            {

                case "OpenAllHours":
                    if (OpenAllHours.               TryParseXML(XML, out var openAllHours,   out ErrorResponse)) { OperatingHours = openAllHours;   return true; }
                    return false;

                case "OperatingHoursSpecification":
                    if (OperatingHoursSpecification.TryParseXML(XML, out var specification,  out ErrorResponse)) { OperatingHours = specification;  return true; }
                    return false;

                case "OperatingHoursByReference":
                    if (OperatingHoursByReference.  TryParseXML(XML, out var byReference,    out ErrorResponse)) { OperatingHours = byReference;    return true; }
                    return false;

                case "UndefinedOperatingHours":
                    if (UndefinedOperatingHours.    TryParseXML(XML, out var undefinedHours, out ErrorResponse)) { OperatingHours = undefinedHours; return true; }
                    return false;

                case "UnknownOperatingHours":
                    if (UnknownOperatingHours.      TryParseXML(XML, out var unknownHours,   out ErrorResponse)) { OperatingHours = unknownHours;   return true; }
                    return false;

                default:
                    ErrorResponse = $"Unknown operating hours type '{XML.XSIType()}'!";
                    return false;

            }

        }

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public abstract XElement ToXML(XName? XMLName = null);

        #endregion

    }

}
