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

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Extension methods for VehicleTypes.
    /// </summary>
    public static class VehicleTypeExtensions
    {

        /// <summary>
        /// Indicates whether this VehicleType is null or empty.
        /// </summary>
        /// <param name="VehicleType">A VehicleType.</param>
        public static Boolean IsNullOrEmpty(this VehicleType? VehicleType)
            => !VehicleType.HasValue || VehicleType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this VehicleType is null or empty.
        /// </summary>
        /// <param name="VehicleType">A VehicleType.</param>
        public static Boolean IsNotNullOrEmpty(this VehicleType? VehicleType)
            => VehicleType.HasValue && VehicleType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A VehicleType.
    /// </summary>
    public readonly struct VehicleType : IId,
                                         IEquatable<VehicleType>,
                                         IComparable<VehicleType>
    {

        #region Data

        private readonly static Dictionary<String, VehicleType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this VehicleType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this VehicleType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the VehicleType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<VehicleType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new VehicleType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a VehicleType.</param>
        private VehicleType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static VehicleType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new VehicleType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a VehicleType.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleType.</param>
        public static VehicleType Parse(String Text)
        {

            if (TryParse(Text, out var vehicleType))
                return vehicleType;

            throw new ArgumentException($"Invalid text representation of a VehicleType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a VehicleType.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleType.</param>
        public static VehicleType? TryParse(String Text)
        {

            if (TryParse(Text, out var vehicleType))
                return vehicleType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out VehicleType)

        /// <summary>
        /// Try to parse the given text as VehicleType.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleType.</param>
        /// <param name="VehicleType">The parsed VehicleType.</param>
        public static Boolean TryParse(String Text, out VehicleType VehicleType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out VehicleType))
                    VehicleType = Register(Text);

                return true;

            }

            VehicleType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this VehicleType.
        /// </summary>
        public VehicleType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Agricultural Vehicle
        /// </summary>
        public static VehicleType  AgriculturalVehicle                   { get; }
            = Register("agriculturalVehicle");

        /// <summary>
        /// Any Vehicle
        /// </summary>
        public static VehicleType  AnyVehicle                            { get; }
            = Register("anyVehicle");

        /// <summary>
        /// Articulated Bus
        /// </summary>
        public static VehicleType  ArticulatedBus                        { get; }
            = Register("articulatedBus");

        /// <summary>
        /// Articulated Trolley Bus
        /// </summary>
        public static VehicleType  ArticulatedTrolleyBus                 { get; }
            = Register("articulatedTrolleyBus");

        /// <summary>
        /// Articulated Vehicle
        /// </summary>
        public static VehicleType  ArticulatedVehicle                    { get; }
            = Register("articulatedVehicle");

        /// <summary>
        /// Bicycle
        /// </summary>
        public static VehicleType  Bicycle                               { get; }
            = Register("bicycle");

        /// <summary>
        /// Bus
        /// </summary>
        public static VehicleType  Bus                                   { get; }
            = Register("bus");

        /// <summary>
        /// Car
        /// </summary>
        public static VehicleType  Car                                   { get; }
            = Register("car");

        /// <summary>
        /// Caravan
        /// </summary>
        public static VehicleType  Caravan                               { get; }
            = Register("caravan");

        /// <summary>
        /// Car or Light Vehicle
        /// </summary>
        public static VehicleType  CarOrLightVehicle                     { get; }
            = Register("carOrLightVehicle");

        /// <summary>
        /// Car with Caravan
        /// </summary>
        public static VehicleType  CarWithCaravan                        { get; }
            = Register("carWithCaravan");

        /// <summary>
        /// Car with Trailer
        /// </summary>
        public static VehicleType  CarWithTrailer                        { get; }
            = Register("carWithTrailer");

        /// <summary>
        /// Construction or Maintenance Vehicle
        /// </summary>
        public static VehicleType  ConstructionOrMaintenanceVehicle      { get; }
            = Register("constructionOrMaintenanceVehicle");

        /// <summary>
        /// Four Wheel Drive
        /// </summary>
        public static VehicleType  FourWheelDrive                        { get; }
            = Register("fourWheelDrive");

        /// <summary>
        /// Heavy Goods Vehicle
        /// </summary>
        public static VehicleType  HeavyGoodsVehicle                     { get; }
            = Register("heavyGoodsVehicle");

        /// <summary>
        /// Heavy Goods Vehicle With Trailer
        /// </summary>
        public static VehicleType  HeavyGoodsVehicleWithTrailer          { get; }
            = Register("heavyGoodsVehicleWithTrailer");

        /// <summary>
        /// Heavy Duty Transporter
        /// </summary>
        public static VehicleType  HeavyDutyTransporter                  { get; }
            = Register("heavyDutyTransporter");

        /// <summary>
        /// Heavy Vehicle
        /// </summary>
        public static VehicleType  HeavyVehicle                          { get; }
            = Register("heavyVehicle");

        /// <summary>
        /// High Sided Vehicle
        /// </summary>
        public static VehicleType  HighSidedVehicle                      { get; }
            = Register("highSidedVehicle");

        /// <summary>
        /// Light Commercial Vehicle
        /// </summary>
        public static VehicleType  LightCommercialVehicle                { get; }
            = Register("lightCommercialVehicle");

        /// <summary>
        /// Large Car
        /// </summary>
        public static VehicleType  LargeCar                              { get; }
            = Register("largeCar");

        /// <summary>
        /// Large Goods Vehicle
        /// </summary>
        public static VehicleType  LargeGoodsVehicle                     { get; }
            = Register("largeGoodsVehicle");

        /// <summary>
        /// Light Commercial Vehicle With Trailer
        /// </summary>
        public static VehicleType  LightCommercialVehicleWithTrailer     { get; }
            = Register("lightCommercialVehicleWithTrailer");

        /// <summary>
        /// Long Heavy Lorry
        /// </summary>
        public static VehicleType  LongHeavyLorry                        { get; }
            = Register("longHeavyLorry");

        /// <summary>
        /// Lorry
        /// </summary>
        public static VehicleType  Lorry                                 { get; }
            = Register("lorry");

        /// <summary>
        /// Metro
        /// </summary>
        public static VehicleType  Metro                                 { get; }
            = Register("metro");

        /// <summary>
        /// Minibus
        /// </summary>
        public static VehicleType  Minibus                               { get; }
            = Register("minibus");

        /// <summary>
        /// Moped
        /// </summary>
        public static VehicleType  Moped                                 { get; }
            = Register("moped");

        /// <summary>
        /// Motorcycle
        /// </summary>
        public static VehicleType  Motorcycle                            { get; }
            = Register("motorcycle");

        /// <summary>
        /// Motorcycle with Side Car
        /// </summary>
        public static VehicleType  MotorcycleWithSideCar                 { get; }
            = Register("motorcycleWithSideCar");

        /// <summary>
        /// Motorhome
        /// </summary>
        public static VehicleType  Motorhome                             { get; }
            = Register("motorhome");

        /// <summary>
        /// Motorscooter
        /// </summary>
        public static VehicleType  Motorscooter                          { get; }
            = Register("motorscooter");

        /// <summary>
        /// Passenger Car
        /// </summary>
        public static VehicleType  PassengerCar                          { get; }
            = Register("passengerCar");

        /// <summary>
        /// Small Car
        /// </summary>
        public static VehicleType  SmallCar                              { get; }
            = Register("smallCar");

        /// <summary>
        /// Tanker
        /// </summary>
        public static VehicleType  Tanker                                { get; }
            = Register("tanker");

        /// <summary>
        /// Three Wheeled Vehicle
        /// </summary>
        public static VehicleType  ThreeWheeledVehicle                   { get; }
            = Register("threeWheeledVehicle");

        /// <summary>
        /// Trailer
        /// </summary>
        public static VehicleType  Trailer                               { get; }
            = Register("trailer");

        /// <summary>
        /// Tram
        /// </summary>
        public static VehicleType  Tram                                  { get; }
            = Register("tram");

        /// <summary>
        /// Trolley Bus
        /// </summary>
        public static VehicleType  TrolleyBus                            { get; }
            = Register("trolleyBus");

        /// <summary>
        /// Two Wheeled Vehicle
        /// </summary>
        public static VehicleType  TwoWheeledVehicle                     { get; }
            = Register("twoWheeledVehicle");

        /// <summary>
        /// Van
        /// </summary>
        public static VehicleType  Van                                   { get; }
            = Register("van");

        /// <summary>
        /// Vehicle with Caravan
        /// </summary>
        public static VehicleType  VehicleWithCaravan                    { get; }
            = Register("vehicleWithCaravan");

        /// <summary>
        /// Vehicle with Catalytic Converter
        /// </summary>
        public static VehicleType  VehicleWithCatalyticConverter         { get; }
            = Register("vehicleWithCatalyticConverter");

        /// <summary>
        /// Vehicle without Catalytic Converter
        /// </summary>
        public static VehicleType  VehicleWithoutCatalyticConverter      { get; }
            = Register("vehicleWithoutCatalyticConverter");

        /// <summary>
        /// Vehicle with Trailer
        /// </summary>
        public static VehicleType  VehicleWithTrailer                    { get; }
            = Register("vehicleWithTrailer");

        /// <summary>
        /// With Even Numbered Registration Plates
        /// </summary>
        public static VehicleType  WithEvenNumberedRegistrationPlates    { get; }
            = Register("withEvenNumberedRegistrationPlates");

        /// <summary>
        /// With Odd Numbered Registration Plates
        /// </summary>
        public static VehicleType  WithOddNumberedRegistrationPlates     { get; }
            = Register("withOddNumberedRegistrationPlates");

        /// <summary>
        /// Unknown
        /// </summary>
        public static VehicleType  Unknown                               { get; }
            = Register("unknown");

        /// <summary>
        /// Other
        /// </summary>
        public static VehicleType  Other                                 { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (VehicleType1, VehicleType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleType1">A VehicleType.</param>
        /// <param name="VehicleType2">Another VehicleType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (VehicleType VehicleType1,
                                           VehicleType VehicleType2)

            => VehicleType1.Equals(VehicleType2);

        #endregion

        #region Operator != (VehicleType1, VehicleType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleType1">A VehicleType.</param>
        /// <param name="VehicleType2">Another VehicleType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (VehicleType VehicleType1,
                                           VehicleType VehicleType2)

            => !VehicleType1.Equals(VehicleType2);

        #endregion

        #region Operator <  (VehicleType1, VehicleType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleType1">A VehicleType.</param>
        /// <param name="VehicleType2">Another VehicleType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (VehicleType VehicleType1,
                                          VehicleType VehicleType2)

            => VehicleType1.CompareTo(VehicleType2) < 0;

        #endregion

        #region Operator <= (VehicleType1, VehicleType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleType1">A VehicleType.</param>
        /// <param name="VehicleType2">Another VehicleType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (VehicleType VehicleType1,
                                           VehicleType VehicleType2)

            => VehicleType1.CompareTo(VehicleType2) <= 0;

        #endregion

        #region Operator >  (VehicleType1, VehicleType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleType1">A VehicleType.</param>
        /// <param name="VehicleType2">Another VehicleType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (VehicleType VehicleType1,
                                          VehicleType VehicleType2)

            => VehicleType1.CompareTo(VehicleType2) > 0;

        #endregion

        #region Operator >= (VehicleType1, VehicleType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleType1">A VehicleType.</param>
        /// <param name="VehicleType2">Another VehicleType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (VehicleType VehicleType1,
                                           VehicleType VehicleType2)

            => VehicleType1.CompareTo(VehicleType2) >= 0;

        #endregion

        #endregion

        #region IComparable<VehicleType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two VehicleTypes.
        /// </summary>
        /// <param name="Object">VehicleType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is VehicleType vehicleType
                   ? CompareTo(vehicleType)
                   : throw new ArgumentException("The given object is not VehicleType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(VehicleType)

        /// <summary>
        /// Compares two VehicleTypes.
        /// </summary>
        /// <param name="VehicleType">VehicleType to compare with.</param>
        public Int32 CompareTo(VehicleType VehicleType)

            => String.Compare(InternalId,
                              VehicleType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<VehicleType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two VehicleTypes for equality.
        /// </summary>
        /// <param name="Object">VehicleType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is VehicleType vehicleType &&
                   Equals(vehicleType);

        #endregion

        #region Equals(VehicleType)

        /// <summary>
        /// Compares two VehicleTypes for equality.
        /// </summary>
        /// <param name="VehicleType">VehicleType to compare with.</param>
        public Boolean Equals(VehicleType VehicleType)

            => String.Equals(InternalId,
                             VehicleType.InternalId,
                             StringComparison.Ordinal);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
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
