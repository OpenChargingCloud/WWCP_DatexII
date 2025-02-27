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
    /// Extension methods for DeliveryUnit.
    /// </summary>
    public static class DeliveryUnitExtensions
    {

        /// <summary>
        /// Indicates whether this DeliveryUnit is null or empty.
        /// </summary>
        /// <param name="DeliveryUnit">A DeliveryUnit.</param>
        public static Boolean IsNullOrEmpty(this DeliveryUnit? DeliveryUnit)
            => !DeliveryUnit.HasValue || DeliveryUnit.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this DeliveryUnit is null or empty.
        /// </summary>
        /// <param name="DeliveryUnit">A DeliveryUnit.</param>
        public static Boolean IsNotNullOrEmpty(this DeliveryUnit? DeliveryUnit)
            => DeliveryUnit.HasValue && DeliveryUnit.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A DeliveryUnit.
    /// </summary>
    public readonly struct DeliveryUnit : IId,
                                          IEquatable<DeliveryUnit>,
                                          IComparable<DeliveryUnit>
    {

        #region Data

        private readonly static Dictionary<String, DeliveryUnit>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this DeliveryUnit is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this DeliveryUnit is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the DeliveryUnit.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered DeliveryUnits.
        /// </summary>
        public static    IEnumerable<DeliveryUnit>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new DeliveryUnit based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a DeliveryUnit.</param>
        private DeliveryUnit(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static DeliveryUnit Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new DeliveryUnit(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a DeliveryUnit.
        /// </summary>
        /// <param name="Text">A text representation of a DeliveryUnit.</param>
        public static DeliveryUnit Parse(String Text)
        {

            if (TryParse(Text, out var deliveryUnit))
                return deliveryUnit;

            throw new ArgumentException($"Invalid text representation of a DeliveryUnit: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a DeliveryUnit.
        /// </summary>
        /// <param name="Text">A text representation of a DeliveryUnit.</param>
        public static DeliveryUnit? TryParse(String Text)
        {

            if (TryParse(Text, out var deliveryUnit))
                return deliveryUnit;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out DeliveryUnit)

        /// <summary>
        /// Try to parse the given text as a DeliveryUnit.
        /// </summary>
        /// <param name="Text">A text representation of a DeliveryUnit.</param>
        /// <param name="DeliveryUnit">The parsed DeliveryUnit.</param>
        public static Boolean TryParse(String Text, out DeliveryUnit DeliveryUnit)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out DeliveryUnit))
                    DeliveryUnit = Register(Text);

                return true;

            }

            DeliveryUnit = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this DeliveryUnit.
        /// </summary>
        public DeliveryUnit Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

#pragma warning disable IDE1006 // Naming Styles

        /// <summary>
        /// kWh
        /// </summary>
        public static DeliveryUnit  kWh    { get; }
            = Register("kWh");

#pragma warning restore IDE1006 // Naming Styles

        #endregion


        #region Operator overloading

        #region Operator == (DeliveryUnit1, DeliveryUnit2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="DeliveryUnit1">A DeliveryUnit.</param>
        /// <param name="DeliveryUnit2">Another DeliveryUnit.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (DeliveryUnit DeliveryUnit1,
                                           DeliveryUnit DeliveryUnit2)

            => DeliveryUnit1.Equals(DeliveryUnit2);

        #endregion

        #region Operator != (DeliveryUnit1, DeliveryUnit2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="DeliveryUnit1">A DeliveryUnit.</param>
        /// <param name="DeliveryUnit2">Another DeliveryUnit.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (DeliveryUnit DeliveryUnit1,
                                           DeliveryUnit DeliveryUnit2)

            => !DeliveryUnit1.Equals(DeliveryUnit2);

        #endregion

        #region Operator <  (DeliveryUnit1, DeliveryUnit2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="DeliveryUnit1">A DeliveryUnit.</param>
        /// <param name="DeliveryUnit2">Another DeliveryUnit.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (DeliveryUnit DeliveryUnit1,
                                          DeliveryUnit DeliveryUnit2)

            => DeliveryUnit1.CompareTo(DeliveryUnit2) < 0;

        #endregion

        #region Operator <= (DeliveryUnit1, DeliveryUnit2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="DeliveryUnit1">A DeliveryUnit.</param>
        /// <param name="DeliveryUnit2">Another DeliveryUnit.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (DeliveryUnit DeliveryUnit1,
                                           DeliveryUnit DeliveryUnit2)

            => DeliveryUnit1.CompareTo(DeliveryUnit2) <= 0;

        #endregion

        #region Operator >  (DeliveryUnit1, DeliveryUnit2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="DeliveryUnit1">A DeliveryUnit.</param>
        /// <param name="DeliveryUnit2">Another DeliveryUnit.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (DeliveryUnit DeliveryUnit1,
                                          DeliveryUnit DeliveryUnit2)

            => DeliveryUnit1.CompareTo(DeliveryUnit2) > 0;

        #endregion

        #region Operator >= (DeliveryUnit1, DeliveryUnit2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="DeliveryUnit1">A DeliveryUnit.</param>
        /// <param name="DeliveryUnit2">Another DeliveryUnit.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (DeliveryUnit DeliveryUnit1,
                                           DeliveryUnit DeliveryUnit2)

            => DeliveryUnit1.CompareTo(DeliveryUnit2) >= 0;

        #endregion

        #endregion

        #region IComparable<DeliveryUnit> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two DeliveryUnits.
        /// </summary>
        /// <param name="Object">A DeliveryUnit to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is DeliveryUnit deliveryUnit
                   ? CompareTo(deliveryUnit)
                   : throw new ArgumentException("The given object is not a DeliveryUnit!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(DeliveryUnit)

        /// <summary>
        /// Compares two DeliveryUnits.
        /// </summary>
        /// <param name="DeliveryUnit">A DeliveryUnit to compare with.</param>
        public Int32 CompareTo(DeliveryUnit DeliveryUnit)

            => String.Compare(InternalId,
                              DeliveryUnit.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<DeliveryUnit> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two DeliveryUnits for equality.
        /// </summary>
        /// <param name="Object">A DeliveryUnit to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is DeliveryUnit deliveryUnit &&
                   Equals(deliveryUnit);

        #endregion

        #region Equals(DeliveryUnit)

        /// <summary>
        /// Compares two DeliveryUnits for equality.
        /// </summary>
        /// <param name="DeliveryUnit">A DeliveryUnit to compare with.</param>
        public Boolean Equals(DeliveryUnit DeliveryUnit)

            => String.Equals(InternalId,
                             DeliveryUnit.InternalId,
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
