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

using System.Xml.Serialization;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// Types of equipment.
    /// </summary>
    public enum EquipmentTypes
    {

        /// <summary>
        /// Bike parking.
        /// </summary>
        [XmlEnum("bathRoom")]
        BathRoom,

        /// <summary>
        /// Bike parking.
        /// </summary>
        [XmlEnum("bikeParking")]
        BikeParking,

        /// <summary>
        /// Cash machine.
        /// </summary>
        [XmlEnum("cashMachine")]
        CashMachine,

        /// <summary>
        /// A possibility to create copies of documents.
        /// </summary>
        [XmlEnum("copyMachineOrService")]
        CopyMachineOrService,

        /// <summary>
        /// Medical equipment to provide first aid after heart attacks.
        /// </summary>
        [XmlEnum("defibrillator")]
        Defibrillator,

        /// <summary>
        /// A system to manage digital short range communication (radio beacon transceivers), e.g. for tolling roadside equipment.
        /// </summary>
        [XmlEnum("dsrcReceiver")]
        DsrcReceiver,

        /// <summary>
        /// Possibility to get rid of sewage (especially for motorhomes).
        /// </summary>
        [XmlEnum("dumpingStation")]
        DumpingStation,

        /// <summary>
        /// For charging vehicles, motorhome supply etc. The quantity attribute specifies the number of charging stations.
        /// </summary>
        [XmlEnum("electricChargingStation")]
        ElectricChargingStation,

        /// <summary>
        /// Indication of the availability of elevators.
        /// </summary>
        [XmlEnum("elevator")]
        Elevator,

        /// <summary>
        /// A possibility to send and/or receive faxes.
        /// </summary>
        [XmlEnum("faxMachineOrService")]
        FaxMachineOrService,

        /// <summary>
        /// Fire extinguisher.
        /// </summary>
        [XmlEnum("fireExtinguisher")]
        FireExtinguisher,

        /// <summary>
        /// A hose for water transport in case of fire.
        /// </summary>
        [XmlEnum("fireHose")]
        FireHose,

        /// <summary>
        /// Fire hydrant.
        /// </summary>
        [XmlEnum("fireHydrant")]
        FireHydrant,

        /// <summary>
        /// Equipment to support first aid on injured people. Note that 'defibrillator' is a separate literal.
        /// </summary>
        [XmlEnum("firstAidEquipment")]
        FirstAidEquipment,

        /// <summary>
        /// A technical equipment to remove ice and snow from the roof of lorries.
        /// </summary>
        [XmlEnum("iceFreeScaffold")]
        IceFreeScaffold,

        /// <summary>
        /// An information point with employees.
        /// </summary>
        [XmlEnum("informationPoint")]
        InformationPoint,

        /// <summary>
        /// An unmanned information point.
        /// </summary>
        [XmlEnum("informationStele")]
        InformationStele,

        /// <summary>
        /// Public internet terminal. Charges may be specified using the TariffsAndPayment section.
        /// </summary>
        [XmlEnum("internetTerminal")]
        InternetTerminal,

        /// <summary>
        /// Public wireless internet. Specifying an amount would be the number of hotspots/access points. Charges may be specified using the TariffsAndPayment section.
        /// </summary>
        [XmlEnum("internetWireless")]
        InternetWireless,

        /// <summary>
        /// Possibility to deposit luggage in a safe way.
        /// </summary>
        [XmlEnum("luggageLocker")]
        LuggageLocker,

        /// <summary>
        /// A payment machine, for example a parking ticket machine.
        /// </summary>
        [XmlEnum("paymentMachine")]
        PaymentMachine,

        /// <summary>
        /// Indicates whether any picnicking facilities, such as tables, chairs and shaded areas, are available.
        /// </summary>
        [XmlEnum("picnicFacilities")]
        PicnicFacilities,

        /// <summary>
        /// A playground for children.
        /// </summary>
        [XmlEnum("playground")]
        Playground,

        /// <summary>
        /// Indicates whether there is a public telephone available that can be used with a card.
        /// </summary>
        [XmlEnum("publicCardPhone")]
        PublicCardPhone,

        /// <summary>
        /// Indicates whether there is a public telephone available that can be used with coins.
        /// </summary>
        [XmlEnum("publicCoinPhone")]
        PublicCoinPhone,

        /// <summary>
        /// Indicates whether there is a public telephone available.
        /// </summary>
        [XmlEnum("publicPhone")]
        PublicPhone,

        /// <summary>
        /// Refuse bins for small amounts of garbage (see also 'wasteDisposal').
        /// </summary>
        [XmlEnum("refuseBin")]
        RefuseBin,

        /// <summary>
        /// Resting facilities.
        /// </summary>
        [XmlEnum("restingFacilities")]
        RestingFacilities,

        /// <summary>
        /// A possibility to store valuable possessions in a safe way.
        /// </summary>
        [XmlEnum("safeDeposit")]
        SafeDeposit,

        /// <summary>
        /// A shelter (against wind, sun, etc.).
        /// </summary>
        [XmlEnum("shelter")]
        Shelter,

        /// <summary>
        /// Indicates whether there are shower facilities available.
        /// </summary>
        [XmlEnum("shower")]
        Shower,

        /// <summary>
        /// Equipment to remove snow and ice.
        /// </summary>
        [XmlEnum("snowAndIceRemovalEquipment")]
        SnowAndIceRemovalEquipment,

        /// <summary>
        /// Indicates whether there are toilets available.
        /// </summary>
        [XmlEnum("toilet")]
        Toilet,

        /// <summary>
        /// A terminal where toll charges can be paid manually (this does not mean a toll gate on the road).
        /// </summary>
        [XmlEnum("tollTerminal")]
        TollTerminal,

        /// <summary>
        /// Equipment to measure and refill tyre air pressure.
        /// </summary>
        [XmlEnum("tyreAirPressureEquipment")]
        TyreAirPressureEquipment,

        /// <summary>
        /// A water basin to wash hands, clothes or dishes.
        /// </summary>
        [XmlEnum("waterBasin")]
        WaterBasin,

        /// <summary>
        /// A vending machine for snacks, coffee etc. (without manpower).
        /// </summary>
        [XmlEnum("vendingMachine")]
        VendingMachine,

        /// <summary>
        /// Supply of fresh water, e.g. for motorhomes.
        /// </summary>
        [XmlEnum("waterSupply")]
        WaterSupply,

        /// <summary>
        /// Possibility to get rid of waste in a legal way (e.g. for truckers or motorhomes). Normal refuse bins are not intended here.
        /// </summary>
        [XmlEnum("wasteDisposal")]
        WasteDisposal,

        /// <summary>
        /// Fresh water out of a tap.
        /// </summary>
        [XmlEnum("waterTap")]
        WaterTap,

        /// <summary>
        /// None.
        /// </summary>
        [XmlEnum("none")]
        None,

        /// <summary>
        /// Unknown.
        /// </summary>
        [XmlEnum("unknown")]
        Unknown,

        /// <summary>
        /// Some other equipment. Use the description attribute to specify it.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }


}
