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
    /// Types of vehicles.
    /// </summary>
    public enum VehicleTypes
    {

        /// <summary>
        /// Vehicle normally used for agricultural purposes, e.g. tractor, combined harvester etc.
        /// </summary>
        [XmlEnum("agriculturalVehicle")]
        AgriculturalVehicle,

        /// <summary>
        /// Vehicle of any type.
        /// </summary>
        [XmlEnum("anyVehicle")]
        AnyVehicle,

        /// <summary>
        /// Articulated bus.
        /// </summary>
        [XmlEnum("articulatedBus")]
        ArticulatedBus,

        /// <summary>
        /// Articulated trolley bus.
        /// </summary>
        [XmlEnum("articulatedTrolleyBus")]
        ArticulatedTrolleyBus,

        /// <summary>
        /// Articulated vehicle.
        /// </summary>
        [XmlEnum("articulatedVehicle")]
        ArticulatedVehicle,

        /// <summary>
        /// Bicycle.
        /// </summary>
        [XmlEnum("bicycle")]
        Bicycle,

        /// <summary>
        /// Bus.
        /// </summary>
        [XmlEnum("bus")]
        Bus,

        /// <summary>
        /// Vehicles designed and constructed for the carriage of passengers and comprising no more than eight seats in addition to the driver’s seat, 
        /// and having a maximum mass (“technically permissible maximum laden mass”) not exceeding 3.5 tons (M1).
        /// </summary>
        [XmlEnum("car")]
        Car,

        /// <summary>
        /// Caravan.
        /// </summary>
        [XmlEnum("caravan")]
        Caravan,

        /// <summary>
        /// Car or light vehicle.
        /// </summary>
        [XmlEnum("carOrLightVehicle")]
        CarOrLightVehicle,

        /// <summary>
        /// Car towing a caravan.
        /// </summary>
        [XmlEnum("carWithCaravan")]
        CarWithCaravan,

        /// <summary>
        /// Car towing a trailer.
        /// </summary>
        [XmlEnum("carWithTrailer")]
        CarWithTrailer,

        /// <summary>
        /// Vehicle normally used for construction or maintenance purposes, e.g. digger, excavator, bulldozer, lorry mounted crane etc.
        /// </summary>
        [XmlEnum("constructionOrMaintenanceVehicle")]
        ConstructionOrMaintenanceVehicle,

        /// <summary>
        /// Four wheel drive vehicle.
        /// </summary>
        [XmlEnum("fourWheelDrive")]
        FourWheelDrive,

        /// <summary>
        /// Vehicles with a total weight above 3,500 kg (vehicle and load).
        /// </summary>
        [XmlEnum("heavyGoodsVehicle")]
        HeavyGoodsVehicle,

        /// <summary>
        /// Heavy goods vehicle with trailer.
        /// </summary>
        [XmlEnum("heavyGoodsVehicleWithTrailer")]
        HeavyGoodsVehicleWithTrailer,

        /// <summary>
        /// A transporter for heavy duty (usually with abnormal dimensions).
        /// </summary>
        [XmlEnum("heavyDutyTransporter")]
        HeavyDutyTransporter,

        /// <summary>
        /// Vehicle whose weight means it should be classed as a heavy vehicle.
        /// </summary>
        [XmlEnum("heavyVehicle")]
        HeavyVehicle,

        /// <summary>
        /// High sided vehicle.
        /// </summary>
        [XmlEnum("highSidedVehicle")]
        HighSidedVehicle,

        /// <summary>
        /// Vehicles for the carriage of goods and having a maximum mass not exceeding 3.5 tonnes (class N1).
        /// </summary>
        [XmlEnum("lightCommercialVehicle")]
        LightCommercialVehicle,

        /// <summary>
        /// Large car.
        /// </summary>
        [XmlEnum("largeCar")]
        LargeCar,

        /// <summary>
        /// Vehicles for the carriage of goods and having a maximum mass exceeding 3.5 tonnes 
        /// (belonging to class N2 when not exceeding 12 tonnes, otherwise class N3).
        /// </summary>
        [XmlEnum("largeGoodsVehicle")]
        LargeGoodsVehicle,

        /// <summary>
        /// Light goods vehicle with trailer.
        /// </summary>
        [XmlEnum("lightCommercialVehicleWithTrailer")]
        LightCommercialVehicleWithTrailer,

        /// <summary>
        /// A heavy lorry that is longer than normal.
        /// </summary>
        [XmlEnum("longHeavyLorry")]
        LongHeavyLorry,

        /// <summary>
        /// Lorry of any type.
        /// </summary>
        [XmlEnum("lorry")]
        Lorry,

        /// <summary>
        /// Metro.
        /// </summary>
        [XmlEnum("metro")]
        Metro,

        /// <summary>
        /// Vehicles designed and constructed for the carriage of passengers, comprising more than eight seats in addition to the driver’s seat, 
        /// and having a maximum mass not exceeding 5 tonnes (class M2).
        /// </summary>
        [XmlEnum("minibus")]
        Minibus,

        /// <summary>
        /// Moped (a two wheeled motor vehicle characterized by a small engine typically less than 50cc and normally having pedals).
        /// </summary>
        [XmlEnum("moped")]
        Moped,

        /// <summary>
        /// Motorcycle.
        /// </summary>
        [XmlEnum("motorcycle")]
        Motorcycle,

        /// <summary>
        /// Three wheeled vehicle comprising a motorcycle with an attached side car.
        /// </summary>
        [XmlEnum("motorcycleWithSideCar")]
        MotorcycleWithSideCar,

        /// <summary>
        /// Motorhome.
        /// </summary>
        [XmlEnum("motorhome")]
        Motorhome,

        /// <summary>
        /// Motorscooter (a two wheeled motor vehicle characterized by a step-through frame and small diameter wheels).
        /// </summary>
        [XmlEnum("motorscooter")]
        Motorscooter,

        /// <summary>
        /// Passenger car.
        /// </summary>
        [XmlEnum("passengerCar")]
        PassengerCar,

        /// <summary>
        /// Small car.
        /// </summary>
        [XmlEnum("smallCar")]
        SmallCar,

        /// <summary>
        /// Vehicle with large tank for carrying bulk liquids.
        /// </summary>
        [XmlEnum("tanker")]
        Tanker,

        /// <summary>
        /// Three wheeled vehicle of unspecified type.
        /// </summary>
        [XmlEnum("threeWheeledVehicle")]
        ThreeWheeledVehicle,

        /// <summary>
        /// Trailer.
        /// </summary>
        [XmlEnum("trailer")]
        Trailer,

        /// <summary>
        /// Tram.
        /// </summary>
        [XmlEnum("tram")]
        Tram,

        /// <summary>
        /// Trolley bus.
        /// </summary>
        [XmlEnum("trolleyBus")]
        TrolleyBus,

        /// <summary>
        /// Two wheeled vehicle of unspecified type.
        /// </summary>
        [XmlEnum("twoWheeledVehicle")]
        TwoWheeledVehicle,

        /// <summary>
        /// Van.
        /// </summary>
        [XmlEnum("van")]
        Van,

        /// <summary>
        /// Vehicle (of unspecified type) towing a caravan.
        /// </summary>
        [XmlEnum("vehicleWithCaravan")]
        VehicleWithCaravan,

        /// <summary>
        /// Vehicle with catalytic converter.
        /// </summary>
        [XmlEnum("vehicleWithCatalyticConverter")]
        VehicleWithCatalyticConverter,

        /// <summary>
        /// Vehicle without catalytic converter.
        /// </summary>
        [XmlEnum("vehicleWithoutCatalyticConverter")]
        VehicleWithoutCatalyticConverter,

        /// <summary>
        /// Vehicle (of unspecified type) towing a trailer.
        /// </summary>
        [XmlEnum("vehicleWithTrailer")]
        VehicleWithTrailer,

        /// <summary>
        /// Vehicle with even numbered registration plate.
        /// </summary>
        [XmlEnum("withEvenNumberedRegistrationPlates")]
        WithEvenNumberedRegistrationPlates,

        /// <summary>
        /// Vehicle with odd numbered registration plate.
        /// </summary>
        [XmlEnum("withOddNumberedRegistrationPlates")]
        WithOddNumberedRegistrationPlates,

        /// <summary>
        /// Unknown.
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        /// <summary>
        /// Other than as defined in this enumeration.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
