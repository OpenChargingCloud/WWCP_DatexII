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
    /// Extension methods for SmartRechargingServices.
    /// </summary>
    public static class SmartRechargingServiceExtensions
    {

        /// <summary>
        /// Indicates whether this SmartRechargingService is null or empty.
        /// </summary>
        /// <param name="SmartRechargingService">A SmartRechargingService.</param>
        public static Boolean IsNullOrEmpty(this SmartRechargingService? SmartRechargingService)
            => !SmartRechargingService.HasValue || SmartRechargingService.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this SmartRechargingService is null or empty.
        /// </summary>
        /// <param name="SmartRechargingService">A SmartRechargingService.</param>
        public static Boolean IsNotNullOrEmpty(this SmartRechargingService? SmartRechargingService)
            => SmartRechargingService.HasValue && SmartRechargingService.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A SmartRechargingService.
    /// </summary>
    public readonly struct SmartRechargingService : IId,
                                                    IEquatable<SmartRechargingService>,
                                                    IComparable<SmartRechargingService>
    {

        #region Data

        private readonly static Dictionary<String, SmartRechargingService>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this SmartRechargingService is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this SmartRechargingService is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the SmartRechargingService.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered SmartRechargingServices.
        /// </summary>
        public static    IEnumerable<SmartRechargingService>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new SmartRechargingService based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a SmartRechargingService.</param>
        private SmartRechargingService(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static SmartRechargingService Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new SmartRechargingService(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a SmartRechargingService.
        /// </summary>
        /// <param name="Text">A text representation of a SmartRechargingService.</param>
        public static SmartRechargingService Parse(String Text)
        {

            if (TryParse(Text, out var smartRechargingService))
                return smartRechargingService;

            throw new ArgumentException($"Invalid text representation of a SmartRechargingService: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a SmartRechargingService.
        /// </summary>
        /// <param name="Text">A text representation of a SmartRechargingService.</param>
        public static SmartRechargingService? TryParse(String Text)
        {

            if (TryParse(Text, out var smartRechargingService))
                return smartRechargingService;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out SmartRechargingService)

        /// <summary>
        /// Try to parse the given text as a SmartRechargingService.
        /// </summary>
        /// <param name="Text">A text representation of a SmartRechargingService.</param>
        /// <param name="SmartRechargingService">The parsed SmartRechargingService.</param>
        public static Boolean TryParse(String Text, out SmartRechargingService SmartRechargingService)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out SmartRechargingService))
                    SmartRechargingService = Register(Text);

                return true;

            }

            SmartRechargingService = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this SmartRechargingService.
        /// </summary>
        public SmartRechargingService Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Remote monitoring and control recharging (e.g. by app).
        /// </summary>
        public static SmartRechargingService  RemoteMonitoring           { get; }
            = Register("remoteMonitoring");

        /// <summary>
        /// User preference configuration for recharging power optimization.
        /// </summary>
        public static SmartRechargingService  PowerOptimisationByUser    { get; }
            = Register("powerOptimisationByUser");

        /// <summary>
        /// Bidirectional charging.
        /// </summary>
        public static SmartRechargingService  BidirectionalRecharging    { get; }
            = Register("bidirectionalRecharging");

        #endregion


        #region Operator overloading

        #region Operator == (SmartRechargingService1, SmartRechargingService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SmartRechargingService1">A SmartRechargingService.</param>
        /// <param name="SmartRechargingService2">Another SmartRechargingService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (SmartRechargingService SmartRechargingService1,
                                           SmartRechargingService SmartRechargingService2)

            => SmartRechargingService1.Equals(SmartRechargingService2);

        #endregion

        #region Operator != (SmartRechargingService1, SmartRechargingService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SmartRechargingService1">A SmartRechargingService.</param>
        /// <param name="SmartRechargingService2">Another SmartRechargingService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (SmartRechargingService SmartRechargingService1,
                                           SmartRechargingService SmartRechargingService2)

            => !SmartRechargingService1.Equals(SmartRechargingService2);

        #endregion

        #region Operator <  (SmartRechargingService1, SmartRechargingService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SmartRechargingService1">A SmartRechargingService.</param>
        /// <param name="SmartRechargingService2">Another SmartRechargingService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (SmartRechargingService SmartRechargingService1,
                                          SmartRechargingService SmartRechargingService2)

            => SmartRechargingService1.CompareTo(SmartRechargingService2) < 0;

        #endregion

        #region Operator <= (SmartRechargingService1, SmartRechargingService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SmartRechargingService1">A SmartRechargingService.</param>
        /// <param name="SmartRechargingService2">Another SmartRechargingService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (SmartRechargingService SmartRechargingService1,
                                           SmartRechargingService SmartRechargingService2)

            => SmartRechargingService1.CompareTo(SmartRechargingService2) <= 0;

        #endregion

        #region Operator >  (SmartRechargingService1, SmartRechargingService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SmartRechargingService1">A SmartRechargingService.</param>
        /// <param name="SmartRechargingService2">Another SmartRechargingService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (SmartRechargingService SmartRechargingService1,
                                          SmartRechargingService SmartRechargingService2)

            => SmartRechargingService1.CompareTo(SmartRechargingService2) > 0;

        #endregion

        #region Operator >= (SmartRechargingService1, SmartRechargingService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SmartRechargingService1">A SmartRechargingService.</param>
        /// <param name="SmartRechargingService2">Another SmartRechargingService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (SmartRechargingService SmartRechargingService1,
                                           SmartRechargingService SmartRechargingService2)

            => SmartRechargingService1.CompareTo(SmartRechargingService2) >= 0;

        #endregion

        #endregion

        #region IComparable<SmartRechargingService> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two SmartRechargingServices.
        /// </summary>
        /// <param name="Object">A SmartRechargingService to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is SmartRechargingService smartRechargingService
                   ? CompareTo(smartRechargingService)
                   : throw new ArgumentException("The given object is not a SmartRechargingService!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(SmartRechargingService)

        /// <summary>
        /// Compares two SmartRechargingServices.
        /// </summary>
        /// <param name="SmartRechargingService">A SmartRechargingService to compare with.</param>
        public Int32 CompareTo(SmartRechargingService SmartRechargingService)

            => String.Compare(InternalId,
                              SmartRechargingService.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<SmartRechargingService> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two SmartRechargingServices for equality.
        /// </summary>
        /// <param name="Object">A SmartRechargingService to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is SmartRechargingService smartRechargingService &&
                   Equals(smartRechargingService);

        #endregion

        #region Equals(SmartRechargingService)

        /// <summary>
        /// Compares two SmartRechargingServices for equality.
        /// </summary>
        /// <param name="SmartRechargingService">A SmartRechargingService to compare with.</param>
        public Boolean Equals(SmartRechargingService SmartRechargingService)

            => String.Equals(InternalId,
                             SmartRechargingService.InternalId,
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
