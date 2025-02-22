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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Contact information.
    /// </summary>
    [XmlType("ContactInformation", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class ContactInformation(IEnumerable<Languages>?  Languages         = null,
                                    String?                  TelephoneNumber   = null,
                                    String?                  FaxNumber         = null,
                                    String?                  EMail             = null)
    {

        /// <summary>
        /// Language(s) that are supported via telephone or mail.
        /// </summary>
        [XmlElement("language",                      Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Languages>  Languages                      { get; set; } = Languages?.Distinct() ?? [];

        /// <summary>
        /// Telephone number, which can be a general number or the number of a specific contact person.
        /// </summary>
        [XmlElement("telephoneNumber",               Namespace = "http://datex2.eu/schema/3/common")]
        public String?                 TelephoneNumber                { get; set; } = TelephoneNumber;

        /// <summary>
        /// Fax number, which can be a general number or the number of a specific contact person.
        /// </summary>
        [XmlElement("faxNumber",                     Namespace = "http://datex2.eu/schema/3/common")]
        public String?                 FaxNumber                      { get; set; } = FaxNumber;

        /// <summary>
        /// E-Mail address, which can be a general mail address or the mail address of a specific contact person.
        /// </summary>
        [XmlElement("eMail",                         Namespace = "http://datex2.eu/schema/3/common")]
        public String?                 EMail                          { get; set; } = EMail;

        /// <summary>
        /// Optional extension element for additional contact information.
        /// </summary>
        [XmlElement("_contactInformationExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?               ContactInformationExtension    { get; set; }

    }

}
