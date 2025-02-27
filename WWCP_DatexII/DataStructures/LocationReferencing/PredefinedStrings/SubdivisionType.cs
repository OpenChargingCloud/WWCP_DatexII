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
    /// Extension methods for SubdivisionTypes.
    /// </summary>
    public static class SubdivisionTypeExtensions
    {

        /// <summary>
        /// Indicates whether this SubdivisionType is null or empty.
        /// </summary>
        /// <param name="SubdivisionType">A SubdivisionType.</param>
        public static Boolean IsNullOrEmpty(this SubdivisionType? SubdivisionType)
            => !SubdivisionType.HasValue || SubdivisionType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this SubdivisionType is null or empty.
        /// </summary>
        /// <param name="SubdivisionType">A SubdivisionType.</param>
        public static Boolean IsNotNullOrEmpty(this SubdivisionType? SubdivisionType)
            => SubdivisionType.HasValue && SubdivisionType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A SubdivisionType.
    /// </summary>
    public readonly struct SubdivisionType : IId,
                                             IEquatable<SubdivisionType>,
                                             IComparable<SubdivisionType>
    {

        #region Data

        private readonly static Dictionary<String, SubdivisionType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this SubdivisionType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this SubdivisionType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the SubdivisionType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<SubdivisionType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new SubdivisionType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a SubdivisionType.</param>
        private SubdivisionType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static SubdivisionType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new SubdivisionType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a SubdivisionType.
        /// </summary>
        /// <param name="Text">A text representation of a SubdivisionType.</param>
        public static SubdivisionType Parse(String Text)
        {

            if (TryParse(Text, out var subdivisionType))
                return subdivisionType;

            throw new ArgumentException($"Invalid text representation of a SubdivisionType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a SubdivisionType.
        /// </summary>
        /// <param name="Text">A text representation of a SubdivisionType.</param>
        public static SubdivisionType? TryParse(String Text)
        {

            if (TryParse(Text, out var subdivisionType))
                return subdivisionType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out SubdivisionType)

        /// <summary>
        /// Try to parse the given text as SubdivisionType.
        /// </summary>
        /// <param name="Text">A text representation of a SubdivisionType.</param>
        /// <param name="SubdivisionType">The parsed SubdivisionType.</param>
        public static Boolean TryParse(String Text, out SubdivisionType SubdivisionType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out SubdivisionType))
                    SubdivisionType = Register(Text);

                return true;

            }

            SubdivisionType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this SubdivisionType.
        /// </summary>
        public SubdivisionType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Administrative Atoll
        /// </summary>
        public static SubdivisionType  AdministrativeAtoll                { get; }
            = Register("administrativeAtoll");

        /// <summary>
        ///Administrative Region
        /// </summary>
        public static SubdivisionType  AdministrativeRegion               { get; }
            = Register("administrativeRegion");

        /// <summary>
        ///Administrative Territory
        /// </summary>
        public static SubdivisionType  AdministrativeTerritory            { get; }
            = Register("administrativeTerritory");

        /// <summary>
        /// Arctic Region
        /// </summary>
        public static SubdivisionType  ArcticRegion                       { get; }
            = Register("arcticRegion");

        /// <summary>
        /// Autonomous City
        /// </summary>
        public static SubdivisionType  AutonomousCity                     { get; }
            = Register("autonomousCity");

        /// <summary>
        /// Autonomous City in North Africa
        /// </summary>
        public static SubdivisionType  AutonomousCityInNorthAfrica        { get; }
            = Register("autonomousCityInNorthAfrica");

        /// <summary>
        /// Autonomous Community
        /// </summary>
        public static SubdivisionType  AutonomousCommunity                { get; }
            = Register("autonomousCommunity");

        /// <summary>
        /// Autonomous District
        /// </summary>
        public static SubdivisionType  AutonomousDistrict                 { get; }
            = Register("autonomousDistrict");

        /// <summary>
        /// Autonomous Province
        /// </summary>
        public static SubdivisionType  AutonomousProvince                 { get; }
            = Register("autonomousProvince");

        /// <summary>
        /// Autonomous Region
        /// </summary>
        public static SubdivisionType  AutonomousRegion                   { get; }
            = Register("autonomousRegion");

        /// <summary>
        /// Canton
        /// </summary>
        public static SubdivisionType  Canton                             { get; }
            = Register("canton");

        /// <summary>
        /// Capital City
        /// </summary>
        public static SubdivisionType  CapitalCity                        { get; }
            = Register("capitalCity");

        /// <summary>
        /// City
        /// </summary>
        public static SubdivisionType  City                               { get; }
            = Register("city");

        /// <summary>
        /// City Municipality
        /// </summary>
        public static SubdivisionType  CityMunicipality                   { get; }
            = Register("cityMunicipality");

        /// <summary>
        /// City of County Right
        /// </summary>
        public static SubdivisionType  CityOfCountyRight                  { get; }
            = Register("cityOfCountyRight");

        /// <summary>
        /// Commune
        /// </summary>
        public static SubdivisionType  Commune                            { get; }
            = Register("commune");

        /// <summary>
        /// Council Area
        /// </summary>
        public static SubdivisionType  CouncilArea                        { get; }
            = Register("councilArea");

        /// <summary>
        /// County
        /// </summary>
        public static SubdivisionType  County                             { get; }
            = Register("county");

        /// <summary>
        /// Country
        /// </summary>
        public static SubdivisionType  Country                            { get; }
            = Register("country");

        /// <summary>
        /// Department
        /// </summary>
        public static SubdivisionType  Department                         { get; }
            = Register("department");

        /// <summary>
        /// Dependency
        /// </summary>
        public static SubdivisionType  Dependency                         { get; }
            = Register("dependency");

        /// <summary>
        /// District
        /// </summary>
        public static SubdivisionType  District                           { get; }
            = Register("district");

        /// <summary>
        /// District Municipality
        /// </summary>
        public static SubdivisionType  DistrictMunicipality               { get; }
            = Register("districtMunicipality");

        /// <summary>
        /// District with Special Status
        /// </summary>
        public static SubdivisionType  DistrictWithSpecialStatus          { get; }
            = Register("districtWithSpecialStatus");

        /// <summary>
        /// Entity
        /// </summary>
        public static SubdivisionType  Entity                             { get; }
            = Register("entity");

        /// <summary>
        /// Geographical Entity
        /// </summary>
        public static SubdivisionType  GeographicalEntity                 { get; }
            = Register("geographicalEntity");

        /// <summary>
        /// Governorate
        /// </summary>
        public static SubdivisionType  Governorate                        { get; }
            = Register("governorate");

        /// <summary>
        /// Laender
        /// </summary>
        public static SubdivisionType  Laender                            { get; }
            = Register("laender");

        /// <summary>
        /// Local Council
        /// </summary>
        public static SubdivisionType  LocalCouncil                       { get; }
            = Register("localCouncil");

        /// <summary>
        /// London Borough
        /// </summary>
        public static SubdivisionType  LondonBorough                      { get; }
            = Register("londonBorough");

        /// <summary>
        /// Metropolitan Area
        /// </summary>
        public static SubdivisionType  MetropolitanArea                   { get; }
            = Register("metropolitanArea");

        /// <summary>
        /// Metropolitan Department
        /// </summary>
        public static SubdivisionType  MetropolitanDepartment             { get; }
            = Register("metropolitanDepartment");

        /// <summary>
        /// Metropolitan District
        /// </summary>
        public static SubdivisionType  MetropolitanDistrict               { get; }
            = Register("metropolitanDistrict");

        /// <summary>
        /// Metropolitan Region
        /// </summary>
        public static SubdivisionType  MetropolitanRegion                 { get; }
            = Register("metropolitanRegion");

        /// <summary>
        /// Municipality
        /// </summary>
        public static SubdivisionType  Municipality                       { get; }
            = Register("municipality");

        /// <summary>
        /// Overseas Department
        /// </summary>
        public static SubdivisionType  OverseasDepartment                 { get; }
            = Register("overseasDepartment");

        /// <summary>
        /// Overseas Region
        /// </summary>
        public static SubdivisionType  OverseasRegion                     { get; }
            = Register("overseasRegion");

        /// <summary>
        /// Overseas Territorial Collectivity
        /// </summary>
        public static SubdivisionType  OverseasTerritorialCollectivity    { get; }
            = Register("overseasTerritorialCollectivity");

        /// <summary>
        /// Parish
        /// </summary>
        public static SubdivisionType  Parish                             { get; }
            = Register("parish");

        /// <summary>
        /// Province
        /// </summary>
        public static SubdivisionType  Province                           { get; }
            = Register("province");

        /// <summary>
        /// Quarter
        /// </summary>
        public static SubdivisionType  Quarter                            { get; }
            = Register("quarter");

        /// <summary>
        /// Region
        /// </summary>
        public static SubdivisionType  Region                             { get; }
            = Register("region");

        /// <summary>
        /// Republic
        /// </summary>
        public static SubdivisionType  Republic                           { get; }
            = Register("republic");

        /// <summary>
        /// Republican City
        /// </summary>
        public static SubdivisionType  RepublicanCity                     { get; }
            = Register("republicanCity");

        /// <summary>
        /// Self Governed Part
        /// </summary>
        public static SubdivisionType  SelfGovernedPart                   { get; }
            = Register("selfGovernedPart");

        /// <summary>
        /// Special Municipality
        /// </summary>
        public static SubdivisionType  SpecialMunicipality                { get; }
            = Register("specialMunicipality");

        /// <summary>
        /// State
        /// </summary>
        public static SubdivisionType  State                              { get; }
            = Register("state");

        /// <summary>
        /// Territorial Unit
        /// </summary>
        public static SubdivisionType  TerritorialUnit                    { get; }
            = Register("territorialUnit");

        /// <summary>
        /// Territory
        /// </summary>
        public static SubdivisionType  Territory                          { get; }
            = Register("territory");

        /// <summary>
        /// Two Tier County
        /// </summary>
        public static SubdivisionType  TwoTierCounty                      { get; }
            = Register("twoTierCounty");

        /// <summary>
        /// Unitary Authority
        /// </summary>
        public static SubdivisionType  UnitaryAuthority                   { get; }
            = Register("unitaryAuthority");

        /// <summary>
        /// Ward
        /// </summary>
        public static SubdivisionType  Ward                               { get; }
            = Register("nuts1Code");

        /// <summary>
        /// Other
        /// </summary>
        public static SubdivisionType  Other                              { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (SubdivisionType1, SubdivisionType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SubdivisionType1">A SubdivisionType.</param>
        /// <param name="SubdivisionType2">Another SubdivisionType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (SubdivisionType SubdivisionType1,
                                           SubdivisionType SubdivisionType2)

            => SubdivisionType1.Equals(SubdivisionType2);

        #endregion

        #region Operator != (SubdivisionType1, SubdivisionType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SubdivisionType1">A SubdivisionType.</param>
        /// <param name="SubdivisionType2">Another SubdivisionType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (SubdivisionType SubdivisionType1,
                                           SubdivisionType SubdivisionType2)

            => !SubdivisionType1.Equals(SubdivisionType2);

        #endregion

        #region Operator <  (SubdivisionType1, SubdivisionType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SubdivisionType1">A SubdivisionType.</param>
        /// <param name="SubdivisionType2">Another SubdivisionType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (SubdivisionType SubdivisionType1,
                                          SubdivisionType SubdivisionType2)

            => SubdivisionType1.CompareTo(SubdivisionType2) < 0;

        #endregion

        #region Operator <= (SubdivisionType1, SubdivisionType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SubdivisionType1">A SubdivisionType.</param>
        /// <param name="SubdivisionType2">Another SubdivisionType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (SubdivisionType SubdivisionType1,
                                           SubdivisionType SubdivisionType2)

            => SubdivisionType1.CompareTo(SubdivisionType2) <= 0;

        #endregion

        #region Operator >  (SubdivisionType1, SubdivisionType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SubdivisionType1">A SubdivisionType.</param>
        /// <param name="SubdivisionType2">Another SubdivisionType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (SubdivisionType SubdivisionType1,
                                          SubdivisionType SubdivisionType2)

            => SubdivisionType1.CompareTo(SubdivisionType2) > 0;

        #endregion

        #region Operator >= (SubdivisionType1, SubdivisionType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SubdivisionType1">A SubdivisionType.</param>
        /// <param name="SubdivisionType2">Another SubdivisionType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (SubdivisionType SubdivisionType1,
                                           SubdivisionType SubdivisionType2)

            => SubdivisionType1.CompareTo(SubdivisionType2) >= 0;

        #endregion

        #endregion

        #region IComparable<SubdivisionType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two SubdivisionTypes.
        /// </summary>
        /// <param name="Object">SubdivisionType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is SubdivisionType SubdivisionType
                   ? CompareTo(SubdivisionType)
                   : throw new ArgumentException("The given object is not SubdivisionType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(SubdivisionType)

        /// <summary>
        /// Compares two SubdivisionTypes.
        /// </summary>
        /// <param name="SubdivisionType">SubdivisionType to compare with.</param>
        public Int32 CompareTo(SubdivisionType SubdivisionType)

            => String.Compare(InternalId,
                              SubdivisionType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<SubdivisionType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two SubdivisionTypes for equality.
        /// </summary>
        /// <param name="Object">SubdivisionType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is SubdivisionType SubdivisionType &&
                   Equals(SubdivisionType);

        #endregion

        #region Equals(SubdivisionType)

        /// <summary>
        /// Compares two SubdivisionTypes for equality.
        /// </summary>
        /// <param name="SubdivisionType">SubdivisionType to compare with.</param>
        public Boolean Equals(SubdivisionType SubdivisionType)

            => String.Equals(InternalId,
                             SubdivisionType.InternalId,
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
