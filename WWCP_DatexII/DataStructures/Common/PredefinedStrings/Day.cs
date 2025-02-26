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
    /// Extension methods for Days.
    /// </summary>
    public static class DayExtensions
    {

        /// <summary>
        /// Indicates whether this Day is null or empty.
        /// </summary>
        /// <param name="Day">A Day.</param>
        public static Boolean IsNullOrEmpty(this Day? Day)
            => !Day.HasValue || Day.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this Day is null or empty.
        /// </summary>
        /// <param name="Day">A Day.</param>
        public static Boolean IsNotNullOrEmpty(this Day? Day)
            => Day.HasValue && Day.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A Day.
    /// </summary>
    public readonly struct Day : IId,
                                 IEquatable<Day>,
                                 IComparable<Day>
    {

        #region Data

        private readonly static Dictionary<String, Day>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this Day is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this Day is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the Day.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<Day>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new Day based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a Day.</param>
        private Day(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static Day Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new Day(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a Day.
        /// </summary>
        /// <param name="Text">A text representation of a Day.</param>
        public static Day Parse(String Text)
        {

            if (TryParse(Text, out var day))
                return day;

            throw new ArgumentException($"Invalid text representation of a Day: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a Day.
        /// </summary>
        /// <param name="Text">A text representation of a Day.</param>
        public static Day? TryParse(String Text)
        {

            if (TryParse(Text, out var day))
                return day;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out Day)

        /// <summary>
        /// Try to parse the given text as Day.
        /// </summary>
        /// <param name="Text">A text representation of a Day.</param>
        /// <param name="Day">The parsed Day.</param>
        public static Boolean TryParse(String Text, out Day Day)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out Day))
                    Day = Register(Text);

                return true;

            }

            Day = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this Day.
        /// </summary>
        public Day Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Monday
        /// </summary>
        public static Day  Monday       { get; }
            = Register("monday");

        /// <summary>
        /// Tuesday
        /// </summary>
        public static Day  Tuesday      { get; }
            = Register("tuesday");

        /// <summary>
        /// Wednesday
        /// </summary>
        public static Day  Wednesday    { get; }
            = Register("wednesday");

        /// <summary>
        /// Thursday
        /// </summary>
        public static Day  Thursday     { get; }
            = Register("thursday");

        /// <summary>
        /// Friday
        /// </summary>
        public static Day  Friday       { get; }
            = Register("friday");

        /// <summary>
        /// Satuday
        /// </summary>
        public static Day  Saturday     { get; }
            = Register("saturday");

        /// <summary>
        /// Sunday
        /// </summary>
        public static Day  Sunday       { get; }
            = Register("sunday");

        #endregion


        #region Operator overloading

        #region Operator == (Day1, Day2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Day1">A Day.</param>
        /// <param name="Day2">Another Day.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Day Day1,
                                           Day Day2)

            => Day1.Equals(Day2);

        #endregion

        #region Operator != (Day1, Day2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Day1">A Day.</param>
        /// <param name="Day2">Another Day.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Day Day1,
                                           Day Day2)

            => !Day1.Equals(Day2);

        #endregion

        #region Operator <  (Day1, Day2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Day1">A Day.</param>
        /// <param name="Day2">Another Day.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Day Day1,
                                          Day Day2)

            => Day1.CompareTo(Day2) < 0;

        #endregion

        #region Operator <= (Day1, Day2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Day1">A Day.</param>
        /// <param name="Day2">Another Day.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Day Day1,
                                           Day Day2)

            => Day1.CompareTo(Day2) <= 0;

        #endregion

        #region Operator >  (Day1, Day2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Day1">A Day.</param>
        /// <param name="Day2">Another Day.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Day Day1,
                                          Day Day2)

            => Day1.CompareTo(Day2) > 0;

        #endregion

        #region Operator >= (Day1, Day2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Day1">A Day.</param>
        /// <param name="Day2">Another Day.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Day Day1,
                                           Day Day2)

            => Day1.CompareTo(Day2) >= 0;

        #endregion

        #endregion

        #region IComparable<Day> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two Days.
        /// </summary>
        /// <param name="Object">Day to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is Day Day
                   ? CompareTo(Day)
                   : throw new ArgumentException("The given object is not Day!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(Day)

        /// <summary>
        /// Compares two Days.
        /// </summary>
        /// <param name="Day">Day to compare with.</param>
        public Int32 CompareTo(Day Day)

            => String.Compare(InternalId,
                              Day.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<Day> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two Days for equality.
        /// </summary>
        /// <param name="Object">Day to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Day Day &&
                   Equals(Day);

        #endregion

        #region Equals(Day)

        /// <summary>
        /// Compares two Days for equality.
        /// </summary>
        /// <param name="Day">Day to compare with.</param>
        public Boolean Equals(Day Day)

            => String.Equals(InternalId,
                             Day.InternalId,
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
