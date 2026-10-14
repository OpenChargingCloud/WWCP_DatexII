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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// A component that provides dimension information. Especially for multi-storey buildings, 
    /// the usable area might be larger than the product from its length and width.
    /// </summary>
    [XmlType("Dimension", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class Dimension(Meter?        Length               = null,
                           Meter?        Width                = null,
                           Meter?        Height               = null,
                           SquareMeter?  UsableArea           = null,
                           XElement?     DimensionExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Length.
        /// </summary>
        [XmlElement("length",               Namespace = "http://datex2.eu/schema/3/common")]
        public Meter?        Length                { get; } = Length;

        /// <summary>
        /// Width.
        /// </summary>
        [XmlElement("width",                Namespace = "http://datex2.eu/schema/3/common")]
        public Meter?        Width                 { get; } = Width;

        /// <summary>
        /// Height.
        /// </summary>
        [XmlElement("height",               Namespace = "http://datex2.eu/schema/3/common")]
        public Meter?        Height                { get; } = Height;

        /// <summary>
        /// The area measured in square metres that is available for some specific purpose.
        /// </summary>
        [XmlElement("usableArea",           Namespace = "http://datex2.eu/schema/3/facilities")]
        public SquareMeter?  UsableArea            { get; } = UsableArea;

        /// <summary>
        /// Optional extension element for additional dimension information.
        /// </summary>
        [XmlElement("_dimensionExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?     DimensionExtension    { get; } = DimensionExtension;

        #endregion


        #region TryParseXML(XML, out Dimension, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a dimension.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Dimension">The parsed dimension.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                             XML,
                                          [NotNullWhen(true)]  out Dimension?  Dimension,
                                          [NotNullWhen(false)] out String?     ErrorResponse)
        {

            Dimension = null;

            if (!XML.TryParseOptionalValue(DatexIINS.Facilities + "length",      "length",       DatexIIXMLExtensions.TryParseMeter, out Meter? length,     out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "width",       "width",        DatexIIXMLExtensions.TryParseMeter, out Meter? width,      out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "height",      "height",       DatexIIXMLExtensions.TryParseMeter, out Meter? height,     out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "usableArea",  "usable area",  SquareMeter.TryParse,                out SquareMeter? usableArea, out ErrorResponse))
            {
                return false;
            }

            Dimension = new Dimension(
                            length,
                            width,
                            height,
                            usableArea,
                            XML.Element(DatexIINS.Facilities + "_dimensionExtension")
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

            => new (XMLName ?? DatexIINS.Facilities + "dimension",

                   Length.HasValue
                       ? new XElement(DatexIINS.Facilities + "length",       Length.    Value.m)
                       : null,

                   Width.HasValue
                       ? new XElement(DatexIINS.Facilities + "width",        Width.     Value.m)
                       : null,

                   Height.HasValue
                       ? new XElement(DatexIINS.Facilities + "height",       Height.    Value.m)
                       : null,

                   UsableArea.HasValue
                       ? new XElement(DatexIINS.Facilities + "usableArea",   UsableArea.Value.Value)
                       : null,

                   DimensionExtension is not null
                       ? new XElement(DatexIINS.Facilities + "_dimensionExtension", DimensionExtension)
                       : null

               );

        #endregion

    }

}
