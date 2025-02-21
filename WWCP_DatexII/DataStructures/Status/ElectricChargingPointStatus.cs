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
using System.Xml.Serialization;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    //[XmlType("FacilityObjectStatus", Namespace = "http://datex2.eu/schema/3/facilities")]
    //public class FacilityObjectStatus
    //{
    //    /// <summary>
    //    /// Reference to the corresponding static facility object.
    //    /// </summary>
    //    [XmlElement("reference", Namespace = "http://datex2.eu/schema/3/facilities")]
    //    public FacilityObjectVersionedReference? Reference { get; set; }

    //    /// <summary>
    //    /// Information on the time when the information has been updated the last time.
    //    /// </summary>
    //    [XmlElement("lastUpdated", Namespace = "http://datex2.eu/schema/3/common")]
    //    public DateTime? LastUpdated { get; set; }

    //    /// <summary>
    //    /// The opening status of this facility (open or not).
    //    /// </summary>
    //    [XmlElement("openingStatus", Namespace = "http://datex2.eu/schema/3/facilities")]
    //    public OpeningStatusEnum? OpeningStatus { get; set; }

    //    /// <summary>
    //    /// The operation status of this facility.
    //    /// </summary>
    //    [XmlElement("operationStatus", Namespace = "http://datex2.eu/schema/3/facilities")]
    //    public OperationStatusEnum? OperationStatus { get; set; }

    //    /// <summary>
    //    /// If true, regular operating hours are in force (can be open or closed).
    //    /// </summary>
    //    [XmlElement("regularOperatingHoursInForce", Namespace = "http://datex2.eu/schema/3/common")]
    //    public bool? RegularOperatingHoursInForce { get; set; }

    //    /// <summary>
    //    /// A description for the status of this facility.
    //    /// </summary>
    //    [XmlElement("statusDescription", Namespace = "http://datex2.eu/schema/3/common")]
    //    public MultilingualString? StatusDescription { get; set; }

    //    /// <summary>
    //    /// Overrides the operating hours information specified in the static part either with a new reference or with a new defined version.
    //    /// </summary>
    //    [XmlElement("newOperatingHours", Namespace = "http://datex2.eu/schema/3/facilities")]
    //    public OperatingHours? NewOperatingHours { get; set; }

    //    /// <summary>
    //    /// Fault information.
    //    /// </summary>
    //    [XmlElement("fault", Namespace = "http://datex2.eu/schema/3/common")]
    //    public Fault? Fault { get; set; }

    //    /// <summary>
    //    /// Extension element for additional content.
    //    /// </summary>
    //    [XmlElement("_facilityObjectStatusExtension", Namespace = "http://datex2.eu/schema/3/common")]
    //    public ExtensionType? FacilityObjectStatusExtension { get; set; }
    //}

    //[XmlType("FacilityStatus", Namespace = "http://datex2.eu/schema/3/facilities")]
    //public class FacilityStatus : FacilityObjectStatus
    //{
    //    /// <summary>
    //    /// Supplemental status information for a facility.
    //    /// </summary>
    //    [XmlElement("supplementalFacilityStatus", Namespace = "http://datex2.eu/schema/3/facilities")]
    //    public List<SupplementalFacilityStatus>? SupplementalFacilityStatus { get; set; }

    //    /// <summary>
    //    /// Extension element for additional content on facility status.
    //    /// </summary>
    //    [XmlElement("_facilityStatusExtension", Namespace = "http://datex2.eu/schema/3/common")]
    //    public ExtensionType? FacilityStatusExtension { get; set; }
    //}


    /// <summary>
    /// Dynamic information on the status of the charging point.
    /// </summary>
    public class ElectricChargingPointStatus //: FacilityStatus
    {

        // Referenz auf das ElectricChargingPoint FacilityObject
        [XmlElement(ElementName = "reference",                   Namespace = "http://datex2.eu/schema/3/facilities")]
        public Reference?                             Reference                     { get; set; }


        [XmlElement(ElementName = "lastUpdated",                 Namespace = "http://datex2.eu/schema/3/facilities")]
        public DateTime?                              LastUpdated                   { get; set; }


        // Neue Betriebszeiten – hier exemplarisch als Marker-Klasse für OpenAllHours
        [XmlElement(ElementName = "newOperatingHours",           Namespace = "http://datex2.eu/schema/3/facilities")]
        public OpenAllHours?                          NewOperatingHours             { get; set; }

        /// <summary>
        /// The status of the refill point.
        /// </summary>
        [XmlElement(ElementName = "status",                      Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public RefillPointStatusEnum                  Status                        { get; set; }


        [XmlElement(ElementName = "energyRateUpdate",            Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyRateUpdate>          EnergyRateUpdate              { get; set; } // might be empty!

        /// <summary>
        /// Estimated waiting time for customers without reservation.
        /// </summary>
        [XmlElement(ElementName = "waitingTime",                 Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public WaitingTime?                           WaitingTime                   { get; set; }


        [XmlElement(ElementName = "plannedRefillPointStatus",    Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<PlannedRefillPointStatus>  PlannedRefillPointStatus      { get; set; } // might be empty!

        /// <summary>
        /// If known, the approximate remaining charging time for the current vehicle on this refill point can be specified.
        /// </summary>
        [XmlElement(ElementName = "remainingChargingTime",       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public TimeSpan?                              RemainingChargingTime         { get; set; }  //Seconds

        /// <summary>
        /// Current used voltage.
        /// </summary>
        [XmlElement(ElementName = "currentVoltage",              Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Volt?                                  CurrentVoltage                { get; set; }

        /// <summary>
        /// The current power in Watts.
        /// </summary>
        [XmlElement(ElementName = "currentChargingPower",        Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Watt?                                  CurrentChargingPower          { get; set; }

        /// <summary>
        /// One or more points of time in the future, at which the charging point will be available (not occupied and not reserved).
        /// </summary>
        [XmlElement(ElementName = "nextAvailableChargingSlots",  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<DateTime>?                 NextAvailableChargingSlots    { get; set; }


    }

}
