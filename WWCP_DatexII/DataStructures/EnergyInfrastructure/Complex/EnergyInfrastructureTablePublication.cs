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
    /// A publication of static information on the infrastructure for vehicle energy supply.
    /// </summary>
    [XmlType("EnergyInfrastructureTablePublication", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public class EnergyInfrastructureTablePublication(DateTimeOffset                           PublicationTime,
                                                      InternationalIdentifier                  PublicationCreator,
                                                      Languages                                Language,

                                                      HeaderInformation?                       HeaderInformation                               = null,
                                                      IEnumerable<EnergyInfrastructureTable>?  EnergyInfrastructureTables                      = null,
                                                      XElement?                                EnergyInfrastructureTablePublicationExtension   = null,

                                                      String?                                  ModelBaseVersion                                = null,
                                                      String?                                  ExtensionName                                   = null,
                                                      String?                                  ExtensionVersion                                = null,
                                                      String?                                  ProfileName                                     = null,
                                                      String?                                  ProfileVersion                                  = null,
                                                      XElement?                                PayloadPublicationExtension                     = null)

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
        /// Management information relating to the publication.
        /// </summary>
        [XmlElement("headerInformation",                               Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public HeaderInformation?                      HeaderInformation                                { get; } = HeaderInformation;

        /// <summary>
        /// A collection of EnergyInfrastructureTable instances.
        /// </summary>
        [XmlElement("energyInfrastructureTable",                       Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<EnergyInfrastructureTable>  EnergyInfrastructureTables                       { get; } = EnergyInfrastructureTables?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional publication information.
        /// </summary>
        [XmlElement("_energyInfrastructureTablePublicationExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                               EnergyInfrastructureTablePublicationExtension    { get; } = EnergyInfrastructureTablePublicationExtension;

        #endregion


        // <?xml version="1.0" encoding="UTF-8"?>
        // <d2:payload xmlns:d2            = "http://datex2.eu/schema/3/d2Payload"
        //             xmlns:com           = "http://datex2.eu/schema/3/common"
        //             xmlns:locx          = "http://datex2.eu/schema/3/locationExtension"
        //             xmlns:loc           = "http://datex2.eu/schema/3/locationReferencing"
        //             xmlns               = "http://datex2.eu/schema/3/afirEnergyInfrastructure"
        //             xmlns:fac           = "http://datex2.eu/schema/3/afirFacilities"
        //             xmlns:prk           = "http://datex2.eu/schema/3/parking"
        //             xmlns:xsi           = "http://www.w3.org/2001/XMLSchema-instance"
        //             xsi:schemaLocation  = "http://datex2.eu/schema/3/d2Payload DATEXII_3_D2Payload.xsd"
        //             xmlns:egi           = "http://datex2.eu/schema/3/afirEnergyInfrastructure"
        //             xsi:type            = "aegi:EnergyInfrastructureTablePublication"
        //             lang                = "de"
        //             modelBaseVersion    = "3"
        //             profileName         = "Level C profile Energy Infrastructure"
        //             profileVersion      = "00-01-00">
        //
        //     <com:publicationTime>2025-01-10T11:13:51+01:00</com:publicationTime>
        //
        //     <com:publicationCreator>
        //         <com:country>de</com:country>
        //         <com:nationalIdentifier>DE-NAP-OrganisationXY</com:nationalIdentifier>
        //     </com:publicationCreator>
        //
        //     <headerInformation>
        //         <com:confidentiality>noRestriction</com:confidentiality>
        //         <com:allowedDeliveryChannel>anyGeneralDeliveryService</com:allowedDeliveryChannel>
        //         <com:informationStatus>test</com:informationStatus>
        //     </headerInformation>
        // 
        //     <energyInfrastructureTable id="2474A514-0E5D-48F9-A908-F185DD4177A2" version="2">
        //       ...
        //     </energyInfrastructureTable>
        // 
        // </d2:payload>


        #region TryParseXML(XML, out EnergyInfrastructureTablePublication, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an EnergyInfrastructureTablePublication.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyInfrastructureTablePublication">The parsed EnergyInfrastructureTablePublication.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                                        XML,
                                          [NotNullWhen(true)]  out EnergyInfrastructureTablePublication?  EnergyInfrastructureTablePublication,
                                          [NotNullWhen(false)] out String?                                ErrorResponse)
        {

            XML = DatexIISchemas.ResolveExtendedValues(new XElement(XML));

            EnergyInfrastructureTablePublication  = null;
            ErrorResponse                         = null;


            #region TryParse PublicationTime               [mandatory]

            if (!XML.TryParseMandatoryTimestamp(DatexIINS.Common + "publicationTime",
                                                "publication time",
                                                out DateTimeOffset publicationTime,
                                                out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse PublicationCreator            [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.Common + "publicationCreator",
                                       "publication creator",
                                       InternationalIdentifier.TryParseXML,
                                       out InternationalIdentifier? publicationCreator,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse Language                      [mandatory]

            if (!XML.TryParseMandatoryAttribute("lang",
                                                "publication language",
                                                LanguagesExtensions.TryParse,
                                                out Languages language,
                                                out ErrorResponse))
            {
                return false;
            }

            #endregion


            #region TryParse HeaderInformation             [optional]

            if (!XML.TryParseOptional(DatexIINS.EnergyInfrastructure + "headerInformation",
                                      "header information",
                                      HeaderInformation.TryParseXML,
                                      out HeaderInformation? headerInformation,
                                      out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse EnergyInfrastructureTables    [optional]

            if (!XML.TryParseOptionalXMLs    (DatexIINS.EnergyInfrastructure + "energyInfrastructureTable",
                                              "energy infrastructure table",
                                              EnergyInfrastructureTable.TryParseXML,
                                              out IEnumerable<EnergyInfrastructureTable> energyInfrastructureTables,
                                              out ErrorResponse))
            {
                return false;
            }

            #endregion


            #region TryParse ModelBaseVersion              [optional]

            if (XML.TryParseOptionalTextAttribute("modelBaseVersion",
                                                  "publication model base version",
                                                  out String? modelBaseVersion,
                                                  out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ExtensionName                 [optional]

            if (XML.TryParseOptionalTextAttribute("extensionName",
                                                  "publication extension name",
                                                  out String? extensionName,
                                                  out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ExtensionVersion              [optional]

            if (XML.TryParseOptionalTextAttribute("extensionVersion",
                                                  "publication extension version",
                                                  out String? extensionVersion,
                                                  out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ProfileName                   [optional]

            if (XML.TryParseOptionalTextAttribute("profileName",
                                                  "publication profile name",
                                                  out String? profileName,
                                                  out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ProfileVersion                [optional]

            if (XML.TryParseOptionalTextAttribute("profileVersion",
                                                  "publication profile version",
                                                  out String? profileVersion,
                                                  out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            EnergyInfrastructureTablePublication  = new EnergyInfrastructureTablePublication(

                                                        publicationTime,
                                                        publicationCreator,
                                                        language,

                                                        headerInformation,
                                                        energyInfrastructureTables,
                                                        XML.Element(DatexIINS.EnergyInfrastructure + "_energyInfrastructureTablePublicationExtension"),

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

                          new XAttribute(DatexIINS.XSI + "schemaLocation",  "http://datex2.eu/schema/3/d2Payload DATEXII_3_D2Payload.xsd"),
                          new XAttribute(DatexIINS.XSI + "type",            "aegi:EnergyInfrastructureTablePublication"),
                          new XAttribute("lang",                             Language.AsText()),
                          new XAttribute("modelBaseVersion",                ModelBaseVersion),
                          new XAttribute("profileName",                     ProfileName),
                          new XAttribute("profileVersion",                  ProfileVersion),

                          new XElement(DatexIINS.Common + "publicationTime",   PublicationTime.ToISO8601()),
                          PublicationCreator.ToXML(),
                          HeaderInformation?.ToXML(DatexIINS.EnergyInfrastructure),

                          EnergyInfrastructureTables.Select(energyInfrastructureTable => energyInfrastructureTable.ToXML()),

                          EnergyInfrastructureTablePublicationExtension is not null
                              ? new XElement(DatexIINS.EnergyInfrastructure + "_energyInfrastructureTablePublicationExtension", EnergyInfrastructureTablePublicationExtension)
                              : null

                      );

            return DatexIISchemas.ExtendUnknownValues(xml);

        }

        #endregion

        #region ToJSON(Dialect = DATEXII_3_7)

        /// <summary>
        /// Return a JSON representation of this publication, in the given dialect.
        /// </summary>
        /// <param name="Dialect">The JSON dialect.</param>
        public JObject ToJSON(DatexIIJSONDialect Dialect = DatexIIJSONDialect.DATEXII_3_7)

            => DatexIIJSON.ToJSON(ToXML(), Dialect);

        #endregion

        #region TryParseJSON(JSON, out EnergyInfrastructureTablePublication, out ErrorResponse, Dialect = DATEXII_3_7)

        /// <summary>
        /// Try to parse the given JSON representation of a publication, in the given dialect.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="EnergyInfrastructureTablePublication">The parsed publication.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="Dialect">The JSON dialect.</param>
        public static Boolean TryParseJSON(JObject                                   JSON,
                                           [NotNullWhen(true)]  out EnergyInfrastructureTablePublication?  EnergyInfrastructureTablePublication,
                                           [NotNullWhen(false)] out String?          ErrorResponse,
                                           DatexIIJSONDialect                        Dialect   = DatexIIJSONDialect.DATEXII_3_7)
        {

            EnergyInfrastructureTablePublication = null;

            if (!DatexIIJSON.TryParse(JSON, out var payloadXML, out ErrorResponse, Dialect))
                return false;

            if (!TryParseXML(payloadXML, out EnergyInfrastructureTablePublication, out ErrorResponse))
                return false;

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
