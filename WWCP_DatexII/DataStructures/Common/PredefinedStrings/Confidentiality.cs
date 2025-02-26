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
    /// Extension methods for confidentialitys.
    /// </summary>
    public static class ConfidentialityExtensions
    {

        /// <summary>
        /// Indicates whether this confidentiality is null or empty.
        /// </summary>
        /// <param name="Confidentiality">A confidentiality.</param>
        public static Boolean IsNullOrEmpty(this Confidentiality? Confidentiality)
            => !Confidentiality.HasValue || Confidentiality.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this confidentiality is null or empty.
        /// </summary>
        /// <param name="Confidentiality">A confidentiality.</param>
        public static Boolean IsNotNullOrEmpty(this Confidentiality? Confidentiality)
            => Confidentiality.HasValue && Confidentiality.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A confidentiality.
    /// </summary>
    public readonly struct Confidentiality : IId,
                                             IEquatable<Confidentiality>,
                                             IComparable<Confidentiality>
    {

        #region Data

        private readonly static Dictionary<String, Confidentiality>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this confidentiality is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this confidentiality is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the confidentiality.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<Confidentiality>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new confidentiality based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a confidentiality.</param>
        private Confidentiality(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static Confidentiality Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new Confidentiality(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a confidentiality.
        /// </summary>
        /// <param name="Text">A text representation of a confidentiality.</param>
        public static Confidentiality Parse(String Text)
        {

            if (TryParse(Text, out var confidentiality))
                return confidentiality;

            throw new ArgumentException($"Invalid text representation of a confidentiality: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a confidentiality.
        /// </summary>
        /// <param name="Text">A text representation of a confidentiality.</param>
        public static Confidentiality? TryParse(String Text)
        {

            if (TryParse(Text, out var confidentiality))
                return confidentiality;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out Confidentiality)

        /// <summary>
        /// Try to parse the given text as confidentiality.
        /// </summary>
        /// <param name="Text">A text representation of a confidentiality.</param>
        /// <param name="Confidentiality">The parsed confidentiality.</param>
        public static Boolean TryParse(String Text, out Confidentiality Confidentiality)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out Confidentiality))
                    Confidentiality = Register(Text);

                return true;

            }

            Confidentiality = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this confidentiality.
        /// </summary>
        public Confidentiality Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// For internal use only of the recipient organisation.
        /// </summary>
        public static Confidentiality  InternalUse                                   { get; }
            = Register("internalUse");

        /// <summary>
        /// No restriction on usage.
        /// </summary>
        public static Confidentiality  NoRestriction                                 { get; }
            = Register("noRestriction");

        /// <summary>
        /// Restricted for use only by authorities.
        /// </summary>
        public static Confidentiality  RestrictedToAuthorities                       { get; }
            = Register("restrictedToAuthorities");

        /// <summary>
        /// Restricted for use only by authorities and traffic operators.
        /// </summary>
        public static Confidentiality  RestrictedToAuthoritiesAndTrafficOperators    { get; }
            = Register("restrictedToAuthoritiesAndTrafficOperators");

        #endregion


        #region Operator overloading

        #region Operator == (Confidentiality1, Confidentiality2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Confidentiality1">A confidentiality.</param>
        /// <param name="Confidentiality2">Another confidentiality.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Confidentiality Confidentiality1,
                                           Confidentiality Confidentiality2)

            => Confidentiality1.Equals(Confidentiality2);

        #endregion

        #region Operator != (Confidentiality1, Confidentiality2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Confidentiality1">A confidentiality.</param>
        /// <param name="Confidentiality2">Another confidentiality.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Confidentiality Confidentiality1,
                                           Confidentiality Confidentiality2)

            => !Confidentiality1.Equals(Confidentiality2);

        #endregion

        #region Operator <  (Confidentiality1, Confidentiality2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Confidentiality1">A confidentiality.</param>
        /// <param name="Confidentiality2">Another confidentiality.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Confidentiality Confidentiality1,
                                          Confidentiality Confidentiality2)

            => Confidentiality1.CompareTo(Confidentiality2) < 0;

        #endregion

        #region Operator <= (Confidentiality1, Confidentiality2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Confidentiality1">A confidentiality.</param>
        /// <param name="Confidentiality2">Another confidentiality.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Confidentiality Confidentiality1,
                                           Confidentiality Confidentiality2)

            => Confidentiality1.CompareTo(Confidentiality2) <= 0;

        #endregion

        #region Operator >  (Confidentiality1, Confidentiality2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Confidentiality1">A confidentiality.</param>
        /// <param name="Confidentiality2">Another confidentiality.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Confidentiality Confidentiality1,
                                          Confidentiality Confidentiality2)

            => Confidentiality1.CompareTo(Confidentiality2) > 0;

        #endregion

        #region Operator >= (Confidentiality1, Confidentiality2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Confidentiality1">A confidentiality.</param>
        /// <param name="Confidentiality2">Another confidentiality.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Confidentiality Confidentiality1,
                                           Confidentiality Confidentiality2)

            => Confidentiality1.CompareTo(Confidentiality2) >= 0;

        #endregion

        #endregion

        #region IComparable<Confidentiality> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two confidentialitys.
        /// </summary>
        /// <param name="Object">confidentiality to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is Confidentiality confidentiality
                   ? CompareTo(confidentiality)
                   : throw new ArgumentException("The given object is not confidentiality!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(Confidentiality)

        /// <summary>
        /// Compares two confidentialitys.
        /// </summary>
        /// <param name="Confidentiality">confidentiality to compare with.</param>
        public Int32 CompareTo(Confidentiality Confidentiality)

            => String.Compare(InternalId,
                              Confidentiality.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<Confidentiality> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two confidentialitys for equality.
        /// </summary>
        /// <param name="Object">confidentiality to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Confidentiality confidentiality &&
                   Equals(confidentiality);

        #endregion

        #region Equals(Confidentiality)

        /// <summary>
        /// Compares two confidentialitys for equality.
        /// </summary>
        /// <param name="Confidentiality">confidentiality to compare with.</param>
        public Boolean Equals(Confidentiality Confidentiality)

            => String.Equals(InternalId,
                             Confidentiality.InternalId,
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
