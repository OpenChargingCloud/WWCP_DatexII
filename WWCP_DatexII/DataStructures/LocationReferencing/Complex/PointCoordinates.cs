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

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// A pair of planar coordinates defining the geodetic position of a single point
    /// using the European Terrestrial Reference System 1989 (ETRS89).
    /// </summary>
    [XmlType("PointCoordinates", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class PointCoordinates(Double                          Latitude,
                                  Double                          Longitude,
                                  IEnumerable<HeightCoordinate>?  HeightCoordinates            = null,
                                  PositionConfidenceEllipse?      PositionConfidenceEllipse    = null,
                                  PositionAccuracy?               HorizontalPositionAccuracy   = null,
                                  XElement?                       PointCoordinatesExtension    = null)
    {

        #region Properties

        /// <summary>
        /// Latitude in decimal degrees using ETRS89.
        /// </summary>
        [XmlElement("latitude",                    Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public Double                         Latitude                      { get; } = Latitude;

        /// <summary>
        /// Longitude in decimal degrees using ETRS89.
        /// </summary>
        [XmlElement("longitude",                   Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public Double                         Longitude                     { get; } = Longitude;

        /// <summary>
        /// Optional height coordinate(s); up to 3 occurrences.
        /// </summary>
        [XmlElement("heightCoordinate",            Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public IEnumerable<HeightCoordinate>  HeightCoordinates             { get; } = HeightCoordinates?.Distinct() ?? [];

        /// <summary>
        /// Position confidence ellipse that describes the uncertainty in the position.
        /// </summary>
        [XmlElement("positionConfidenceEllipse",   Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PositionConfidenceEllipse?     PositionConfidenceEllipse     { get; } = PositionConfidenceEllipse;

        /// <summary>
        /// Defines the horizontal position accuracy according to EN 16803-1.
        /// </summary>
        [XmlElement("horizontalPositionAccuracy",  Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PositionAccuracy?              HorizontalPositionAccuracy    { get; } = HorizontalPositionAccuracy;

        /// <summary>
        /// Optional extension element for additional point coordinates information.
        /// </summary>
        [XmlElement("_pointCoordinatesExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                      PointCoordinatesExtension     { get; } = PointCoordinatesExtension;

        #endregion

    }

}
