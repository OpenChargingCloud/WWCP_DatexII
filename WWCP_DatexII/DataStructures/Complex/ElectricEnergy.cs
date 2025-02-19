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
    /// >A specific offer of energy, optional described with its electrical mix and its rates.
    /// </summary>
    public class ElectricEnergy
    {

        /// <summary>
        /// A name for this energy product
        /// </summary>
        [XmlElement(ElementName = "energyProductName",        Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public AdditionalInformation?                   EnergyProductName             { get; set; }

        /// <summary>
        /// True if 100% from regenerative sources (CO2 and nuclear waste is zero)
        /// </summary>
        [XmlElement(ElementName = "isGreenEnergy",            Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Boolean?                                 IsGreenEnergy                 { get; set; }

        /// <summary>
        /// Exhausted carbon dioxide in grams per kilowatt hour.
        /// </summary>
        [XmlElement(ElementName = "carbonDioxideImpact",      Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Single?                                  CarbonDioxideImpact           { get; set; }

        /// <summary>
        /// Produced nuclear waste in grams per kilowatt hour.
        /// </summary>
        [XmlElement(ElementName = "nuclearWasteImpact",       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Single?                                  NuclearWasteImpact            { get; set; }

        /// <summary>
        /// Specifies one or more rates for this type of electric energy by reference to existing EnergyRate specifications.
        /// </summary>
        [XmlElement(ElementName = "energyRateByReference",    Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<Reference>?                  EnergyRateByReference         { get; set; }

        /// <summary>
        /// Defines one or more energy source ratios.
        /// </summary>
        [XmlElement(ElementName = "electricEnergySourceRatio", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<ElectricEnergySourceRatio>?  ElectricEnergySourceRatios    { get; set; }

        /// <summary>
        /// Mobility service providers offering contract-based recharging.
        /// </summary>
        [XmlElement(ElementName = "mobilityServiceProvider", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<OrganisationSpecification>?  MobilityServiceProviders      { get; set; }

        /// <summary>
        /// Specifies one or more rates for this type of electric energy.
        /// </summary>
        [XmlElement(ElementName = "energyRate", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyRate>?                 EnergyRates                   { get; set; }

        ///// <summary>
        ///// Optional extension element for additional content.
        ///// </summary>
        //[XmlElement(ElementName = "_electricEnergyExtension", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        //public ExtensionType?                           ElectricEnergyExtension       { get; set; }

    }

}
