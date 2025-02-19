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
    /// The type of price for energy.
    /// </summary>
    public enum PriceTypes
    {

        /// <summary>
        /// The given price is per minute of charging.
        /// </summary>
        [XmlEnum("pricePerMinute")]
        PricePerMinute,

        /// <summary>
        /// The given price is per kWh of electric energy.
        /// </summary>
        [XmlEnum("pricePerKWh")]
        PricePerKWh,

        /// <summary>
        /// The given price is a base price, which has to be added.
        /// </summary>
        [XmlEnum("basePrice")]
        BasePrice,

        /// <summary>
        /// The given price is a flatrate price, independent of the fueled amount.
        /// </summary>
        [XmlEnum("flatRate")]
        FlatRate,

        /// <summary>
        /// Charging or refueling is free.
        /// </summary>
        [XmlEnum("free")]
        Free,

        /// <summary>
        /// Other method of pricing.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
