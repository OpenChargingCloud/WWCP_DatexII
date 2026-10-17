/*
 * Copyright (c) 2014-2026 GraphDefined GmbH <achim.friedland@graphdefined.com>
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
    /// Extension methods for TypesOfIdentifier.
    /// </summary>
    public static class TypeOfIdentifierExtensions
    {

        /// <summary>
        /// Indicates whether this TypeOfIdentifier is null or empty.
        /// </summary>
        /// <param name="TypeOfIdentifier">A TypeOfIdentifier.</param>
        public static Boolean IsNullOrEmpty(this TypeOfIdentifier? TypeOfIdentifier)
            => !TypeOfIdentifier.HasValue || TypeOfIdentifier.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this TypeOfIdentifier is null or empty.
        /// </summary>
        /// <param name="TypeOfIdentifier">A TypeOfIdentifier.</param>
        public static Boolean IsNotNullOrEmpty(this TypeOfIdentifier? TypeOfIdentifier)
            => TypeOfIdentifier.HasValue && TypeOfIdentifier.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// The type of an external identifier. DATEX II 3.7 knows only evseId, as an extended value; the Mobilithek profile adds the others.
    /// </summary>
    public readonly struct TypeOfIdentifier : IId,
                                        IEquatable<TypeOfIdentifier>,
                                        IComparable<TypeOfIdentifier>
    {

        #region Data

        private readonly static Dictionary<String, TypeOfIdentifier>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this TypeOfIdentifier is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this TypeOfIdentifier is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the TypeOfIdentifier.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<TypeOfIdentifier>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new TypeOfIdentifier based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a TypeOfIdentifier.</param>
        private TypeOfIdentifier(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static TypeOfIdentifier Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new TypeOfIdentifier(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a TypeOfIdentifier.
        /// </summary>
        /// <param name="Text">A text representation of a TypeOfIdentifier.</param>
        public static TypeOfIdentifier Parse(String Text)
        {

            if (TryParse(Text, out var typeOfIdentifier))
                return typeOfIdentifier;

            throw new ArgumentException($"Invalid text representation of a TypeOfIdentifier: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a TypeOfIdentifier.
        /// </summary>
        /// <param name="Text">A text representation of a TypeOfIdentifier.</param>
        public static TypeOfIdentifier? TryParse(String Text)
        {

            if (TryParse(Text, out var typeOfIdentifier))
                return typeOfIdentifier;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out TypeOfIdentifier)

        /// <summary>
        /// Try to parse the given text as TypeOfIdentifier.
        /// </summary>
        /// <param name="Text">A text representation of a TypeOfIdentifier.</param>
        /// <param name="TypeOfIdentifier">The parsed TypeOfIdentifier.</param>
        public static Boolean TryParse(String Text, out TypeOfIdentifier TypeOfIdentifier)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out TypeOfIdentifier))
                    TypeOfIdentifier = Register(Text);

                return true;

            }

            TypeOfIdentifier = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this TypeOfIdentifier.
        /// </summary>
        public TypeOfIdentifier Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// An EVSE identification according to ISO 15118-2, e.g. DE*ABC*E12345*1.
        /// </summary>
        public static TypeOfIdentifier  EVSEId    { get; }
            = Register("evseId");

        /// <summary>
        /// The identification of a charging station at the Bundesnetzagentur (Mobilithek profile).
        /// </summary>
        public static TypeOfIdentifier  StationIdBNetzA    { get; }
            = Register("stationIdBNetzA");

        /// <summary>
        /// The identification of an operator at the Bundesnetzagentur (Mobilithek profile).
        /// </summary>
        public static TypeOfIdentifier  OperatorIdBNetzA    { get; }
            = Register("operatorIdBNetzA");

        /// <summary>
        /// An operator identification, e.g. DE*ABC (Mobilithek profile).
        /// </summary>
        public static TypeOfIdentifier  OperatorId    { get; }
            = Register("operatorId");

        /// <summary>
        /// A mobility service provider identification, e.g. DE-ABC (Mobilithek profile).
        /// </summary>
        public static TypeOfIdentifier  ProviderId    { get; }
            = Register("providerId");

        /// <summary>
        /// The identification of a connector within its charging point, e.g. 1 (as in OCPI and POI3). No type of the
        /// Mobilithek profile, so it is an otherTypeOfIdentifier there as well.
        /// </summary>
        public static TypeOfIdentifier  ConnectorId    { get; }
            = Register("connectorId");

        #endregion


        #region Operator overloading

        #region Operator == (TypeOfIdentifier1, TypeOfIdentifier2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="TypeOfIdentifier1">A TypeOfIdentifier.</param>
        /// <param name="TypeOfIdentifier2">Another TypeOfIdentifier.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (TypeOfIdentifier TypeOfIdentifier1,
                                           TypeOfIdentifier TypeOfIdentifier2)

            => TypeOfIdentifier1.Equals(TypeOfIdentifier2);

        #endregion

        #region Operator != (TypeOfIdentifier1, TypeOfIdentifier2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="TypeOfIdentifier1">A TypeOfIdentifier.</param>
        /// <param name="TypeOfIdentifier2">Another TypeOfIdentifier.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (TypeOfIdentifier TypeOfIdentifier1,
                                           TypeOfIdentifier TypeOfIdentifier2)

            => !TypeOfIdentifier1.Equals(TypeOfIdentifier2);

        #endregion

        #region Operator <  (TypeOfIdentifier1, TypeOfIdentifier2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="TypeOfIdentifier1">A TypeOfIdentifier.</param>
        /// <param name="TypeOfIdentifier2">Another TypeOfIdentifier.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (TypeOfIdentifier TypeOfIdentifier1,
                                          TypeOfIdentifier TypeOfIdentifier2)

            => TypeOfIdentifier1.CompareTo(TypeOfIdentifier2) < 0;

        #endregion

        #region Operator <= (TypeOfIdentifier1, TypeOfIdentifier2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="TypeOfIdentifier1">A TypeOfIdentifier.</param>
        /// <param name="TypeOfIdentifier2">Another TypeOfIdentifier.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (TypeOfIdentifier TypeOfIdentifier1,
                                           TypeOfIdentifier TypeOfIdentifier2)

            => TypeOfIdentifier1.CompareTo(TypeOfIdentifier2) <= 0;

        #endregion

        #region Operator >  (TypeOfIdentifier1, TypeOfIdentifier2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="TypeOfIdentifier1">A TypeOfIdentifier.</param>
        /// <param name="TypeOfIdentifier2">Another TypeOfIdentifier.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (TypeOfIdentifier TypeOfIdentifier1,
                                          TypeOfIdentifier TypeOfIdentifier2)

            => TypeOfIdentifier1.CompareTo(TypeOfIdentifier2) > 0;

        #endregion

        #region Operator >= (TypeOfIdentifier1, TypeOfIdentifier2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="TypeOfIdentifier1">A TypeOfIdentifier.</param>
        /// <param name="TypeOfIdentifier2">Another TypeOfIdentifier.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (TypeOfIdentifier TypeOfIdentifier1,
                                           TypeOfIdentifier TypeOfIdentifier2)

            => TypeOfIdentifier1.CompareTo(TypeOfIdentifier2) >= 0;

        #endregion

        #endregion

        #region IComparable<TypeOfIdentifier> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two TypesOfIdentifier.
        /// </summary>
        /// <param name="Object">TypeOfIdentifier to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is TypeOfIdentifier TypeOfIdentifier
                   ? CompareTo(TypeOfIdentifier)
                   : throw new ArgumentException("The given object is not TypeOfIdentifier!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(TypeOfIdentifier)

        /// <summary>
        /// Compares two TypesOfIdentifier.
        /// </summary>
        /// <param name="TypeOfIdentifier">TypeOfIdentifier to compare with.</param>
        public Int32 CompareTo(TypeOfIdentifier TypeOfIdentifier)

            => String.Compare(InternalId,
                              TypeOfIdentifier.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<TypeOfIdentifier> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two TypesOfIdentifier for equality.
        /// </summary>
        /// <param name="Object">TypeOfIdentifier to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is TypeOfIdentifier TypeOfIdentifier &&
                   Equals(TypeOfIdentifier);

        #endregion

        #region Equals(TypeOfIdentifier)

        /// <summary>
        /// Compares two TypesOfIdentifier for equality.
        /// </summary>
        /// <param name="TypeOfIdentifier">TypeOfIdentifier to compare with.</param>
        public Boolean Equals(TypeOfIdentifier TypeOfIdentifier)

            => String.Equals(InternalId,
                             TypeOfIdentifier.InternalId,
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
