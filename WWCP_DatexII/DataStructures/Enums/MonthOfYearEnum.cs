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
    /// Placeholder enum for com:_MonthOfYearEnum. Define as needed.
    /// </summary>
    public enum MonthOfYearEnum
    {

        [XmlEnum("january")]
        January,

        [XmlEnum("february")]
        February,

        [XmlEnum("march")]
        March,

        [XmlEnum("april")]
        April,

        [XmlEnum("may")]
        May,

        [XmlEnum("june")]
        June,

        [XmlEnum("july")]
        July,

        [XmlEnum("august")]
        August,

        [XmlEnum("september")]
        September,

        [XmlEnum("october")]
        October,

        [XmlEnum("november")]
        November,

        [XmlEnum("december")]
        December,

        [XmlEnum("_extended")]
        Extended

    }

}
