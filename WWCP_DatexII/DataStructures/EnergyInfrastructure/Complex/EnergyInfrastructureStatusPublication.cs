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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Dynamic information on the status of energy supplying sites.
    /// </summary>
    [XmlType("EnergyInfrastructureStatusPublication", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
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
                                                       XElement?                                                  PayloadPublicationExtension                      = null)

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
        [XmlElement("tableReference",                                   Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyInfrastructureTableVersionedReference>  TableReferences                                   { get; } = TableReferences?.               Distinct() ?? [];

        /// <summary>
        /// Management information relating to the publication.
        /// </summary>
        [XmlElement("headerInformation",                                Namespace = "http://datex2.eu/schema/3/common")]
        public HeaderInformation?                                        HeaderInformation                                 { get; } = HeaderInformation;

        /// <summary>
        /// Dynamic status information of one or more energy supplying sites.
        /// </summary>
        [XmlElement("energyInfrastructureSiteStatus",                   Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyInfrastructureSiteStatus>               EnergyInfrastructureSiteStatus                    { get; } = EnergyInfrastructureSiteStatus?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional publication information.
        /// </summary>
        [XmlElement("_energyInfrastructureStatusPublicationExtension",  Namespace = "http://datex2.eu/schema/3/common")]
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

            if (XML.TryParseOptionalElements(DatexIINS.EnergyInfrastructure + "tableReference",
                                             "publication table references",
                                             EnergyInfrastructureTableVersionedReference.TryParseXML,
                                             out IEnumerable<EnergyInfrastructureTableVersionedReference> tableReferences,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse HeaderInformation                 [optional]

            if (XML.TryParseOptional(DatexIINS.EnergyInfrastructure + "headerInformation",
                                     "header information",
                                     HeaderInformation.TryParseXML,
                                     out HeaderInformation? headerInformation,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse EnergyInfrastructureSiteStatus    [optional]

            if (XML.TryParseOptionalElements(DatexIINS.EnergyInfrastructure + "energyInfrastructureSiteStatus",
                                             "header information",
                                             EnergyInfrastructure.EnergyInfrastructureSiteStatus.TryParseXML,
                                             out IEnumerable<EnergyInfrastructureSiteStatus> energyInfrastructureSiteStatus,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
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
                                                         XML.Element(DatexIINS.Common + "_energyInfrastructureStatusPublicationExtension"),

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

    }

}
