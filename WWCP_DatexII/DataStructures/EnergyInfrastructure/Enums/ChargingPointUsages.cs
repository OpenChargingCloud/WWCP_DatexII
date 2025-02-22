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

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Type of usage for an electric charging point
    /// </summary>
    public enum ChargingPointUsages
    {

        /// <summary>
        /// Charging of electric boats.
        /// </summary>
        [XmlEnum("electricBoat")]
        ElectricBoat,

        /// <summary>
        /// Charging of E-Bikes.
        /// </summary>
        [XmlEnum("electricBike")]
        ElectricBike,

        /// <summary>
        /// Provides a plug for electrical devices (e.g. shaver, mobile phones, hair dryer, ...).
        /// </summary>
        [XmlEnum("electricalDevices")]
        ElectricalDevices,

        /// <summary>
        /// Charging of E-Motorcycles.
        /// </summary>
        [XmlEnum("electricMotorcycle")]
        ElectricMotorcycle,

        /// <summary>
        /// Charging of electric vehicles.
        /// </summary>
        [XmlEnum("electricVehicle")]
        ElectricVehicle,

        /// <summary>
        /// Supply for lorries with power consumption, e.g. for refrigerated goods transports.
        /// </summary>
        [XmlEnum("lorryPowerConsumption")]
        LorryPowerConsumption,

        /// <summary>
        /// Supply for motorhomes or caravans.
        /// </summary>
        [XmlEnum("motorhomeOrCaravanSupply")]
        MotorhomeOrCaravanSupply,

        /// <summary>
        /// The charging point supplies an overhead line, usually connected via pantographs.
        /// </summary>
        [XmlEnum("overheadLineDrivenVehicles")]
        OverheadLineDrivenVehicles,

        /// <summary>
        /// Other usage for the electric charging stations.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
