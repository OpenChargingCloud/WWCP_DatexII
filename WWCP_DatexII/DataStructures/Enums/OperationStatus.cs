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

    public enum OperationStatus
    {

        /// <summary>
        /// The specified element is in operation right now.
        /// </summary>
        [XmlEnum("inOperation")]
        InOperation,

        /// <summary>
        /// The specified element is in operation on a limited basis.
        /// </summary>
        [XmlEnum("limitedOperation")]
        LimitedOperation,

        /// <summary>
        /// The specified element is not operating right now.
        /// </summary>
        [XmlEnum("notInOperation")]
        NotInOperation,

        /// <summary>
        /// The specified element is not operating due to abnormal conditions (holidays, restoration-works, long-term closure, etc.).
        /// </summary>
        [XmlEnum("notInOperationAbnormal")]
        NotInOperationAbnormal,

        /// <summary>
        /// The specified element is not in operation due to a technical defect.
        /// </summary>
        [XmlEnum("technicalDefect")]
        TechnicalDefect,

        /// <summary>
        /// There is no information about the operation status.
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        [XmlEnum("_extended")]
        Extended

    }

}
