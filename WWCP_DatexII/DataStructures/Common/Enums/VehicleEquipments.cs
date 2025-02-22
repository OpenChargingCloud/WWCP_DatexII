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
    /// Types of vehicle equipment in use or on board.
    /// </summary>
    public enum VehicleEquipments
    {

        /// <summary>
        /// Vehicle not using snow chains.
        /// </summary>
        [XmlEnum("notUsingSnowChains")]
        NotUsingSnowChains,

        /// <summary>
        /// Vehicle not using either snow tyres or snow chains.
        /// </summary>
        [XmlEnum("notUsingSnowChainsOrTyres")]
        NotUsingSnowChainsOrTyres,

        /// <summary>
        /// Vehicle using snow chains.
        /// </summary>
        [XmlEnum("snowChainsInUse")]
        SnowChainsInUse,

        /// <summary>
        /// Vehicle using snow tyres.
        /// </summary>
        [XmlEnum("snowTyresInUse")]
        SnowTyresInUse,

        /// <summary>
        /// Vehicle using snow tyres or snow chains.
        /// </summary>
        [XmlEnum("snowChainsOrTyresInUse")]
        SnowChainsOrTyresInUse,

        /// <summary>
        /// Vehicle which is not carrying on board snow tyres or chains.
        /// </summary>
        [XmlEnum("withoutSnowTyresOrChainsOnBoard")]
        WithoutSnowTyresOrChainsOnBoard,

        [XmlEnum("_extended")]
        Extended

    }

}
