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
    /// Extension methods for CalendarWeekWithinMonthEnums.
    /// </summary>
    public static class CalendarWeekWithinMonthEnumExtensions
    {

        /// <summary>
        /// Indicates whether this CalendarWeekWithinMonthEnum is null or empty.
        /// </summary>
        /// <param name="CalendarWeekWithinMonthEnum">A CalendarWeekWithinMonthEnum.</param>
        public static Boolean IsNullOrEmpty(this CalendarWeekWithinMonthEnum? CalendarWeekWithinMonthEnum)
            => !CalendarWeekWithinMonthEnum.HasValue || CalendarWeekWithinMonthEnum.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this CalendarWeekWithinMonthEnum is null or empty.
        /// </summary>
        /// <param name="CalendarWeekWithinMonthEnum">A CalendarWeekWithinMonthEnum.</param>
        public static Boolean IsNotNullOrEmpty(this CalendarWeekWithinMonthEnum? CalendarWeekWithinMonthEnum)
            => CalendarWeekWithinMonthEnum.HasValue && CalendarWeekWithinMonthEnum.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A CalendarWeekWithinMonthEnum.
    /// </summary>
    public readonly struct CalendarWeekWithinMonthEnum : IId,
                                                         IEquatable<CalendarWeekWithinMonthEnum>,
                                                         IComparable<CalendarWeekWithinMonthEnum>
    {

        #region Data

        private readonly static Dictionary<String, CalendarWeekWithinMonthEnum>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this CalendarWeekWithinMonthEnum is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this CalendarWeekWithinMonthEnum is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the CalendarWeekWithinMonthEnum.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<CalendarWeekWithinMonthEnum>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new CalendarWeekWithinMonthEnum based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a CalendarWeekWithinMonthEnum.</param>
        private CalendarWeekWithinMonthEnum(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static CalendarWeekWithinMonthEnum Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new CalendarWeekWithinMonthEnum(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a CalendarWeekWithinMonthEnum.
        /// </summary>
        /// <param name="Text">A text representation of a CalendarWeekWithinMonthEnum.</param>
        public static CalendarWeekWithinMonthEnum Parse(String Text)
        {

            if (TryParse(Text, out var day))
                return day;

            throw new ArgumentException($"Invalid text representation of a CalendarWeekWithinMonthEnum: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a CalendarWeekWithinMonthEnum.
        /// </summary>
        /// <param name="Text">A text representation of a CalendarWeekWithinMonthEnum.</param>
        public static CalendarWeekWithinMonthEnum? TryParse(String Text)
        {

            if (TryParse(Text, out var day))
                return day;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out CalendarWeekWithinMonthEnum)

        /// <summary>
        /// Try to parse the given text as CalendarWeekWithinMonthEnum.
        /// </summary>
        /// <param name="Text">A text representation of a CalendarWeekWithinMonthEnum.</param>
        /// <param name="CalendarWeekWithinMonthEnum">The parsed CalendarWeekWithinMonthEnum.</param>
        public static Boolean TryParse(String Text, out CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out CalendarWeekWithinMonthEnum))
                    CalendarWeekWithinMonthEnum = Register(Text);

                return true;

            }

            CalendarWeekWithinMonthEnum = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this CalendarWeekWithinMonthEnum.
        /// </summary>
        public CalendarWeekWithinMonthEnum Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Calendar week containing the first of the month. Several days of the first week may occur in the previous calendar month.
        /// By construction, the last week of a preceding month can also be the first week of a subsequent month.
        /// </summary>
        public static CalendarWeekWithinMonthEnum  FirstWeek       { get; }
            = Register("firstWeek");

        /// <summary>
        /// Second week of the month.
        /// </summary>
        public static CalendarWeekWithinMonthEnum  SecondWeek       { get; }
            = Register("secondWeek");

        /// <summary>
        /// Third week of the month.
        /// </summary>
        public static CalendarWeekWithinMonthEnum  ThirdWeek       { get; }
            = Register("thirdWeek");

        /// <summary>
        /// Fourth week of the month.
        /// </summary>
        public static CalendarWeekWithinMonthEnum  FourthWeek       { get; }
            = Register("fourthWeek");

        /// <summary>
        /// Fifth week of the month.
        /// </summary>
        public static CalendarWeekWithinMonthEnum  FifthWeek       { get; }
            = Register("fifthWeek");

        /// <summary>
        /// Sixth week of the month.
        /// </summary>
        public static CalendarWeekWithinMonthEnum  SixthWeek       { get; }
            = Register("sixthWeek");

        /// <summary>
        /// Last calendar week within the month, regardless of its actual number. The last week is the week beginning with Monday 
        /// and containing the last day of the month.
        /// </summary>
        public static CalendarWeekWithinMonthEnum  LastWeek       { get; }
            = Register("lastWeek");

        #endregion


        #region Operator overloading

        #region Operator == (CalendarWeekWithinMonthEnum1, CalendarWeekWithinMonthEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CalendarWeekWithinMonthEnum1">A CalendarWeekWithinMonthEnum.</param>
        /// <param name="CalendarWeekWithinMonthEnum2">Another CalendarWeekWithinMonthEnum.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum1,
                                           CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum2)

            => CalendarWeekWithinMonthEnum1.Equals(CalendarWeekWithinMonthEnum2);

        #endregion

        #region Operator != (CalendarWeekWithinMonthEnum1, CalendarWeekWithinMonthEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CalendarWeekWithinMonthEnum1">A CalendarWeekWithinMonthEnum.</param>
        /// <param name="CalendarWeekWithinMonthEnum2">Another CalendarWeekWithinMonthEnum.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum1,
                                           CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum2)

            => !CalendarWeekWithinMonthEnum1.Equals(CalendarWeekWithinMonthEnum2);

        #endregion

        #region Operator <  (CalendarWeekWithinMonthEnum1, CalendarWeekWithinMonthEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CalendarWeekWithinMonthEnum1">A CalendarWeekWithinMonthEnum.</param>
        /// <param name="CalendarWeekWithinMonthEnum2">Another CalendarWeekWithinMonthEnum.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum1,
                                          CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum2)

            => CalendarWeekWithinMonthEnum1.CompareTo(CalendarWeekWithinMonthEnum2) < 0;

        #endregion

        #region Operator <= (CalendarWeekWithinMonthEnum1, CalendarWeekWithinMonthEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CalendarWeekWithinMonthEnum1">A CalendarWeekWithinMonthEnum.</param>
        /// <param name="CalendarWeekWithinMonthEnum2">Another CalendarWeekWithinMonthEnum.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum1,
                                           CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum2)

            => CalendarWeekWithinMonthEnum1.CompareTo(CalendarWeekWithinMonthEnum2) <= 0;

        #endregion

        #region Operator >  (CalendarWeekWithinMonthEnum1, CalendarWeekWithinMonthEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CalendarWeekWithinMonthEnum1">A CalendarWeekWithinMonthEnum.</param>
        /// <param name="CalendarWeekWithinMonthEnum2">Another CalendarWeekWithinMonthEnum.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum1,
                                          CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum2)

            => CalendarWeekWithinMonthEnum1.CompareTo(CalendarWeekWithinMonthEnum2) > 0;

        #endregion

        #region Operator >= (CalendarWeekWithinMonthEnum1, CalendarWeekWithinMonthEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="CalendarWeekWithinMonthEnum1">A CalendarWeekWithinMonthEnum.</param>
        /// <param name="CalendarWeekWithinMonthEnum2">Another CalendarWeekWithinMonthEnum.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum1,
                                           CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum2)

            => CalendarWeekWithinMonthEnum1.CompareTo(CalendarWeekWithinMonthEnum2) >= 0;

        #endregion

        #endregion

        #region IComparable<CalendarWeekWithinMonthEnum> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two CalendarWeekWithinMonthEnums.
        /// </summary>
        /// <param name="Object">CalendarWeekWithinMonthEnum to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum
                   ? CompareTo(CalendarWeekWithinMonthEnum)
                   : throw new ArgumentException("The given object is not CalendarWeekWithinMonthEnum!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(CalendarWeekWithinMonthEnum)

        /// <summary>
        /// Compares two CalendarWeekWithinMonthEnums.
        /// </summary>
        /// <param name="CalendarWeekWithinMonthEnum">CalendarWeekWithinMonthEnum to compare with.</param>
        public Int32 CompareTo(CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum)

            => String.Compare(InternalId,
                              CalendarWeekWithinMonthEnum.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<CalendarWeekWithinMonthEnum> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two CalendarWeekWithinMonthEnums for equality.
        /// </summary>
        /// <param name="Object">CalendarWeekWithinMonthEnum to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum &&
                   Equals(CalendarWeekWithinMonthEnum);

        #endregion

        #region Equals(CalendarWeekWithinMonthEnum)

        /// <summary>
        /// Compares two CalendarWeekWithinMonthEnums for equality.
        /// </summary>
        /// <param name="CalendarWeekWithinMonthEnum">CalendarWeekWithinMonthEnum to compare with.</param>
        public Boolean Equals(CalendarWeekWithinMonthEnum CalendarWeekWithinMonthEnum)

            => String.Equals(InternalId,
                             CalendarWeekWithinMonthEnum.InternalId,
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
