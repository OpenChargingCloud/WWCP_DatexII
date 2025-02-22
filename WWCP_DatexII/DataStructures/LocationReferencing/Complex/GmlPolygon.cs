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
    /// Planar surface defined by one exterior boundary and zero or more interior boundaries.
    /// </summary>
    [XmlType("GmlPolygon", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class GmlPolygon(GmlLinearRing                Exterior,
                            IEnumerable<GmlLinearRing>?  Interior   = null)
    {

        /// <summary>
        /// A boundary of a polygonal surface consisting of a ring, i.e. in the normal 2D case, a closed polygonal line distinguished as exterior.
        /// Such a polygonal line must have at least 4 pairs of coordinates.
        /// </summary>
        [XmlElement("exterior",              Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public GmlLinearRing                Exterior              { get; set; } = Exterior;

        /// <summary>
        /// A boundary of internal patches of a polygonal surface consisting of a ring feature.
        /// </summary>
        [XmlElement("interior",              Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public IEnumerable<GmlLinearRing>  Interior               { get; set; } = Interior?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional polygon information.
        /// </summary>
        [XmlElement("_gmlPolygonExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                   GmlPolygonExtension    { get; set; }

    }

}
