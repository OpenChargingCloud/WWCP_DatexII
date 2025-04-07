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
    /// Extension methods for EnergyInfrastructureSiteType.
    /// </summary>
    public static class EnergyInfrastructureSiteTypeExtensions
    {

        /// <summary>
        /// Indicates whether this EnergyInfrastructureSiteType is null or empty.
        /// </summary>
        /// <param name="EnergyInfrastructureSiteType">An EnergyInfrastructureSiteType.</param>
        public static Boolean IsNullOrEmpty(this EnergyInfrastructureSiteType? EnergyInfrastructureSiteType)
            => !EnergyInfrastructureSiteType.HasValue || EnergyInfrastructureSiteType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this EnergyInfrastructureSiteType is null or empty.
        /// </summary>
        /// <param name="EnergyInfrastructureSiteType">An EnergyInfrastructureSiteType.</param>
        public static Boolean IsNotNullOrEmpty(this EnergyInfrastructureSiteType? EnergyInfrastructureSiteType)
            => EnergyInfrastructureSiteType.HasValue && EnergyInfrastructureSiteType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An EnergyInfrastructureSiteType.
    /// </summary>
    public readonly struct EnergyInfrastructureSiteType : IId,
                                                          IEquatable<EnergyInfrastructureSiteType>,
                                                          IComparable<EnergyInfrastructureSiteType>
    {

        #region Data

        private readonly static Dictionary<String, EnergyInfrastructureSiteType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this EnergyInfrastructureSiteType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this EnergyInfrastructureSiteType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the EnergyInfrastructureSiteType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered EnergyInfrastructureSiteTypes.
        /// </summary>
        public static    IEnumerable<EnergyInfrastructureSiteType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new EnergyInfrastructureSiteType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an EnergyInfrastructureSiteType.</param>
        private EnergyInfrastructureSiteType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static EnergyInfrastructureSiteType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new EnergyInfrastructureSiteType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an EnergyInfrastructureSiteType.
        /// </summary>
        /// <param name="Text">A text representation of an EnergyInfrastructureSiteType.</param>
        public static EnergyInfrastructureSiteType Parse(String Text)
        {

            if (TryParse(Text, out var energyInfrastructureSiteType))
                return energyInfrastructureSiteType;

            throw new ArgumentException($"Invalid text representation of an EnergyInfrastructureSiteType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an EnergyInfrastructureSiteType.
        /// </summary>
        /// <param name="Text">A text representation of an EnergyInfrastructureSiteType.</param>
        public static EnergyInfrastructureSiteType? TryParse(String Text)
        {

            if (TryParse(Text, out var energyInfrastructureSiteType))
                return energyInfrastructureSiteType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out EnergyInfrastructureSiteType)

        /// <summary>
        /// Try to parse the given text as an EnergyInfrastructureSiteType.
        /// </summary>
        /// <param name="Text">A text representation of an EnergyInfrastructureSiteType.</param>
        /// <param name="EnergyInfrastructureSiteType">The parsed EnergyInfrastructureSiteType.</param>
        public static Boolean TryParse(String Text, out EnergyInfrastructureSiteType EnergyInfrastructureSiteType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out EnergyInfrastructureSiteType))
                    EnergyInfrastructureSiteType = Register(Text);

                return true;

            }

            EnergyInfrastructureSiteType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this EnergyInfrastructureSiteType.
        /// </summary>
        public EnergyInfrastructureSiteType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// The energy infrastructure site is located inside some building, for example in a car park.
        /// </summary>
        public static EnergyInfrastructureSiteType  InBuilding       { get; }
            = Register("inBuilding");

        /// <summary>
        /// The energy infrastructure site is an open space, for example a parking site.
        /// </summary>
        public static EnergyInfrastructureSiteType  OpenSpace        { get; }
            = Register("openSpace");

        /// <summary>
        /// The energy infrastructure site is located alongside a street, for example some singular charging stations.
        /// </summary>
        public static EnergyInfrastructureSiteType  OnStreet         { get; }
            = Register("onStreet");

        /// <summary>
        /// The energy infrastructure site is located on a company site.
        /// </summary>
        public static EnergyInfrastructureSiteType  OnCompanySite    { get; }
            = Register("onCompanySite");

        /// <summary>
        /// Other types of energy infrastructure sites.
        /// </summary>
        public static EnergyInfrastructureSiteType  Other            { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (EnergyInfrastructureSiteType1, EnergyInfrastructureSiteType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EnergyInfrastructureSiteType1">An EnergyInfrastructureSiteType.</param>
        /// <param name="EnergyInfrastructureSiteType2">Another EnergyInfrastructureSiteType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (EnergyInfrastructureSiteType EnergyInfrastructureSiteType1,
                                           EnergyInfrastructureSiteType EnergyInfrastructureSiteType2)

            => EnergyInfrastructureSiteType1.Equals(EnergyInfrastructureSiteType2);

        #endregion

        #region Operator != (EnergyInfrastructureSiteType1, EnergyInfrastructureSiteType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EnergyInfrastructureSiteType1">An EnergyInfrastructureSiteType.</param>
        /// <param name="EnergyInfrastructureSiteType2">Another EnergyInfrastructureSiteType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (EnergyInfrastructureSiteType EnergyInfrastructureSiteType1,
                                           EnergyInfrastructureSiteType EnergyInfrastructureSiteType2)

            => !EnergyInfrastructureSiteType1.Equals(EnergyInfrastructureSiteType2);

        #endregion

        #region Operator <  (EnergyInfrastructureSiteType1, EnergyInfrastructureSiteType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EnergyInfrastructureSiteType1">An EnergyInfrastructureSiteType.</param>
        /// <param name="EnergyInfrastructureSiteType2">Another EnergyInfrastructureSiteType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (EnergyInfrastructureSiteType EnergyInfrastructureSiteType1,
                                          EnergyInfrastructureSiteType EnergyInfrastructureSiteType2)

            => EnergyInfrastructureSiteType1.CompareTo(EnergyInfrastructureSiteType2) < 0;

        #endregion

        #region Operator <= (EnergyInfrastructureSiteType1, EnergyInfrastructureSiteType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EnergyInfrastructureSiteType1">An EnergyInfrastructureSiteType.</param>
        /// <param name="EnergyInfrastructureSiteType2">Another EnergyInfrastructureSiteType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (EnergyInfrastructureSiteType EnergyInfrastructureSiteType1,
                                           EnergyInfrastructureSiteType EnergyInfrastructureSiteType2)

            => EnergyInfrastructureSiteType1.CompareTo(EnergyInfrastructureSiteType2) <= 0;

        #endregion

        #region Operator >  (EnergyInfrastructureSiteType1, EnergyInfrastructureSiteType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EnergyInfrastructureSiteType1">An EnergyInfrastructureSiteType.</param>
        /// <param name="EnergyInfrastructureSiteType2">Another EnergyInfrastructureSiteType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (EnergyInfrastructureSiteType EnergyInfrastructureSiteType1,
                                          EnergyInfrastructureSiteType EnergyInfrastructureSiteType2)

            => EnergyInfrastructureSiteType1.CompareTo(EnergyInfrastructureSiteType2) > 0;

        #endregion

        #region Operator >= (EnergyInfrastructureSiteType1, EnergyInfrastructureSiteType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EnergyInfrastructureSiteType1">An EnergyInfrastructureSiteType.</param>
        /// <param name="EnergyInfrastructureSiteType2">Another EnergyInfrastructureSiteType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (EnergyInfrastructureSiteType EnergyInfrastructureSiteType1,
                                           EnergyInfrastructureSiteType EnergyInfrastructureSiteType2)

            => EnergyInfrastructureSiteType1.CompareTo(EnergyInfrastructureSiteType2) >= 0;

        #endregion

        #endregion

        #region IComparable<EnergyInfrastructureSiteType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two EnergyInfrastructureSiteTypes.
        /// </summary>
        /// <param name="Object">An EnergyInfrastructureSiteType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is EnergyInfrastructureSiteType energyInfrastructureSiteType
                   ? CompareTo(energyInfrastructureSiteType)
                   : throw new ArgumentException("The given object is not an EnergyInfrastructureSiteType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(EnergyInfrastructureSiteType)

        /// <summary>
        /// Compares two EnergyInfrastructureSiteTypes.
        /// </summary>
        /// <param name="EnergyInfrastructureSiteType">An EnergyInfrastructureSiteType to compare with.</param>
        public Int32 CompareTo(EnergyInfrastructureSiteType EnergyInfrastructureSiteType)

            => String.Compare(InternalId,
                              EnergyInfrastructureSiteType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<EnergyInfrastructureSiteType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two EnergyInfrastructureSiteTypes for equality.
        /// </summary>
        /// <param name="Object">An EnergyInfrastructureSiteType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is EnergyInfrastructureSiteType energyInfrastructureSiteType &&
                   Equals(energyInfrastructureSiteType);

        #endregion

        #region Equals(EnergyInfrastructureSiteType)

        /// <summary>
        /// Compares two EnergyInfrastructureSiteTypes for equality.
        /// </summary>
        /// <param name="EnergyInfrastructureSiteType">An EnergyInfrastructureSiteType to compare with.</param>
        public Boolean Equals(EnergyInfrastructureSiteType EnergyInfrastructureSiteType)

            => String.Equals(InternalId,
                             EnergyInfrastructureSiteType.InternalId,
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
