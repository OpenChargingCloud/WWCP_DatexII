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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Contact information.
    /// </summary>
    [XmlType("ContactInformation", Namespace = "http://datex2.eu/schema/3/afirFacilities")]
    public class ContactInformation(IEnumerable<Languages>?  Languages                     = null,
                                    String?                  TelephoneNumber               = null,
                                    String?                  FaxNumber                     = null,
                                    String?                  EMail                         = null,
                                    XElement?                ContactInformationExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Language(s) that are supported via telephone or mail.
        /// </summary>
        [XmlElement("language",                      Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Languages>  Languages                      { get; } = Languages?.Distinct() ?? [];

        /// <summary>
        /// Telephone number, which can be a general number or the number of a specific contact person.
        /// </summary>
        [XmlElement("telephoneNumber",               Namespace = "http://datex2.eu/schema/3/common")]
        public String?                 TelephoneNumber                { get; } = TelephoneNumber;

        /// <summary>
        /// Fax number, which can be a general number or the number of a specific contact person.
        /// </summary>
        [XmlElement("faxNumber",                     Namespace = "http://datex2.eu/schema/3/common")]
        public String?                 FaxNumber                      { get; } = FaxNumber;

        /// <summary>
        /// E-Mail address, which can be a general mail address or the mail address of a specific contact person.
        /// </summary>
        [XmlElement("eMail",                         Namespace = "http://datex2.eu/schema/3/common")]
        public String?                 EMail                          { get; } = EMail;

        /// <summary>
        /// Optional extension element for additional contact information.
        /// </summary>
        [XmlElement("_contactInformationExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?               ContactInformationExtension    { get; } = ContactInformationExtension;

        #endregion


        #region TryParseXML(XML, out ContactInformation, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of contact information.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ContactInformation">The parsed contact information.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                      XML,
                                          [NotNullWhen(true)]  out ContactInformation?  ContactInformation,
                                          [NotNullWhen(false)] out String?              ErrorResponse)
        {

            ContactInformation = null;

            if (XML.XSIType() == "ContactPerson")
            {
                if (ContactPerson.TryParseXML(XML, out var contactPerson, out ErrorResponse))
                {
                    ContactInformation = contactPerson;
                    return true;
                }
                return false;
            }

            if (!XML.TryParseOptionalValues(DatexIINS.Facilities + "language",
                                            "language",
                                            DatexIIXMLExtensions.TryParseLanguage,
                                            out IEnumerable<Languages> languages,
                                            out ErrorResponse))
            {
                return false;
            }

            ContactInformation = new ContactInformation(
                                     languages,
                                     XML.OptionalText(DatexIINS.Facilities + "telephoneNumber"),
                                     XML.OptionalText(DatexIINS.Facilities + "faxNumber"),
                                     XML.OptionalText(DatexIINS.Facilities + "eMail"),
                                     XML.Element(DatexIINS.Facilities + "_contactInformationExtension")
                                 );

            return true;

        }

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public virtual XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.Facilities + "contactInformation",

                   Languages.Select(language => new XElement(DatexIINS.Facilities + "language",  language.AsText())),

                   TelephoneNumber is not null && TelephoneNumber.Length > 0
                       ? new XElement(DatexIINS.Facilities + "telephoneNumber",                  TelephoneNumber)
                       : null,

                   FaxNumber is not null && FaxNumber.Length > 0
                       ? new XElement(DatexIINS.Facilities + "faxNumber",                        FaxNumber)
                       : null,

                   EMail is not null && EMail.Length > 0
                       ? new XElement(DatexIINS.Facilities + "eMail",                            EMail)
                       : null,

                   ContactInformationExtension is not null
                       ? ContactInformationExtension.ToExtensionXML(DatexIINS.Facilities + "_contactInformationExtension")
                       : null

               );

        #endregion

    }

}
