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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// An image, with encoded data and identification of format.
    /// </summary>
    [XmlType("Image", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class Image(Byte[]        Data,
                       ImageFormats  Format)
    {

        /// <summary>
        /// Encoded image data.
        /// </summary>
        [XmlElement("imageData",        Namespace = "http://datex2.eu/schema/3/common")]
        public Byte[]        Data              { get; set; } = Data;

        /// <summary>
        /// Identifies the image format of the associated image data.
        /// </summary>
        [XmlElement("imageFormat",      Namespace = "http://datex2.eu/schema/3/facilities")]
        public ImageFormats  Format            { get; set; } = Format;

        /// <summary>
        /// Optional extension element for additional content.
        /// </summary>
        [XmlElement("_imageExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?     ImageExtension    { get; set; }

    }

}
