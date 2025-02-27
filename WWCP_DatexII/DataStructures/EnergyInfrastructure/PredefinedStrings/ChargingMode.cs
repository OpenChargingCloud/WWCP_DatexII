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
    /// Extension methods for ChargingModes.
    /// </summary>
    public static class ChargingModeExtensions
    {

        /// <summary>
        /// Indicates whether this ChargingMode is null or empty.
        /// </summary>
        /// <param name="ChargingMode">A ChargingMode.</param>
        public static Boolean IsNullOrEmpty(this ChargingMode? ChargingMode)
            => !ChargingMode.HasValue || ChargingMode.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this ChargingMode is null or empty.
        /// </summary>
        /// <param name="ChargingMode">A ChargingMode.</param>
        public static Boolean IsNotNullOrEmpty(this ChargingMode? ChargingMode)
            => ChargingMode.HasValue && ChargingMode.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A ChargingMode.
    /// </summary>
    public readonly struct ChargingMode : IId,
                                          IEquatable<ChargingMode>,
                                          IComparable<ChargingMode>
    {

        #region Data

        private readonly static Dictionary<String, ChargingMode>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this ChargingMode is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this ChargingMode is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the ChargingMode.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered ChargingModes.
        /// </summary>
        public static    IEnumerable<ChargingMode>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ChargingMode based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a ChargingMode.</param>
        private ChargingMode(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ChargingMode Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ChargingMode(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a ChargingMode.
        /// </summary>
        /// <param name="Text">A text representation of a ChargingMode.</param>
        public static ChargingMode Parse(String Text)
        {

            if (TryParse(Text, out var chargingMode))
                return chargingMode;

            throw new ArgumentException($"Invalid text representation of a ChargingMode: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a ChargingMode.
        /// </summary>
        /// <param name="Text">A text representation of a ChargingMode.</param>
        public static ChargingMode? TryParse(String Text)
        {

            if (TryParse(Text, out var chargingMode))
                return chargingMode;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ChargingMode)

        /// <summary>
        /// Try to parse the given text as a ChargingMode.
        /// </summary>
        /// <param name="Text">A text representation of a ChargingMode.</param>
        /// <param name="ChargingMode">The parsed ChargingMode.</param>
        public static Boolean TryParse(String Text, out ChargingMode ChargingMode)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ChargingMode))
                    ChargingMode = Register(Text);

                return true;

            }

            ChargingMode = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this ChargingMode.
        /// </summary>
        public ChargingMode Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Mode 1, AC 1 phase.
        /// </summary>
        public static ChargingMode  Mode1AC1p          { get; }
            = Register("mode1AC1p");

        /// <summary>
        /// Mode 1, AC 3 phases.
        /// </summary>
        public static ChargingMode  Mode1AC3p          { get; }
            = Register("mode1AC3p");

        /// <summary>
        /// Mode 2, AC 1 phase.
        /// </summary>
        public static ChargingMode  Mode2AC1p          { get; }
            = Register("mode2AC1p");

        /// <summary>
        /// Mode 2, AC 3 phases.
        /// </summary>
        public static ChargingMode  Mode2AC3p          { get; }
            = Register("mode2AC3p");

        /// <summary>
        /// Mode 3, AC 3 phases.
        /// </summary>
        public static ChargingMode  Mode3AC3p          { get; }
            = Register("mode3AC3p");

        /// <summary>
        /// Mode 4, DC.
        /// </summary>
        public static ChargingMode  Mode4DC            { get; }
            = Register("mode4DC");

        /// <summary>
        /// Legacy-Inductive.
        /// </summary>
        public static ChargingMode  LegacyInductive    { get; }
            = Register("legacyInductive");

        /// <summary>
        /// Charging with a combined charging solution (CCS). AC and DC are used simultaneously.
        /// </summary>
        public static ChargingMode  CCS                { get; }
            = Register("ccs");

        /// <summary>
        /// Some other charging mode.
        /// </summary>
        public static ChargingMode  Other              { get; }
            = Register("other");

        /// <summary>
        /// The type of the charging mode is unknown.
        /// </summary>
        public static ChargingMode  Unknown            { get; }
            = Register("unknown");

        #endregion


        #region Operator overloading

        #region Operator == (ChargingMode1, ChargingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingMode1">A ChargingMode.</param>
        /// <param name="ChargingMode2">Another ChargingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ChargingMode ChargingMode1,
                                           ChargingMode ChargingMode2)

            => ChargingMode1.Equals(ChargingMode2);

        #endregion

        #region Operator != (ChargingMode1, ChargingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingMode1">A ChargingMode.</param>
        /// <param name="ChargingMode2">Another ChargingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ChargingMode ChargingMode1,
                                           ChargingMode ChargingMode2)

            => !ChargingMode1.Equals(ChargingMode2);

        #endregion

        #region Operator <  (ChargingMode1, ChargingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingMode1">A ChargingMode.</param>
        /// <param name="ChargingMode2">Another ChargingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ChargingMode ChargingMode1,
                                          ChargingMode ChargingMode2)

            => ChargingMode1.CompareTo(ChargingMode2) < 0;

        #endregion

        #region Operator <= (ChargingMode1, ChargingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingMode1">A ChargingMode.</param>
        /// <param name="ChargingMode2">Another ChargingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ChargingMode ChargingMode1,
                                           ChargingMode ChargingMode2)

            => ChargingMode1.CompareTo(ChargingMode2) <= 0;

        #endregion

        #region Operator >  (ChargingMode1, ChargingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingMode1">A ChargingMode.</param>
        /// <param name="ChargingMode2">Another ChargingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ChargingMode ChargingMode1,
                                          ChargingMode ChargingMode2)

            => ChargingMode1.CompareTo(ChargingMode2) > 0;

        #endregion

        #region Operator >= (ChargingMode1, ChargingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ChargingMode1">A ChargingMode.</param>
        /// <param name="ChargingMode2">Another ChargingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ChargingMode ChargingMode1,
                                           ChargingMode ChargingMode2)

            => ChargingMode1.CompareTo(ChargingMode2) >= 0;

        #endregion

        #endregion

        #region IComparable<ChargingMode> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two ChargingModes.
        /// </summary>
        /// <param name="Object">A ChargingMode to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ChargingMode chargingMode
                   ? CompareTo(chargingMode)
                   : throw new ArgumentException("The given object is not a ChargingMode!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ChargingMode)

        /// <summary>
        /// Compares two ChargingModes.
        /// </summary>
        /// <param name="ChargingMode">A ChargingMode to compare with.</param>
        public Int32 CompareTo(ChargingMode ChargingMode)

            => String.Compare(InternalId,
                              ChargingMode.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<ChargingMode> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two ChargingModes for equality.
        /// </summary>
        /// <param name="Object">A ChargingMode to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ChargingMode chargingMode &&
                   Equals(chargingMode);

        #endregion

        #region Equals(ChargingMode)

        /// <summary>
        /// Compares two ChargingModes for equality.
        /// </summary>
        /// <param name="ChargingMode">A ChargingMode to compare with.</param>
        public Boolean Equals(ChargingMode ChargingMode)

            => String.Equals(InternalId,
                             ChargingMode.InternalId,
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
