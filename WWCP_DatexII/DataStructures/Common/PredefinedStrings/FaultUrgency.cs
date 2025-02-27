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
    /// Extension methods for FaultUrgencies.
    /// </summary>
    public static class FaultUrgencyExtensions
    {

        /// <summary>
        /// Indicates whether this FaultUrgency is null or empty.
        /// </summary>
        /// <param name="FaultUrgency">A FaultUrgency.</param>
        public static Boolean IsNullOrEmpty(this FaultUrgency? FaultUrgency)
            => !FaultUrgency.HasValue || FaultUrgency.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this FaultUrgency is null or empty.
        /// </summary>
        /// <param name="FaultUrgency">A FaultUrgency.</param>
        public static Boolean IsNotNullOrEmpty(this FaultUrgency? FaultUrgency)
            => FaultUrgency.HasValue && FaultUrgency.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A FaultUrgency.
    /// </summary>
    public readonly struct FaultUrgency : IId,
                                          IEquatable<FaultUrgency>,
                                          IComparable<FaultUrgency>
    {

        #region Data

        private readonly static Dictionary<String, FaultUrgency>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this FaultUrgency is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this FaultUrgency is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the FaultUrgency.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<FaultUrgency>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new FaultUrgency based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a FaultUrgency.</param>
        private FaultUrgency(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static FaultUrgency Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new FaultUrgency(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a FaultUrgency.
        /// </summary>
        /// <param name="Text">A text representation of a FaultUrgency.</param>
        public static FaultUrgency Parse(String Text)
        {

            if (TryParse(Text, out var faultUrgency))
                return faultUrgency;

            throw new ArgumentException($"Invalid text representation of a FaultUrgency: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a FaultUrgency.
        /// </summary>
        /// <param name="Text">A text representation of a FaultUrgency.</param>
        public static FaultUrgency? TryParse(String Text)
        {

            if (TryParse(Text, out var faultUrgency))
                return faultUrgency;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out FaultUrgency)

        /// <summary>
        /// Try to parse the given text as FaultUrgency.
        /// </summary>
        /// <param name="Text">A text representation of a FaultUrgency.</param>
        /// <param name="FaultUrgency">The parsed FaultUrgency.</param>
        public static Boolean TryParse(String Text, out FaultUrgency FaultUrgency)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out FaultUrgency))
                    FaultUrgency = Register(Text);

                return true;

            }

            FaultUrgency = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this FaultUrgency.
        /// </summary>
        public FaultUrgency Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// The urgency to rectify this fault is normal.
        /// </summary>
        public static FaultUrgency  Normal             { get; }
            = Register("normal");

        /// <summary>
        /// It is urgent to rectify this fault.
        /// </summary>
        public static FaultUrgency  Urgent             { get; }
            = Register("urgent");

        /// <summary>
        /// It is extremely urgent to rectify this fault.
        /// </summary>
        public static FaultUrgency  ExtremelyUrgent    { get; }
            = Register("extremelyUrgent");

        /// <summary>
        /// The urgency to rectify this fault is not known.
        /// </summary>
        public static FaultUrgency  Unknown            { get; }
            = Register("unknown");

        #endregion


        #region Operator overloading

        #region Operator == (FaultUrgency1, FaultUrgency2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultUrgency1">A FaultUrgency.</param>
        /// <param name="FaultUrgency2">Another FaultUrgency.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (FaultUrgency FaultUrgency1,
                                           FaultUrgency FaultUrgency2)

            => FaultUrgency1.Equals(FaultUrgency2);

        #endregion

        #region Operator != (FaultUrgency1, FaultUrgency2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultUrgency1">A FaultUrgency.</param>
        /// <param name="FaultUrgency2">Another FaultUrgency.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (FaultUrgency FaultUrgency1,
                                           FaultUrgency FaultUrgency2)

            => !FaultUrgency1.Equals(FaultUrgency2);

        #endregion

        #region Operator <  (FaultUrgency1, FaultUrgency2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultUrgency1">A FaultUrgency.</param>
        /// <param name="FaultUrgency2">Another FaultUrgency.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (FaultUrgency FaultUrgency1,
                                          FaultUrgency FaultUrgency2)

            => FaultUrgency1.CompareTo(FaultUrgency2) < 0;

        #endregion

        #region Operator <= (FaultUrgency1, FaultUrgency2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultUrgency1">A FaultUrgency.</param>
        /// <param name="FaultUrgency2">Another FaultUrgency.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (FaultUrgency FaultUrgency1,
                                           FaultUrgency FaultUrgency2)

            => FaultUrgency1.CompareTo(FaultUrgency2) <= 0;

        #endregion

        #region Operator >  (FaultUrgency1, FaultUrgency2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultUrgency1">A FaultUrgency.</param>
        /// <param name="FaultUrgency2">Another FaultUrgency.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (FaultUrgency FaultUrgency1,
                                          FaultUrgency FaultUrgency2)

            => FaultUrgency1.CompareTo(FaultUrgency2) > 0;

        #endregion

        #region Operator >= (FaultUrgency1, FaultUrgency2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="FaultUrgency1">A FaultUrgency.</param>
        /// <param name="FaultUrgency2">Another FaultUrgency.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (FaultUrgency FaultUrgency1,
                                           FaultUrgency FaultUrgency2)

            => FaultUrgency1.CompareTo(FaultUrgency2) >= 0;

        #endregion

        #endregion

        #region IComparable<FaultUrgency> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two FaultUrgencies.
        /// </summary>
        /// <param name="Object">FaultUrgency to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is FaultUrgency faultUrgency
                   ? CompareTo(faultUrgency)
                   : throw new ArgumentException("The given object is not FaultUrgency!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(FaultUrgency)

        /// <summary>
        /// Compares two FaultUrgencies.
        /// </summary>
        /// <param name="FaultUrgency">FaultUrgency to compare with.</param>
        public Int32 CompareTo(FaultUrgency FaultUrgency)

            => String.Compare(InternalId,
                              FaultUrgency.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<FaultUrgency> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two FaultUrgencies for equality.
        /// </summary>
        /// <param name="Object">FaultUrgency to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is FaultUrgency faultUrgency &&
                   Equals(faultUrgency);

        #endregion

        #region Equals(FaultUrgency)

        /// <summary>
        /// Compares two FaultUrgencies for equality.
        /// </summary>
        /// <param name="FaultUrgency">FaultUrgency to compare with.</param>
        public Boolean Equals(FaultUrgency FaultUrgency)

            => String.Equals(InternalId,
                             FaultUrgency.InternalId,
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
