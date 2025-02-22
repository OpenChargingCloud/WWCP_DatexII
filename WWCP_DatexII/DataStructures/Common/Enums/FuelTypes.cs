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
    /// Type of fuel used by a vehicle.
    /// </summary>
    public enum FuelTypes
    {

        /// <summary>
        /// All sort of fuel is accepted.
        /// </summary>
        [XmlEnum("all")]
        All,

        /// <summary>
        /// Battery.
        /// </summary>
        [XmlEnum("battery")]
        Battery,

        /// <summary>
        /// Biodiesel.
        /// </summary>
        [XmlEnum("biodiesel")]
        Biodiesel,

        /// <summary>
        /// Fuel used for compression-ignition (CI) engines.
        /// </summary>
        [XmlEnum("diesel")]
        Diesel,

        /// <summary>
        /// Diesel and battery hybrid.
        /// </summary>
        [XmlEnum("dieselBatteryHybrid")]
        DieselBatteryHybrid,

        /// <summary>
        /// Ethanol.
        /// </summary>
        [XmlEnum("ethanol")]
        Ethanol,

        /// <summary>
        /// Hydrogen.
        /// </summary>
        [XmlEnum("hydrogen")]
        Hydrogen,

        /// <summary>
        /// Liquid gas of any type including LPG.
        /// </summary>
        [XmlEnum("liquidGas")]
        LiquidGas,

        /// <summary>
        /// Liquid petroleum gas.
        /// </summary>
        [XmlEnum("lpg")]
        Lpg,

        /// <summary>
        /// Methane gas.
        /// </summary>
        [XmlEnum("methane")]
        Methane,

        /// <summary>
        /// Fuel used for positive-ignition (PI) engines.
        /// </summary>
        [XmlEnum("petrol")]
        Petrol,

        /// <summary>
        /// Petrol with 95 octane.
        /// </summary>
        [XmlEnum("petrol95Octane")]
        Petrol95Octane,

        /// <summary>
        /// Petrol with 98 octane.
        /// </summary>
        [XmlEnum("petrol98Octane")]
        Petrol98Octane,

        /// <summary>
        /// Petrol and battery hybrid.
        /// </summary>
        [XmlEnum("petrolBatteryHybrid")]
        PetrolBatteryHybrid,

        /// <summary>
        /// Leaded petrol.
        /// </summary>
        [XmlEnum("petrolLeaded")]
        PetrolLeaded,

        /// <summary>
        /// Unleaded petrol.
        /// </summary>
        [XmlEnum("petrolUnleaded")]
        PetrolUnleaded,

        /// <summary>
        /// The sort of fuel is not known.
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        /// <summary>
        /// Other.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
