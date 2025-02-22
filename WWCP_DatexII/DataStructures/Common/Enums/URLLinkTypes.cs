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

using System.Xml.Serialization;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Types of URL links.
    /// </summary>
    public enum URLLinkTypes
    {

        /// <summary>
        /// URL link to a PDF document.
        /// </summary>
        [XmlEnum("documentPdf")]
        DocumentPdf,

        /// <summary>
        /// URL link to an HTML page.
        /// </summary>
        [XmlEnum("html")]
        Html,

        /// <summary>
        /// URL link to an image.
        /// </summary>
        [XmlEnum("image")]
        Image,

        /// <summary>
        /// URL link to an RSS feed.
        /// </summary>
        [XmlEnum("rss")]
        Rss,

        /// <summary>
        /// URL link to a video stream.
        /// </summary>
        [XmlEnum("videoStream")]
        VideoStream,

        /// <summary>
        /// URL link to a voice stream.
        /// </summary>
        [XmlEnum("voiceStream")]
        VoiceStream,

        /// <summary>
        /// Other than as defined in this enumeration.
        /// </summary>
        [XmlEnum("other")]
        Other,

        /// <summary>
        /// Extended value for non-standard URL link types.
        /// </summary>
        [XmlEnum("_extended")]
        Extended

    }

}
