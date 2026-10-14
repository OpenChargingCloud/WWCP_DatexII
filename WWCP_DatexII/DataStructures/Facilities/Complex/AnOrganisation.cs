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

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// An organisation, specified in place (DATEX II 3.7: afac:AnOrganisation), e.g. the operator of a site.
    /// Its identifications, e.g. the operator identification, are external identifiers.
    /// </summary>
    public class AnOrganisation(MultilingualString                Name,
                                DateTimeOffset?                   LastUpdated                  = null,
                                MultilingualString?               LegalName                    = null,
                                MultilingualString?               Description                  = null,
                                URL?                              LinkToGeneralInformation     = null,
                                URL?                              LinkToLogo                   = null,
                                URL?                              LinkToWebform                = null,
                                Boolean?                          Available24hours             = null,
                                IEnumerable<MultilingualString>?  Responsibility               = null,
                                Boolean?                          PublishingAgreement          = null,
                                OrganisationType?                 Type                         = null,
                                String?                           NationalOrganisationNumber   = null,
                                String?                           NationalRegister             = null,
                                String?                           VATIdentificationNumber      = null,
                                IEnumerable<OrganisationUnit>?    OrganisationUnits            = null,
                                IEnumerable<ExternalIdentifier>?  ExternalIdentifier           = null,
                                IEnumerable<AOrganisation>?       SubOrganisations             = null,
                                XElement?                         AnOrganisationExtension      = null,
                                OverallPeriod?                    GeneralTimeValidity          = null,
                                XElement?                         OrganisationExtension        = null)

        : AOrganisation(GeneralTimeValidity,
                        OrganisationExtension)

    {

        #region Properties

        /// <summary>
        /// The name of the organisation.
        /// </summary>
        public MultilingualString               Name                          { get; } = Name;

        /// <summary>
        /// When the information on the organisation was last updated.
        /// </summary>
        public DateTimeOffset?                  LastUpdated                   { get; } = LastUpdated;

        /// <summary>
        /// The legal name of the organisation.
        /// </summary>
        public MultilingualString?              LegalName                     { get; } = LegalName;

        /// <summary>
        /// A description of the organisation.
        /// </summary>
        public MultilingualString?              Description                   { get; } = Description;

        /// <summary>
        /// A link to general information on the organisation, e.g. its homepage.
        /// </summary>
        public URL?                             LinkToGeneralInformation      { get; } = LinkToGeneralInformation;

        /// <summary>
        /// A link to the logo of the organisation.
        /// </summary>
        public URL?                             LinkToLogo                    { get; } = LinkToLogo;

        /// <summary>
        /// A link to a web form of the organisation.
        /// </summary>
        public URL?                             LinkToWebform                 { get; } = LinkToWebform;

        /// <summary>
        /// Whether the organisation can be reached around the clock.
        /// </summary>
        public Boolean?                         Available24hours              { get; } = Available24hours;

        /// <summary>
        /// What the organisation is responsible for.
        /// </summary>
        public IEnumerable<MultilingualString>  Responsibility                { get; } = Responsibility?.Distinct() ?? [];

        /// <summary>
        /// Whether the organisation agreed to the publication of its data.
        /// </summary>
        public Boolean?                         PublishingAgreement           { get; } = PublishingAgreement;

        /// <summary>
        /// The type of the organisation.
        /// </summary>
        public OrganisationType?                Type                          { get; } = Type;

        /// <summary>
        /// The national number of the organisation.
        /// </summary>
        public String?                          NationalOrganisationNumber    { get; } = NationalOrganisationNumber;

        /// <summary>
        /// The national register of the organisation.
        /// </summary>
        public String?                          NationalRegister              { get; } = NationalRegister;

        /// <summary>
        /// The VAT identification number of the organisation.
        /// </summary>
        public String?                          VATIdentificationNumber       { get; } = VATIdentificationNumber;

        /// <summary>
        /// The units of the organisation, e.g. with contact information.
        /// </summary>
        public IEnumerable<OrganisationUnit>    OrganisationUnits             { get; } = OrganisationUnits?.Distinct() ?? [];

        /// <summary>
        /// The identifications of the organisation in other systems, e.g. its operator identification.
        /// </summary>
        public IEnumerable<ExternalIdentifier>  ExternalIdentifier            { get; } = ExternalIdentifier?.Distinct() ?? [];

        /// <summary>
        /// The sub organisations.
        /// </summary>
        public IEnumerable<AOrganisation>       SubOrganisations              { get; } = SubOrganisations?.Distinct() ?? [];

        /// <summary>
        /// An optional extension.
        /// </summary>
        public XElement?                        AnOrganisationExtension       { get; } = AnOrganisationExtension;

        #endregion


        #region TryParseXML(XML, out AnOrganisation, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an organisation.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="AnOrganisation">The parsed organisation.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                  XML,
                                          [NotNullWhen(true)]  out AnOrganisation?  AnOrganisation,
                                          [NotNullWhen(false)] out String?          ErrorResponse)
        {

            AnOrganisation = null;

            if (!TryParseParts(XML, out var parts, out ErrorResponse))
                return false;

            AnOrganisation = new AnOrganisation(
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
                                 parts.AnOrganisationExtension,
                                 parts.GeneralTimeValidity,
                                 parts.OrganisationExtension
                             );

            return true;

        }

        #endregion

        #region (protected static) TryParseParts(XML, out Parts, out ErrorResponse)

        /// <summary>
        /// The particles of an organisation, as parsed.
        /// </summary>
        protected internal sealed record Parts(MultilingualString                Name,
                                               DateTimeOffset?                   LastUpdated,
                                               MultilingualString?               LegalName,
                                               MultilingualString?               Description,
                                               URL?                              LinkToGeneralInformation,
                                               URL?                              LinkToLogo,
                                               URL?                              LinkToWebform,
                                               Boolean?                          Available24hours,
                                               IEnumerable<MultilingualString>   Responsibility,
                                               Boolean?                          PublishingAgreement,
                                               OrganisationType?                 Type,
                                               String?                           NationalOrganisationNumber,
                                               String?                           NationalRegister,
                                               String?                           VATIdentificationNumber,
                                               IEnumerable<OrganisationUnit>     OrganisationUnits,
                                               IEnumerable<ExternalIdentifier>   ExternalIdentifier,
                                               IEnumerable<AOrganisation>        SubOrganisations,
                                               XElement?                         AnOrganisationExtension,
                                               OverallPeriod?                    GeneralTimeValidity,
                                               XElement?                         OrganisationExtension);

        /// <summary>
        /// Try to parse the particles of an organisation.
        /// </summary>
        protected internal static Boolean TryParseParts(XElement                          XML,
                                                        [NotNullWhen(true)]  out Parts?   Parts,
                                                        [NotNullWhen(false)] out String?  ErrorResponse)
        {

            Parts = null;

            if (!XML.TryParseMandatoryXML (DatexIINS.Facilities + "name",                          "organisation name",          MultilingualString.TryParseXML,         out MultilingualString?               name,                  out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "lastUpdated",                   "last updated",               DatexIIXMLExtensions.TryParseTimestamp,  out DateTimeOffset?                   lastUpdated,           out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.Facilities + "legalName",                     "legal name",                 MultilingualString.TryParseXML,         out MultilingualString?               legalName,             out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.Facilities + "description",                   "description",                MultilingualString.TryParseXML,         out MultilingualString?               description,           out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "linkToGeneralInformation",      "link to general information", URL.TryParse,                          out URL?                              linkToGeneralInformation, out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "linkToLogo",                    "link to logo",               URL.TryParse,                           out URL?                              linkToLogo,            out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "linkToWebform",                 "link to web form",           URL.TryParse,                           out URL?                              linkToWebform,         out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "available24hours",              "available 24 hours",         DatexIIXMLExtensions.TryParseBoolean,   out Boolean?                          available24hours,      out ErrorResponse) ||
                !XML.TryParseOptionalXMLs (DatexIINS.Facilities + "responsibility",                "responsibility",             MultilingualString.TryParseXML,         out IEnumerable<MultilingualString>   responsibility,        out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "publishingAgreement",           "publishing agreement",       DatexIIXMLExtensions.TryParseBoolean,   out Boolean?                          publishingAgreement,   out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "type",                          "organisation type",          OrganisationType.TryParse,              out OrganisationType?                 type,                  out ErrorResponse) ||
                !XML.TryParseOptionalXMLs (DatexIINS.Facilities + "organisationUnit",              "organisation unit",          OrganisationUnit.TryParseXML,           out IEnumerable<OrganisationUnit>     organisationUnits,     out ErrorResponse) ||
                !XML.TryParseOptionalXMLs (DatexIINS.Facilities + "externalIdentifier",            "external identifier",        Facilities.ExternalIdentifier.TryParseXML, out IEnumerable<ExternalIdentifier> externalIdentifiers, out ErrorResponse) ||
                !XML.TryParseOptionalXMLs (DatexIINS.Facilities + "subOrganisation",               "sub organisation",           AOrganisation.TryParseXML,              out IEnumerable<AOrganisation>        subOrganisations,      out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.Facilities + "generalTimeValidity",           "general time validity",      OverallPeriod.TryParseXML,              out OverallPeriod?                    generalTimeValidity,   out ErrorResponse))
            {
                return false;
            }

            Parts = new Parts(
                        name,
                        lastUpdated,
                        legalName,
                        description,
                        linkToGeneralInformation,
                        linkToLogo,
                        linkToWebform,
                        available24hours,
                        responsibility,
                        publishingAgreement,
                        type,
                        XML.OptionalText(DatexIINS.Facilities + "nationalOrganisationNumber"),
                        XML.OptionalText(DatexIINS.Facilities + "nationalRegister"),
                        XML.OptionalText(DatexIINS.Facilities + "vatIdentificationNumber"),
                        organisationUnits,
                        externalIdentifiers,
                        subOrganisations,
                        XML.Element(DatexIINS.Facilities + "_anOrganisationExtension"),
                        generalTimeValidity,
                        XML.Element(DatexIINS.Facilities + "_organisationExtension")
                    );

            return true;

        }

        #endregion


        #region (protected) ToXMLElements()

        /// <summary>
        /// The particles of an organisation, in schema order.
        /// </summary>
        protected override IEnumerable<Object?> ToXMLElements()
        {

            foreach (var element in base.ToXMLElements())
                yield return element;

            if (LastUpdated.HasValue)
                yield return new XElement(DatexIINS.Facilities + "lastUpdated",                 LastUpdated.Value.ToISO8601WithOffset());

            yield return Name.ToXML(DatexIINS.Facilities + "name");

            if (LegalName is not null)
                yield return LegalName.  ToXML(DatexIINS.Facilities + "legalName");

            if (Description is not null)
                yield return Description.ToXML(DatexIINS.Facilities + "description");

            if (LinkToGeneralInformation.HasValue)
                yield return new XElement(DatexIINS.Facilities + "linkToGeneralInformation",    LinkToGeneralInformation.Value.ToString());

            if (LinkToLogo.HasValue)
                yield return new XElement(DatexIINS.Facilities + "linkToLogo",                  LinkToLogo.              Value.ToString());

            if (LinkToWebform.HasValue)
                yield return new XElement(DatexIINS.Facilities + "linkToWebform",               LinkToWebform.           Value.ToString());

            if (Available24hours.HasValue)
                yield return new XElement(DatexIINS.Facilities + "available24hours",            Available24hours.Value ? "true" : "false");

            foreach (var responsibility in Responsibility)
                yield return responsibility.ToXML(DatexIINS.Facilities + "responsibility");

            if (PublishingAgreement.HasValue)
                yield return new XElement(DatexIINS.Facilities + "publishingAgreement",         PublishingAgreement.Value ? "true" : "false");

            if (Type.HasValue)
                yield return new XElement(DatexIINS.Facilities + "type",                        Type.Value.ToString());

            if (NationalOrganisationNumber is not null)
                yield return new XElement(DatexIINS.Facilities + "nationalOrganisationNumber",  NationalOrganisationNumber);

            if (NationalRegister is not null)
                yield return new XElement(DatexIINS.Facilities + "nationalRegister",            NationalRegister);

            if (VATIdentificationNumber is not null)
                yield return new XElement(DatexIINS.Facilities + "vatIdentificationNumber",     VATIdentificationNumber);

            foreach (var organisationUnit in OrganisationUnits)
                yield return organisationUnit.ToXML(DatexIINS.Facilities + "organisationUnit");

            foreach (var externalIdentifier in ExternalIdentifier)
                yield return externalIdentifier.ToXML(DatexIINS.Facilities + "externalIdentifier");

            foreach (var subOrganisation in SubOrganisations)
                yield return subOrganisation.ToXML(DatexIINS.Facilities + "subOrganisation");

            if (AnOrganisationExtension is not null)
                yield return new XElement(DatexIINS.Facilities + "_anOrganisationExtension", AnOrganisationExtension);

        }

        #endregion

        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public override XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.Facilities + "organisation",
                    new XAttribute(DatexIINS.XSI + "type", "afac:AnOrganisation"),
                    ToXMLElements());

        #endregion

    }

}
