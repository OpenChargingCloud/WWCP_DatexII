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

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Extension methods for CurrentTypes.
    /// </summary>
    public static class CurrentTypeExtensions
    {

        /// <summary>
        /// Indicates whether this CurrentType is null or empty.
        /// </summary>
        /// <param name="CurrentType">A CurrentType.</param>
        public static Boolean IsNullOrEmpty(this CurrentType? CurrentType)
            => !CurrentType.HasValue || CurrentType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this CurrentType is null or empty.
        /// </summary>
        /// <param name="CurrentType">A CurrentType.</param>
        public static Boolean IsNotNullOrEmpty(this CurrentType? CurrentType)
            => CurrentType.HasValue && CurrentType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// The type of current of an electric charging point (DATEX II 3.7).
    /// </summary>
    public readonly struct CurrentType : IId,
                                        IEquatable<CurrentType>,
                                        IComparable<CurrentType>
    {

        #region Data

        private readonly static Dictionary<String, CurrentType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this CurrentType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this CurrentType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the CurrentType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<CurrentType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new CurrentType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a CurrentType.</param>
        private CurrentType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static CurrentType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new CurrentType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a CurrentType.
        /// </summary>
        /// <param name="Text">A text representation of a CurrentType.</param>
        public static CurrentType Parse(String Text)
        {

            if (TryParse(Text, out var currentType))
                return currentType;

            throw new ArgumentException($"Invalid text representation of a CurrentType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a CurrentType.
        /// </summary>
        /// <param name="Text">A text representation of a CurrentType.</param>
        public static CurrentType? TryParse(String Text)
        {

            if (TryParse(Text, out var currentType))
                return currentType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out CurrentType)

        /// <summary>
        /// Try to parse the given text as CurrentType.
        /// </summary>
        /// <param name="Text">A text representation of a CurrentType.</param>
        /// <param name="CurrentType">The parsed CurrentType.</param>
        public static Boolean TryParse(String Text, out CurrentType CurrentType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out CurrentType))
                    CurrentType = Register(Text);

                return true;

            }

            CurrentType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this CurrentType.
        /// </summary>
        public CurrentType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Alternating current.
        /// </summary>
        public static CurrentType  AC    { get; }
            = Register("ac");

        /// <summary>
        /// Direct current.
        /// </summary>
        public static CurrentType  DC    { get; }
            = Register("dc");

        #endregion


        #region Operator overloading

        #region Operator == (CurrentType1, CurrentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CurrentType1">A CurrentType.</param>
        /// <param name="CurrentType2">Another CurrentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (CurrentType CurrentType1,
                                           CurrentType CurrentType2)

            => CurrentType1.Equals(CurrentType2);

        #endregion

        #region Operator != (CurrentType1, CurrentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CurrentType1">A CurrentType.</param>
        /// <param name="CurrentType2">Another CurrentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (CurrentType CurrentType1,
                                           CurrentType CurrentType2)

            => !CurrentType1.Equals(CurrentType2);

        #endregion

        #region Operator <  (CurrentType1, CurrentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CurrentType1">A CurrentType.</param>
        /// <param name="CurrentType2">Another CurrentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (CurrentType CurrentType1,
                                          CurrentType CurrentType2)

            => CurrentType1.CompareTo(CurrentType2) < 0;

        #endregion

        #region Operator <= (CurrentType1, CurrentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CurrentType1">A CurrentType.</param>
        /// <param name="CurrentType2">Another CurrentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (CurrentType CurrentType1,
                                           CurrentType CurrentType2)

            => CurrentType1.CompareTo(CurrentType2) <= 0;

        #endregion

        #region Operator >  (CurrentType1, CurrentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CurrentType1">A CurrentType.</param>
        /// <param name="CurrentType2">Another CurrentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (CurrentType CurrentType1,
                                          CurrentType CurrentType2)

            => CurrentType1.CompareTo(CurrentType2) > 0;

        #endregion

        #region Operator >= (CurrentType1, CurrentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CurrentType1">A CurrentType.</param>
        /// <param name="CurrentType2">Another CurrentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (CurrentType CurrentType1,
                                           CurrentType CurrentType2)

            => CurrentType1.CompareTo(CurrentType2) >= 0;

        #endregion

        #endregion

        #region IComparable<CurrentType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two CurrentTypes.
        /// </summary>
        /// <param name="Object">CurrentType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is CurrentType CurrentType
                   ? CompareTo(CurrentType)
                   : throw new ArgumentException("The given object is not CurrentType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(CurrentType)

        /// <summary>
        /// Compares two CurrentTypes.
        /// </summary>
        /// <param name="CurrentType">CurrentType to compare with.</param>
        public Int32 CompareTo(CurrentType CurrentType)

            => String.Compare(InternalId,
                              CurrentType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<CurrentType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two CurrentTypes for equality.
        /// </summary>
        /// <param name="Object">CurrentType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is CurrentType CurrentType &&
                   Equals(CurrentType);

        #endregion

        #region Equals(CurrentType)

        /// <summary>
        /// Compares two CurrentTypes for equality.
        /// </summary>
        /// <param name="CurrentType">CurrentType to compare with.</param>
        public Boolean Equals(CurrentType CurrentType)

            => String.Equals(InternalId,
                             CurrentType.InternalId,
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
