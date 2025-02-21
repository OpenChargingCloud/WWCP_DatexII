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
    /// Updates a rate defined in the static part of the model.
    /// </summary>
    [XmlType("EnergyRateUpdate", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyRateUpdate(DateTime                   LastUpdated,
                                  EnergyRateReference        EnergyRateReference,
                                  MultilingualString?        AdditionalInformation   = null,
                                  IEnumerable<EnergyPrice>?  EnergyPrices            = null)
    {

        /// <summary>
        /// The date/time at which this information was last updated.
        /// </summary>
        [XmlElement("lastUpdated",            Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime                  LastUpdated              { get; set; } = LastUpdated;

        /// <summary>
        /// Specifies the EnergyRate that is updated here.
        /// </summary>
        [XmlElement("energyRateReference",    Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public EnergyRateReference       EnergyRateReference      { get; set; } = EnergyRateReference;

        /// <summary>
        /// Free text field for additional information regarding the energy rates.
        /// </summary>
        [XmlElement("additionalInformation",  Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?       AdditionalInformation    { get; set; } = AdditionalInformation;

        /// <summary>
        /// A collection of energy price definitions.
        /// </summary>
        [XmlElement("energyPrice",            Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyPrice>  EnergyPrices             { get; set; } = EnergyPrices ?? [];

        ///// <summary>
        ///// Optional extension element for additional energy rate update information.
        ///// </summary>
        //[XmlElement("_energyRateUpdateExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? EnergyRateUpdateExtension { get; set; }

    }

}
