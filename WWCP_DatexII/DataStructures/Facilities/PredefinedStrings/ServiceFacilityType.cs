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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Extension methods for ServiceFacilityTypes.
    /// </summary>
    public static class ServiceFacilityTypeExtensions
    {

        /// <summary>
        /// Indicates whether this ServiceFacilityType is null or empty.
        /// </summary>
        /// <param name="ServiceFacilityType">A ServiceFacilityType.</param>
        public static Boolean IsNullOrEmpty(this ServiceFacilityType? ServiceFacilityType)
            => !ServiceFacilityType.HasValue || ServiceFacilityType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this ServiceFacilityType is null or empty.
        /// </summary>
        /// <param name="ServiceFacilityType">A ServiceFacilityType.</param>
        public static Boolean IsNotNullOrEmpty(this ServiceFacilityType? ServiceFacilityType)
            => ServiceFacilityType.HasValue && ServiceFacilityType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A ServiceFacilityType.
    /// </summary>
    public readonly struct ServiceFacilityType : IId,
                                                 IEquatable<ServiceFacilityType>,
                                                 IComparable<ServiceFacilityType>
    {

        #region Data

        private readonly static Dictionary<String, ServiceFacilityType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this ServiceFacilityType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this ServiceFacilityType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the ServiceFacilityType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered ServiceFacilityTypes.
        /// </summary>
        public static    IEnumerable<ServiceFacilityType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ServiceFacilityType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a ServiceFacilityType.</param>
        private ServiceFacilityType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ServiceFacilityType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ServiceFacilityType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a ServiceFacilityType.
        /// </summary>
        /// <param name="Text">A text representation of a ServiceFacilityType.</param>
        public static ServiceFacilityType Parse(String Text)
        {

            if (TryParse(Text, out var serviceFacilityType))
                return serviceFacilityType;

            throw new ArgumentException($"Invalid text representation of a ServiceFacilityType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a ServiceFacilityType.
        /// </summary>
        /// <param name="Text">A text representation of a ServiceFacilityType.</param>
        public static ServiceFacilityType? TryParse(String Text)
        {

            if (TryParse(Text, out var serviceFacilityType))
                return serviceFacilityType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ServiceFacilityType)

        /// <summary>
        /// Try to parse the given text as a ServiceFacilityType.
        /// </summary>
        /// <param name="Text">A text representation of a ServiceFacilityType.</param>
        /// <param name="ServiceFacilityType">The parsed ServiceFacilityType.</param>
        public static Boolean TryParse(String Text, out ServiceFacilityType ServiceFacilityType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ServiceFacilityType))
                    ServiceFacilityType = Register(Text);

                return true;

            }

            ServiceFacilityType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this ServiceFacilityType.
        /// </summary>
        public ServiceFacilityType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Bike Garage
        /// </summary>
        public static ServiceFacilityType  BikeGarage                 { get; }
            = Register("bikeGarage");

        /// <summary>
        /// Bike Sharing
        /// </summary>
        public static ServiceFacilityType  BikeSharing                { get; }
            = Register("bikeSharing");

        /// <summary>
        /// Cafe
        /// </summary>
        public static ServiceFacilityType  Cafe                       { get; }
            = Register("cafe");

        /// <summary>
        /// Car Wash
        /// </summary>
        public static ServiceFacilityType  CarWash                    { get; }
            = Register("carWash");

        /// <summary>
        /// Catering Service
        /// </summary>
        public static ServiceFacilityType  CateringService            { get; }
            = Register("cateringService");

        /// <summary>
        /// Docstop
        /// </summary>
        public static ServiceFacilityType  Docstop                    { get; }
            = Register("docstop");

        /// <summary>
        /// Food Shopping
        /// </summary>
        public static ServiceFacilityType  FoodShopping               { get; }
            = Register("foodShopping");

        /// <summary>
        /// Hotel
        /// </summary>
        public static ServiceFacilityType  Hotel                      { get; }
            = Register("hotel");

        /// <summary>
        /// Kiosk
        /// </summary>
        public static ServiceFacilityType  Kiosk                      { get; }
            = Register("kiosk");

        /// <summary>
        /// Laundry
        /// </summary>
        public static ServiceFacilityType  Laundry                    { get; }
            = Register("laundry");

        /// <summary>
        /// Leisure Activities
        /// </summary>
        public static ServiceFacilityType  LeisureActivities          { get; }
            = Register("leisureActivities");

        /// <summary>
        /// Medical Facility
        /// </summary>
        public static ServiceFacilityType  MedicalFacility            { get; }
            = Register("medicalFacility");

        /// <summary>
        /// Motel
        /// </summary>
        public static ServiceFacilityType  Motel                      { get; }
            = Register("motel");

        /// <summary>
        /// Motorcycle Garage
        /// </summary>
        public static ServiceFacilityType  MotorcycleGarage           { get; }
            = Register("motorcycleGarage");

        /// <summary>
        /// Motorway Restaurant
        /// </summary>
        public static ServiceFacilityType  MotorwayRestaurant         { get; }
            = Register("motorwayRestaurant");

        /// <summary>
        /// Motorway Restaurant Small
        /// </summary>
        public static ServiceFacilityType  MotorwayRestaurantSmall    { get; }
            = Register("motorwayRestaurantSmall");

        /// <summary>
        /// Overnight Accommodation
        /// </summary>
        public static ServiceFacilityType  OvernightAccommodation     { get; }
            = Register("overnightAccommodation");

        /// <summary>
        /// Petrol Station
        /// </summary>
        public static ServiceFacilityType  PetrolStation              { get; }
            = Register("petrolStation");

        /// <summary>
        /// Pharmacy
        /// </summary>
        public static ServiceFacilityType  Pharmacy                   { get; }
            = Register("pharmacy");

        /// <summary>
        /// Pay Desk
        /// </summary>
        public static ServiceFacilityType  PayDesk                    { get; }
            = Register("payDesk");

        /// <summary>
        /// Indicates whether a police station is on site or very close.
        /// </summary>
        public static ServiceFacilityType  Police                     { get; }
            = Register("police");

        /// <summary>
        /// Restaurant.
        /// </summary>
        public static ServiceFacilityType  Restaurant                 { get; }
            = Register("restaurant");

        /// <summary>
        /// Restaurant Self Service
        /// </summary>
        public static ServiceFacilityType  RestaurantSelfService      { get; }
            = Register("restaurantSelfService");

        /// <summary>
        /// Shop
        /// </summary>
        public static ServiceFacilityType  Shop                       { get; }
            = Register("shop");

        /// <summary>
        /// Snack Bar
        /// </summary>
        public static ServiceFacilityType  SnackBar                   { get; }
            = Register("snackBar");

        /// <summary>
        /// Spare Parts Shopping
        /// </summary>
        public static ServiceFacilityType  SparePartsShopping         { get; }
            = Register("sparePartsShopping");

        /// <summary>
        /// Tourist Information
        /// </summary>
        public static ServiceFacilityType  TouristInformation         { get; }
            = Register("touristInformation");

        /// <summary>
        /// Truck Repair
        /// </summary>
        public static ServiceFacilityType  TruckRepair                { get; }
            = Register("truckRepair");

        /// <summary>
        /// Truck Wash
        /// </summary>
        public static ServiceFacilityType  TruckWash                  { get; }
            = Register("truckWash");

        /// <summary>
        /// Tyre Repair
        /// </summary>
        public static ServiceFacilityType  TyreRepair                 { get; }
            = Register("tyreRepair");

        /// <summary>
        /// Vehicle Maintenance
        /// </summary>
        public static ServiceFacilityType  VehicleMaintenance         { get; }
            = Register("vehicleMaintenance");

        /// <summary>
        /// Unknown
        /// </summary>
        public static ServiceFacilityType  Unknown                    { get; }
            = Register("unknown");

        /// <summary>
        /// Other
        /// </summary>
        public static ServiceFacilityType  Other                      { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (ServiceFacilityType1, ServiceFacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceFacilityType1">A ServiceFacilityType.</param>
        /// <param name="ServiceFacilityType2">Another ServiceFacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ServiceFacilityType ServiceFacilityType1,
                                           ServiceFacilityType ServiceFacilityType2)

            => ServiceFacilityType1.Equals(ServiceFacilityType2);

        #endregion

        #region Operator != (ServiceFacilityType1, ServiceFacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceFacilityType1">A ServiceFacilityType.</param>
        /// <param name="ServiceFacilityType2">Another ServiceFacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ServiceFacilityType ServiceFacilityType1,
                                           ServiceFacilityType ServiceFacilityType2)

            => !ServiceFacilityType1.Equals(ServiceFacilityType2);

        #endregion

        #region Operator <  (ServiceFacilityType1, ServiceFacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceFacilityType1">A ServiceFacilityType.</param>
        /// <param name="ServiceFacilityType2">Another ServiceFacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ServiceFacilityType ServiceFacilityType1,
                                          ServiceFacilityType ServiceFacilityType2)

            => ServiceFacilityType1.CompareTo(ServiceFacilityType2) < 0;

        #endregion

        #region Operator <= (ServiceFacilityType1, ServiceFacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceFacilityType1">A ServiceFacilityType.</param>
        /// <param name="ServiceFacilityType2">Another ServiceFacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ServiceFacilityType ServiceFacilityType1,
                                           ServiceFacilityType ServiceFacilityType2)

            => ServiceFacilityType1.CompareTo(ServiceFacilityType2) <= 0;

        #endregion

        #region Operator >  (ServiceFacilityType1, ServiceFacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceFacilityType1">A ServiceFacilityType.</param>
        /// <param name="ServiceFacilityType2">Another ServiceFacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ServiceFacilityType ServiceFacilityType1,
                                          ServiceFacilityType ServiceFacilityType2)

            => ServiceFacilityType1.CompareTo(ServiceFacilityType2) > 0;

        #endregion

        #region Operator >= (ServiceFacilityType1, ServiceFacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceFacilityType1">A ServiceFacilityType.</param>
        /// <param name="ServiceFacilityType2">Another ServiceFacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ServiceFacilityType ServiceFacilityType1,
                                           ServiceFacilityType ServiceFacilityType2)

            => ServiceFacilityType1.CompareTo(ServiceFacilityType2) >= 0;

        #endregion

        #endregion

        #region IComparable<ServiceFacilityType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two ServiceFacilityTypes.
        /// </summary>
        /// <param name="Object">A ServiceFacilityType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ServiceFacilityType serviceFacilityType
                   ? CompareTo(serviceFacilityType)
                   : throw new ArgumentException("The given object is not a ServiceFacilityType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ServiceFacilityType)

        /// <summary>
        /// Compares two ServiceFacilityTypes.
        /// </summary>
        /// <param name="ServiceFacilityType">A ServiceFacilityType to compare with.</param>
        public Int32 CompareTo(ServiceFacilityType ServiceFacilityType)

            => String.Compare(InternalId,
                              ServiceFacilityType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<ServiceFacilityType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two ServiceFacilityTypes for equality.
        /// </summary>
        /// <param name="Object">A ServiceFacilityType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ServiceFacilityType serviceFacilityType &&
                   Equals(serviceFacilityType);

        #endregion

        #region Equals(ServiceFacilityType)

        /// <summary>
        /// Compares two ServiceFacilityTypes for equality.
        /// </summary>
        /// <param name="ServiceFacilityType">A ServiceFacilityType to compare with.</param>
        public Boolean Equals(ServiceFacilityType ServiceFacilityType)

            => String.Equals(InternalId,
                             ServiceFacilityType.InternalId,
                             StringComparison.Ordinal);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()

            => InternalId?.ToLower().GetHashCode() ?? 0;

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => InternalId ?? "";

        #endregion

    }

}
