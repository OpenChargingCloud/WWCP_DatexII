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
    /// Logical comparison operations.
    /// </summary>
    public enum ComparisonOperators
    {

        /// <summary>
        /// Logical comparison operator of "equal to".
        /// </summary>
        [XmlEnum("equalTo")]
        EqualTo,

        /// <summary>
        /// Logical comparison operator of "greater than".
        /// </summary>
        [XmlEnum("greaterThan")]
        GreaterThan,

        /// <summary>
        /// Logical comparison operator of "greater than or equal to".
        /// </summary>
        [XmlEnum("greaterThanOrEqualTo")]
        GreaterThanOrEqualTo,

        /// <summary>
        /// Logical comparison operator of "less than".
        /// </summary>
        [XmlEnum("lessThan")]
        LessThan,

        /// <summary>
        /// Logical comparison operator of "less than or equal to".
        /// </summary>
        [XmlEnum("lessThanOrEqualTo")]
        LessThanOrEqualTo,

        [XmlEnum("_extended")]
        Extended

    }

}
