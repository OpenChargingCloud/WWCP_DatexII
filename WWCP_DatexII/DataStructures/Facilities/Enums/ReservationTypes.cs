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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Different types of reservation.
    /// </summary>
    public enum ReservationTypes
    {

        /// <summary>
        /// Reservation is optional.
        /// </summary>
        [XmlEnum("optional")]
        Optional,

        /// <summary>
        /// Reservation is mandatory.
        /// </summary>
        [XmlEnum("mandatory")]
        Mandatory,

        /// <summary>
        /// Reservation is not possible.
        /// </summary>
        [XmlEnum("notAvailable")]
        NotAvailable,

        /// <summary>
        /// There is a specific contingent that can be reserved.
        /// </summary>
        [XmlEnum("partly")]
        Partly,

        /// <summary>
        /// Possibility of reservation is unknown.
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        /// <summary>
        /// Possibility of reservation is not specified.
        /// </summary>
        [XmlEnum("unspecified")]
        Unspecified,

        [XmlEnum("_extended")]
        Extended

    }

}
