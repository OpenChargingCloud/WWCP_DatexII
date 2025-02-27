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

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Extension methods for ElectricEnergySourceType.
    /// </summary>
    public static class ElectricEnergySourceTypeExtensions
    {

        /// <summary>
        /// Indicates whether this ElectricEnergySourceType is null or empty.
        /// </summary>
        /// <param name="ElectricEnergySourceType">An ElectricEnergySourceType.</param>
        public static Boolean IsNullOrEmpty(this ElectricEnergySourceType? ElectricEnergySourceType)
            => !ElectricEnergySourceType.HasValue || ElectricEnergySourceType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this ElectricEnergySourceType is null or empty.
        /// </summary>
        /// <param name="ElectricEnergySourceType">An ElectricEnergySourceType.</param>
        public static Boolean IsNotNullOrEmpty(this ElectricEnergySourceType? ElectricEnergySourceType)
            => ElectricEnergySourceType.HasValue && ElectricEnergySourceType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An ElectricEnergySourceType.
    /// </summary>
    public readonly struct ElectricEnergySourceType : IId,
                                                      IEquatable<ElectricEnergySourceType>,
                                                      IComparable<ElectricEnergySourceType>
    {

        #region Data

        private readonly static Dictionary<String, ElectricEnergySourceType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this ElectricEnergySourceType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this ElectricEnergySourceType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the ElectricEnergySourceType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered ElectricEnergySourceTypes.
        /// </summary>
        public static    IEnumerable<ElectricEnergySourceType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ElectricEnergySourceType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an ElectricEnergySourceType.</param>
        private ElectricEnergySourceType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ElectricEnergySourceType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ElectricEnergySourceType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an ElectricEnergySourceType.
        /// </summary>
        /// <param name="Text">A text representation of an ElectricEnergySourceType.</param>
        public static ElectricEnergySourceType Parse(String Text)
        {

            if (TryParse(Text, out var electricEnergySourceType))
                return electricEnergySourceType;

            throw new ArgumentException($"Invalid text representation of an ElectricEnergySourceType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an ElectricEnergySourceType.
        /// </summary>
        /// <param name="Text">A text representation of an ElectricEnergySourceType.</param>
        public static ElectricEnergySourceType? TryParse(String Text)
        {

            if (TryParse(Text, out var electricEnergySourceType))
                return electricEnergySourceType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ElectricEnergySourceType)

        /// <summary>
        /// Try to parse the given text as an ElectricEnergySourceType.
        /// </summary>
        /// <param name="Text">A text representation of an ElectricEnergySourceType.</param>
        /// <param name="ElectricEnergySourceType">The parsed ElectricEnergySourceType.</param>
        public static Boolean TryParse(String Text, out ElectricEnergySourceType ElectricEnergySourceType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ElectricEnergySourceType))
                    ElectricEnergySourceType = Register(Text);

                return true;

            }

            ElectricEnergySourceType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this ElectricEnergySourceType.
        /// </summary>
        public ElectricEnergySourceType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Biogas energy source.
        /// </summary>
        public static ElectricEnergySourceType  Biogas           { get; }
            = Register("biogas");

        /// <summary>
        /// Coal energy source.
        /// </summary>
        public static ElectricEnergySourceType  Coal             { get; }
            = Register("coal");

        /// <summary>
        /// Gas energy source.
        /// </summary>
        public static ElectricEnergySourceType  Gas              { get; }
            = Register("gas");

        /// <summary>
        /// Nuclear power sources.
        /// </summary>
        public static ElectricEnergySourceType  Nuclear          { get; }
            = Register("nuclear");

        /// <summary>
        /// Solar energy source.
        /// </summary>
        public static ElectricEnergySourceType  Solar            { get; }
            = Register("solar");

        /// <summary>
        /// Water energy source.
        /// </summary>
        public static ElectricEnergySourceType  Water            { get; }
            = Register("water");

        /// <summary>
        /// Wind energy source.
        /// </summary>
        public static ElectricEnergySourceType  Wind             { get; }
            = Register("chademo");

        /// <summary>
        /// All kinds of green energy sources.
        /// </summary>
        public static ElectricEnergySourceType  GeneralGreen     { get; }
            = Register("generalGreen");

        /// <summary>
        /// All kinds of fossil power sources.
        /// </summary>
        public static ElectricEnergySourceType  GeneralFossil    { get; }
            = Register("generalFossil");

        /// <summary>
        /// Any other energy source.
        /// </summary>
        public static ElectricEnergySourceType  Other            { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (ElectricEnergySourceType1, ElectricEnergySourceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ElectricEnergySourceType1">An ElectricEnergySourceType.</param>
        /// <param name="ElectricEnergySourceType2">Another ElectricEnergySourceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ElectricEnergySourceType ElectricEnergySourceType1,
                                           ElectricEnergySourceType ElectricEnergySourceType2)

            => ElectricEnergySourceType1.Equals(ElectricEnergySourceType2);

        #endregion

        #region Operator != (ElectricEnergySourceType1, ElectricEnergySourceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ElectricEnergySourceType1">An ElectricEnergySourceType.</param>
        /// <param name="ElectricEnergySourceType2">Another ElectricEnergySourceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ElectricEnergySourceType ElectricEnergySourceType1,
                                           ElectricEnergySourceType ElectricEnergySourceType2)

            => !ElectricEnergySourceType1.Equals(ElectricEnergySourceType2);

        #endregion

        #region Operator <  (ElectricEnergySourceType1, ElectricEnergySourceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ElectricEnergySourceType1">An ElectricEnergySourceType.</param>
        /// <param name="ElectricEnergySourceType2">Another ElectricEnergySourceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ElectricEnergySourceType ElectricEnergySourceType1,
                                          ElectricEnergySourceType ElectricEnergySourceType2)

            => ElectricEnergySourceType1.CompareTo(ElectricEnergySourceType2) < 0;

        #endregion

        #region Operator <= (ElectricEnergySourceType1, ElectricEnergySourceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ElectricEnergySourceType1">An ElectricEnergySourceType.</param>
        /// <param name="ElectricEnergySourceType2">Another ElectricEnergySourceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ElectricEnergySourceType ElectricEnergySourceType1,
                                           ElectricEnergySourceType ElectricEnergySourceType2)

            => ElectricEnergySourceType1.CompareTo(ElectricEnergySourceType2) <= 0;

        #endregion

        #region Operator >  (ElectricEnergySourceType1, ElectricEnergySourceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ElectricEnergySourceType1">An ElectricEnergySourceType.</param>
        /// <param name="ElectricEnergySourceType2">Another ElectricEnergySourceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ElectricEnergySourceType ElectricEnergySourceType1,
                                          ElectricEnergySourceType ElectricEnergySourceType2)

            => ElectricEnergySourceType1.CompareTo(ElectricEnergySourceType2) > 0;

        #endregion

        #region Operator >= (ElectricEnergySourceType1, ElectricEnergySourceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ElectricEnergySourceType1">An ElectricEnergySourceType.</param>
        /// <param name="ElectricEnergySourceType2">Another ElectricEnergySourceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ElectricEnergySourceType ElectricEnergySourceType1,
                                           ElectricEnergySourceType ElectricEnergySourceType2)

            => ElectricEnergySourceType1.CompareTo(ElectricEnergySourceType2) >= 0;

        #endregion

        #endregion

        #region IComparable<ElectricEnergySourceType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two ElectricEnergySourceTypes.
        /// </summary>
        /// <param name="Object">An ElectricEnergySourceType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ElectricEnergySourceType electricEnergySourceType
                   ? CompareTo(electricEnergySourceType)
                   : throw new ArgumentException("The given object is not an ElectricEnergySourceType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ElectricEnergySourceType)

        /// <summary>
        /// Compares two ElectricEnergySourceTypes.
        /// </summary>
        /// <param name="ElectricEnergySourceType">An ElectricEnergySourceType to compare with.</param>
        public Int32 CompareTo(ElectricEnergySourceType ElectricEnergySourceType)

            => String.Compare(InternalId,
                              ElectricEnergySourceType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<ElectricEnergySourceType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two ElectricEnergySourceTypes for equality.
        /// </summary>
        /// <param name="Object">An ElectricEnergySourceType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ElectricEnergySourceType electricEnergySourceType &&
                   Equals(electricEnergySourceType);

        #endregion

        #region Equals(ElectricEnergySourceType)

        /// <summary>
        /// Compares two ElectricEnergySourceTypes for equality.
        /// </summary>
        /// <param name="ElectricEnergySourceType">An ElectricEnergySourceType to compare with.</param>
        public Boolean Equals(ElectricEnergySourceType ElectricEnergySourceType)

            => String.Equals(InternalId,
                             ElectricEnergySourceType.InternalId,
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
