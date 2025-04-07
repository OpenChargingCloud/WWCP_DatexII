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
    /// Extension methods for AltitudeAccuracies.
    /// </summary>
    public static class AltitudeAccuracyExtensions
    {

        /// <summary>
        /// Indicates whether this AltitudeAccuracy is null or empty.
        /// </summary>
        /// <param name="AltitudeAccuracy">An AltitudeAccuracy.</param>
        public static Boolean IsNullOrEmpty(this AltitudeAccuracy? AltitudeAccuracy)
            => !AltitudeAccuracy.HasValue || AltitudeAccuracy.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this AltitudeAccuracy is null or empty.
        /// </summary>
        /// <param name="AltitudeAccuracy">An AltitudeAccuracy.</param>
        public static Boolean IsNotNullOrEmpty(this AltitudeAccuracy? AltitudeAccuracy)
            => AltitudeAccuracy.HasValue && AltitudeAccuracy.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An AltitudeAccuracy.
    /// </summary>
    public readonly struct AltitudeAccuracy : IId,
                                              IEquatable<AltitudeAccuracy>,
                                              IComparable<AltitudeAccuracy>
    {

        #region Data

        private readonly static Dictionary<String, AltitudeAccuracy>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this AltitudeAccuracy is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this AltitudeAccuracy is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the AltitudeAccuracy.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<AltitudeAccuracy>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new AltitudeAccuracy based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an AltitudeAccuracy.</param>
        private AltitudeAccuracy(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static AltitudeAccuracy Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new AltitudeAccuracy(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an AltitudeAccuracy.
        /// </summary>
        /// <param name="Text">A text representation of an AltitudeAccuracy.</param>
        public static AltitudeAccuracy Parse(String Text)
        {

            if (TryParse(Text, out var areaPlace))
                return areaPlace;

            throw new ArgumentException($"Invalid text representation of an AltitudeAccuracy: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an AltitudeAccuracy.
        /// </summary>
        /// <param name="Text">A text representation of an AltitudeAccuracy.</param>
        public static AltitudeAccuracy? TryParse(String Text)
        {

            if (TryParse(Text, out var areaPlace))
                return areaPlace;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out AltitudeAccuracy)

        /// <summary>
        /// Try to parse the given text as AltitudeAccuracy.
        /// </summary>
        /// <param name="Text">A text representation of an AltitudeAccuracy.</param>
        /// <param name="AltitudeAccuracy">The parsed AltitudeAccuracy.</param>
        public static Boolean TryParse(String Text, out AltitudeAccuracy AltitudeAccuracy)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out AltitudeAccuracy))
                    AltitudeAccuracy = Register(Text);

                return true;

            }

            AltitudeAccuracy = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this AltitudeAccuracy.
        /// </summary>
        public AltitudeAccuracy Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 1 centimetre.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan1Centimetre      { get; }
            = Register("equalToOrLessThan1Centimetre");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 2 centimetres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan2Centimetres     { get; }
            = Register("equalToOrLessThan2Centimetres");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 5 centimetres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan5Centimetres     { get; }
            = Register("equalToOrLessThan5Centimetres");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 10 centimetres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan10Centimetres    { get; }
            = Register("equalToOrLessThan10Centimetres");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 20 centimetres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan20Centimetres    { get; }
            = Register("equalToOrLessThan20Centimetres");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 50 centimetres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan50Centimetres    { get; }
            = Register("equalToOrLessThan50Centimetres");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 1 metre.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan1Metre           { get; }
            = Register("equalToOrLessThan1Metre");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 2 metres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan2Metres          { get; }
            = Register("equalToOrLessThan2Metres");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 5 metres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan5Metres          { get; }
            = Register("equalToOrLessThan5Metres");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 10 metres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan10Metres         { get; }
            = Register("equalToOrLessThan10Metres");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 20 metres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan20Metres         { get; }
            = Register("equalToOrLessThan20Metres");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 50 metres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan50Metres         { get; }
            = Register("equalToOrLessThan50Metres");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 100 metres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan100Metres        { get; }
            = Register("equalToOrLessThan100Metres");

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 200 metres.
        /// </summary>
        public static AltitudeAccuracy  EqualToOrLessThan200Metres        { get; }
            = Register("equalToOrLessThan200Metres");

        #endregion


        #region Operator overloading

        #region Operator == (AltitudeAccuracy1, AltitudeAccuracy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AltitudeAccuracy1">An AltitudeAccuracy.</param>
        /// <param name="AltitudeAccuracy2">Another AltitudeAccuracy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (AltitudeAccuracy AltitudeAccuracy1,
                                           AltitudeAccuracy AltitudeAccuracy2)

            => AltitudeAccuracy1.Equals(AltitudeAccuracy2);

        #endregion

        #region Operator != (AltitudeAccuracy1, AltitudeAccuracy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AltitudeAccuracy1">An AltitudeAccuracy.</param>
        /// <param name="AltitudeAccuracy2">Another AltitudeAccuracy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (AltitudeAccuracy AltitudeAccuracy1,
                                           AltitudeAccuracy AltitudeAccuracy2)

            => !AltitudeAccuracy1.Equals(AltitudeAccuracy2);

        #endregion

        #region Operator <  (AltitudeAccuracy1, AltitudeAccuracy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AltitudeAccuracy1">An AltitudeAccuracy.</param>
        /// <param name="AltitudeAccuracy2">Another AltitudeAccuracy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (AltitudeAccuracy AltitudeAccuracy1,
                                          AltitudeAccuracy AltitudeAccuracy2)

            => AltitudeAccuracy1.CompareTo(AltitudeAccuracy2) < 0;

        #endregion

        #region Operator <= (AltitudeAccuracy1, AltitudeAccuracy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AltitudeAccuracy1">An AltitudeAccuracy.</param>
        /// <param name="AltitudeAccuracy2">Another AltitudeAccuracy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (AltitudeAccuracy AltitudeAccuracy1,
                                           AltitudeAccuracy AltitudeAccuracy2)

            => AltitudeAccuracy1.CompareTo(AltitudeAccuracy2) <= 0;

        #endregion

        #region Operator >  (AltitudeAccuracy1, AltitudeAccuracy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AltitudeAccuracy1">An AltitudeAccuracy.</param>
        /// <param name="AltitudeAccuracy2">Another AltitudeAccuracy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (AltitudeAccuracy AltitudeAccuracy1,
                                          AltitudeAccuracy AltitudeAccuracy2)

            => AltitudeAccuracy1.CompareTo(AltitudeAccuracy2) > 0;

        #endregion

        #region Operator >= (AltitudeAccuracy1, AltitudeAccuracy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AltitudeAccuracy1">An AltitudeAccuracy.</param>
        /// <param name="AltitudeAccuracy2">Another AltitudeAccuracy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (AltitudeAccuracy AltitudeAccuracy1,
                                           AltitudeAccuracy AltitudeAccuracy2)

            => AltitudeAccuracy1.CompareTo(AltitudeAccuracy2) >= 0;

        #endregion

        #endregion

        #region IComparable<AltitudeAccuracy> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two AltitudeAccuracies.
        /// </summary>
        /// <param name="Object">AltitudeAccuracy to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is AltitudeAccuracy AltitudeAccuracy
                   ? CompareTo(AltitudeAccuracy)
                   : throw new ArgumentException("The given object is not AltitudeAccuracy!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(AltitudeAccuracy)

        /// <summary>
        /// Compares two AltitudeAccuracies.
        /// </summary>
        /// <param name="AltitudeAccuracy">AltitudeAccuracy to compare with.</param>
        public Int32 CompareTo(AltitudeAccuracy AltitudeAccuracy)

            => String.Compare(InternalId,
                              AltitudeAccuracy.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<AltitudeAccuracy> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two AltitudeAccuracies for equality.
        /// </summary>
        /// <param name="Object">AltitudeAccuracy to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is AltitudeAccuracy AltitudeAccuracy &&
                   Equals(AltitudeAccuracy);

        #endregion

        #region Equals(AltitudeAccuracy)

        /// <summary>
        /// Compares two AltitudeAccuracies for equality.
        /// </summary>
        /// <param name="AltitudeAccuracy">AltitudeAccuracy to compare with.</param>
        public Boolean Equals(AltitudeAccuracy AltitudeAccuracy)

            => String.Equals(InternalId,
                             AltitudeAccuracy.InternalId,
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
