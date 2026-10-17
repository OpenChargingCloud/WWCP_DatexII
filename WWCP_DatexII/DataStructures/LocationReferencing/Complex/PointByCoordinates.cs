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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// A single point defined only by a coordinate set with an optional bearing direction.
    /// </summary>
    [XmlType("PointByCoordinates", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class PointByCoordinates(PointCoordinates  PointCoordinates,
                                    AngleInDegrees?   Bearing                       = null,
                                    XElement?         PointByCoordinatesExtension   = null)
    {

        #region Properties

        /// <summary>
        /// The coordinates that define the point.
        /// </summary>
        [XmlElement("pointCoordinates",              Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PointCoordinates  PointCoordinates               { get; } = PointCoordinates;

        /// <summary>
        /// A bearing at the point measured in degrees (0 - 359). Unless otherwise specified the reference 
        /// direction corresponding to 0 degrees is North. Values increase in the clockwise direction.
        /// </summary>
        [XmlElement("bearing",                       Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public AngleInDegrees?   Bearing                        { get; } = Bearing;

        /// <summary>
        /// Optional extension element for additional point by coordinates information.
        /// </summary>
        [XmlElement("_pointByCoordinatesExtension",  Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public XElement?         PointByCoordinatesExtension    { get; } = PointByCoordinatesExtension;

        #endregion


        #region TryParseXML(XML, out PointByCoordinates, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a point by coordinates.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="PointByCoordinates">The parsed point by coordinates.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                      XML,
                                          [NotNullWhen(true)]  out PointByCoordinates?  PointByCoordinates,
                                          [NotNullWhen(false)] out String?              ErrorResponse)
        {

            PointByCoordinates = null;

            if (!XML.TryParseMandatoryXML(DatexIINS.LocationReferencing + "pointCoordinates",
                                          "point coordinates",
                                          LocationReferencing.PointCoordinates.TryParseXML,
                                          out PointCoordinates? pointCoordinates,
                                          out ErrorResponse))
            {
                return false;
            }

            if (!XML.TryParseOptionalValue(DatexIINS.LocationReferencing + "bearing",
                                           "bearing",
                                           DatexIIXMLExtensions.TryParseUInt16,
                                           out UInt16? bearing,
                                           out ErrorResponse))
            {
                return false;
            }

            PointByCoordinates = new PointByCoordinates(
                                     pointCoordinates,
                                     bearing.HasValue ? new AngleInDegrees(bearing.Value) : null,
                                     XML.Element(DatexIINS.LocationReferencing + "_pointByCoordinatesExtension")
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

            => new (XMLName ?? DatexIINS.LocationReferencing + "pointByCoordinates",

                   Bearing.HasValue
                       ? new XElement(DatexIINS.LocationReferencing + "bearing",   Bearing.Value.Value)
                       : null,

                   PointCoordinates.ToXML(DatexIINS.LocationReferencing + "pointCoordinates"),

                   PointByCoordinatesExtension is not null
                       ? PointByCoordinatesExtension.ToExtensionXML(DatexIINS.LocationReferencing + "_pointByCoordinatesExtension")
                       : null

               );

        #endregion

    }

}
