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
    /// Extension methods for SpecialDayTypes.
    /// </summary>
    public static class SpecialDayTypeExtensions
    {

        /// <summary>
        /// Indicates whether this SpecialDayType is null or empty.
        /// </summary>
        /// <param name="SpecialDayType">A SpecialDayType.</param>
        public static Boolean IsNullOrEmpty(this SpecialDayType? SpecialDayType)
            => !SpecialDayType.HasValue || SpecialDayType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this SpecialDayType is null or empty.
        /// </summary>
        /// <param name="SpecialDayType">A SpecialDayType.</param>
        public static Boolean IsNotNullOrEmpty(this SpecialDayType? SpecialDayType)
            => SpecialDayType.HasValue && SpecialDayType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A SpecialDayType.
    /// </summary>
    public readonly struct SpecialDayType : IId,
                                            IEquatable<SpecialDayType>,
                                            IComparable<SpecialDayType>
    {

        #region Data

        private readonly static Dictionary<String, SpecialDayType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this SpecialDayType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this SpecialDayType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the SpecialDayType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<SpecialDayType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new SpecialDayType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a SpecialDayType.</param>
        private SpecialDayType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static SpecialDayType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new SpecialDayType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a SpecialDayType.
        /// </summary>
        /// <param name="Text">A text representation of a SpecialDayType.</param>
        public static SpecialDayType Parse(String Text)
        {

            if (TryParse(Text, out var specialDayType))
                return specialDayType;

            throw new ArgumentException($"Invalid text representation of a SpecialDayType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a SpecialDayType.
        /// </summary>
        /// <param name="Text">A text representation of a SpecialDayType.</param>
        public static SpecialDayType? TryParse(String Text)
        {

            if (TryParse(Text, out var specialDayType))
                return specialDayType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out SpecialDayType)

        /// <summary>
        /// Try to parse the given text as SpecialDayType.
        /// </summary>
        /// <param name="Text">A text representation of a SpecialDayType.</param>
        /// <param name="SpecialDayType">The parsed SpecialDayType.</param>
        public static Boolean TryParse(String Text, out SpecialDayType SpecialDayType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out SpecialDayType))
                    SpecialDayType = Register(Text);

                return true;

            }

            SpecialDayType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this SpecialDayType.
        /// </summary>
        public SpecialDayType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// The day preceding a public holiday.
        /// </summary>
        public static SpecialDayType  DayBeforePublicHoliday       { get; }
            = Register("dayBeforePublicHoliday");

        /// <summary>
        /// A public holiday in general.
        /// </summary>
        public static SpecialDayType  PublicHoliday                { get; }
            = Register("publicHoliday");

        /// <summary>
        /// A day following a public holiday.
        /// </summary>
        public static SpecialDayType  DayFollowingPublicHoliday    { get; }
            = Register("dayFollowingPublicHoliday");

        /// <summary>
        /// A day between a public holiday and the weekend.
        /// </summary>
        public static SpecialDayType  LongWeekendDay               { get; }
            = Register("longWeekendDay");

        /// <summary>
        /// A holiday in lieu of a public holiday that falls on a weekend.
        /// </summary>
        public static SpecialDayType  InLieuOfPublicHoliday        { get; }
            = Register("inLieuOfPublicHoliday");

        /// <summary>
        /// A school day.
        /// </summary>
        public static SpecialDayType  SchoolDay                    { get; }
            = Register("schoolDay");

        /// <summary>
        /// A day within the school holidays.
        /// </summary>
        public static SpecialDayType  SchoolHolidays               { get; }
            = Register("schoolHolidays");

        /// <summary>
        /// A day of a public event.
        /// </summary>
        public static SpecialDayType  PublicEventDay               { get; }
            = Register("publicEventDay");

        /// <summary>
        /// Some other special day.
        /// </summary>
        public static SpecialDayType  Other                        { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (SpecialDayType1, SpecialDayType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SpecialDayType1">A SpecialDayType.</param>
        /// <param name="SpecialDayType2">Another SpecialDayType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (SpecialDayType SpecialDayType1,
                                           SpecialDayType SpecialDayType2)

            => SpecialDayType1.Equals(SpecialDayType2);

        #endregion

        #region Operator != (SpecialDayType1, SpecialDayType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SpecialDayType1">A SpecialDayType.</param>
        /// <param name="SpecialDayType2">Another SpecialDayType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (SpecialDayType SpecialDayType1,
                                           SpecialDayType SpecialDayType2)

            => !SpecialDayType1.Equals(SpecialDayType2);

        #endregion

        #region Operator <  (SpecialDayType1, SpecialDayType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SpecialDayType1">A SpecialDayType.</param>
        /// <param name="SpecialDayType2">Another SpecialDayType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (SpecialDayType SpecialDayType1,
                                          SpecialDayType SpecialDayType2)

            => SpecialDayType1.CompareTo(SpecialDayType2) < 0;

        #endregion

        #region Operator <= (SpecialDayType1, SpecialDayType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SpecialDayType1">A SpecialDayType.</param>
        /// <param name="SpecialDayType2">Another SpecialDayType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (SpecialDayType SpecialDayType1,
                                           SpecialDayType SpecialDayType2)

            => SpecialDayType1.CompareTo(SpecialDayType2) <= 0;

        #endregion

        #region Operator >  (SpecialDayType1, SpecialDayType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SpecialDayType1">A SpecialDayType.</param>
        /// <param name="SpecialDayType2">Another SpecialDayType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (SpecialDayType SpecialDayType1,
                                          SpecialDayType SpecialDayType2)

            => SpecialDayType1.CompareTo(SpecialDayType2) > 0;

        #endregion

        #region Operator >= (SpecialDayType1, SpecialDayType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SpecialDayType1">A SpecialDayType.</param>
        /// <param name="SpecialDayType2">Another SpecialDayType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (SpecialDayType SpecialDayType1,
                                           SpecialDayType SpecialDayType2)

            => SpecialDayType1.CompareTo(SpecialDayType2) >= 0;

        #endregion

        #endregion

        #region IComparable<SpecialDayType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two SpecialDayTypes.
        /// </summary>
        /// <param name="Object">SpecialDayType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is SpecialDayType SpecialDayType
                   ? CompareTo(SpecialDayType)
                   : throw new ArgumentException("The given object is not SpecialDayType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(SpecialDayType)

        /// <summary>
        /// Compares two SpecialDayTypes.
        /// </summary>
        /// <param name="SpecialDayType">SpecialDayType to compare with.</param>
        public Int32 CompareTo(SpecialDayType SpecialDayType)

            => String.Compare(InternalId,
                              SpecialDayType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<SpecialDayType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two SpecialDayTypes for equality.
        /// </summary>
        /// <param name="Object">SpecialDayType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is SpecialDayType SpecialDayType &&
                   Equals(SpecialDayType);

        #endregion

        #region Equals(SpecialDayType)

        /// <summary>
        /// Compares two SpecialDayTypes for equality.
        /// </summary>
        /// <param name="SpecialDayType">SpecialDayType to compare with.</param>
        public Boolean Equals(SpecialDayType SpecialDayType)

            => String.Equals(InternalId,
                             SpecialDayType.InternalId,
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
