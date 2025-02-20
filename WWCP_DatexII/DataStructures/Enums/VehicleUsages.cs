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
    /// Types of usage of a vehicle.
    /// </summary>
    public enum VehicleUsages
    {

        /// <summary>
        /// Vehicle used for agricultural purposes.
        /// </summary>
        [XmlEnum("agricultural")]
        Agricultural,

        /// <summary>
        /// Vehicles operated by a car-sharing company.
        /// </summary>
        [XmlEnum("carSharing")]
        CarSharing,

        /// <summary>
        /// Vehicles that are used to deliver goods in a city area.
        /// </summary>
        [XmlEnum("cityLogistics")]
        CityLogistics,

        /// <summary>
        /// Vehicle which is limited to non-private usage or public transport usage.
        /// </summary>
        [XmlEnum("commercial")]
        Commercial,

        /// <summary>
        /// Vehicle used by the emergency services.
        /// </summary>
        [XmlEnum("emergencyServices")]
        EmergencyServices,

        /// <summary>
        /// Vehicle used by the military.
        /// </summary>
        [XmlEnum("military")]
        Military,

        /// <summary>
        /// Vehicle used for non-commercial or private purposes.
        /// </summary>
        [XmlEnum("nonCommercial")]
        NonCommercial,

        /// <summary>
        /// Vehicle used as part of a patrol service, e.g. road operator or automobile association patrol vehicle.
        /// </summary>
        [XmlEnum("patrol")]
        Patrol,

        /// <summary>
        /// Vehicle used to provide a recovery service.
        /// </summary>
        [XmlEnum("recoveryServices")]
        RecoveryServices,

        /// <summary>
        /// Vehicle used for road maintenance or construction work purposes.
        /// </summary>
        [XmlEnum("roadMaintenanceOrConstruction")]
        RoadMaintenanceOrConstruction,

        /// <summary>
        /// Vehicle used by the road operator.
        /// </summary>
        [XmlEnum("roadOperator")]
        RoadOperator,

        /// <summary>
        /// Vehicle used to provide an authorised taxi service.
        /// </summary>
        [XmlEnum("taxi")]
        Taxi,

        [XmlEnum("_extended")]
        Extended

    }

}
