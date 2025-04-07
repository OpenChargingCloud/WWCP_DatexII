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

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Extension methods for informationStatuss.
    /// </summary>
    public static class InformationStatusExtensions
    {

        /// <summary>
        /// Indicates whether this information status is null or empty.
        /// </summary>
        /// <param name="InformationStatus">An information status.</param>
        public static Boolean IsNullOrEmpty(this InformationStatus? InformationStatus)
            => !InformationStatus.HasValue || InformationStatus.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this information status is null or empty.
        /// </summary>
        /// <param name="InformationStatus">An information status.</param>
        public static Boolean IsNotNullOrEmpty(this InformationStatus? InformationStatus)
            => InformationStatus.HasValue && InformationStatus.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An information status.
    /// </summary>
    public readonly struct InformationStatus : IId,
                                               IEquatable<InformationStatus>,
                                               IComparable<InformationStatus>
    {

        #region Data

        private readonly static Dictionary<String, InformationStatus>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                                 InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this information status is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this information status is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the information status.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered information status.
        /// </summary>
        public static    IEnumerable<InformationStatus>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new information status based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an information status.</param>
        private InformationStatus(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static InformationStatus Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new InformationStatus(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an information status.
        /// </summary>
        /// <param name="Text">A text representation of an information status.</param>
        public static InformationStatus Parse(String Text)
        {

            if (TryParse(Text, out var informationStatus))
                return informationStatus;

            throw new ArgumentException($"Invalid text representation of an information status: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an information status.
        /// </summary>
        /// <param name="Text">A text representation of an information status.</param>
        public static InformationStatus? TryParse(String Text)
        {

            if (TryParse(Text, out var informationStatus))
                return informationStatus;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out InformationStatus)

        /// <summary>
        /// Try to parse the given text as an information status.
        /// </summary>
        /// <param name="Text">A text representation of an information status.</param>
        /// <param name="InformationStatus">The parsed information status.</param>
        public static Boolean TryParse(String Text, out InformationStatus InformationStatus)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out InformationStatus))
                    InformationStatus = Register(Text);

                return true;

            }

            InformationStatus = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this information status.
        /// </summary>
        public InformationStatus Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// The information is real. It is not a test or exercise.
        /// </summary>
        public static InformationStatus  Real                 { get; }
            = Register("real");

        /// <summary>
        /// The information is part of an exercise which is for testing security.
        /// </summary>
        public static InformationStatus  SecurityExercise     { get; }
            = Register("securityExercise");

        /// <summary>
        /// The information is part of an exercise which includes tests of associated technical subsystems.
        /// </summary>
        public static InformationStatus  TechnicalExercise    { get; }
            = Register("technicalExercise");

        /// <summary>
        /// The information is part of a test for checking the exchange of this type of information.
        /// </summary>
        public static InformationStatus  Test                 { get; }
            = Register("test");

        #endregion


        #region Operator overloading

        #region Operator == (InformationStatus1, InformationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationStatus1">An information status.</param>
        /// <param name="InformationStatus2">Another information status.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (InformationStatus InformationStatus1,
                                           InformationStatus InformationStatus2)

            => InformationStatus1.Equals(InformationStatus2);

        #endregion

        #region Operator != (InformationStatus1, InformationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationStatus1">An information status.</param>
        /// <param name="InformationStatus2">Another information status.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (InformationStatus InformationStatus1,
                                           InformationStatus InformationStatus2)

            => !InformationStatus1.Equals(InformationStatus2);

        #endregion

        #region Operator <  (InformationStatus1, InformationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationStatus1">An information status.</param>
        /// <param name="InformationStatus2">Another information status.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (InformationStatus InformationStatus1,
                                          InformationStatus InformationStatus2)

            => InformationStatus1.CompareTo(InformationStatus2) < 0;

        #endregion

        #region Operator <= (InformationStatus1, InformationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationStatus1">An information status.</param>
        /// <param name="InformationStatus2">Another information status.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (InformationStatus InformationStatus1,
                                           InformationStatus InformationStatus2)

            => InformationStatus1.CompareTo(InformationStatus2) <= 0;

        #endregion

        #region Operator >  (InformationStatus1, InformationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationStatus1">An information status.</param>
        /// <param name="InformationStatus2">Another information status.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (InformationStatus InformationStatus1,
                                          InformationStatus InformationStatus2)

            => InformationStatus1.CompareTo(InformationStatus2) > 0;

        #endregion

        #region Operator >= (InformationStatus1, InformationStatus2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationStatus1">An information status.</param>
        /// <param name="InformationStatus2">Another information status.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (InformationStatus InformationStatus1,
                                           InformationStatus InformationStatus2)

            => InformationStatus1.CompareTo(InformationStatus2) >= 0;

        #endregion

        #endregion

        #region IComparable<InformationStatus> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two information status.
        /// </summary>
        /// <param name="Object">An information status to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is InformationStatus informationStatus
                   ? CompareTo(informationStatus)
                   : throw new ArgumentException("The given object is not an information status!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(InformationStatus)

        /// <summary>
        /// Compares two information status.
        /// </summary>
        /// <param name="InformationStatus">An information status to compare with.</param>
        public Int32 CompareTo(InformationStatus InformationStatus)

            => String.Compare(InternalId,
                              InformationStatus.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<InformationStatus> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two information status for equality.
        /// </summary>
        /// <param name="Object">An information status to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is InformationStatus informationStatus &&
                   Equals(informationStatus);

        #endregion

        #region Equals(InformationStatus)

        /// <summary>
        /// Compares two information status for equality.
        /// </summary>
        /// <param name="InformationStatus">An information status to compare with.</param>
        public Boolean Equals(InformationStatus InformationStatus)

            => String.Equals(InternalId,
                             InformationStatus.InternalId,
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
