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
    /// Extension methods for PositionConfidenceCodedErrors.
    /// </summary>
    public static class PositionConfidenceCodedErrorExtensions
    {

        /// <summary>
        /// Indicates whether this PositionConfidenceCodedError is null or empty.
        /// </summary>
        /// <param name="PositionConfidenceCodedError">A PositionConfidenceCodedError.</param>
        public static Boolean IsNullOrEmpty(this PositionConfidenceCodedError? PositionConfidenceCodedError)
            => !PositionConfidenceCodedError.HasValue || PositionConfidenceCodedError.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this PositionConfidenceCodedError is null or empty.
        /// </summary>
        /// <param name="PositionConfidenceCodedError">A PositionConfidenceCodedError.</param>
        public static Boolean IsNotNullOrEmpty(this PositionConfidenceCodedError? PositionConfidenceCodedError)
            => PositionConfidenceCodedError.HasValue && PositionConfidenceCodedError.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A PositionConfidenceCodedError.
    /// </summary>
    public readonly struct PositionConfidenceCodedError : IId,
                                                          IEquatable<PositionConfidenceCodedError>,
                                                          IComparable<PositionConfidenceCodedError>
    {

        #region Data

        private readonly static Dictionary<String, PositionConfidenceCodedError>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this PositionConfidenceCodedError is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this PositionConfidenceCodedError is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the PositionConfidenceCodedError.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<PositionConfidenceCodedError>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new PositionConfidenceCodedError based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a PositionConfidenceCodedError.</param>
        private PositionConfidenceCodedError(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static PositionConfidenceCodedError Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new PositionConfidenceCodedError(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a PositionConfidenceCodedError.
        /// </summary>
        /// <param name="Text">A text representation of a PositionConfidenceCodedError.</param>
        public static PositionConfidenceCodedError Parse(String Text)
        {

            if (TryParse(Text, out var positionConfidenceCodedError))
                return positionConfidenceCodedError;

            throw new ArgumentException($"Invalid text representation of a PositionConfidenceCodedError: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a PositionConfidenceCodedError.
        /// </summary>
        /// <param name="Text">A text representation of a PositionConfidenceCodedError.</param>
        public static PositionConfidenceCodedError? TryParse(String Text)
        {

            if (TryParse(Text, out var positionConfidenceCodedError))
                return positionConfidenceCodedError;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out PositionConfidenceCodedError)

        /// <summary>
        /// Try to parse the given text as PositionConfidenceCodedError.
        /// </summary>
        /// <param name="Text">A text representation of a PositionConfidenceCodedError.</param>
        /// <param name="PositionConfidenceCodedError">The parsed PositionConfidenceCodedError.</param>
        public static Boolean TryParse(String Text, out PositionConfidenceCodedError PositionConfidenceCodedError)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out PositionConfidenceCodedError))
                    PositionConfidenceCodedError = Register(Text);

                return true;

            }

            PositionConfidenceCodedError = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this PositionConfidenceCodedError.
        /// </summary>
        public PositionConfidenceCodedError Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Indicates the accuracy is out of range, i.e. greater than 4093 cm for horizontal position.
        /// </summary>
        public static PositionConfidenceCodedError  OutOfRange     { get; }
            = Register("outOfRange");

        /// <summary>
        /// Indicates the accuracy information is unavailable.
        /// </summary>
        public static PositionConfidenceCodedError  Unavailable    { get; }
            = Register("unavailable");

        #endregion


        #region Operator overloading

        #region Operator == (PositionConfidenceCodedError1, PositionConfidenceCodedError2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PositionConfidenceCodedError1">A PositionConfidenceCodedError.</param>
        /// <param name="PositionConfidenceCodedError2">Another PositionConfidenceCodedError.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (PositionConfidenceCodedError PositionConfidenceCodedError1,
                                           PositionConfidenceCodedError PositionConfidenceCodedError2)

            => PositionConfidenceCodedError1.Equals(PositionConfidenceCodedError2);

        #endregion

        #region Operator != (PositionConfidenceCodedError1, PositionConfidenceCodedError2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PositionConfidenceCodedError1">A PositionConfidenceCodedError.</param>
        /// <param name="PositionConfidenceCodedError2">Another PositionConfidenceCodedError.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (PositionConfidenceCodedError PositionConfidenceCodedError1,
                                           PositionConfidenceCodedError PositionConfidenceCodedError2)

            => !PositionConfidenceCodedError1.Equals(PositionConfidenceCodedError2);

        #endregion

        #region Operator <  (PositionConfidenceCodedError1, PositionConfidenceCodedError2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PositionConfidenceCodedError1">A PositionConfidenceCodedError.</param>
        /// <param name="PositionConfidenceCodedError2">Another PositionConfidenceCodedError.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (PositionConfidenceCodedError PositionConfidenceCodedError1,
                                          PositionConfidenceCodedError PositionConfidenceCodedError2)

            => PositionConfidenceCodedError1.CompareTo(PositionConfidenceCodedError2) < 0;

        #endregion

        #region Operator <= (PositionConfidenceCodedError1, PositionConfidenceCodedError2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PositionConfidenceCodedError1">A PositionConfidenceCodedError.</param>
        /// <param name="PositionConfidenceCodedError2">Another PositionConfidenceCodedError.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (PositionConfidenceCodedError PositionConfidenceCodedError1,
                                           PositionConfidenceCodedError PositionConfidenceCodedError2)

            => PositionConfidenceCodedError1.CompareTo(PositionConfidenceCodedError2) <= 0;

        #endregion

        #region Operator >  (PositionConfidenceCodedError1, PositionConfidenceCodedError2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PositionConfidenceCodedError1">A PositionConfidenceCodedError.</param>
        /// <param name="PositionConfidenceCodedError2">Another PositionConfidenceCodedError.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (PositionConfidenceCodedError PositionConfidenceCodedError1,
                                          PositionConfidenceCodedError PositionConfidenceCodedError2)

            => PositionConfidenceCodedError1.CompareTo(PositionConfidenceCodedError2) > 0;

        #endregion

        #region Operator >= (PositionConfidenceCodedError1, PositionConfidenceCodedError2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PositionConfidenceCodedError1">A PositionConfidenceCodedError.</param>
        /// <param name="PositionConfidenceCodedError2">Another PositionConfidenceCodedError.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (PositionConfidenceCodedError PositionConfidenceCodedError1,
                                           PositionConfidenceCodedError PositionConfidenceCodedError2)

            => PositionConfidenceCodedError1.CompareTo(PositionConfidenceCodedError2) >= 0;

        #endregion

        #endregion

        #region IComparable<PositionConfidenceCodedError> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two PositionConfidenceCodedErrors.
        /// </summary>
        /// <param name="Object">PositionConfidenceCodedError to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is PositionConfidenceCodedError PositionConfidenceCodedError
                   ? CompareTo(PositionConfidenceCodedError)
                   : throw new ArgumentException("The given object is not PositionConfidenceCodedError!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(PositionConfidenceCodedError)

        /// <summary>
        /// Compares two PositionConfidenceCodedErrors.
        /// </summary>
        /// <param name="PositionConfidenceCodedError">PositionConfidenceCodedError to compare with.</param>
        public Int32 CompareTo(PositionConfidenceCodedError PositionConfidenceCodedError)

            => String.Compare(InternalId,
                              PositionConfidenceCodedError.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<PositionConfidenceCodedError> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two PositionConfidenceCodedErrors for equality.
        /// </summary>
        /// <param name="Object">PositionConfidenceCodedError to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is PositionConfidenceCodedError PositionConfidenceCodedError &&
                   Equals(PositionConfidenceCodedError);

        #endregion

        #region Equals(PositionConfidenceCodedError)

        /// <summary>
        /// Compares two PositionConfidenceCodedErrors for equality.
        /// </summary>
        /// <param name="PositionConfidenceCodedError">PositionConfidenceCodedError to compare with.</param>
        public Boolean Equals(PositionConfidenceCodedError PositionConfidenceCodedError)

            => String.Equals(InternalId,
                             PositionConfidenceCodedError.InternalId,
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
