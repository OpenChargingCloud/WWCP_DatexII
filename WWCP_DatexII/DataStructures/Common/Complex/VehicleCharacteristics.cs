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
    public class VehicleCharacteristics(IEnumerable<FuelType>?                          FuelType                           = null,
                                        LoadType?                                       LoadType                           = null,
                                        VehicleEquipment?                               VehicleEquipment                   = null,
                                        IEnumerable<VehicleType>?                       VehicleType                        = null,
                                        IEnumerable<EUVehicleCategory>?                 EUVehicleCategory                  = null,
                                        VehicleUsage?                                   VehicleUsage                       = null,
                                        UInt16?                                         YearOfFirstRegistration            = null,
                                        IEnumerable<GrossWeightCharacteristic>?         GrossWeightCharacteristic          = null,
                                        IEnumerable<HeightCharacteristic>?              HeightCharacteristic               = null,
                                        IEnumerable<LengthCharacteristic>?              LengthCharacteristic               = null,
                                        IEnumerable<WidthCharacteristic>?               WidthCharacteristic                = null,
                                        IEnumerable<HeaviestAxleWeightCharacteristic>?  HeaviestAxleWeightCharacteristic   = null,
                                        IEnumerable<NumberOfAxlesCharacteristic>?       NumberOfAxlesCharacteristic        = null,
                                        Emissions?                                      Emissions                          = null,

                                        XElement?                                       VehicleCharacteristicsExtension    = null)
    {

        #region Properties

        /// <summary>
        /// The type of fuel used by the vehicle.
        /// </summary>
        [XmlElement("fuelType",                          Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<FuelType>                          FuelType                            { get; } = FuelType?.                        Distinct() ?? [];

        /// <summary>
        /// The type of load carried by the vehicle, especially in respect of hazardous loads.
        /// </summary>
        [XmlElement("loadType",                          Namespace = "http://datex2.eu/schema/3/common")]
        public LoadType?                                      LoadType                            { get; } = LoadType;

        /// <summary>
        /// The type of equipment in use or on board the vehicle.
        /// </summary>
        [XmlElement("vehicleEquipment",                  Namespace = "http://datex2.eu/schema/3/common")]
        public VehicleEquipment?                              VehicleEquipment                    { get; } = VehicleEquipment;

        /// <summary>
        /// Vehicle type.
        /// </summary>
        [XmlElement("vehicleType",                       Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<VehicleType>                       VehicleType                         { get; } = VehicleType?.                     Distinct() ?? [];

        /// <summary>
        /// Vehicle categories as defined in EU Directive 2007/46/EG and Regulation (EU) No 168/2013.
        /// </summary>
        [XmlElement("euVehicleCategory",                 Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EUVehicleCategory>                 EUVehicleCategory                   { get; } = EUVehicleCategory?.               Distinct() ?? [];

        /// <summary>
        /// The type of usage of the vehicle (i.e. for what purpose is the vehicle being used).
        /// </summary>
        [XmlElement("vehicleUsage",                      Namespace = "http://datex2.eu/schema/3/common")]
        public VehicleUsage?                                  VehicleUsage                        { get; } = VehicleUsage;

        /// <summary>
        /// Year of first registration of the vehicle.
        /// </summary>
        [XmlElement("yearOfFirstRegistration",           Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16?                                        YearOfFirstRegistration             { get; } = YearOfFirstRegistration;

        /// <summary>
        /// Characteristic representing the gross weight of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("grossWeightCharacteristic",         Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<GrossWeightCharacteristic>         GrossWeightCharacteristic           { get; } = GrossWeightCharacteristic?.       Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the height of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("heightCharacteristic",              Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<HeightCharacteristic>              HeightCharacteristic                { get; } = HeightCharacteristic?.            Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the length of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("lengthCharacteristic",              Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<LengthCharacteristic>              LengthCharacteristic                { get; } = LengthCharacteristic?.            Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the width of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("widthCharacteristic",               Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<WidthCharacteristic>               WidthCharacteristic                 { get; } = WidthCharacteristic?.             Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the heaviest axle weight of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("heaviestAxleWeightCharacteristic",  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<HeaviestAxleWeightCharacteristic>  HeaviestAxleWeightCharacteristic    { get; } = HeaviestAxleWeightCharacteristic?.Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the number of axles of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("numberOfAxlesCharacteristic",       Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<NumberOfAxlesCharacteristic>       NumberOfAxlesCharacteristic         { get; } = NumberOfAxlesCharacteristic?.     Distinct() ?? [];

        /// <summary>
        /// Emissions information for the vehicle.
        /// </summary>
        [XmlElement("emissions",                         Namespace = "http://datex2.eu/schema/3/common")]
        public Emissions?                                     Emissions                           { get; } = Emissions;

        /// <summary>
        /// Optional extension element for additional vehicle characteristics.
        /// </summary>
        [XmlElement("_vehicleCharacteristicsExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                      VehicleCharacteristicsExtension     { get; } = VehicleCharacteristicsExtension;

        #endregion

    }

}
