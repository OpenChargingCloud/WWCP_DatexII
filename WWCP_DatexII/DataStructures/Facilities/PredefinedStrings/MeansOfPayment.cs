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
    /// Extension methods for MeansOfPayments.
    /// </summary>
    public static class MeansOfPaymentExtensions
    {

        /// <summary>
        /// Indicates whether this MeansOfPayment is null or empty.
        /// </summary>
        /// <param name="MeansOfPayment">A MeansOfPayment.</param>
        public static Boolean IsNullOrEmpty(this MeansOfPayment? MeansOfPayment)
            => !MeansOfPayment.HasValue || MeansOfPayment.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this MeansOfPayment is null or empty.
        /// </summary>
        /// <param name="MeansOfPayment">A MeansOfPayment.</param>
        public static Boolean IsNotNullOrEmpty(this MeansOfPayment? MeansOfPayment)
            => MeansOfPayment.HasValue && MeansOfPayment.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A MeansOfPayment.
    /// </summary>
    public readonly struct MeansOfPayment : IId,
                                           IEquatable<MeansOfPayment>,
                                           IComparable<MeansOfPayment>
    {

        #region Data

        private readonly static Dictionary<String, MeansOfPayment>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this MeansOfPayment is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this MeansOfPayment is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the MeansOfPayment.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered MeansOfPayments.
        /// </summary>
        public static    IEnumerable<MeansOfPayment>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new MeansOfPayment based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a MeansOfPayment.</param>
        private MeansOfPayment(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static MeansOfPayment Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new MeansOfPayment(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a MeansOfPayment.
        /// </summary>
        /// <param name="Text">A text representation of a MeansOfPayment.</param>
        public static MeansOfPayment Parse(String Text)
        {

            if (TryParse(Text, out var paymentTiming))
                return paymentTiming;

            throw new ArgumentException($"Invalid text representation of a MeansOfPayment: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a MeansOfPayment.
        /// </summary>
        /// <param name="Text">A text representation of a MeansOfPayment.</param>
        public static MeansOfPayment? TryParse(String Text)
        {

            if (TryParse(Text, out var paymentTiming))
                return paymentTiming;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out MeansOfPayment)

        /// <summary>
        /// Try to parse the given text as a MeansOfPayment.
        /// </summary>
        /// <param name="Text">A text representation of a MeansOfPayment.</param>
        /// <param name="MeansOfPayment">The parsed MeansOfPayment.</param>
        public static Boolean TryParse(String Text, out MeansOfPayment MeansOfPayment)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out MeansOfPayment))
                    MeansOfPayment = Register(Text);

                return true;

            }

            MeansOfPayment = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this MeansOfPayment.
        /// </summary>
        public MeansOfPayment Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Payment Credit Card
        /// </summary>
        public static MeansOfPayment  PaymentCreditCard    { get; }
            = Register("paymentCreditCard");

        /// <summary>
        /// Cash Bills Only
        /// </summary>
        public static MeansOfPayment  CashBillsOnly        { get; }
            = Register("cashBillsOnly");

        /// <summary>
        /// Cash Coins Only
        /// </summary>
        public static MeansOfPayment  CashCoinsOnly        { get; }
            = Register("cashCoinsOnly");

        /// <summary>
        /// Toll Tag
        /// </summary>
        public static MeansOfPayment  TollTag              { get; }
            = Register("tollTag");

        /// <summary>
        /// Mobile Account
        /// </summary>
        public static MeansOfPayment  MobileAccount        { get; }
            = Register("mobileAccount");

        /// <summary>
        /// Cash, Coins and Bills
        /// </summary>
        public static MeansOfPayment  CashCoinsAndBills    { get; }
            = Register("cashCoinsAndBills");

        /// <summary>
        /// Prepay
        /// </summary>
        public static MeansOfPayment  Prepay               { get; }
            = Register("prepay");

        /// <summary>
        /// Payment Debit Card
        /// </summary>
        public static MeansOfPayment  PaymentDebitCard     { get; }
            = Register("paymentDebitCard");

        /// <summary>
        /// Payment Value Card
        /// </summary>
        public static MeansOfPayment  PaymentValueCard     { get; }
            = Register("paymentValueCard");

        /// <summary>
        /// Near Field Communication
        /// </summary>
        public static MeansOfPayment  NFC                  { get; }
            = Register("nfc");

        /// <summary>
        /// EMV
        /// </summary>
        public static MeansOfPayment  EMV                  { get; }
            = Register("emv");

        /// <summary>
        /// QR-Code
        /// </summary>
        public static MeansOfPayment  QRCode               { get; }
            = Register("qrCode");

        /// <summary>
        /// Website
        /// </summary>
        public static MeansOfPayment  Website              { get; }
            = Register("website");

        /// <summary>
        /// Unknown
        /// </summary>
        public static MeansOfPayment  Unknown              { get; }
            = Register("unknown");

        #endregion


        #region Operator overloading

        #region Operator == (MeansOfPayment1, MeansOfPayment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MeansOfPayment1">A MeansOfPayment.</param>
        /// <param name="MeansOfPayment2">Another MeansOfPayment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (MeansOfPayment MeansOfPayment1,
                                           MeansOfPayment MeansOfPayment2)

            => MeansOfPayment1.Equals(MeansOfPayment2);

        #endregion

        #region Operator != (MeansOfPayment1, MeansOfPayment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MeansOfPayment1">A MeansOfPayment.</param>
        /// <param name="MeansOfPayment2">Another MeansOfPayment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (MeansOfPayment MeansOfPayment1,
                                           MeansOfPayment MeansOfPayment2)

            => !MeansOfPayment1.Equals(MeansOfPayment2);

        #endregion

        #region Operator <  (MeansOfPayment1, MeansOfPayment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MeansOfPayment1">A MeansOfPayment.</param>
        /// <param name="MeansOfPayment2">Another MeansOfPayment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (MeansOfPayment MeansOfPayment1,
                                          MeansOfPayment MeansOfPayment2)

            => MeansOfPayment1.CompareTo(MeansOfPayment2) < 0;

        #endregion

        #region Operator <= (MeansOfPayment1, MeansOfPayment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MeansOfPayment1">A MeansOfPayment.</param>
        /// <param name="MeansOfPayment2">Another MeansOfPayment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (MeansOfPayment MeansOfPayment1,
                                           MeansOfPayment MeansOfPayment2)

            => MeansOfPayment1.CompareTo(MeansOfPayment2) <= 0;

        #endregion

        #region Operator >  (MeansOfPayment1, MeansOfPayment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MeansOfPayment1">A MeansOfPayment.</param>
        /// <param name="MeansOfPayment2">Another MeansOfPayment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (MeansOfPayment MeansOfPayment1,
                                          MeansOfPayment MeansOfPayment2)

            => MeansOfPayment1.CompareTo(MeansOfPayment2) > 0;

        #endregion

        #region Operator >= (MeansOfPayment1, MeansOfPayment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MeansOfPayment1">A MeansOfPayment.</param>
        /// <param name="MeansOfPayment2">Another MeansOfPayment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (MeansOfPayment MeansOfPayment1,
                                           MeansOfPayment MeansOfPayment2)

            => MeansOfPayment1.CompareTo(MeansOfPayment2) >= 0;

        #endregion

        #endregion

        #region IComparable<MeansOfPayment> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two MeansOfPayments.
        /// </summary>
        /// <param name="Object">A MeansOfPayment to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is MeansOfPayment paymentTiming
                   ? CompareTo(paymentTiming)
                   : throw new ArgumentException("The given object is not a MeansOfPayment!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(MeansOfPayment)

        /// <summary>
        /// Compares two MeansOfPayments.
        /// </summary>
        /// <param name="MeansOfPayment">A MeansOfPayment to compare with.</param>
        public Int32 CompareTo(MeansOfPayment MeansOfPayment)

            => String.Compare(InternalId,
                              MeansOfPayment.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<MeansOfPayment> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two MeansOfPayments for equality.
        /// </summary>
        /// <param name="Object">A MeansOfPayment to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is MeansOfPayment paymentTiming &&
                   Equals(paymentTiming);

        #endregion

        #region Equals(MeansOfPayment)

        /// <summary>
        /// Compares two MeansOfPayments for equality.
        /// </summary>
        /// <param name="MeansOfPayment">A MeansOfPayment to compare with.</param>
        public Boolean Equals(MeansOfPayment MeansOfPayment)

            => String.Equals(InternalId,
                             MeansOfPayment.InternalId,
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
