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
    /// Charging mode according to IEC 61851-1 terminology
    /// </summary>
    public enum ChargingModes
    {

        /// <summary>
        /// Mode 1, AC 1 phase.
        /// </summary>
        [XmlEnum("mode1AC1p")]
        Mode1AC1p,

        /// <summary>
        /// Mode 1, AC 3 phases.
        /// </summary>
        [XmlEnum("mode1AC3p")]
        Mode1AC3p,

        /// <summary>
        /// Mode 2, AC 1 phase.
        /// </summary>
        [XmlEnum("mode2AC1p")]
        Mode2AC1p,

        /// <summary>
        /// Mode 2, AC 3 phases.
        /// </summary>
        [XmlEnum("mode2AC3p")]
        Mode2AC3p,

        /// <summary>
        /// Mode 3, AC 3 phases.
        /// </summary>
        [XmlEnum("mode3AC3p")]
        Mode3AC3p,

        /// <summary>
        /// Mode 4, DC.
        /// </summary>
        [XmlEnum("mode4DC")]
        Mode4DC,

        /// <summary>
        /// Legacy-Inductive.
        /// </summary>
        [XmlEnum("legacyInductive")]
        LegacyInductive,

        /// <summary>
        /// Charging with a combined charging solution (CCS). AC and DC are used simultaneously.
        /// </summary>
        [XmlEnum("ccs")]
        Ccs,

        /// <summary>
        /// Some other charging mode.
        /// </summary>
        [XmlEnum("other")]
        Other,

        /// <summary>
        /// The type of the charging mode is unknown.
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        [XmlEnum("_extended")]
        Extended

    }

}
