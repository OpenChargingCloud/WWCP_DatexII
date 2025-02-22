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
    /// Specification of a continuous period of time within a 24 hour period.
    /// </summary>
    [XmlType("TimePeriodOfDay", Namespace = "http://datex2.eu/schema/3/common")]
    public class TimePeriodOfDay(String StartTimeOfPeriod,
                                 String EndTimeOfPeriod)
    {

        /// <summary>
        /// Start of time period.
        /// </summary>
        [XmlElement("startTimeOfPeriod",  Namespace = "http://datex2.eu/schema/3/common")]
        public String     StartTimeOfPeriod           { get; set; } = StartTimeOfPeriod;

        /// <summary>
        /// End of time period.
        /// </summary>
        [XmlElement("endTimeOfPeriod",    Namespace = "http://datex2.eu/schema/3/common")]
        public String     EndTimeOfPeriod             { get; set; } = EndTimeOfPeriod;

        /// <summary>
        /// Optional extension element for additional time period information.
        /// </summary>
        [XmlElement("_timePeriodOfDayExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  TimePeriodOfDayExtension    { get; set; }

    }

}
