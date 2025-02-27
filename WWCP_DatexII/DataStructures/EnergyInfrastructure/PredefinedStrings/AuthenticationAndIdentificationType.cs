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
    /// Extension methods for AuthenticationAndIdentificationType.
    /// </summary>
    public static class AuthenticationAndIdentificationTypeExtensions
    {

        /// <summary>
        /// Indicates whether this AuthenticationAndIdentificationType is null or empty.
        /// </summary>
        /// <param name="AuthenticationAndIdentificationType">An AuthenticationAndIdentificationType.</param>
        public static Boolean IsNullOrEmpty(this AuthenticationAndIdentificationType? AuthenticationAndIdentificationType)
            => !AuthenticationAndIdentificationType.HasValue || AuthenticationAndIdentificationType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this AuthenticationAndIdentificationType is null or empty.
        /// </summary>
        /// <param name="AuthenticationAndIdentificationType">An AuthenticationAndIdentificationType.</param>
        public static Boolean IsNotNullOrEmpty(this AuthenticationAndIdentificationType? AuthenticationAndIdentificationType)
            => AuthenticationAndIdentificationType.HasValue && AuthenticationAndIdentificationType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An AuthenticationAndIdentificationType.
    /// </summary>
    public readonly struct AuthenticationAndIdentificationType : IId,
                                                                 IEquatable<AuthenticationAndIdentificationType>,
                                                                 IComparable<AuthenticationAndIdentificationType>
    {

        #region Data

        private readonly static Dictionary<String, AuthenticationAndIdentificationType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this AuthenticationAndIdentificationType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this AuthenticationAndIdentificationType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the AuthenticationAndIdentificationType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered AuthenticationAndIdentificationTypes.
        /// </summary>
        public static    IEnumerable<AuthenticationAndIdentificationType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new AuthenticationAndIdentificationType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an AuthenticationAndIdentificationType.</param>
        private AuthenticationAndIdentificationType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static AuthenticationAndIdentificationType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new AuthenticationAndIdentificationType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an AuthenticationAndIdentificationType.
        /// </summary>
        /// <param name="Text">A text representation of an AuthenticationAndIdentificationType.</param>
        public static AuthenticationAndIdentificationType Parse(String Text)
        {

            if (TryParse(Text, out var authenticationAndIdentificationType))
                return authenticationAndIdentificationType;

            throw new ArgumentException($"Invalid text representation of an AuthenticationAndIdentificationType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an AuthenticationAndIdentificationType.
        /// </summary>
        /// <param name="Text">A text representation of an AuthenticationAndIdentificationType.</param>
        public static AuthenticationAndIdentificationType? TryParse(String Text)
        {

            if (TryParse(Text, out var authenticationAndIdentificationType))
                return authenticationAndIdentificationType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out AuthenticationAndIdentificationType)

        /// <summary>
        /// Try to parse the given text as an AuthenticationAndIdentificationType.
        /// </summary>
        /// <param name="Text">A text representation of an AuthenticationAndIdentificationType.</param>
        /// <param name="AuthenticationAndIdentificationType">The parsed AuthenticationAndIdentificationType.</param>
        public static Boolean TryParse(String Text, out AuthenticationAndIdentificationType AuthenticationAndIdentificationType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out AuthenticationAndIdentificationType))
                    AuthenticationAndIdentificationType = Register(Text);

                return true;

            }

            AuthenticationAndIdentificationType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this AuthenticationAndIdentificationType.
        /// </summary>
        public AuthenticationAndIdentificationType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Phone (active RFID chip)
        /// </summary>
        public static AuthenticationAndIdentificationType  ActiveRFIDChip     { get; }
            = Register("activeRFIDChip");

        /// <summary>
        /// Apps
        /// </summary>
        public static AuthenticationAndIdentificationType  Apps               { get; }
            = Register("apps");

        /// <summary>
        /// RFID Calypso
        /// </summary>
        public static AuthenticationAndIdentificationType  Calypso            { get; }
            = Register("calypso");

        /// <summary>
        /// No specific authentication by using cash
        /// </summary>
        public static AuthenticationAndIdentificationType  CashPayment        { get; }
            = Register("cashPayment");

        /// <summary>
        /// Credit card
        /// </summary>
        public static AuthenticationAndIdentificationType  CreditCard         { get; }
            = Register("creditCard");

        /// <summary>
        /// Debit card
        /// </summary>
        public static AuthenticationAndIdentificationType  DebitCard          { get; }
            = Register("debitCard");

        /// <summary>
        /// RFID Card / Phone NFC - Mifare Classic
        /// </summary>
        public static AuthenticationAndIdentificationType  MifareClassic      { get; }
            = Register("mifareClassic");

        /// <summary>
        /// RFID Card / Phone NFC - Mifare Desfire
        /// </summary>
        public static AuthenticationAndIdentificationType  MifareDesfire      { get; }
            = Register("xxx");

        /// <summary>
        /// Nearfield communication
        /// </summary>
        public static AuthenticationAndIdentificationType  NFC                { get; }
            = Register("nfc");

        /// <summary>
        /// Over the air according to ISO 15118
        /// </summary>
        public static AuthenticationAndIdentificationType  OverTheAir         { get; }
            = Register("overTheAir");

        /// <summary>
        /// Phone (dialog with platform)
        /// </summary>
        public static AuthenticationAndIdentificationType  PhoneDialog        { get; }
            = Register("phoneDialog");

        /// <summary>
        /// Phone (SMS)
        /// </summary>
        public static AuthenticationAndIdentificationType  PhoneSMS           { get; }
            = Register("phoneSMS");

        /// <summary>
        /// PINPAD
        /// </summary>
        public static AuthenticationAndIdentificationType  PINPad             { get; }
            = Register("pinpad");

        /// <summary>
        /// PLC according to ISO 15118
        /// </summary>
        public static AuthenticationAndIdentificationType  PLC                { get; }
            = Register("plc");

        /// <summary>
        /// Pre-Paid card
        /// </summary>
        public static AuthenticationAndIdentificationType  PrepaidCard        { get; }
            = Register("prepaidCard");

        /// <summary>
        /// RFID
        /// </summary>
        public static AuthenticationAndIdentificationType  RFID               { get; }
            = Register("rfid");

        /// <summary>
        /// Using a website
        /// </summary>
        public static AuthenticationAndIdentificationType  Website            { get; }
            = Register("website");

        /// <summary>
        /// No authentication/identification required.
        /// </summary>
        public static AuthenticationAndIdentificationType  UnlimitedAccess    { get; }
            = Register("unlimitedAccess");

        /// <summary>
        /// No access granted
        /// </summary>
        public static AuthenticationAndIdentificationType  NoAccess           { get; }
            = Register("noAccess");

        #endregion


        #region Operator overloading

        #region Operator == (AuthenticationAndIdentificationType1, AuthenticationAndIdentificationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AuthenticationAndIdentificationType1">An AuthenticationAndIdentificationType.</param>
        /// <param name="AuthenticationAndIdentificationType2">Another AuthenticationAndIdentificationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (AuthenticationAndIdentificationType AuthenticationAndIdentificationType1,
                                           AuthenticationAndIdentificationType AuthenticationAndIdentificationType2)

            => AuthenticationAndIdentificationType1.Equals(AuthenticationAndIdentificationType2);

        #endregion

        #region Operator != (AuthenticationAndIdentificationType1, AuthenticationAndIdentificationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AuthenticationAndIdentificationType1">An AuthenticationAndIdentificationType.</param>
        /// <param name="AuthenticationAndIdentificationType2">Another AuthenticationAndIdentificationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (AuthenticationAndIdentificationType AuthenticationAndIdentificationType1,
                                           AuthenticationAndIdentificationType AuthenticationAndIdentificationType2)

            => !AuthenticationAndIdentificationType1.Equals(AuthenticationAndIdentificationType2);

        #endregion

        #region Operator <  (AuthenticationAndIdentificationType1, AuthenticationAndIdentificationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AuthenticationAndIdentificationType1">An AuthenticationAndIdentificationType.</param>
        /// <param name="AuthenticationAndIdentificationType2">Another AuthenticationAndIdentificationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (AuthenticationAndIdentificationType AuthenticationAndIdentificationType1,
                                          AuthenticationAndIdentificationType AuthenticationAndIdentificationType2)

            => AuthenticationAndIdentificationType1.CompareTo(AuthenticationAndIdentificationType2) < 0;

        #endregion

        #region Operator <= (AuthenticationAndIdentificationType1, AuthenticationAndIdentificationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AuthenticationAndIdentificationType1">An AuthenticationAndIdentificationType.</param>
        /// <param name="AuthenticationAndIdentificationType2">Another AuthenticationAndIdentificationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (AuthenticationAndIdentificationType AuthenticationAndIdentificationType1,
                                           AuthenticationAndIdentificationType AuthenticationAndIdentificationType2)

            => AuthenticationAndIdentificationType1.CompareTo(AuthenticationAndIdentificationType2) <= 0;

        #endregion

        #region Operator >  (AuthenticationAndIdentificationType1, AuthenticationAndIdentificationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AuthenticationAndIdentificationType1">An AuthenticationAndIdentificationType.</param>
        /// <param name="AuthenticationAndIdentificationType2">Another AuthenticationAndIdentificationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (AuthenticationAndIdentificationType AuthenticationAndIdentificationType1,
                                          AuthenticationAndIdentificationType AuthenticationAndIdentificationType2)

            => AuthenticationAndIdentificationType1.CompareTo(AuthenticationAndIdentificationType2) > 0;

        #endregion

        #region Operator >= (AuthenticationAndIdentificationType1, AuthenticationAndIdentificationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AuthenticationAndIdentificationType1">An AuthenticationAndIdentificationType.</param>
        /// <param name="AuthenticationAndIdentificationType2">Another AuthenticationAndIdentificationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (AuthenticationAndIdentificationType AuthenticationAndIdentificationType1,
                                           AuthenticationAndIdentificationType AuthenticationAndIdentificationType2)

            => AuthenticationAndIdentificationType1.CompareTo(AuthenticationAndIdentificationType2) >= 0;

        #endregion

        #endregion

        #region IComparable<AuthenticationAndIdentificationType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two AuthenticationAndIdentificationTypes.
        /// </summary>
        /// <param name="Object">An AuthenticationAndIdentificationType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is AuthenticationAndIdentificationType authenticationAndIdentificationType
                   ? CompareTo(authenticationAndIdentificationType)
                   : throw new ArgumentException("The given object is not an AuthenticationAndIdentificationType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(AuthenticationAndIdentificationType)

        /// <summary>
        /// Compares two AuthenticationAndIdentificationTypes.
        /// </summary>
        /// <param name="AuthenticationAndIdentificationType">An AuthenticationAndIdentificationType to compare with.</param>
        public Int32 CompareTo(AuthenticationAndIdentificationType AuthenticationAndIdentificationType)

            => String.Compare(InternalId,
                              AuthenticationAndIdentificationType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<AuthenticationAndIdentificationType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two AuthenticationAndIdentificationTypes for equality.
        /// </summary>
        /// <param name="Object">An AuthenticationAndIdentificationType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is AuthenticationAndIdentificationType authenticationAndIdentificationType &&
                   Equals(authenticationAndIdentificationType);

        #endregion

        #region Equals(AuthenticationAndIdentificationType)

        /// <summary>
        /// Compares two AuthenticationAndIdentificationTypes for equality.
        /// </summary>
        /// <param name="AuthenticationAndIdentificationType">An AuthenticationAndIdentificationType to compare with.</param>
        public Boolean Equals(AuthenticationAndIdentificationType AuthenticationAndIdentificationType)

            => String.Equals(InternalId,
                             AuthenticationAndIdentificationType.InternalId,
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
