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

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// The ISO 3166-2 representation for the named area.
    /// </summary>
    [XmlType("IsoNamedArea", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class ISONamedArea(MultilingualString  AreaName,
                              SubdivisionType     SubdivisionType,
                              SubdivisionCode     SubdivisionCode,
                              NamedAreaType?      NamedAreaType           = null,
                              Country?            Country                 = null,
                              XElement?           NamedAreaExtension      = null,
                              XElement?           ISONamedAreaExtension   = null,

                              XElement?           ANamedAreaExtension     = null)

        : NamedArea(AreaName,
                    NamedAreaType,
                    Country,
                    NamedAreaExtension,
                    ANamedAreaExtension)

    {

        #region Properties

        /// <summary>
        /// The ISO 3166-2 subdivision type for the named area.
        /// </summary>
        [XmlElement("subdivisionType",         Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public SubdivisionType  SubdivisionType          { get; } = SubdivisionType;

        /// <summary>
        /// The second part of an ISO 3166-2 subdivision code for the named area.
        /// This may be used along with the country attribute from the parent class to form a full ISO 3166-2 subdivision code.
        /// </summary>
        [XmlElement("subdivisionCode",         Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public SubdivisionCode  SubdivisionCode          { get; } = SubdivisionCode;

        /// <summary>
        /// Optional extension element for additional ISO named area information.
        /// </summary>
        [XmlElement("_isoNamedAreaExtension",  Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public XElement?        ISONamedAreaExtension    { get; } = ISONamedAreaExtension;

        #endregion


        #region TryParseXML(XML, out ISONamedArea, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an ISO named area.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ISONamedArea">The parsed ISO named area.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                XML,
                                          [NotNullWhen(true)]  out ISONamedArea?  ISONamedArea,
                                          [NotNullWhen(false)] out String?        ErrorResponse)
        {

            ISONamedArea = null;

            if (!NamedArea.TryParseXML(XML, out var namedArea, out ErrorResponse))
                return false;

            if (!XML.TryParseMandatoryValue(DatexIINS.LocationReferencing + "subdivisionType",  "subdivision type",  LocationReferencing.SubdivisionType.TryParse,  out SubdivisionType subdivisionType,  out ErrorResponse))
                return false;

            var subdivisionCode = XML.OptionalText(DatexIINS.LocationReferencing + "subdivisionCode");

            if (subdivisionCode is null || subdivisionCode.Length > 3)
            {
                ErrorResponse = subdivisionCode is null
                                    ? "The subdivision code is missing!"
                                    : $"Invalid subdivision code '{subdivisionCode}'!";
                return false;
            }

            ISONamedArea = new ISONamedArea(
                               namedArea.AreaName,
                               subdivisionType,
                               new SubdivisionCode(subdivisionCode),
                               namedArea.NamedAreaType,
                               namedArea.Country,
                               namedArea.NamedAreaExtension,
                               XML.Element(DatexIINS.LocationReferencing + "_isoNamedAreaExtension"),
                               namedArea.ANamedAreaExtension
                           );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public override XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.Common + "namedArea",

                    // The schema declares namedArea with the abstract type com:NamedArea.
                    new XAttribute(DatexIINS.XSI + "type", "loc:IsoNamedArea"),

                    ToXMLElements(),

                    new XElement(DatexIINS.LocationReferencing + "subdivisionType",   SubdivisionType.ToString()),
                    new XElement(DatexIINS.LocationReferencing + "subdivisionCode",   SubdivisionCode.ToString()),

                    ISONamedAreaExtension.ToExtensionXML(DatexIINS.LocationReferencing + "_isoNamedAreaExtension"));

        #endregion

    }

}
