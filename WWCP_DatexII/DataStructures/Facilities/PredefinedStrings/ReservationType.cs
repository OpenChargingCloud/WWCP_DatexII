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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Extension methods for ReservationTypes.
    /// </summary>
    public static class ReservationTypeExtensions
    {

        /// <summary>
        /// Indicates whether this ReservationType is null or empty.
        /// </summary>
        /// <param name="ReservationType">A ReservationType.</param>
        public static Boolean IsNullOrEmpty(this ReservationType? ReservationType)
            => !ReservationType.HasValue || ReservationType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this ReservationType is null or empty.
        /// </summary>
        /// <param name="ReservationType">A ReservationType.</param>
        public static Boolean IsNotNullOrEmpty(this ReservationType? ReservationType)
            => ReservationType.HasValue && ReservationType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A ReservationType.
    /// </summary>
    public readonly struct ReservationType : IId,
                                             IEquatable<ReservationType>,
                                             IComparable<ReservationType>
    {

        #region Data

        private readonly static Dictionary<String, ReservationType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this ReservationType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this ReservationType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the ReservationType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered ReservationTypes.
        /// </summary>
        public static    IEnumerable<ReservationType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ReservationType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a ReservationType.</param>
        private ReservationType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ReservationType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ReservationType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a ReservationType.
        /// </summary>
        /// <param name="Text">A text representation of a ReservationType.</param>
        public static ReservationType Parse(String Text)
        {

            if (TryParse(Text, out var reservationType))
                return reservationType;

            throw new ArgumentException($"Invalid text representation of a ReservationType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a ReservationType.
        /// </summary>
        /// <param name="Text">A text representation of a ReservationType.</param>
        public static ReservationType? TryParse(String Text)
        {

            if (TryParse(Text, out var reservationType))
                return reservationType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ReservationType)

        /// <summary>
        /// Try to parse the given text as a ReservationType.
        /// </summary>
        /// <param name="Text">A text representation of a ReservationType.</param>
        /// <param name="ReservationType">The parsed ReservationType.</param>
        public static Boolean TryParse(String Text, out ReservationType ReservationType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ReservationType))
                    ReservationType = Register(Text);

                return true;

            }

            ReservationType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this ReservationType.
        /// </summary>
        public ReservationType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Optional
        /// </summary>
        public static ReservationType  Optional        { get; }
            = Register("optional");

        /// <summary>
        /// Mandatory
        /// </summary>
        public static ReservationType  Mandatory       { get; }
            = Register("mandatory");

        /// <summary>
        /// Not Available
        /// </summary>
        public static ReservationType  NotAvailable    { get; }
            = Register("notAvailable");

        /// <summary>
        /// Partly
        /// </summary>
        public static ReservationType  Partly          { get; }
            = Register("partly");

        /// <summary>
        /// Unknown
        /// </summary>
        public static ReservationType  Unknown         { get; }
            = Register("unknown");

        /// <summary>
        /// Unspecified
        /// </summary>
        public static ReservationType  Unspecified     { get; }
            = Register("unspecified");

        #endregion


        #region Operator overloading

        #region Operator == (ReservationType1, ReservationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReservationType1">A ReservationType.</param>
        /// <param name="ReservationType2">Another ReservationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ReservationType ReservationType1,
                                           ReservationType ReservationType2)

            => ReservationType1.Equals(ReservationType2);

        #endregion

        #region Operator != (ReservationType1, ReservationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReservationType1">A ReservationType.</param>
        /// <param name="ReservationType2">Another ReservationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ReservationType ReservationType1,
                                           ReservationType ReservationType2)

            => !ReservationType1.Equals(ReservationType2);

        #endregion

        #region Operator <  (ReservationType1, ReservationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReservationType1">A ReservationType.</param>
        /// <param name="ReservationType2">Another ReservationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ReservationType ReservationType1,
                                          ReservationType ReservationType2)

            => ReservationType1.CompareTo(ReservationType2) < 0;

        #endregion

        #region Operator <= (ReservationType1, ReservationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReservationType1">A ReservationType.</param>
        /// <param name="ReservationType2">Another ReservationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ReservationType ReservationType1,
                                           ReservationType ReservationType2)

            => ReservationType1.CompareTo(ReservationType2) <= 0;

        #endregion

        #region Operator >  (ReservationType1, ReservationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReservationType1">A ReservationType.</param>
        /// <param name="ReservationType2">Another ReservationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ReservationType ReservationType1,
                                          ReservationType ReservationType2)

            => ReservationType1.CompareTo(ReservationType2) > 0;

        #endregion

        #region Operator >= (ReservationType1, ReservationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReservationType1">A ReservationType.</param>
        /// <param name="ReservationType2">Another ReservationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ReservationType ReservationType1,
                                           ReservationType ReservationType2)

            => ReservationType1.CompareTo(ReservationType2) >= 0;

        #endregion

        #endregion

        #region IComparable<ReservationType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two ReservationTypes.
        /// </summary>
        /// <param name="Object">A ReservationType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ReservationType reservationType
                   ? CompareTo(reservationType)
                   : throw new ArgumentException("The given object is not a ReservationType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ReservationType)

        /// <summary>
        /// Compares two ReservationTypes.
        /// </summary>
        /// <param name="ReservationType">A ReservationType to compare with.</param>
        public Int32 CompareTo(ReservationType ReservationType)

            => String.Compare(InternalId,
                              ReservationType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<ReservationType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two ReservationTypes for equality.
        /// </summary>
        /// <param name="Object">A ReservationType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ReservationType reservationType &&
                   Equals(reservationType);

        #endregion

        #region Equals(ReservationType)

        /// <summary>
        /// Compares two ReservationTypes for equality.
        /// </summary>
        /// <param name="ReservationType">A ReservationType to compare with.</param>
        public Boolean Equals(ReservationType ReservationType)

            => String.Equals(InternalId,
                             ReservationType.InternalId,
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
