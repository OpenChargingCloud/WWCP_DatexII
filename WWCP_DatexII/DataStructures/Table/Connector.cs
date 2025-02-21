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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// Parameters and description of an interface that is available
    /// at the given electric charging point to connect vehicles.
    /// </summary>
    /// <param name="ConnectorType">Specification of the connector, i.e. the charging interface type.</param>
    /// <param name="MaxPowerAtSocket">Maximum power at the socket.</param>
    /// <param name="OtherConnector">Some other connector / charging interface.</param>
    /// <param name="CountryOfDomesticSocket">Countries for which the domestic socket is applicable. Only needed if explicit type of a domestic socket is not specified.</param>
    /// <param name="ChargingMode">Available charging modes.</param>
    /// <param name="ConnectorFormat">Information on the cable type used.</param>
    /// <param name="Voltage">Possible degrees of voltage.</param>
    /// <param name="MaximumCurrent">Maximum current.</param>
    public class Connector(ConnectorTypes         ConnectorType,
                           Watt                   MaxPowerAtSocket,
                           String?                OtherConnector           = null,
                           IEnumerable<Country>?  CountryOfDomesticSocket  = null,
                           ChargingModes?         ChargingMode             = null,
                           ConnectorFormats?      ConnectorFormat          = null,
                           Volt?                  Voltage                  = null,
                           Ampere?                MaximumCurrent           = null)
    {

        /// <summary>
        /// Specification of the connector, i.e. the charging interface type.
        /// </summary>
        [XmlElement(ElementName = "connectorType",   Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public ConnectorTypes         ConnectorType              { get; set; } = ConnectorType;

        /// <summary>
        /// Maximum power at the socket.
        /// </summary>
        [XmlElement("maxPowerAtSocket",              Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Watt                   MaxPowerAtSocket           { get; set; } = MaxPowerAtSocket;

        /// <summary>
        /// Some other connector / charging interface.
        /// </summary>
        [XmlElement(ElementName = "otherConnector",  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public String?                OtherConnector             { get; set; } = OtherConnector;

        /// <summary>
        /// Countries for which the domestic socket is applicable.
        /// Only needed if explicit type of a domestic socket is not specified.
        /// </summary>
        [XmlElement("countryOfDomesticSocket",       Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Country>?  CountryOfDomesticSocket    { get; set; } = CountryOfDomesticSocket ?? [];  // 2-Letter-Code

        /// <summary>
        /// Available charging modes.
        /// </summary>
        [XmlElement("chargingMode",                  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public ChargingModes?         ChargingMode               { get; set; } = ChargingMode;

        /// <summary>
        /// Information on the cable type used.
        /// </summary>
        [XmlElement("connectorFormat",               Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public ConnectorFormats?      ConnectorFormat            { get; set; } = ConnectorFormat;

        /// <summary>
        /// Possible degrees of voltage.
        /// </summary>
        [XmlElement("voltage",                       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Volt?                  Voltage                    { get; set; } = Voltage;

        /// <summary>
        /// Maximum current.
        /// </summary>
        [XmlElement("maximumCurrent",                Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Ampere?                MaximumCurrent             { get; set; } = MaximumCurrent;

        ///// <summary>
        ///// Optional extension element for additional content.
        ///// </summary>
        //[XmlElement("_connectorExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? ConnectorExtension { get; set; }

    }

}
