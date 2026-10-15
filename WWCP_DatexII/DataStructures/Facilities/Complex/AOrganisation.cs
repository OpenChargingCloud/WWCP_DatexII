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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Provides information about an organisation including its units and contact information.
    /// </summary>
    [XmlType("Organisation", Namespace = "http://datex2.eu/schema/3/afirFacilities")]
    public abstract class AOrganisation(OverallPeriod?  GeneralTimeValidity     = null,
                                        XElement?       OrganisationExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Specification of a time validity, for which this organisation information is valid in general.
        /// This is not a specification of the operating hours as such.
        /// </summary>
        [XmlElement("generalTimeValidity", Namespace = "http://datex2.eu/schema/3/common")]
        public OverallPeriod?  GeneralTimeValidity      { get; } = GeneralTimeValidity;

        /// <summary>
        /// Optional extension element for additional organisation information.
        /// </summary>
        [XmlElement("_organisationExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?       OrganisationExtension    { get; } = OrganisationExtension;

        #endregion


        #region TryParseXML(XML, out Organisation, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an organisation, by its xsi:type.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Organisation">The parsed organisation.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                 XML,
                                          [NotNullWhen(true)]  out AOrganisation?  Organisation,
                                          [NotNullWhen(false)] out String?         ErrorResponse)
        {

            Organisation   = null;
            ErrorResponse  = null;

            switch (XML.XSIType())
            {

                case "AnOrganisation":
                    if (AnOrganisation.           TryParseXML(XML, out var anOrganisation,  out ErrorResponse)) { Organisation = anOrganisation;  return true; }
                    return false;

                case "ReferenceableOrganisation":
                    if (ReferenceableOrganisation.TryParseXML(XML, out var referenceable,   out ErrorResponse)) { Organisation = referenceable;   return true; }
                    return false;

                case "OrganisationByReference":
                    if (OrganisationByReference.  TryParseXML(XML, out var byReference,   out ErrorResponse)) { Organisation = byReference;   return true; }
                    return false;

                case "UnknownOrganisation":
                    Organisation = new UnknownOrganisation(
                                       XML.Element(DatexIINS.Facilities + "_unknownOrganisationExtension"),
                                       null,
                                       XML.Element(DatexIINS.Facilities + "_organisationExtension")
                                   );
                    return true;

                case "UndefinedOrganisation":
                    Organisation = new UndefinedOrganisation(
                                       XML.Element(DatexIINS.Facilities + "_undefinedOrganisationExtension"),
                                       null,
                                       XML.Element(DatexIINS.Facilities + "_organisationExtension")
                                   );
                    return true;

                default:
                    ErrorResponse = $"Unknown organisation type '{XML.XSIType()}'!";
                    return false;

            }

        }

        #endregion


        #region (protected) ToXMLElements()

        /// <summary>
        /// The part every organisation variant shares, in schema order.
        /// </summary>
        protected virtual IEnumerable<Object?> ToXMLElements()
        {

            if (GeneralTimeValidity is not null)
                yield return GeneralTimeValidity.ToXML(DatexIINS.Facilities + "generalTimeValidity");

            if (OrganisationExtension is not null)
                yield return OrganisationExtension.ToExtensionXML(DatexIINS.Facilities + "_organisationExtension");

        }

        #endregion

        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public abstract XElement ToXML(XName? XMLName = null);

        #endregion

    }

}
