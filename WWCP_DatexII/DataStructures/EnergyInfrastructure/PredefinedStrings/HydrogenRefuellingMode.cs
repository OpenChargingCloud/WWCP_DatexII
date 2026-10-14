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

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Extension methods for HydrogenRefuellingModes.
    /// </summary>
    public static class HydrogenRefuellingModeExtensions
    {

        /// <summary>
        /// Indicates whether this HydrogenRefuellingMode is null or empty.
        /// </summary>
        /// <param name="HydrogenRefuellingMode">A HydrogenRefuellingMode.</param>
        public static Boolean IsNullOrEmpty(this HydrogenRefuellingMode? HydrogenRefuellingMode)
            => !HydrogenRefuellingMode.HasValue || HydrogenRefuellingMode.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this HydrogenRefuellingMode is null or empty.
        /// </summary>
        /// <param name="HydrogenRefuellingMode">A HydrogenRefuellingMode.</param>
        public static Boolean IsNotNullOrEmpty(this HydrogenRefuellingMode? HydrogenRefuellingMode)
            => HydrogenRefuellingMode.HasValue && HydrogenRefuellingMode.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// The refuelling mode of a hydrogen station (DATEX II 3.7: aegi:HydrogenRefuellingModeEnum).
    /// </summary>
    public readonly struct HydrogenRefuellingMode : IId,
                                        IEquatable<HydrogenRefuellingMode>,
                                        IComparable<HydrogenRefuellingMode>
    {

        #region Data

        private readonly static Dictionary<String, HydrogenRefuellingMode>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this HydrogenRefuellingMode is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this HydrogenRefuellingMode is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the HydrogenRefuellingMode.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<HydrogenRefuellingMode>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new HydrogenRefuellingMode based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a HydrogenRefuellingMode.</param>
        private HydrogenRefuellingMode(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static HydrogenRefuellingMode Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new HydrogenRefuellingMode(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a HydrogenRefuellingMode.
        /// </summary>
        /// <param name="Text">A text representation of a HydrogenRefuellingMode.</param>
        public static HydrogenRefuellingMode Parse(String Text)
        {

            if (TryParse(Text, out var hydrogenRefuellingMode))
                return hydrogenRefuellingMode;

            throw new ArgumentException($"Invalid text representation of a HydrogenRefuellingMode: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a HydrogenRefuellingMode.
        /// </summary>
        /// <param name="Text">A text representation of a HydrogenRefuellingMode.</param>
        public static HydrogenRefuellingMode? TryParse(String Text)
        {

            if (TryParse(Text, out var hydrogenRefuellingMode))
                return hydrogenRefuellingMode;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out HydrogenRefuellingMode)

        /// <summary>
        /// Try to parse the given text as HydrogenRefuellingMode.
        /// </summary>
        /// <param name="Text">A text representation of a HydrogenRefuellingMode.</param>
        /// <param name="HydrogenRefuellingMode">The parsed HydrogenRefuellingMode.</param>
        public static Boolean TryParse(String Text, out HydrogenRefuellingMode HydrogenRefuellingMode)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out HydrogenRefuellingMode))
                    HydrogenRefuellingMode = Register(Text);

                return true;

            }

            HydrogenRefuellingMode = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this HydrogenRefuellingMode.
        /// </summary>
        public HydrogenRefuellingMode Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Gaseous hydrogen at 350 bar for cars.
        /// </summary>
        public static HydrogenRefuellingMode  GaseousHydrogen350barCar    { get; }
            = Register("gaseousHydrogen350barCar");

        /// <summary>
        /// Gaseous hydrogen at 350 bar for trucks.
        /// </summary>
        public static HydrogenRefuellingMode  GaseousHydrogen350barTruck    { get; }
            = Register("gaseousHydrogen350barTruck");

        /// <summary>
        /// Gaseous hydrogen at 700 bar for cars.
        /// </summary>
        public static HydrogenRefuellingMode  GaseousHydrogen700barCar    { get; }
            = Register("gaseousHydrogen700barCar");

        /// <summary>
        /// Gaseous hydrogen at 700 bar for trucks.
        /// </summary>
        public static HydrogenRefuellingMode  GaseousHydrogen700barTruck    { get; }
            = Register("gaseousHydrogen700barTruck");

        /// <summary>
        /// Liquid hydrogen.
        /// </summary>
        public static HydrogenRefuellingMode  LiquidHydrogen    { get; }
            = Register("liquidHydrogen");

        /// <summary>
        /// Cryo-compressed hydrogen (the schema spells it 'cryoCompressedHydrogren').
        /// </summary>
        public static HydrogenRefuellingMode  CryoCompressedHydrogen    { get; }
            = Register("cryoCompressedHydrogren");

        /// <summary>
        /// Unknown.
        /// </summary>
        public static HydrogenRefuellingMode  Unknown    { get; }
            = Register("unknown");

        /// <summary>
        /// Other.
        /// </summary>
        public static HydrogenRefuellingMode  Other    { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (HydrogenRefuellingMode1, HydrogenRefuellingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HydrogenRefuellingMode1">A HydrogenRefuellingMode.</param>
        /// <param name="HydrogenRefuellingMode2">Another HydrogenRefuellingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (HydrogenRefuellingMode HydrogenRefuellingMode1,
                                           HydrogenRefuellingMode HydrogenRefuellingMode2)

            => HydrogenRefuellingMode1.Equals(HydrogenRefuellingMode2);

        #endregion

        #region Operator != (HydrogenRefuellingMode1, HydrogenRefuellingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HydrogenRefuellingMode1">A HydrogenRefuellingMode.</param>
        /// <param name="HydrogenRefuellingMode2">Another HydrogenRefuellingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (HydrogenRefuellingMode HydrogenRefuellingMode1,
                                           HydrogenRefuellingMode HydrogenRefuellingMode2)

            => !HydrogenRefuellingMode1.Equals(HydrogenRefuellingMode2);

        #endregion

        #region Operator <  (HydrogenRefuellingMode1, HydrogenRefuellingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HydrogenRefuellingMode1">A HydrogenRefuellingMode.</param>
        /// <param name="HydrogenRefuellingMode2">Another HydrogenRefuellingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (HydrogenRefuellingMode HydrogenRefuellingMode1,
                                          HydrogenRefuellingMode HydrogenRefuellingMode2)

            => HydrogenRefuellingMode1.CompareTo(HydrogenRefuellingMode2) < 0;

        #endregion

        #region Operator <= (HydrogenRefuellingMode1, HydrogenRefuellingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HydrogenRefuellingMode1">A HydrogenRefuellingMode.</param>
        /// <param name="HydrogenRefuellingMode2">Another HydrogenRefuellingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (HydrogenRefuellingMode HydrogenRefuellingMode1,
                                           HydrogenRefuellingMode HydrogenRefuellingMode2)

            => HydrogenRefuellingMode1.CompareTo(HydrogenRefuellingMode2) <= 0;

        #endregion

        #region Operator >  (HydrogenRefuellingMode1, HydrogenRefuellingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HydrogenRefuellingMode1">A HydrogenRefuellingMode.</param>
        /// <param name="HydrogenRefuellingMode2">Another HydrogenRefuellingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (HydrogenRefuellingMode HydrogenRefuellingMode1,
                                          HydrogenRefuellingMode HydrogenRefuellingMode2)

            => HydrogenRefuellingMode1.CompareTo(HydrogenRefuellingMode2) > 0;

        #endregion

        #region Operator >= (HydrogenRefuellingMode1, HydrogenRefuellingMode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HydrogenRefuellingMode1">A HydrogenRefuellingMode.</param>
        /// <param name="HydrogenRefuellingMode2">Another HydrogenRefuellingMode.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (HydrogenRefuellingMode HydrogenRefuellingMode1,
                                           HydrogenRefuellingMode HydrogenRefuellingMode2)

            => HydrogenRefuellingMode1.CompareTo(HydrogenRefuellingMode2) >= 0;

        #endregion

        #endregion

        #region IComparable<HydrogenRefuellingMode> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two HydrogenRefuellingModes.
        /// </summary>
        /// <param name="Object">HydrogenRefuellingMode to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is HydrogenRefuellingMode HydrogenRefuellingMode
                   ? CompareTo(HydrogenRefuellingMode)
                   : throw new ArgumentException("The given object is not HydrogenRefuellingMode!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(HydrogenRefuellingMode)

        /// <summary>
        /// Compares two HydrogenRefuellingModes.
        /// </summary>
        /// <param name="HydrogenRefuellingMode">HydrogenRefuellingMode to compare with.</param>
        public Int32 CompareTo(HydrogenRefuellingMode HydrogenRefuellingMode)

            => String.Compare(InternalId,
                              HydrogenRefuellingMode.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<HydrogenRefuellingMode> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two HydrogenRefuellingModes for equality.
        /// </summary>
        /// <param name="Object">HydrogenRefuellingMode to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is HydrogenRefuellingMode HydrogenRefuellingMode &&
                   Equals(HydrogenRefuellingMode);

        #endregion

        #region Equals(HydrogenRefuellingMode)

        /// <summary>
        /// Compares two HydrogenRefuellingModes for equality.
        /// </summary>
        /// <param name="HydrogenRefuellingMode">HydrogenRefuellingMode to compare with.</param>
        public Boolean Equals(HydrogenRefuellingMode HydrogenRefuellingMode)

            => String.Equals(InternalId,
                             HydrogenRefuellingMode.InternalId,
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
