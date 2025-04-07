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
    /// Extension methods for VehicleEquipments.
    /// </summary>
    public static class VehicleEquipmentExtensions
    {

        /// <summary>
        /// Indicates whether this VehicleEquipment is null or empty.
        /// </summary>
        /// <param name="VehicleEquipment">A VehicleEquipment.</param>
        public static Boolean IsNullOrEmpty(this VehicleEquipment? VehicleEquipment)
            => !VehicleEquipment.HasValue || VehicleEquipment.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this VehicleEquipment is null or empty.
        /// </summary>
        /// <param name="VehicleEquipment">A VehicleEquipment.</param>
        public static Boolean IsNotNullOrEmpty(this VehicleEquipment? VehicleEquipment)
            => VehicleEquipment.HasValue && VehicleEquipment.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A VehicleEquipment.
    /// </summary>
    public readonly struct VehicleEquipment : IId,
                                              IEquatable<VehicleEquipment>,
                                              IComparable<VehicleEquipment>
    {

        #region Data

        private readonly static Dictionary<String, VehicleEquipment>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this VehicleEquipment is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this VehicleEquipment is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the VehicleEquipment.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<VehicleEquipment>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new VehicleEquipment based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a VehicleEquipment.</param>
        private VehicleEquipment(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static VehicleEquipment Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new VehicleEquipment(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a VehicleEquipment.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleEquipment.</param>
        public static VehicleEquipment Parse(String Text)
        {

            if (TryParse(Text, out var vehicleEquipment))
                return vehicleEquipment;

            throw new ArgumentException($"Invalid text representation of a VehicleEquipment: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a VehicleEquipment.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleEquipment.</param>
        public static VehicleEquipment? TryParse(String Text)
        {

            if (TryParse(Text, out var vehicleEquipment))
                return vehicleEquipment;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out VehicleEquipment)

        /// <summary>
        /// Try to parse the given text as VehicleEquipment.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleEquipment.</param>
        /// <param name="VehicleEquipment">The parsed VehicleEquipment.</param>
        public static Boolean TryParse(String Text, out VehicleEquipment VehicleEquipment)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out VehicleEquipment))
                    VehicleEquipment = Register(Text);

                return true;

            }

            VehicleEquipment = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this VehicleEquipment.
        /// </summary>
        public VehicleEquipment Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Vehicle not using snow chains.
        /// </summary>
        public static VehicleEquipment  NotUsingSnowChains                 { get; }
            = Register("notUsingSnowChains");

        /// <summary>
        /// Vehicle not using either snow tyres or snow chains.
        /// </summary>
        public static VehicleEquipment  NotUsingSnowChainsOrTyres          { get; }
            = Register("notUsingSnowChainsOrTyres");

        /// <summary>
        /// Vehicle using snow chains.
        /// </summary>
        public static VehicleEquipment  SnowChainsInUse                    { get; }
            = Register("snowChainsInUse");

        /// <summary>
        /// Vehicle using snow tyres.
        /// </summary>
        public static VehicleEquipment  SnowTyresInUse                     { get; }
            = Register("snowTyresInUse");

        /// <summary>
        /// Vehicle using snow tyres or snow chains.
        /// </summary>
        public static VehicleEquipment  SnowChainsOrTyresInUse             { get; }
            = Register("snowChainsOrTyresInUse");

        /// <summary>
        /// Vehicle which is not carrying on board snow tyres or chains.
        /// </summary>
        public static VehicleEquipment  WithoutSnowTyresOrChainsOnBoard    { get; }
            = Register("withoutSnowTyresOrChainsOnBoard");

        #endregion


        #region Operator overloading

        #region Operator == (VehicleEquipment1, VehicleEquipment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleEquipment1">A VehicleEquipment.</param>
        /// <param name="VehicleEquipment2">Another VehicleEquipment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (VehicleEquipment VehicleEquipment1,
                                           VehicleEquipment VehicleEquipment2)

            => VehicleEquipment1.Equals(VehicleEquipment2);

        #endregion

        #region Operator != (VehicleEquipment1, VehicleEquipment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleEquipment1">A VehicleEquipment.</param>
        /// <param name="VehicleEquipment2">Another VehicleEquipment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (VehicleEquipment VehicleEquipment1,
                                           VehicleEquipment VehicleEquipment2)

            => !VehicleEquipment1.Equals(VehicleEquipment2);

        #endregion

        #region Operator <  (VehicleEquipment1, VehicleEquipment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleEquipment1">A VehicleEquipment.</param>
        /// <param name="VehicleEquipment2">Another VehicleEquipment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (VehicleEquipment VehicleEquipment1,
                                          VehicleEquipment VehicleEquipment2)

            => VehicleEquipment1.CompareTo(VehicleEquipment2) < 0;

        #endregion

        #region Operator <= (VehicleEquipment1, VehicleEquipment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleEquipment1">A VehicleEquipment.</param>
        /// <param name="VehicleEquipment2">Another VehicleEquipment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (VehicleEquipment VehicleEquipment1,
                                           VehicleEquipment VehicleEquipment2)

            => VehicleEquipment1.CompareTo(VehicleEquipment2) <= 0;

        #endregion

        #region Operator >  (VehicleEquipment1, VehicleEquipment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleEquipment1">A VehicleEquipment.</param>
        /// <param name="VehicleEquipment2">Another VehicleEquipment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (VehicleEquipment VehicleEquipment1,
                                          VehicleEquipment VehicleEquipment2)

            => VehicleEquipment1.CompareTo(VehicleEquipment2) > 0;

        #endregion

        #region Operator >= (VehicleEquipment1, VehicleEquipment2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleEquipment1">A VehicleEquipment.</param>
        /// <param name="VehicleEquipment2">Another VehicleEquipment.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (VehicleEquipment VehicleEquipment1,
                                           VehicleEquipment VehicleEquipment2)

            => VehicleEquipment1.CompareTo(VehicleEquipment2) >= 0;

        #endregion

        #endregion

        #region IComparable<VehicleEquipment> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two VehicleEquipments.
        /// </summary>
        /// <param name="Object">VehicleEquipment to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is VehicleEquipment vehicleEquipment
                   ? CompareTo(vehicleEquipment)
                   : throw new ArgumentException("The given object is not VehicleEquipment!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(VehicleEquipment)

        /// <summary>
        /// Compares two VehicleEquipments.
        /// </summary>
        /// <param name="VehicleEquipment">VehicleEquipment to compare with.</param>
        public Int32 CompareTo(VehicleEquipment VehicleEquipment)

            => String.Compare(InternalId,
                              VehicleEquipment.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<VehicleEquipment> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two VehicleEquipments for equality.
        /// </summary>
        /// <param name="Object">VehicleEquipment to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is VehicleEquipment vehicleEquipment &&
                   Equals(vehicleEquipment);

        #endregion

        #region Equals(VehicleEquipment)

        /// <summary>
        /// Compares two VehicleEquipments for equality.
        /// </summary>
        /// <param name="VehicleEquipment">VehicleEquipment to compare with.</param>
        public Boolean Equals(VehicleEquipment VehicleEquipment)

            => String.Equals(InternalId,
                             VehicleEquipment.InternalId,
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
