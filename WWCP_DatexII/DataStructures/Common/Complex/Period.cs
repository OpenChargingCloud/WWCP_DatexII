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
    /// A continuous time period or a set of discontinuous time periods defined by the
    /// intersection of a set of criteria all within an overall delimiting interval.
    /// </summary>
    [XmlType("Period", Namespace = "http://datex2.eu/schema/3/common")]
    public class Period(DateTime?                      StartOfPeriod                 = null,
                        DateTime?                      EndOfPeriod                   = null,
                        MultilingualString?            PeriodName                    = null,
                        IEnumerable<TimePeriodOfDay>?  RecurringTimePeriodOfDay      = null,
                        IEnumerable<DayWeekMonth>?     RecurringDayWeekMonthPeriod   = null,
                        IEnumerable<SpecialDay>?       RecurringSpecialDay           = null)
    {

        /// <summary>
        /// Start of period.
        /// </summary>
        [XmlElement("startOfPeriod", Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime?                     StartOfPeriod                  { get; set; } = StartOfPeriod;

        /// <summary>
        /// End of a period.
        /// </summary>
        [XmlElement("endOfPeriod", Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime?                     EndOfPeriod                    { get; set; } = EndOfPeriod;

        /// <summary>
        /// The name of the period.
        /// </summary>
        [XmlElement("periodName", Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?           PeriodName                     { get; set; } = PeriodName;

        /// <summary>
        /// A recurring period of a day.
        /// </summary>
        [XmlElement("recurringTimePeriodOfDay", Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<TimePeriodOfDay>  RecurringTimePeriodOfDay       { get; set; } = RecurringTimePeriodOfDay?.   Distinct() ?? [];

        /// <summary>
        /// A recurring period defined in terms of days of the week, weeks of the month and months of the year.
        /// </summary>
        [XmlElement("recurringDayWeekMonthPeriod", Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<DayWeekMonth>     RecurringDayWeekMonthPeriod    { get; set; } = RecurringDayWeekMonthPeriod?.Distinct() ?? [];

        /// <summary>
        /// A recurring period in terms of special days.
        /// </summary>
        [XmlElement("recurringSpecialDay", Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<SpecialDay>       RecurringSpecialDay            { get; set; } = RecurringSpecialDay?.        Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional period information.
        /// </summary>
        [XmlElement("_periodExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                     PeriodExtension                { get; set; }

    }

}
