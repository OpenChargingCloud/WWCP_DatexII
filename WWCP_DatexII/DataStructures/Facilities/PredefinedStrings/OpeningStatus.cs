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
    /// Extension methods for OpeningStatus.
    /// </summary>
    public static class OpeningStatusExtensions
    {

        /// <summary>
        /// Indicates whether this OpeningStatus is null or empty.
        /// </summary>
        /// <param name="OpeningStatus">An OpeningStatus.</param>
        public static Boolean IsNullOrEmpty(this OpeningStatus? OpeningStatus)
            => !OpeningStatus.HasValue || OpeningStatus.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this OpeningStatus is null or empty.
        /// </summary>
        /// <param name="OpeningStatus">An OpeningStatus.</param>
        public static Boolean IsNotNullOrEmpty(this OpeningStatus? OpeningStatus)
            => OpeningStatus.HasValue && OpeningStatus.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An OpeningStatus.
    /// </summary>
    public readonly struct OpeningStatus : IId,
                                           IEquatable<OpeningStatus>,
                                           IComparable<OpeningStatus>
    {

        #region Data

        private readonly static Dictionary<String, OpeningStatus>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this OpeningStatus is null or empty.
        /// </summary>
        public readonly  Boolean                     IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this OpeningStatus is NOT null or empty.
        /// </summary>
        public readonly  Boolean                     IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the OpeningStatus.
        /// </summary>
        public readonly  UInt64                      Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered OpeningStatus.
        /// </summary>
        public static    IEnumerable<OpeningStatus>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new OpeningStatus based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an OpeningStatus.</param>
        private OpeningStatus(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static OpeningStatus Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new OpeningStatus(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an OpeningStatus.
        /// </summary>
        /// <param name="Text">A text representation of an OpeningStatus.</param>
        public static OpeningStatus Parse(String Text)
        {

            if (TryParse(Text, out var openingStatus))
                return openingStatus;

            throw new ArgumentException($"Invalid text representation of an OpeningStatus: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an OpeningStatus.
        /// </summary>
        /// <param name="Text">A text representation of an OpeningStatus.</param>
        public static OpeningStatus? TryParse(String Text)
        {

            if (TryParse(Text, out var openingStatus))
                return openingStatus;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out OpeningStatus)

        /// <summary>
        /// Try to parse the given text as an OpeningStatus.
        /// </summary>
        /// <param name="Text">A text representation of an OpeningStatus.</param>
        /// <param name="OpeningStatus">The parsed OpeningStatus.</param>
        public static Boolean TryParse(String Text, out OpeningStatus OpeningStatus)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out OpeningStatus))
                    OpeningStatus = Register(Text);

                return true;

            }

            OpeningStatus = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this OpeningStatus.
        /// </summary>
        public OpeningStatus Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Open resp. available.
        /// </summary>
        public static OpeningStatus  Open                         { get; }
            = Register("open");

        /// <summary>
        /// Open, but with limited services.
        /// </summary>
        public static OpeningStatus  OpenWithServiceLimitation    { get; }
            = Register("openWithServiceLimitation");

        /// <summary>
        /// Closed, usually because of the regular opening times.
        /// </summary>
        public static OpeningStatus  Closed                       { get; }
            = Register("closed");

        /// <summary>
        /// Closed because of a holiday or holidays.
        /// </summary>
        public static OpeningStatus  ClosedOnHoliday              { get; }
            = Register("closedOnHoliday");

        /// <summary>
        /// Closed because of maintenance activities.
        /// </summary>
        public static OpeningStatus  ClosedOnMaintenance          { get; }
            = Register("closedOnMaintenance");

        /// <summary>
        /// Temporarily closed.
        /// </summary>
        public static OpeningStatus  TemporarilyClosed            { get; }
            = Register("temporarilyClosed");

        /// <summary>
        /// The opening status is unknown.
        /// </summary>
        public static OpeningStatus  StatusUnknown                { get; }
            = Register("statusUnknown");

        /// <summary>
        /// Other.
        /// </summary>
        public static OpeningStatus  Other                        { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (OpeningStatus1, OpeningStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OpeningStatus1">An OpeningStatus.</param>
        /// <param name="OpeningStatus2">Another OpeningStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (OpeningStatus OpeningStatus1,
                                           OpeningStatus OpeningStatus2)

            => OpeningStatus1.Equals(OpeningStatus2);

        #endregion

        #region Operator != (OpeningStatus1, OpeningStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OpeningStatus1">An OpeningStatus.</param>
        /// <param name="OpeningStatus2">Another OpeningStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (OpeningStatus OpeningStatus1,
                                           OpeningStatus OpeningStatus2)

            => !OpeningStatus1.Equals(OpeningStatus2);

        #endregion

        #region Operator <  (OpeningStatus1, OpeningStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OpeningStatus1">An OpeningStatus.</param>
        /// <param name="OpeningStatus2">Another OpeningStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (OpeningStatus OpeningStatus1,
                                          OpeningStatus OpeningStatus2)

            => OpeningStatus1.CompareTo(OpeningStatus2) < 0;

        #endregion

        #region Operator <= (OpeningStatus1, OpeningStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OpeningStatus1">An OpeningStatus.</param>
        /// <param name="OpeningStatus2">Another OpeningStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (OpeningStatus OpeningStatus1,
                                           OpeningStatus OpeningStatus2)

            => OpeningStatus1.CompareTo(OpeningStatus2) <= 0;

        #endregion

        #region Operator >  (OpeningStatus1, OpeningStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OpeningStatus1">An OpeningStatus.</param>
        /// <param name="OpeningStatus2">Another OpeningStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (OpeningStatus OpeningStatus1,
                                          OpeningStatus OpeningStatus2)

            => OpeningStatus1.CompareTo(OpeningStatus2) > 0;

        #endregion

        #region Operator >= (OpeningStatus1, OpeningStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="OpeningStatus1">An OpeningStatus.</param>
        /// <param name="OpeningStatus2">Another OpeningStatus.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (OpeningStatus OpeningStatus1,
                                           OpeningStatus OpeningStatus2)

            => OpeningStatus1.CompareTo(OpeningStatus2) >= 0;

        #endregion

        #endregion

        #region IComparable<OpeningStatus> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two OpeningStatus.
        /// </summary>
        /// <param name="Object">An OpeningStatus to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is OpeningStatus openingStatus
                   ? CompareTo(openingStatus)
                   : throw new ArgumentException("The given object is not an OpeningStatus!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(OpeningStatus)

        /// <summary>
        /// Compares two OpeningStatus.
        /// </summary>
        /// <param name="OpeningStatus">An OpeningStatus to compare with.</param>
        public Int32 CompareTo(OpeningStatus OpeningStatus)

            => String.Compare(InternalId,
                              OpeningStatus.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<OpeningStatus> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two OpeningStatus for equality.
        /// </summary>
        /// <param name="Object">An OpeningStatus to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is OpeningStatus openingStatus &&
                   Equals(openingStatus);

        #endregion

        #region Equals(OpeningStatus)

        /// <summary>
        /// Compares two OpeningStatus for equality.
        /// </summary>
        /// <param name="OpeningStatus">An OpeningStatus to compare with.</param>
        public Boolean Equals(OpeningStatus OpeningStatus)

            => String.Equals(InternalId,
                             OpeningStatus.InternalId,
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
