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
    /// Extension methods for NutsCodeTypes.
    /// </summary>
    public static class NutsCodeTypeExtensions
    {

        /// <summary>
        /// Indicates whether this NutsCodeType is null or empty.
        /// </summary>
        /// <param name="NutsCodeType">A NutsCodeType.</param>
        public static Boolean IsNullOrEmpty(this NutsCodeType? NutsCodeType)
            => !NutsCodeType.HasValue || NutsCodeType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this NutsCodeType is null or empty.
        /// </summary>
        /// <param name="NutsCodeType">A NutsCodeType.</param>
        public static Boolean IsNotNullOrEmpty(this NutsCodeType? NutsCodeType)
            => NutsCodeType.HasValue && NutsCodeType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A NutsCodeType.
    /// </summary>
    public readonly struct NutsCodeType : IId,
                                          IEquatable<NutsCodeType>,
                                          IComparable<NutsCodeType>
    {

        #region Data

        private readonly static Dictionary<String, NutsCodeType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this NutsCodeType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this NutsCodeType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the NutsCodeType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<NutsCodeType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new NutsCodeType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a NutsCodeType.</param>
        private NutsCodeType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static NutsCodeType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new NutsCodeType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a NutsCodeType.
        /// </summary>
        /// <param name="Text">A text representation of a NutsCodeType.</param>
        public static NutsCodeType Parse(String Text)
        {

            if (TryParse(Text, out var nutsCodeType))
                return nutsCodeType;

            throw new ArgumentException($"Invalid text representation of a NutsCodeType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a NutsCodeType.
        /// </summary>
        /// <param name="Text">A text representation of a NutsCodeType.</param>
        public static NutsCodeType? TryParse(String Text)
        {

            if (TryParse(Text, out var nutsCodeType))
                return nutsCodeType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out NutsCodeType)

        /// <summary>
        /// Try to parse the given text as NutsCodeType.
        /// </summary>
        /// <param name="Text">A text representation of a NutsCodeType.</param>
        /// <param name="NutsCodeType">The parsed NutsCodeType.</param>
        public static Boolean TryParse(String Text, out NutsCodeType NutsCodeType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out NutsCodeType))
                    NutsCodeType = Register(Text);

                return true;

            }

            NutsCodeType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this NutsCodeType.
        /// </summary>
        public NutsCodeType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// NUTS 1 code.
        /// </summary>
        public static NutsCodeType  Nuts1Code    { get; }
            = Register("nuts1Code");

        /// <summary>
        /// NUTS 2 code.
        /// </summary>
        public static NutsCodeType  Nuts2Code    { get; }
            = Register("nuts2Code");

        /// <summary>
        /// NUTS 3 code.
        /// </summary>
        public static NutsCodeType  Nuts3Code    { get; }
            = Register("nuts3Code");

        /// <summary>
        /// LAU 1 code.
        /// </summary>
        public static NutsCodeType  Lau1Code     { get; }
            = Register("lau1Code");

        /// <summary>
        /// LAU 2 code.
        /// </summary>
        public static NutsCodeType  Lau2Code     { get; }
            = Register("lau2Code");

        #endregion


        #region Operator overloading

        #region Operator == (NutsCodeType1, NutsCodeType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NutsCodeType1">A NutsCodeType.</param>
        /// <param name="NutsCodeType2">Another NutsCodeType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (NutsCodeType NutsCodeType1,
                                           NutsCodeType NutsCodeType2)

            => NutsCodeType1.Equals(NutsCodeType2);

        #endregion

        #region Operator != (NutsCodeType1, NutsCodeType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NutsCodeType1">A NutsCodeType.</param>
        /// <param name="NutsCodeType2">Another NutsCodeType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (NutsCodeType NutsCodeType1,
                                           NutsCodeType NutsCodeType2)

            => !NutsCodeType1.Equals(NutsCodeType2);

        #endregion

        #region Operator <  (NutsCodeType1, NutsCodeType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NutsCodeType1">A NutsCodeType.</param>
        /// <param name="NutsCodeType2">Another NutsCodeType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (NutsCodeType NutsCodeType1,
                                          NutsCodeType NutsCodeType2)

            => NutsCodeType1.CompareTo(NutsCodeType2) < 0;

        #endregion

        #region Operator <= (NutsCodeType1, NutsCodeType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NutsCodeType1">A NutsCodeType.</param>
        /// <param name="NutsCodeType2">Another NutsCodeType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (NutsCodeType NutsCodeType1,
                                           NutsCodeType NutsCodeType2)

            => NutsCodeType1.CompareTo(NutsCodeType2) <= 0;

        #endregion

        #region Operator >  (NutsCodeType1, NutsCodeType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NutsCodeType1">A NutsCodeType.</param>
        /// <param name="NutsCodeType2">Another NutsCodeType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (NutsCodeType NutsCodeType1,
                                          NutsCodeType NutsCodeType2)

            => NutsCodeType1.CompareTo(NutsCodeType2) > 0;

        #endregion

        #region Operator >= (NutsCodeType1, NutsCodeType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NutsCodeType1">A NutsCodeType.</param>
        /// <param name="NutsCodeType2">Another NutsCodeType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (NutsCodeType NutsCodeType1,
                                           NutsCodeType NutsCodeType2)

            => NutsCodeType1.CompareTo(NutsCodeType2) >= 0;

        #endregion

        #endregion

        #region IComparable<NutsCodeType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two NutsCodeTypes.
        /// </summary>
        /// <param name="Object">NutsCodeType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is NutsCodeType NutsCodeType
                   ? CompareTo(NutsCodeType)
                   : throw new ArgumentException("The given object is not NutsCodeType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(NutsCodeType)

        /// <summary>
        /// Compares two NutsCodeTypes.
        /// </summary>
        /// <param name="NutsCodeType">NutsCodeType to compare with.</param>
        public Int32 CompareTo(NutsCodeType NutsCodeType)

            => String.Compare(InternalId,
                              NutsCodeType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<NutsCodeType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two NutsCodeTypes for equality.
        /// </summary>
        /// <param name="Object">NutsCodeType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is NutsCodeType NutsCodeType &&
                   Equals(NutsCodeType);

        #endregion

        #region Equals(NutsCodeType)

        /// <summary>
        /// Compares two NutsCodeTypes for equality.
        /// </summary>
        /// <param name="NutsCodeType">NutsCodeType to compare with.</param>
        public Boolean Equals(NutsCodeType NutsCodeType)

            => String.Equals(InternalId,
                             NutsCodeType.InternalId,
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
