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
    /// Extension methods for NamedAreaTypes.
    /// </summary>
    public static class NamedAreaTypeExtensions
    {

        /// <summary>
        /// Indicates whether this NamedAreaType is null or empty.
        /// </summary>
        /// <param name="NamedAreaType">A NamedAreaType.</param>
        public static Boolean IsNullOrEmpty(this NamedAreaType? NamedAreaType)
            => !NamedAreaType.HasValue || NamedAreaType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this NamedAreaType is null or empty.
        /// </summary>
        /// <param name="NamedAreaType">A NamedAreaType.</param>
        public static Boolean IsNotNullOrEmpty(this NamedAreaType? NamedAreaType)
            => NamedAreaType.HasValue && NamedAreaType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A NamedAreaType.
    /// </summary>
    public readonly struct NamedAreaType : IId,
                                           IEquatable<NamedAreaType>,
                                           IComparable<NamedAreaType>
    {

        #region Data

        private readonly static Dictionary<String, NamedAreaType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this NamedAreaType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this NamedAreaType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the NamedAreaType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<NamedAreaType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new NamedAreaType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a NamedAreaType.</param>
        private NamedAreaType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static NamedAreaType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new NamedAreaType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a NamedAreaType.
        /// </summary>
        /// <param name="Text">A text representation of a NamedAreaType.</param>
        public static NamedAreaType Parse(String Text)
        {

            if (TryParse(Text, out var namedAreaType))
                return namedAreaType;

            throw new ArgumentException($"Invalid text representation of a NamedAreaType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a NamedAreaType.
        /// </summary>
        /// <param name="Text">A text representation of a NamedAreaType.</param>
        public static NamedAreaType? TryParse(String Text)
        {

            if (TryParse(Text, out var namedAreaType))
                return namedAreaType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out NamedAreaType)

        /// <summary>
        /// Try to parse the given text as NamedAreaType.
        /// </summary>
        /// <param name="Text">A text representation of a NamedAreaType.</param>
        /// <param name="NamedAreaType">The parsed NamedAreaType.</param>
        public static Boolean TryParse(String Text, out NamedAreaType NamedAreaType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out NamedAreaType))
                    NamedAreaType = Register(Text);

                return true;

            }

            NamedAreaType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this NamedAreaType.
        /// </summary>
        public NamedAreaType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Application Region
        /// </summary>
        public static NamedAreaType  ApplicationRegion           { get; }
            = Register("applicationRegion");

        /// <summary>
        /// Continent
        /// </summary>
        public static NamedAreaType  Continent                   { get; }
            = Register("continent");

        /// <summary>
        /// Country
        /// </summary>
        public static NamedAreaType  Country                     { get; }
            = Register("country");

        /// <summary>
        /// Country Group
        /// </summary>
        public static NamedAreaType  CountryGroup                { get; }
            = Register("countryGroup");

        /// <summary>
        /// Car Park Area
        /// </summary>
        public static NamedAreaType  CarParkArea                 { get; }
            = Register("carParkArea");

        /// <summary>
        /// Carpool Area
        /// </summary>
        public static NamedAreaType  CarpoolArea                 { get; }
            = Register("carpoolArea");

        /// <summary>
        /// Fuzzy Area
        /// </summary>
        public static NamedAreaType  FuzzyArea                   { get; }
            = Register("fuzzyArea");

        /// <summary>
        /// Industrial Area
        /// </summary>
        public static NamedAreaType  IndustrialArea              { get; }
            = Register("industrialArea");

        /// <summary>
        /// Lake
        /// </summary>
        public static NamedAreaType  Lake                        { get; }
            = Register("lake");

        /// <summary>
        /// Meteorological Area
        /// </summary>
        public static NamedAreaType  MeteorologicalArea          { get; }
            = Register("meteorologicalArea");

        /// <summary>
        /// Metropolitan Area
        /// </summary>
        public static NamedAreaType  MetropolitanArea            { get; }
            = Register("metropolitanArea");

        /// <summary>
        /// Municipality
        /// </summary>
        public static NamedAreaType  Municipality                { get; }
            = Register("municipality");

        /// <summary>
        /// Park and Ride Site
        /// </summary>
        public static NamedAreaType  ParkAndRideSite             { get; }
            = Register("parkAndRideSite");

        /// <summary>
        /// Rural County
        /// </summary>
        public static NamedAreaType  RuralCounty                 { get; }
            = Register("ruralCounty");

        /// <summary>
        /// Sea
        /// </summary>
        public static NamedAreaType  Sea                         { get; }
            = Register("sea");

        /// <summary>
        /// Tourist Area
        /// </summary>
        public static NamedAreaType  TouristArea                 { get; }
            = Register("touristArea");

        /// <summary>
        /// Traffic Area
        /// </summary>
        public static NamedAreaType  TrafficArea                 { get; }
            = Register("trafficArea");

        /// <summary>
        /// Urban County
        /// </summary>
        public static NamedAreaType  UrbanCounty                 { get; }
            = Register("urbanCounty");

        /// <summary>
        /// Order1 Administrative Area
        /// </summary>
        public static NamedAreaType  Order1AdministrativeArea    { get; }
            = Register("order1AdministrativeArea");

        /// <summary>
        /// Order2 Administrative Area
        /// </summary>
        public static NamedAreaType  Order2AdministrativeArea    { get; }
            = Register("order2AdministrativeArea");

        /// <summary>
        /// Order3 Administrative Area
        /// </summary>
        public static NamedAreaType  Order3AdministrativeArea    { get; }
            = Register("order3AdministrativeArea");

        /// <summary>
        /// Order4 Administrative Area
        /// </summary>
        public static NamedAreaType  Order4AdministrativeArea    { get; }
            = Register("order4AdministrativeArea");

        /// <summary>
        /// Order5 Administrative Area
        /// </summary>
        public static NamedAreaType  Order5AdministrativeArea    { get; }
            = Register("order5AdministrativeArea");

        /// <summary>
        /// Police Force Control Area
        /// </summary>
        public static NamedAreaType  PoliceForceControlArea      { get; }
            = Register("policeForceControlArea");

        /// <summary>
        /// Road Operator Control Area
        /// </summary>
        public static NamedAreaType  RoadOperatorControlArea     { get; }
            = Register("roadOperatorControlArea");

        /// <summary>
        /// Water Area
        /// </summary>
        public static NamedAreaType  WaterArea                   { get; }
            = Register("waterArea");

        #endregion


        #region Operator overloading

        #region Operator == (NamedAreaType1, NamedAreaType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NamedAreaType1">A NamedAreaType.</param>
        /// <param name="NamedAreaType2">Another NamedAreaType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (NamedAreaType NamedAreaType1,
                                           NamedAreaType NamedAreaType2)

            => NamedAreaType1.Equals(NamedAreaType2);

        #endregion

        #region Operator != (NamedAreaType1, NamedAreaType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NamedAreaType1">A NamedAreaType.</param>
        /// <param name="NamedAreaType2">Another NamedAreaType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (NamedAreaType NamedAreaType1,
                                           NamedAreaType NamedAreaType2)

            => !NamedAreaType1.Equals(NamedAreaType2);

        #endregion

        #region Operator <  (NamedAreaType1, NamedAreaType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NamedAreaType1">A NamedAreaType.</param>
        /// <param name="NamedAreaType2">Another NamedAreaType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (NamedAreaType NamedAreaType1,
                                          NamedAreaType NamedAreaType2)

            => NamedAreaType1.CompareTo(NamedAreaType2) < 0;

        #endregion

        #region Operator <= (NamedAreaType1, NamedAreaType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NamedAreaType1">A NamedAreaType.</param>
        /// <param name="NamedAreaType2">Another NamedAreaType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (NamedAreaType NamedAreaType1,
                                           NamedAreaType NamedAreaType2)

            => NamedAreaType1.CompareTo(NamedAreaType2) <= 0;

        #endregion

        #region Operator >  (NamedAreaType1, NamedAreaType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NamedAreaType1">A NamedAreaType.</param>
        /// <param name="NamedAreaType2">Another NamedAreaType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (NamedAreaType NamedAreaType1,
                                          NamedAreaType NamedAreaType2)

            => NamedAreaType1.CompareTo(NamedAreaType2) > 0;

        #endregion

        #region Operator >= (NamedAreaType1, NamedAreaType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="NamedAreaType1">A NamedAreaType.</param>
        /// <param name="NamedAreaType2">Another NamedAreaType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (NamedAreaType NamedAreaType1,
                                           NamedAreaType NamedAreaType2)

            => NamedAreaType1.CompareTo(NamedAreaType2) >= 0;

        #endregion

        #endregion

        #region IComparable<NamedAreaType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two NamedAreaTypes.
        /// </summary>
        /// <param name="Object">NamedAreaType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is NamedAreaType NamedAreaType
                   ? CompareTo(NamedAreaType)
                   : throw new ArgumentException("The given object is not NamedAreaType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(NamedAreaType)

        /// <summary>
        /// Compares two NamedAreaTypes.
        /// </summary>
        /// <param name="NamedAreaType">NamedAreaType to compare with.</param>
        public Int32 CompareTo(NamedAreaType NamedAreaType)

            => String.Compare(InternalId,
                              NamedAreaType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<NamedAreaType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two NamedAreaTypes for equality.
        /// </summary>
        /// <param name="Object">NamedAreaType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is NamedAreaType NamedAreaType &&
                   Equals(NamedAreaType);

        #endregion

        #region Equals(NamedAreaType)

        /// <summary>
        /// Compares two NamedAreaTypes for equality.
        /// </summary>
        /// <param name="NamedAreaType">NamedAreaType to compare with.</param>
        public Boolean Equals(NamedAreaType NamedAreaType)

            => String.Equals(InternalId,
                             NamedAreaType.InternalId,
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
