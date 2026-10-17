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
    /// A rate dedicated for Energy.
    /// </summary>
    [XmlType("EnergyRate", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public class EnergyRate(String                 Id,
                            DateTimeOffset         LastUpdated,
                            RatePolicy             RatePolicy,
                            IEnumerable<Currency>  ApplicableCurrency,
                            MultilingualString?    RateName                    = null,
                            Boolean?               CombinationWithParkingFee   = null,
                            AmountOfMoney?         MinimumDeliveryFee          = null,
                            AmountOfMoney?         MaximumDeliveryFee          = null,
                            Percentage?            Discount                    = null,
                            MultilingualString?    AdditionalInformation       = null,
                            Payment?               Payment                     = null,
                            OverallPeriod?         OverallPeriod               = null,
                            XElement?              EnergyRateExtension         = null,
                            IEnumerable<EnergyPrice>? EnergyPrices             = null)
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
        [XmlElement("lastUpdated",                Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public DateTimeOffset         LastUpdated                  { get; } = LastUpdated;

        /// <summary>
        /// Indication if the price is contract based or ad hoc.
        /// </summary>
        [XmlElement("ratePolicy",                 Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public RatePolicy             RatePolicy                   { get; } = RatePolicy;

        /// <summary>
        /// A general information on the applicable monetary currency.
        /// </summary>
        [XmlElement("applicableCurrency",         Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<Currency>  ApplicableCurrency           { get; } = ApplicableCurrency.Distinct() ?? [];

        /// <summary>
        /// A name for this rate, for example the name of the specific energy contract.
        /// </summary>
        [XmlElement("rateName",                   Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public MultilingualString?    RateName                     { get; } = RateName;

        /// <summary>
        /// [TRUE] indicates that tax is included; [FALSE] indicates that tax is added additionally.
        /// </summary>
        [XmlElement("combinationWithParkingFee",  Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public Boolean?               CombinationWithParkingFee    { get; } = CombinationWithParkingFee;

        /// <summary>
        /// A minimum delivery fee.
        /// </summary>
        [XmlElement("minimumDeliveryFee",         Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public AmountOfMoney?         MinimumDeliveryFee           { get; } = MinimumDeliveryFee;

        /// <summary>
        /// A maximum delivery fee.
        /// </summary>
        [XmlElement("maximumDeliveryFee",         Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public AmountOfMoney?         MaximumDeliveryFee           { get; } = MaximumDeliveryFee;

        /// <summary>
        /// Discount indicated in %.
        /// </summary>
        [XmlElement("discount",                   Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public Percentage?            Discount                     { get; } = Discount;

        /// <summary>
        /// Free text field for additional information regarding the energy rates.
        /// </summary>
        [XmlElement("additionalInformation",      Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public MultilingualString?    AdditionalInformation        { get; } = AdditionalInformation;

        /// <summary>
        /// Payment method information.
        /// </summary>
        [XmlElement("payment",                    Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public Payment?               Payment                      { get; } = Payment;

        /// <summary>
        /// The specification of the period during which the energy rate is valid.
        /// </summary>
        [XmlElement("overallPeriod",              Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public OverallPeriod?         OverallPeriod                { get; } = OverallPeriod;

        /// <summary>
        /// Optional extension element for additional energy rate information.
        /// </summary>
        [XmlElement("_energyRateExtension",       Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public XElement?              EnergyRateExtension          { get; } = EnergyRateExtension;

        /// <summary>
        /// The prices of this rate.
        /// </summary>
        [XmlElement("energyPrice",            Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<EnergyPrice>  EnergyPrices         { get; } = EnergyPrices?.Distinct() ?? [];

        #endregion


        #region TryParseXML(XML, out EnergyRate, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an energy rate.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyRate">The parsed energy rate.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                              XML,
                                          [NotNullWhen(true)]  out EnergyRate?  EnergyRate,
                                          [NotNullWhen(false)] out String?      ErrorResponse)
        {

            EnergyRate = null;

            #region TryParse Id, RatePolicy, LastUpdated       [mandatory]

            var id = XML.Attribute("id")?.Value.Trim();

            if (id is null || id.Length == 0)
            {
                ErrorResponse = "The mandatory 'id' attribute of the energy rate is missing!";
                return false;
            }

            if (!XML.TryParseMandatoryValue(DatexIINS.EnergyInfrastructure + "ratePolicy",   "rate policy",   EnergyInfrastructure.RatePolicy.TryParse, out RatePolicy     ratePolicy,  out ErrorResponse) ||
                !XML.TryParseMandatoryValue(DatexIINS.EnergyInfrastructure + "lastUpdated",  "last updated",  DatexIIXMLExtensions.TryParseTimestamp,   out DateTimeOffset lastUpdated, out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse ApplicableCurrency                [mandatory]

            var currencies = new List<Currency>();

            foreach (var currencyXML in XML.Elements(DatexIINS.EnergyInfrastructure + "applicableCurrency"))
            {

                if (!Currency.TryParse(currencyXML.Value.Trim(), out var currency))
                {
                    ErrorResponse = $"Invalid applicable currency '{currencyXML.Value}'!";
                    return false;
                }

                currencies.Add(currency);

            }

            if (currencies.Count == 0)
            {
                ErrorResponse = "The mandatory applicable currency of the energy rate is missing!";
                return false;
            }

            #endregion

            #region TryParse optional values and elements

            if (!XML.TryParseOptionalXML  (DatexIINS.EnergyInfrastructure + "rateName",                   "rate name",                     MultilingualString.TryParseXML,        out MultilingualString?       rateName,                  out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "combinationWithParkingFee",  "combination with parking fee",  DatexIIXMLExtensions.TryParseBoolean,  out Boolean?                  combinationWithParkingFee, out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "maximumDeliveryFee",         "maximum delivery fee",          AmountOfMoney.TryParse,                out AmountOfMoney?            maximumDeliveryFee,        out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "minimumDeliveryFee",         "minimum delivery fee",          AmountOfMoney.TryParse,                out AmountOfMoney?            minimumDeliveryFee,        out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "discount",                   "discount",                      Percentage.TryParse,                   out Percentage?               discount,                  out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.EnergyInfrastructure + "additionalInformation",      "additional information",        MultilingualString.TryParseXML,        out MultilingualString?       additionalInformation,     out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.EnergyInfrastructure + "payment",                    "payment",                       Facilities.Payment.TryParseXML,        out Payment?                  payment,             out ErrorResponse) ||
                !XML.TryParseOptionalXMLs (DatexIINS.EnergyInfrastructure + "energyPrice",                "energy price",                  EnergyPrice.TryParseXML,               out IEnumerable<EnergyPrice>  energyPrices,              out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.EnergyInfrastructure + "overallPeriod",              "overall period",                Common.OverallPeriod.TryParseXML,      out OverallPeriod?            overallPeriod,             out ErrorResponse))
            {
                return false;
            }

            #endregion


            EnergyRate = new EnergyRate(
                             id,
                             lastUpdated,
                             ratePolicy,
                             currencies,
                             rateName,
                             combinationWithParkingFee,
                             minimumDeliveryFee,
                             maximumDeliveryFee,
                             discount,
                             additionalInformation,
                             payment,
                             overallPeriod,
                             XML.Element(DatexIINS.EnergyInfrastructure + "_energyRateExtension"),
                             energyPrices
                         );

            return true;

        }

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

                   Payment is not null
                       ? Payment.ToXML(DatexIINS.EnergyInfrastructure + "payment")
                       : null,

                   EnergyPrices.Select(energyPrice => energyPrice.ToXML(DatexIINS.EnergyInfrastructure + "energyPrice")),

                   OverallPeriod is not null
                       ? OverallPeriod.ToXML(DatexIINS.EnergyInfrastructure + "overallPeriod")
                       : null,

                   EnergyRateExtension is not null
                       ? EnergyRateExtension.ToExtensionXML(DatexIINS.EnergyInfrastructure + "_energyRateExtension")
                       : null

               );

        #endregion

    }

}
