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

using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// The characteristics of a vehicle, e.g. lorry of gross weight greater than 30 tonnes.
    /// </summary>
    [XmlType("VehicleCharacteristics", Namespace = "http://datex2.eu/schema/3/common")]
    public class VehicleCharacteristics(IEnumerable<FuelTypes>?                         FuelType                           = null,
                                        LoadTypes?                                      LoadType                           = null,
                                        VehicleEquipments?                              VehicleEquipment                   = null,
                                        IEnumerable<VehicleTypes>?                      VehicleType                        = null,
                                        IEnumerable<EUVehicleCategories>?               EUVehicleCategory                  = null,
                                        VehicleUsages?                                  VehicleUsage                       = null,
                                        UInt16?                                         YearOfFirstRegistration            = null,
                                        IEnumerable<GrossWeightCharacteristic>?         GrossWeightCharacteristic          = null,
                                        IEnumerable<HeightCharacteristic>?              HeightCharacteristic               = null,
                                        IEnumerable<LengthCharacteristic>?              LengthCharacteristic               = null,
                                        IEnumerable<WidthCharacteristic>?               WidthCharacteristic                = null,
                                        IEnumerable<HeaviestAxleWeightCharacteristic>?  HeaviestAxleWeightCharacteristic   = null,
                                        IEnumerable<NumberOfAxlesCharacteristic>?       NumberOfAxlesCharacteristic        = null,
                                        Emissions?                                      Emissions                          = null)
    {

        /// <summary>
        /// The type of fuel used by the vehicle.
        /// </summary>
        [XmlElement("fuelType",                          Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<FuelTypes>                         FuelType                            { get; set; } = FuelType?.Distinct() ?? [];

        /// <summary>
        /// The type of load carried by the vehicle, especially in respect of hazardous loads.
        /// </summary>
        [XmlElement("loadType",                          Namespace = "http://datex2.eu/schema/3/common")]
        public LoadTypes?                                     LoadType                            { get; set; } = LoadType;

        /// <summary>
        /// The type of equipment in use or on board the vehicle.
        /// </summary>
        [XmlElement("vehicleEquipment",                  Namespace = "http://datex2.eu/schema/3/common")]
        public VehicleEquipments?                             VehicleEquipment                    { get; set; } = VehicleEquipment;

        /// <summary>
        /// Vehicle type.
        /// </summary>
        [XmlElement("vehicleType",                       Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<VehicleTypes>                      VehicleType                         { get; set; } = VehicleType?.Distinct() ?? [];

        /// <summary>
        /// Vehicle categories as defined in EU Directive 2007/46/EG and Regulation (EU) No 168/2013.
        /// </summary>
        [XmlElement("euVehicleCategory",                 Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EUVehicleCategories>               EUVehicleCategory                   { get; set; } = EUVehicleCategory?.Distinct() ?? [];

        /// <summary>
        /// The type of usage of the vehicle (i.e. for what purpose is the vehicle being used).
        /// </summary>
        [XmlElement("vehicleUsage",                      Namespace = "http://datex2.eu/schema/3/common")]
        public VehicleUsages?                                 VehicleUsage                        { get; set; } = VehicleUsage;

        /// <summary>
        /// Year of first registration of the vehicle.
        /// </summary>
        [XmlElement("yearOfFirstRegistration",           Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16?                                        YearOfFirstRegistration             { get; set; } = YearOfFirstRegistration;

        /// <summary>
        /// Characteristic representing the gross weight of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("grossWeightCharacteristic",         Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<GrossWeightCharacteristic>         GrossWeightCharacteristic           { get; set; } = GrossWeightCharacteristic?.Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the height of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("heightCharacteristic",              Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<HeightCharacteristic>              HeightCharacteristic                { get; set; } = HeightCharacteristic?.Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the length of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("lengthCharacteristic",              Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<LengthCharacteristic>              LengthCharacteristic                { get; set; } = LengthCharacteristic?.Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the width of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("widthCharacteristic",               Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<WidthCharacteristic>               WidthCharacteristic                 { get; set; } = WidthCharacteristic?.Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the heaviest axle weight of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("heaviestAxleWeightCharacteristic",  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<HeaviestAxleWeightCharacteristic>  HeaviestAxleWeightCharacteristic    { get; set; } = HeaviestAxleWeightCharacteristic?.Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the number of axles of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("numberOfAxlesCharacteristic",       Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<NumberOfAxlesCharacteristic>       NumberOfAxlesCharacteristic         { get; set; } = NumberOfAxlesCharacteristic?.Distinct() ?? [];

        /// <summary>
        /// Emissions information for the vehicle.
        /// </summary>
        [XmlElement("emissions",                         Namespace = "http://datex2.eu/schema/3/common")]
        public Emissions?                                     Emissions                           { get; set; } = Emissions;

        /// <summary>
        /// Optional extension element for additional vehicle characteristics.
        /// </summary>
        [XmlElement("_vehicleCharacteristicsExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                      VehicleCharacteristicsExtension     { get; set; }

    }

}
