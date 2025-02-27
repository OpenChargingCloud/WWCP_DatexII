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

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Specification of an organisation. It must contain at least one unit.
    /// </summary>
    [XmlType("OrganisationSpecification", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class OrganisationSpecification(String                            Id,
                                           String                            Version,
                                           MultilingualString                Name,

                                           String?                           OperatorId                           = null,
                                           String?                           ProviderId                           = null,
                                           String?                           OperatorIdBNetzA                     = null,
                                           DateTime?                         LastUpdated                          = null,
                                           String?                           ExternalCode                         = null,
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
                                           IEnumerable<AOrganisation>?       SubOrganisations                     = null,

                                           OverallPeriod?                    GeneralTimeValidity                  = null,

                                           XElement?                         OrganisationSpecificationExtension   = null)

        : AOrganisation(GeneralTimeValidity)

    {

        #region Properties

        [XmlAttribute("id")]
        public String                           Id                                    { get; } = Id;


        [XmlAttribute("version")]
        public String                           Version                               { get; } = Version;

        /// <summary>
        /// Name of the organisation.
        /// </summary>
        [XmlElement("name",                                 Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString               Name                                  { get; } = Name;


        /// <summary>
        /// Only in case of energy related operator: Operator-ID.
        /// </summary>
        [XmlElement("operatorId",                           Namespace = "http://datex2.eu/schema/3/common")]
        public String?                          OperatorId                            { get; } = OperatorId;

        /// <summary>
        /// Only in case of mobility service provider: Provider ID.
        /// </summary>
        [XmlElement("providerId",                           Namespace = "http://datex2.eu/schema/3/common")]
        public String?                          ProviderId                            { get; } = ProviderId;

        /// <summary>
        /// Only in case of energy-related operator: Betreiber ID der Bundesnetzagentur.
        /// </summary>
        [XmlElement("operatorIdBnetzA",                     Namespace = "http://datex2.eu/schema/3/common")]
        public String?                          OperatorIdBNetzA                      { get; } = OperatorIdBNetzA;

        /// <summary>
        /// The date/time at which this information was last updated.
        /// </summary>
        [XmlElement("lastUpdated",                          Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime?                        LastUpdated                           { get; } = LastUpdated;

        /// <summary>
        /// An external code to identify the organisation.
        /// </summary>
        [XmlElement("externalCode",                         Namespace = "http://datex2.eu/schema/3/common")]
        public String?                          ExternalCode                          { get; } = ExternalCode;

        /// <summary>
        /// The legal name of the organisation.
        /// </summary>
        [XmlElement("legalName",                            Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?              LegalName                             { get; } = LegalName;

        /// <summary>
        /// A description of the organisation.
        /// </summary>
        [XmlElement("description",                          Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?              Description                           { get; } = Description;

        /// <summary>
        /// An internet address from where further relevant information about this organisation may be obtained.
        /// </summary>
        [XmlElement("linkToGeneralInformation",             Namespace = "http://datex2.eu/schema/3/common")]
        public URL?                             LinkToGeneralInformation              { get; } = LinkToGeneralInformation;

        /// <summary>
        /// An internet address from where the logo of this organisation may be obtained.
        /// </summary>
        [XmlElement("linkToLogo",                           Namespace = "http://datex2.eu/schema/3/common")]
        public URL?                             LinkToLogo                            { get; } = LinkToLogo;

        /// <summary>
        /// An internet address pointing to a webform, which people can use to send messages to this organisation.
        /// </summary>
        [XmlElement("linkToWebform",                        Namespace = "http://datex2.eu/schema/3/common")]
        public URL?                             LinkToWebform                         { get; } = LinkToWebform;

        /// <summary>
        /// Specifies if the availability is 24 hours a day.
        /// If omitted, this information is unknown or heterogeneous.
        /// </summary>
        [XmlElement("available24hours",                     Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?                         Available24hours                      { get; } = Available24hours;

        /// <summary>
        /// Specification of services or other duties the organisation is responsible for.
        /// </summary>
        [XmlElement("responsibility",                       Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<MultilingualString>  Responsibility                        { get; } = Responsibility ?? [];

        /// <summary>
        /// Indication whether the organisation accepted publishing its information.
        /// </summary>
        [XmlElement("publishingAgreement",                  Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?                         PublishingAgreement                   { get; } = PublishingAgreement;

        /// <summary>
        /// Information whether the organisation in question belongs to the public or private sector.
        /// </summary>
        [XmlElement("type",                                 Namespace = "http://datex2.eu/schema/3/facilities")]
        public OrganisationType?                Type                                  { get; } = Type;

        /// <summary>
        /// The national organisation number in the specified national register, if applicable.
        /// </summary>
        [XmlElement("nationalOrganisationNumber",           Namespace = "http://datex2.eu/schema/3/common")]
        public String?                          NationalOrganisationNumber            { get; } = NationalOrganisationNumber;

        /// <summary>
        /// A national register where the organisation is registered.
        /// </summary>
        [XmlElement("nationalRegister",                     Namespace = "http://datex2.eu/schema/3/common")]
        public String?                          NationalRegister                      { get; } = NationalRegister;

        /// <summary>
        /// The VAT identification number of the organisation.
        /// </summary>
        [XmlElement("vatIdentificationNumber",              Namespace = "http://datex2.eu/schema/3/common")]
        public String?                          VATIdentificationNumber               { get; } = VATIdentificationNumber;

        /// <summary>
        /// One or more organisational units.
        /// </summary>
        [XmlElement("organisationUnit",                     Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<OrganisationUnit>    OrganisationUnits                     { get; } = OrganisationUnits ?? [];

        /// <summary>
        /// A sub organisation that could substitute its role.
        /// </summary>
        [XmlElement("subOrganisation",                      Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<AOrganisation>       SubOrganisations                      { get; } = SubOrganisations ?? [];

        /// <summary>
        /// Optional extension element for additional organisation specification information.
        /// </summary>
        [XmlElement("_organisationSpecificationExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                        OrganisationSpecificationExtension    { get; } = OrganisationSpecificationExtension;

        #endregion

    }

}
