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

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// List of service channels or devices on which information or data exchanged can be delivered.
    /// </summary>
    public enum InformationDeliveryServices
    {

        /// <summary>
        /// Includes any general delivery channel such as broadcast channels (e.g. radio,
        /// tv, RDS-TMC, TPEG services, etc.) or web publishing available to public or
        /// to specific users, depending on Service Provider policies.
        /// </summary>
        [XmlEnum("anyGeneralDeliveryService")]
        AnyGeneralDeliveryService,

        /// <summary>
        /// Specific services which deliver warning alerts to end users to enhance safety
        /// via any specific application available to drivers, including C-ITS services.
        /// </summary>
        [XmlEnum("safetyServices")]
        SafetyServices,

        /// <summary>
        /// Variable Message Signs or any other visual roadside devices which information
        /// are accessible to drivers which aim to affect driving style improving safety
        /// and road network LoS.
        /// </summary>
        [XmlEnum("vms")]
        Vms,

        [XmlEnum("_extended")]
        Extended

    }

}
