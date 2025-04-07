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
    /// Extension methods for FaultSeverities.
    /// </summary>
    public static class FaultSeverityExtensions
    {

        /// <summary>
        /// Indicates whether this FaultSeverity is null or empty.
        /// </summary>
        /// <param name="FaultSeverity">A FaultSeverity.</param>
        public static Boolean IsNullOrEmpty(this FaultSeverity? FaultSeverity)
            => !FaultSeverity.HasValue || FaultSeverity.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this FaultSeverity is null or empty.
        /// </summary>
        /// <param name="FaultSeverity">A FaultSeverity.</param>
        public static Boolean IsNotNullOrEmpty(this FaultSeverity? FaultSeverity)
            => FaultSeverity.HasValue && FaultSeverity.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A FaultSeverity.
    /// </summary>
    public readonly struct FaultSeverity : IId,
                                           IEquatable<FaultSeverity>,
                                           IComparable<FaultSeverity>
    {

        #region Data

        private readonly static Dictionary<String, FaultSeverity>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this FaultSeverity is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this FaultSeverity is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the FaultSeverity.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<FaultSeverity>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new FaultSeverity based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a FaultSeverity.</param>
        private FaultSeverity(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static FaultSeverity Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new FaultSeverity(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a FaultSeverity.
        /// </summary>
        /// <param name="Text">A text representation of a FaultSeverity.</param>
        public static FaultSeverity Parse(String Text)
        {

            if (TryParse(Text, out var faultSeverity))
                return faultSeverity;

            throw new ArgumentException($"Invalid text representation of a FaultSeverity: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a FaultSeverity.
        /// </summary>
        /// <param name="Text">A text representation of a FaultSeverity.</param>
        public static FaultSeverity? TryParse(String Text)
        {

            if (TryParse(Text, out var faultSeverity))
                return faultSeverity;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out FaultSeverity)

        /// <summary>
        /// Try to parse the given text as FaultSeverity.
        /// </summary>
        /// <param name="Text">A text representation of a FaultSeverity.</param>
        /// <param name="FaultSeverity">The parsed FaultSeverity.</param>
        public static Boolean TryParse(String Text, out FaultSeverity FaultSeverity)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out FaultSeverity))
                    FaultSeverity = Register(Text);

                return true;

            }

            FaultSeverity = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this FaultSeverity.
        /// </summary>
        public FaultSeverity Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// The fault is of low severity and has only limited impact on the usability
        /// of the equipment or the value of the data generated by the equipment.
        /// </summary>
        public static FaultSeverity  Low        { get; }
            = Register("low");

        /// <summary>
        /// The fault is of medium severity which will significantly limit the usability
        /// of the equipment or devalue the usefulness of the data generated by the equipment.
        /// </summary>
        public static FaultSeverity  Medium     { get; }
            = Register("medium");

        /// <summary>
        /// The fault is of high severity which will render the equipment unusable or
        /// any data generated by the equipment to be of no value.
        /// </summary>
        public static FaultSeverity  High       { get; }
            = Register("high");

        /// <summary>
        /// The fault is of unknown severity and its effect on the usability of the equipment or
        /// the usefulness of the data generated by the equipment cannot be assessed.
        /// </summary>
        public static FaultSeverity  Unknown    { get; }
            = Register("unknown");

        #endregion


        #region Operator overloading

        #region Operator == (FaultSeverity1, FaultSeverity2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultSeverity1">A FaultSeverity.</param>
        /// <param name="FaultSeverity2">Another FaultSeverity.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (FaultSeverity FaultSeverity1,
                                           FaultSeverity FaultSeverity2)

            => FaultSeverity1.Equals(FaultSeverity2);

        #endregion

        #region Operator != (FaultSeverity1, FaultSeverity2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultSeverity1">A FaultSeverity.</param>
        /// <param name="FaultSeverity2">Another FaultSeverity.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (FaultSeverity FaultSeverity1,
                                           FaultSeverity FaultSeverity2)

            => !FaultSeverity1.Equals(FaultSeverity2);

        #endregion

        #region Operator <  (FaultSeverity1, FaultSeverity2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultSeverity1">A FaultSeverity.</param>
        /// <param name="FaultSeverity2">Another FaultSeverity.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (FaultSeverity FaultSeverity1,
                                          FaultSeverity FaultSeverity2)

            => FaultSeverity1.CompareTo(FaultSeverity2) < 0;

        #endregion

        #region Operator <= (FaultSeverity1, FaultSeverity2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultSeverity1">A FaultSeverity.</param>
        /// <param name="FaultSeverity2">Another FaultSeverity.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (FaultSeverity FaultSeverity1,
                                           FaultSeverity FaultSeverity2)

            => FaultSeverity1.CompareTo(FaultSeverity2) <= 0;

        #endregion

        #region Operator >  (FaultSeverity1, FaultSeverity2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultSeverity1">A FaultSeverity.</param>
        /// <param name="FaultSeverity2">Another FaultSeverity.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (FaultSeverity FaultSeverity1,
                                          FaultSeverity FaultSeverity2)

            => FaultSeverity1.CompareTo(FaultSeverity2) > 0;

        #endregion

        #region Operator >= (FaultSeverity1, FaultSeverity2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultSeverity1">A FaultSeverity.</param>
        /// <param name="FaultSeverity2">Another FaultSeverity.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (FaultSeverity FaultSeverity1,
                                           FaultSeverity FaultSeverity2)

            => FaultSeverity1.CompareTo(FaultSeverity2) >= 0;

        #endregion

        #endregion

        #region IComparable<FaultSeverity> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two FaultSeverities.
        /// </summary>
        /// <param name="Object">FaultSeverity to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is FaultSeverity faultSeverity
                   ? CompareTo(faultSeverity)
                   : throw new ArgumentException("The given object is not FaultSeverity!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(FaultSeverity)

        /// <summary>
        /// Compares two FaultSeverities.
        /// </summary>
        /// <param name="FaultSeverity">FaultSeverity to compare with.</param>
        public Int32 CompareTo(FaultSeverity FaultSeverity)

            => String.Compare(InternalId,
                              FaultSeverity.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<FaultSeverity> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two FaultSeverities for equality.
        /// </summary>
        /// <param name="Object">FaultSeverity to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is FaultSeverity faultSeverity &&
                   Equals(faultSeverity);

        #endregion

        #region Equals(FaultSeverity)

        /// <summary>
        /// Compares two FaultSeverities for equality.
        /// </summary>
        /// <param name="FaultSeverity">FaultSeverity to compare with.</param>
        public Boolean Equals(FaultSeverity FaultSeverity)

            => String.Equals(InternalId,
                             FaultSeverity.InternalId,
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
