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
    /// Extension methods for UserTypes.
    /// </summary>
    public static class UserTypeExtensions
    {

        /// <summary>
        /// Indicates whether this UserType is null or empty.
        /// </summary>
        /// <param name="UserType">An UserType.</param>
        public static Boolean IsNullOrEmpty(this UserType? UserType)
            => !UserType.HasValue || UserType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this UserType is null or empty.
        /// </summary>
        /// <param name="UserType">An UserType.</param>
        public static Boolean IsNotNullOrEmpty(this UserType? UserType)
            => UserType.HasValue && UserType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An UserType.
    /// </summary>
    public readonly struct UserType : IId,
                                      IEquatable<UserType>,
                                      IComparable<UserType>
    {

        #region Data

        private readonly static Dictionary<String, UserType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this UserType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this UserType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the UserType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered UserTypes.
        /// </summary>
        public static    IEnumerable<UserType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new UserType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an UserType.</param>
        private UserType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static UserType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new UserType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an UserType.
        /// </summary>
        /// <param name="Text">A text representation of an UserType.</param>
        public static UserType Parse(String Text)
        {

            if (TryParse(Text, out var userType))
                return userType;

            throw new ArgumentException($"Invalid text representation of an UserType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an UserType.
        /// </summary>
        /// <param name="Text">A text representation of an UserType.</param>
        public static UserType? TryParse(String Text)
        {

            if (TryParse(Text, out var userType))
                return userType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out UserType)

        /// <summary>
        /// Try to parse the given text as an UserType.
        /// </summary>
        /// <param name="Text">A text representation of an UserType.</param>
        /// <param name="UserType">The parsed UserType.</param>
        public static Boolean TryParse(String Text, out UserType UserType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out UserType))
                    UserType = Register(Text);

                return true;

            }

            UserType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this UserType.
        /// </summary>
        public UserType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// All users.
        /// </summary>
        public static UserType  AllUsers                    { get; }
            = Register("allUsers");

        /// <summary>
        /// Commuters.
        /// </summary>
        public static UserType  Commuters                   { get; }
            = Register("commuters");

        /// <summary>
        /// Customers.
        /// </summary>
        public static UserType  Customers                   { get; }
            = Register("customers");

        /// <summary>
        /// Elderly users.
        /// </summary>
        public static UserType  ElderlyUsers                { get; }
            = Register("elderlyUsers");

        /// <summary>
        /// Employees.
        /// </summary>
        public static UserType  Employees                   { get; }
            = Register("xxemployeesx");

        /// <summary>
        /// Families.
        /// </summary>
        public static UserType  Families                    { get; }
            = Register("families");

        /// <summary>
        /// Persons with Disabilities.
        /// </summary>
        public static UserType  PersonsWithDisabilities     { get; }
            = Register("personsWithDisabilities");

        /// <summary>
        /// People with hearing loss.
        /// </summary>
        public static UserType  HearingImpaired             { get; }
            = Register("hearingImpaired");

        /// <summary>
        /// Hotel Guests.
        /// </summary>
        public static UserType  HotelGuests                 { get; }
            = Register("hotelGuests");

        /// <summary>
        /// Long-term Parker.
        /// </summary>
        public static UserType  LongTermParkers             { get; }
            = Register("longTermParkers");

        /// <summary>
        /// Members.
        /// </summary>
        public static UserType  Members                     { get; }
            = Register("members");

        /// <summary>
        /// Men.
        /// </summary>
        public static UserType  Men                         { get; }
            = Register("men");

        /// <summary>
        /// Overnight Parker.
        /// </summary>
        public static UserType  OvernightParkers            { get; }
            = Register("overnightParkers");

        /// <summary>
        /// Park and Cycle User.
        /// </summary>
        public static UserType  ParkAndCycleUser            { get; }
            = Register("parkAndCycleUser");

        /// <summary>
        /// Users that are exchanging into public transport at a park and ride station.
        /// </summary>
        public static UserType  ParkAndRideUsers            { get; }
            = Register("parkAndRideUsers");

        /// <summary>
        /// Park and walk user.
        /// </summary>
        public static UserType  ParkAndWalkUser             { get; }
            = Register("parkAndWalkUser");

        /// <summary>
        /// Pensioners.
        /// </summary>
        public static UserType  Pensioners                  { get; }
            = Register("pensioners");

        /// <summary>
        /// Pregnant Women.
        /// </summary>
        public static UserType  PregnantWomen               { get; }
            = Register("pregnantWomen");

        /// <summary>
        /// Registered Disabled Persons.
        /// </summary>
        public static UserType  RegisteredDisabledUsers     { get; }
            = Register("registeredDisabledUsers");

        /// <summary>
        /// Those who have a valid reservation, e.g. for the duration of parking.
        /// </summary>
        public static UserType  ReservationHolders          { get; }
            = Register("reservationHolders");

        /// <summary>
        /// Local Residents.
        /// </summary>
        public static UserType  Residents                   { get; }
            = Register("residents");

        /// <summary>
        /// Season Ticket Holders.
        /// </summary>
        public static UserType  SeasonTicketHolders         { get; }
            = Register("seasonTicketHolders");

        /// <summary>
        /// Shoppers.
        /// </summary>
        public static UserType  Shoppers                    { get; }
            = Register("shoppers");

        /// <summary>
        /// Short-term Parker.
        /// </summary>
        public static UserType  ShortTermParkers            { get; }
            = Register("shortTermParkers");

        /// <summary>
        /// Sport Event Away Supporters.
        /// </summary>
        public static UserType  SportEventAwaySupporters    { get; }
            = Register("sportEventAwaySupporters");

        /// <summary>
        /// Sport Event Home Supporters.
        /// </summary>
        public static UserType  SportEventHomeSupporters    { get; }
            = Register("sportEventHomeSupporters");

        /// <summary>
        /// Students.
        /// </summary>
        public static UserType  Students                    { get; }
            = Register("students");

        /// <summary>
        /// Staff.
        /// </summary>
        public static UserType  Staff                       { get; }
            = Register("staff");

        /// <summary>
        /// Subscribers.
        /// </summary>
        public static UserType  Subscribers                 { get; }
            = Register("subscribers");

        /// <summary>
        /// Visitors.
        /// </summary>
        public static UserType  Visitors                    { get; }
            = Register("visitors");

        /// <summary>
        /// People with vision impairment.
        /// </summary>
        public static UserType  VisuallyImpaired            { get; }
            = Register("visuallyImpaired");

        /// <summary>
        /// Wheelchair Users.
        /// </summary>
        public static UserType  WheelchairUsers             { get; }
            = Register("wheelchairUsers");

        /// <summary>
        /// Women.
        /// </summary>
        public static UserType  Women                       { get; }
            = Register("women");

        /// <summary>
        /// Unknown.
        /// </summary>
        public static UserType  Unknown                     { get; }
            = Register("unknown");

        /// <summary>
        /// Other.
        /// </summary>
        public static UserType  Other                       { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (UserType1, UserType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="UserType1">An UserType.</param>
        /// <param name="UserType2">Another UserType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (UserType UserType1,
                                           UserType UserType2)

            => UserType1.Equals(UserType2);

        #endregion

        #region Operator != (UserType1, UserType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="UserType1">An UserType.</param>
        /// <param name="UserType2">Another UserType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (UserType UserType1,
                                           UserType UserType2)

            => !UserType1.Equals(UserType2);

        #endregion

        #region Operator <  (UserType1, UserType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="UserType1">An UserType.</param>
        /// <param name="UserType2">Another UserType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (UserType UserType1,
                                          UserType UserType2)

            => UserType1.CompareTo(UserType2) < 0;

        #endregion

        #region Operator <= (UserType1, UserType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="UserType1">An UserType.</param>
        /// <param name="UserType2">Another UserType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (UserType UserType1,
                                           UserType UserType2)

            => UserType1.CompareTo(UserType2) <= 0;

        #endregion

        #region Operator >  (UserType1, UserType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="UserType1">An UserType.</param>
        /// <param name="UserType2">Another UserType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (UserType UserType1,
                                          UserType UserType2)

            => UserType1.CompareTo(UserType2) > 0;

        #endregion

        #region Operator >= (UserType1, UserType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="UserType1">An UserType.</param>
        /// <param name="UserType2">Another UserType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (UserType UserType1,
                                           UserType UserType2)

            => UserType1.CompareTo(UserType2) >= 0;

        #endregion

        #endregion

        #region IComparable<UserType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two UserTypes.
        /// </summary>
        /// <param name="Object">An UserType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is UserType userType
                   ? CompareTo(userType)
                   : throw new ArgumentException("The given object is not an UserType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(UserType)

        /// <summary>
        /// Compares two UserTypes.
        /// </summary>
        /// <param name="UserType">An UserType to compare with.</param>
        public Int32 CompareTo(UserType UserType)

            => String.Compare(InternalId,
                              UserType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<UserType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two UserTypes for equality.
        /// </summary>
        /// <param name="Object">An UserType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is UserType userType &&
                   Equals(userType);

        #endregion

        #region Equals(UserType)

        /// <summary>
        /// Compares two UserTypes for equality.
        /// </summary>
        /// <param name="UserType">An UserType to compare with.</param>
        public Boolean Equals(UserType UserType)

            => String.Equals(InternalId,
                             UserType.InternalId,
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
