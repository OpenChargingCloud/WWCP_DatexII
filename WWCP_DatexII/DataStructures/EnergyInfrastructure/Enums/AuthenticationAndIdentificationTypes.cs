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

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A table of authentication and/or identification methods
    /// </summary>
    public enum AuthenticationAndIdentificationTypes
    {

        /// <summary>
        /// Phone (active RFID chip)
        /// </summary>
        [XmlEnum("activeRFIDChip")]
        ActiveRFIDChip,

        /// <summary>
        /// Apps
        /// </summary>
        [XmlEnum("apps")]
        Apps,

        /// <summary>
        /// RFID Calypso
        /// </summary>
        [XmlEnum("calypso")]
        Calypso,

        /// <summary>
        /// No specific authentication by using cash
        /// </summary>
        [XmlEnum("cashPayment")]
        CashPayment,

        /// <summary>
        /// Credit card
        /// </summary>
        [XmlEnum("creditCard")]
        CreditCard,

        /// <summary>
        /// Debit card
        /// </summary>
        [XmlEnum("debitCard")]
        DebitCard,

        /// <summary>
        /// RFID Card / Phone NFC - Mifare Classic
        /// </summary>
        [XmlEnum("mifareClassic")]
        MifareClassic,

        /// <summary>
        /// RFID Card / Phone NFC - Mifare Desfire
        /// </summary>
        [XmlEnum("mifareDesfire")]
        MifareDesfire,

        /// <summary>
        /// Nearfield communication
        /// </summary>
        [XmlEnum("nfc")]
        Nfc,

        /// <summary>
        /// Over the air according to ISO 15118
        /// </summary>
        [XmlEnum("overTheAir")]
        OverTheAir,

        /// <summary>
        /// Phone (dialog with platform)
        /// </summary>
        [XmlEnum("phoneDialog")]
        PhoneDialog,

        /// <summary>
        /// Phone (SMS)
        /// </summary>
        [XmlEnum("phoneSMS")]
        PhoneSMS,

        /// <summary>
        /// PINPAD
        /// </summary>
        [XmlEnum("pinpad")]
        Pinpad,

        /// <summary>
        /// PLC according to ISO 15118
        /// </summary>
        [XmlEnum("plc")]
        Plc,

        /// <summary>
        /// Pre-Paid card
        /// </summary>
        [XmlEnum("prepaidCard")]
        PrepaidCard,

        /// <summary>
        /// RFID
        /// </summary>
        [XmlEnum("rfid")]
        Rfid,

        /// <summary>
        /// Using a website
        /// </summary>
        [XmlEnum("website")]
        Website,

        /// <summary>
        /// No authentication/identification required.
        /// </summary>
        [XmlEnum("unlimitedAccess")]
        UnlimitedAccess,

        /// <summary>
        /// No access granted
        /// </summary>
        [XmlEnum("noAccess")]
        NoAccess,

        [XmlEnum("_extended")]
        Extended

    }

}
