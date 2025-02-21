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
    /// The status of a refill point.
    /// </summary>
    public enum RefillPointStatusEnum
    {

        /// <summary>
        /// The refill point is not occupied, has got enough energy resources and can be used.
        /// </summary>
        [XmlEnum("available")]
        Available,

        /// <summary>
        /// The refill point is not accessible because of a physical barrier, e.g. a car.
        /// </summary>
        [XmlEnum("blocked")]
        Blocked,

        /// <summary>
        /// The refill point is currently in use for charging.
        /// </summary>
        [XmlEnum("charging")]
        Charging,

        /// <summary>
        /// The refill point has got a fault.
        /// </summary>
        [XmlEnum("faulted")]
        Faulted,

        /// <summary>
        /// The refill point is not yet active or it is no longer available (deleted).
        /// </summary>
        [XmlEnum("inoperative")]
        Inoperative,

        /// <summary>
        /// The refill point is in use, this might include vehicle charging activity.
        /// </summary>
        [XmlEnum("occupied")]
        Occupied,

        /// <summary>
        /// The refill point is currently out of order.
        /// </summary>
        [XmlEnum("outOfOrder")]
        OutOfOrder,

        /// <summary>
        /// The refill point is out of stock, i.e. energy resources are empty.
        /// </summary>
        [XmlEnum("outOfStock")]
        OutOfStock,

        /// <summary>
        /// The refill point is planned, will be operating soon.
        /// </summary>
        [XmlEnum("planned")]
        Planned,

        /// <summary>
        /// The refill point was discontinued/removed.
        /// </summary>
        [XmlEnum("removed")]
        Removed,

        /// <summary>
        /// The refill point is reserved by a customer, i.e. it is not available for other users right now.
        /// </summary>
        [XmlEnum("reserved")]
        Reserved,

        /// <summary>
        /// There is no energy available at this refill point.
        /// </summary>
        [XmlEnum("unavailable")]
        Unavailable,

        /// <summary>
        /// The status of the refill point is unknown (can also be offline).
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        [XmlEnum("_extended")]
        Extended

    }

}
