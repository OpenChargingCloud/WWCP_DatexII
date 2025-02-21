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
    /// Dynamic information on the status of the refill point.
    /// </summary>
    [XmlType("RefillPointStatus", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class RefillPointStatus(FacilityObjectVersionedReference  Reference,
                                   DateTime?                         LastUpdated                    = null,
                                   OpeningStatus?                    OpeningStatus                  = null,
                                   OperationStatus?                  OperationStatus                = null,
                                   Boolean?                          RegularOperatingHoursInForce   = null,
                                   MultilingualString?               StatusDescription              = null,
                                   AOperatingHours?                  NewOperatingHours              = null,
                                   Fault?                            Fault                          = null)

        : FacilityStatus(Reference,
                         LastUpdated,
                         OpeningStatus,
                         OperationStatus,
                         RegularOperatingHoursInForce,
                         StatusDescription,
                         NewOperatingHours,
                         Fault)

    {

        /// <summary>
        /// Status of the refill point.
        /// </summary>
        [XmlElement("status", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public RefillPointStatusEnum                  Status { get; set; }

        /// <summary>
        /// Amount of delivery units still in stock (with delivery units as defined in the static information model).
        /// </summary>
        [XmlElement("unitsInStock", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Units?                                 UnitsInStock { get; set; }

        /// <summary>
        /// Updates to the energy rate applicable at this refill point.
        /// </summary>
        [XmlElement("energyRateUpdate", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyRateUpdate>          EnergyRateUpdates { get; set; }

        /// <summary>
        /// Estimated waiting time for customers without reservation.
        /// </summary>
        [XmlElement("waitingTime", Namespace = "http://datex2.eu/schema/3/facilities")]
        public TimeSpan?                              WaitingTime { get; set; }

        /// <summary>
        /// Planned status information for the refill point (e.g. reservations).
        /// </summary>
        [XmlElement("plannedRefillPointStatus", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<PlannedRefillPointStatus>  PlannedRefillPointStatuses { get; set; }

        ///// <summary>
        ///// Optional extension element for additional refill point status information.
        ///// </summary>
        //[XmlElement("_refillPointStatusExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? RefillPointStatusExtension { get; set; }

    }

}
