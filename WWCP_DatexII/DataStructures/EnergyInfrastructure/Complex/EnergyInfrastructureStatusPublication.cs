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

using cloud.charging.open.protocols.DatexII.v3.Common;
using Newtonsoft.Json.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Dynamic information on the status of energy supplying sites.
    /// </summary>
    [XmlType("EnergyInfrastructureStatusPublication", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public class EnergyInfrastructureStatusPublication(DateTimeOffset                                             PublicationTime,
                                                       InternationalIdentifier                                    PublicationCreator,
                                                       Languages                                                  Language,

                                                       IEnumerable<EnergyInfrastructureTableVersionedReference>?  TableReferences                                  = null,
                                                       HeaderInformation?                                         HeaderInformation                                = null,
                                                       IEnumerable<EnergyInfrastructureSiteStatus>?               EnergyInfrastructureSiteStatus                   = null,
                                                       XElement?                                                  EnergyInfrastructureStatusPublicationExtension   = null,

                                                       String?                                                    ModelBaseVersion                                 = null,
                                                       String?                                                    ExtensionName                                    = null,
                                                       String?                                                    ExtensionVersion                                 = null,
                                                       String?                                                    ProfileName                                      = null,
                                                       String?                                                    ProfileVersion                                   = null,
                                                       XElement?                                                  PayloadPublicationExtension                      = null,
                                                       Boolean                                                    IsDelta                                          = false)

        : APayloadPublication(PublicationTime,
                              PublicationCreator,
                              Language,

                              ModelBaseVersion,
                              ExtensionName,
                              ExtensionVersion,
                              ProfileName,
                              ProfileVersion,
                              PayloadPublicationExtension)

    {

        #region Properties

        /// <summary>
        /// Reference to static tables containing the sites referenced in this publication.
        /// </summary>
        [XmlElement("tableReference",                                   Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<EnergyInfrastructureTableVersionedReference>  TableReferences                                   { get; } = TableReferences?.               Distinct() ?? [];

        /// <summary>
        /// Management information relating to the publication.
        /// </summary>
        [XmlElement("headerInformation",                                Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public HeaderInformation?                                        HeaderInformation                                 { get; } = HeaderInformation;

        /// <summary>
        /// Dynamic status information of one or more energy supplying sites.
        /// </summary>
        [XmlElement("energyInfrastructureSiteStatus",                   Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<EnergyInfrastructureSiteStatus>               EnergyInfrastructureSiteStatus                    { get; } = EnergyInfrastructureSiteStatus?.Distinct() ?? [];

        /// <summary>
        /// Whether this publication has only what changed since the last one (deltaPush), or everything (snapshotPush).
        /// Only the Mobilithek JSON dialect carries it.
        /// </summary>
        public Boolean                                                   IsDelta                                           { get; } = IsDelta;

        /// <summary>
        /// Optional extension element for additional publication information.
        /// </summary>
        [XmlElement("_energyInfrastructureStatusPublicationExtension",  Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public XElement?                                                 EnergyInfrastructureStatusPublicationExtension    { get; } = EnergyInfrastructureStatusPublicationExtension;

        #endregion


        #region TryParseXML(XML, out EnergyInfrastructureStatusPublication, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an EnergyInfrastructureStatusPublication.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyInfrastructureStatusPublication">The parsed EnergyInfrastructureStatusPublication.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                                         XML,
                                          [NotNullWhen(true)]  out EnergyInfrastructureStatusPublication?  EnergyInfrastructureStatusPublication,
                                          [NotNullWhen(false)] out String?                                 ErrorResponse)
        {

            XML = DatexIISchemas.ResolveExtendedValues(new XElement(XML));

            EnergyInfrastructureStatusPublication  = null;
            ErrorResponse                          = null;


            #region TryParse PublicationTime                   [mandatory]

            if (!XML.TryParseMandatoryTimestamp(DatexIINS.Common + "publicationTime",
                                                "publication time",
                                                out DateTimeOffset publicationTime,
                                                out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse PublicationCreator                [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.Common + "publicationCreator",
                                       "publication creator",
                                       InternationalIdentifier.TryParseXML,
                                       out InternationalIdentifier? publicationCreator,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse Language                          [mandatory]

            if (!XML.TryParseMandatoryAttribute("lang",
                                                "publication language",
                                                LanguagesExtensions.TryParse,
                                                out Languages language,
                                                out ErrorResponse))
            {
                return false;
            }

            #endregion


            #region TryParse TableReferences                   [optional]

            if (!XML.TryParseOptionalXMLs    (DatexIINS.EnergyInfrastructure + "tableReference",
                                              "publication table references",
                                              EnergyInfrastructureTableVersionedReference.TryParseXML,
                                              out IEnumerable<EnergyInfrastructureTableVersionedReference> tableReferences,
                                              out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse HeaderInformation                 [optional]

            if (!XML.TryParseOptional(DatexIINS.EnergyInfrastructure + "headerInformation",
                                      "header information",
                                      HeaderInformation.TryParseXML,
                                      out HeaderInformation? headerInformation,
                                      out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse EnergyInfrastructureSiteStatus    [optional]

            if (!XML.TryParseOptionalXMLs    (DatexIINS.EnergyInfrastructure + "energyInfrastructureSiteStatus",
                                              "energy infrastructure site status",
                                              EnergyInfrastructure.EnergyInfrastructureSiteStatus.TryParseXML,
                                              out IEnumerable<EnergyInfrastructureSiteStatus> energyInfrastructureSiteStatus,
                                              out ErrorResponse))
            {
                return false;
            }

            #endregion


            #region TryParse ModelBaseVersion                  [optional]

            if (XML.TryParseOptionalTextAttribute("modelBaseVersion",
                                                  "publication model base version",
                                                  out String? modelBaseVersion,
                                                  out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ExtensionName                     [optional]

            if (XML.TryParseOptionalTextAttribute("extensionName",
                                                  "publication extension name",
                                                  out String? extensionName,
                                                  out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ExtensionVersion                  [optional]

            if (XML.TryParseOptionalTextAttribute("extensionVersion",
                                                  "publication extension version",
                                                  out String? extensionVersion,
                                                  out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ProfileName                       [optional]

            if (XML.TryParseOptionalTextAttribute("profileName",
                                                  "publication profile name",
                                                  out String? profileName,
                                                  out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ProfileVersion                    [optional]

            if (XML.TryParseOptionalTextAttribute("profileVersion",
                                                  "publication profile version",
                                                  out String? profileVersion,
                                                  out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            EnergyInfrastructureStatusPublication  = new EnergyInfrastructureStatusPublication(

                                                         publicationTime,
                                                         publicationCreator,
                                                         language,

                                                         tableReferences,
                                                         headerInformation,
                                                         energyInfrastructureSiteStatus,
                                                         XML.Element(DatexIINS.EnergyInfrastructure + "_energyInfrastructureStatusPublicationExtension"),

                                                         modelBaseVersion,
                                                         extensionName,
                                                         extensionVersion,
                                                         profileName,
                                                         profileVersion,
                                                         XML.Element(DatexIINS.Common + "_payloadPublicationExtension")

                                                     );

            return true;

        }

        #endregion


        #region ToXML()

        public XElement ToXML()
        {

            var xml = new XElement(DatexIINS.D2Payload + "payload",

                          new XAttribute(XNamespace.Xmlns + "d2",    DatexIINS.D2Payload),
                          new XAttribute(XNamespace.Xmlns + "com",   DatexIINS.Common),
                          new XAttribute(XNamespace.Xmlns + "aegi",   DatexIINS.EnergyInfrastructure),
                          new XAttribute(XNamespace.Xmlns + "afac",   DatexIINS.Facilities),
                          new XAttribute(XNamespace.Xmlns + "locx",  DatexIINS.LocationExtension),
                          new XAttribute(XNamespace.Xmlns + "loc",   DatexIINS.LocationReferencing),
                          new XAttribute(XNamespace.Xmlns + "comx",  DatexIINS.CommonExtension),
                          new XAttribute(XNamespace.Xmlns + "xsi",   DatexIINS.XSI),

                          new XAttribute(DatexIINS.XSI + "schemaLocation",    "http://datex2.eu/schema/3/d2Payload DATEXII_3_D2Payload.xsd"),
                          new XAttribute(DatexIINS.XSI + "type",              "aegi:EnergyInfrastructureStatusPublication"),
                          new XAttribute("lang",                               Language.AsText()),
                          new XAttribute("modelBaseVersion",                  ModelBaseVersion),
                          new XAttribute("profileName",                       ProfileName),
                          new XAttribute("profileVersion",                    ProfileVersion),

                          new XElement(DatexIINS.Common + "publicationTime",   PublicationTime.ToISO8601()),
                          PublicationCreator.ToXML(),
                          TableReferences.Select(tableReference => tableReference.ToXML()),
                          HeaderInformation?.ToXML(DatexIINS.EnergyInfrastructure),

                          EnergyInfrastructureSiteStatus.Select(siteStatus => siteStatus.ToXML(DatexIINS.EnergyInfrastructure + "energyInfrastructureSiteStatus")),

                          EnergyInfrastructureStatusPublicationExtension is not null
                              ? EnergyInfrastructureStatusPublicationExtension.ToExtensionXML(DatexIINS.EnergyInfrastructure + "_energyInfrastructureStatusPublicationExtension")
                              : null

                      );

            return DatexIISchemas.ExtendUnknownValues(xml);

        }

        #endregion

        #region AsDelta(IsDelta = true)

        /// <summary>
        /// This publication, marked as having only what changed (or, with false, everything).
        /// </summary>
        /// <param name="IsDelta">Whether the publication has only what changed.</param>
        public EnergyInfrastructureStatusPublication AsDelta(Boolean IsDelta = true)

            => new (PublicationTime,
                    PublicationCreator,
                    Language,
                    TableReferences,
                    HeaderInformation,
                    EnergyInfrastructureSiteStatus,
                    EnergyInfrastructureStatusPublicationExtension,
                    ModelBaseVersion,
                    ExtensionName,
                    ExtensionVersion,
                    ProfileName,
                    ProfileVersion,
                    PayloadPublicationExtension,
                    IsDelta);

        #endregion

        #region ToJSON(Dialect = DATEXII_3_7)

        /// <summary>
        /// Return a JSON representation of this publication, in the given dialect.
        /// The Mobilithek dialect wraps it into a message container, as a snapshotPush or, when IsDelta, as a deltaPush.
        /// </summary>
        /// <param name="Dialect">The JSON dialect.</param>
        public JObject ToJSON(DatexIIJSONDialect Dialect = DatexIIJSONDialect.DATEXII_3_7)

            => DatexIIJSON.ToJSON(ToXML(), Dialect, IsDelta);

        #endregion

        #region TryParseJSON(JSON, out EnergyInfrastructureStatusPublication, out ErrorResponse, Dialect = DATEXII_3_7)

        /// <summary>
        /// Try to parse the given JSON representation of a publication, in the given dialect.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="EnergyInfrastructureStatusPublication">The parsed publication.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="Dialect">The JSON dialect.</param>
        public static Boolean TryParseJSON(JObject                                   JSON,
                                           [NotNullWhen(true)]  out EnergyInfrastructureStatusPublication?  EnergyInfrastructureStatusPublication,
                                           [NotNullWhen(false)] out String?          ErrorResponse,
                                           DatexIIJSONDialect                        Dialect   = DatexIIJSONDialect.DATEXII_3_7)
        {

            EnergyInfrastructureStatusPublication = null;

            if (!DatexIIJSON.TryParse(JSON, out var payloadXML, out ErrorResponse, Dialect))
                return false;

            if (!TryParseXML(payloadXML, out EnergyInfrastructureStatusPublication, out ErrorResponse))
                return false;

            if (Dialect == DatexIIJSONDialect.Mobilithek_01_00_00 &&
                JSON["messageContainer"]?["exchangeInformation"]?["exchangeContext"]?["codedExchangeProtocol"]?["value"]?.Value<String>() == "deltaPush")
            {
                EnergyInfrastructureStatusPublication = EnergyInfrastructureStatusPublication.AsDelta();
            }

            return true;

        }

        #endregion

        #region ToXDocument()

        public XDocument ToXDocument()
        {

            return new XDocument(
                       new XDeclaration("1.0", "UTF-8", null),
                       ToXML()
                   );

        }

        #endregion

    }

}
