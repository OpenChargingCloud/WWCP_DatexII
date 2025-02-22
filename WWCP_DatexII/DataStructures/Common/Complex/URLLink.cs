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

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Details of a Uniform Resource Locator (URL) address pointing to a resource available on the Internet
    /// from where further relevant information may be obtained.
    /// </summary>
    [XmlType("UrlLink", Namespace = "http://datex2.eu/schema/3/common")]
    public class URLLink(URL                  URLLinkAddress,
                         MultilingualString?  URLLinkDescription   = null,
                         URLLinkTypes?        URLLinkType          = null)
    {

        /// <summary>
        /// A Uniform Resource Locator (URL) address pointing to a resource available on the Internet.
        /// </summary>
        [XmlElement("urlLinkAddress",      Namespace = "http://datex2.eu/schema/3/common")]
        public URL                  URLLinkAddress        { get; set; } = URLLinkAddress;

        /// <summary>
        /// Description of the relevant information available on the Internet from the URL link.
        /// </summary>
        [XmlElement("urlLinkDescription",  Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?  URLLinkDescription    { get; set; } = URLLinkDescription;

        /// <summary>
        /// Details of the type of relevant information available on the Internet from the URL link.
        /// </summary>
        [XmlElement("urlLinkType",         Namespace = "http://datex2.eu/schema/3/common")]
        public URLLinkTypes?        URLLinkType           { get; set; } = URLLinkType;

        /// <summary>
        /// Optional extension element for additional URL link information.
        /// </summary>
        [XmlElement("_urlLinkExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            URLLinkExtension      { get; set; }

    }

}
