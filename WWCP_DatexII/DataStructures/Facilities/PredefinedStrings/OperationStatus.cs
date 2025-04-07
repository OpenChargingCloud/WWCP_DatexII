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
    /// Extension methods for OperationStatus.
    /// </summary>
    public static class OperationStatusExtensions
    {

        /// <summary>
        /// Indicates whether this OperationStatus is null or empty.
        /// </summary>
        /// <param name="OperationStatus">An OperationStatus.</param>
        public static Boolean IsNullOrEmpty(this OperationStatus? OperationStatus)
            => !OperationStatus.HasValue || OperationStatus.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this OperationStatus is null or empty.
        /// </summary>
        /// <param name="OperationStatus">An OperationStatus.</param>
        public static Boolean IsNotNullOrEmpty(this OperationStatus? OperationStatus)
            => OperationStatus.HasValue && OperationStatus.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An OperationStatus.
    /// </summary>
    public readonly struct OperationStatus : IId,
                                             IEquatable<OperationStatus>,
                                             IComparable<OperationStatus>
    {

        #region Data

        private readonly static Dictionary<String, OperationStatus>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this OperationStatus is null or empty.
        /// </summary>
        public readonly  Boolean                     IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this OperationStatus is NOT null or empty.
        /// </summary>
        public readonly  Boolean                     IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the OperationStatus.
        /// </summary>
        public readonly  UInt64                      Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered OperationStatus.
        /// </summary>
        public static    IEnumerable<OperationStatus>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new OperationStatus based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an OperationStatus.</param>
        private OperationStatus(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static OperationStatus Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new OperationStatus(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an OperationStatus.
        /// </summary>
        /// <param name="Text">A text representation of an OperationStatus.</param>
        public static OperationStatus Parse(String Text)
        {

            if (TryParse(Text, out var operationStatus))
                return operationStatus;

            throw new ArgumentException($"Invalid text representation of an OperationStatus: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an OperationStatus.
        /// </summary>
        /// <param name="Text">A text representation of an OperationStatus.</param>
        public static OperationStatus? TryParse(String Text)
        {

            if (TryParse(Text, out var operationStatus))
                return operationStatus;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out OperationStatus)

        /// <summary>
        /// Try to parse the given text as an OperationStatus.
        /// </summary>
        /// <param name="Text">A text representation of an OperationStatus.</param>
        /// <param name="OperationStatus">The parsed OperationStatus.</param>
        public static Boolean TryParse(String Text, out OperationStatus OperationStatus)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out OperationStatus))
                    OperationStatus = Register(Text);

                return true;

            }

            OperationStatus = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this OperationStatus.
        /// </summary>
        public OperationStatus Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// The specified element is in operation right now.
        /// </summary>
        public static OperationStatus  InOperation               { get; }
            = Register("inOperation");

        /// <summary>
        /// The specified element is in operation on a limited basis.
        /// </summary>
        public static OperationStatus  LimitedOperation          { get; }
            = Register("limitedOperation");

        /// <summary>
        /// The specified element is not operating right now.
        /// </summary>
        public static OperationStatus  NotInOperation            { get; }
            = Register("notInOperation");

        /// <summary>
        /// The specified element is not operating due to abnormal conditions (holidays, restoration-works, long-term closure, etc.).
        /// </summary>
        public static OperationStatus  NotInOperationAbnormal    { get; }
            = Register("notInOperationAbnormal");

        /// <summary>
        /// The specified element is not in operation due to a technical defect.
        /// </summary>
        public static OperationStatus  TechnicalDefect           { get; }
            = Register("technicalDefect");

        /// <summary>
        /// There is no information about the operation status.
        /// </summary>
        public static OperationStatus  Unknown                   { get; }
            = Register("unknown");

        #endregion


        #region Operator overloading

        #region Operator == (OperationStatus1, OperationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OperationStatus1">An OperationStatus.</param>
        /// <param name="OperationStatus2">Another OperationStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (OperationStatus OperationStatus1,
                                           OperationStatus OperationStatus2)

            => OperationStatus1.Equals(OperationStatus2);

        #endregion

        #region Operator != (OperationStatus1, OperationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OperationStatus1">An OperationStatus.</param>
        /// <param name="OperationStatus2">Another OperationStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (OperationStatus OperationStatus1,
                                           OperationStatus OperationStatus2)

            => !OperationStatus1.Equals(OperationStatus2);

        #endregion

        #region Operator <  (OperationStatus1, OperationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OperationStatus1">An OperationStatus.</param>
        /// <param name="OperationStatus2">Another OperationStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (OperationStatus OperationStatus1,
                                          OperationStatus OperationStatus2)

            => OperationStatus1.CompareTo(OperationStatus2) < 0;

        #endregion

        #region Operator <= (OperationStatus1, OperationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OperationStatus1">An OperationStatus.</param>
        /// <param name="OperationStatus2">Another OperationStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (OperationStatus OperationStatus1,
                                           OperationStatus OperationStatus2)

            => OperationStatus1.CompareTo(OperationStatus2) <= 0;

        #endregion

        #region Operator >  (OperationStatus1, OperationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OperationStatus1">An OperationStatus.</param>
        /// <param name="OperationStatus2">Another OperationStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (OperationStatus OperationStatus1,
                                          OperationStatus OperationStatus2)

            => OperationStatus1.CompareTo(OperationStatus2) > 0;

        #endregion

        #region Operator >= (OperationStatus1, OperationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OperationStatus1">An OperationStatus.</param>
        /// <param name="OperationStatus2">Another OperationStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (OperationStatus OperationStatus1,
                                           OperationStatus OperationStatus2)

            => OperationStatus1.CompareTo(OperationStatus2) >= 0;

        #endregion

        #endregion

        #region IComparable<OperationStatus> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two OperationStatus.
        /// </summary>
        /// <param name="Object">An OperationStatus to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is OperationStatus operationStatus
                   ? CompareTo(operationStatus)
                   : throw new ArgumentException("The given object is not an OperationStatus!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(OperationStatus)

        /// <summary>
        /// Compares two OperationStatus.
        /// </summary>
        /// <param name="OperationStatus">An OperationStatus to compare with.</param>
        public Int32 CompareTo(OperationStatus OperationStatus)

            => String.Compare(InternalId,
                              OperationStatus.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<OperationStatus> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two OperationStatus for equality.
        /// </summary>
        /// <param name="Object">An OperationStatus to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is OperationStatus operationStatus &&
                   Equals(operationStatus);

        #endregion

        #region Equals(OperationStatus)

        /// <summary>
        /// Compares two OperationStatus for equality.
        /// </summary>
        /// <param name="OperationStatus">An OperationStatus to compare with.</param>
        public Boolean Equals(OperationStatus OperationStatus)

            => String.Equals(InternalId,
                             OperationStatus.InternalId,
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
