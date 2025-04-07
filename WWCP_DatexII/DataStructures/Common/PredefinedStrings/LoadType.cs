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
    /// Extension methods for LoadTypes.
    /// </summary>
    public static class LoadTypeExtensions
    {

        /// <summary>
        /// Indicates whether this LoadType is null or empty.
        /// </summary>
        /// <param name="LoadType">A LoadType.</param>
        public static Boolean IsNullOrEmpty(this LoadType? LoadType)
            => !LoadType.HasValue || LoadType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this LoadType is null or empty.
        /// </summary>
        /// <param name="LoadType">A LoadType.</param>
        public static Boolean IsNotNullOrEmpty(this LoadType? LoadType)
            => LoadType.HasValue && LoadType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A LoadType.
    /// </summary>
    public readonly struct LoadType : IId,
                                      IEquatable<LoadType>,
                                      IComparable<LoadType>
    {

        #region Data

        private readonly static Dictionary<String, LoadType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this LoadType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this LoadType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the LoadType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<LoadType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new LoadType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a LoadType.</param>
        private LoadType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static LoadType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new LoadType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a LoadType.
        /// </summary>
        /// <param name="Text">A text representation of a LoadType.</param>
        public static LoadType Parse(String Text)
        {

            if (TryParse(Text, out var loadType))
                return loadType;

            throw new ArgumentException($"Invalid text representation of a LoadType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a LoadType.
        /// </summary>
        /// <param name="Text">A text representation of a LoadType.</param>
        public static LoadType? TryParse(String Text)
        {

            if (TryParse(Text, out var loadType))
                return loadType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out LoadType)

        /// <summary>
        /// Try to parse the given text as LoadType.
        /// </summary>
        /// <param name="Text">A text representation of a LoadType.</param>
        /// <param name="LoadType">The parsed LoadType.</param>
        public static Boolean TryParse(String Text, out LoadType LoadType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out LoadType))
                    LoadType = Register(Text);

                return true;

            }

            LoadType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this LoadType.
        /// </summary>
        public LoadType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Abnormal Load
        /// </summary>
        public static LoadType  AbnormalLoad                           { get; }
            = Register("abnormalLoad");

        /// <summary>
        /// Ammunition
        /// </summary>
        public static LoadType  Ammunition                             { get; }
            = Register("ammunition");

        /// <summary>
        /// Chemicals
        /// </summary>
        public static LoadType  Chemicals                              { get; }
            = Register("chemicals");

        /// <summary>
        /// Combustible Materials
        /// </summary>
        public static LoadType  CombustibleMaterials                   { get; }
            = Register("combustibleMaterials");

        /// <summary>
        /// Corrosive Materials
        /// </summary>
        public static LoadType  CorrosiveMaterials                     { get; }
            = Register("corrosiveMaterials");

        /// <summary>
        /// Debris
        /// </summary>
        public static LoadType  Debris                                 { get; }
            = Register("debris");

        /// <summary>
        /// Empty
        /// </summary>
        public static LoadType  Empty                                  { get; }
            = Register("empty");

        /// <summary>
        /// Explosive Materials
        /// </summary>
        public static LoadType  ExplosiveMaterials                     { get; }
            = Register("explosiveMaterials");

        /// <summary>
        /// Extra High Load
        /// </summary>
        public static LoadType  ExtraHighLoad                          { get; }
            = Register("extraHighLoad");

        /// <summary>
        /// Extra Long Load
        /// </summary>
        public static LoadType  ExtraLongLoad                          { get; }
            = Register("extraLongLoad");

        /// <summary>
        /// Extra Wide Load
        /// </summary>
        public static LoadType  ExtraWideLoad                          { get; }
            = Register("extraWideLoad");

        /// <summary>
        /// Fuel
        /// </summary>
        public static LoadType  Fuel                                   { get; }
            = Register("fuel");

        /// <summary>
        /// Glass
        /// </summary>
        public static LoadType  Glass                                  { get; }
            = Register("glass");

        /// <summary>
        /// Goods
        /// </summary>
        public static LoadType  Goods                                  { get; }
            = Register("goods");

        /// <summary>
        /// Hazardous Materials
        /// </summary>
        public static LoadType  HazardousMaterials                     { get; }
            = Register("hazardousMaterials");

        /// <summary>
        /// Liquid
        /// </summary>
        public static LoadType  Liquid                                 { get; }
            = Register("liquid");

        /// <summary>
        /// Livestock
        /// </summary>
        public static LoadType  Livestock                              { get; }
            = Register("livestock");

        /// <summary>
        /// Materials
        /// </summary>
        public static LoadType  Materials                              { get; }
            = Register("materials");

        /// <summary>
        /// Materials Dangerous for People
        /// </summary>
        public static LoadType  MaterialsDangerousForPeople            { get; }
            = Register("materialsDangerousForPeople");

        /// <summary>
        /// Materials Dangerous for the Environment
        /// </summary>
        public static LoadType  MaterialsDangerousForTheEnvironment    { get; }
            = Register("materialsDangerousForTheEnvironment");

        /// <summary>
        /// Materials Dangerous for Water
        /// </summary>
        public static LoadType  MaterialsDangerousForWater             { get; }
            = Register("materialsDangerousForWater");

        /// <summary>
        /// Oil
        /// </summary>
        public static LoadType  Oil                                    { get; }
            = Register("oil");

        /// <summary>
        /// Ordinary
        /// </summary>
        public static LoadType  Ordinary                               { get; }
            = Register("ordinary");

        /// <summary>
        /// Perishable Products
        /// </summary>
        public static LoadType  PerishableProducts                     { get; }
            = Register("perishableProducts");

        /// <summary>
        /// Petrol
        /// </summary>
        public static LoadType  Petrol                                 { get; }
            = Register("petrol");

        /// <summary>
        /// Pharmaceutical Materials
        /// </summary>
        public static LoadType  PharmaceuticalMaterials                { get; }
            = Register("pharmaceuticalMaterials");

        /// <summary>
        /// Radioactive Materials
        /// </summary>
        public static LoadType  RadioactiveMaterials                   { get; }
            = Register("radioactiveMaterials");

        /// <summary>
        /// Refrigerated Goods
        /// </summary>
        public static LoadType  RefrigeratedGoods                      { get; }
            = Register("refrigeratedGoods");

        /// <summary>
        /// Refuse
        /// </summary>
        public static LoadType  Refuse                                 { get; }
            = Register("refuse");

        /// <summary>
        /// Toxic Materials
        /// </summary>
        public static LoadType  ToxicMaterials                         { get; }
            = Register("toxicMaterials");

        /// <summary>
        /// Vehicles
        /// </summary>
        public static LoadType  Vehicles                               { get; }
            = Register("vehicles");

        /// <summary>
        /// Other
        /// </summary>
        public static LoadType  Other                                  { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (LoadType1, LoadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LoadType1">A LoadType.</param>
        /// <param name="LoadType2">Another LoadType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (LoadType LoadType1,
                                           LoadType LoadType2)

            => LoadType1.Equals(LoadType2);

        #endregion

        #region Operator != (LoadType1, LoadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LoadType1">A LoadType.</param>
        /// <param name="LoadType2">Another LoadType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (LoadType LoadType1,
                                           LoadType LoadType2)

            => !LoadType1.Equals(LoadType2);

        #endregion

        #region Operator <  (LoadType1, LoadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LoadType1">A LoadType.</param>
        /// <param name="LoadType2">Another LoadType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (LoadType LoadType1,
                                          LoadType LoadType2)

            => LoadType1.CompareTo(LoadType2) < 0;

        #endregion

        #region Operator <= (LoadType1, LoadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LoadType1">A LoadType.</param>
        /// <param name="LoadType2">Another LoadType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (LoadType LoadType1,
                                           LoadType LoadType2)

            => LoadType1.CompareTo(LoadType2) <= 0;

        #endregion

        #region Operator >  (LoadType1, LoadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LoadType1">A LoadType.</param>
        /// <param name="LoadType2">Another LoadType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (LoadType LoadType1,
                                          LoadType LoadType2)

            => LoadType1.CompareTo(LoadType2) > 0;

        #endregion

        #region Operator >= (LoadType1, LoadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LoadType1">A LoadType.</param>
        /// <param name="LoadType2">Another LoadType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (LoadType LoadType1,
                                           LoadType LoadType2)

            => LoadType1.CompareTo(LoadType2) >= 0;

        #endregion

        #endregion

        #region IComparable<LoadType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two LoadTypes.
        /// </summary>
        /// <param name="Object">LoadType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is LoadType loadType
                   ? CompareTo(loadType)
                   : throw new ArgumentException("The given object is not LoadType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(LoadType)

        /// <summary>
        /// Compares two LoadTypes.
        /// </summary>
        /// <param name="LoadType">LoadType to compare with.</param>
        public Int32 CompareTo(LoadType LoadType)

            => String.Compare(InternalId,
                              LoadType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<LoadType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two LoadTypes for equality.
        /// </summary>
        /// <param name="Object">LoadType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is LoadType loadType &&
                   Equals(loadType);

        #endregion

        #region Equals(LoadType)

        /// <summary>
        /// Compares two LoadTypes for equality.
        /// </summary>
        /// <param name="LoadType">LoadType to compare with.</param>
        public Boolean Equals(LoadType LoadType)

            => String.Equals(InternalId,
                             LoadType.InternalId,
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
