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
    public abstract class APayloadPublication(DateTime                 PublicationTime,
                                              InternationalIdentifier  PublicationCreator,
                                              Languages                Language)
    {

        /// <summary>
        /// Date/time at which the payload publication was created.
        /// </summary>
        [XmlElement("publicationTime",     Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime                 PublicationTime                { get; set; } = PublicationTime;

        /// <summary>
        /// Identifier of the publication creator.
        /// </summary>
        [XmlElement("publicationCreator",  Namespace = "http://datex2.eu/schema/3/common")]
        public InternationalIdentifier  PublicationCreator             { get; set; } = PublicationCreator;

        /// <summary>
        /// The default language used throughout the payload publication.
        /// </summary>
        [XmlAttribute("lang")]
        public Languages                Language                       { get; set; } = Language;

        /// <summary>
        /// Model base version, fixed to "3".
        /// </summary>
        [XmlAttribute("modelBaseVersion")]
        public String                   ModelBaseVersion               { get; }      = "3";

        /// <summary>
        /// Optional extension name.
        /// </summary>
        [XmlAttribute("extensionName")]
        public String?                  ExtensionName                  { get; set; }

        /// <summary>
        /// Optional extension version.
        /// </summary>
        [XmlAttribute("extensionVersion")]
        public String?                  ExtensionVersion               { get; set; }

        /// <summary>
        /// The profile name, default "Level C profile Energy Infrastructure".
        /// </summary>
        [XmlAttribute("profileName")]
        public String                   ProfileName                    { get; } = "Level C profile Energy Infrastructure";

        /// <summary>
        /// The profile version, default "00-02-00".
        /// </summary>
        [XmlAttribute("profileVersion")]
        public String                   ProfileVersion                 { get; } = "00-02-00";

        /// <summary>
        /// Optional extension element for additional payload publication information.
        /// </summary>
        [XmlElement("_payloadPublicationExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                PayloadPublicationExtension    { get; set; }

    }

}
