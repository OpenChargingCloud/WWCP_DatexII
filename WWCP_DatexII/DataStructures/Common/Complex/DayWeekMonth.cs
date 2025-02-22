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
    /// Specification of periods defined by the intersection of days or instances of them, calendar weeks and months.
    /// </summary>
    [XmlType("DayWeekMonth", Namespace = "http://datex2.eu/schema/3/common")]
    public class DayWeekMonth(IEnumerable<Days>?         ApplicableDay     = null,
                              IEnumerable<MonthOfYear>?  ApplicableMonth   = null)
    {

        /// <summary>
        /// Applicable day of the week.
        /// "All days of the week" is expressed by non-inclusion of this element.
        /// </summary>
        [XmlElement("applicableDay", Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Days>         ApplicableDay            { get; set; } = ApplicableDay?.  Distinct() ?? [];

        /// <summary>
        /// Applicable month of the year.
        /// "All months of the year" is expressed by non-inclusion of this element.
        /// </summary>
        [XmlElement("applicableMonth", Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<MonthOfYear>  ApplicableMonth          { get; set; } = ApplicableMonth?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional information.
        /// </summary>
        [XmlElement("_dayWeekMonthExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                 DayWeekMonthExtension    { get; set; }

    }

}
