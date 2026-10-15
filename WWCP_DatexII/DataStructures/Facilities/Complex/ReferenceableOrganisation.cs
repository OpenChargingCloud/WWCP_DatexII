/*
 * Copyright (c) 2014-2026 GraphDefined GmbH <achim.friedland@graphdefined.com>
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
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// An organisation with an identification and a version, so others can refer to it with an OrganisationByReference
    /// (DATEX II 3.7: afac:ReferenceableOrganisation).
    /// </summary>
    public class ReferenceableOrganisation(String                            Id,
                                           String                            Version,
                                           MultilingualString                Name,
                                           DateTimeOffset?                   LastUpdated                          = null,
                                           MultilingualString?               LegalName                            = null,
                                           MultilingualString?               Description                          = null,
                                           URL?                              LinkToGeneralInformation             = null,
                                           URL?                              LinkToLogo                           = null,
                                           URL?                              LinkToWebform                        = null,
                                           Boolean?                          Available24hours                     = null,
                                           IEnumerable<MultilingualString>?  Responsibility                       = null,
                                           Boolean?                          PublishingAgreement                  = null,
                                           OrganisationType?                 Type                                 = null,
                                           String?                           NationalOrganisationNumber           = null,
                                           String?                           NationalRegister                     = null,
                                           String?                           VATIdentificationNumber              = null,
                                           IEnumerable<OrganisationUnit>?    OrganisationUnits                    = null,
                                           IEnumerable<ExternalIdentifier>?  ExternalIdentifier                   = null,
                                           IEnumerable<AOrganisation>?       SubOrganisations                     = null,
                                           XElement?                         ReferenceableOrganisationExtension   = null,
                                           XElement?                         AnOrganisationExtension              = null,
                                           OverallPeriod?                    GeneralTimeValidity                  = null,
                                           XElement?                         OrganisationExtension                = null)

        : AnOrganisation(Name,
                         LastUpdated,
                         LegalName,
                         Description,
                         LinkToGeneralInformation,
                         LinkToLogo,
                         LinkToWebform,
                         Available24hours,
                         Responsibility,
                         PublishingAgreement,
                         Type,
                         NationalOrganisationNumber,
                         NationalRegister,
                         VATIdentificationNumber,
                         OrganisationUnits,
                         ExternalIdentifier,
                         SubOrganisations,
                         AnOrganisationExtension,
                         GeneralTimeValidity,
                         OrganisationExtension)

    {

        #region Properties

        /// <summary>
        /// The identification of the organisation.
        /// </summary>
        public String     Id                                    { get; } = Id;

        /// <summary>
        /// The version of the organisation.
        /// </summary>
        public String     Version                               { get; } = Version;

        /// <summary>
        /// An optional extension.
        /// </summary>
        public XElement?  ReferenceableOrganisationExtension    { get; } = ReferenceableOrganisationExtension;

        #endregion


        #region TryParseXML(XML, out ReferenceableOrganisation, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a referenceable organisation.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ReferenceableOrganisation">The parsed organisation.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                             XML,
                                          [NotNullWhen(true)]  out ReferenceableOrganisation?  ReferenceableOrganisation,
                                          [NotNullWhen(false)] out String?                     ErrorResponse)
        {

            ReferenceableOrganisation = null;

            var id       = XML.Attribute("id")?.     Value.Trim();
            var version  = XML.Attribute("version")?.Value.Trim();

            if (id is null || id.Length == 0 || version is null)
            {
                ErrorResponse = "The mandatory 'id' or 'version' attribute of the organisation is missing!";
                return false;
            }

            if (!TryParseParts(XML, out var parts, out ErrorResponse))
                return false;

            ReferenceableOrganisation = new ReferenceableOrganisation(
                                            id,
                                            version,
                                            parts.Name,
                                            parts.LastUpdated,
                                            parts.LegalName,
                                            parts.Description,
                                            parts.LinkToGeneralInformation,
                                            parts.LinkToLogo,
                                            parts.LinkToWebform,
                                            parts.Available24hours,
                                            parts.Responsibility,
                                            parts.PublishingAgreement,
                                            parts.Type,
                                            parts.NationalOrganisationNumber,
                                            parts.NationalRegister,
                                            parts.VATIdentificationNumber,
                                            parts.OrganisationUnits,
                                            parts.ExternalIdentifier,
                                            parts.SubOrganisations,
                                            XML.Element(DatexIINS.Facilities + "_referenceableOrganisationExtension"),
                                            parts.AnOrganisationExtension,
                                            parts.GeneralTimeValidity,
                                            parts.OrganisationExtension
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

            => new (XMLName ?? DatexIINS.Facilities + "organisation",
                    new XAttribute(DatexIINS.XSI + "type", "afac:ReferenceableOrganisation"),
                    new XAttribute("id",      Id),
                    new XAttribute("version", Version),
                    ToXMLElements(),
                    ReferenceableOrganisationExtension is not null
                        ? ReferenceableOrganisationExtension.ToExtensionXML(DatexIINS.Facilities + "_referenceableOrganisationExtension")
                        : null);

        #endregion

    }

}
