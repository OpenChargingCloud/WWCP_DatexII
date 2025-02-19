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

using org.GraphDefined.Vanaheimr.Illias;
using System.Xml.Serialization;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// Ratio for the specified energy source.
    /// </summary>
    public class ElectricEnergySourceRatio
    {

        /// <summary>
        /// An electric energy source.
        /// </summary>
        [XmlElement(ElementName = "energySource",       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public ElectricEnergySourceTypes  EnergySource         { get; set; }

        /// <summary>
        /// Some other energy source.
        /// </summary>
        [XmlElement(ElementName = "otherEnergySource",  Namespace = "http://datex2.eu/schema/3/common")]
        public String?                    OtherEnergySource    { get; set; }

        /// <summary>
        /// The percentage ratio value of this energy source.
        /// </summary>
        [XmlElement(ElementName = "sourceRatioValue",   Namespace = "http://datex2.eu/schema/3/common")]
        public PercentageDouble           SourceRatioValue     { get; set; }

        ///// <summary>
        ///// Optional extension element for additional content.
        ///// </summary>
        //[XmlElement(ElementName = "_electricEnergySourceRatioExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? ElectricEnergySourceRatioExtension { get; set; }

    }

}
