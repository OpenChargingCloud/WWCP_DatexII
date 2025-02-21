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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// A street oriented addressing structure supporting delivery.
    /// </summary>
    [XmlType("Address", Namespace = "http://datex2.eu/schema/3/locationExtension")]
    public class Address(String?                    Postcode       = null,
                         MultilingualString?        City           = null,
                         Country?                   CountryCode    = null,
                         IEnumerable<AddressLine>?  AddressLines   = null)
    {

        /// <summary>
        /// Postcode or postal code for the address.
        /// </summary>
        [XmlElement("postcode", Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public String?                   Postcode        { get; set; } = Postcode;

        /// <summary>
        /// Postal city name of the address.
        /// </summary>
        [XmlElement("city", Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public MultilingualString?       City            { get; set; } = City;

        /// <summary>
        /// EN ISO 3166-1 two-character country code.
        /// </summary>
        [XmlElement("countryCode", Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public Country?                  CountryCode     { get; set; } = CountryCode;

        /// <summary>
        /// One or more address lines.
        /// </summary>
        [XmlElement("addressLine", Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public IEnumerable<AddressLine>  AddressLines    { get; set; } = AddressLines?.Distinct() ?? [];

        ///// <summary>
        ///// Optional extension element for additional address information.
        ///// </summary>
        //[XmlElement("_addressExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? AddressExtension { get; set; }

    }

}
