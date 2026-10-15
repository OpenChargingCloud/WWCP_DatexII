/*
 * Copyright (c) 2014-2026 GraphDefined GmbH <achim.friedland@graphdefined.com>
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
using System.Diagnostics.CodeAnalysis;

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// How a rate can be paid (DATEX II 3.7: afac:Payment).
    /// </summary>
    /// <param name="PaymentModes">When payment takes place, e.g. before or after.</param>
    /// <param name="PaymentMeans">The means of payment, e.g. a credit card or a QR code.</param>
    /// <param name="OtherPaymentMeans">Other means of payment.</param>
    /// <param name="BrandsAccepted">The payment brands accepted, e.g. Visa.</param>
    /// <param name="OtherBrandsAccepted">Other payment brands accepted.</param>
    /// <param name="PaymentExtension">An optional extension.</param>
    public class Payment(IEnumerable<PaymentTiming>?       PaymentModes          = null,
                         IEnumerable<MeansOfPayment>?      PaymentMeans          = null,
                         IEnumerable<MultilingualString>?  OtherPaymentMeans     = null,
                         IEnumerable<PaymentBrand>?        BrandsAccepted        = null,
                         IEnumerable<String>?              OtherBrandsAccepted   = null,
                         XElement?                         PaymentExtension      = null)
    {

        #region Properties

        /// <summary>
        /// When payment takes place.
        /// </summary>
        public IEnumerable<PaymentTiming>       PaymentModes           { get; } = PaymentModes?.       Distinct() ?? [];

        /// <summary>
        /// The means of payment.
        /// </summary>
        public IEnumerable<MeansOfPayment>      PaymentMeans           { get; } = PaymentMeans?.       Distinct() ?? [];

        /// <summary>
        /// Other means of payment.
        /// </summary>
        public IEnumerable<MultilingualString>  OtherPaymentMeans      { get; } = OtherPaymentMeans?.  Distinct() ?? [];

        /// <summary>
        /// The payment brands accepted.
        /// </summary>
        public IEnumerable<PaymentBrand>        BrandsAccepted         { get; } = BrandsAccepted?.     Distinct() ?? [];

        /// <summary>
        /// Other payment brands accepted.
        /// </summary>
        public IEnumerable<String>              OtherBrandsAccepted    { get; } = OtherBrandsAccepted?.Distinct() ?? [];

        /// <summary>
        /// An optional extension.
        /// </summary>
        public XElement?                        PaymentExtension       { get; } = PaymentExtension;

        #endregion


        #region TryParseXML(XML, out Payment, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a payment.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Payment">The parsed payment.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                           XML,
                                          [NotNullWhen(true)]  out Payment?  Payment,
                                          [NotNullWhen(false)] out String?   ErrorResponse)
        {

            Payment = null;

            if (!XML.TryParseOptionalValues(DatexIINS.Facilities + "paymentMode",        "payment mode",         PaymentTiming. TryParse,        out IEnumerable<PaymentTiming>       paymentModes,      out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.Facilities + "paymentMeans",       "payment means",        MeansOfPayment.TryParse,        out IEnumerable<MeansOfPayment>      paymentMeans,      out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.Facilities + "otherPaymentMeans",  "other payment means",  MultilingualString.TryParseXML, out IEnumerable<MultilingualString>  otherPaymentMeans, out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.Facilities + "brandsAccepted",     "brands accepted",      PaymentBrand.  TryParse,        out IEnumerable<PaymentBrand>        brandsAccepted,    out ErrorResponse))
            {
                return false;
            }

            Payment = new Payment(
                          paymentModes,
                          paymentMeans,
                          otherPaymentMeans,
                          brandsAccepted,
                          XML.Elements(DatexIINS.Facilities + "otherBrandsAccepted").Select(brand => brand.Value.Trim()).ToArray(),
                          XML.Element(DatexIINS.Facilities + "_paymentExtension")
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

            => new (XMLName ?? DatexIINS.Facilities + "payment",

                   PaymentModes.       Select(paymentMode       => new XElement(DatexIINS.Facilities + "paymentMode",          paymentMode. ToString())),
                   PaymentMeans.       Select(paymentMeans      => new XElement(DatexIINS.Facilities + "paymentMeans",         paymentMeans.ToString())),
                   OtherPaymentMeans.  Select(otherPaymentMeans => otherPaymentMeans.ToXML(DatexIINS.Facilities + "otherPaymentMeans")),
                   BrandsAccepted.     Select(brand             => new XElement(DatexIINS.Facilities + "brandsAccepted",       brand.       ToString())),
                   OtherBrandsAccepted.Select(brand             => new XElement(DatexIINS.Facilities + "otherBrandsAccepted",  brand)),

                   PaymentExtension is not null
                       ? PaymentExtension.ToExtensionXML(DatexIINS.Facilities + "_paymentExtension")
                       : null

               );

        #endregion

    }

}
