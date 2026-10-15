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
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;

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
    /// <param name="ConnectorFormat">Information on the cable type used.</param>
    /// <param name="Voltage">Possible degrees of voltage.</param>
    /// <param name="MaximumCurrent">Maximum current.</param>
    /// <param name="ExternalIdentifier">External identifiers of the connector (DATEX II 3.7).</param>
    [XmlType("Connector", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public class Connector(ConnectorType          ConnectorType,
                           Watt                   MaxPowerAtSocket,
                           String?                OtherConnector           = null,
                           IEnumerable<Country>?  CountryOfDomesticSocket  = null,
                           ConnectorFormat?       ConnectorFormat          = null,
                           Volt?                  Voltage                  = null,
                           Ampere?                MaximumCurrent           = null,
                           IEnumerable<Facilities.ExternalIdentifier>?  ExternalIdentifier  = null,
                           XElement?              ConnectorExtension       = null)
    {

        #region Properties

        /// <summary>
        /// Specification of the connector, i.e. the charging interface type.
        /// </summary>
        [XmlElement(ElementName = "connectorType",            Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public ConnectorType          ConnectorType              { get; } = ConnectorType;

        /// <summary>
        /// Maximum power at the socket.
        /// </summary>
        [XmlElement(ElementName = "maxPowerAtSocket",         Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public Watt                   MaxPowerAtSocket           { get; } = MaxPowerAtSocket;

        /// <summary>
        /// Some other connector / charging interface.
        /// </summary>
        [XmlElement(ElementName = "otherConnector",           Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public String?                OtherConnector             { get; } = OtherConnector;

        /// <summary>
        /// Countries for which the domestic socket is applicable.
        /// Only needed if explicit type of a domestic socket is not specified.
        /// </summary>
        [XmlElement(ElementName = "countryOfDomesticSocket",  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Country>?  CountryOfDomesticSocket    { get; } = CountryOfDomesticSocket ?? [];  // 2-Letter-Code

        /// <summary>
        /// Information on the cable type used.
        /// </summary>
        [XmlElement(ElementName = "connectorFormat",          Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public ConnectorFormat?       ConnectorFormat            { get; } = ConnectorFormat;

        /// <summary>
        /// Possible degrees of voltage.
        /// </summary>
        [XmlElement(ElementName = "voltage",                  Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public Volt?                  Voltage                    { get; } = Voltage;

        /// <summary>
        /// Maximum current.
        /// </summary>
        [XmlElement(ElementName = "maximumCurrent",           Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public Ampere?                MaximumCurrent             { get; } = MaximumCurrent;

        /// <summary>
        /// External identifiers of the connector (DATEX II 3.7).
        /// </summary>
        public IEnumerable<Facilities.ExternalIdentifier>  ExternalIdentifier  { get; } = ExternalIdentifier?.ToArray() ?? [];

        /// <summary>
        /// Optional extension element for additional content.
        /// </summary>
        [XmlElement("_connectorExtension",                    Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?              ConnectorExtension         { get; } = ConnectorExtension;

        #endregion


        #region TryParseXML(XML, out Connector, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a connector.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Connector">The parsed connector.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                             XML,
                                          [NotNullWhen(true)]  out Connector?  Connector,
                                          [NotNullWhen(false)] out String?     ErrorResponse)
        {

            Connector = null;

            if (!XML.TryParseMandatoryValue(DatexIINS.EnergyInfrastructure + "connectorType",     "connector type",       EnergyInfrastructure.ConnectorType.TryParse,    out ConnectorType     connectorType,    out ErrorResponse) ||
                !XML.TryParseMandatoryValue(DatexIINS.EnergyInfrastructure + "maxPowerAtSocket",  "max power at socket",  DatexIIXMLExtensions.TryParseWatt,               out Watt              maxPowerAtSocket, out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "connectorFormat",   "connector format",     EnergyInfrastructure.ConnectorFormat.TryParse,  out ConnectorFormat?  connectorFormat,  out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "voltage",           "voltage",              DatexIIXMLExtensions.TryParseVolt,               out Volt?             voltage,          out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "maximumCurrent",    "maximum current",      DatexIIXMLExtensions.TryParseAmpere,             out Ampere?           maximumCurrent,   out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "externalIdentifier","external identifier",  Facilities.ExternalIdentifier.TryParseXML,       out IEnumerable<Facilities.ExternalIdentifier> externalIdentifiers, out ErrorResponse))
            {
                return false;
            }

            var countries = new List<Country>();

            foreach (var countryXML in XML.Elements(DatexIINS.EnergyInfrastructure + "countryOfDomesticSocket"))
            {

                if (!Country.TryParseAlpha2Code(countryXML.Value.Trim().ToUpperInvariant(), out var country))
                {
                    ErrorResponse = $"Invalid country of domestic socket '{countryXML.Value}'!";
                    return false;
                }

                countries.Add(country);

            }

            Connector = new Connector(
                            connectorType,
                            maxPowerAtSocket,
                            XML.OptionalText(DatexIINS.EnergyInfrastructure + "otherConnector"),
                            countries,
                            connectorFormat,
                            voltage,
                            maximumCurrent,
                            externalIdentifiers,
                            XML.Element(DatexIINS.EnergyInfrastructure + "_connectorExtension")
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

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "connector",

                   new XElement(DatexIINS.EnergyInfrastructure + "connectorType",              ConnectorType.ToString()),

                   OtherConnector is not null && OtherConnector.Length > 0
                       ? new XElement(DatexIINS.EnergyInfrastructure + "otherConnector",       OtherConnector)
                       : null,

                   CountryOfDomesticSocket?.Select(country => new XElement(DatexIINS.EnergyInfrastructure + "countryOfDomesticSocket", country.Alpha2Code)),

                   ConnectorFormat.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "connectorFormat",      ConnectorFormat.Value.ToString())
                       : null,

                   new XElement(DatexIINS.EnergyInfrastructure + "maxPowerAtSocket",           MaxPowerAtSocket.Value),

                   Voltage.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "voltage",              Voltage.       Value.Value)
                       : null,

                   MaximumCurrent.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "maximumCurrent",       MaximumCurrent.Value.Value)
                       : null,

                   ExternalIdentifier.Select(identifier => identifier.ToXML(DatexIINS.EnergyInfrastructure + "externalIdentifier")),

                   ConnectorExtension is not null
                       ? ConnectorExtension.ToExtensionXML(DatexIINS.EnergyInfrastructure + "_connectorExtension")
                       : null

               );

        #endregion

    }

}
