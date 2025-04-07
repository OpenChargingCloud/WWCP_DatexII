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
    /// Extension methods for LowEmissionLevels.
    /// </summary>
    public static class LowEmissionLevelExtensions
    {

        /// <summary>
        /// Indicates whether this LowEmissionLevel is null or empty.
        /// </summary>
        /// <param name="LowEmissionLevel">A LowEmissionLevel.</param>
        public static Boolean IsNullOrEmpty(this LowEmissionLevel? LowEmissionLevel)
            => !LowEmissionLevel.HasValue || LowEmissionLevel.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this LowEmissionLevel is null or empty.
        /// </summary>
        /// <param name="LowEmissionLevel">A LowEmissionLevel.</param>
        public static Boolean IsNotNullOrEmpty(this LowEmissionLevel? LowEmissionLevel)
            => LowEmissionLevel.HasValue && LowEmissionLevel.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A LowEmissionLevel.
    /// </summary>
    public readonly struct LowEmissionLevel : IId,
                                              IEquatable<LowEmissionLevel>,
                                              IComparable<LowEmissionLevel>
    {

        #region Data

        private readonly static Dictionary<String, LowEmissionLevel>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this LowEmissionLevel is null or empty.
        /// </summary>
        public readonly  Boolean                IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this LowEmissionLevel is NOT null or empty.
        /// </summary>
        public readonly  Boolean                IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the LowEmissionLevel.
        /// </summary>
        public readonly  UInt64                 Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<LowEmissionLevel>  AllLowEmissionLevels
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new LowEmissionLevel based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a LowEmissionLevel.</param>
        private LowEmissionLevel(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static LowEmissionLevel Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new LowEmissionLevel(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a LowEmissionLevel.
        /// </summary>
        /// <param name="Text">A text representation of a LowEmissionLevel.</param>
        public static LowEmissionLevel Parse(String Text)
        {

            if (TryParse(Text, out var lowEmissionLevel))
                return lowEmissionLevel;

            throw new ArgumentException($"Invalid text representation of a LowEmissionLevel: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a LowEmissionLevel.
        /// </summary>
        /// <param name="Text">A text representation of a LowEmissionLevel.</param>
        public static LowEmissionLevel? TryParse(String Text)
        {

            if (TryParse(Text, out var lowEmissionLevel))
                return lowEmissionLevel;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out LowEmissionLevel)

        /// <summary>
        /// Try to parse the given text as LowEmissionLevel.
        /// </summary>
        /// <param name="Text">A text representation of a LowEmissionLevel.</param>
        /// <param name="LowEmissionLevel">The parsed LowEmissionLevel.</param>
        public static Boolean TryParse(String Text, out LowEmissionLevel LowEmissionLevel)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out LowEmissionLevel))
                    LowEmissionLevel = Register(Text);

                return true;

            }

            LowEmissionLevel = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this LowEmissionLevel.
        /// </summary>
        public LowEmissionLevel Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Vehicles with low level emission.
        /// </summary>
        public static LowEmissionLevel  LowLevelEmission    { get; }
            = Register("lowLevelEmission");

        /// <summary>
        /// Only vehicles that do not produce emissions (e.g. electric driven).
        /// Hybrid driven cars are allowed, when they switch to emission free mode within the considered situation.
        /// </summary>
        public static LowEmissionLevel  FreeOfEmission      { get; }
            = Register("freeOfEmission");

        #endregion


        #region Operator overloading

        #region Operator == (LowEmissionLevel1, LowEmissionLevel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LowEmissionLevel1">A LowEmissionLevel.</param>
        /// <param name="LowEmissionLevel2">Another LowEmissionLevel.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (LowEmissionLevel LowEmissionLevel1,
                                           LowEmissionLevel LowEmissionLevel2)

            => LowEmissionLevel1.Equals(LowEmissionLevel2);

        #endregion

        #region Operator != (LowEmissionLevel1, LowEmissionLevel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LowEmissionLevel1">A LowEmissionLevel.</param>
        /// <param name="LowEmissionLevel2">Another LowEmissionLevel.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (LowEmissionLevel LowEmissionLevel1,
                                           LowEmissionLevel LowEmissionLevel2)

            => !LowEmissionLevel1.Equals(LowEmissionLevel2);

        #endregion

        #region Operator <  (LowEmissionLevel1, LowEmissionLevel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LowEmissionLevel1">A LowEmissionLevel.</param>
        /// <param name="LowEmissionLevel2">Another LowEmissionLevel.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (LowEmissionLevel LowEmissionLevel1,
                                          LowEmissionLevel LowEmissionLevel2)

            => LowEmissionLevel1.CompareTo(LowEmissionLevel2) < 0;

        #endregion

        #region Operator <= (LowEmissionLevel1, LowEmissionLevel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LowEmissionLevel1">A LowEmissionLevel.</param>
        /// <param name="LowEmissionLevel2">Another LowEmissionLevel.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (LowEmissionLevel LowEmissionLevel1,
                                           LowEmissionLevel LowEmissionLevel2)

            => LowEmissionLevel1.CompareTo(LowEmissionLevel2) <= 0;

        #endregion

        #region Operator >  (LowEmissionLevel1, LowEmissionLevel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LowEmissionLevel1">A LowEmissionLevel.</param>
        /// <param name="LowEmissionLevel2">Another LowEmissionLevel.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (LowEmissionLevel LowEmissionLevel1,
                                          LowEmissionLevel LowEmissionLevel2)

            => LowEmissionLevel1.CompareTo(LowEmissionLevel2) > 0;

        #endregion

        #region Operator >= (LowEmissionLevel1, LowEmissionLevel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="LowEmissionLevel1">A LowEmissionLevel.</param>
        /// <param name="LowEmissionLevel2">Another LowEmissionLevel.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (LowEmissionLevel LowEmissionLevel1,
                                           LowEmissionLevel LowEmissionLevel2)

            => LowEmissionLevel1.CompareTo(LowEmissionLevel2) >= 0;

        #endregion

        #endregion

        #region IComparable<LowEmissionLevel> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two LowEmissionLevels.
        /// </summary>
        /// <param name="Object">LowEmissionLevel to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is LowEmissionLevel lowEmissionLevel
                   ? CompareTo(lowEmissionLevel)
                   : throw new ArgumentException("The given object is not LowEmissionLevel!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(LowEmissionLevel)

        /// <summary>
        /// Compares two LowEmissionLevels.
        /// </summary>
        /// <param name="LowEmissionLevel">LowEmissionLevel to compare with.</param>
        public Int32 CompareTo(LowEmissionLevel LowEmissionLevel)

            => String.Compare(InternalId,
                              LowEmissionLevel.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<LowEmissionLevel> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two LowEmissionLevels for equality.
        /// </summary>
        /// <param name="Object">LowEmissionLevel to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is LowEmissionLevel lowEmissionLevel &&
                   Equals(lowEmissionLevel);

        #endregion

        #region Equals(LowEmissionLevel)

        /// <summary>
        /// Compares two LowEmissionLevels for equality.
        /// </summary>
        /// <param name="LowEmissionLevel">LowEmissionLevel to compare with.</param>
        public Boolean Equals(LowEmissionLevel LowEmissionLevel)

            => String.Equals(InternalId,
                             LowEmissionLevel.InternalId,
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
