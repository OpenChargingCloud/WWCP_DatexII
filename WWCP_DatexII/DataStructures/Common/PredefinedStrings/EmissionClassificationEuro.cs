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
    /// Extension methods for EmissionClassificationEuros.
    /// </summary>
    public static class EmissionClassificationEuroExtensions
    {

        /// <summary>
        /// Indicates whether this EmissionClassificationEuro is null or empty.
        /// </summary>
        /// <param name="EmissionClassificationEuro">An EmissionClassificationEuro.</param>
        public static Boolean IsNullOrEmpty(this EmissionClassificationEuro? EmissionClassificationEuro)
            => !EmissionClassificationEuro.HasValue || EmissionClassificationEuro.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this EmissionClassificationEuro is null or empty.
        /// </summary>
        /// <param name="EmissionClassificationEuro">An EmissionClassificationEuro.</param>
        public static Boolean IsNotNullOrEmpty(this EmissionClassificationEuro? EmissionClassificationEuro)
            => EmissionClassificationEuro.HasValue && EmissionClassificationEuro.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An EmissionClassificationEuro.
    /// </summary>
    public readonly struct EmissionClassificationEuro : IId,
                                                        IEquatable<EmissionClassificationEuro>,
                                                        IComparable<EmissionClassificationEuro>
    {

        #region Data

        private readonly static Dictionary<String, EmissionClassificationEuro>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                                          InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this EmissionClassificationEuro is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this EmissionClassificationEuro is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the EmissionClassificationEuro.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered EmissionClassificationEuros.
        /// </summary>
        public static    IEnumerable<EmissionClassificationEuro>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new EmissionClassificationEuro based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an EmissionClassificationEuro.</param>
        private EmissionClassificationEuro(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static EmissionClassificationEuro Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new EmissionClassificationEuro(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an EmissionClassificationEuro.
        /// </summary>
        /// <param name="Text">A text representation of an EmissionClassificationEuro.</param>
        public static EmissionClassificationEuro Parse(String Text)
        {

            if (TryParse(Text, out var emissionClassificationEuro))
                return emissionClassificationEuro;

            throw new ArgumentException($"Invalid text representation of an EmissionClassificationEuro: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an EmissionClassificationEuro.
        /// </summary>
        /// <param name="Text">A text representation of an EmissionClassificationEuro.</param>
        public static EmissionClassificationEuro? TryParse(String Text)
        {

            if (TryParse(Text, out var emissionClassificationEuro))
                return emissionClassificationEuro;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out EmissionClassificationEuro)

        /// <summary>
        /// Try to parse the given text as an EmissionClassificationEuro.
        /// </summary>
        /// <param name="Text">A text representation of an EmissionClassificationEuro.</param>
        /// <param name="EmissionClassificationEuro">The parsed EmissionClassificationEuro.</param>
        public static Boolean TryParse(String Text, out EmissionClassificationEuro EmissionClassificationEuro)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out EmissionClassificationEuro))
                    EmissionClassificationEuro = Register(Text);

                return true;

            }

            EmissionClassificationEuro = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this EmissionClassificationEuro.
        /// </summary>
        public EmissionClassificationEuro Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Euro 5
        /// </summary>
        public static EmissionClassificationEuro  Euro5     { get; }
            = Register("euro5");

        /// <summary>
        /// Euro 5a
        /// </summary>
        public static EmissionClassificationEuro  Euro5a    { get; }
            = Register("euro5a");

        /// <summary>
        /// Euro 5b
        /// </summary>
        public static EmissionClassificationEuro  Euro5b    { get; }
            = Register("euro5b");

        /// <summary>
        /// Euro 6
        /// </summary>
        public static EmissionClassificationEuro  Euro6     { get; }
            = Register("euro6");

        /// <summary>
        /// Euro 6a
        /// </summary>
        public static EmissionClassificationEuro  Euro6a    { get; }
            = Register("euro6a");

        /// <summary>
        /// Euro 6b
        /// </summary>
        public static EmissionClassificationEuro  Euro6b    { get; }
            = Register("euro6b");

        /// <summary>
        /// Euro 6c
        /// </summary>
        public static EmissionClassificationEuro  Euro6c    { get; }
            = Register("euro6c");

        /// <summary>
        /// Euro V
        /// </summary>
        public static EmissionClassificationEuro  EuroV     { get; }
            = Register("euroV");

        /// <summary>
        /// Euro VI
        /// </summary>
        public static EmissionClassificationEuro  EuroVI    { get; }
            = Register("euroVI");

        /// <summary>
        /// Any other level
        /// </summary>
        public static EmissionClassificationEuro  Other     { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (EmissionClassificationEuro1, EmissionClassificationEuro2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuro1">An EmissionClassificationEuro.</param>
        /// <param name="EmissionClassificationEuro2">Another EmissionClassificationEuro.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (EmissionClassificationEuro EmissionClassificationEuro1,
                                           EmissionClassificationEuro EmissionClassificationEuro2)

            => EmissionClassificationEuro1.Equals(EmissionClassificationEuro2);

        #endregion

        #region Operator != (EmissionClassificationEuro1, EmissionClassificationEuro2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuro1">An EmissionClassificationEuro.</param>
        /// <param name="EmissionClassificationEuro2">Another EmissionClassificationEuro.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (EmissionClassificationEuro EmissionClassificationEuro1,
                                           EmissionClassificationEuro EmissionClassificationEuro2)

            => !EmissionClassificationEuro1.Equals(EmissionClassificationEuro2);

        #endregion

        #region Operator <  (EmissionClassificationEuro1, EmissionClassificationEuro2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuro1">An EmissionClassificationEuro.</param>
        /// <param name="EmissionClassificationEuro2">Another EmissionClassificationEuro.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (EmissionClassificationEuro EmissionClassificationEuro1,
                                          EmissionClassificationEuro EmissionClassificationEuro2)

            => EmissionClassificationEuro1.CompareTo(EmissionClassificationEuro2) < 0;

        #endregion

        #region Operator <= (EmissionClassificationEuro1, EmissionClassificationEuro2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuro1">An EmissionClassificationEuro.</param>
        /// <param name="EmissionClassificationEuro2">Another EmissionClassificationEuro.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (EmissionClassificationEuro EmissionClassificationEuro1,
                                           EmissionClassificationEuro EmissionClassificationEuro2)

            => EmissionClassificationEuro1.CompareTo(EmissionClassificationEuro2) <= 0;

        #endregion

        #region Operator >  (EmissionClassificationEuro1, EmissionClassificationEuro2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuro1">An EmissionClassificationEuro.</param>
        /// <param name="EmissionClassificationEuro2">Another EmissionClassificationEuro.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (EmissionClassificationEuro EmissionClassificationEuro1,
                                          EmissionClassificationEuro EmissionClassificationEuro2)

            => EmissionClassificationEuro1.CompareTo(EmissionClassificationEuro2) > 0;

        #endregion

        #region Operator >= (EmissionClassificationEuro1, EmissionClassificationEuro2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuro1">An EmissionClassificationEuro.</param>
        /// <param name="EmissionClassificationEuro2">Another EmissionClassificationEuro.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (EmissionClassificationEuro EmissionClassificationEuro1,
                                           EmissionClassificationEuro EmissionClassificationEuro2)

            => EmissionClassificationEuro1.CompareTo(EmissionClassificationEuro2) >= 0;

        #endregion

        #endregion

        #region IComparable<EmissionClassificationEuro> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two EmissionClassificationEuros.
        /// </summary>
        /// <param name="Object">An EmissionClassificationEuro to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is EmissionClassificationEuro emissionClassificationEuro
                   ? CompareTo(emissionClassificationEuro)
                   : throw new ArgumentException("The given object is not an EmissionClassificationEuro!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(EmissionClassificationEuro)

        /// <summary>
        /// Compares two EmissionClassificationEuros.
        /// </summary>
        /// <param name="EmissionClassificationEuro">An EmissionClassificationEuro to compare with.</param>
        public Int32 CompareTo(EmissionClassificationEuro EmissionClassificationEuro)

            => String.Compare(InternalId,
                              EmissionClassificationEuro.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<EmissionClassificationEuro> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two EmissionClassificationEuros for equality.
        /// </summary>
        /// <param name="Object">An EmissionClassificationEuro to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is EmissionClassificationEuro emissionClassificationEuro &&
                   Equals(emissionClassificationEuro);

        #endregion

        #region Equals(EmissionClassificationEuro)

        /// <summary>
        /// Compares two EmissionClassificationEuros for equality.
        /// </summary>
        /// <param name="EmissionClassificationEuro">An EmissionClassificationEuro to compare with.</param>
        public Boolean Equals(EmissionClassificationEuro EmissionClassificationEuro)

            => String.Equals(InternalId,
                             EmissionClassificationEuro.InternalId,
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
