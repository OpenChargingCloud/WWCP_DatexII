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
    /// An identifier/name whose range is specific to the particular country.
    /// </summary>
    [XmlType("InternationalIdentifier", Namespace = "http://datex2.eu/schema/3/common")]
    public class InternationalIdentifier(Country    Country,
                                         String     NationalIdentifier,
                                         XElement?  InternationalIdentifierExtension   = null)
    {

        #region Properties

        /// <summary>
        /// EN ISO 3166-1 two-character country code.
        /// </summary>
        [XmlElement("country",                            Namespace = "http://datex2.eu/schema/3/common")]
        public Country    Country                             { get; } = Country;

        /// <summary>
        /// Identifier or name unique within the specified country.
        /// </summary>
        [XmlElement("nationalIdentifier",                 Namespace = "http://datex2.eu/schema/3/common")]
        public String     NationalIdentifier                  { get; } = NationalIdentifier.Trim();

        /// <summary>
        /// Optional extension element for additional international identifier information.
        /// </summary>
        [XmlElement("_internationalIdentifierExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  InternationalIdentifierExtension    { get; } = InternationalIdentifierExtension;

        #endregion


        #region TryParseXML(XML, out InternationalIdentifier, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an InternationalIdentifier.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="InternationalIdentifier">The parsed InternationalIdentifier.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                           XML,
                                          [NotNullWhen(true)]  out InternationalIdentifier?  InternationalIdentifier,
                                          [NotNullWhen(false)] out String?                   ErrorResponse)
        {

            InternationalIdentifier  = null;
            ErrorResponse            = null;

            #region TryParse Country               [mandatory]

            if (!XML.TryParseMandatory<Country>(DatexIINS.Common + "country",
                                                "country",
                                                Country.TryParse,
                                                out var country,
                                                out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse NationalIdentifier    [mandatory]  // Maybe some RegEx?

            if (!XML.TryParseMandatoryText(DatexIINS.Common + "nationalIdentifier",
                                           "national identifier",
                                           out var nationalIdentifier,
                                           out ErrorResponse))
            {
                return false;
            }

            #endregion


            InternationalIdentifier = new InternationalIdentifier(
                                          country,
                                          nationalIdentifier,
                                          XML.Element(DatexIINS.Common + "_internationalIdentifierExtension")
                                      );

            return true;

        }

        #endregion

        #region ToXML()

        public XElement ToXML()
        {

            var xml = new XElement(DatexIINS.Common + "publicationCreator",

                                new XElement(DatexIINS.Common + "country",                             Country.Alpha2Code.ToLower()),
                                new XElement(DatexIINS.Common + "nationalIdentifier",                  NationalIdentifier),

                          InternationalIdentifierExtension is not null
                              ? new XElement(DatexIINS.Common + "_internationalIdentifierExtension",   InternationalIdentifierExtension)
                              : null

                      );

            return xml;

        }

        #endregion


    }

}
