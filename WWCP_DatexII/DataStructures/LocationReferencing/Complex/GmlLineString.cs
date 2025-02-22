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
    /// Line string based on GML (EN ISO 19136) definition: a curve defined by a series of two or more coordinate tuples.
    /// Unlike standard GML, this implementation may allow self-intersecting geometries.
    /// If the srsName attribute is not present, posList is assumed to use the "ETRS89-LatLonh" reference system.
    /// </summary>
    [XmlType("GmlLineString", Namespace = "http://datex2.eu/schema/3/locationExtension")]
    public class GmlLineString(GmlPosList  PosList,
                               UInt32?     SrsDimension   = null,
                               String?     SrsName        = null)
    {

        /// <summary>
        /// List of coordinate tuples defining the geometry of this GmlLineString.
        /// There must be at least 2 tuples of coordinates. The typical form is a space-separated list of latitude and longitude pairs.
        /// </summary>
        [XmlElement("posList",                  Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public GmlPosList  PosList                   { get; set; } = PosList;

        /// <summary>
        /// Provides the size of the tuple of coordinates of each point.
        /// This number is typically 2 or 3. By default, when omitted, the dimension shall be interpreted as 2.
        /// </summary>
        [XmlAttribute("srsDimension")]
        public UInt32?     SrsDimension              { get; set; } = SrsDimension;

        /// <summary>
        /// Specifies the Coordinate Reference System (CRS) used to interpret the coordinates in this GmlLineString.
        /// </summary>
        [XmlAttribute("srsName")]
        public String?     SrsName                   { get; set; } = SrsName;

        /// <summary>
        /// Optional extension element for additional line string information.
        /// </summary>
        [XmlElement("_gmlLineStringExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?   GmlLineStringExtension    { get; set; }

    }

}
