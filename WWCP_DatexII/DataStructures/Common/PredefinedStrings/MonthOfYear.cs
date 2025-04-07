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
    /// Extension methods for MonthOfYears.
    /// </summary>
    public static class MonthOfYearExtensions
    {

        /// <summary>
        /// Indicates whether this MonthOfYear is null or empty.
        /// </summary>
        /// <param name="MonthOfYear">A MonthOfYear.</param>
        public static Boolean IsNullOrEmpty(this MonthOfYear? MonthOfYear)
            => !MonthOfYear.HasValue || MonthOfYear.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this MonthOfYear is null or empty.
        /// </summary>
        /// <param name="MonthOfYear">A MonthOfYear.</param>
        public static Boolean IsNotNullOrEmpty(this MonthOfYear? MonthOfYear)
            => MonthOfYear.HasValue && MonthOfYear.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A MonthOfYear.
    /// </summary>
    public readonly struct MonthOfYear : IId,
                                         IEquatable<MonthOfYear>,
                                         IComparable<MonthOfYear>
    {

        #region Data

        private readonly static Dictionary<String, MonthOfYear>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this MonthOfYear is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this MonthOfYear is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the MonthOfYear.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<MonthOfYear>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new MonthOfYear based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a MonthOfYear.</param>
        private MonthOfYear(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static MonthOfYear Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new MonthOfYear(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a MonthOfYear.
        /// </summary>
        /// <param name="Text">A text representation of a MonthOfYear.</param>
        public static MonthOfYear Parse(String Text)
        {

            if (TryParse(Text, out var monthOfYear))
                return monthOfYear;

            throw new ArgumentException($"Invalid text representation of a MonthOfYear: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a MonthOfYear.
        /// </summary>
        /// <param name="Text">A text representation of a MonthOfYear.</param>
        public static MonthOfYear? TryParse(String Text)
        {

            if (TryParse(Text, out var monthOfYear))
                return monthOfYear;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out MonthOfYear)

        /// <summary>
        /// Try to parse the given text as MonthOfYear.
        /// </summary>
        /// <param name="Text">A text representation of a MonthOfYear.</param>
        /// <param name="MonthOfYear">The parsed MonthOfYear.</param>
        public static Boolean TryParse(String Text, out MonthOfYear MonthOfYear)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out MonthOfYear))
                    MonthOfYear = Register(Text);

                return true;

            }

            MonthOfYear = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this MonthOfYear.
        /// </summary>
        public MonthOfYear Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// January
        /// </summary>
        public static MonthOfYear  January      { get; }
            = Register("january");

        /// <summary>
        /// February
        /// </summary>
        public static MonthOfYear  February     { get; }
            = Register("february");

        /// <summary>
        /// March
        /// </summary>
        public static MonthOfYear  March        { get; }
            = Register("march");

        /// <summary>
        /// April
        /// </summary>
        public static MonthOfYear  April        { get; }
            = Register("april");

        /// <summary>
        /// May
        /// </summary>
        public static MonthOfYear  May          { get; }
            = Register("may");

        /// <summary>
        /// June
        /// </summary>
        public static MonthOfYear  June         { get; }
            = Register("june");

        /// <summary>
        /// July
        /// </summary>
        public static MonthOfYear  July         { get; }
            = Register("july");

        /// <summary>
        /// August
        /// </summary>
        public static MonthOfYear  August       { get; }
            = Register("august");

        /// <summary>
        /// September
        /// </summary>
        public static MonthOfYear  September    { get; }
            = Register("september");

        /// <summary>
        /// October
        /// </summary>
        public static MonthOfYear  October      { get; }
            = Register("october");

        /// <summary>
        /// November
        /// </summary>
        public static MonthOfYear  November     { get; }
            = Register("november");

        /// <summary>
        /// December
        /// </summary>
        public static MonthOfYear  December     { get; }
            = Register("december");

        #endregion


        #region Operator overloading

        #region Operator == (MonthOfYear1, MonthOfYear2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MonthOfYear1">A MonthOfYear.</param>
        /// <param name="MonthOfYear2">Another MonthOfYear.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (MonthOfYear MonthOfYear1,
                                           MonthOfYear MonthOfYear2)

            => MonthOfYear1.Equals(MonthOfYear2);

        #endregion

        #region Operator != (MonthOfYear1, MonthOfYear2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MonthOfYear1">A MonthOfYear.</param>
        /// <param name="MonthOfYear2">Another MonthOfYear.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (MonthOfYear MonthOfYear1,
                                           MonthOfYear MonthOfYear2)

            => !MonthOfYear1.Equals(MonthOfYear2);

        #endregion

        #region Operator <  (MonthOfYear1, MonthOfYear2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MonthOfYear1">A MonthOfYear.</param>
        /// <param name="MonthOfYear2">Another MonthOfYear.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (MonthOfYear MonthOfYear1,
                                          MonthOfYear MonthOfYear2)

            => MonthOfYear1.CompareTo(MonthOfYear2) < 0;

        #endregion

        #region Operator <= (MonthOfYear1, MonthOfYear2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MonthOfYear1">A MonthOfYear.</param>
        /// <param name="MonthOfYear2">Another MonthOfYear.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (MonthOfYear MonthOfYear1,
                                           MonthOfYear MonthOfYear2)

            => MonthOfYear1.CompareTo(MonthOfYear2) <= 0;

        #endregion

        #region Operator >  (MonthOfYear1, MonthOfYear2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MonthOfYear1">A MonthOfYear.</param>
        /// <param name="MonthOfYear2">Another MonthOfYear.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (MonthOfYear MonthOfYear1,
                                          MonthOfYear MonthOfYear2)

            => MonthOfYear1.CompareTo(MonthOfYear2) > 0;

        #endregion

        #region Operator >= (MonthOfYear1, MonthOfYear2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MonthOfYear1">A MonthOfYear.</param>
        /// <param name="MonthOfYear2">Another MonthOfYear.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (MonthOfYear MonthOfYear1,
                                           MonthOfYear MonthOfYear2)

            => MonthOfYear1.CompareTo(MonthOfYear2) >= 0;

        #endregion

        #endregion

        #region IComparable<MonthOfYear> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two MonthOfYears.
        /// </summary>
        /// <param name="Object">MonthOfYear to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is MonthOfYear MonthOfYear
                   ? CompareTo(MonthOfYear)
                   : throw new ArgumentException("The given object is not MonthOfYear!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(MonthOfYear)

        /// <summary>
        /// Compares two MonthOfYears.
        /// </summary>
        /// <param name="MonthOfYear">MonthOfYear to compare with.</param>
        public Int32 CompareTo(MonthOfYear MonthOfYear)

            => String.Compare(InternalId,
                              MonthOfYear.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<MonthOfYear> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two MonthOfYears for equality.
        /// </summary>
        /// <param name="Object">MonthOfYear to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is MonthOfYear MonthOfYear &&
                   Equals(MonthOfYear);

        #endregion

        #region Equals(MonthOfYear)

        /// <summary>
        /// Compares two MonthOfYears for equality.
        /// </summary>
        /// <param name="MonthOfYear">MonthOfYear to compare with.</param>
        public Boolean Equals(MonthOfYear MonthOfYear)

            => String.Equals(InternalId,
                             MonthOfYear.InternalId,
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
