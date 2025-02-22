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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    public enum MeansOfPayment
    {

        /// <summary>
        /// Payment by electronic payment credit card which is a small plastic card issued by a bank, building society, or other financial institution (ISO/IEC 7813 and other related standards), allowing the holder to purchase goods or services on credit.
        /// </summary>
        [XmlEnum("paymentCreditCard")]
        PaymentCreditCard,

        /// <summary>
        /// Cash payment using bills only.
        /// </summary>
        [XmlEnum("cashBillsOnly")]
        CashBillsOnly,

        /// <summary>
        /// Cash payment with coins only.
        /// </summary>
        [XmlEnum("cashCoinsOnly")]
        CashCoinsOnly,

        /// <summary>
        /// Toll tag (RFID) or similar, with associated account.
        /// </summary>
        [XmlEnum("tollTag")]
        TollTag,

        /// <summary>
        /// Payment method using an app or other functions typically via a smartphone to a linked bank or card account.
        /// </summary>
        [XmlEnum("mobileAccount")]
        MobileAccount,

        /// <summary>
        /// Cash payment using bills and/or coins only.
        /// </summary>
        [XmlEnum("cashCoinsAndBills")]
        CashCoinsAndBills,

        /// <summary>
        /// Advanced payment for parking right.
        /// </summary>
        [XmlEnum("prepay")]
        Prepay,

        /// <summary>
        /// Payment by electronic payment debit card which is a small plastic card (ISO/IEC 7813 and other related standards), allowing the holder to transfer money electronically from their bank account when making a purchase.
        /// </summary>
        [XmlEnum("paymentDebitCard")]
        PaymentDebitCard,

        /// <summary>
        /// Payment by electronic payment debit card which is a small plastic card (ISO/IEC 7813 and other related standards) with a monetary value stored on the card itself, that may not be linked to an external account maintained by a financial institution.
        /// </summary>
        [XmlEnum("paymentValueCard")]
        PaymentValueCard,

        /// <summary>
        /// Near field communication.
        /// </summary>
        [XmlEnum("nfc")]
        Nfc,

        /// <summary>
        /// Payment by cards with EMC chip.
        /// </summary>
        [XmlEnum("emv")]
        Emv,

        /// <summary>
        /// Payment with a given QR-code.
        /// </summary>
        [XmlEnum("qrCode")]
        QrCode,

        /// <summary>
        /// Payment by website.
        /// </summary>
        [XmlEnum("website")]
        Website,

        /// <summary>
        /// Unknown.
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        [XmlEnum("_extended")]
        Extended

    }

}
