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
    /// Extension methods for InstanceOfDays.
    /// </summary>
    public static class InstanceOfDayExtensions
    {

        /// <summary>
        /// Indicates whether this InstanceOfDay is null or empty.
        /// </summary>
        /// <param name="InstanceOfDay">An InstanceOfDay.</param>
        public static Boolean IsNullOrEmpty(this InstanceOfDay? InstanceOfDay)
            => !InstanceOfDay.HasValue || InstanceOfDay.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this InstanceOfDay is null or empty.
        /// </summary>
        /// <param name="InstanceOfDay">An InstanceOfDay.</param>
        public static Boolean IsNotNullOrEmpty(this InstanceOfDay? InstanceOfDay)
            => InstanceOfDay.HasValue && InstanceOfDay.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An InstanceOfDay.
    /// </summary>
    public readonly struct InstanceOfDay : IId,
                                           IEquatable<InstanceOfDay>,
                                           IComparable<InstanceOfDay>
    {

        #region Data

        private readonly static Dictionary<String, InstanceOfDay>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                                          InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this InstanceOfDay is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this InstanceOfDay is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the InstanceOfDay.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered InstanceOfDays.
        /// </summary>
        public static    IEnumerable<InstanceOfDay>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new InstanceOfDay based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an InstanceOfDay.</param>
        private InstanceOfDay(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static InstanceOfDay Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new InstanceOfDay(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an InstanceOfDay.
        /// </summary>
        /// <param name="Text">A text representation of an InstanceOfDay.</param>
        public static InstanceOfDay Parse(String Text)
        {

            if (TryParse(Text, out var instanceOfDay))
                return instanceOfDay;

            throw new ArgumentException($"Invalid text representation of an InstanceOfDay: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an InstanceOfDay.
        /// </summary>
        /// <param name="Text">A text representation of an InstanceOfDay.</param>
        public static InstanceOfDay? TryParse(String Text)
        {

            if (TryParse(Text, out var InstanceOfDay))
                return InstanceOfDay;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out InstanceOfDay)

        /// <summary>
        /// Try to parse the given text as an InstanceOfDay.
        /// </summary>
        /// <param name="Text">A text representation of an InstanceOfDay.</param>
        /// <param name="InstanceOfDay">The parsed InstanceOfDay.</param>
        public static Boolean TryParse(String Text, out InstanceOfDay InstanceOfDay)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out InstanceOfDay))
                    InstanceOfDay = Register(Text);

                return true;

            }

            InstanceOfDay = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this InstanceOfDay.
        /// </summary>
        public InstanceOfDay Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// First instance of specified day of week in month.
        /// </summary>
        public static InstanceOfDay  FirstInstance     { get; }
            = Register("firstInstance");

        /// <summary>
        /// Second instance of specified day of week in month.
        /// </summary>
        public static InstanceOfDay  SecondInstance    { get; }
            = Register("secondInstance");

        /// <summary>
        /// Third instance of specified day of week in month.
        /// </summary>
        public static InstanceOfDay  ThirdInstance     { get; }
            = Register("thirdInstance");

        /// <summary>
        /// Fourth instance of specified day of week in month.
        /// </summary>
        public static InstanceOfDay  FourthInstance    { get; }
            = Register("fourthInstance");

        /// <summary>
        /// Fifth instance of specified day of week in month.
        /// </summary>
        public static InstanceOfDay  FifthInstance     { get; }
            = Register("fifthInstance");

        /// <summary>
        /// Last instance of specified day of week in month (regardless of its actual instance number).
        /// </summary>
        public static InstanceOfDay  LastInstance      { get; }
            = Register("lastInstance");

        #endregion


        #region Operator overloading

        #region Operator == (InstanceOfDay1, InstanceOfDay2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InstanceOfDay1">An InstanceOfDay.</param>
        /// <param name="InstanceOfDay2">Another InstanceOfDay.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (InstanceOfDay InstanceOfDay1,
                                           InstanceOfDay InstanceOfDay2)

            => InstanceOfDay1.Equals(InstanceOfDay2);

        #endregion

        #region Operator != (InstanceOfDay1, InstanceOfDay2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InstanceOfDay1">An InstanceOfDay.</param>
        /// <param name="InstanceOfDay2">Another InstanceOfDay.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (InstanceOfDay InstanceOfDay1,
                                           InstanceOfDay InstanceOfDay2)

            => !InstanceOfDay1.Equals(InstanceOfDay2);

        #endregion

        #region Operator <  (InstanceOfDay1, InstanceOfDay2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InstanceOfDay1">An InstanceOfDay.</param>
        /// <param name="InstanceOfDay2">Another InstanceOfDay.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (InstanceOfDay InstanceOfDay1,
                                          InstanceOfDay InstanceOfDay2)

            => InstanceOfDay1.CompareTo(InstanceOfDay2) < 0;

        #endregion

        #region Operator <= (InstanceOfDay1, InstanceOfDay2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InstanceOfDay1">An InstanceOfDay.</param>
        /// <param name="InstanceOfDay2">Another InstanceOfDay.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (InstanceOfDay InstanceOfDay1,
                                           InstanceOfDay InstanceOfDay2)

            => InstanceOfDay1.CompareTo(InstanceOfDay2) <= 0;

        #endregion

        #region Operator >  (InstanceOfDay1, InstanceOfDay2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InstanceOfDay1">An InstanceOfDay.</param>
        /// <param name="InstanceOfDay2">Another InstanceOfDay.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (InstanceOfDay InstanceOfDay1,
                                          InstanceOfDay InstanceOfDay2)

            => InstanceOfDay1.CompareTo(InstanceOfDay2) > 0;

        #endregion

        #region Operator >= (InstanceOfDay1, InstanceOfDay2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InstanceOfDay1">An InstanceOfDay.</param>
        /// <param name="InstanceOfDay2">Another InstanceOfDay.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (InstanceOfDay InstanceOfDay1,
                                           InstanceOfDay InstanceOfDay2)

            => InstanceOfDay1.CompareTo(InstanceOfDay2) >= 0;

        #endregion

        #endregion

        #region IComparable<InstanceOfDay> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two InstanceOfDays.
        /// </summary>
        /// <param name="Object">An InstanceOfDay to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is InstanceOfDay instanceOfDay
                   ? CompareTo(instanceOfDay)
                   : throw new ArgumentException("The given object is not an InstanceOfDay!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(InstanceOfDay)

        /// <summary>
        /// Compares two InstanceOfDays.
        /// </summary>
        /// <param name="InstanceOfDay">An InstanceOfDay to compare with.</param>
        public Int32 CompareTo(InstanceOfDay InstanceOfDay)

            => String.Compare(InternalId,
                              InstanceOfDay.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<InstanceOfDay> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two InstanceOfDays for equality.
        /// </summary>
        /// <param name="Object">An InstanceOfDay to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is InstanceOfDay instanceOfDay &&
                   Equals(instanceOfDay);

        #endregion

        #region Equals(InstanceOfDay)

        /// <summary>
        /// Compares two InstanceOfDays for equality.
        /// </summary>
        /// <param name="InstanceOfDay">An InstanceOfDay to compare with.</param>
        public Boolean Equals(InstanceOfDay InstanceOfDay)

            => String.Equals(InternalId,
                             InstanceOfDay.InternalId,
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
