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

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// A table of commonly used connectors / charging interfaces.
    /// </summary>
    public enum ConnectorTypes
    {

        /// <summary>
        /// CHAdeMO, 600 V DC. Used mostly in Japan.
        /// </summary>
        [XmlEnum("chademo")]
        Chademo,

        /// <summary>
        /// CEE3, 230 V, 16 A.
        /// </summary>
        [XmlEnum("cee3")]
        Cee3,

        /// <summary>
        /// CEE5, 400 V, 16-63 A.
        /// </summary>
        [XmlEnum("cee5")]
        Cee5,

        /// <summary>
        /// Yazaki, 400 VDC, 125 A, Asian standard.
        /// </summary>
        [XmlEnum("yazaki")]
        Yazaki,

        /// <summary>
        /// A domestic socket of unspecified type. Applicable countries should be specified in separate attribute.
        /// </summary>
        [XmlEnum("domestic")]
        Domestic,

        /// <summary>
        /// Domestic socket type A (mainly used in the USA, Canada, Mexico &amp; Japan).
        /// </summary>
        [XmlEnum("domesticA")]
        DomesticA,

        /// <summary>
        /// Domestic socket type B (mainly used in the USA, Canada &amp; Mexico).
        /// </summary>
        [XmlEnum("domesticB")]
        DomesticB,

        /// <summary>
        /// Domestic socket type C (commonly used in Europe, South America &amp; Asia).
        /// </summary>
        [XmlEnum("domesticC")]
        DomesticC,

        /// <summary>
        /// Domestic socket type D (mainly used in India).
        /// </summary>
        [XmlEnum("domesticD")]
        DomesticD,

        /// <summary>
        /// Domestic socket type E (primarily used in France, Belgium, Poland, Slovakia &amp; Czechia).
        /// </summary>
        [XmlEnum("domesticE")]
        DomesticE,

        /// <summary>
        /// Domestic socket type F (used almost everywhere in Europe &amp; Russia, except for the UK &amp; Ireland).
        /// </summary>
        [XmlEnum("domesticF")]
        DomesticF,

        /// <summary>
        /// Domestic socket type G (mainly used in the United Kingdom, Ireland, Malta, Malaysia, Singapore &amp; the Arabian Peninsula).
        /// </summary>
        [XmlEnum("domesticG")]
        DomesticG,

        /// <summary>
        /// Domestic socket type H (used exclusively in Israel, the West Bank &amp; the Gaza Strip).
        /// </summary>
        [XmlEnum("domesticH")]
        DomesticH,

        /// <summary>
        /// Domestic socket type I (mainly used in Australia, New Zealand, China &amp; Argentina).
        /// </summary>
        [XmlEnum("domesticI")]
        DomesticI,

        /// <summary>
        /// Domestic socket type J (used almost exclusively in Switzerland &amp; Liechtenstein).
        /// </summary>
        [XmlEnum("domesticJ")]
        DomesticJ,

        /// <summary>
        /// Domestic socket type K (used almost exclusively in Denmark &amp; Greenland).
        /// </summary>
        [XmlEnum("domesticK")]
        DomesticK,

        /// <summary>
        /// Domestic socket type L (used almost exclusively in Italy &amp; Chile).
        /// </summary>
        [XmlEnum("domesticL")]
        DomesticL,

        /// <summary>
        /// Domestic socket type M (mainly used in South Africa).
        /// </summary>
        [XmlEnum("domesticM")]
        DomesticM,

        /// <summary>
        /// Domestic socket type N (used in Brazil and South Africa).
        /// </summary>
        [XmlEnum("domesticN")]
        DomesticN,

        /// <summary>
        /// Domestic socket type O (used exclusively in Thailand).
        /// </summary>
        [XmlEnum("domesticO")]
        DomesticO,

        /// <summary>
        /// IEC 60309-2 Industrial Connector single phase 16 amperes (usually blue).
        /// </summary>
        [XmlEnum("iec60309x2single16")]
        Iec60309x2Single16,

        /// <summary>
        /// IEC 60309-2 Industrial Connector three phase 16 amperes (usually red).
        /// </summary>
        [XmlEnum("iec60309x2three16")]
        Iec60309x2Three16,

        /// <summary>
        /// IEC 60309-2 Industrial Connector three phase 32 amperes (usually red).
        /// </summary>
        [XmlEnum("iec60309x2three32")]
        Iec60309x2Three32,

        /// <summary>
        /// IEC 60309-2 Industrial Connector three phase 64 amperes (usually red).
        /// </summary>
        [XmlEnum("iec60309x2three64")]
        Iec60309x2Three64,

        /// <summary>
        /// IEC 62196 Type 1 "SAE J1772". Mostly used in USA and Asia.
        /// </summary>
        [XmlEnum("iec62196T1")]
        Iec62196T1,

        /// <summary>
        /// Combo Type 1 based, DC.
        /// </summary>
        [XmlEnum("iec62196T1COMBO")]
        Iec62196T1COMBO,

        /// <summary>
        /// IEC 62196 Type 2 "Mennekes" - 400 V, 16-63 A. Mandatory in Europe; EN 17186 identifier "C".
        /// </summary>
        [XmlEnum("iec62196T2")]
        Iec62196T2,

        /// <summary>
        /// Combo Type 2 based, DC, Type 2 connector with extension for simultaneous DC-charging.
        /// </summary>
        [XmlEnum("iec62196T2COMBO")]
        Iec62196T2COMBO,

        /// <summary>
        /// IEC 62196 Type 3A; EN 17186 identifier "D".
        /// </summary>
        [XmlEnum("iec62196T3A")]
        Iec62196T3A,

        /// <summary>
        /// IEC 62196 Type 3C "Scame"; EN 17186 identifier "E".
        /// </summary>
        [XmlEnum("iec62196T3C")]
        Iec62196T3C,

        /// <summary>
        /// MCS.
        /// </summary>
        [XmlEnum("mcs")]
        Mcs,

        /// <summary>
        /// On-board Bottom-up-Pantograph typically for bus charging.
        /// </summary>
        [XmlEnum("pantographBottomUp")]
        PantographBottomUp,

        /// <summary>
        /// Off-board Top-down-Pantograph typically for bus charging.
        /// </summary>
        [XmlEnum("pantographTopDown")]
        PantographTopDown,

        /// <summary>
        /// Tesla Connector EU (modification of the Type 2 connector).
        /// </summary>
        [XmlEnum("teslaConnectorEurope")]
        TeslaConnectorEurope,

        /// <summary>
        /// Tesla connector used in America (Tesla specific).
        /// </summary>
        [XmlEnum("teslaConnectorAmerica")]
        TeslaConnectorAmerica,

        /// <summary>
        /// Tesla Connector "Roadster"-type (round, 4 pin).
        /// </summary>
        [XmlEnum("teslaR")]
        TeslaR,

        /// <summary>
        /// Tesla Connector "Model-S"-type (oval, 5 pin).
        /// </summary>
        [XmlEnum("teslaS")]
        TeslaS,

        /// <summary>
        /// Other charging interface.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
