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
    /// Extension methods for EquipmentTypes.
    /// </summary>
    public static class EquipmentTypeExtensions
    {

        /// <summary>
        /// Indicates whether this EquipmentType is null or empty.
        /// </summary>
        /// <param name="EquipmentType">An EquipmentType.</param>
        public static Boolean IsNullOrEmpty(this EquipmentType? EquipmentType)
            => !EquipmentType.HasValue || EquipmentType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this EquipmentType is null or empty.
        /// </summary>
        /// <param name="EquipmentType">An EquipmentType.</param>
        public static Boolean IsNotNullOrEmpty(this EquipmentType? EquipmentType)
            => EquipmentType.HasValue && EquipmentType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An EquipmentType.
    /// </summary>
    public readonly struct EquipmentType : IId,
                                           IEquatable<EquipmentType>,
                                           IComparable<EquipmentType>
    {

        #region Data

        private readonly static Dictionary<String, EquipmentType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this EquipmentType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this EquipmentType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the EquipmentType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered EquipmentTypes.
        /// </summary>
        public static    IEnumerable<EquipmentType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new EquipmentType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an EquipmentType.</param>
        private EquipmentType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static EquipmentType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new EquipmentType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an EquipmentType.
        /// </summary>
        /// <param name="Text">A text representation of an EquipmentType.</param>
        public static EquipmentType Parse(String Text)
        {

            if (TryParse(Text, out var equipmentType))
                return equipmentType;

            throw new ArgumentException($"Invalid text representation of an EquipmentType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an EquipmentType.
        /// </summary>
        /// <param name="Text">A text representation of an EquipmentType.</param>
        public static EquipmentType? TryParse(String Text)
        {

            if (TryParse(Text, out var equipmentType))
                return equipmentType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out EquipmentType)

        /// <summary>
        /// Try to parse the given text as an EquipmentType.
        /// </summary>
        /// <param name="Text">A text representation of an EquipmentType.</param>
        /// <param name="EquipmentType">The parsed EquipmentType.</param>
        public static Boolean TryParse(String Text, out EquipmentType EquipmentType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out EquipmentType))
                    EquipmentType = Register(Text);

                return true;

            }

            EquipmentType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this EquipmentType.
        /// </summary>
        public EquipmentType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Bath Room
        /// </summary>
        public static EquipmentType  BathRoom     { get; }
            = Register("bathRoom");

        /// <summary>
        /// Bike Parking.
        /// </summary>
        public static EquipmentType  BikeParking     { get; }
            = Register("bikeParking");

        /// <summary>
        /// Cash Machine.
        /// </summary>
        public static EquipmentType  CashMachine     { get; }
            = Register("cashMachine");

        /// <summary>
        /// Copy Machine or Service
        /// </summary>
        public static EquipmentType  CopyMachineOrService     { get; }
            = Register("copyMachineOrService");

        /// <summary>
        /// Defibrillator
        /// </summary>
        public static EquipmentType  Defibrillator     { get; }
            = Register("defibrillator");

        /// <summary>
        /// Dedicated Short-Range Communications (DSRC) Receiver
        /// </summary>
        public static EquipmentType  DSRCReceiver     { get; }
            = Register("dsrcReceiver");

        /// <summary>
        /// Dumping Station
        /// </summary>
        public static EquipmentType  DumpingStation     { get; }
            = Register("dumpingStation");

        /// <summary>
        /// Electric Charging Station
        /// </summary>
        public static EquipmentType  ElectricChargingStation     { get; }
            = Register("electricChargingStation");

        /// <summary>
        /// Elevator
        /// </summary>
        public static EquipmentType  Elevator     { get; }
            = Register("elevator");

        /// <summary>
        /// Fax Machine or Service
        /// </summary>
        public static EquipmentType  FaxMachineOrService     { get; }
            = Register("faxMachineOrService");

        /// <summary>
        /// Fire Extinguisher.
        /// </summary>
        public static EquipmentType  FireExtinguisher     { get; }
            = Register("fireExtinguisher");

        /// <summary>
        /// Fire Hose
        /// </summary>
        public static EquipmentType  FireHose     { get; }
            = Register("fireHose");

        /// <summary>
        /// Fire Hydrant
        /// </summary>
        public static EquipmentType  FireHydrant     { get; }
            = Register("fireHydrant");

        /// <summary>
        /// First Aid Equipment
        /// </summary>
        public static EquipmentType  FirstAidEquipment     { get; }
            = Register("firstAidEquipment");

        /// <summary>
        /// Ice Free Scaffold
        /// </summary>
        public static EquipmentType  IceFreeScaffold     { get; }
            = Register("iceFreeScaffold");

        /// <summary>
        /// Information Point
        /// </summary>
        public static EquipmentType  InformationPoint     { get; }
            = Register("informationPoint");

        /// <summary>
        /// Information Stele
        /// </summary>
        public static EquipmentType  InformationStele     { get; }
            = Register("informationStele");

        /// <summary>
        /// Internet Terminal
        /// </summary>
        public static EquipmentType  InternetTerminal     { get; }
            = Register("internetTerminal");

        /// <summary>
        /// Internet Wireless
        /// </summary>
        public static EquipmentType  InternetWireless     { get; }
            = Register("internetWireless");

        /// <summary>
        /// Luggage Locker
        /// </summary>
        public static EquipmentType  LuggageLocker     { get; }
            = Register("luggageLocker");

        /// <summary>
        /// Payment Machine
        /// </summary>
        public static EquipmentType  PaymentMachine     { get; }
            = Register("paymentMachine");

        /// <summary>
        /// Picnic Facilities
        /// </summary>
        public static EquipmentType  PicnicFacilities     { get; }
            = Register("picnicFacilities");

        /// <summary>
        /// Playground
        /// </summary>
        public static EquipmentType  Playground     { get; }
            = Register("playground");

        /// <summary>
        /// Public Card Phone
        /// </summary>
        public static EquipmentType  PublicCardPhone     { get; }
            = Register("publicCardPhone");

        /// <summary>
        /// Public Coin Phone
        /// </summary>
        public static EquipmentType  PublicCoinPhone     { get; }
            = Register("publicCoinPhone");

        /// <summary>
        /// Public Phone
        /// </summary>
        public static EquipmentType  PublicPhone     { get; }
            = Register("publicPhone");

        /// <summary>
        /// Refuse Bin
        /// </summary>
        public static EquipmentType  RefuseBin     { get; }
            = Register("refuseBin");

        /// <summary>
        /// Resting Facilities
        /// </summary>
        public static EquipmentType  RestingFacilities     { get; }
            = Register("restingFacilities");

        /// <summary>
        /// Safe Deposit
        /// </summary>
        public static EquipmentType  SafeDeposit     { get; }
            = Register("safeDeposit");

        /// <summary>
        /// Shelter
        /// </summary>
        public static EquipmentType  Shelter     { get; }
            = Register("shelter");

        /// <summary>
        /// Shower
        /// </summary>
        public static EquipmentType  Shower     { get; }
            = Register("shower");

        /// <summary>
        /// Snow and Ice Removal Equipment
        /// </summary>
        public static EquipmentType  SnowAndIceRemovalEquipment     { get; }
            = Register("snowAndIceRemovalEquipment");

        /// <summary>
        /// Toilet
        /// </summary>
        public static EquipmentType  Toilet     { get; }
            = Register("toilet");

        /// <summary>
        /// Toll Terminal
        /// </summary>
        public static EquipmentType  TollTerminal     { get; }
            = Register("tollTerminal");

        /// <summary>
        /// Tyre Air Pressure Equipment
        /// </summary>
        public static EquipmentType  TyreAirPressureEquipment     { get; }
            = Register("tyreAirPressureEquipment");

        /// <summary>
        /// Water Basin
        /// </summary>
        public static EquipmentType  WaterBasin     { get; }
            = Register("waterBasin");

        /// <summary>
        /// Vending Machine
        /// </summary>
        public static EquipmentType  VendingMachine     { get; }
            = Register("vendingMachine");

        /// <summary>
        /// Water Supply
        /// </summary>
        public static EquipmentType  WaterSupply     { get; }
            = Register("waterSupply");

        /// <summary>
        /// Waste Disposal
        /// </summary>
        public static EquipmentType  WasteDisposal     { get; }
            = Register("wasteDisposal");

        /// <summary>
        /// Water Tap
        /// </summary>
        public static EquipmentType  WaterTap     { get; }
            = Register("waterTap");

        /// <summary>
        /// None
        /// </summary>
        public static EquipmentType  None     { get; }
            = Register("none");

        /// <summary>
        /// Unknown
        /// </summary>
        public static EquipmentType  Unknown     { get; }
            = Register("unknown");

        /// <summary>
        /// Other
        /// </summary>
        public static EquipmentType  Other     { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (EquipmentType1, EquipmentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EquipmentType1">An EquipmentType.</param>
        /// <param name="EquipmentType2">Another EquipmentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (EquipmentType EquipmentType1,
                                           EquipmentType EquipmentType2)

            => EquipmentType1.Equals(EquipmentType2);

        #endregion

        #region Operator != (EquipmentType1, EquipmentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EquipmentType1">An EquipmentType.</param>
        /// <param name="EquipmentType2">Another EquipmentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (EquipmentType EquipmentType1,
                                           EquipmentType EquipmentType2)

            => !EquipmentType1.Equals(EquipmentType2);

        #endregion

        #region Operator <  (EquipmentType1, EquipmentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EquipmentType1">An EquipmentType.</param>
        /// <param name="EquipmentType2">Another EquipmentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (EquipmentType EquipmentType1,
                                          EquipmentType EquipmentType2)

            => EquipmentType1.CompareTo(EquipmentType2) < 0;

        #endregion

        #region Operator <= (EquipmentType1, EquipmentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EquipmentType1">An EquipmentType.</param>
        /// <param name="EquipmentType2">Another EquipmentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (EquipmentType EquipmentType1,
                                           EquipmentType EquipmentType2)

            => EquipmentType1.CompareTo(EquipmentType2) <= 0;

        #endregion

        #region Operator >  (EquipmentType1, EquipmentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EquipmentType1">An EquipmentType.</param>
        /// <param name="EquipmentType2">Another EquipmentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (EquipmentType EquipmentType1,
                                          EquipmentType EquipmentType2)

            => EquipmentType1.CompareTo(EquipmentType2) > 0;

        #endregion

        #region Operator >= (EquipmentType1, EquipmentType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EquipmentType1">An EquipmentType.</param>
        /// <param name="EquipmentType2">Another EquipmentType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (EquipmentType EquipmentType1,
                                           EquipmentType EquipmentType2)

            => EquipmentType1.CompareTo(EquipmentType2) >= 0;

        #endregion

        #endregion

        #region IComparable<EquipmentType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two EquipmentTypes.
        /// </summary>
        /// <param name="Object">An EquipmentType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is EquipmentType equipmentType
                   ? CompareTo(equipmentType)
                   : throw new ArgumentException("The given object is not an EquipmentType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(EquipmentType)

        /// <summary>
        /// Compares two EquipmentTypes.
        /// </summary>
        /// <param name="EquipmentType">An EquipmentType to compare with.</param>
        public Int32 CompareTo(EquipmentType EquipmentType)

            => String.Compare(InternalId,
                              EquipmentType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<EquipmentType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two EquipmentTypes for equality.
        /// </summary>
        /// <param name="Object">An EquipmentType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is EquipmentType equipmentType &&
                   Equals(equipmentType);

        #endregion

        #region Equals(EquipmentType)

        /// <summary>
        /// Compares two EquipmentTypes for equality.
        /// </summary>
        /// <param name="EquipmentType">An EquipmentType to compare with.</param>
        public Boolean Equals(EquipmentType EquipmentType)

            => String.Equals(InternalId,
                             EquipmentType.InternalId,
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
