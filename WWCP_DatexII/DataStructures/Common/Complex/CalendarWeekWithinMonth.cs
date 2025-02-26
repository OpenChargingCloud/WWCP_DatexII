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
    /// Specification of periods defined by relevant calendar weeks in a month, according to ISO8601.
    /// Note: Calendar weeks start with Monday. The first week is the week containing the first day of the month.
    /// </summary>
    [XmlType("CalendarWeekWithinMonth", Namespace = "http://datex2.eu/schema/3/common")]
    public class CalendarWeekWithinMonth(IEnumerable<CalendarWeekWithinMonthEnum>?  ApplicableCalenderWeekWithinMonth   = null,
                                         XElement?                                  CalendarWeekWithinMonthExtension    = null,

                                         IEnumerable<Day>?                          ApplicableDays                      = null,
                                         IEnumerable<MonthOfYear>?                  ApplicableMonths                    = null,
                                         XElement?                                  DayWeekMonthExtension               = null)

        : DayWeekMonth(ApplicableDays,
                       ApplicableMonths,
                       DayWeekMonthExtension)

    {

        /// <summary>
        /// Calendar week in month.
        /// "All weeks of the month" is expressed by not using the CalendarWeekWithinMonth class.
        /// Note: Calendar weeks start with Monday. The first week is the week containing the first day of the month.
        /// Must have at least one and no more than six values.
        /// </summary>
        [XmlElement("applicableCalenderWeekWithinMonth", Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<CalendarWeekWithinMonthEnum>  ApplicableCalenderWeekWithinMonth    { get; set; } = ApplicableCalenderWeekWithinMonth?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional information.
        /// </summary>
        [XmlElement("_calendarWeekWithinMonthExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                 CalendarWeekWithinMonthExtension     { get; set; } = CalendarWeekWithinMonthExtension;

    }

}
