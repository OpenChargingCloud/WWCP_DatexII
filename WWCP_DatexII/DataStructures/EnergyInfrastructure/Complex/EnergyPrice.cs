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
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A price definition for energy refuelling.
    /// </summary>
    [XmlType("EnergyPrice", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyPrice(PriceType            PriceType,
                             AmountOfMoney        Value,
                             Boolean?             TaxIncluded             = null,
                             Percentage?          TaxRate                 = null,
                             MultilingualString?  AdditionalInformation   = null,
                             OverallPeriod?       OverallPeriod           = null,
                             XElement?            EnergyPriceExtension    = null)
    {

        #region Properties

        /// <summary>
        /// The type of price for the energy supply.
        /// </summary>
        [XmlElement("priceType",              Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public PriceType            PriceType                { get; } = PriceType;

        /// <summary>
        /// The price for the energy according to the price type.
        /// </summary>
        [XmlElement("value",                  Namespace = "http://datex2.eu/schema/3/facilities")]
        public AmountOfMoney        Value                    { get; } = Value;

        /// <summary>
        /// [TRUE] indicates that tax is included; [FALSE] indicates that tax is added additionally.
        /// </summary>
        [XmlElement("taxIncluded",            Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?             TaxIncluded              { get; } = TaxIncluded;

        /// <summary>
        /// The percentage rate of tax to be applied.
        /// </summary>
        [XmlElement("taxRate",                Namespace = "http://datex2.eu/schema/3/common")]
        public Percentage?          TaxRate                  { get; } = TaxRate;

        /// <summary>
        /// Free text field for additional information regarding the price.
        /// </summary>
        [XmlElement("additionalInformation",  Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?  AdditionalInformation    { get; } = AdditionalInformation;

        /// <summary>
        /// The overall period during which the price is applicable.
        /// </summary>
        [XmlElement("overallPeriod",          Namespace = "http://datex2.eu/schema/3/common")]
        public OverallPeriod?       OverallPeriod            { get; } = OverallPeriod;

        /// <summary>
        /// Optional extension element for additional energy price information.
        /// </summary>
        [XmlElement("_energyPriceExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            EnergyPriceExtension     { get; } = EnergyPriceExtension;

        #endregion


        #region TryParseXML(XML, out EnergyPrice, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an EnergyPrice.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyPrice">The parsed EnergyPrice.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                               XML,
                                          [NotNullWhen(true)]  out EnergyPrice?  EnergyPrice,
                                          [NotNullWhen(false)] out String?       ErrorResponse)
        {

            EnergyPrice    = null;
            ErrorResponse  = null;

            // <?xml version="1.0"?>
            // <energyPrice xmlns="http://datex2.eu/schema/3/energyInfrastructure">
            //     <priceType>pricePerKWh</priceType>
            //     <value>0.37</value>
            // </energyPrice>

            #region TryParse PriceType                [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.EnergyInfrastructure + "priceType",
                                       "price type",
                                       PriceType.TryParse,
                                       out PriceType priceType,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse Value                    [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.EnergyInfrastructure + "value",
                                       "amount of money",
                                       AmountOfMoney.TryParse,
                                       out AmountOfMoney value,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse TaxIncluded              [optional]

            if (XML.TryParseOptional(DatexIINS.Facilities + "taxIncluded",
                                     "tax included",
                                     out Boolean? taxIncluded,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse TaxRate                  [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "taxRate",
                                     "tax rate",
                                     Percentage.TryParse,
                                     out Percentage? taxRate,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse AdditionalInformation    [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "additionalInformation",
                                     "overall period",
                                     MultilingualString.TryParseXML,
                                     out MultilingualString? additionalInformation,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse OverallPeriod            [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "overallPeriod",
                                     "overall period",
                                     OverallPeriod.TryParseXML,
                                     out OverallPeriod? overallPeriod,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            EnergyPrice = new EnergyPrice(

                              priceType,
                              value,
                              taxIncluded,
                              taxRate,
                              additionalInformation,
                              overallPeriod,

                              XML.Element(DatexIINS.Common + "_energyPriceExtension")

                          );

            return true;

        }

        #endregion


    }

}
