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

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Dynamic information on the status of the charging point.
    /// </summary>
    [XmlType("ElectricChargingPointStatus", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class ElectricChargingPointStatus(RefillPointStatusEnum                   Status,
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
                                             Fault?                                  Fault                          = null,

                                             TimeSpan?                               RemainingChargingTime          = null,
                                             Volt?                                   CurrentVoltage                 = null,
                                             Watt?                                   CurrentChargingPower           = null,
                                             IEnumerable<DateTime>?                  NextAvailableChargingSlots     = null)

        : RefillPointStatus(Status,
                            Reference,

                            UnitsInStock,
                            EnergyRateUpdates,
                            WaitingTime,
                            PlannedRefillPointStatuses,

                            LastUpdated,
                            OpeningStatus,
                            OperationStatus,
                            RegularOperatingHoursInForce,
                            StatusDescription,
                            NewOperatingHours,
                            Fault)

    {

        /// <summary>
        /// If known, the approximate remaining charging time (in seconds) for the current vehicle on this refill point.
        /// </summary>
        [XmlElement("remainingChargingTime",                  Namespace = "http://datex2.eu/schema/3/common")]
        public TimeSpan?              RemainingChargingTime                   { get; set; } = RemainingChargingTime;

        /// <summary>
        /// The current used voltage.
        /// </summary>
        [XmlElement("currentVoltage",                         Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Volt?                  CurrentVoltage                          { get; set; } = CurrentVoltage;

        /// <summary>
        /// The current charging power in Watts.
        /// </summary>
        [XmlElement("currentChargingPower",                   Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Watt?                  CurrentChargingPower                    { get; set; } = CurrentChargingPower;

        /// <summary>
        /// One or more points of time in the future at which the charging point will be available (not occupied and not reserved).
        /// </summary>
        [XmlElement("nextAvailableChargingSlots",             Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<DateTime>  NextAvailableChargingSlots              { get; set; } = NextAvailableChargingSlots?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional electric charging point status information.
        /// </summary>
        [XmlElement("_electricChargingPointStatusExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?              ElectricChargingPointStatusExtension    { get; set; }

    }

}
