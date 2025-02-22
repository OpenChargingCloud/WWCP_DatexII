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
    /// Information on accessibility and facilities for persons with disabilities.
    /// </summary>
    public enum Accessibilities
    {

        /// <summary>
        /// Accessible without any steps or other barriers.
        /// </summary>
        [XmlEnum("barrierFreeAccessible")]
        BarrierFreeAccessible,

        /// <summary>
        /// Accessible for persons with disabilities. Wheelchair accessible is a special form of it.
        /// </summary>
        [XmlEnum("disabilityAccessible")]
        DisabilityAccessible,

        /// <summary>
        /// Accessible by people in a wheelchair.
        /// </summary>
        [XmlEnum("wheelChairAccessible")]
        WheelChairAccessible,

        /// <summary>
        /// There are special facilities for persons with disabilities, like handrails or special furniture.
        /// </summary>
        [XmlEnum("disabilityFacilities")]
        DisabilityFacilities,

        /// <summary>
        /// There is some orientation system, which helps blind or visually impaired people (e.g. acoustic system or tactile paving).
        /// </summary>
        [XmlEnum("orientationSystemForBlindPeople")]
        OrientationSystemForBlindPeople,

        /// <summary>
        /// There is a visible mark for the privilege of persons with disabilities (e.g. a wheelchair symbol).
        /// </summary>
        [XmlEnum("marking")]
        Marking,

        /// <summary>
        /// No form of special accessibility; usually not convenient for persons with disabilities, e.g. because of steps or barriers.
        /// </summary>
        [XmlEnum("none")]
        None,

        /// <summary>
        /// It is unknown whether there is a special form of accessibility.
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        /// <summary>
        /// Other.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
