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

using System.Xml.Linq;
using System.Xml.Serialization;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Ratio for the specified energy source.
    /// </summary>
    [XmlType("ElectricEnergySourceRatio", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class ElectricEnergySourceRatio(ElectricEnergySourceType  EnergySource,
                                           PercentageDouble          SourceRatioValue,
                                           String?                   OtherEnergySource                    = null,
                                           XElement?                 ElectricEnergySourceRatioExtension   = null)
    {

        #region Properties

        /// <summary>
        /// An electric energy source.
        /// </summary>
        [XmlElement(ElementName = "energySource",       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public ElectricEnergySourceType  EnergySource                          { get; } = EnergySource;

        /// <summary>
        /// Some other energy source.
        /// </summary>
        [XmlElement(ElementName = "otherEnergySource",  Namespace = "http://datex2.eu/schema/3/common")]
        public String?                   OtherEnergySource                     { get; } = OtherEnergySource;

        /// <summary>
        /// The percentage ratio value of this energy source.
        /// </summary>
        [XmlElement(ElementName = "sourceRatioValue",   Namespace = "http://datex2.eu/schema/3/common")]
        public PercentageDouble          SourceRatioValue                      { get; } = SourceRatioValue;

        /// <summary>
        /// Optional extension element for additional content.
        /// </summary>
        [XmlElement(ElementName = "_electricEnergySourceRatioExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                 ElectricEnergySourceRatioExtension    { get; } = ElectricEnergySourceRatioExtension;

        #endregion

    }

}
