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

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A rate dedicated for Energy.
    /// </summary>
    [XmlType("EnergyRate", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyRate(String                 Id,
                            DateTime               LastUpdated,
                            RatePolicy             RatePolicy,
                            IEnumerable<Currency>  ApplicableCurrency,
                            MultilingualString?    RateName                    = null,
                            Boolean?               CombinationWithParkingFee   = null,
                            AmountOfMoney?         MinimumDeliveryFee          = null,
                            AmountOfMoney?         MaximumDeliveryFee          = null,
                            Percentage?            Discount                    = null,
                            MultilingualString?    AdditionalInformation       = null,
                            PaymentMethod?         PaymentMethod               = null,
                            OverallPeriod?         OverallPeriod               = null,
                            XElement?              EnergyRateExtension         = null)
    {

        #region Properties

        /// <summary>
        /// Unique identifier for this EnergyRate.
        /// </summary>
        [XmlAttribute("id")]
        public String                 Id                           { get; } = Id;

        /// <summary>
        /// The date/time at which this information was last updated.
        /// </summary>
        [XmlElement("lastUpdated",                Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime               LastUpdated                  { get; } = LastUpdated;

        /// <summary>
        /// Indication if the price is contract based or ad hoc.
        /// </summary>
        [XmlElement("ratePolicy",                 Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public RatePolicy             RatePolicy                   { get; } = RatePolicy;

        /// <summary>
        /// A general information on the applicable monetary currency.
        /// </summary>
        [XmlElement("applicableCurrency",         Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<Currency>  ApplicableCurrency           { get; } = ApplicableCurrency.Distinct() ?? [];

        /// <summary>
        /// A name for this rate, for example the name of the specific energy contract.
        /// </summary>
        [XmlElement("rateName",                   Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?    RateName                     { get; } = RateName;

        /// <summary>
        /// [TRUE] indicates that tax is included; [FALSE] indicates that tax is added additionally.
        /// </summary>
        [XmlElement("combinationWithParkingFee",  Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?               CombinationWithParkingFee    { get; } = CombinationWithParkingFee;

        /// <summary>
        /// A minimum delivery fee.
        /// </summary>
        [XmlElement("minimumDeliveryFee",         Namespace = "http://datex2.eu/schema/3/facilities")]
        public AmountOfMoney?         MinimumDeliveryFee           { get; } = MinimumDeliveryFee;

        /// <summary>
        /// A maximum delivery fee.
        /// </summary>
        [XmlElement("maximumDeliveryFee",         Namespace = "http://datex2.eu/schema/3/facilities")]
        public AmountOfMoney?         MaximumDeliveryFee           { get; } = MaximumDeliveryFee;

        /// <summary>
        /// Discount indicated in %.
        /// </summary>
        [XmlElement("discount",                   Namespace = "http://datex2.eu/schema/3/common")]
        public Percentage?            Discount                     { get; } = Discount;

        /// <summary>
        /// Free text field for additional information regarding the energy rates.
        /// </summary>
        [XmlElement("additionalInformation",      Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?    AdditionalInformation        { get; } = AdditionalInformation;

        /// <summary>
        /// Payment method information.
        /// </summary>
        [XmlElement("paymentMethod",              Namespace = "http://datex2.eu/schema/3/facilities")]
        public PaymentMethod?         PaymentMethod                { get; } = PaymentMethod;

        /// <summary>
        /// The specification of the period during which the energy rate is valid.
        /// </summary>
        [XmlElement("overallPeriod",              Namespace = "http://datex2.eu/schema/3/common")]
        public OverallPeriod?         OverallPeriod                { get; } = OverallPeriod;

        /// <summary>
        /// Optional extension element for additional energy rate information.
        /// </summary>
        [XmlElement("_energyRateExtension",       Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?              EnergyRateExtension          { get; } = EnergyRateExtension;

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "energyRate",

                   new XAttribute("id",                                     Id),

                   new XElement(DatexIINS.EnergyInfrastructure + "ratePolicy",                     RatePolicy. ToString()),
                   new XElement(DatexIINS.EnergyInfrastructure + "lastUpdated",                    LastUpdated.ToISO8601()),

                   ApplicableCurrency.Select(currency => new XElement(DatexIINS.EnergyInfrastructure + "applicableCurrency", currency.ISOCode)),

                   RateName is not null
                       ? RateName.ToXML(DatexIINS.EnergyInfrastructure + "rateName")
                       : null,

                   CombinationWithParkingFee.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "combinationWithParkingFee", CombinationWithParkingFee.Value)
                       : null,

                   MaximumDeliveryFee.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "maximumDeliveryFee",        MaximumDeliveryFee.Value.Value)
                       : null,

                   MinimumDeliveryFee.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "minimumDeliveryFee",        MinimumDeliveryFee.Value.Value)
                       : null,

                   Discount.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "discount",                  Discount.          Value.Value)
                       : null,

                   AdditionalInformation is not null
                       ? AdditionalInformation.ToXML(DatexIINS.EnergyInfrastructure + "additionalInformation")
                       : null,

                   PaymentMethod is not null
                       ? PaymentMethod.ToXML(DatexIINS.EnergyInfrastructure + "paymentMethod")
                       : null,

                   // The schema allows energyPrice 0..n here, but EnergyRate has no
                   // property to hold them -- only EnergyRateUpdate does. Nothing to
                   // write until the data model carries it.

                   OverallPeriod is not null
                       ? OverallPeriod.ToXML(DatexIINS.EnergyInfrastructure + "overallPeriod")
                       : null,

                   EnergyRateExtension is not null
                       ? new XElement(DatexIINS.EnergyInfrastructure + "_energyRateExtension", EnergyRateExtension)
                       : null

               );

        #endregion

    }

}
