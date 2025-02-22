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
    /// Types of areas.
    /// </summary>
    public enum NamedAreaTypes
    {

        [XmlEnum("applicationRegion")]
        ApplicationRegion,

        [XmlEnum("continent")]
        Continent,

        [XmlEnum("country")]
        Country,

        [XmlEnum("countryGroup")]
        CountryGroup,

        [XmlEnum("carParkArea")]
        CarParkArea,

        [XmlEnum("carpoolArea")]
        CarpoolArea,

        [XmlEnum("fuzzyArea")]
        FuzzyArea,

        [XmlEnum("industrialArea")]
        IndustrialArea,

        [XmlEnum("lake")]
        Lake,

        [XmlEnum("meteorologicalArea")]
        MeteorologicalArea,

        [XmlEnum("metropolitanArea")]
        MetropolitanArea,

        [XmlEnum("municipality")]
        Municipality,

        [XmlEnum("parkAndRideSite")]
        ParkAndRideSite,

        [XmlEnum("ruralCounty")]
        RuralCounty,

        [XmlEnum("sea")]
        Sea,

        [XmlEnum("touristArea")]
        TouristArea,

        [XmlEnum("trafficArea")]
        TrafficArea,

        [XmlEnum("urbanCounty")]
        UrbanCounty,

        [XmlEnum("order1AdministrativeArea")]
        Order1AdministrativeArea,

        [XmlEnum("order2AdministrativeArea")]
        Order2AdministrativeArea,

        [XmlEnum("order3AdministrativeArea")]
        Order3AdministrativeArea,

        [XmlEnum("order4AdministrativeArea")]
        Order4AdministrativeArea,

        [XmlEnum("order5AdministrativeArea")]
        Order5AdministrativeArea,

        [XmlEnum("policeForceControlArea")]
        PoliceForceControlArea,

        [XmlEnum("roadOperatorControlArea")]
        RoadOperatorControlArea,

        [XmlEnum("waterArea")]
        WaterArea,

        [XmlEnum("_extended")]
        Extended

    }

}
