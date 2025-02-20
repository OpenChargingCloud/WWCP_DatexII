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
    /// A service facility. In distinction to equipment, a service is usually manned.
    /// </summary>
    public enum ServiceFacilityTypes
    {
        /// <summary>
        /// A place where bikes are repaired.
        /// </summary>
        [XmlEnum("bikeGarage")]
        BikeGarage,

        /// <summary>
        /// Bike Sharing.
        /// </summary>
        [XmlEnum("bikeSharing")]
        BikeSharing,

        /// <summary>
        /// Cafe.
        /// </summary>
        [XmlEnum("cafe")]
        Cafe,

        /// <summary>
        /// Car wash.
        /// </summary>
        [XmlEnum("carWash")]
        CarWash,

        /// <summary>
        /// Catering service.
        /// </summary>
        [XmlEnum("cateringService")]
        CateringService,

        /// <summary>
        /// The site is part of the Docstop project (http://www.docstoponline.eu), which means medical assistance for professional drivers.
        /// </summary>
        [XmlEnum("docstop")]
        Docstop,

        /// <summary>
        /// Food shopping.
        /// </summary>
        [XmlEnum("foodShopping")]
        FoodShopping,

        /// <summary>
        /// A hotel.
        /// </summary>
        [XmlEnum("hotel")]
        Hotel,

        /// <summary>
        /// Kiosk.
        /// </summary>
        [XmlEnum("kiosk")]
        Kiosk,

        /// <summary>
        /// A possibility for washing clothes (might also be a laundromat with coins).
        /// </summary>
        [XmlEnum("laundry")]
        Laundry,

        /// <summary>
        /// There are leisure activities offered on the site or in the very near surrounding. Use the additional description attribute to give details.
        /// </summary>
        [XmlEnum("leisureActivities")]
        LeisureActivities,

        /// <summary>
        /// Medical facility.
        /// </summary>
        [XmlEnum("medicalFacility")]
        MedicalFacility,

        /// <summary>
        /// Hotel located aside a motorway.
        /// </summary>
        [XmlEnum("motel")]
        Motel,

        /// <summary>
        /// A place where motorcycles are repaired.
        /// </summary>
        [XmlEnum("motorcycleGarage")]
        MotorcycleGarage,

        /// <summary>
        /// Restaurant located on a motorway rest area.
        /// </summary>
        [XmlEnum("motorwayRestaurant")]
        MotorwayRestaurant,

        /// <summary>
        /// Smaller type of restaurant located on a motorway rest area. Might be with limited offers.
        /// </summary>
        [XmlEnum("motorwayRestaurantSmall")]
        MotorwayRestaurantSmall,

        /// <summary>
        /// An accommodation to stay overnight.
        /// </summary>
        [XmlEnum("overnightAccommodation")]
        OvernightAccommodation,

        /// <summary>
        /// Indicates whether it is possible to get petrol.
        /// </summary>
        [XmlEnum("petrolStation")]
        PetrolStation,

        /// <summary>
        /// Pharmacy.
        /// </summary>
        [XmlEnum("pharmacy")]
        Pharmacy,

        /// <summary>
        /// A manned possibility to pay.
        /// </summary>
        [XmlEnum("payDesk")]
        PayDesk,

        /// <summary>
        /// Indicates whether a police station is on site or very close.
        /// </summary>
        [XmlEnum("police")]
        Police,

        /// <summary>
        /// Restaurant.
        /// </summary>
        [XmlEnum("restaurant")]
        Restaurant,

        /// <summary>
        /// A restaurant where people arrange and fetch their meal themselves; this might include a buffet.
        /// </summary>
        [XmlEnum("restaurantSelfService")]
        RestaurantSelfService,

        /// <summary>
        /// A shop of unspecified kind.
        /// </summary>
        [XmlEnum("shop")]
        Shop,

        /// <summary>
        /// A snack bar.
        /// </summary>
        [XmlEnum("snackBar")]
        SnackBar,

        /// <summary>
        /// Spare parts shopping.
        /// </summary>
        [XmlEnum("sparePartsShopping")]
        SparePartsShopping,

        /// <summary>
        /// Tourist information with employees.
        /// </summary>
        [XmlEnum("touristInformation")]
        TouristInformation,

        /// <summary>
        /// Truck repair.
        /// </summary>
        [XmlEnum("truckRepair")]
        TruckRepair,

        /// <summary>
        /// Truck wash.
        /// </summary>
        [XmlEnum("truckWash")]
        TruckWash,

        /// <summary>
        /// A tyre repair service.
        /// </summary>
        [XmlEnum("tyreRepair")]
        TyreRepair,

        /// <summary>
        /// Garage repair service.
        /// </summary>
        [XmlEnum("vehicleMaintenance")]
        VehicleMaintenance,

        /// <summary>
        /// Unknown.
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        /// <summary>
        /// Some other service facility. Use the description attribute to specify it.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
