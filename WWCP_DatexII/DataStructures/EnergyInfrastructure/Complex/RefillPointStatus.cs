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

using System.Xml.Linq;
using System.Xml.Serialization;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Dynamic information on the status of the refill point.
    /// </summary>
    [XmlType("RefillPointStatus", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class RefillPointStatus(RefillPointStatusEnum                   Status,
                                   FacilityObjectVersionedReference        Reference,

                                   Units?                                  UnitsInStock                   = null,
                                   IEnumerable<EnergyRateUpdate>?          EnergyRateUpdates              = null,
                                   TimeSpan?                               WaitingTime                    = null,
                                   IEnumerable<PlannedRefillPointStatus>?  PlannedRefillPointStatuses     = null,

                                   DateTime?                               LastUpdated                    = null,
                                   OpeningStatus?                          OpeningStatus                  = null,
                                   OperationStatus?                        OperationStatus                = null,
                                   Boolean?                                RegularOperatingHoursInForce   = null,
                                   MultilingualString?                     StatusDescription              = null,
                                   AOperatingHours?                        NewOperatingHours              = null,
                                   Fault?                                  Fault                          = null)

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
        [XmlElement("status",                       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public RefillPointStatusEnum                  Status                        { get; set; } = Status;

        /// <summary>
        /// Amount of delivery units still in stock (with delivery units as defined in the static information model).
        /// </summary>
        [XmlElement("unitsInStock",                 Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Units?                                 UnitsInStock                  { get; set; } = UnitsInStock;

        /// <summary>
        /// Updates to the energy rate applicable at this refill point.
        /// </summary>
        [XmlElement("energyRateUpdate",             Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyRateUpdate>          EnergyRateUpdates             { get; set; } = EnergyRateUpdates?.         Distinct() ?? [];

        /// <summary>
        /// Estimated waiting time for customers without reservation.
        /// </summary>
        [XmlElement("waitingTime",                  Namespace = "http://datex2.eu/schema/3/facilities")]
        public TimeSpan?                              WaitingTime                   { get; set; } = WaitingTime;

        /// <summary>
        /// Planned status information for the refill point (e.g. reservations).
        /// </summary>
        [XmlElement("plannedRefillPointStatus",     Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<PlannedRefillPointStatus>  PlannedRefillPointStatuses    { get; set; } = PlannedRefillPointStatuses?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional refill point status information.
        /// </summary>
        [XmlElement("_refillPointStatusExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                              RefillPointStatusExtension    { get; set; }

    }

}
