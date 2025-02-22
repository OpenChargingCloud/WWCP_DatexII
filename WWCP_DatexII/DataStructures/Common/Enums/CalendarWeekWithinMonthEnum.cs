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

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Calendar week within month (see ISO 8601).
    /// </summary>
    public enum CalendarWeekWithinMonthEnum
    {

        /// <summary>
        /// Calendar week containing the first of the month. Several days of the first week may occur in the previous calendar month.
        /// By construction, the last week of a preceding month can also be the first week of a subsequent month.
        /// </summary>
        [XmlEnum("firstWeek")]
        FirstWeek,

        /// <summary>
        /// Second week of the month.
        /// </summary>
        [XmlEnum("secondWeek")]
        SecondWeek,

        /// <summary>
        /// Third week of the month.
        /// </summary>
        [XmlEnum("thirdWeek")]
        ThirdWeek,

        /// <summary>
        /// Fourth week of the month.
        /// </summary>
        [XmlEnum("fourthWeek")]
        FourthWeek,

        /// <summary>
        /// Fifth week of the month.
        /// </summary>
        [XmlEnum("fifthWeek")]
        FifthWeek,

        /// <summary>
        /// Sixth week of the month.
        /// </summary>
        [XmlEnum("sixthWeek")]
        SixthWeek,

        /// <summary>
        /// Last calendar week within the month, regardless of its actual number. The last week is the week beginning with Monday 
        /// and containing the last day of the month.
        /// </summary>
        [XmlEnum("lastWeek")]
        LastWeek,

        /// <summary>
        /// Extended value for additional or non-standard cases.
        /// </summary>
        [XmlEnum("_extended")]
        Extended

    }

}
