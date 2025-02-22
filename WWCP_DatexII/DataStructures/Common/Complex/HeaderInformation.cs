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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Management information relating to the data contained within a publication.
    /// </summary>
    [XmlType("HeaderInformation", Namespace = "http://datex2.eu/schema/3/common")]
    public class HeaderInformation(InformationStatus                          InformationStatus,
                                   Confidentialities?                         Confidentiality          = null,
                                   IEnumerable<InformationDeliveryServices>?  AllowedDeliveryChannel   = null)
    {

        /// <summary>
        /// The status of the related information (real, test, exercise, etc.).
        /// </summary>
        [XmlElement("informationStatus",       Namespace = "http://datex2.eu/schema/3/common")]
        public InformationStatus                         InformationStatus             { get; set; } = InformationStatus;

        /// <summary>
        /// The extent to which the related information may be circulated, according to the recipient type.
        /// </summary>
        [XmlElement("confidentiality",         Namespace = "http://datex2.eu/schema/3/common")]
        public Confidentialities?                        Confidentiality               { get; set; } = Confidentiality;

        /// <summary>
        /// The allowed delivery channels.
        /// </summary>
        [XmlElement("allowedDeliveryChannel",  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<InformationDeliveryServices>  AllowedDeliveryChannel        { get; set; } = AllowedDeliveryChannel?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional header information.
        /// </summary>
        [XmlElement("_headerInformationExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                 HeaderInformationExtension    { get; set; }

    }

}
