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

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// Third coordinate for points defined geodetically.
    /// </summary>
    [XmlType("HeightCoordinate", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class HeightCoordinate(Meter                HeightValue,
                                  HeightType?          HeightType                  = null,
                                  AltitudeConfidence?  AltitudeConfidence          = null,
                                  PositionAccuracy?    VerticalPositionAccuracy    = null,
                                  XElement?            HeightCoordinateExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Value in metres for the height measured vertically at the planar coordinates the point corresponds to.
        /// </summary>
        [XmlElement("heightValue",                 Namespace = "http://datex2.eu/schema/3/common")]
        public Meter                HeightValue                  { get; } = HeightValue;

        /// <summary>
        /// Type of measured height. When omitted, it is supposed to be the ellipsoidal height.
        /// </summary>
        [XmlElement("heightType",                  Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public HeightType?          HeightType                   { get; } = HeightType;

        /// <summary>
        /// Altitude confidence.
        /// </summary>
        [XmlElement("altitudeConfidence",          Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public AltitudeConfidence?  AltitudeConfidence           { get; } = AltitudeConfidence;

        /// <summary>
        /// Defines the vertical position accuracy according to EN16803-1.
        /// </summary>
        [XmlElement("verticalPositionAccuracy",    Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PositionAccuracy?    VerticalPositionAccuracy     { get; } = VerticalPositionAccuracy;

        /// <summary>
        /// Optional extension element for additional height coordinate information.
        /// </summary>
        [XmlElement("_heightCoordinateExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            HeightCoordinateExtension    { get; } = HeightCoordinateExtension;

        #endregion

    }

}
