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
    /// Extension methods for RefillPointStatus.
    /// </summary>
    public static class RefillPointStatusEnumExtensions
    {

        /// <summary>
        /// Indicates whether this RefillPointStatus is null or empty.
        /// </summary>
        /// <param name="RefillPointStatusEnum">A RefillPointStatus.</param>
        public static Boolean IsNullOrEmpty(this RefillPointStatusEnum? RefillPointStatusEnum)
            => !RefillPointStatusEnum.HasValue || RefillPointStatusEnum.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this RefillPointStatus is null or empty.
        /// </summary>
        /// <param name="RefillPointStatusEnum">A RefillPointStatus.</param>
        public static Boolean IsNotNullOrEmpty(this RefillPointStatusEnum? RefillPointStatusEnum)
            => RefillPointStatusEnum.HasValue && RefillPointStatusEnum.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A RefillPointStatus.
    /// </summary>
    public readonly struct RefillPointStatusEnum : IId,
                                                   IEquatable<RefillPointStatusEnum>,
                                                   IComparable<RefillPointStatusEnum>
    {

        #region Data

        private readonly static Dictionary<String, RefillPointStatusEnum>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                                     InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this RefillPointStatus is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this RefillPointStatus is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the RefillPointStatus.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered RefillPointStatus.
        /// </summary>
        public static    IEnumerable<RefillPointStatusEnum>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new RefillPointStatus based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a RefillPointStatus.</param>
        private RefillPointStatusEnum(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static RefillPointStatusEnum Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new RefillPointStatusEnum(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a RefillPointStatus.
        /// </summary>
        /// <param name="Text">A text representation of a RefillPointStatus.</param>
        public static RefillPointStatusEnum Parse(String Text)
        {

            if (TryParse(Text, out var refillPointStatus))
                return refillPointStatus;

            throw new ArgumentException($"Invalid text representation of a RefillPointStatus: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a RefillPointStatus.
        /// </summary>
        /// <param name="Text">A text representation of a RefillPointStatus.</param>
        public static RefillPointStatusEnum? TryParse(String Text)
        {

            if (TryParse(Text, out var refillPointStatus))
                return refillPointStatus;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out RefillPointStatusEnum)

        /// <summary>
        /// Try to parse the given text as a RefillPointStatus.
        /// </summary>
        /// <param name="Text">A text representation of a RefillPointStatus.</param>
        /// <param name="RefillPointStatusEnum">The parsed RefillPointStatus.</param>
        public static Boolean TryParse(String Text, out RefillPointStatusEnum RefillPointStatusEnum)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out RefillPointStatusEnum))
                    RefillPointStatusEnum = Register(Text);

                return true;

            }

            RefillPointStatusEnum = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this RefillPointStatus.
        /// </summary>
        public RefillPointStatusEnum Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// For internal use only of the recipient organisation.
        /// </summary>
        public static RefillPointStatusEnum  InternalUse                                   { get; }
            = Register("internalUse");

        /// <summary>
        /// The refill point is not occupied, has got enough energy resources and can be used.
        /// </summary>
        public static RefillPointStatusEnum  Available                                   { get; }
            = Register("available");

        /// <summary>
        /// The refill point is not accessible because of a physical barrier, e.g. a car.
        /// </summary>
        public static RefillPointStatusEnum  Blocked                                   { get; }
            = Register("blocked");

        /// <summary>
        /// The refill point is currently in use for charging.
        /// </summary>
        public static RefillPointStatusEnum  Charging                                   { get; }
            = Register("charging");

        /// <summary>
        /// The refill point has got a fault.
        /// </summary>
        public static RefillPointStatusEnum  Faulted                                   { get; }
            = Register("faulted");

        /// <summary>
        /// The refill point is not yet active or it is no longer available (deleted).
        /// </summary>
        public static RefillPointStatusEnum  Inoperative    { get; }
            = Register("inoperative");

        /// <summary>
        /// The refill point is in use, this might include vehicle charging activity.
        /// </summary>
        public static RefillPointStatusEnum  Occupied       { get; }
            = Register("occupied");

        /// <summary>
        /// The refill point is currently out of order.
        /// </summary>
        public static RefillPointStatusEnum  OutOfOrder     { get; }
            = Register("outOfOrder");

        /// <summary>
        /// The refill point is out of stock, i.e. energy resources are empty.
        /// </summary>
        public static RefillPointStatusEnum  OutOfStock     { get; }
            = Register("outOfStock");

        /// <summary>
        /// The refill point is planned, will be operating soon.
        /// </summary>
        public static RefillPointStatusEnum  Planned        { get; }
            = Register("planned");

        /// <summary>
        /// The refill point was discontinued/removed.
        /// </summary>
        public static RefillPointStatusEnum  Removed        { get; }
            = Register("removed");

        /// <summary>
        /// The refill point is reserved by a customer, i.e. it is not available for other users right now.
        /// </summary>
        public static RefillPointStatusEnum  Reserved       { get; }
            = Register("reserved");

        /// <summary>
        /// There is no energy available at this refill point.
        /// </summary>
        public static RefillPointStatusEnum  Unavailable    { get; }
            = Register("unavailable");

        /// <summary>
        /// The status of the refill point is unknown (can also be offline).
        /// </summary>
        public static RefillPointStatusEnum  Unknown        { get; }
            = Register("unknown");

        #endregion


        #region Operator overloading

        #region Operator == (RefillPointStatusEnum1, RefillPointStatusEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RefillPointStatusEnum1">A RefillPointStatus.</param>
        /// <param name="RefillPointStatusEnum2">Another RefillPointStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (RefillPointStatusEnum RefillPointStatusEnum1,
                                           RefillPointStatusEnum RefillPointStatusEnum2)

            => RefillPointStatusEnum1.Equals(RefillPointStatusEnum2);

        #endregion

        #region Operator != (RefillPointStatusEnum1, RefillPointStatusEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RefillPointStatusEnum1">A RefillPointStatus.</param>
        /// <param name="RefillPointStatusEnum2">Another RefillPointStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (RefillPointStatusEnum RefillPointStatusEnum1,
                                           RefillPointStatusEnum RefillPointStatusEnum2)

            => !RefillPointStatusEnum1.Equals(RefillPointStatusEnum2);

        #endregion

        #region Operator <  (RefillPointStatusEnum1, RefillPointStatusEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RefillPointStatusEnum1">A RefillPointStatus.</param>
        /// <param name="RefillPointStatusEnum2">Another RefillPointStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (RefillPointStatusEnum RefillPointStatusEnum1,
                                          RefillPointStatusEnum RefillPointStatusEnum2)

            => RefillPointStatusEnum1.CompareTo(RefillPointStatusEnum2) < 0;

        #endregion

        #region Operator <= (RefillPointStatusEnum1, RefillPointStatusEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RefillPointStatusEnum1">A RefillPointStatus.</param>
        /// <param name="RefillPointStatusEnum2">Another RefillPointStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (RefillPointStatusEnum RefillPointStatusEnum1,
                                           RefillPointStatusEnum RefillPointStatusEnum2)

            => RefillPointStatusEnum1.CompareTo(RefillPointStatusEnum2) <= 0;

        #endregion

        #region Operator >  (RefillPointStatusEnum1, RefillPointStatusEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RefillPointStatusEnum1">A RefillPointStatus.</param>
        /// <param name="RefillPointStatusEnum2">Another RefillPointStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (RefillPointStatusEnum RefillPointStatusEnum1,
                                          RefillPointStatusEnum RefillPointStatusEnum2)

            => RefillPointStatusEnum1.CompareTo(RefillPointStatusEnum2) > 0;

        #endregion

        #region Operator >= (RefillPointStatusEnum1, RefillPointStatusEnum2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RefillPointStatusEnum1">A RefillPointStatus.</param>
        /// <param name="RefillPointStatusEnum2">Another RefillPointStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (RefillPointStatusEnum RefillPointStatusEnum1,
                                           RefillPointStatusEnum RefillPointStatusEnum2)

            => RefillPointStatusEnum1.CompareTo(RefillPointStatusEnum2) >= 0;

        #endregion

        #endregion

        #region IComparable<RefillPointStatusEnum> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two RefillPointStatus.
        /// </summary>
        /// <param name="Object">A RefillPointStatus to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is RefillPointStatusEnum refillPointStatus
                   ? CompareTo(refillPointStatus)
                   : throw new ArgumentException("The given object is not a RefillPointStatus!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(RefillPointStatusEnum)

        /// <summary>
        /// Compares two RefillPointStatus.
        /// </summary>
        /// <param name="RefillPointStatusEnum">A RefillPointStatus to compare with.</param>
        public Int32 CompareTo(RefillPointStatusEnum RefillPointStatusEnum)

            => String.Compare(InternalId,
                              RefillPointStatusEnum.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<RefillPointStatusEnum> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two RefillPointStatus for equality.
        /// </summary>
        /// <param name="Object">A RefillPointStatus to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is RefillPointStatusEnum refillPointStatus &&
                   Equals(refillPointStatus);

        #endregion

        #region Equals(RefillPointStatusEnum)

        /// <summary>
        /// Compares two RefillPointStatus for equality.
        /// </summary>
        /// <param name="RefillPointStatusEnum">A RefillPointStatus to compare with.</param>
        public Boolean Equals(RefillPointStatusEnum RefillPointStatusEnum)

            => String.Equals(InternalId,
                             RefillPointStatusEnum.InternalId,
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
