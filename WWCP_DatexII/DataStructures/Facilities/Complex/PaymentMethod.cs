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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Provides information on the means of payment available.
    /// </summary>
    [XmlType("PaymentMethod", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class PaymentMethod(IEnumerable<MeansOfPayment>?          PaymentMeans             = null,
                               IEnumerable<PaymentTiming>?           PaymentMode              = null,
                               IEnumerable<MultilingualString>?      OtherPaymentMeans        = null,
                               IEnumerable<BrandsAcceptedText>?      BrandsAcceptedText       = null,
                               IEnumerable<BrandsAcceptedCodeList>?  BrandsAcceptedCodeList   = null,
                               XElement?                             PaymentMethodExtension   = null)

    {

        #region Properties

        /// <summary>
        /// The means of payment available.
        /// </summary>
        [XmlElement("paymentMeans",             Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<MeansOfPayment>          PaymentMeans              { get; } = PaymentMeans?.          Distinct() ?? [];

        /// <summary>
        /// Defines the timing of the parking or other mobility related payment.
        /// </summary>
        [XmlElement("paymentMode",              Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<PaymentTiming>           PaymentMode               { get; } = PaymentMode?.           Distinct() ?? [];

        /// <summary>
        /// Other means of payment that are available.
        /// </summary>
        [XmlElement("otherPaymentMeans",        Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<MultilingualString>      OtherPaymentMeans         { get; } = OtherPaymentMeans?.     Distinct() ?? [];

        /// <summary>
        /// Free text elements for specifying accepted payment brands.
        /// </summary>
        [XmlElement("brandsAcceptedText",       Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<BrandsAcceptedText>      BrandsAcceptedText        { get; } = BrandsAcceptedText?.    Distinct() ?? [];

        /// <summary>
        /// Lists of codes representing accepted payment brands.
        /// </summary>
        [XmlElement("brandsAcceptedCodeList",   Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<BrandsAcceptedCodeList>  BrandsAcceptedCodeList    { get; } = BrandsAcceptedCodeList?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional payment method information.
        /// </summary>
        [XmlElement("_paymentMethodExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                            PaymentMethodExtension    { get; } = PaymentMethodExtension;

        #endregion

    }

}
