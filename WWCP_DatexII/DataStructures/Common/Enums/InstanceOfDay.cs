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
    /// Instances of a day of the week in a month.
    /// </summary>
    public enum InstanceOfDay
    {

        /// <summary>
        /// First instance of specified day of week in month.
        /// </summary>
        [XmlEnum("firstInstance")]
        FirstInstance,

        /// <summary>
        /// Second instance of specified day of week in month.
        /// </summary>
        [XmlEnum("secondInstance")]
        SecondInstance,

        /// <summary>
        /// Third instance of specified day of week in month.
        /// </summary>
        [XmlEnum("thirdInstance")]
        ThirdInstance,

        /// <summary>
        /// Fourth instance of specified day of week in month.
        /// </summary>
        [XmlEnum("fourthInstance")]
        FourthInstance,

        /// <summary>
        /// Fifth instance of specified day of week in month.
        /// </summary>
        [XmlEnum("fifthInstance")]
        FifthInstance,

        /// <summary>
        /// Last instance of specified day of week in month (regardless of its actual instance number).
        /// </summary>
        [XmlEnum("lastInstance")]
        LastInstance,

        /// <summary>
        /// Extended value for non-standard instances.
        /// </summary>
        [XmlEnum("_extended")]
        Extended

    }

}
