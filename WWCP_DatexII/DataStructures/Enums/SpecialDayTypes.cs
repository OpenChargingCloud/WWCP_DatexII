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
    /// Collection of special types of days.
    /// </summary>
    public enum SpecialDayTypes
    {

        /// <summary>
        /// The day preceding a public holiday.
        /// </summary>
        [XmlEnum("dayBeforePublicHoliday")]
        DayBeforePublicHoliday,

        /// <summary>
        /// A public holiday in general.
        /// </summary>
        [XmlEnum("publicHoliday")]
        PublicHoliday,

        /// <summary>
        /// A day following a public holiday.
        /// </summary>
        [XmlEnum("dayFollowingPublicHoliday")]
        DayFollowingPublicHoliday,

        /// <summary>
        /// A day between a public holiday and the weekend.
        /// </summary>
        [XmlEnum("longWeekendDay")]
        LongWeekendDay,

        /// <summary>
        /// A holiday in lieu of a public holiday that falls on a weekend.
        /// </summary>
        [XmlEnum("inLieuOfPublicHoliday")]
        InLieuOfPublicHoliday,

        /// <summary>
        /// A school day.
        /// </summary>
        [XmlEnum("schoolDay")]
        SchoolDay,

        /// <summary>
        /// A day within the school holidays.
        /// </summary>
        [XmlEnum("schoolHolidays")]
        SchoolHolidays,

        /// <summary>
        /// A day of a public event.
        /// </summary>
        [XmlEnum("publicEventDay")]
        PublicEventDay,

        /// <summary>
        /// Some other special day.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
