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
    /// Extension methods for ChargingPointUsages.
    /// </summary>
    public static class ChargingPointUsageExtensions
    {

        /// <summary>
        /// Indicates whether this ChargingPointUsage is null or empty.
        /// </summary>
        /// <param name="ChargingPointUsage">A ChargingPointUsage.</param>
        public static Boolean IsNullOrEmpty(this ChargingPointUsage? ChargingPointUsage)
            => !ChargingPointUsage.HasValue || ChargingPointUsage.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this ChargingPointUsage is null or empty.
        /// </summary>
        /// <param name="ChargingPointUsage">A ChargingPointUsage.</param>
        public static Boolean IsNotNullOrEmpty(this ChargingPointUsage? ChargingPointUsage)
            => ChargingPointUsage.HasValue && ChargingPointUsage.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A ChargingPointUsage.
    /// </summary>
    public readonly struct ChargingPointUsage : IId,
                                                IEquatable<ChargingPointUsage>,
                                                IComparable<ChargingPointUsage>
    {

        #region Data

        private readonly static Dictionary<String, ChargingPointUsage>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this ChargingPointUsage is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this ChargingPointUsage is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the ChargingPointUsage.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered ChargingPointUsages.
        /// </summary>
        public static    IEnumerable<ChargingPointUsage>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ChargingPointUsage based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a ChargingPointUsage.</param>
        private ChargingPointUsage(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ChargingPointUsage Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ChargingPointUsage(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a ChargingPointUsage.
        /// </summary>
        /// <param name="Text">A text representation of a ChargingPointUsage.</param>
        public static ChargingPointUsage Parse(String Text)
        {

            if (TryParse(Text, out var chargingPointUsage))
                return chargingPointUsage;

            throw new ArgumentException($"Invalid text representation of a ChargingPointUsage: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a ChargingPointUsage.
        /// </summary>
        /// <param name="Text">A text representation of a ChargingPointUsage.</param>
        public static ChargingPointUsage? TryParse(String Text)
        {

            if (TryParse(Text, out var chargingPointUsage))
                return chargingPointUsage;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ChargingPointUsage)

        /// <summary>
        /// Try to parse the given text as a ChargingPointUsage.
        /// </summary>
        /// <param name="Text">A text representation of a ChargingPointUsage.</param>
        /// <param name="ChargingPointUsage">The parsed ChargingPointUsage.</param>
        public static Boolean TryParse(String Text, out ChargingPointUsage ChargingPointUsage)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ChargingPointUsage))
                    ChargingPointUsage = Register(Text);

                return true;

            }

            ChargingPointUsage = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this ChargingPointUsage.
        /// </summary>
        public ChargingPointUsage Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Charging of electric boats.
        /// </summary>
        public static ChargingPointUsage  ElectricBoat                  { get; }
            = Register("electricBoat");

        /// <summary>
        /// Charging of E-Bikes.
        /// </summary>
        public static ChargingPointUsage  ElectricBike                  { get; }
            = Register("electricBike");

        /// <summary>
        /// Provides a plug for electrical devices (e.g. shaver, mobile phones, hair dryer, ...).
        /// </summary>
        public static ChargingPointUsage  ElectricalDevices             { get; }
            = Register("electricalDevices");

        /// <summary>
        /// Charging of E-Motorcycles.
        /// </summary>
        public static ChargingPointUsage  ElectricMotorcycle            { get; }
            = Register("electricMotorcycle");

        /// <summary>
        /// Charging of electric vehicles.
        /// </summary>
        public static ChargingPointUsage  ElectricVehicle               { get; }
            = Register("electricVehicle");

        /// <summary>
        /// Supply for lorries with power consumption, e.g. for refrigerated goods transports.
        /// </summary>
        public static ChargingPointUsage  LorryPowerConsumption         { get; }
            = Register("lorryPowerConsumption");

        /// <summary>
        /// Supply for motorhomes or caravans.
        /// </summary>
        public static ChargingPointUsage  MotorhomeOrCaravanSupply      { get; }
            = Register("motorhomeOrCaravanSupply");

        /// <summary>
        /// The charging point supplies an overhead line, usually connected via pantographs.
        /// </summary>
        public static ChargingPointUsage  OverheadLineDrivenVehicles    { get; }
            = Register("overheadLineDrivenVehicles");

        /// <summary>
        /// Other usage for the electric charging stations.
        /// </summary>
        public static ChargingPointUsage  Other                         { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (ChargingPointUsage1, ChargingPointUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingPointUsage1">A ChargingPointUsage.</param>
        /// <param name="ChargingPointUsage2">Another ChargingPointUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ChargingPointUsage ChargingPointUsage1,
                                           ChargingPointUsage ChargingPointUsage2)

            => ChargingPointUsage1.Equals(ChargingPointUsage2);

        #endregion

        #region Operator != (ChargingPointUsage1, ChargingPointUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingPointUsage1">A ChargingPointUsage.</param>
        /// <param name="ChargingPointUsage2">Another ChargingPointUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ChargingPointUsage ChargingPointUsage1,
                                           ChargingPointUsage ChargingPointUsage2)

            => !ChargingPointUsage1.Equals(ChargingPointUsage2);

        #endregion

        #region Operator <  (ChargingPointUsage1, ChargingPointUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingPointUsage1">A ChargingPointUsage.</param>
        /// <param name="ChargingPointUsage2">Another ChargingPointUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ChargingPointUsage ChargingPointUsage1,
                                          ChargingPointUsage ChargingPointUsage2)

            => ChargingPointUsage1.CompareTo(ChargingPointUsage2) < 0;

        #endregion

        #region Operator <= (ChargingPointUsage1, ChargingPointUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingPointUsage1">A ChargingPointUsage.</param>
        /// <param name="ChargingPointUsage2">Another ChargingPointUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ChargingPointUsage ChargingPointUsage1,
                                           ChargingPointUsage ChargingPointUsage2)

            => ChargingPointUsage1.CompareTo(ChargingPointUsage2) <= 0;

        #endregion

        #region Operator >  (ChargingPointUsage1, ChargingPointUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingPointUsage1">A ChargingPointUsage.</param>
        /// <param name="ChargingPointUsage2">Another ChargingPointUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ChargingPointUsage ChargingPointUsage1,
                                          ChargingPointUsage ChargingPointUsage2)

            => ChargingPointUsage1.CompareTo(ChargingPointUsage2) > 0;

        #endregion

        #region Operator >= (ChargingPointUsage1, ChargingPointUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingPointUsage1">A ChargingPointUsage.</param>
        /// <param name="ChargingPointUsage2">Another ChargingPointUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ChargingPointUsage ChargingPointUsage1,
                                           ChargingPointUsage ChargingPointUsage2)

            => ChargingPointUsage1.CompareTo(ChargingPointUsage2) >= 0;

        #endregion

        #endregion

        #region IComparable<ChargingPointUsage> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two ChargingPointUsages.
        /// </summary>
        /// <param name="Object">A ChargingPointUsage to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ChargingPointUsage chargingPointUsage
                   ? CompareTo(chargingPointUsage)
                   : throw new ArgumentException("The given object is not a ChargingPointUsage!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ChargingPointUsage)

        /// <summary>
        /// Compares two ChargingPointUsages.
        /// </summary>
        /// <param name="ChargingPointUsage">A ChargingPointUsage to compare with.</param>
        public Int32 CompareTo(ChargingPointUsage ChargingPointUsage)

            => String.Compare(InternalId,
                              ChargingPointUsage.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<ChargingPointUsage> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two ChargingPointUsages for equality.
        /// </summary>
        /// <param name="Object">A ChargingPointUsage to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ChargingPointUsage chargingPointUsage &&
                   Equals(chargingPointUsage);

        #endregion

        #region Equals(ChargingPointUsage)

        /// <summary>
        /// Compares two ChargingPointUsages for equality.
        /// </summary>
        /// <param name="ChargingPointUsage">A ChargingPointUsage to compare with.</param>
        public Boolean Equals(ChargingPointUsage ChargingPointUsage)

            => String.Equals(InternalId,
                             ChargingPointUsage.InternalId,
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
