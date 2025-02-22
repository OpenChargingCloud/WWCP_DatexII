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

    public enum PaymentBrands
    {

        /// <summary>
        /// American Express
        /// </summary>
        [XmlEnum("americanExpress")]
        AmericanExpress,

        /// <summary>
        /// Apple pay
        /// </summary>
        [XmlEnum("applePay")]
        ApplePay,

        /// <summary>
        /// Cirrus
        /// </summary>
        [XmlEnum("cirrus")]
        Cirrus,

        /// <summary>
        /// Diners Club
        /// </summary>
        [XmlEnum("dinersClub")]
        DinersClub,

        /// <summary>
        /// Discover Card
        /// </summary>
        [XmlEnum("discoverCard")]
        DiscoverCard,

        /// <summary>
        /// Giro card
        /// </summary>
        [XmlEnum("giroCard")]
        GiroCard,

        /// <summary>
        /// Maestro
        /// </summary>
        [XmlEnum("maestro")]
        Maestro,

        /// <summary>
        /// Master Card
        /// </summary>
        [XmlEnum("masterCard")]
        MasterCard,

        /// <summary>
        /// Visa
        /// </summary>
        [XmlEnum("visa")]
        Visa,

        /// <summary>
        /// VPay
        /// </summary>
        [XmlEnum("vpay")]
        VPay,

        /// <summary>
        /// Another brand
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
