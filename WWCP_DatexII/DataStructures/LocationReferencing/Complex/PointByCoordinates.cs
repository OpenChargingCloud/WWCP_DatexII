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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// A single point defined only by a coordinate set with an optional bearing direction.
    /// </summary>
    [XmlType("PointByCoordinates", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class PointByCoordinates(PointCoordinates  PointCoordinates,
                                    AngleInDegrees?   Bearing   = null)
    {

        /// <summary>
        /// The coordinates that define the point.
        /// </summary>
        [XmlElement("pointCoordinates",              Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PointCoordinates  PointCoordinates               { get; set; } = PointCoordinates;

        /// <summary>
        /// A bearing at the point measured in degrees (0 - 359). Unless otherwise specified the reference 
        /// direction corresponding to 0 degrees is North. Values increase in the clockwise direction.
        /// </summary>
        [XmlElement("bearing",                       Namespace = "http://datex2.eu/schema/3/common")]
        public AngleInDegrees?   Bearing                        { get; set; } = Bearing;

        /// <summary>
        /// Optional extension element for additional point by coordinates information.
        /// </summary>
        [XmlElement("_pointByCoordinatesExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?         PointByCoordinatesExtension    { get; set; }

    }

}
