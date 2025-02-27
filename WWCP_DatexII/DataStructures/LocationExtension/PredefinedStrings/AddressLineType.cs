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

namespace cloud.charging.open.protocols.DatexII.v3.LocationExtension
{

    /// <summary>
    /// Extension methods for AddressLineTypes.
    /// </summary>
    public static class AddressLineTypeExtensions
    {

        /// <summary>
        /// Indicates whether this AddressLineType is null or empty.
        /// </summary>
        /// <param name="AddressLineType">An AddressLineType.</param>
        public static Boolean IsNullOrEmpty(this AddressLineType? AddressLineType)
            => !AddressLineType.HasValue || AddressLineType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this AddressLineType is null or empty.
        /// </summary>
        /// <param name="AddressLineType">An AddressLineType.</param>
        public static Boolean IsNotNullOrEmpty(this AddressLineType? AddressLineType)
            => AddressLineType.HasValue && AddressLineType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An AddressLineType.
    /// </summary>
    public readonly struct AddressLineType : IId,
                                             IEquatable<AddressLineType>,
                                             IComparable<AddressLineType>
    {

        #region Data

        private readonly static Dictionary<String, AddressLineType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this AddressLineType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this AddressLineType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the AddressLineType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<AddressLineType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new AddressLineType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an AddressLineType.</param>
        private AddressLineType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static AddressLineType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new AddressLineType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an AddressLineType.
        /// </summary>
        /// <param name="Text">A text representation of an AddressLineType.</param>
        public static AddressLineType Parse(String Text)
        {

            if (TryParse(Text, out var addressLineType))
                return addressLineType;

            throw new ArgumentException($"Invalid text representation of an AddressLineType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an AddressLineType.
        /// </summary>
        /// <param name="Text">A text representation of an AddressLineType.</param>
        public static AddressLineType? TryParse(String Text)
        {

            if (TryParse(Text, out var addressLineType))
                return addressLineType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out AddressLineType)

        /// <summary>
        /// Try to parse the given text as AddressLineType.
        /// </summary>
        /// <param name="Text">A text representation of an AddressLineType.</param>
        /// <param name="AddressLineType">The parsed AddressLineType.</param>
        public static Boolean TryParse(String Text, out AddressLineType AddressLineType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out AddressLineType))
                    AddressLineType = Register(Text);

                return true;

            }

            AddressLineType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this AddressLineType.
        /// </summary>
        public AddressLineType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Element indicating a discrete element of a building forming the address.
        /// </summary>
        public static AddressLineType  Apartment            { get; }
            = Register("apartment");

        /// <summary>
        /// Element identifying the number or name and type of the edifice or construction relevant for the address.
        /// </summary>
        public static AddressLineType  Building             { get; }
            = Register("building");

        /// <summary>
        /// A postal delivery location identifier, not necessarily a physical location.
        /// </summary>
        public static AddressLineType  PoBox                { get; }
            = Register("poBox");

        /// <summary>
        /// An element representing a section of a building or organisation.
        /// </summary>
        public static AddressLineType  Unit                 { get; }
            = Register("unit");

        /// <summary>
        /// Element indicating the name of the area within or adjacent to the town in which delivery address is.
        /// </summary>
        public static AddressLineType  Region               { get; }
            = Register("region");

        /// <summary>
        /// Element indicating the name of the populated place in which a delivery point is located, or near to or via which the delivery point is accessed.
        /// </summary>
        public static AddressLineType  Town                 { get; }
            = Register("town");

        /// <summary>
        /// Element specifying the geographic or administrative area of the country for the address.
        /// </summary>
        public static AddressLineType  DistrictTerritory    { get; }
            = Register("districtTerritory");

        /// <summary>
        /// Element indicating the floor or level on which a delivery point is located in a multi-storey building.
        /// </summary>
        public static AddressLineType  Floor                { get; }
            = Register("floor");

        /// <summary>
        /// Element indicating road or street identifier or name.
        /// </summary>
        public static AddressLineType  Street               { get; }
            = Register("street");

        /// <summary>
        /// Element indicating a house number.
        /// </summary>
        public static AddressLineType  HouseNumber          { get; }
            = Register("houseNumber");

        /// <summary>
        /// A non-predefined text line for general purpose.
        /// </summary>
        public static AddressLineType  GeneralTextLine      { get; }
            = Register("generalTextLine");

        #endregion


        #region Operator overloading

        #region Operator == (AddressLineType1, AddressLineType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AddressLineType1">An AddressLineType.</param>
        /// <param name="AddressLineType2">Another AddressLineType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (AddressLineType AddressLineType1,
                                           AddressLineType AddressLineType2)

            => AddressLineType1.Equals(AddressLineType2);

        #endregion

        #region Operator != (AddressLineType1, AddressLineType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AddressLineType1">An AddressLineType.</param>
        /// <param name="AddressLineType2">Another AddressLineType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (AddressLineType AddressLineType1,
                                           AddressLineType AddressLineType2)

            => !AddressLineType1.Equals(AddressLineType2);

        #endregion

        #region Operator <  (AddressLineType1, AddressLineType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AddressLineType1">An AddressLineType.</param>
        /// <param name="AddressLineType2">Another AddressLineType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (AddressLineType AddressLineType1,
                                          AddressLineType AddressLineType2)

            => AddressLineType1.CompareTo(AddressLineType2) < 0;

        #endregion

        #region Operator <= (AddressLineType1, AddressLineType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AddressLineType1">An AddressLineType.</param>
        /// <param name="AddressLineType2">Another AddressLineType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (AddressLineType AddressLineType1,
                                           AddressLineType AddressLineType2)

            => AddressLineType1.CompareTo(AddressLineType2) <= 0;

        #endregion

        #region Operator >  (AddressLineType1, AddressLineType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AddressLineType1">An AddressLineType.</param>
        /// <param name="AddressLineType2">Another AddressLineType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (AddressLineType AddressLineType1,
                                          AddressLineType AddressLineType2)

            => AddressLineType1.CompareTo(AddressLineType2) > 0;

        #endregion

        #region Operator >= (AddressLineType1, AddressLineType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AddressLineType1">An AddressLineType.</param>
        /// <param name="AddressLineType2">Another AddressLineType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (AddressLineType AddressLineType1,
                                           AddressLineType AddressLineType2)

            => AddressLineType1.CompareTo(AddressLineType2) >= 0;

        #endregion

        #endregion

        #region IComparable<AddressLineType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two AddressLineTypes.
        /// </summary>
        /// <param name="Object">AddressLineType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is AddressLineType AddressLineType
                   ? CompareTo(AddressLineType)
                   : throw new ArgumentException("The given object is not AddressLineType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(AddressLineType)

        /// <summary>
        /// Compares two AddressLineTypes.
        /// </summary>
        /// <param name="AddressLineType">AddressLineType to compare with.</param>
        public Int32 CompareTo(AddressLineType AddressLineType)

            => String.Compare(InternalId,
                              AddressLineType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<AddressLineType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two AddressLineTypes for equality.
        /// </summary>
        /// <param name="Object">AddressLineType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is AddressLineType AddressLineType &&
                   Equals(AddressLineType);

        #endregion

        #region Equals(AddressLineType)

        /// <summary>
        /// Compares two AddressLineTypes for equality.
        /// </summary>
        /// <param name="AddressLineType">AddressLineType to compare with.</param>
        public Boolean Equals(AddressLineType AddressLineType)

            => String.Equals(InternalId,
                             AddressLineType.InternalId,
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
