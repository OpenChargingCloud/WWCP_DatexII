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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Extension methods for FacilityTypes.
    /// </summary>
    public static class FacilityTypeExtensions
    {

        /// <summary>
        /// Indicates whether this FacilityType is null or empty.
        /// </summary>
        /// <param name="FacilityType">A FacilityType.</param>
        public static Boolean IsNullOrEmpty(this FacilityType? FacilityType)
            => !FacilityType.HasValue || FacilityType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this FacilityType is null or empty.
        /// </summary>
        /// <param name="FacilityType">A FacilityType.</param>
        public static Boolean IsNotNullOrEmpty(this FacilityType? FacilityType)
            => FacilityType.HasValue && FacilityType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// The type of an associated facility (DATEX II 3.7).
    /// </summary>
    public readonly struct FacilityType : IId,
                                        IEquatable<FacilityType>,
                                        IComparable<FacilityType>
    {

        #region Data

        private readonly static Dictionary<String, FacilityType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this FacilityType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this FacilityType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the FacilityType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<FacilityType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new FacilityType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a FacilityType.</param>
        private FacilityType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static FacilityType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new FacilityType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a FacilityType.
        /// </summary>
        /// <param name="Text">A text representation of a FacilityType.</param>
        public static FacilityType Parse(String Text)
        {

            if (TryParse(Text, out var facilityType))
                return facilityType;

            throw new ArgumentException($"Invalid text representation of a FacilityType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a FacilityType.
        /// </summary>
        /// <param name="Text">A text representation of a FacilityType.</param>
        public static FacilityType? TryParse(String Text)
        {

            if (TryParse(Text, out var facilityType))
                return facilityType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out FacilityType)

        /// <summary>
        /// Try to parse the given text as FacilityType.
        /// </summary>
        /// <param name="Text">A text representation of a FacilityType.</param>
        /// <param name="FacilityType">The parsed FacilityType.</param>
        public static Boolean TryParse(String Text, out FacilityType FacilityType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out FacilityType))
                    FacilityType = Register(Text);

                return true;

            }

            FacilityType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this FacilityType.
        /// </summary>
        public FacilityType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// An airport.
        /// </summary>
        public static FacilityType  Airport    { get; }
            = Register("airport");

        /// <summary>
        /// A car park.
        /// </summary>
        public static FacilityType  CarPark    { get; }
            = Register("carPark");

        /// <summary>
        /// A car rental station.
        /// </summary>
        public static FacilityType  CarRentalStation    { get; }
            = Register("carRentalStation");

        /// <summary>
        /// An electric charging station.
        /// </summary>
        public static FacilityType  ElectricChargingStation    { get; }
            = Register("electricChargingStation");

        /// <summary>
        /// An energy infrastructure site.
        /// </summary>
        public static FacilityType  EnergyInfrastructureSite    { get; }
            = Register("energyInfrastructureSite");

        /// <summary>
        /// A lorry parking site.
        /// </summary>
        public static FacilityType  LorryParkingSite    { get; }
            = Register("lorryParkingSite");

        /// <summary>
        /// A parking site.
        /// </summary>
        public static FacilityType  ParkingSite    { get; }
            = Register("parkingSite");

        /// <summary>
        /// A petrol station.
        /// </summary>
        public static FacilityType  PetrolStation    { get; }
            = Register("petrolStation");

        /// <summary>
        /// A public transport depot.
        /// </summary>
        public static FacilityType  PublicTransportDepot    { get; }
            = Register("publicTransportDepot");

        /// <summary>
        /// A public transport hub.
        /// </summary>
        public static FacilityType  PublicTransportHub    { get; }
            = Register("publicTransportHub");

        /// <summary>
        /// A shopping centre.
        /// </summary>
        public static FacilityType  ShoppingCentre    { get; }
            = Register("shoppingCentre");

        /// <summary>
        /// A train station.
        /// </summary>
        public static FacilityType  TrainStation    { get; }
            = Register("trainStation");

        /// <summary>
        /// Another type of facility.
        /// </summary>
        public static FacilityType  Other    { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (FacilityType1, FacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FacilityType1">A FacilityType.</param>
        /// <param name="FacilityType2">Another FacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (FacilityType FacilityType1,
                                           FacilityType FacilityType2)

            => FacilityType1.Equals(FacilityType2);

        #endregion

        #region Operator != (FacilityType1, FacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FacilityType1">A FacilityType.</param>
        /// <param name="FacilityType2">Another FacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (FacilityType FacilityType1,
                                           FacilityType FacilityType2)

            => !FacilityType1.Equals(FacilityType2);

        #endregion

        #region Operator <  (FacilityType1, FacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FacilityType1">A FacilityType.</param>
        /// <param name="FacilityType2">Another FacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (FacilityType FacilityType1,
                                          FacilityType FacilityType2)

            => FacilityType1.CompareTo(FacilityType2) < 0;

        #endregion

        #region Operator <= (FacilityType1, FacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FacilityType1">A FacilityType.</param>
        /// <param name="FacilityType2">Another FacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (FacilityType FacilityType1,
                                           FacilityType FacilityType2)

            => FacilityType1.CompareTo(FacilityType2) <= 0;

        #endregion

        #region Operator >  (FacilityType1, FacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FacilityType1">A FacilityType.</param>
        /// <param name="FacilityType2">Another FacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (FacilityType FacilityType1,
                                          FacilityType FacilityType2)

            => FacilityType1.CompareTo(FacilityType2) > 0;

        #endregion

        #region Operator >= (FacilityType1, FacilityType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FacilityType1">A FacilityType.</param>
        /// <param name="FacilityType2">Another FacilityType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (FacilityType FacilityType1,
                                           FacilityType FacilityType2)

            => FacilityType1.CompareTo(FacilityType2) >= 0;

        #endregion

        #endregion

        #region IComparable<FacilityType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two FacilityTypes.
        /// </summary>
        /// <param name="Object">FacilityType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is FacilityType FacilityType
                   ? CompareTo(FacilityType)
                   : throw new ArgumentException("The given object is not FacilityType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(FacilityType)

        /// <summary>
        /// Compares two FacilityTypes.
        /// </summary>
        /// <param name="FacilityType">FacilityType to compare with.</param>
        public Int32 CompareTo(FacilityType FacilityType)

            => String.Compare(InternalId,
                              FacilityType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<FacilityType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two FacilityTypes for equality.
        /// </summary>
        /// <param name="Object">FacilityType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is FacilityType FacilityType &&
                   Equals(FacilityType);

        #endregion

        #region Equals(FacilityType)

        /// <summary>
        /// Compares two FacilityTypes for equality.
        /// </summary>
        /// <param name="FacilityType">FacilityType to compare with.</param>
        public Boolean Equals(FacilityType FacilityType)

            => String.Equals(InternalId,
                             FacilityType.InternalId,
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
