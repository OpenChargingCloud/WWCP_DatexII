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

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Specification of a special type of day, possibly also a public holiday. Can be country or region specific.
    /// </summary>
    [XmlType("SpecialDay", Namespace = "http://datex2.eu/schema/3/common")]
    public class SpecialDay(Boolean                   IntersectWithApplicableDays,
                            SpecialDayTypes           SpecialDayType,
                            PublicEventTypes?         PublicEvent   = null,
                            IEnumerable<ANamedArea>?  NamedAreas    = null)
    {

        /// <summary>
        /// When true, the period is the intersection of applicable days and this special day.
        /// When false, the period is the union of applicable days and this special day.
        /// </summary>
        [XmlElement("intersectWithApplicableDays",  Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean                  IntersectWithApplicableDays    { get; set; } = IntersectWithApplicableDays;

        /// <summary>
        /// Specification of a special day, for example schoolDay, publicHoliday, etc.
        /// </summary>
        [XmlElement("specialDayType",               Namespace = "http://datex2.eu/schema/3/common")]
        public SpecialDayTypes          SpecialDayType                 { get; set; } = SpecialDayType;

        /// <summary>
        /// Type of public event on this special day.
        /// </summary>
        [XmlElement("publicEvent",                  Namespace = "http://datex2.eu/schema/3/common")]
        public PublicEventTypes?        PublicEvent                    { get; set; } = PublicEvent;

        /// <summary>
        /// One or more named areas (e.g. regions or localities) to which this special day applies.
        /// </summary>
        [XmlElement("namedArea",                    Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<ANamedArea>  NamedAreas                     { get; set; } = NamedAreas?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional special day information.
        /// </summary>
        [XmlElement("_specialDayExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                SpecialDayExtension            { get; set; }

    }

}
