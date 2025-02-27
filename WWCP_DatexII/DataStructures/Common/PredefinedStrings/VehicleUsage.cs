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
    /// Extension methods for VehicleUsages.
    /// </summary>
    public static class VehicleUsageExtensions
    {

        /// <summary>
        /// Indicates whether this VehicleUsage is null or empty.
        /// </summary>
        /// <param name="VehicleUsage">A VehicleUsage.</param>
        public static Boolean IsNullOrEmpty(this VehicleUsage? VehicleUsage)
            => !VehicleUsage.HasValue || VehicleUsage.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this VehicleUsage is null or empty.
        /// </summary>
        /// <param name="VehicleUsage">A VehicleUsage.</param>
        public static Boolean IsNotNullOrEmpty(this VehicleUsage? VehicleUsage)
            => VehicleUsage.HasValue && VehicleUsage.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A VehicleUsage.
    /// </summary>
    public readonly struct VehicleUsage : IId,
                                          IEquatable<VehicleUsage>,
                                          IComparable<VehicleUsage>
    {

        #region Data

        private readonly static Dictionary<String, VehicleUsage>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this VehicleUsage is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this VehicleUsage is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the VehicleUsage.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<VehicleUsage>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new VehicleUsage based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a VehicleUsage.</param>
        private VehicleUsage(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static VehicleUsage Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new VehicleUsage(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a VehicleUsage.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleUsage.</param>
        public static VehicleUsage Parse(String Text)
        {

            if (TryParse(Text, out var vehicleUsage))
                return vehicleUsage;

            throw new ArgumentException($"Invalid text representation of a VehicleUsage: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a VehicleUsage.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleUsage.</param>
        public static VehicleUsage? TryParse(String Text)
        {

            if (TryParse(Text, out var vehicleUsage))
                return vehicleUsage;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out VehicleUsage)

        /// <summary>
        /// Try to parse the given text as VehicleUsage.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleUsage.</param>
        /// <param name="VehicleUsage">The parsed VehicleUsage.</param>
        public static Boolean TryParse(String Text, out VehicleUsage VehicleUsage)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out VehicleUsage))
                    VehicleUsage = Register(Text);

                return true;

            }

            VehicleUsage = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this VehicleUsage.
        /// </summary>
        public VehicleUsage Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Vehicle used for agricultural purposes.
        /// </summary>
        public static VehicleUsage  Agricultural                     { get; }
            = Register("agricultural");

        /// <summary>
        /// Vehicles operated by a car-sharing company.
        /// </summary>
        public static VehicleUsage  CarSharing                       { get; }
            = Register("carSharing");

        /// <summary>
        /// Vehicles that are used to deliver goods in a city area.
        /// </summary>
        public static VehicleUsage  CityLogistics                    { get; }
            = Register("cityLogistics");

        /// <summary>
        /// Vehicle which is limited to non-private usage or public transport usage.
        /// </summary>
        public static VehicleUsage  Commercial                       { get; }
            = Register("commercial");

        /// <summary>
        /// Vehicle used by the emergency services.
        /// </summary>
        public static VehicleUsage  EmergencyServices                { get; }
            = Register("emergencyServices");

        /// <summary>
        /// Vehicle used by the military.
        /// </summary>
        public static VehicleUsage  Military                         { get; }
            = Register("military");

        /// <summary>
        /// Vehicle used for non-commercial or private purposes.
        /// </summary>
        public static VehicleUsage  NonCommercial                    { get; }
            = Register("nonCommercial");

        /// <summary>
        /// Vehicle used as part of a patrol service, e.g. road operator or automobile association patrol vehicle.
        /// </summary>
        public static VehicleUsage  Patrol                           { get; }
            = Register("patrol");

        /// <summary>
        /// Vehicle used to provide a recovery service.
        /// </summary>
        public static VehicleUsage  RecoveryServices                 { get; }
            = Register("recoveryServices");

        /// <summary>
        /// Vehicle used for road maintenance or construction work purposes.
        /// </summary>
        public static VehicleUsage  RoadMaintenanceOrConstruction    { get; }
            = Register("roadMaintenanceOrConstruction");

        /// <summary>
        /// Vehicle used by the road operator.
        /// </summary>
        public static VehicleUsage  RoadOperator                     { get; }
            = Register("roadOperator");

        /// <summary>
        /// Vehicle used to provide an authorised taxi service.
        /// </summary>
        public static VehicleUsage  Taxi                             { get; }
            = Register("taxi");

        #endregion


        #region Operator overloading

        #region Operator == (VehicleUsage1, VehicleUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleUsage1">A VehicleUsage.</param>
        /// <param name="VehicleUsage2">Another VehicleUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (VehicleUsage VehicleUsage1,
                                           VehicleUsage VehicleUsage2)

            => VehicleUsage1.Equals(VehicleUsage2);

        #endregion

        #region Operator != (VehicleUsage1, VehicleUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleUsage1">A VehicleUsage.</param>
        /// <param name="VehicleUsage2">Another VehicleUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (VehicleUsage VehicleUsage1,
                                           VehicleUsage VehicleUsage2)

            => !VehicleUsage1.Equals(VehicleUsage2);

        #endregion

        #region Operator <  (VehicleUsage1, VehicleUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleUsage1">A VehicleUsage.</param>
        /// <param name="VehicleUsage2">Another VehicleUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (VehicleUsage VehicleUsage1,
                                          VehicleUsage VehicleUsage2)

            => VehicleUsage1.CompareTo(VehicleUsage2) < 0;

        #endregion

        #region Operator <= (VehicleUsage1, VehicleUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleUsage1">A VehicleUsage.</param>
        /// <param name="VehicleUsage2">Another VehicleUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (VehicleUsage VehicleUsage1,
                                           VehicleUsage VehicleUsage2)

            => VehicleUsage1.CompareTo(VehicleUsage2) <= 0;

        #endregion

        #region Operator >  (VehicleUsage1, VehicleUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleUsage1">A VehicleUsage.</param>
        /// <param name="VehicleUsage2">Another VehicleUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (VehicleUsage VehicleUsage1,
                                          VehicleUsage VehicleUsage2)

            => VehicleUsage1.CompareTo(VehicleUsage2) > 0;

        #endregion

        #region Operator >= (VehicleUsage1, VehicleUsage2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleUsage1">A VehicleUsage.</param>
        /// <param name="VehicleUsage2">Another VehicleUsage.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (VehicleUsage VehicleUsage1,
                                           VehicleUsage VehicleUsage2)

            => VehicleUsage1.CompareTo(VehicleUsage2) >= 0;

        #endregion

        #endregion

        #region IComparable<VehicleUsage> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two VehicleUsages.
        /// </summary>
        /// <param name="Object">VehicleUsage to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is VehicleUsage vehicleUsage
                   ? CompareTo(vehicleUsage)
                   : throw new ArgumentException("The given object is not VehicleUsage!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(VehicleUsage)

        /// <summary>
        /// Compares two VehicleUsages.
        /// </summary>
        /// <param name="VehicleUsage">VehicleUsage to compare with.</param>
        public Int32 CompareTo(VehicleUsage VehicleUsage)

            => String.Compare(InternalId,
                              VehicleUsage.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<VehicleUsage> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two VehicleUsages for equality.
        /// </summary>
        /// <param name="Object">VehicleUsage to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is VehicleUsage vehicleUsage &&
                   Equals(vehicleUsage);

        #endregion

        #region Equals(VehicleUsage)

        /// <summary>
        /// Compares two VehicleUsages for equality.
        /// </summary>
        /// <param name="VehicleUsage">VehicleUsage to compare with.</param>
        public Boolean Equals(VehicleUsage VehicleUsage)

            => String.Equals(InternalId,
                             VehicleUsage.InternalId,
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
