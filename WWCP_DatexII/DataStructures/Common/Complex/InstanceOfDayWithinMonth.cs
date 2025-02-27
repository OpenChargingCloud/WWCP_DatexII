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
    /// Specification of periods defined by the instance of a specific weekday within a month (e.g. 3rd Tuesday in May).
    /// </summary>
    [XmlType("InstanceOfDayWithinMonth", Namespace = "http://datex2.eu/schema/3/common")]
    public class InstanceOfDayWithinMonth(IEnumerable<InstanceOfDay>  ApplicableInstanceOfDayWithinMonth,
                                          XElement?                   InstanceOfDayWithinMonthExtension   = null,

                                          IEnumerable<Day>?           ApplicableDays                      = null,
                                          IEnumerable<MonthOfYear>?   ApplicableMonths                    = null,
                                          XElement?                   DayWeekMonthExtension               = null)

        : DayWeekMonth(ApplicableDays,
                       ApplicableMonths,
                       DayWeekMonthExtension)

    {

        #region Properties

        /// <summary>
        /// The specified instance(s) of the day of week within the month.
        /// At least one and at most five instances must be provided.
        /// </summary>
        [XmlElement("applicableInstanceOfDayWithinMonth",  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<InstanceOfDay>  ApplicableInstanceOfDayWithinMonth    { get; } = ApplicableInstanceOfDayWithinMonth.Distinct();

        /// <summary>
        /// Optional extension element for additional instance-of-day-within-month information.
        /// </summary>
        [XmlElement("_instanceOfDayWithinMonthExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                   InstanceOfDayWithinMonthExtension     { get; } = InstanceOfDayWithinMonthExtension;

        #endregion

    }

}
