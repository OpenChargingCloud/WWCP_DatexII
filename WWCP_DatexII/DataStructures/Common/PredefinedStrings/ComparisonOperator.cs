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
    /// Extension methods for ComparisonOperators.
    /// </summary>
    public static class ComparisonOperatorExtensions
    {

        /// <summary>
        /// Indicates whether this ComparisonOperator is null or empty.
        /// </summary>
        /// <param name="ComparisonOperator">A ComparisonOperator.</param>
        public static Boolean IsNullOrEmpty(this ComparisonOperator? ComparisonOperator)
            => !ComparisonOperator.HasValue || ComparisonOperator.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this ComparisonOperator is null or empty.
        /// </summary>
        /// <param name="ComparisonOperator">A ComparisonOperator.</param>
        public static Boolean IsNotNullOrEmpty(this ComparisonOperator? ComparisonOperator)
            => ComparisonOperator.HasValue && ComparisonOperator.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A ComparisonOperator.
    /// </summary>
    public readonly struct ComparisonOperator : IId,
                                                IEquatable<ComparisonOperator>,
                                                IComparable<ComparisonOperator>
    {

        #region Data

        private readonly static Dictionary<String, ComparisonOperator>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this ComparisonOperator is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this ComparisonOperator is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the ComparisonOperator.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<ComparisonOperator>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ComparisonOperator based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a ComparisonOperator.</param>
        private ComparisonOperator(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ComparisonOperator Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ComparisonOperator(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a ComparisonOperator.
        /// </summary>
        /// <param name="Text">A text representation of a ComparisonOperator.</param>
        public static ComparisonOperator Parse(String Text)
        {

            if (TryParse(Text, out var comparisonOperator))
                return comparisonOperator;

            throw new ArgumentException($"Invalid text representation of a ComparisonOperator: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a ComparisonOperator.
        /// </summary>
        /// <param name="Text">A text representation of a ComparisonOperator.</param>
        public static ComparisonOperator? TryParse(String Text)
        {

            if (TryParse(Text, out var comparisonOperator))
                return comparisonOperator;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ComparisonOperator)

        /// <summary>
        /// Try to parse the given text as ComparisonOperator.
        /// </summary>
        /// <param name="Text">A text representation of a ComparisonOperator.</param>
        /// <param name="ComparisonOperator">The parsed ComparisonOperator.</param>
        public static Boolean TryParse(String Text, out ComparisonOperator ComparisonOperator)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ComparisonOperator))
                    ComparisonOperator = Register(Text);

                return true;

            }

            ComparisonOperator = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this ComparisonOperator.
        /// </summary>
        public ComparisonOperator Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Logical comparison operator of "equal to".
        /// </summary>
        public static ComparisonOperator  EqualTo                 { get; }
            = Register("equalTo");

        /// <summary>
        /// Logical comparison operator of "greater than".
        /// </summary>
        public static ComparisonOperator  GreaterThan             { get; }
            = Register("greaterThan");

        /// <summary>
        /// Logical comparison operator of "greater than or equal to".
        /// </summary>
        public static ComparisonOperator  GreaterThanOrEqualTo    { get; }
            = Register("greaterThanOrEqualTo");

        /// <summary>
        /// Logical comparison operator of "less than".
        /// </summary>
        public static ComparisonOperator  LessThan                { get; }
            = Register("lessThan");

        /// <summary>
        /// Logical comparison operator of "less than or equal to".
        /// </summary>
        public static ComparisonOperator  LessThanOrEqualTo       { get; }
            = Register("lessThanOrEqualTo");

        #endregion


        #region Operator overloading

        #region Operator == (ComparisonOperator1, ComparisonOperator2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ComparisonOperator1">A ComparisonOperator.</param>
        /// <param name="ComparisonOperator2">Another ComparisonOperator.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ComparisonOperator ComparisonOperator1,
                                           ComparisonOperator ComparisonOperator2)

            => ComparisonOperator1.Equals(ComparisonOperator2);

        #endregion

        #region Operator != (ComparisonOperator1, ComparisonOperator2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ComparisonOperator1">A ComparisonOperator.</param>
        /// <param name="ComparisonOperator2">Another ComparisonOperator.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ComparisonOperator ComparisonOperator1,
                                           ComparisonOperator ComparisonOperator2)

            => !ComparisonOperator1.Equals(ComparisonOperator2);

        #endregion

        #region Operator <  (ComparisonOperator1, ComparisonOperator2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ComparisonOperator1">A ComparisonOperator.</param>
        /// <param name="ComparisonOperator2">Another ComparisonOperator.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ComparisonOperator ComparisonOperator1,
                                          ComparisonOperator ComparisonOperator2)

            => ComparisonOperator1.CompareTo(ComparisonOperator2) < 0;

        #endregion

        #region Operator <= (ComparisonOperator1, ComparisonOperator2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ComparisonOperator1">A ComparisonOperator.</param>
        /// <param name="ComparisonOperator2">Another ComparisonOperator.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ComparisonOperator ComparisonOperator1,
                                           ComparisonOperator ComparisonOperator2)

            => ComparisonOperator1.CompareTo(ComparisonOperator2) <= 0;

        #endregion

        #region Operator >  (ComparisonOperator1, ComparisonOperator2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ComparisonOperator1">A ComparisonOperator.</param>
        /// <param name="ComparisonOperator2">Another ComparisonOperator.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ComparisonOperator ComparisonOperator1,
                                          ComparisonOperator ComparisonOperator2)

            => ComparisonOperator1.CompareTo(ComparisonOperator2) > 0;

        #endregion

        #region Operator >= (ComparisonOperator1, ComparisonOperator2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ComparisonOperator1">A ComparisonOperator.</param>
        /// <param name="ComparisonOperator2">Another ComparisonOperator.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ComparisonOperator ComparisonOperator1,
                                           ComparisonOperator ComparisonOperator2)

            => ComparisonOperator1.CompareTo(ComparisonOperator2) >= 0;

        #endregion

        #endregion

        #region IComparable<ComparisonOperator> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two ComparisonOperators.
        /// </summary>
        /// <param name="Object">ComparisonOperator to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ComparisonOperator ComparisonOperator
                   ? CompareTo(ComparisonOperator)
                   : throw new ArgumentException("The given object is not ComparisonOperator!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ComparisonOperator)

        /// <summary>
        /// Compares two ComparisonOperators.
        /// </summary>
        /// <param name="ComparisonOperator">ComparisonOperator to compare with.</param>
        public Int32 CompareTo(ComparisonOperator ComparisonOperator)

            => String.Compare(InternalId,
                              ComparisonOperator.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<ComparisonOperator> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two ComparisonOperators for equality.
        /// </summary>
        /// <param name="Object">ComparisonOperator to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ComparisonOperator ComparisonOperator &&
                   Equals(ComparisonOperator);

        #endregion

        #region Equals(ComparisonOperator)

        /// <summary>
        /// Compares two ComparisonOperators for equality.
        /// </summary>
        /// <param name="ComparisonOperator">ComparisonOperator to compare with.</param>
        public Boolean Equals(ComparisonOperator ComparisonOperator)

            => String.Equals(InternalId,
                             ComparisonOperator.InternalId,
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
