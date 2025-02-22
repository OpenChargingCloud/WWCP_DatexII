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
    /// An area defined by a set of polygons according to GML (EN ISO 19136).
    /// </summary>
    [XmlType("GmlMultiPolygon", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class GmlMultiPolygon(MultilingualString?       GmlAreaName   = null,
                                 IEnumerable<GmlPolygon>?  GmlPolygons   = null)
    {

        /// <summary>
        /// Name of the multi-polygon area.
        /// </summary>
        [XmlElement("gmlAreaName",                Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?      GmlAreaName                 { get; set; } = GmlAreaName;

        /// <summary>
        /// A collection of GML polygons that define the area.
        /// </summary>
        [XmlElement("gmlPolygon",                 Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public IEnumerable<GmlPolygon>  GmlPolygons                 { get; set; } = GmlPolygons?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional multi-polygon information.
        /// </summary>
        [XmlElement("_gmlMultiPolygonExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                GmlMultiPolygonExtension    { get; set; }

    }

}
