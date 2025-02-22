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
    /// A table of different service levels to be expected for fuelling/charging and payment.
    /// </summary>
    public enum ServiceTypes
    {

        /// <summary>
        /// Presence of physical persons attending the recharging or refuelling station.
        /// </summary>
        [XmlEnum("pyhsicalAttendance")]
        PyhsicalAttendance,

        /// <summary>
        /// Unattended station, fuelling and payment to be done without assistance.
        /// </summary>
        [XmlEnum("unattended")]
        Unattended,

        [XmlEnum("_extended")]
        Extended

    }

}
