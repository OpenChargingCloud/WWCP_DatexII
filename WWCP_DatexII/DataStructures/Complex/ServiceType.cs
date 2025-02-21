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
    /// A specification of service types for the fuelling/charging and payment process.
    /// </summary>
    [XmlType("ServiceType", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class ServiceType(ServiceTypes    ServiceTypeValue,
                             OverallPeriod?  OverallPeriod   = null)
    {

        /// <summary>
        /// Information on different service types for the fuelling/charging and payment process.
        /// </summary>
        [XmlElement("serviceType", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public ServiceTypes    ServiceTypeValue    { get; set; } = ServiceTypeValue;

        /// <summary>
        /// The overall period during which the service is available.
        /// </summary>
        [XmlElement("overallPeriod", Namespace = "http://datex2.eu/schema/3/common")]
        public OverallPeriod?  OverallPeriod       { get; set; } = OverallPeriod;

        ///// <summary>
        ///// Optional extension element for additional service type information.
        ///// </summary>
        //[XmlElement("_serviceTypeExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? ServiceTypeExtension { get; set; }

    }

}
