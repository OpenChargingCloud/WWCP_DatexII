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

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// Coded value for type of height.
    /// </summary>
    public enum HeightTypes
    {

        /// <summary>
        /// Value measured vertically above the reference ellipsoid.
        /// </summary>
        [XmlEnum("ellipsoidalHeight")]
        EllipsoidalHeight,

        /// <summary>
        /// Height type corresponding to a value measured along the direction of gravity above the reference geoid.
        /// </summary>
        [XmlEnum("gravityRelatedHeight")]
        GravityRelatedHeight,

        /// <summary>
        /// Height type corresponding to a value measured vertically above the ground level at this point.
        /// </summary>
        [XmlEnum("relativeHeight")]
        RelativeHeight,

        [XmlEnum("_extended")]
        Extended

    }

}
