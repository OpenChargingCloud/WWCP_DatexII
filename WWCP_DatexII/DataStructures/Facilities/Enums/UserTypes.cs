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

using System.Xml.Serialization;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Types of different users, for example used in the context of parking.
    /// </summary>
    public enum UserTypes
    {

        /// <summary>
        /// All users.
        /// </summary>
        [XmlEnum("allUsers")]
        AllUsers,

        /// <summary>
        /// Commuters.
        /// </summary>
        [XmlEnum("commuters")]
        Commuters,

        /// <summary>
        /// Customers.
        /// </summary>
        [XmlEnum("customers")]
        Customers,

        /// <summary>
        /// Elderly users.
        /// </summary>
        [XmlEnum("elderlyUsers")]
        ElderlyUsers,

        /// <summary>
        /// Employees.
        /// </summary>
        [XmlEnum("employees")]
        Employees,

        /// <summary>
        /// Families.
        /// </summary>
        [XmlEnum("families")]
        Families,

        /// <summary>
        /// Persons with disabilities.
        /// </summary>
        [XmlEnum("personsWithDisabilities")]
        PersonsWithDisabilities,

        /// <summary>
        /// People with hearing loss.
        /// </summary>
        [XmlEnum("hearingImpaired")]
        HearingImpaired,

        /// <summary>
        /// Hotel guests.
        /// </summary>
        [XmlEnum("hotelGuests")]
        HotelGuests,

        /// <summary>
        /// Long-term parker.
        /// </summary>
        [XmlEnum("longTermParkers")]
        LongTermParkers,

        /// <summary>
        /// Members.
        /// </summary>
        [XmlEnum("members")]
        Members,

        /// <summary>
        /// Men.
        /// </summary>
        [XmlEnum("men")]
        Men,

        /// <summary>
        /// Overnight parker.
        /// </summary>
        [XmlEnum("overnightParkers")]
        OvernightParkers,

        /// <summary>
        /// Park and cycle user.
        /// </summary>
        [XmlEnum("parkAndCycleUser")]
        ParkAndCycleUser,

        /// <summary>
        /// Users that are exchanging into public transport at a park and ride station.
        /// </summary>
        [XmlEnum("parkAndRideUsers")]
        ParkAndRideUsers,

        /// <summary>
        /// Park and walk user.
        /// </summary>
        [XmlEnum("parkAndWalkUser")]
        ParkAndWalkUser,

        /// <summary>
        /// Pensioners.
        /// </summary>
        [XmlEnum("pensioners")]
        Pensioners,

        /// <summary>
        /// Pregnant women.
        /// </summary>
        [XmlEnum("pregnantWomen")]
        PregnantWomen,

        /// <summary>
        /// Registered disabled persons.
        /// </summary>
        [XmlEnum("registeredDisabledUsers")]
        RegisteredDisabledUsers,

        /// <summary>
        /// Those who have a valid reservation, e.g. for the duration of parking.
        /// </summary>
        [XmlEnum("reservationHolders")]
        ReservationHolders,

        /// <summary>
        /// Local residents.
        /// </summary>
        [XmlEnum("residents")]
        Residents,

        /// <summary>
        /// Season ticket holders.
        /// </summary>
        [XmlEnum("seasonTicketHolders")]
        SeasonTicketHolders,

        /// <summary>
        /// Shoppers.
        /// </summary>
        [XmlEnum("shoppers")]
        Shoppers,

        /// <summary>
        /// Short-term parker.
        /// </summary>
        [XmlEnum("shortTermParkers")]
        ShortTermParkers,

        /// <summary>
        /// Sport event away supporters.
        /// </summary>
        [XmlEnum("sportEventAwaySupporters")]
        SportEventAwaySupporters,

        /// <summary>
        /// Sport event home supporters.
        /// </summary>
        [XmlEnum("sportEventHomeSupporters")]
        SportEventHomeSupporters,

        /// <summary>
        /// Students.
        /// </summary>
        [XmlEnum("students")]
        Students,

        /// <summary>
        /// Staff.
        /// </summary>
        [XmlEnum("staff")]
        Staff,

        /// <summary>
        /// Subscribers.
        /// </summary>
        [XmlEnum("subscribers")]
        Subscribers,

        /// <summary>
        /// Visitors.
        /// </summary>
        [XmlEnum("visitors")]
        Visitors,

        /// <summary>
        /// People with vision impairment.
        /// </summary>
        [XmlEnum("visuallyImpaired")]
        VisuallyImpaired,

        /// <summary>
        /// Wheelchair users.
        /// </summary>
        [XmlEnum("wheelchairUsers")]
        WheelchairUsers,

        /// <summary>
        /// Women.
        /// </summary>
        [XmlEnum("women")]
        Women,

        /// <summary>
        /// Unknown.
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        /// <summary>
        /// Other.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
