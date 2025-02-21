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
using org.GraphDefined.Vanaheimr.Hermod.HTTP;
using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// Technical infrastructure at a specific location that facilitates electric charging of one vehicle at a time.
    /// </summary>
    [XmlType("ElectricChargingPoint", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class ElectricChargingPoint(String                                Id,
                                       String                                Version,
                                       DeliveryUnitEnum                      DeliveryUnit,

                                       MultilingualString?                   Name                     = null,
                                       IEnumerable<MultilingualString>?      Alias                    = null,
                                       String?                               ExternalIdentifier       = null,
                                       DateTime?                             LastUpdated              = null,
                                       MultilingualString?                   Description              = null,
                                       IEnumerable<Accessibilities>?         Accessibility            = null,
                                       IEnumerable<MultilingualString>?      AdditionalInformation    = null,
                                       IEnumerable<URL>?                     InformationWebsites      = null,
                                       IEnumerable<URL>?                     PhotoURLs                = null,
                                       IEnumerable<Image>?                   Photos                   = null,
                                       AOperatingHours?                      OperatingHours           = null,
                                       LocationReference?                    LocationReference        = null,
                                       AOrganisation?                        Owner                    = null,
                                       AOrganisation?                        Operator                 = null,
                                       AOrganisation?                        Helpdesk                 = null,
                                       IEnumerable<VehicleCharacteristics>?  ApplicableForVehicles    = null,
                                       Dimension?                            Dimension                = null,
                                       Amenities?                            Amenities                = null,

                                       IEnumerable<ASupplementalFacility>?   SupplementalFacilities   = null,
                                       IEnumerable<DedicatedParkingSpaces>?  DedicatedParkingSpaces   = null,

                                       Units?                                MinimumDeliveryAmount    = null,
                                       Units?                                MaximumDeliveryAmount    = null,
                                       MultilingualString?                   ModelType                = null,
                                       ReservationTypes?                     Reservation              = null)

        : RefillPoint(Id,
                      Version,
                      DeliveryUnit,

                      Name,
                      Alias,
                      ExternalIdentifier,
                      LastUpdated,
                      Description,
                      Accessibility,
                      AdditionalInformation,
                      InformationWebsites,
                      PhotoURLs,
                      Photos,
                      OperatingHours,
                      LocationReference,
                      Owner,
                      Operator,
                      Helpdesk,
                      ApplicableForVehicles,
                      Dimension,
                      Amenities,

                      SupplementalFacilities,
                      DedicatedParkingSpaces,

                      MinimumDeliveryAmount,
                      MaximumDeliveryAmount,
                      ModelType,
                      Reservation)

    {

        /// <summary>
        /// Electric Vehicle Supply Equipment Identifier according to ISO 15118.
        /// </summary>
        [XmlElement("evseId", Namespace = "http://datex2.eu/schema/3/common")]
        public String?                                  EvseId { get; set; }

        /// <summary>
        /// Usage type of the electric charging point.
        /// </summary>
        [XmlElement("usageType", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<ChargingPointUsageTypes>     UsageType { get; set; }

        /// <summary>
        /// Type of vehicle to grid communication used.
        /// </summary>
        [XmlElement("vehicleToGridCommunicationType", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<VehicleToGridCommunication>  VehicleToGridCommunicationType { get; set; }

        /// <summary>
        /// Information on the number of connectors at this charging point.
        /// </summary>
        [XmlElement("numberOfConnectors", Namespace = "http://datex2.eu/schema/3/common")]
        public Byte?                                    NumberOfConnectors { get; set; }

        /// <summary>
        /// Possible degrees of voltage.
        /// </summary>
        [XmlElement("availableVoltage", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<Volt>                        AvailableVoltage { get; set; }

        /// <summary>
        /// Possible degrees of charging power in Watts.
        /// </summary>
        [XmlElement("availableChargingPower", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<Watt>                        AvailableChargingPower { get; set; }

        /// <summary>
        /// Smart recharging services that are available.
        /// </summary>
        [XmlElement("smartRechargingServices", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<SmartRechargingServices>     SmartRechargingServices { get; set; }

        /// <summary>
        /// Other smart recharging services that are available.
        /// </summary>
        [XmlElement("otherSmartRechargingServices", Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<MultilingualString>          OtherSmartRechargingServices { get; set; }

        /// <summary>
        /// Specify the connector(s).
        /// </summary>
        [XmlElement("connector", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<Connector>                   Connector { get; set; }

        /// <summary>
        /// Electric energy definitions applicable at this charging point.
        /// </summary>
        [XmlElement("electricEnergy", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<ElectricEnergy>              ElectricEnergy { get; set; }

        ///// <summary>
        ///// Optional extension element for additional electric charging point information.
        ///// </summary>
        //[XmlElement("_electricChargingPointExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? ElectricChargingPointExtension { get; set; }

    }

}
