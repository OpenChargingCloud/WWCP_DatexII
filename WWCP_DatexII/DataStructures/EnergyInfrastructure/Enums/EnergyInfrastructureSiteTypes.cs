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

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// The type of an energy infrastructure site.
    /// </summary>
    public enum EnergyInfrastructureSiteTypes
    {

        /// <summary>
        /// The energy infrastructure site is located inside some building, for example in a car park.
        /// </summary>
        [XmlEnum("inBuilding")]
        InBuilding,

        /// <summary>
        /// The energy infrastructure site is an open space, for example a parking site.
        /// </summary>
        [XmlEnum("openSpace")]
        OpenSpace,

        /// <summary>
        /// The energy infrastructure site is located alongside a street, for example some singular charging stations.
        /// </summary>
        [XmlEnum("onstreet")]
        Onstreet,

        /// <summary>
        /// The energy infrastructure site is located on a company site.
        /// </summary>
        [XmlEnum("onCompanySite")]
        OnCompanySite,

        /// <summary>
        /// Other types of energy infrastructure sites.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
