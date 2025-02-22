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
    /// ISO 3166-2 subdivision types.
    /// </summary>
    public enum SubdivisionTypes
    {

        [XmlEnum("administrativeAtoll")]
        AdministrativeAtoll,

        [XmlEnum("administrativeRegion")]
        AdministrativeRegion,

        [XmlEnum("administrativeTerritory")]
        AdministrativeTerritory,

        [XmlEnum("arcticRegion")]
        ArcticRegion,

        [XmlEnum("autonomousCity")]
        AutonomousCity,

        [XmlEnum("autonomousCityInNorthAfrica")]
        AutonomousCityInNorthAfrica,

        [XmlEnum("autonomousCommunity")]
        AutonomousCommunity,

        [XmlEnum("autonomousDistrict")]
        AutonomousDistrict,

        [XmlEnum("autonomousProvince")]
        AutonomousProvince,

        [XmlEnum("autonomousRegion")]
        AutonomousRegion,

        [XmlEnum("canton")]
        Canton,

        [XmlEnum("capitalCity")]
        CapitalCity,

        [XmlEnum("city")]
        City,

        [XmlEnum("cityMunicipality")]
        CityMunicipality,

        [XmlEnum("cityOfCountyRight")]
        CityOfCountyRight,

        [XmlEnum("commune")]
        Commune,

        [XmlEnum("councilArea")]
        CouncilArea,

        [XmlEnum("county")]
        County,

        [XmlEnum("country")]
        Country,

        [XmlEnum("department")]
        Department,

        [XmlEnum("dependency")]
        Dependency,

        [XmlEnum("district")]
        District,

        [XmlEnum("districtMunicipality")]
        DistrictMunicipality,

        [XmlEnum("districtWithSpecialStatus")]
        DistrictWithSpecialStatus,

        [XmlEnum("entity")]
        Entity,

        [XmlEnum("geographicalEntity")]
        GeographicalEntity,

        [XmlEnum("governorate")]
        Governorate,

        [XmlEnum("laender")]
        Laender,

        [XmlEnum("localCouncil")]
        LocalCouncil,

        [XmlEnum("londonBorough")]
        LondonBorough,

        [XmlEnum("metropolitanArea")]
        MetropolitanArea,

        [XmlEnum("metropolitanDepartment")]
        MetropolitanDepartment,

        [XmlEnum("metropolitanDistrict")]
        MetropolitanDistrict,

        [XmlEnum("metropolitanRegion")]
        MetropolitanRegion,

        [XmlEnum("municipality")]
        Municipality,

        [XmlEnum("overseasDepartment")]
        OverseasDepartment,

        [XmlEnum("overseasRegion")]
        OverseasRegion,

        [XmlEnum("overseasTerritorialCollectivity")]
        OverseasTerritorialCollectivity,

        [XmlEnum("parish")]
        Parish,

        [XmlEnum("province")]
        Province,

        [XmlEnum("quarter")]
        Quarter,

        [XmlEnum("region")]
        Region,

        [XmlEnum("republic")]
        Republic,

        [XmlEnum("republicanCity")]
        RepublicanCity,

        [XmlEnum("selfGovernedPart")]
        SelfGovernedPart,

        [XmlEnum("specialMunicipality")]
        SpecialMunicipality,

        [XmlEnum("state")]
        State,

        [XmlEnum("territorialUnit")]
        TerritorialUnit,

        [XmlEnum("territory")]
        Territory,

        [XmlEnum("twoTierCounty")]
        TwoTierCounty,

        [XmlEnum("unitaryAuthority")]
        UnitaryAuthority,

        [XmlEnum("ward")]
        Ward,

        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
