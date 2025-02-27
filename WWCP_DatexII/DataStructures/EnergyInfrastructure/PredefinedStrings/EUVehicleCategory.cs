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
    /// Extension methods for EUVehicleCategories.
    /// </summary>
    public static class EUVehicleCategoryExtensions
    {

        /// <summary>
        /// Indicates whether this EUVehicleCategory is null or empty.
        /// </summary>
        /// <param name="EUVehicleCategory">A EUVehicleCategory.</param>
        public static Boolean IsNullOrEmpty(this EUVehicleCategory? EUVehicleCategory)
            => !EUVehicleCategory.HasValue || EUVehicleCategory.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this EUVehicleCategory is null or empty.
        /// </summary>
        /// <param name="EUVehicleCategory">A EUVehicleCategory.</param>
        public static Boolean IsNotNullOrEmpty(this EUVehicleCategory? EUVehicleCategory)
            => EUVehicleCategory.HasValue && EUVehicleCategory.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An EU Vehicle Category.
    /// </summary>
    public readonly struct EUVehicleCategory : IId,
                                               IEquatable<EUVehicleCategory>,
                                               IComparable<EUVehicleCategory>
    {

        #region Data

        private readonly static Dictionary<String, EUVehicleCategory>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this EUVehicleCategory is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this EUVehicleCategory is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the EUVehicleCategory.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered EUVehicleCategories.
        /// </summary>
        public static    IEnumerable<EUVehicleCategory>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new EUVehicleCategory based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a EUVehicleCategory.</param>
        private EUVehicleCategory(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static EUVehicleCategory Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new EUVehicleCategory(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a EUVehicleCategory.
        /// </summary>
        /// <param name="Text">A text representation of a EUVehicleCategory.</param>
        public static EUVehicleCategory Parse(String Text)
        {

            if (TryParse(Text, out var euVehicleCategory))
                return euVehicleCategory;

            throw new ArgumentException($"Invalid text representation of a EUVehicleCategory: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a EUVehicleCategory.
        /// </summary>
        /// <param name="Text">A text representation of a EUVehicleCategory.</param>
        public static EUVehicleCategory? TryParse(String Text)
        {

            if (TryParse(Text, out var euVehicleCategory))
                return euVehicleCategory;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out EUVehicleCategory)

        /// <summary>
        /// Try to parse the given text as a EUVehicleCategory.
        /// </summary>
        /// <param name="Text">A text representation of a EUVehicleCategory.</param>
        /// <param name="EUVehicleCategory">The parsed EUVehicleCategory.</param>
        public static Boolean TryParse(String Text, out EUVehicleCategory EUVehicleCategory)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out EUVehicleCategory))
                    EUVehicleCategory = Register(Text);

                return true;

            }

            EUVehicleCategory = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this EUVehicleCategory.
        /// </summary>
        public EUVehicleCategory Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Mopeds, Motorcycles, Motor Tricycles and Quadricycles.
        /// </summary>
        public static EUVehicleCategory  L                  { get; }
            = Register("l");

        /// <summary>
        /// Light two-wheel powered vehicle.
        /// </summary>
        public static EUVehicleCategory  L1                 { get; }
            = Register("l1");

        /// <summary>
        /// Three-wheel moped.
        /// </summary>
        public static EUVehicleCategory  L2                 { get; }
            = Register("l2");

        /// <summary>
        /// Two-wheel motorcycle.
        /// </summary>
        public static EUVehicleCategory  L3                 { get; }
            = Register("l3");

        /// <summary>
        /// Two-wheel motorcycle with side-car.
        /// </summary>
        public static EUVehicleCategory  L4                 { get; }
            = Register("l4");

        /// <summary>
        /// Powered tricycle.
        /// </summary>
        public static EUVehicleCategory  L5                 { get; }
            = Register("l5");

        /// <summary>
        /// Light quadricycle.
        /// </summary>
        public static EUVehicleCategory  L6                 { get; }
            = Register("l6");

        /// <summary>
        /// Heavy quadricycle.
        /// </summary>
        public static EUVehicleCategory  L7                 { get; }
            = Register("l7");

        /// <summary>
        /// Motor vehicles with at least four wheels designed and constructed for the carriage of passengers.
        /// </summary>
        public static EUVehicleCategory  M                  { get; }
            = Register("m");

        /// <summary>
        /// Trailers, the sum of the technically permissible masses per axle of which does not exceed 1,500 kg.
        /// </summary>
        public static EUVehicleCategory  R1                 { get; }
            = Register("r1");

        /// <summary>
        /// Trailers, the sum of the technically permissible masses per axle of which exceeds 1,500 kg but does not exceed 3,500 kg.
        /// </summary>
        public static EUVehicleCategory  R2                 { get; }
            = Register("r2");

        /// <summary>
        /// Trailers, the sum of the technically permissible masses per axle of which exceeds 3,500 kg but does not exceed 21,000 kg.
        /// </summary>
        public static EUVehicleCategory  R3                 { get; }
            = Register("r3");

        /// <summary>
        /// Trailers, the sum of the technically permissible masses per axle of which exceeds 21,000 kg.
        /// </summary>
        public static EUVehicleCategory  R4                 { get; }
            = Register("r4");

        /// <summary>
        /// Wheeled tractors with the closest axle to the driver having a minimum track width of not less than 1,500 mm; with an unladen mass in running order of more than 600 kg and with a ground clearance of not more than 1,000 mm.
        /// </summary>
        public static EUVehicleCategory  T1                 { get; }
            = Register("t1");

        /// <summary>
        /// Vehicle category t2 according to Regulation(EU) No 167/2013.
        /// </summary>
        public static EUVehicleCategory  T2                 { get; }
            = Register("t2");

        /// <summary>
        /// Wheeled tractors with unladen mass in running order of not more than 600 kg.
        /// </summary>
        public static EUVehicleCategory  T3                 { get; }
            = Register("t3");

        /// <summary>
        /// Special purpose wheeled tractors with a maximum design speed of not more than 40 km/h.
        /// </summary>
        public static EUVehicleCategory  T4                 { get; }
            = Register("t4");

        /// <summary>
        /// Category T4.1: High clearance tractors.
        /// </summary>
        public static EUVehicleCategory  T41                { get; }
            = Register("t41");

        /// <summary>
        /// Category T4.2: Extra-wide tractors.
        /// </summary>
        public static EUVehicleCategory  T42                { get; }
            = Register("t42");

        /// <summary>
        /// Category T4.3: Low clearance tractors.
        /// </summary>
        public static EUVehicleCategory  T43                { get; }
            = Register("t43");

        /// <summary>
        /// Vehicles designed and constructed for the carriage of passengers and comprising no more than eight seats in addition to the driver’s seat.
        /// </summary>
        public static EUVehicleCategory  M1                 { get; }
            = Register("m1");

        /// <summary>
        /// Vehicles designed and constructed for the carriage of passengers, comprising more than eight seats in addition to the driver’s seat, and having a maximum mass not exceeding 5 tonnes.
        /// </summary>
        public static EUVehicleCategory  M2                 { get; }
            = Register("m2");

        /// <summary>
        /// Vehicles designed and constructed for the carriage of passengers, comprising more than eight seats in addition to the driver’s seat, and having a maximum mass exceeding 5 tonnes.
        /// </summary>
        public static EUVehicleCategory  M3                 { get; }
            = Register("m3");

        /// <summary>
        /// Motor vehicles with at least four wheels designed and constructed for the carriage of goods.
        /// </summary>
        public static EUVehicleCategory  N                  { get; }
            = Register("n");

        /// <summary>
        /// Vehicles designed and constructed for the carriage of goods and having a maximum mass not exceeding 3,5 tonnes.
        /// </summary>
        public static EUVehicleCategory  N1                 { get; }
            = Register("n1");

        /// <summary>
        /// Light commercial vehicles with ≤ 1,305 kg reference mass.
        /// </summary>
        public static EUVehicleCategory  N1ClassI          { get; }
            = Register("n1ClassI");

        /// <summary>
        /// Light commercial vehicles with 1,305–1,760 kg reference mass.
        /// </summary>
        public static EUVehicleCategory  N1ClassII          { get; }
            = Register("xxn1ClassIIx");

        /// <summary>
        /// N1 Class III.
        /// </summary>
        public static EUVehicleCategory  N1ClassIII         { get; }
            = Register("n1ClassIII");

        /// <summary>
        /// Light commercial vehicles > 1,760 kg reference mass and max 3,500 kg.
        /// </summary>
        public static EUVehicleCategory  N1ClassIIIAndN2    { get; }
            = Register("n1ClassIIIAndN2");

        /// <summary>
        /// Vehicles designed and constructed for the carriage of goods and having a maximum mass exceeding 3,5 tonnes but not exceeding 12 tonnes.
        /// </summary>
        public static EUVehicleCategory  N2                 { get; }
            = Register("n2");

        /// <summary>
        /// Vehicles designed and constructed for the carriage of goods and having a maximum mass exceeding 12 tonnes.
        /// </summary>
        public static EUVehicleCategory  N3                 { get; }
            = Register("n3");

        /// <summary>
        /// Trailers (including semi-trailers).
        /// </summary>
        public static EUVehicleCategory  O                  { get; }
            = Register("o");

        /// <summary>
        /// Trailers with a maximum mass not exceeding 0,75 tonnes.
        /// </summary>
        public static EUVehicleCategory  O1                 { get; }
            = Register("o1");

        /// <summary>
        /// Trailers with a maximum mass exceeding 0,75 tonnes but not exceeding 3,5 tonnes.
        /// </summary>
        public static EUVehicleCategory  O2                 { get; }
            = Register("o2");

        /// <summary>
        /// Trailers with a maximum mass exceeding 3,5 tonnes but not exceeding 10 tonnes.
        /// </summary>
        public static EUVehicleCategory  O3                 { get; }
            = Register("o3");

        /// <summary>
        /// Trailers with a maximum mass exceeding 10 tonnes.
        /// </summary>
        public static EUVehicleCategory  O4                 { get; }
            = Register("o4");

        /// <summary>
        /// Some other vehicle category.
        /// </summary>
        public static EUVehicleCategory  Other              { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (EUVehicleCategory1, EUVehicleCategory2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EUVehicleCategory1">A EUVehicleCategory.</param>
        /// <param name="EUVehicleCategory2">Another EUVehicleCategory.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (EUVehicleCategory EUVehicleCategory1,
                                           EUVehicleCategory EUVehicleCategory2)

            => EUVehicleCategory1.Equals(EUVehicleCategory2);

        #endregion

        #region Operator != (EUVehicleCategory1, EUVehicleCategory2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EUVehicleCategory1">A EUVehicleCategory.</param>
        /// <param name="EUVehicleCategory2">Another EUVehicleCategory.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (EUVehicleCategory EUVehicleCategory1,
                                           EUVehicleCategory EUVehicleCategory2)

            => !EUVehicleCategory1.Equals(EUVehicleCategory2);

        #endregion

        #region Operator <  (EUVehicleCategory1, EUVehicleCategory2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EUVehicleCategory1">A EUVehicleCategory.</param>
        /// <param name="EUVehicleCategory2">Another EUVehicleCategory.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (EUVehicleCategory EUVehicleCategory1,
                                          EUVehicleCategory EUVehicleCategory2)

            => EUVehicleCategory1.CompareTo(EUVehicleCategory2) < 0;

        #endregion

        #region Operator <= (EUVehicleCategory1, EUVehicleCategory2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EUVehicleCategory1">A EUVehicleCategory.</param>
        /// <param name="EUVehicleCategory2">Another EUVehicleCategory.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (EUVehicleCategory EUVehicleCategory1,
                                           EUVehicleCategory EUVehicleCategory2)

            => EUVehicleCategory1.CompareTo(EUVehicleCategory2) <= 0;

        #endregion

        #region Operator >  (EUVehicleCategory1, EUVehicleCategory2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EUVehicleCategory1">A EUVehicleCategory.</param>
        /// <param name="EUVehicleCategory2">Another EUVehicleCategory.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (EUVehicleCategory EUVehicleCategory1,
                                          EUVehicleCategory EUVehicleCategory2)

            => EUVehicleCategory1.CompareTo(EUVehicleCategory2) > 0;

        #endregion

        #region Operator >= (EUVehicleCategory1, EUVehicleCategory2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EUVehicleCategory1">A EUVehicleCategory.</param>
        /// <param name="EUVehicleCategory2">Another EUVehicleCategory.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (EUVehicleCategory EUVehicleCategory1,
                                           EUVehicleCategory EUVehicleCategory2)

            => EUVehicleCategory1.CompareTo(EUVehicleCategory2) >= 0;

        #endregion

        #endregion

        #region IComparable<EUVehicleCategory> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two EUVehicleCategories.
        /// </summary>
        /// <param name="Object">A EUVehicleCategory to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is EUVehicleCategory euVehicleCategory
                   ? CompareTo(euVehicleCategory)
                   : throw new ArgumentException("The given object is not a EUVehicleCategory!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(EUVehicleCategory)

        /// <summary>
        /// Compares two EUVehicleCategories.
        /// </summary>
        /// <param name="EUVehicleCategory">A EUVehicleCategory to compare with.</param>
        public Int32 CompareTo(EUVehicleCategory EUVehicleCategory)

            => String.Compare(InternalId,
                              EUVehicleCategory.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<EUVehicleCategory> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two EUVehicleCategories for equality.
        /// </summary>
        /// <param name="Object">A EUVehicleCategory to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is EUVehicleCategory euVehicleCategory &&
                   Equals(euVehicleCategory);

        #endregion

        #region Equals(EUVehicleCategory)

        /// <summary>
        /// Compares two EUVehicleCategories for equality.
        /// </summary>
        /// <param name="EUVehicleCategory">A EUVehicleCategory to compare with.</param>
        public Boolean Equals(EUVehicleCategory EUVehicleCategory)

            => String.Equals(InternalId,
                             EUVehicleCategory.InternalId,
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
