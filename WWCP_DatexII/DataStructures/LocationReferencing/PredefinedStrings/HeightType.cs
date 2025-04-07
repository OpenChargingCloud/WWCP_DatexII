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

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// Extension methods for HeightTypes.
    /// </summary>
    public static class HeightTypeExtensions
    {

        /// <summary>
        /// Indicates whether this HeightType is null or empty.
        /// </summary>
        /// <param name="HeightType">A HeightType.</param>
        public static Boolean IsNullOrEmpty(this HeightType? HeightType)
            => !HeightType.HasValue || HeightType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this HeightType is null or empty.
        /// </summary>
        /// <param name="HeightType">A HeightType.</param>
        public static Boolean IsNotNullOrEmpty(this HeightType? HeightType)
            => HeightType.HasValue && HeightType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A HeightType.
    /// </summary>
    public readonly struct HeightType : IId,
                                        IEquatable<HeightType>,
                                        IComparable<HeightType>
    {

        #region Data

        private readonly static Dictionary<String, HeightType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this HeightType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this HeightType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the HeightType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<HeightType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new HeightType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a HeightType.</param>
        private HeightType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static HeightType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new HeightType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a HeightType.
        /// </summary>
        /// <param name="Text">A text representation of a HeightType.</param>
        public static HeightType Parse(String Text)
        {

            if (TryParse(Text, out var heightType))
                return heightType;

            throw new ArgumentException($"Invalid text representation of a HeightType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a HeightType.
        /// </summary>
        /// <param name="Text">A text representation of a HeightType.</param>
        public static HeightType? TryParse(String Text)
        {

            if (TryParse(Text, out var heightType))
                return heightType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out HeightType)

        /// <summary>
        /// Try to parse the given text as HeightType.
        /// </summary>
        /// <param name="Text">A text representation of a HeightType.</param>
        /// <param name="HeightType">The parsed HeightType.</param>
        public static Boolean TryParse(String Text, out HeightType HeightType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out HeightType))
                    HeightType = Register(Text);

                return true;

            }

            HeightType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this HeightType.
        /// </summary>
        public HeightType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Value measured vertically above the reference ellipsoid.
        /// </summary>
        public static HeightType  EllipsoidalHeight       { get; }
            = Register("ellipsoidalHeight");

        /// <summary>
        /// Height type corresponding to a value measured along the direction of gravity above the reference geoid.
        /// </summary>
        public static HeightType  GravityRelatedHeight    { get; }
            = Register("gravityRelatedHeight");

        /// <summary>
        /// Height type corresponding to a value measured vertically above the ground level at this point.
        /// </summary>
        public static HeightType  RelativeHeight          { get; }
            = Register("relativeHeight");

        #endregion


        #region Operator overloading

        #region Operator == (HeightType1, HeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HeightType1">A HeightType.</param>
        /// <param name="HeightType2">Another HeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (HeightType HeightType1,
                                           HeightType HeightType2)

            => HeightType1.Equals(HeightType2);

        #endregion

        #region Operator != (HeightType1, HeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HeightType1">A HeightType.</param>
        /// <param name="HeightType2">Another HeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (HeightType HeightType1,
                                           HeightType HeightType2)

            => !HeightType1.Equals(HeightType2);

        #endregion

        #region Operator <  (HeightType1, HeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HeightType1">A HeightType.</param>
        /// <param name="HeightType2">Another HeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (HeightType HeightType1,
                                          HeightType HeightType2)

            => HeightType1.CompareTo(HeightType2) < 0;

        #endregion

        #region Operator <= (HeightType1, HeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HeightType1">A HeightType.</param>
        /// <param name="HeightType2">Another HeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (HeightType HeightType1,
                                           HeightType HeightType2)

            => HeightType1.CompareTo(HeightType2) <= 0;

        #endregion

        #region Operator >  (HeightType1, HeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HeightType1">A HeightType.</param>
        /// <param name="HeightType2">Another HeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (HeightType HeightType1,
                                          HeightType HeightType2)

            => HeightType1.CompareTo(HeightType2) > 0;

        #endregion

        #region Operator >= (HeightType1, HeightType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HeightType1">A HeightType.</param>
        /// <param name="HeightType2">Another HeightType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (HeightType HeightType1,
                                           HeightType HeightType2)

            => HeightType1.CompareTo(HeightType2) >= 0;

        #endregion

        #endregion

        #region IComparable<HeightType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two HeightTypes.
        /// </summary>
        /// <param name="Object">HeightType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is HeightType HeightType
                   ? CompareTo(HeightType)
                   : throw new ArgumentException("The given object is not HeightType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(HeightType)

        /// <summary>
        /// Compares two HeightTypes.
        /// </summary>
        /// <param name="HeightType">HeightType to compare with.</param>
        public Int32 CompareTo(HeightType HeightType)

            => String.Compare(InternalId,
                              HeightType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<HeightType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two HeightTypes for equality.
        /// </summary>
        /// <param name="Object">HeightType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is HeightType HeightType &&
                   Equals(HeightType);

        #endregion

        #region Equals(HeightType)

        /// <summary>
        /// Compares two HeightTypes for equality.
        /// </summary>
        /// <param name="HeightType">HeightType to compare with.</param>
        public Boolean Equals(HeightType HeightType)

            => String.Equals(InternalId,
                             HeightType.InternalId,
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
