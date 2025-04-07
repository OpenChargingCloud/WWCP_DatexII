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

using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;
using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Extension methods for WeightTypes.
    /// </summary>
    public static class WeightTypeExtensions
    {

        /// <summary>
        /// Indicates whether this WeightType is null or empty.
        /// </summary>
        /// <param name="WeightType">A WeightType.</param>
        public static Boolean IsNullOrEmpty(this WeightType? WeightType)
            => !WeightType.HasValue || WeightType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this WeightType is null or empty.
        /// </summary>
        /// <param name="WeightType">A WeightType.</param>
        public static Boolean IsNotNullOrEmpty(this WeightType? WeightType)
            => WeightType.HasValue && WeightType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A WeightType.
    /// </summary>
    public readonly struct WeightType : IId,
                                        IEquatable<WeightType>,
                                        IComparable<WeightType>
    {

        #region Data

        private readonly static Dictionary<String, WeightType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this WeightType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this WeightType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the WeightType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<WeightType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new WeightType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a WeightType.</param>
        private WeightType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static WeightType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new WeightType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a WeightType.
        /// </summary>
        /// <param name="Text">A text representation of a WeightType.</param>
        public static WeightType Parse(String Text)
        {

            if (TryParse(Text, out var weightType))
                return weightType;

            throw new ArgumentException($"Invalid text representation of a WeightType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a WeightType.
        /// </summary>
        /// <param name="Text">A text representation of a WeightType.</param>
        public static WeightType? TryParse(String Text)
        {

            if (TryParse(Text, out var weightType))
                return weightType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out WeightType)

        /// <summary>
        /// Try to parse the given text as WeightType.
        /// </summary>
        /// <param name="Text">A text representation of a WeightType.</param>
        /// <param name="WeightType">The parsed WeightType.</param>
        public static Boolean TryParse(String Text, out WeightType WeightType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out WeightType))
                    WeightType = Register(Text);

                return true;

            }

            WeightType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this WeightType.
        /// </summary>
        public WeightType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// The weight is the actual weight of a specific vehicle.
        /// </summary>
        public static WeightType  Actual              { get; }
            = Register("actual");

        /// <summary>
        /// The weight is the maximum permitted weight for a vehicle.
        /// </summary>
        public static WeightType  MaximumPermitted    { get; }
            = Register("maximumPermitted");

        #endregion


        #region Operator overloading

        #region Operator == (WeightType1, WeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="WeightType1">A WeightType.</param>
        /// <param name="WeightType2">Another WeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (WeightType WeightType1,
                                           WeightType WeightType2)

            => WeightType1.Equals(WeightType2);

        #endregion

        #region Operator != (WeightType1, WeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="WeightType1">A WeightType.</param>
        /// <param name="WeightType2">Another WeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (WeightType WeightType1,
                                           WeightType WeightType2)

            => !WeightType1.Equals(WeightType2);

        #endregion

        #region Operator <  (WeightType1, WeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="WeightType1">A WeightType.</param>
        /// <param name="WeightType2">Another WeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (WeightType WeightType1,
                                          WeightType WeightType2)

            => WeightType1.CompareTo(WeightType2) < 0;

        #endregion

        #region Operator <= (WeightType1, WeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="WeightType1">A WeightType.</param>
        /// <param name="WeightType2">Another WeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (WeightType WeightType1,
                                           WeightType WeightType2)

            => WeightType1.CompareTo(WeightType2) <= 0;

        #endregion

        #region Operator >  (WeightType1, WeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="WeightType1">A WeightType.</param>
        /// <param name="WeightType2">Another WeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (WeightType WeightType1,
                                          WeightType WeightType2)

            => WeightType1.CompareTo(WeightType2) > 0;

        #endregion

        #region Operator >= (WeightType1, WeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="WeightType1">A WeightType.</param>
        /// <param name="WeightType2">Another WeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (WeightType WeightType1,
                                           WeightType WeightType2)

            => WeightType1.CompareTo(WeightType2) >= 0;

        #endregion

        #endregion

        #region IComparable<WeightType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two WeightTypes.
        /// </summary>
        /// <param name="Object">WeightType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is WeightType weightType
                   ? CompareTo(weightType)
                   : throw new ArgumentException("The given object is not WeightType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(WeightType)

        /// <summary>
        /// Compares two WeightTypes.
        /// </summary>
        /// <param name="WeightType">WeightType to compare with.</param>
        public Int32 CompareTo(WeightType WeightType)

            => String.Compare(InternalId,
                              WeightType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<WeightType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two WeightTypes for equality.
        /// </summary>
        /// <param name="Object">WeightType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is WeightType weightType &&
                   Equals(weightType);

        #endregion

        #region Equals(WeightType)

        /// <summary>
        /// Compares two WeightTypes for equality.
        /// </summary>
        /// <param name="WeightType">WeightType to compare with.</param>
        public Boolean Equals(WeightType WeightType)

            => String.Equals(InternalId,
                             WeightType.InternalId,
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
