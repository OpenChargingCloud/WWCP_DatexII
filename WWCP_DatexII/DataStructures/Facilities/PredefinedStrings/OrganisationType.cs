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
    /// Extension methods for OrganisationTypes.
    /// </summary>
    public static class OrganisationTypeExtensions
    {

        /// <summary>
        /// Indicates whether this OrganisationType is null or empty.
        /// </summary>
        /// <param name="OrganisationType">An OrganisationType.</param>
        public static Boolean IsNullOrEmpty(this OrganisationType? OrganisationType)
            => !OrganisationType.HasValue || OrganisationType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this OrganisationType is null or empty.
        /// </summary>
        /// <param name="OrganisationType">An OrganisationType.</param>
        public static Boolean IsNotNullOrEmpty(this OrganisationType? OrganisationType)
            => OrganisationType.HasValue && OrganisationType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An OrganisationType.
    /// </summary>
    public readonly struct OrganisationType : IId,
                                              IEquatable<OrganisationType>,
                                              IComparable<OrganisationType>
    {

        #region Data

        private readonly static Dictionary<String, OrganisationType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this OrganisationType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this OrganisationType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the OrganisationType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered OrganisationTypes.
        /// </summary>
        public static    IEnumerable<OrganisationType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new OrganisationType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an OrganisationType.</param>
        private OrganisationType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static OrganisationType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new OrganisationType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an OrganisationType.
        /// </summary>
        /// <param name="Text">A text representation of an OrganisationType.</param>
        public static OrganisationType Parse(String Text)
        {

            if (TryParse(Text, out var organisationType))
                return organisationType;

            throw new ArgumentException($"Invalid text representation of an OrganisationType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an OrganisationType.
        /// </summary>
        /// <param name="Text">A text representation of an OrganisationType.</param>
        public static OrganisationType? TryParse(String Text)
        {

            if (TryParse(Text, out var organisationType))
                return organisationType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out OrganisationType)

        /// <summary>
        /// Try to parse the given text as an OrganisationType.
        /// </summary>
        /// <param name="Text">A text representation of an OrganisationType.</param>
        /// <param name="OrganisationType">The parsed OrganisationType.</param>
        public static Boolean TryParse(String Text, out OrganisationType OrganisationType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out OrganisationType))
                    OrganisationType = Register(Text);

                return true;

            }

            OrganisationType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this OrganisationType.
        /// </summary>
        public OrganisationType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Public sector organisation.
        /// </summary>
        public static OrganisationType  PublicSector     { get; }
            = Register("publicSector");

        /// <summary>
        /// Private sector organisation.
        /// </summary>
        public static OrganisationType  PrivateSector    { get; }
            = Register("privateSector");

        #endregion


        #region Operator overloading

        #region Operator == (OrganisationType1, OrganisationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OrganisationType1">An OrganisationType.</param>
        /// <param name="OrganisationType2">Another OrganisationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (OrganisationType OrganisationType1,
                                           OrganisationType OrganisationType2)

            => OrganisationType1.Equals(OrganisationType2);

        #endregion

        #region Operator != (OrganisationType1, OrganisationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OrganisationType1">An OrganisationType.</param>
        /// <param name="OrganisationType2">Another OrganisationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (OrganisationType OrganisationType1,
                                           OrganisationType OrganisationType2)

            => !OrganisationType1.Equals(OrganisationType2);

        #endregion

        #region Operator <  (OrganisationType1, OrganisationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OrganisationType1">An OrganisationType.</param>
        /// <param name="OrganisationType2">Another OrganisationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (OrganisationType OrganisationType1,
                                          OrganisationType OrganisationType2)

            => OrganisationType1.CompareTo(OrganisationType2) < 0;

        #endregion

        #region Operator <= (OrganisationType1, OrganisationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OrganisationType1">An OrganisationType.</param>
        /// <param name="OrganisationType2">Another OrganisationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (OrganisationType OrganisationType1,
                                           OrganisationType OrganisationType2)

            => OrganisationType1.CompareTo(OrganisationType2) <= 0;

        #endregion

        #region Operator >  (OrganisationType1, OrganisationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OrganisationType1">An OrganisationType.</param>
        /// <param name="OrganisationType2">Another OrganisationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (OrganisationType OrganisationType1,
                                          OrganisationType OrganisationType2)

            => OrganisationType1.CompareTo(OrganisationType2) > 0;

        #endregion

        #region Operator >= (OrganisationType1, OrganisationType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OrganisationType1">An OrganisationType.</param>
        /// <param name="OrganisationType2">Another OrganisationType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (OrganisationType OrganisationType1,
                                           OrganisationType OrganisationType2)

            => OrganisationType1.CompareTo(OrganisationType2) >= 0;

        #endregion

        #endregion

        #region IComparable<OrganisationType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two OrganisationTypes.
        /// </summary>
        /// <param name="Object">An OrganisationType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is OrganisationType organisationType
                   ? CompareTo(organisationType)
                   : throw new ArgumentException("The given object is not an OrganisationType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(OrganisationType)

        /// <summary>
        /// Compares two OrganisationTypes.
        /// </summary>
        /// <param name="OrganisationType">An OrganisationType to compare with.</param>
        public Int32 CompareTo(OrganisationType OrganisationType)

            => String.Compare(InternalId,
                              OrganisationType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<OrganisationType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two OrganisationTypes for equality.
        /// </summary>
        /// <param name="Object">An OrganisationType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is OrganisationType organisationType &&
                   Equals(organisationType);

        #endregion

        #region Equals(OrganisationType)

        /// <summary>
        /// Compares two OrganisationTypes for equality.
        /// </summary>
        /// <param name="OrganisationType">An OrganisationType to compare with.</param>
        public Boolean Equals(OrganisationType OrganisationType)

            => String.Equals(InternalId,
                             OrganisationType.InternalId,
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
