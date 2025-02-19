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

    public class RefillPoint
    {

        [XmlAttribute("id")]
        public String?                       Id                                { get; set; }


        [XmlAttribute("version")]
        public String?                       Version                           { get; set; }


        [XmlElement(ElementName = "deliveryUnit",                    Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public String?                       DeliveryUnit                      { get; set; }


        [XmlElement(ElementName = "modelType",                       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public AdditionalInformation?        ModelType                         { get; set; }


        [XmlElement(ElementName = "evseId",                          Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public String?                       EvseId                            { get; set; }


        [XmlElement(ElementName = "usageType",                       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public String?                       UsageType                         { get; set; }


        [XmlElement(ElementName = "vehicleToGridCommunicationType",  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public String?                       VehicleToGridCommunicationType    { get; set; }


        [XmlElement(ElementName = "numberOfConnectors",              Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Int32?                        NumberOfConnectors                { get; set; }


        [XmlElement(ElementName = "availableVoltage",                Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<Int32>?           AvailableVoltage                  { get; set; }


        [XmlElement(ElementName = "availableChargingPower",          Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<Int32>?           AvailableChargingPower            { get; set; }


        [XmlElement(ElementName = "smartRechargingServices",         Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<String>?          SmartRechargingServices           { get; set; }


        [XmlElement(ElementName = "otherSmartRechargingServices",    Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public AdditionalInformation?        OtherSmartRechargingServices      { get; set; }


        [XmlElement(ElementName = "connector",                       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<Connector>?       Connector                         { get; set; }


        [XmlElement(ElementName = "electricEnergy",                  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<ElectricEnergy>?  ElectricEnergies                  { get; set; }

    }

}
