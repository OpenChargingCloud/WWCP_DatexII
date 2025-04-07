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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Extension methods for PaymentTimings.
    /// </summary>
    public static class PaymentTimingExtensions
    {

        /// <summary>
        /// Indicates whether this PaymentTiming is null or empty.
        /// </summary>
        /// <param name="PaymentTiming">A PaymentTiming.</param>
        public static Boolean IsNullOrEmpty(this PaymentTiming? PaymentTiming)
            => !PaymentTiming.HasValue || PaymentTiming.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this PaymentTiming is null or empty.
        /// </summary>
        /// <param name="PaymentTiming">A PaymentTiming.</param>
        public static Boolean IsNotNullOrEmpty(this PaymentTiming? PaymentTiming)
            => PaymentTiming.HasValue && PaymentTiming.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A PaymentTiming.
    /// </summary>
    public readonly struct PaymentTiming : IId,
                                           IEquatable<PaymentTiming>,
                                           IComparable<PaymentTiming>
    {

        #region Data

        private readonly static Dictionary<String, PaymentTiming>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this PaymentTiming is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this PaymentTiming is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the PaymentTiming.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered PaymentTimings.
        /// </summary>
        public static    IEnumerable<PaymentTiming>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new PaymentTiming based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a PaymentTiming.</param>
        private PaymentTiming(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static PaymentTiming Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new PaymentTiming(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a PaymentTiming.
        /// </summary>
        /// <param name="Text">A text representation of a PaymentTiming.</param>
        public static PaymentTiming Parse(String Text)
        {

            if (TryParse(Text, out var paymentTiming))
                return paymentTiming;

            throw new ArgumentException($"Invalid text representation of a PaymentTiming: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a PaymentTiming.
        /// </summary>
        /// <param name="Text">A text representation of a PaymentTiming.</param>
        public static PaymentTiming? TryParse(String Text)
        {

            if (TryParse(Text, out var paymentTiming))
                return paymentTiming;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out PaymentTiming)

        /// <summary>
        /// Try to parse the given text as a PaymentTiming.
        /// </summary>
        /// <param name="Text">A text representation of a PaymentTiming.</param>
        /// <param name="PaymentTiming">The parsed PaymentTiming.</param>
        public static Boolean TryParse(String Text, out PaymentTiming PaymentTiming)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out PaymentTiming))
                    PaymentTiming = Register(Text);

                return true;

            }

            PaymentTiming = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this PaymentTiming.
        /// </summary>
        public PaymentTiming Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Prepay
        /// </summary>
        public static PaymentTiming  Prepay            { get; }
            = Register("prepay");

        /// <summary>
        /// Payment on Entry
        /// </summary>
        public static PaymentTiming  PayOnEntry        { get; }
            = Register("payOnEntry");

        /// <summary>
        /// Payment prior to Exit
        /// </summary>
        public static PaymentTiming  PayPriorToExit    { get; }
            = Register("payPriorToExit");

        /// <summary>
        /// Payment after Exit
        /// </summary>
        public static PaymentTiming  PayAfterExit      { get; }
            = Register("payAfterExit");

        /// <summary>
        /// Pay and Exit
        /// </summary>
        public static PaymentTiming  PayAndExit        { get; }
            = Register("payAndExit");

        /// <summary>
        /// Other
        /// </summary>
        public static PaymentTiming  Other             { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (PaymentTiming1, PaymentTiming2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PaymentTiming1">A PaymentTiming.</param>
        /// <param name="PaymentTiming2">Another PaymentTiming.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (PaymentTiming PaymentTiming1,
                                           PaymentTiming PaymentTiming2)

            => PaymentTiming1.Equals(PaymentTiming2);

        #endregion

        #region Operator != (PaymentTiming1, PaymentTiming2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PaymentTiming1">A PaymentTiming.</param>
        /// <param name="PaymentTiming2">Another PaymentTiming.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (PaymentTiming PaymentTiming1,
                                           PaymentTiming PaymentTiming2)

            => !PaymentTiming1.Equals(PaymentTiming2);

        #endregion

        #region Operator <  (PaymentTiming1, PaymentTiming2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PaymentTiming1">A PaymentTiming.</param>
        /// <param name="PaymentTiming2">Another PaymentTiming.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (PaymentTiming PaymentTiming1,
                                          PaymentTiming PaymentTiming2)

            => PaymentTiming1.CompareTo(PaymentTiming2) < 0;

        #endregion

        #region Operator <= (PaymentTiming1, PaymentTiming2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PaymentTiming1">A PaymentTiming.</param>
        /// <param name="PaymentTiming2">Another PaymentTiming.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (PaymentTiming PaymentTiming1,
                                           PaymentTiming PaymentTiming2)

            => PaymentTiming1.CompareTo(PaymentTiming2) <= 0;

        #endregion

        #region Operator >  (PaymentTiming1, PaymentTiming2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PaymentTiming1">A PaymentTiming.</param>
        /// <param name="PaymentTiming2">Another PaymentTiming.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (PaymentTiming PaymentTiming1,
                                          PaymentTiming PaymentTiming2)

            => PaymentTiming1.CompareTo(PaymentTiming2) > 0;

        #endregion

        #region Operator >= (PaymentTiming1, PaymentTiming2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PaymentTiming1">A PaymentTiming.</param>
        /// <param name="PaymentTiming2">Another PaymentTiming.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (PaymentTiming PaymentTiming1,
                                           PaymentTiming PaymentTiming2)

            => PaymentTiming1.CompareTo(PaymentTiming2) >= 0;

        #endregion

        #endregion

        #region IComparable<PaymentTiming> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two PaymentTimings.
        /// </summary>
        /// <param name="Object">A PaymentTiming to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is PaymentTiming paymentTiming
                   ? CompareTo(paymentTiming)
                   : throw new ArgumentException("The given object is not a PaymentTiming!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(PaymentTiming)

        /// <summary>
        /// Compares two PaymentTimings.
        /// </summary>
        /// <param name="PaymentTiming">A PaymentTiming to compare with.</param>
        public Int32 CompareTo(PaymentTiming PaymentTiming)

            => String.Compare(InternalId,
                              PaymentTiming.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<PaymentTiming> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two PaymentTimings for equality.
        /// </summary>
        /// <param name="Object">A PaymentTiming to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is PaymentTiming paymentTiming &&
                   Equals(paymentTiming);

        #endregion

        #region Equals(PaymentTiming)

        /// <summary>
        /// Compares two PaymentTimings for equality.
        /// </summary>
        /// <param name="PaymentTiming">A PaymentTiming to compare with.</param>
        public Boolean Equals(PaymentTiming PaymentTiming)

            => String.Equals(InternalId,
                             PaymentTiming.InternalId,
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
