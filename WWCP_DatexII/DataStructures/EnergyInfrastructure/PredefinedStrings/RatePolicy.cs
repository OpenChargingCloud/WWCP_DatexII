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
    /// Extension methods for RatePolicies.
    /// </summary>
    public static class RatePolicyExtensions
    {

        /// <summary>
        /// Indicates whether this RatePolicy is null or empty.
        /// </summary>
        /// <param name="RatePolicy">A RatePolicy.</param>
        public static Boolean IsNullOrEmpty(this RatePolicy? RatePolicy)
            => !RatePolicy.HasValue || RatePolicy.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this RatePolicy is null or empty.
        /// </summary>
        /// <param name="RatePolicy">A RatePolicy.</param>
        public static Boolean IsNotNullOrEmpty(this RatePolicy? RatePolicy)
            => RatePolicy.HasValue && RatePolicy.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A RatePolicy.
    /// </summary>
    public readonly struct RatePolicy : IId,
                                        IEquatable<RatePolicy>,
                                        IComparable<RatePolicy>
    {

        #region Data

        private readonly static Dictionary<String, RatePolicy>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this RatePolicy is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this RatePolicy is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the RatePolicy.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<RatePolicy>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new RatePolicy based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a RatePolicy.</param>
        private RatePolicy(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static RatePolicy Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new RatePolicy(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a RatePolicy.
        /// </summary>
        /// <param name="Text">A text representation of a RatePolicy.</param>
        public static RatePolicy Parse(String Text)
        {

            if (TryParse(Text, out var ratePolicy))
                return ratePolicy;

            throw new ArgumentException($"Invalid text representation of a RatePolicy: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a RatePolicy.
        /// </summary>
        /// <param name="Text">A text representation of a RatePolicy.</param>
        public static RatePolicy? TryParse(String Text)
        {

            if (TryParse(Text, out var ratePolicy))
                return ratePolicy;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out RatePolicy)

        /// <summary>
        /// Try to parse the given text as RatePolicy.
        /// </summary>
        /// <param name="Text">A text representation of a RatePolicy.</param>
        /// <param name="RatePolicy">The parsed RatePolicy.</param>
        public static Boolean TryParse(String Text, out RatePolicy RatePolicy)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out RatePolicy))
                    RatePolicy = Register(Text);

                return true;

            }

            RatePolicy = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this RatePolicy.
        /// </summary>
        public RatePolicy Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// A contract defines the pricing.
        /// </summary>
        public static RatePolicy  Contract    { get; }
            = Register("contract");

        /// <summary>
        /// Prices are for ad hoc refueling.
        /// </summary>
        public static RatePolicy  AdHoc       { get; }
            = Register("adHoc");

        #endregion


        #region Operator overloading

        #region Operator == (RatePolicy1, RatePolicy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RatePolicy1">A RatePolicy.</param>
        /// <param name="RatePolicy2">Another RatePolicy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (RatePolicy RatePolicy1,
                                           RatePolicy RatePolicy2)

            => RatePolicy1.Equals(RatePolicy2);

        #endregion

        #region Operator != (RatePolicy1, RatePolicy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RatePolicy1">A RatePolicy.</param>
        /// <param name="RatePolicy2">Another RatePolicy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (RatePolicy RatePolicy1,
                                           RatePolicy RatePolicy2)

            => !RatePolicy1.Equals(RatePolicy2);

        #endregion

        #region Operator <  (RatePolicy1, RatePolicy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RatePolicy1">A RatePolicy.</param>
        /// <param name="RatePolicy2">Another RatePolicy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (RatePolicy RatePolicy1,
                                          RatePolicy RatePolicy2)

            => RatePolicy1.CompareTo(RatePolicy2) < 0;

        #endregion

        #region Operator <= (RatePolicy1, RatePolicy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RatePolicy1">A RatePolicy.</param>
        /// <param name="RatePolicy2">Another RatePolicy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (RatePolicy RatePolicy1,
                                           RatePolicy RatePolicy2)

            => RatePolicy1.CompareTo(RatePolicy2) <= 0;

        #endregion

        #region Operator >  (RatePolicy1, RatePolicy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RatePolicy1">A RatePolicy.</param>
        /// <param name="RatePolicy2">Another RatePolicy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (RatePolicy RatePolicy1,
                                          RatePolicy RatePolicy2)

            => RatePolicy1.CompareTo(RatePolicy2) > 0;

        #endregion

        #region Operator >= (RatePolicy1, RatePolicy2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RatePolicy1">A RatePolicy.</param>
        /// <param name="RatePolicy2">Another RatePolicy.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (RatePolicy RatePolicy1,
                                           RatePolicy RatePolicy2)

            => RatePolicy1.CompareTo(RatePolicy2) >= 0;

        #endregion

        #endregion

        #region IComparable<RatePolicy> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two RatePolicies.
        /// </summary>
        /// <param name="Object">RatePolicy to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is RatePolicy RatePolicy
                   ? CompareTo(RatePolicy)
                   : throw new ArgumentException("The given object is not RatePolicy!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(RatePolicy)

        /// <summary>
        /// Compares two RatePolicies.
        /// </summary>
        /// <param name="RatePolicy">RatePolicy to compare with.</param>
        public Int32 CompareTo(RatePolicy RatePolicy)

            => String.Compare(InternalId,
                              RatePolicy.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<RatePolicy> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two RatePolicies for equality.
        /// </summary>
        /// <param name="Object">RatePolicy to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is RatePolicy RatePolicy &&
                   Equals(RatePolicy);

        #endregion

        #region Equals(RatePolicy)

        /// <summary>
        /// Compares two RatePolicies for equality.
        /// </summary>
        /// <param name="RatePolicy">RatePolicy to compare with.</param>
        public Boolean Equals(RatePolicy RatePolicy)

            => String.Equals(InternalId,
                             RatePolicy.InternalId,
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
