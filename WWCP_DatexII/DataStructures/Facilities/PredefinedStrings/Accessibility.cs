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
    /// Extension methods for Accessibilities.
    /// </summary>
    public static class AccessibilityExtensions
    {

        /// <summary>
        /// Indicates whether this Accessibility is null or empty.
        /// </summary>
        /// <param name="Accessibility">An Accessibility.</param>
        public static Boolean IsNullOrEmpty(this Accessibility? Accessibility)
            => !Accessibility.HasValue || Accessibility.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this Accessibility is null or empty.
        /// </summary>
        /// <param name="Accessibility">An Accessibility.</param>
        public static Boolean IsNotNullOrEmpty(this Accessibility? Accessibility)
            => Accessibility.HasValue && Accessibility.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An Accessibility.
    /// </summary>
    public readonly struct Accessibility : IId,
                                           IEquatable<Accessibility>,
                                           IComparable<Accessibility>
    {

        #region Data

        private readonly static Dictionary<String, Accessibility>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this Accessibility is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this Accessibility is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the Accessibility.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered Accessibilitys.
        /// </summary>
        public static    IEnumerable<Accessibility>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new Accessibility based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an Accessibility.</param>
        private Accessibility(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static Accessibility Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new Accessibility(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an Accessibility.
        /// </summary>
        /// <param name="Text">A text representation of an Accessibility.</param>
        public static Accessibility Parse(String Text)
        {

            if (TryParse(Text, out var accessibility))
                return accessibility;

            throw new ArgumentException($"Invalid text representation of an Accessibility: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an Accessibility.
        /// </summary>
        /// <param name="Text">A text representation of an Accessibility.</param>
        public static Accessibility? TryParse(String Text)
        {

            if (TryParse(Text, out var accessibility))
                return accessibility;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out Accessibility)

        /// <summary>
        /// Try to parse the given text as an Accessibility.
        /// </summary>
        /// <param name="Text">A text representation of an Accessibility.</param>
        /// <param name="Accessibility">The parsed Accessibility.</param>
        public static Boolean TryParse(String Text, out Accessibility Accessibility)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out Accessibility))
                    Accessibility = Register(Text);

                return true;

            }

            Accessibility = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this Accessibility.
        /// </summary>
        public Accessibility Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Barrier Free Accessible
        /// </summary>
        public static Accessibility  BarrierFreeAccessible              { get; }
            = Register("barrierFreeAccessible");

        /// <summary>
        /// Disability Accessible
        /// </summary>
        public static Accessibility  DisabilityAccessible               { get; }
            = Register("disabilityAccessible");

        /// <summary>
        /// Wheel Chair Accessible
        /// </summary>
        public static Accessibility  WheelChairAccessible               { get; }
            = Register("wheelChairAccessible");

        /// <summary>
        /// Disability Facilities
        /// </summary>
        public static Accessibility  DisabilityFacilities               { get; }
            = Register("disabilityFacilities");

        /// <summary>
        /// Orientation System for Blind People
        /// </summary>
        public static Accessibility  OrientationSystemForBlindPeople    { get; }
            = Register("orientationSystemForBlindPeople");

        /// <summary>
        /// Marking
        /// </summary>
        public static Accessibility  Marking                            { get; }
            = Register("marking");

        /// <summary>
        /// None
        /// </summary>
        public static Accessibility  None                               { get; }
            = Register("none");

        /// <summary>
        /// Unknown
        /// </summary>
        public static Accessibility  Unknown                            { get; }
            = Register("unknown");

        /// <summary>
        /// Other
        /// </summary>
        public static Accessibility  Other                              { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (Accessibility1, Accessibility2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Accessibility1">An Accessibility.</param>
        /// <param name="Accessibility2">Another Accessibility.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Accessibility Accessibility1,
                                           Accessibility Accessibility2)

            => Accessibility1.Equals(Accessibility2);

        #endregion

        #region Operator != (Accessibility1, Accessibility2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Accessibility1">An Accessibility.</param>
        /// <param name="Accessibility2">Another Accessibility.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Accessibility Accessibility1,
                                           Accessibility Accessibility2)

            => !Accessibility1.Equals(Accessibility2);

        #endregion

        #region Operator <  (Accessibility1, Accessibility2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Accessibility1">An Accessibility.</param>
        /// <param name="Accessibility2">Another Accessibility.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Accessibility Accessibility1,
                                          Accessibility Accessibility2)

            => Accessibility1.CompareTo(Accessibility2) < 0;

        #endregion

        #region Operator <= (Accessibility1, Accessibility2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Accessibility1">An Accessibility.</param>
        /// <param name="Accessibility2">Another Accessibility.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Accessibility Accessibility1,
                                           Accessibility Accessibility2)

            => Accessibility1.CompareTo(Accessibility2) <= 0;

        #endregion

        #region Operator >  (Accessibility1, Accessibility2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Accessibility1">An Accessibility.</param>
        /// <param name="Accessibility2">Another Accessibility.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Accessibility Accessibility1,
                                          Accessibility Accessibility2)

            => Accessibility1.CompareTo(Accessibility2) > 0;

        #endregion

        #region Operator >= (Accessibility1, Accessibility2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Accessibility1">An Accessibility.</param>
        /// <param name="Accessibility2">Another Accessibility.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Accessibility Accessibility1,
                                           Accessibility Accessibility2)

            => Accessibility1.CompareTo(Accessibility2) >= 0;

        #endregion

        #endregion

        #region IComparable<Accessibility> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two Accessibilitys.
        /// </summary>
        /// <param name="Object">An Accessibility to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is Accessibility accessibility
                   ? CompareTo(accessibility)
                   : throw new ArgumentException("The given object is not an Accessibility!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(Accessibility)

        /// <summary>
        /// Compares two Accessibilitys.
        /// </summary>
        /// <param name="Accessibility">An Accessibility to compare with.</param>
        public Int32 CompareTo(Accessibility Accessibility)

            => String.Compare(InternalId,
                              Accessibility.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<Accessibility> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two Accessibilitys for equality.
        /// </summary>
        /// <param name="Object">An Accessibility to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Accessibility accessibility &&
                   Equals(accessibility);

        #endregion

        #region Equals(Accessibility)

        /// <summary>
        /// Compares two Accessibilitys for equality.
        /// </summary>
        /// <param name="Accessibility">An Accessibility to compare with.</param>
        public Boolean Equals(Accessibility Accessibility)

            => String.Equals(InternalId,
                             Accessibility.InternalId,
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
