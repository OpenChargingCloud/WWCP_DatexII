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
    /// Extension methods for ServiceType.
    /// </summary>
    public static class ServiceTypeExtensions
    {

        /// <summary>
        /// Indicates whether this ServiceType is null or empty.
        /// </summary>
        /// <param name="ServiceType">A ServiceType.</param>
        public static Boolean IsNullOrEmpty(this ServiceType? ServiceType)
            => !ServiceType.HasValue || ServiceType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this ServiceType is null or empty.
        /// </summary>
        /// <param name="ServiceType">A ServiceType.</param>
        public static Boolean IsNotNullOrEmpty(this ServiceType? ServiceType)
            => ServiceType.HasValue && ServiceType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A ServiceType.
    /// </summary>
    public readonly struct ServiceType : IId,
                                         IEquatable<ServiceType>,
                                         IComparable<ServiceType>
    {

        #region Data

        private readonly static Dictionary<String, ServiceType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this ServiceType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this ServiceType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the ServiceType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered ServiceTypes.
        /// </summary>
        public static    IEnumerable<ServiceType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ServiceType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a ServiceType.</param>
        private ServiceType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ServiceType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ServiceType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a ServiceType.
        /// </summary>
        /// <param name="Text">A text representation of a ServiceType.</param>
        public static ServiceType Parse(String Text)
        {

            if (TryParse(Text, out var serviceType))
                return serviceType;

            throw new ArgumentException($"Invalid text representation of a ServiceType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a ServiceType.
        /// </summary>
        /// <param name="Text">A text representation of a ServiceType.</param>
        public static ServiceType? TryParse(String Text)
        {

            if (TryParse(Text, out var serviceType))
                return serviceType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ServiceType)

        /// <summary>
        /// Try to parse the given text as a ServiceType.
        /// </summary>
        /// <param name="Text">A text representation of a ServiceType.</param>
        /// <param name="ServiceType">The parsed ServiceType.</param>
        public static Boolean TryParse(String Text, out ServiceType ServiceType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ServiceType))
                    ServiceType = Register(Text);

                return true;

            }

            ServiceType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this ServiceType.
        /// </summary>
        public ServiceType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Presence of physical persons attending the recharging or refuelling station.
        /// </summary>
        public static ServiceType  PhysicalAttendance    { get; }
            = Register("physicalAttendance");

        /// <summary>
        /// Unattended station, fuelling and payment to be done without assistance.
        /// </summary>
        public static ServiceType  Unattended            { get; }
            = Register("unattended");

        #endregion


        #region Operator overloading

        #region Operator == (ServiceType1, ServiceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceType1">A ServiceType.</param>
        /// <param name="ServiceType2">Another ServiceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ServiceType ServiceType1,
                                           ServiceType ServiceType2)

            => ServiceType1.Equals(ServiceType2);

        #endregion

        #region Operator != (ServiceType1, ServiceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceType1">A ServiceType.</param>
        /// <param name="ServiceType2">Another ServiceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ServiceType ServiceType1,
                                           ServiceType ServiceType2)

            => !ServiceType1.Equals(ServiceType2);

        #endregion

        #region Operator <  (ServiceType1, ServiceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceType1">A ServiceType.</param>
        /// <param name="ServiceType2">Another ServiceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ServiceType ServiceType1,
                                          ServiceType ServiceType2)

            => ServiceType1.CompareTo(ServiceType2) < 0;

        #endregion

        #region Operator <= (ServiceType1, ServiceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceType1">A ServiceType.</param>
        /// <param name="ServiceType2">Another ServiceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ServiceType ServiceType1,
                                           ServiceType ServiceType2)

            => ServiceType1.CompareTo(ServiceType2) <= 0;

        #endregion

        #region Operator >  (ServiceType1, ServiceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceType1">A ServiceType.</param>
        /// <param name="ServiceType2">Another ServiceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ServiceType ServiceType1,
                                          ServiceType ServiceType2)

            => ServiceType1.CompareTo(ServiceType2) > 0;

        #endregion

        #region Operator >= (ServiceType1, ServiceType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ServiceType1">A ServiceType.</param>
        /// <param name="ServiceType2">Another ServiceType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ServiceType ServiceType1,
                                           ServiceType ServiceType2)

            => ServiceType1.CompareTo(ServiceType2) >= 0;

        #endregion

        #endregion

        #region IComparable<ServiceType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two ServiceTypes.
        /// </summary>
        /// <param name="Object">A ServiceType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ServiceType serviceType
                   ? CompareTo(serviceType)
                   : throw new ArgumentException("The given object is not a ServiceType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ServiceType)

        /// <summary>
        /// Compares two ServiceTypes.
        /// </summary>
        /// <param name="ServiceType">A ServiceType to compare with.</param>
        public Int32 CompareTo(ServiceType ServiceType)

            => String.Compare(InternalId,
                              ServiceType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<ServiceType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two ServiceTypes for equality.
        /// </summary>
        /// <param name="Object">A ServiceType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ServiceType serviceType &&
                   Equals(serviceType);

        #endregion

        #region Equals(ServiceType)

        /// <summary>
        /// Compares two ServiceTypes for equality.
        /// </summary>
        /// <param name="ServiceType">A ServiceType to compare with.</param>
        public Boolean Equals(ServiceType ServiceType)

            => String.Equals(InternalId,
                             ServiceType.InternalId,
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
