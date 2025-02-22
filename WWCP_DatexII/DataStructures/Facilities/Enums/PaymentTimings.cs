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

    public enum PaymentTimings
    {

        /// <summary>
        /// Pay before entry.
        /// </summary>
        [XmlEnum("prepay")]
        Prepay,

        /// <summary>
        /// Pay at start of the parking session (e.g. for pay and display).
        /// </summary>
        [XmlEnum("payOnEntry")]
        PayOnEntry,

        /// <summary>
        /// Pay at machine on foot prior to returning to vehicle and use payment ticket to exit.
        /// </summary>
        [XmlEnum("payPriorToExit")]
        PayPriorToExit,

        /// <summary>
        /// Payment on account.
        /// </summary>
        [XmlEnum("payAfterExit")]
        PayAfterExit,

        /// <summary>
        /// Pay directly at the exit with a payment card or other means of payment.
        /// </summary>
        [XmlEnum("payAndExit")]
        PayAndExit,

        /// <summary>
        /// Other.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
