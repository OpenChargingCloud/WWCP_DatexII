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
    /// Extension methods for VehicleToGridCommunications.
    /// </summary>
    public static class VehicleToGridCommunicationExtensions
    {

        /// <summary>
        /// Indicates whether this VehicleToGridCommunication is null or empty.
        /// </summary>
        /// <param name="VehicleToGridCommunication">A VehicleToGridCommunication.</param>
        public static Boolean IsNullOrEmpty(this VehicleToGridCommunication? VehicleToGridCommunication)
            => !VehicleToGridCommunication.HasValue || VehicleToGridCommunication.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this VehicleToGridCommunication is null or empty.
        /// </summary>
        /// <param name="VehicleToGridCommunication">A VehicleToGridCommunication.</param>
        public static Boolean IsNotNullOrEmpty(this VehicleToGridCommunication? VehicleToGridCommunication)
            => VehicleToGridCommunication.HasValue && VehicleToGridCommunication.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A VehicleToGridCommunication.
    /// </summary>
    public readonly struct VehicleToGridCommunication : IId,
                                                        IEquatable<VehicleToGridCommunication>,
                                                        IComparable<VehicleToGridCommunication>
    {

        #region Data

        private readonly static Dictionary<String, VehicleToGridCommunication>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this VehicleToGridCommunication is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this VehicleToGridCommunication is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the VehicleToGridCommunication.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered VehicleToGridCommunications.
        /// </summary>
        public static    IEnumerable<VehicleToGridCommunication>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new VehicleToGridCommunication based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a VehicleToGridCommunication.</param>
        private VehicleToGridCommunication(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static VehicleToGridCommunication Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new VehicleToGridCommunication(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a VehicleToGridCommunication.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleToGridCommunication.</param>
        public static VehicleToGridCommunication Parse(String Text)
        {

            if (TryParse(Text, out var vehicleToGridCommunication))
                return vehicleToGridCommunication;

            throw new ArgumentException($"Invalid text representation of a VehicleToGridCommunication: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a VehicleToGridCommunication.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleToGridCommunication.</param>
        public static VehicleToGridCommunication? TryParse(String Text)
        {

            if (TryParse(Text, out var vehicleToGridCommunication))
                return vehicleToGridCommunication;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out VehicleToGridCommunication)

        /// <summary>
        /// Try to parse the given text as a VehicleToGridCommunication.
        /// </summary>
        /// <param name="Text">A text representation of a VehicleToGridCommunication.</param>
        /// <param name="VehicleToGridCommunication">The parsed VehicleToGridCommunication.</param>
        public static Boolean TryParse(String Text, out VehicleToGridCommunication VehicleToGridCommunication)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out VehicleToGridCommunication))
                    VehicleToGridCommunication = Register(Text);

                return true;

            }

            VehicleToGridCommunication = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this VehicleToGridCommunication.
        /// </summary>
        public VehicleToGridCommunication Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// No communication between vehicle and the grid.
        /// </summary>
        public static VehicleToGridCommunication  None         { get; }
            = Register("none");

        /// <summary>
        /// Communication according to ISO15118.
        /// </summary>
        public static VehicleToGridCommunication  ISO15118     { get; }
            = Register("iso15118");

        /// <summary>
        /// Communication according to IEC/TS 61980-2.
        /// </summary>
        public static VehicleToGridCommunication  IEC619802    { get; }
            = Register("iec619802");

        /// <summary>
        /// Communication according to other guidelines/specifications.
        /// </summary>
        public static VehicleToGridCommunication  Other        { get; }
            = Register("other");

        /// <summary>
        /// The type of communication is unknown.
        /// </summary>
        public static VehicleToGridCommunication  Unknown      { get; }
            = Register("unknown");

        #endregion


        #region Operator overloading

        #region Operator == (VehicleToGridCommunication1, VehicleToGridCommunication2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleToGridCommunication1">A VehicleToGridCommunication.</param>
        /// <param name="VehicleToGridCommunication2">Another VehicleToGridCommunication.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (VehicleToGridCommunication VehicleToGridCommunication1,
                                           VehicleToGridCommunication VehicleToGridCommunication2)

            => VehicleToGridCommunication1.Equals(VehicleToGridCommunication2);

        #endregion

        #region Operator != (VehicleToGridCommunication1, VehicleToGridCommunication2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleToGridCommunication1">A VehicleToGridCommunication.</param>
        /// <param name="VehicleToGridCommunication2">Another VehicleToGridCommunication.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (VehicleToGridCommunication VehicleToGridCommunication1,
                                           VehicleToGridCommunication VehicleToGridCommunication2)

            => !VehicleToGridCommunication1.Equals(VehicleToGridCommunication2);

        #endregion

        #region Operator <  (VehicleToGridCommunication1, VehicleToGridCommunication2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleToGridCommunication1">A VehicleToGridCommunication.</param>
        /// <param name="VehicleToGridCommunication2">Another VehicleToGridCommunication.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (VehicleToGridCommunication VehicleToGridCommunication1,
                                          VehicleToGridCommunication VehicleToGridCommunication2)

            => VehicleToGridCommunication1.CompareTo(VehicleToGridCommunication2) < 0;

        #endregion

        #region Operator <= (VehicleToGridCommunication1, VehicleToGridCommunication2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleToGridCommunication1">A VehicleToGridCommunication.</param>
        /// <param name="VehicleToGridCommunication2">Another VehicleToGridCommunication.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (VehicleToGridCommunication VehicleToGridCommunication1,
                                           VehicleToGridCommunication VehicleToGridCommunication2)

            => VehicleToGridCommunication1.CompareTo(VehicleToGridCommunication2) <= 0;

        #endregion

        #region Operator >  (VehicleToGridCommunication1, VehicleToGridCommunication2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleToGridCommunication1">A VehicleToGridCommunication.</param>
        /// <param name="VehicleToGridCommunication2">Another VehicleToGridCommunication.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (VehicleToGridCommunication VehicleToGridCommunication1,
                                          VehicleToGridCommunication VehicleToGridCommunication2)

            => VehicleToGridCommunication1.CompareTo(VehicleToGridCommunication2) > 0;

        #endregion

        #region Operator >= (VehicleToGridCommunication1, VehicleToGridCommunication2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VehicleToGridCommunication1">A VehicleToGridCommunication.</param>
        /// <param name="VehicleToGridCommunication2">Another VehicleToGridCommunication.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (VehicleToGridCommunication VehicleToGridCommunication1,
                                           VehicleToGridCommunication VehicleToGridCommunication2)

            => VehicleToGridCommunication1.CompareTo(VehicleToGridCommunication2) >= 0;

        #endregion

        #endregion

        #region IComparable<VehicleToGridCommunication> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two VehicleToGridCommunications.
        /// </summary>
        /// <param name="Object">A VehicleToGridCommunication to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is VehicleToGridCommunication vehicleToGridCommunication
                   ? CompareTo(vehicleToGridCommunication)
                   : throw new ArgumentException("The given object is not a VehicleToGridCommunication!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(VehicleToGridCommunication)

        /// <summary>
        /// Compares two VehicleToGridCommunications.
        /// </summary>
        /// <param name="VehicleToGridCommunication">A VehicleToGridCommunication to compare with.</param>
        public Int32 CompareTo(VehicleToGridCommunication VehicleToGridCommunication)

            => String.Compare(InternalId,
                              VehicleToGridCommunication.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<VehicleToGridCommunication> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two VehicleToGridCommunications for equality.
        /// </summary>
        /// <param name="Object">A VehicleToGridCommunication to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is VehicleToGridCommunication vehicleToGridCommunication &&
                   Equals(vehicleToGridCommunication);

        #endregion

        #region Equals(VehicleToGridCommunication)

        /// <summary>
        /// Compares two VehicleToGridCommunications for equality.
        /// </summary>
        /// <param name="VehicleToGridCommunication">A VehicleToGridCommunication to compare with.</param>
        public Boolean Equals(VehicleToGridCommunication VehicleToGridCommunication)

            => String.Equals(InternalId,
                             VehicleToGridCommunication.InternalId,
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
