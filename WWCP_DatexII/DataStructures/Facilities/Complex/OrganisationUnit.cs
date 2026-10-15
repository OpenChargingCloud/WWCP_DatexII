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

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// A unit within the organisation, which has a separate location, operating hours, address and/or contacts.
    /// </summary>
    [XmlType("OrganisationUnit", Namespace = "http://datex2.eu/schema/3/afirFacilities")]
    public class OrganisationUnit(MultilingualString?               Name                        = null,
                                  IEnumerable<MultilingualString>?  Function                    = null,
                                  ALocationReference?               LocationReference           = null,
                                  IEnumerable<ContactInformation>?  ContactInformation          = null,
                                  AOperatingHours?                  OperatingHours              = null,
                                  XElement?                         OrganisationUnitExtension   = null)
    {

        #region Properties

        /// <summary>
        /// A name for this organisation unit.
        /// </summary>
        [XmlElement("name",                        Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?              Name                         { get; } = Name;

        /// <summary>
        /// Functions this unit is responsible for or a specific type, e.g. headquarter or sales.
        /// </summary>
        [XmlElement("function",                    Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<MultilingualString>  Function                     { get; } = Function?.          Distinct() ?? [];

        /// <summary>
        /// Location reference for this organisation unit.
        /// </summary>
        [XmlElement("locationReference",           Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public ALocationReference?              LocationReference            { get; } = LocationReference;

        /// <summary>
        /// Contact information for this organisation unit.
        /// </summary>
        [XmlElement("contactInformation",          Namespace = "http://datex2.eu/schema/3/afirFacilities")]
        public IEnumerable<ContactInformation>  ContactInformation           { get; } = ContactInformation?.Distinct() ?? [];

        /// <summary>
        /// Operating hours of this organisation unit.
        /// </summary>
        [XmlElement("operatingHours",              Namespace = "http://datex2.eu/schema/3/afirFacilities")]
        public AOperatingHours?                 OperatingHours               { get; } = OperatingHours;

        /// <summary>
        /// Optional extension element for additional organisation unit information.
        /// </summary>
        [XmlElement("_organisationUnitExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                        OrganisationUnitExtension    { get; } = OrganisationUnitExtension;

        #endregion


        #region TryParseXML(XML, out OrganisationUnit, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an organisation unit.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="OrganisationUnit">The parsed organisation unit.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                    XML,
                                          [NotNullWhen(true)]  out OrganisationUnit?  OrganisationUnit,
                                          [NotNullWhen(false)] out String?            ErrorResponse)
        {

            OrganisationUnit = null;

            if (!XML.TryParseOptionalXML (DatexIINS.Facilities + "name",                "name",                 MultilingualString.          TryParseXML, out MultilingualString?               name,               out ErrorResponse) ||
                !XML.TryParseOptionalXMLs(DatexIINS.Facilities + "function",            "function",             MultilingualString.          TryParseXML, out IEnumerable<MultilingualString>   function,           out ErrorResponse) ||
                !XML.TryParseOptionalXML (DatexIINS.Facilities + "locationReference",   "location reference",   ALocationReference.          TryParseXML, out ALocationReference?               locationReference,  out ErrorResponse) ||
                !XML.TryParseOptionalXMLs(DatexIINS.Facilities + "contactInformation",  "contact information",  Facilities.ContactInformation.TryParseXML, out IEnumerable<ContactInformation> contactInformation, out ErrorResponse) ||
                !XML.TryParseOptionalXML (DatexIINS.Facilities + "operatingHours",      "operating hours",      AOperatingHours.             TryParseXML, out AOperatingHours?                  operatingHours,     out ErrorResponse))
            {
                return false;
            }

            OrganisationUnit = new OrganisationUnit(
                                   name,
                                   function,
                                   locationReference,
                                   contactInformation,
                                   operatingHours,
                                   XML.Element(DatexIINS.Facilities + "_organisationUnitExtension")
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

            => new (XMLName ?? DatexIINS.Facilities + "organisationUnit",

                   Name is not null
                       ? Name.ToXML(DatexIINS.Facilities + "name")
                       : null,

                   Function.Select(function => function.ToXML(DatexIINS.Facilities + "function")),

                   LocationReference is not null
                       ? LocationReference.ToXML(DatexIINS.Facilities + "locationReference")
                       : null,

                   ContactInformation.Select(contactInformation => contactInformation.ToXML(DatexIINS.Facilities + "contactInformation")),

                   OperatingHours is not null
                       ? OperatingHours.ToXML(DatexIINS.Facilities + "operatingHours")
                       : null,

                   OrganisationUnitExtension is not null
                       ? OrganisationUnitExtension.ToExtensionXML(DatexIINS.Facilities + "_organisationUnitExtension")
                       : null

               );

        #endregion

    }

}
