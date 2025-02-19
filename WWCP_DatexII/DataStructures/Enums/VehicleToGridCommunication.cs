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
    /// Communication between vehicles and the grid.
    /// </summary>
    public enum VehicleToGridCommunication
    {

        /// <summary>
        /// No communication between vehicle and the grid.
        /// </summary>
        [XmlEnum("none")]
        None,

        /// <summary>
        /// Communication according to ISO15118.
        /// </summary>
        [XmlEnum("iso15118")]
        Iso15118,

        /// <summary>
        /// Communication according to IEC/TS 61980-2.
        /// </summary>
        [XmlEnum("iec619802")]
        Iec619802,

        /// <summary>
        /// Communication according to other guidelines/specifications.
        /// </summary>
        [XmlEnum("other")]
        Other,

        /// <summary>
        /// The type of communication is unknown.
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        [XmlEnum("_extended")]
        Extended

    }

}
