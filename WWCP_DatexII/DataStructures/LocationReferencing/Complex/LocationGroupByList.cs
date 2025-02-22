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
    /// A group of (i.e. more than one) physically separate locations which have
    /// no specific order and where each location is explicitly listed.
    /// </summary>
    [XmlType("LocationGroupByList", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class LocationGroupByList(IEnumerable<ALocation> LocationContainedInGroup)

        : ALocationGroup

    {
        /// <summary>
        /// A location contained in a non-ordered group of locations.
        /// Must have at least two locations.
        /// </summary>
        [XmlElement("locationContainedInGroup",       Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public IEnumerable<ALocation>  LocationContainedInGroup        { get; set; } = LocationContainedInGroup.Distinct();

        /// <summary>
        /// Optional extension element for additional LocationGroupByList information.
        /// </summary>
        [XmlElement("_locationGroupByListExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?               LocationGroupByListExtension    { get; set; }

    }

}
