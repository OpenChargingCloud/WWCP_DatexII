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

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// A class defining information concerning one line of a postal address.
    /// </summary>
    [XmlType("AddressLine", Namespace = "http://datex2.eu/schema/3/locationExtension")]
    public class AddressLine(UInt32              Order,
                             MultilingualString  Text,
                             AddressLineTypes    Type)
    {

        /// <summary>
        /// The sequence order that the address line element should be displayed in.
        /// </summary>
        [XmlAttribute("order")]
        public UInt32              Order    { get; set; } = Order;

        /// <summary>
        /// Free-text description for the address line element.
        /// </summary>
        [XmlElement("text", Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString  Text     { get; set; } = Text;

        /// <summary>
        /// The type for the address line element.
        /// </summary>
        [XmlElement("type", Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public AddressLineTypes    Type     { get; set; } = Type;

        ///// <summary>
        ///// Optional extension element for additional address line information.
        ///// </summary>
        //[XmlElement("_addressLineExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? AddressLineExtension { get; set; }

    }

}
