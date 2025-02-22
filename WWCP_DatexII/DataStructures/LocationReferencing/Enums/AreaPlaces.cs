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
    /// Type of area place(s).
    /// </summary>
    public enum AreaPlaces
    {

        /// <summary>
        /// At national borders.
        /// </summary>
        [XmlEnum("atBorders")]
        AtBorders,

        /// <summary>
        /// At high altitudes.
        /// </summary>
        [XmlEnum("atHighAltitudes")]
        AtHighAltitudes,

        /// <summary>
        /// In built up areas, i.e. villages, towns, and cities.
        /// </summary>
        [XmlEnum("inBuiltUpAreas")]
        InBuiltUpAreas,

        /// <summary>
        /// On sections of the road where it runs through or adjacent to forested areas.
        /// </summary>
        [XmlEnum("inForestedAreas")]
        InForestedAreas,

        /// <summary>
        /// In galleries.
        /// </summary>
        [XmlEnum("inGalleries")]
        InGalleries,

        /// <summary>
        /// In low-lying areas.
        /// </summary>
        [XmlEnum("inLowLyingAreas")]
        InLowLyingAreas,

        /// <summary>
        /// In rural areas, i.e. outside villages, towns, and cities.
        /// </summary>
        [XmlEnum("inRuralAreas")]
        InRuralAreas,

        /// <summary>
        /// In shaded areas.
        /// </summary>
        [XmlEnum("inShadedAreas")]
        InShadedAreas,

        /// <summary>
        /// In the city centre areas.
        /// </summary>
        [XmlEnum("inTheInnerCityAreas")]
        InTheInnerCityAreas,

        /// <summary>
        /// In tunnels.
        /// </summary>
        [XmlEnum("inTunnels")]
        InTunnels,

        /// <summary>
        /// On bridges.
        /// </summary>
        [XmlEnum("onBridges")]
        OnBridges,

        /// <summary>
        /// On downhill sections of the road.
        /// </summary>
        [XmlEnum("onDownhillSections")]
        OnDownhillSections,

        /// <summary>
        /// On elevated sections of the road.
        /// </summary>
        [XmlEnum("onElevatedSections")]
        OnElevatedSections,

        /// <summary>
        /// On entering or leaving tunnels.
        /// </summary>
        [XmlEnum("onEnteringOrLeavingTunnels")]
        OnEnteringOrLeavingTunnels,

        /// <summary>
        /// On flyover sections of the road.
        /// </summary>
        [XmlEnum("onFlyovers")]
        OnFlyovers,

        /// <summary>
        /// On mountain passes.
        /// </summary>
        [XmlEnum("onPasses")]
        OnPasses,

        /// <summary>
        /// On underground sections of the road.
        /// </summary>
        [XmlEnum("onUndergroundSections")]
        OnUndergroundSections,

        /// <summary>
        /// On underpasses, i.e. sections of the road which pass under another road.
        /// </summary>
        [XmlEnum("onUnderpasses")]
        OnUnderpasses,

        [XmlEnum("_extended")]
        Extended

    }

}
