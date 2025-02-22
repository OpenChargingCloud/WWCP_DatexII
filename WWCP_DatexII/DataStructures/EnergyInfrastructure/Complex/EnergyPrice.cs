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

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using System.Xml.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A price definition for energy refueling.
    /// </summary>
    [XmlType("EnergyPrice", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyPrice(PriceTypes           PriceType,
                             AmountOfMoney        Value,
                             Boolean?             TaxIncluded             = null,
                             Percentage?          TaxRate                 = null,
                             MultilingualString?  AdditionalInformation   = null,
                             OverallPeriod?       OverallPeriod           = null)
    {

        /// <summary>
        /// The type of price for the energy supply.
        /// </summary>
        [XmlElement("priceType",              Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public PriceTypes           PriceType                { get; set; } = PriceType;

        /// <summary>
        /// The price for the energy according to the price type.
        /// </summary>
        [XmlElement("value",                  Namespace = "http://datex2.eu/schema/3/facilities")]
        public AmountOfMoney        Value                    { get; set; } = Value;

        /// <summary>
        /// [TRUE] indicates that tax is included; [FALSE] indicates that tax is added additionally.
        /// </summary>
        [XmlElement("taxIncluded",            Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?             TaxIncluded              { get; set; } = TaxIncluded;

        /// <summary>
        /// The percentage rate of tax to be applied.
        /// </summary>
        [XmlElement("taxRate",                Namespace = "http://datex2.eu/schema/3/common")]
        public Percentage?          TaxRate                  { get; set; } = TaxRate;

        /// <summary>
        /// Free text field for additional information regarding the price.
        /// </summary>
        [XmlElement("additionalInformation",  Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?  AdditionalInformation    { get; set; } = AdditionalInformation;

        /// <summary>
        /// The overall period during which the price is applicable.
        /// </summary>
        [XmlElement("overallPeriod",          Namespace = "http://datex2.eu/schema/3/common")]
        public OverallPeriod?       OverallPeriod            { get; set; } = OverallPeriod;

        /// <summary>
        /// Optional extension element for additional energy price information.
        /// </summary>
        [XmlElement("_energyPriceExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            EnergyPriceExtension     { get; set; }

    }

}
