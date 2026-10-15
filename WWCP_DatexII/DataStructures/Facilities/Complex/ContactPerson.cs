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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// A specific contact person.
    /// </summary>
    [XmlType("ContactPerson", Namespace = "http://datex2.eu/schema/3/afirFacilities")]
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


        #region TryParseXML(XML, out ContactPerson, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a contact person.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ContactPerson">The parsed contact person.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                 XML,
                                          [NotNullWhen(true)]  out ContactPerson?  ContactPerson,
                                          [NotNullWhen(false)] out String?         ErrorResponse)
        {

            ContactPerson = null;

            var name = XML.OptionalText(DatexIINS.Facilities + "name");

            if (name is null)
            {
                ErrorResponse = "The mandatory name of the contact person is missing!";
                return false;
            }

            if (!XML.TryParseOptionalValues(DatexIINS.Facilities + "language",  "language",  DatexIIXMLExtensions.TryParseLanguage,  out IEnumerable<Languages>  languages, out ErrorResponse) ||
                !XML.TryParseOptionalXML   (DatexIINS.Facilities + "title",     "title",     MultilingualString.TryParseXML,         out MultilingualString?     title,     out ErrorResponse) ||
                !XML.TryParseOptionalXML   (DatexIINS.Facilities + "position",  "position",  MultilingualString.TryParseXML,         out MultilingualString?     position,  out ErrorResponse))
            {
                return false;
            }

            ContactPerson = new ContactPerson(
                                name,
                                XML.OptionalText(DatexIINS.Facilities + "firstName"),
                                title,
                                position,
                                languages,
                                XML.OptionalText(DatexIINS.Facilities + "telephoneNumber"),
                                XML.OptionalText(DatexIINS.Facilities + "faxNumber"),
                                XML.OptionalText(DatexIINS.Facilities + "eMail"),
                                XML.Element(DatexIINS.Facilities + "_contactPersonExtension")
                            );

            return true;

        }

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public override XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.Facilities + "contactInformation",

                   // A contact information of the derived type.
                   new XAttribute(DatexIINS.XSI + "type", "afac:ContactPerson"),

                   base.ToXML().Elements(),

                   new XElement(DatexIINS.Facilities + "name",             Name),

                   FirstName is not null && FirstName.Length > 0
                       ? new XElement(DatexIINS.Facilities + "firstName",  FirstName)
                       : null,

                   Title is not null
                       ? Title.ToXML(DatexIINS.Facilities + "title")
                       : null,

                   Position is not null
                       ? Position.ToXML(DatexIINS.Facilities + "position")
                       : null,

                   ContactPersonExtension is not null
                       ? ContactPersonExtension.ToExtensionXML(DatexIINS.Facilities + "_contactPersonExtension")
                       : null

               );

        #endregion

    }

}
