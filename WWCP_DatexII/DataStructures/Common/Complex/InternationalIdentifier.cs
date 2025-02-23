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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// An identifier/name whose range is specific to the particular country.
    /// </summary>
    [XmlType("InternationalIdentifier", Namespace = "http://datex2.eu/schema/3/common")]
    public class InternationalIdentifier(Country  Country,
                                         String   NationalIdentifier)
    {

        /// <summary>
        /// EN ISO 3166-1 two-character country code.
        /// </summary>
        [XmlElement("country",             Namespace = "http://datex2.eu/schema/3/common")]
        public Country    Country                             { get; set; } = Country;

        /// <summary>
        /// Identifier or name unique within the specified country.
        /// </summary>
        [XmlElement("nationalIdentifier",  Namespace = "http://datex2.eu/schema/3/common")]
        public String     NationalIdentifier                  { get; set; } = NationalIdentifier.Trim();

        /// <summary>
        /// Optional extension element for additional international identifier information.
        /// </summary>
        [XmlElement("_internationalIdentifierExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  InternationalIdentifierExtension    { get; set; }


        public XElement ToXML()
        {

            var xml = new XElement(XML_IO.nsCom + "publicationCreator",
                          new XElement(XML_IO.nsCom + "country",             Country.Alpha2Code.ToLower()),
                          new XElement(XML_IO.nsCom + "nationalIdentifier",  NationalIdentifier)
                      );

            return xml;

        }


    }

}
