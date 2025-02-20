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
    /// Types of load carried by a vehicle.
    /// </summary>
    public enum LoadTypes
    {

        /// <summary>
        /// A load that exceeds normal vehicle dimensions in terms of height, length, width, gross vehicle weight or axle weight or any combination thereof.
        /// Generally termed an "abnormal load".
        /// </summary>
        [XmlEnum("abnormalLoad")]
        AbnormalLoad,

        /// <summary>
        /// Ammunition.
        /// </summary>
        [XmlEnum("ammunition")]
        Ammunition,

        /// <summary>
        /// Chemicals of unspecified type.
        /// </summary>
        [XmlEnum("chemicals")]
        Chemicals,

        /// <summary>
        /// Combustible materials of unspecified type.
        /// </summary>
        [XmlEnum("combustibleMaterials")]
        CombustibleMaterials,

        /// <summary>
        /// Corrosive materials of unspecified type.
        /// </summary>
        [XmlEnum("corrosiveMaterials")]
        CorrosiveMaterials,

        /// <summary>
        /// Debris of unspecified type.
        /// </summary>
        [XmlEnum("debris")]
        Debris,

        /// <summary>
        /// No load.
        /// </summary>
        [XmlEnum("empty")]
        Empty,

        /// <summary>
        /// Explosive materials of unspecified type.
        /// </summary>
        [XmlEnum("explosiveMaterials")]
        ExplosiveMaterials,

        /// <summary>
        /// A load of exceptional height.
        /// </summary>
        [XmlEnum("extraHighLoad")]
        ExtraHighLoad,

        /// <summary>
        /// A load of exceptional length.
        /// </summary>
        [XmlEnum("extraLongLoad")]
        ExtraLongLoad,

        /// <summary>
        /// A load of exceptional width.
        /// </summary>
        [XmlEnum("extraWideLoad")]
        ExtraWideLoad,

        /// <summary>
        /// Fuel of unspecified type.
        /// </summary>
        [XmlEnum("fuel")]
        Fuel,

        /// <summary>
        /// Glass.
        /// </summary>
        [XmlEnum("glass")]
        Glass,

        /// <summary>
        /// Any goods of a commercial nature.
        /// </summary>
        [XmlEnum("goods")]
        Goods,

        /// <summary>
        /// Materials classed as being of a hazardous nature.
        /// </summary>
        [XmlEnum("hazardousMaterials")]
        HazardousMaterials,

        /// <summary>
        /// Liquid of an unspecified nature.
        /// </summary>
        [XmlEnum("liquid")]
        Liquid,

        /// <summary>
        /// Livestock.
        /// </summary>
        [XmlEnum("livestock")]
        Livestock,

        /// <summary>
        /// General materials of unspecified type.
        /// </summary>
        [XmlEnum("materials")]
        Materials,

        /// <summary>
        /// Materials that present a danger to people or animals.
        /// </summary>
        [XmlEnum("materialsDangerousForPeople")]
        MaterialsDangerousForPeople,

        /// <summary>
        /// Materials that present a danger to the environment.
        /// </summary>
        [XmlEnum("materialsDangerousForTheEnvironment")]
        MaterialsDangerousForTheEnvironment,

        /// <summary>
        /// Materials that are dangerous when exposed to water (e.g. react exothermically).
        /// </summary>
        [XmlEnum("materialsDangerousForWater")]
        MaterialsDangerousForWater,

        /// <summary>
        /// Oil.
        /// </summary>
        [XmlEnum("oil")]
        Oil,

        /// <summary>
        /// Materials that present limited environmental or health risk: non-combustible, non-toxic, non-corrosive.
        /// </summary>
        [XmlEnum("ordinary")]
        Ordinary,

        /// <summary>
        /// Products or produce that will significantly degrade in quality or freshness over a short period.
        /// </summary>
        [XmlEnum("perishableProducts")]
        PerishableProducts,

        /// <summary>
        /// Petrol or petroleum.
        /// </summary>
        [XmlEnum("petrol")]
        Petrol,

        /// <summary>
        /// Pharmaceutical materials.
        /// </summary>
        [XmlEnum("pharmaceuticalMaterials")]
        PharmaceuticalMaterials,

        /// <summary>
        /// Materials that emit significant quantities of electromagnetic radiation and may present a risk.
        /// </summary>
        [XmlEnum("radioactiveMaterials")]
        RadioactiveMaterials,

        /// <summary>
        /// Refrigerated goods.
        /// </summary>
        [XmlEnum("refrigeratedGoods")]
        RefrigeratedGoods,

        /// <summary>
        /// Refuse.
        /// </summary>
        [XmlEnum("refuse")]
        Refuse,

        /// <summary>
        /// Materials of a toxic nature which may damage the environment or endanger public health.
        /// </summary>
        [XmlEnum("toxicMaterials")]
        ToxicMaterials,

        /// <summary>
        /// Vehicles of any type which are being transported.
        /// </summary>
        [XmlEnum("vehicles")]
        Vehicles,

        /// <summary>
        /// Other than as defined in this enumeration.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
