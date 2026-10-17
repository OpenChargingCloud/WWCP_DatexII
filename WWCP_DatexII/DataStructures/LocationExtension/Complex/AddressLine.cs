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

namespace cloud.charging.open.protocols.DatexII.v3.LocationExtension
{

    /// <summary>
    /// A class defining information concerning one line of a postal address.
    /// </summary>
    [XmlType("AddressLine", Namespace = "http://datex2.eu/schema/3/locationExtension")]
    public class AddressLine(UInt32              Order,
                             MultilingualString  Text,
                             AddressLineType     Type,
                             XElement?           AddressLineExtension   = null)
    {

        #region Properties

        /// <summary>
        /// The sequence order that the address line element should be displayed in.
        /// </summary>
        [XmlAttribute("order")]
        public UInt32              Order                   { get; } = Order;

        /// <summary>
        /// Free-text description for the address line element.
        /// </summary>
        [XmlElement("text",                   Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public MultilingualString  Text                    { get; } = Text;

        /// <summary>
        /// The type for the address line element.
        /// </summary>
        [XmlElement("type",                   Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public AddressLineType     Type                    { get; } = Type;

        /// <summary>
        /// Optional extension element for additional address line information.
        /// </summary>
        [XmlElement("_addressLineExtension",  Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public XElement?           AddressLineExtension    { get; } = AddressLineExtension;

        #endregion


        #region TryParseXML(XML, out AddressLine, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an address line.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="AddressLine">The parsed address line.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                               XML,
                                          [NotNullWhen(true)]  out AddressLine?  AddressLine,
                                          [NotNullWhen(false)] out String?       ErrorResponse)
        {

            AddressLine = null;

            if (!UInt32.TryParse(XML.Attribute("order")?.Value, out var order))
            {
                ErrorResponse = $"Invalid or missing address line order '{XML.Attribute("order")?.Value}'!";
                return false;
            }

            if (!XML.TryParseMandatoryValue(DatexIINS.LocationExtension + "type",
                                            "address line type",
                                            AddressLineType.TryParse,
                                            out AddressLineType type,
                                            out ErrorResponse))
            {
                return false;
            }

            if (!XML.TryParseMandatoryXML(DatexIINS.LocationExtension + "text",
                                          "address line text",
                                          MultilingualString.TryParseXML,
                                          out MultilingualString? text,
                                          out ErrorResponse))
            {
                return false;
            }

            AddressLine = new AddressLine(
                              order,
                              text,
                              type,
                              XML.Element(DatexIINS.LocationExtension + "_addressLineExtension")
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

            => new (XMLName ?? DatexIINS.LocationExtension + "addressLine",

                   new XAttribute("order",                   Order),

                   new XElement(DatexIINS.LocationExtension + "type",           Type.ToString()),

                   Text.ToXML(DatexIINS.LocationExtension + "text"),

                   AddressLineExtension is not null
                       ? AddressLineExtension.ToExtensionXML(DatexIINS.LocationExtension + "_addressLineExtension")
                       : null

               );

        #endregion

    }

}
