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
    /// Extension methods for FuelTypes.
    /// </summary>
    public static class FuelTypeExtensions
    {

        /// <summary>
        /// Indicates whether this FuelType is null or empty.
        /// </summary>
        /// <param name="FuelType">A FuelType.</param>
        public static Boolean IsNullOrEmpty(this FuelType? FuelType)
            => !FuelType.HasValue || FuelType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this FuelType is null or empty.
        /// </summary>
        /// <param name="FuelType">A FuelType.</param>
        public static Boolean IsNotNullOrEmpty(this FuelType? FuelType)
            => FuelType.HasValue && FuelType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A FuelType.
    /// </summary>
    public readonly struct FuelType : IId,
                                      IEquatable<FuelType>,
                                      IComparable<FuelType>
    {

        #region Data

        private readonly static Dictionary<String, FuelType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this FuelType is null or empty.
        /// </summary>
        public readonly  Boolean                IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this FuelType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the FuelType.
        /// </summary>
        public readonly  UInt64                 Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<FuelType>  AllFuelTypes
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new FuelType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a FuelType.</param>
        private FuelType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static FuelType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new FuelType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a FuelType.
        /// </summary>
        /// <param name="Text">A text representation of a FuelType.</param>
        public static FuelType Parse(String Text)
        {

            if (TryParse(Text, out var fuelType))
                return fuelType;

            throw new ArgumentException($"Invalid text representation of a FuelType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a FuelType.
        /// </summary>
        /// <param name="Text">A text representation of a FuelType.</param>
        public static FuelType? TryParse(String Text)
        {

            if (TryParse(Text, out var fuelType))
                return fuelType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out FuelType)

        /// <summary>
        /// Try to parse the given text as FuelType.
        /// </summary>
        /// <param name="Text">A text representation of a FuelType.</param>
        /// <param name="FuelType">The parsed FuelType.</param>
        public static Boolean TryParse(String Text, out FuelType FuelType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out FuelType))
                    FuelType = Register(Text);

                return true;

            }

            FuelType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this FuelType.
        /// </summary>
        public FuelType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// All sort of fuel is accepted
        /// </summary>
        public static FuelType  All                    { get; }
            = Register("all");

        /// <summary>
        /// Battery
        /// </summary>
        public static FuelType  Battery                { get; }
            = Register("battery");

        /// <summary>
        /// Biodiesel
        /// </summary>
        public static FuelType  Biodiesel              { get; }
            = Register("biodiesel");

        /// <summary>
        /// Diesel
        /// </summary>
        public static FuelType  Diesel                 { get; }
            = Register("diesel");

        /// <summary>
        /// Diesel and Battery Hybrid
        /// </summary>
        public static FuelType  DieselBatteryHybrid    { get; }
            = Register("dieselBatteryHybrid");

        /// <summary>
        /// Ethanol
        /// </summary>
        public static FuelType  Ethanol                { get; }
            = Register("ethanol");

        /// <summary>
        /// Hydrogen
        /// </summary>
        public static FuelType  Hydrogen               { get; }
            = Register("hydrogen");

        /// <summary>
        /// Liquid Gas
        /// </summary>
        public static FuelType  LiquidGas              { get; }
            = Register("liquidGas");

        /// <summary>
        /// Liquid Petroleum Gas
        /// </summary>
        public static FuelType  LPG                    { get; }
            = Register("lpg");

        /// <summary>
        /// Methane Gas
        /// </summary>
        public static FuelType  Methane                { get; }
            = Register("methane");

        /// <summary>
        /// Petrol
        /// </summary>
        public static FuelType  Petrol                 { get; }
            = Register("petrol");

        /// <summary>
        /// Petrol with 95 Octane
        /// </summary>
        public static FuelType  Petrol95Octane         { get; }
            = Register("petrol95Octane");

        /// <summary>
        /// Petrol with 98 Octane
        /// </summary>
        public static FuelType  Petrol98Octane         { get; }
            = Register("petrol98Octane");

        /// <summary>
        /// Petrol Battery Hybrid
        /// </summary>
        public static FuelType  PetrolBatteryHybrid    { get; }
            = Register("petrolBatteryHybrid");

        /// <summary>
        /// Petrol Leaded
        /// </summary>
        public static FuelType  PetrolLeaded           { get; }
            = Register("petrolLeaded");

        /// <summary>
        /// Petrol Unleaded
        /// </summary>
        public static FuelType  PetrolUnleaded         { get; }
            = Register("petrolUnleaded");

        /// <summary>
        /// Unknown
        /// </summary>
        public static FuelType  Unknown                { get; }
            = Register("unknown");

        /// <summary>
        /// Other
        /// </summary>
        public static FuelType  Other                  { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (FuelType1, FuelType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FuelType1">A FuelType.</param>
        /// <param name="FuelType2">Another FuelType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (FuelType FuelType1,
                                           FuelType FuelType2)

            => FuelType1.Equals(FuelType2);

        #endregion

        #region Operator != (FuelType1, FuelType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FuelType1">A FuelType.</param>
        /// <param name="FuelType2">Another FuelType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (FuelType FuelType1,
                                           FuelType FuelType2)

            => !FuelType1.Equals(FuelType2);

        #endregion

        #region Operator <  (FuelType1, FuelType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FuelType1">A FuelType.</param>
        /// <param name="FuelType2">Another FuelType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (FuelType FuelType1,
                                          FuelType FuelType2)

            => FuelType1.CompareTo(FuelType2) < 0;

        #endregion

        #region Operator <= (FuelType1, FuelType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FuelType1">A FuelType.</param>
        /// <param name="FuelType2">Another FuelType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (FuelType FuelType1,
                                           FuelType FuelType2)

            => FuelType1.CompareTo(FuelType2) <= 0;

        #endregion

        #region Operator >  (FuelType1, FuelType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FuelType1">A FuelType.</param>
        /// <param name="FuelType2">Another FuelType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (FuelType FuelType1,
                                          FuelType FuelType2)

            => FuelType1.CompareTo(FuelType2) > 0;

        #endregion

        #region Operator >= (FuelType1, FuelType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FuelType1">A FuelType.</param>
        /// <param name="FuelType2">Another FuelType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (FuelType FuelType1,
                                           FuelType FuelType2)

            => FuelType1.CompareTo(FuelType2) >= 0;

        #endregion

        #endregion

        #region IComparable<FuelType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two FuelTypes.
        /// </summary>
        /// <param name="Object">FuelType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is FuelType fuelType
                   ? CompareTo(fuelType)
                   : throw new ArgumentException("The given object is not FuelType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(FuelType)

        /// <summary>
        /// Compares two FuelTypes.
        /// </summary>
        /// <param name="FuelType">FuelType to compare with.</param>
        public Int32 CompareTo(FuelType FuelType)

            => String.Compare(InternalId,
                              FuelType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<FuelType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two FuelTypes for equality.
        /// </summary>
        /// <param name="Object">FuelType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is FuelType fuelType &&
                   Equals(fuelType);

        #endregion

        #region Equals(FuelType)

        /// <summary>
        /// Compares two FuelTypes for equality.
        /// </summary>
        /// <param name="FuelType">FuelType to compare with.</param>
        public Boolean Equals(FuelType FuelType)

            => String.Equals(InternalId,
                             FuelType.InternalId,
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
