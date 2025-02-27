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
    /// Extension methods for Availabilities.
    /// </summary>
    public static class AvailabilityExtensions
    {

        /// <summary>
        /// Indicates whether this Availability is null or empty.
        /// </summary>
        /// <param name="Availability">An Availability.</param>
        public static Boolean IsNullOrEmpty(this Availability? Availability)
            => !Availability.HasValue || Availability.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this Availability is null or empty.
        /// </summary>
        /// <param name="Availability">An Availability.</param>
        public static Boolean IsNotNullOrEmpty(this Availability? Availability)
            => Availability.HasValue && Availability.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An Availability.
    /// </summary>
    public readonly struct Availability : IId,
                                          IEquatable<Availability>,
                                          IComparable<Availability>
    {

        #region Data

        private readonly static Dictionary<String, Availability>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this Availability is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this Availability is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the Availability.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered Availabilitys.
        /// </summary>
        public static    IEnumerable<Availability>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new Availability based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an Availability.</param>
        private Availability(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static Availability Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new Availability(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an Availability.
        /// </summary>
        /// <param name="Text">A text representation of an Availability.</param>
        public static Availability Parse(String Text)
        {

            if (TryParse(Text, out var availability))
                return availability;

            throw new ArgumentException($"Invalid text representation of an Availability: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an Availability.
        /// </summary>
        /// <param name="Text">A text representation of an Availability.</param>
        public static Availability? TryParse(String Text)
        {

            if (TryParse(Text, out var availability))
                return availability;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out Availability)

        /// <summary>
        /// Try to parse the given text as an Availability.
        /// </summary>
        /// <param name="Text">A text representation of an Availability.</param>
        /// <param name="Availability">The parsed Availability.</param>
        public static Boolean TryParse(String Text, out Availability Availability)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out Availability))
                    Availability = Register(Text);

                return true;

            }

            Availability = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this Availability.
        /// </summary>
        public Availability Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// The element in question is available.
        /// </summary>
        public static Availability  Available       { get; }
            = Register("available");

        /// <summary>
        /// The element in question is not available.
        /// </summary>
        public static Availability  NotAvailable    { get; }
            = Register("notAvailable");

        /// <summary>
        /// There is no information about whether the element in question is available or not.
        /// </summary>
        public static Availability  Unknown         { get; }
            = Register("unknown");

        #endregion


        #region Operator overloading

        #region Operator == (Availability1, Availability2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Availability1">An Availability.</param>
        /// <param name="Availability2">Another Availability.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Availability Availability1,
                                           Availability Availability2)

            => Availability1.Equals(Availability2);

        #endregion

        #region Operator != (Availability1, Availability2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Availability1">An Availability.</param>
        /// <param name="Availability2">Another Availability.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Availability Availability1,
                                           Availability Availability2)

            => !Availability1.Equals(Availability2);

        #endregion

        #region Operator <  (Availability1, Availability2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Availability1">An Availability.</param>
        /// <param name="Availability2">Another Availability.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Availability Availability1,
                                          Availability Availability2)

            => Availability1.CompareTo(Availability2) < 0;

        #endregion

        #region Operator <= (Availability1, Availability2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Availability1">An Availability.</param>
        /// <param name="Availability2">Another Availability.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Availability Availability1,
                                           Availability Availability2)

            => Availability1.CompareTo(Availability2) <= 0;

        #endregion

        #region Operator >  (Availability1, Availability2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Availability1">An Availability.</param>
        /// <param name="Availability2">Another Availability.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Availability Availability1,
                                          Availability Availability2)

            => Availability1.CompareTo(Availability2) > 0;

        #endregion

        #region Operator >= (Availability1, Availability2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Availability1">An Availability.</param>
        /// <param name="Availability2">Another Availability.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Availability Availability1,
                                           Availability Availability2)

            => Availability1.CompareTo(Availability2) >= 0;

        #endregion

        #endregion

        #region IComparable<Availability> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two Availabilitys.
        /// </summary>
        /// <param name="Object">An Availability to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is Availability availability
                   ? CompareTo(availability)
                   : throw new ArgumentException("The given object is not an Availability!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(Availability)

        /// <summary>
        /// Compares two Availabilitys.
        /// </summary>
        /// <param name="Availability">An Availability to compare with.</param>
        public Int32 CompareTo(Availability Availability)

            => String.Compare(InternalId,
                              Availability.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<Availability> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two Availabilitys for equality.
        /// </summary>
        /// <param name="Object">An Availability to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Availability availability &&
                   Equals(availability);

        #endregion

        #region Equals(Availability)

        /// <summary>
        /// Compares two Availabilitys for equality.
        /// </summary>
        /// <param name="Availability">An Availability to compare with.</param>
        public Boolean Equals(Availability Availability)

            => String.Equals(InternalId,
                             Availability.InternalId,
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
