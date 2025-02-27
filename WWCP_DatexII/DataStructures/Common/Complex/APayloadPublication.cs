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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// A payload publication of traffic related information or associated management information created at a specific point in time 
    /// that can be exchanged via a DATEX II interface.
    /// </summary>
    [XmlType("PayloadPublication", Namespace = "http://datex2.eu/schema/3/common")]
    public abstract class APayloadPublication(DateTimeOffset           PublicationTime,
                                              InternationalIdentifier  PublicationCreator,
                                              Languages                Language,

                                              String?                  ModelBaseVersion              = null,
                                              String?                  ExtensionName                 = null,
                                              String?                  ExtensionVersion              = null,
                                              String?                  ProfileName                   = null,
                                              String?                  ProfileVersion                = null,
                                              XElement?                PayloadPublicationExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Date/time at which the payload publication was created.
        /// </summary>
        [XmlElement("publicationTime",     Namespace = "http://datex2.eu/schema/3/common")]
        public DateTimeOffset           PublicationTime                { get; } = PublicationTime;

        /// <summary>
        /// Identifier of the publication creator.
        /// </summary>
        [XmlElement("publicationCreator",  Namespace = "http://datex2.eu/schema/3/common")]
        public InternationalIdentifier  PublicationCreator             { get; } = PublicationCreator;

        /// <summary>
        /// The default language used throughout the payload publication.
        /// </summary>
        [XmlAttribute("lang")]
        public Languages                Language                       { get; } = Language;

        /// <summary>
        /// Model base version, fixed to "3".
        /// </summary>
        [XmlAttribute("modelBaseVersion")]
        public String                   ModelBaseVersion               { get; } = ModelBaseVersion ?? "3";

        /// <summary>
        /// Optional extension name.
        /// </summary>
        [XmlAttribute("extensionName")]
        public String?                  ExtensionName                  { get; } = ExtensionName;

        /// <summary>
        /// Optional extension version.
        /// </summary>
        [XmlAttribute("extensionVersion")]
        public String?                  ExtensionVersion               { get; } = ExtensionVersion;

        /// <summary>
        /// The profile name, default "Level C profile Energy Infrastructure".
        /// </summary>
        [XmlAttribute("profileName")]
        public String                   ProfileName                    { get; } = ProfileName      ?? "Level C profile Energy Infrastructure";

        /// <summary>
        /// The profile version, default "00-02-00".
        /// </summary>
        [XmlAttribute("profileVersion")]
        public String                   ProfileVersion                 { get; } = ProfileVersion   ?? "00-02-00";

        /// <summary>
        /// Optional extension element for additional payload publication information.
        /// </summary>
        [XmlElement("_payloadPublicationExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                PayloadPublicationExtension    { get; } = PayloadPublicationExtension;

        #endregion

    }

}
