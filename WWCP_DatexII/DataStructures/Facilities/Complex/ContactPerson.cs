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

using cloud.charging.open.protocols.DatexII.v3.Common;
using System.Xml.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// A specific contact person.
    /// </summary>
    [XmlType("ContactPerson", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class ContactPerson(String                   Name,

                               String?                  FirstName                = null,
                               MultilingualString?      Title                    = null,
                               MultilingualString?      Position                 = null,

                               IEnumerable<Languages>?  Languages                = null,
                               String?                  TelephoneNumber          = null,
                               String?                  FaxNumber                = null,
                               String?                  EMail                    = null,
                               XElement?                ContactPersonExtension   = null)

        : ContactInformation(Languages,
                             TelephoneNumber,
                             FaxNumber,
                             EMail)

    {

        #region Properties

        /// <summary>
        /// The (last) name of the contact person.
        /// </summary>
        [XmlElement("name",                     Namespace = "http://datex2.eu/schema/3/common")]
        public String               Name                      { get; } = Name;

        /// <summary>
        /// The first name of the contact person.
        /// </summary>
        [XmlElement("firstName",                Namespace = "http://datex2.eu/schema/3/common")]
        public String?              FirstName                 { get; } = FirstName;

        /// <summary>
        /// Title information of the contact person.
        /// </summary>
        [XmlElement("title",                    Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?  Title                     { get; } = Title;

        /// <summary>
        /// The position of the contact person.
        /// </summary>
        [XmlElement("position",                 Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?  Position                  { get; } = Position;

        /// <summary>
        /// Optional extension element for additional contact person information.
        /// </summary>
        [XmlElement("_contactPersonExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            ContactPersonExtension    { get; } = ContactPersonExtension;

        #endregion

    }

}
