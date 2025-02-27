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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
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
    [XmlType("Connector", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class Connector(ConnectorType          ConnectorType,
                           Watt                   MaxPowerAtSocket,
                           String?                OtherConnector           = null,
                           IEnumerable<Country>?  CountryOfDomesticSocket  = null,
                           ChargingMode?          ChargingMode             = null,
                           ConnectorFormat?       ConnectorFormat          = null,
                           Volt?                  Voltage                  = null,
                           Ampere?                MaximumCurrent           = null,
                           XElement?              ConnectorExtension       = null)
    {

        #region Properties

        /// <summary>
        /// Specification of the connector, i.e. the charging interface type.
        /// </summary>
        [XmlElement(ElementName = "connectorType",            Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public ConnectorType          ConnectorType              { get; } = ConnectorType;

        /// <summary>
        /// Maximum power at the socket.
        /// </summary>
        [XmlElement(ElementName = "maxPowerAtSocket",         Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Watt                   MaxPowerAtSocket           { get; } = MaxPowerAtSocket;

        /// <summary>
        /// Some other connector / charging interface.
        /// </summary>
        [XmlElement(ElementName = "otherConnector",           Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public String?                OtherConnector             { get; } = OtherConnector;

        /// <summary>
        /// Countries for which the domestic socket is applicable.
        /// Only needed if explicit type of a domestic socket is not specified.
        /// </summary>
        [XmlElement(ElementName = "countryOfDomesticSocket",  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Country>?  CountryOfDomesticSocket    { get; } = CountryOfDomesticSocket ?? [];  // 2-Letter-Code

        /// <summary>
        /// Available charging modes.
        /// </summary>
        [XmlElement(ElementName = "chargingMode",             Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public ChargingMode?          ChargingMode               { get; } = ChargingMode;

        /// <summary>
        /// Information on the cable type used.
        /// </summary>
        [XmlElement(ElementName = "connectorFormat",          Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public ConnectorFormat?       ConnectorFormat            { get; } = ConnectorFormat;

        /// <summary>
        /// Possible degrees of voltage.
        /// </summary>
        [XmlElement(ElementName = "voltage",                  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Volt?                  Voltage                    { get; } = Voltage;

        /// <summary>
        /// Maximum current.
        /// </summary>
        [XmlElement(ElementName = "maximumCurrent",           Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Ampere?                MaximumCurrent             { get; } = MaximumCurrent;

        /// <summary>
        /// Optional extension element for additional content.
        /// </summary>
        [XmlElement("_connectorExtension",                    Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?              ConnectorExtension         { get; } = ConnectorExtension;

        #endregion

    }

}
