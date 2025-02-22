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
    /// The emission level of a vehicle.
    /// </summary>
    public enum LowEmissionLevels
    {

        /// <summary>
        /// Vehicles with low level emission.
        /// </summary>
        [XmlEnum("lowLevelEmission")]
        LowLevelEmission,

        /// <summary>
        /// Only vehicles that do not produce emissions (e.g. electric driven). Hybrid driven cars are allowed, when they switch to emission free mode within the considered situation.
        /// </summary>
        [XmlEnum("freeOfEmission")]
        FreeOfEmission,

        [XmlEnum("_extended")]
        Extended

    }

}
