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
    /// Extension methods for EmissionClassificationEuroExtensions.
    /// </summary>
    public static class EmissionClassificationEuroExtensionExtensions
    {

        /// <summary>
        /// Indicates whether this EmissionClassificationEuroExtension is null or empty.
        /// </summary>
        /// <param name="EmissionClassificationEuroExtension">An EmissionClassificationEuroExtension.</param>
        public static Boolean IsNullOrEmpty(this EmissionClassificationEuroExtension? EmissionClassificationEuroExtension)
            => !EmissionClassificationEuroExtension.HasValue || EmissionClassificationEuroExtension.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this EmissionClassificationEuroExtension is null or empty.
        /// </summary>
        /// <param name="EmissionClassificationEuroExtension">An EmissionClassificationEuroExtension.</param>
        public static Boolean IsNotNullOrEmpty(this EmissionClassificationEuroExtension? EmissionClassificationEuroExtension)
            => EmissionClassificationEuroExtension.HasValue && EmissionClassificationEuroExtension.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An EmissionClassificationEuroExtension.
    /// </summary>
    public readonly struct EmissionClassificationEuroExtension : IId,
                                                                 IEquatable<EmissionClassificationEuroExtension>,
                                                                 IComparable<EmissionClassificationEuroExtension>
    {

        #region Data

        private readonly static Dictionary<String, EmissionClassificationEuroExtension>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                                          InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this EmissionClassificationEuroExtension is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this EmissionClassificationEuroExtension is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the EmissionClassificationEuroExtension.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered EmissionClassificationEuroExtensions.
        /// </summary>
        public static    IEnumerable<EmissionClassificationEuroExtension>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new EmissionClassificationEuroExtension based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an EmissionClassificationEuroExtension.</param>
        private EmissionClassificationEuroExtension(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static EmissionClassificationEuroExtension Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new EmissionClassificationEuroExtension(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an EmissionClassificationEuroExtension.
        /// </summary>
        /// <param name="Text">A text representation of an EmissionClassificationEuroExtension.</param>
        public static EmissionClassificationEuroExtension Parse(String Text)
        {

            if (TryParse(Text, out var emissionClassificationEuroExtension))
                return emissionClassificationEuroExtension;

            throw new ArgumentException($"Invalid text representation of an EmissionClassificationEuroExtension: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an EmissionClassificationEuroExtension.
        /// </summary>
        /// <param name="Text">A text representation of an EmissionClassificationEuroExtension.</param>
        public static EmissionClassificationEuroExtension? TryParse(String Text)
        {

            if (TryParse(Text, out var emissionClassificationEuroExtension))
                return emissionClassificationEuroExtension;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out EmissionClassificationEuroExtension)

        /// <summary>
        /// Try to parse the given text as an EmissionClassificationEuroExtension.
        /// </summary>
        /// <param name="Text">A text representation of an EmissionClassificationEuroExtension.</param>
        /// <param name="EmissionClassificationEuroExtension">The parsed EmissionClassificationEuroExtension.</param>
        public static Boolean TryParse(String Text, out EmissionClassificationEuroExtension EmissionClassificationEuroExtension)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out EmissionClassificationEuroExtension))
                    EmissionClassificationEuroExtension = Register(Text);

                return true;

            }

            EmissionClassificationEuroExtension = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this EmissionClassificationEuroExtension.
        /// </summary>
        public EmissionClassificationEuroExtension Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Euro Unknown
        /// </summary>
        public static EmissionClassificationEuroExtension  EuroUnknown     { get; }
            = Register("euroUnknown");

        /// <summary>
        /// Euro I
        /// </summary>
        public static EmissionClassificationEuroExtension  EuroI     { get; }
            = Register("euroI");

        /// <summary>
        /// Euro II
        /// </summary>
        public static EmissionClassificationEuroExtension  EuroII        { get; }
            = Register("xxeuroIIx");

        /// <summary>
        /// Euro III
        /// </summary>
        public static EmissionClassificationEuroExtension  EuroIII       { get; }
            = Register("euroIII");

        /// <summary>
        /// Euro 0
        /// </summary>
        public static EmissionClassificationEuroExtension  Euro0         { get; }
            = Register("euro0");

        /// <summary>
        /// Euro 4
        /// </summary>
        public static EmissionClassificationEuroExtension  Euro4         { get; }
            = Register("euro4");

        /// <summary>
        /// Euro 6d
        /// </summary>
        public static EmissionClassificationEuroExtension  Euro6d        { get; }
            = Register("euro6d");

        /// <summary>
        /// Euro 6d Temp
        /// </summary>
        public static EmissionClassificationEuroExtension  Euro6dTemp    { get; }
            = Register("euro6dTemp");

        /// <summary>
        /// Euro IV
        /// </summary>
        public static EmissionClassificationEuroExtension  EuroIV        { get; }
            = Register("euroIV");

        /// <summary>
        /// Euro 1
        /// </summary>
        public static EmissionClassificationEuroExtension  Euro1         { get; }
            = Register("euro1");

        /// <summary>
        /// Euro 2
        /// </summary>
        public static EmissionClassificationEuroExtension  Euro2         { get; }
            = Register("euro2");

        /// <summary>
        /// Euro 3
        /// </summary>
        public static EmissionClassificationEuroExtension  Euro3         { get; }
            = Register("euro3");

        #endregion


        #region Operator overloading

        #region Operator == (EmissionClassificationEuroExtension1, EmissionClassificationEuroExtension2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuroExtension1">An EmissionClassificationEuroExtension.</param>
        /// <param name="EmissionClassificationEuroExtension2">Another EmissionClassificationEuroExtension.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (EmissionClassificationEuroExtension EmissionClassificationEuroExtension1,
                                           EmissionClassificationEuroExtension EmissionClassificationEuroExtension2)

            => EmissionClassificationEuroExtension1.Equals(EmissionClassificationEuroExtension2);

        #endregion

        #region Operator != (EmissionClassificationEuroExtension1, EmissionClassificationEuroExtension2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuroExtension1">An EmissionClassificationEuroExtension.</param>
        /// <param name="EmissionClassificationEuroExtension2">Another EmissionClassificationEuroExtension.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (EmissionClassificationEuroExtension EmissionClassificationEuroExtension1,
                                           EmissionClassificationEuroExtension EmissionClassificationEuroExtension2)

            => !EmissionClassificationEuroExtension1.Equals(EmissionClassificationEuroExtension2);

        #endregion

        #region Operator <  (EmissionClassificationEuroExtension1, EmissionClassificationEuroExtension2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuroExtension1">An EmissionClassificationEuroExtension.</param>
        /// <param name="EmissionClassificationEuroExtension2">Another EmissionClassificationEuroExtension.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (EmissionClassificationEuroExtension EmissionClassificationEuroExtension1,
                                          EmissionClassificationEuroExtension EmissionClassificationEuroExtension2)

            => EmissionClassificationEuroExtension1.CompareTo(EmissionClassificationEuroExtension2) < 0;

        #endregion

        #region Operator <= (EmissionClassificationEuroExtension1, EmissionClassificationEuroExtension2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuroExtension1">An EmissionClassificationEuroExtension.</param>
        /// <param name="EmissionClassificationEuroExtension2">Another EmissionClassificationEuroExtension.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (EmissionClassificationEuroExtension EmissionClassificationEuroExtension1,
                                           EmissionClassificationEuroExtension EmissionClassificationEuroExtension2)

            => EmissionClassificationEuroExtension1.CompareTo(EmissionClassificationEuroExtension2) <= 0;

        #endregion

        #region Operator >  (EmissionClassificationEuroExtension1, EmissionClassificationEuroExtension2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuroExtension1">An EmissionClassificationEuroExtension.</param>
        /// <param name="EmissionClassificationEuroExtension2">Another EmissionClassificationEuroExtension.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (EmissionClassificationEuroExtension EmissionClassificationEuroExtension1,
                                          EmissionClassificationEuroExtension EmissionClassificationEuroExtension2)

            => EmissionClassificationEuroExtension1.CompareTo(EmissionClassificationEuroExtension2) > 0;

        #endregion

        #region Operator >= (EmissionClassificationEuroExtension1, EmissionClassificationEuroExtension2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EmissionClassificationEuroExtension1">An EmissionClassificationEuroExtension.</param>
        /// <param name="EmissionClassificationEuroExtension2">Another EmissionClassificationEuroExtension.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (EmissionClassificationEuroExtension EmissionClassificationEuroExtension1,
                                           EmissionClassificationEuroExtension EmissionClassificationEuroExtension2)

            => EmissionClassificationEuroExtension1.CompareTo(EmissionClassificationEuroExtension2) >= 0;

        #endregion

        #endregion

        #region IComparable<EmissionClassificationEuroExtension> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two EmissionClassificationEuroExtensions.
        /// </summary>
        /// <param name="Object">An EmissionClassificationEuroExtension to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is EmissionClassificationEuroExtension emissionClassificationEuroExtension
                   ? CompareTo(emissionClassificationEuroExtension)
                   : throw new ArgumentException("The given object is not an EmissionClassificationEuroExtension!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(EmissionClassificationEuroExtension)

        /// <summary>
        /// Compares two EmissionClassificationEuroExtensions.
        /// </summary>
        /// <param name="EmissionClassificationEuroExtension">An EmissionClassificationEuroExtension to compare with.</param>
        public Int32 CompareTo(EmissionClassificationEuroExtension EmissionClassificationEuroExtension)

            => String.Compare(InternalId,
                              EmissionClassificationEuroExtension.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<EmissionClassificationEuroExtension> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two EmissionClassificationEuroExtensions for equality.
        /// </summary>
        /// <param name="Object">An EmissionClassificationEuroExtension to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is EmissionClassificationEuroExtension emissionClassificationEuroExtension &&
                   Equals(emissionClassificationEuroExtension);

        #endregion

        #region Equals(EmissionClassificationEuroExtension)

        /// <summary>
        /// Compares two EmissionClassificationEuroExtensions for equality.
        /// </summary>
        /// <param name="EmissionClassificationEuroExtension">An EmissionClassificationEuroExtension to compare with.</param>
        public Boolean Equals(EmissionClassificationEuroExtension EmissionClassificationEuroExtension)

            => String.Equals(InternalId,
                             EmissionClassificationEuroExtension.InternalId,
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
