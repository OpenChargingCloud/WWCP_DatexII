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

using System.Xml.Serialization;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// A continuous or discontinuous period of validity defined by overall bounding start and end times and
    /// the possible intersection of valid periods (potentially recurring) with the complement of exception
    /// periods (also potentially recurring).
    /// </summary>
    [XmlType("OverallPeriod", Namespace = "http://datex2.eu/schema/3/common")]
    public class OverallPeriod(DateTime              OverallStartTime,
                               DateTime?             OverallEndTime    = null,
                               IEnumerable<Period>?  ValidPeriod       = null,
                               IEnumerable<Period>?  ExceptionPeriod   = null)
    {

        /// <summary>
        /// Start of bounding period of validity defined by date and time.
        /// </summary>
        [XmlElement("overallStartTime", Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime             OverallStartTime    { get; set; } = OverallStartTime;

        /// <summary>
        /// End of bounding period of validity defined by date and time.
        /// </summary>
        [XmlElement("overallEndTime", Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime?            OverallEndTime      { get; set; } = OverallEndTime;

        /// <summary>
        /// A single time period, a recurring time period or a set of different recurring time periods during which validity is true.
        /// </summary>
        [XmlElement("validPeriod", Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Period>  ValidPeriod         { get; set; } = ValidPeriod     ?? [];

        /// <summary>
        /// A single time period, a recurring time period or a set of different recurring time periods during which validity is false.
        /// </summary>
        [XmlElement("exceptionPeriod", Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Period>  ExceptionPeriod     { get; set; } = ExceptionPeriod ?? [];

        ///// <summary>
        ///// Optional extension element for additional overall period information.
        ///// </summary>
        //[XmlElement("_overallPeriodExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? OverallPeriodExtension { get; set; }

    }

}
