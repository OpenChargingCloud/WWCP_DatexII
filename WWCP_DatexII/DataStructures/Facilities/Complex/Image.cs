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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// An image, with encoded data and identification of format.
    /// </summary>
    [XmlType("Image", Namespace = "http://datex2.eu/schema/3/afirFacilities")]
    public class Image(Byte[]       Data,
                       ImageFormat  Format,
                       XElement?    ImageExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Encoded image data.
        /// </summary>
        [XmlElement("imageData",        Namespace = "http://datex2.eu/schema/3/common")]
        public Byte[]       Data              { get; } = Data;

        /// <summary>
        /// Identifies the image format of the associated image data.
        /// </summary>
        [XmlElement("imageFormat",      Namespace = "http://datex2.eu/schema/3/afirFacilities")]
        public ImageFormat  Format            { get; } = Format;

        /// <summary>
        /// Optional extension element for additional content.
        /// </summary>
        [XmlElement("_imageExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?    ImageExtension    { get; } = ImageExtension;

        #endregion


        #region TryParseXML(XML, out Image, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an image.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Image">The parsed image.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                          XML,
                                          [NotNullWhen(true)]  out Image?   Image,
                                          [NotNullWhen(false)] out String?  ErrorResponse)
        {

            Image          = null;
            ErrorResponse  = null;

            var data = XML.OptionalText(DatexIINS.Facilities + "imageData");

            if (data is null)
            {
                ErrorResponse = "The mandatory image data is missing!";
                return false;
            }

            Byte[] bytes;

            try
            {
                bytes = Convert.FromBase64String(data);
            }
            catch (FormatException)
            {
                ErrorResponse = "The image data is no base64!";
                return false;
            }

            if (!XML.TryParseMandatoryValue(DatexIINS.Facilities + "imageFormat",
                                            "image format",
                                            ImageFormat.TryParse,
                                            out ImageFormat format,
                                            out ErrorResponse))
            {
                return false;
            }

            Image = new Image(
                        bytes,
                        format,
                        XML.Element(DatexIINS.Facilities + "_imageExtension")
                    );

            return true;

        }

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.Facilities + "image",

                   new XElement(DatexIINS.Facilities + "imageData",     Convert.ToBase64String(Data)),
                   new XElement(DatexIINS.Facilities + "imageFormat",   Format.ToString()),

                   ImageExtension is not null
                       ? ImageExtension.ToExtensionXML(DatexIINS.Facilities + "_imageExtension")
                       : null

               );

        #endregion

    }

}
