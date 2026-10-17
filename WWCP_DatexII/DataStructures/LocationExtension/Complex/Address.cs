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

using System.Xml.Serialization;
using System.Xml.Linq;
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationExtension
{

    /// <summary>
    /// A street oriented addressing structure supporting delivery.
    /// </summary>
    [XmlType("Address", Namespace = "http://datex2.eu/schema/3/locationExtension")]
    public class Address(String?                    Postcode           = null,
                         MultilingualString?        City               = null,
                         Country?                   CountryCode        = null,
                         IEnumerable<AddressLine>?  AddressLines       = null,
                         XElement?                  AddressExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Postcode or postal code for the address.
        /// </summary>
        [XmlElement("postcode",           Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public String?                   Postcode            { get; } = Postcode;

        /// <summary>
        /// Postal city name of the address.
        /// </summary>
        [XmlElement("city",               Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public MultilingualString?       City                { get; } = City;

        /// <summary>
        /// EN ISO 3166-1 two-character country code.
        /// </summary>
        [XmlElement("countryCode",        Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public Country?                  CountryCode         { get; } = CountryCode;

        /// <summary>
        /// One or more address lines.
        /// </summary>
        [XmlElement("addressLine",        Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public IEnumerable<AddressLine>  AddressLines        { get; } = AddressLines?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional address information.
        /// </summary>
        [XmlElement("_addressExtension",  Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public XElement?                 AddressExtension    { get; } = AddressExtension;

        #endregion


        #region TryParseXML(XML, out Address, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an address.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Address">The parsed address.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                           XML,
                                          [NotNullWhen(true)]  out Address?  Address,
                                          [NotNullWhen(false)] out String?   ErrorResponse)
        {

            Address = null;

            if (!XML.TryParseOptionalXML(DatexIINS.LocationExtension + "city",
                                         "city",
                                         MultilingualString.TryParseXML,
                                         out MultilingualString? city,
                                         out ErrorResponse))
            {
                return false;
            }

            Country? country = null;

            if (XML.OptionalText(DatexIINS.LocationExtension + "countryCode") is String countryCode &&
                !Country.TryParseAlpha2Code(countryCode.ToUpperInvariant(), out country))
            {
                ErrorResponse = $"Invalid country code '{countryCode}'!";
                return false;
            }

            if (!XML.TryParseOptionalXMLs(DatexIINS.LocationExtension + "addressLine",
                                          "address line",
                                          AddressLine.TryParseXML,
                                          out IEnumerable<AddressLine> addressLines,
                                          out ErrorResponse))
            {
                return false;
            }

            Address = new Address(
                          XML.OptionalText(DatexIINS.LocationExtension + "postcode"),
                          city,
                          country,
                          addressLines,
                          XML.Element(DatexIINS.LocationExtension + "_addressExtension")
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

            => new (XMLName ?? DatexIINS.LocationExtension + "address",

                   Postcode is not null && Postcode.Length > 0
                       ? new XElement(DatexIINS.LocationExtension + "postcode",      Postcode)
                       : null,

                   City is not null
                       ? City.ToXML(DatexIINS.LocationExtension + "city")
                       : null,

                   CountryCode is not null
                       ? new XElement(DatexIINS.LocationExtension + "countryCode",   CountryCode.Alpha2Code)
                       : null,

                   AddressLines.Select(addressLine => addressLine.ToXML(DatexIINS.LocationExtension + "addressLine")),

                   AddressExtension is not null
                       ? AddressExtension.ToExtensionXML(DatexIINS.LocationExtension + "_addressExtension")
                       : null

               );

        #endregion

    }

}
