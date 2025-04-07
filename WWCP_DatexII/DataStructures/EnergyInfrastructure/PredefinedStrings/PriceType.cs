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

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Extension methods for PriceType.
    /// </summary>
    public static class PriceTypeExtensions
    {

        /// <summary>
        /// Indicates whether this PriceType is null or empty.
        /// </summary>
        /// <param name="PriceType">A PriceType.</param>
        public static Boolean IsNullOrEmpty(this PriceType? PriceType)
            => !PriceType.HasValue || PriceType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this PriceType is null or empty.
        /// </summary>
        /// <param name="PriceType">A PriceType.</param>
        public static Boolean IsNotNullOrEmpty(this PriceType? PriceType)
            => PriceType.HasValue && PriceType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A PriceType.
    /// </summary>
    public readonly struct PriceType : IId,
                                       IEquatable<PriceType>,
                                       IComparable<PriceType>
    {

        #region Data

        private readonly static Dictionary<String, PriceType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this PriceType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this PriceType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the PriceType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered PriceTypes.
        /// </summary>
        public static    IEnumerable<PriceType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new PriceType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a PriceType.</param>
        private PriceType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static PriceType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new PriceType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a PriceType.
        /// </summary>
        /// <param name="Text">A text representation of a PriceType.</param>
        public static PriceType Parse(String Text)
        {

            if (TryParse(Text, out var priceType))
                return priceType;

            throw new ArgumentException($"Invalid text representation of a PriceType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a PriceType.
        /// </summary>
        /// <param name="Text">A text representation of a PriceType.</param>
        public static PriceType? TryParse(String Text)
        {

            if (TryParse(Text, out var priceType))
                return priceType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out PriceType)

        /// <summary>
        /// Try to parse the given text as a PriceType.
        /// </summary>
        /// <param name="Text">A text representation of a PriceType.</param>
        /// <param name="PriceType">The parsed PriceType.</param>
        public static Boolean TryParse(String Text, out PriceType PriceType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out PriceType))
                    PriceType = Register(Text);

                return true;

            }

            PriceType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this PriceType.
        /// </summary>
        public PriceType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// For internal use only of the recipient organisation.
        /// </summary>
        public static PriceType  InternalUse                                   { get; }
            = Register("internalUse");

        /// <summary>
        /// The given price is per minute of charging.
        /// </summary>
        public static PriceType  PricePerMinute                                   { get; }
            = Register("pricePerMinute");

        /// <summary>
        /// The given price is per kWh of electric energy.
        /// </summary>
        public static PriceType  PricePerKWh                                   { get; }
            = Register("pricePerKWh");

        /// <summary>
        /// The given price is a base price, which has to be added.
        /// </summary>
        public static PriceType  BasePrice                                   { get; }
            = Register("basePrice");

        /// <summary>
        /// The given price is a flatrate price, independent of the fueled amount.
        /// </summary>
        public static PriceType  FlatRate                                   { get; }
            = Register("flatRate");

        /// <summary>
        /// Charging or refueling is free.
        /// </summary>
        public static PriceType  Free                                   { get; }
            = Register("free");

        /// <summary>
        /// Other method of pricing.
        /// </summary>
        public static PriceType  Other                                   { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (PriceType1, PriceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PriceType1">A PriceType.</param>
        /// <param name="PriceType2">Another PriceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (PriceType PriceType1,
                                           PriceType PriceType2)

            => PriceType1.Equals(PriceType2);

        #endregion

        #region Operator != (PriceType1, PriceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PriceType1">A PriceType.</param>
        /// <param name="PriceType2">Another PriceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (PriceType PriceType1,
                                           PriceType PriceType2)

            => !PriceType1.Equals(PriceType2);

        #endregion

        #region Operator <  (PriceType1, PriceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PriceType1">A PriceType.</param>
        /// <param name="PriceType2">Another PriceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (PriceType PriceType1,
                                          PriceType PriceType2)

            => PriceType1.CompareTo(PriceType2) < 0;

        #endregion

        #region Operator <= (PriceType1, PriceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PriceType1">A PriceType.</param>
        /// <param name="PriceType2">Another PriceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (PriceType PriceType1,
                                           PriceType PriceType2)

            => PriceType1.CompareTo(PriceType2) <= 0;

        #endregion

        #region Operator >  (PriceType1, PriceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PriceType1">A PriceType.</param>
        /// <param name="PriceType2">Another PriceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (PriceType PriceType1,
                                          PriceType PriceType2)

            => PriceType1.CompareTo(PriceType2) > 0;

        #endregion

        #region Operator >= (PriceType1, PriceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PriceType1">A PriceType.</param>
        /// <param name="PriceType2">Another PriceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (PriceType PriceType1,
                                           PriceType PriceType2)

            => PriceType1.CompareTo(PriceType2) >= 0;

        #endregion

        #endregion

        #region IComparable<PriceType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two PriceTypes.
        /// </summary>
        /// <param name="Object">A PriceType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is PriceType priceType
                   ? CompareTo(priceType)
                   : throw new ArgumentException("The given object is not a PriceType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(PriceType)

        /// <summary>
        /// Compares two PriceTypes.
        /// </summary>
        /// <param name="PriceType">A PriceType to compare with.</param>
        public Int32 CompareTo(PriceType PriceType)

            => String.Compare(InternalId,
                              PriceType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<PriceType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two PriceTypes for equality.
        /// </summary>
        /// <param name="Object">A PriceType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is PriceType priceType &&
                   Equals(priceType);

        #endregion

        #region Equals(PriceType)

        /// <summary>
        /// Compares two PriceTypes for equality.
        /// </summary>
        /// <param name="PriceType">A PriceType to compare with.</param>
        public Boolean Equals(PriceType PriceType)

            => String.Equals(InternalId,
                             PriceType.InternalId,
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
