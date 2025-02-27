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

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Represents a multilingual string value with a language attribute.
    /// </summary>
    [XmlType("MultilingualStringValue", Namespace = "http://datex2.eu/schema/3/common")]
    public class MultilingualStringValue(Languages  Language,
                                         String     Text)
    {

        #region Properties

        /// <summary>
        /// The language of the text.
        /// </summary>
        [XmlAttribute("lang")]
        public Languages  Language    { get; } = Language;

        /// <summary>
        /// The text.
        /// </summary>
        [XmlText]
        public String     Text        { get; } = Text;

        #endregion


        #region TryParseXML(XML, out MultilingualStringValue, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a MultilingualStringValue.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="MultilingualStringValue">The parsed MultilingualStringValue.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                           XML,
                                          [NotNullWhen(true)]  out MultilingualStringValue?  MultilingualStringValue,
                                          [NotNullWhen(false)] out String?                   ErrorResponse)
        {

            MultilingualStringValue  = null;
            ErrorResponse            = null;


            #region TryParse Language    [mandatory]

            if (!XML.TryParseMandatoryAttribute("lang",
                                                "language",
                                                LanguagesExtensions.TryParse,
                                                out Languages language,
                                                out ErrorResponse))
            {
                return false;
            }

            #endregion


            MultilingualStringValue = new MultilingualStringValue(
                                          language,
                                          XML.Value
                                      );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null)

        public XElement ToXML(XName? XMLName = null)
        {

            var xml = new XElement(XMLName ?? "value",

                          new XAttribute("lang", Language.ToString()),

                          Text

                      );

            return xml;

        }

        #endregion

    }

}
