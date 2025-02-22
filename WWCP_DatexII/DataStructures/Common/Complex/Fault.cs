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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Information about a fault relating to a specific piece of equipment or process.
    /// </summary>
    [XmlType("Fault", Namespace = "http://datex2.eu/schema/3/common")]
    public class Fault(DateTime             FaultLastUpdateTime,
                       String?              FaultIdentifier         = null,
                       MultilingualString?  FaultDescription        = null,
                       DateTime?            FaultCreationTime       = null,
                       FaultSeverities?     FaultImpactSeverity     = null,
                       FaultUrgencies?      FaultUrgencyToRectify   = null,
                       String?              ManufacturerFaultCode   = null)

    {

        /// <summary>
        /// The date and time at which the fault information as specified in this instance was last updated.
        /// </summary>
        [XmlElement("faultLastUpdateTime", Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime             FaultLastUpdateTime      { get; set; } = FaultLastUpdateTime;

        /// <summary>
        /// Unique identifier of the fault.
        /// </summary>
        [XmlElement("faultIdentifier", Namespace = "http://datex2.eu/schema/3/common")]
        public String?              FaultIdentifier          { get; set; } = FaultIdentifier;

        /// <summary>
        /// Textual description of the fault.
        /// </summary>
        [XmlElement("faultDescription", Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?  FaultDescription         { get; set; } = FaultDescription;

        /// <summary>
        /// The date and time at which the fault was originally recorded/reported.
        /// </summary>
        [XmlElement("faultCreationTime", Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime?            FaultCreationTime        { get; set; } = FaultCreationTime;

        /// <summary>
        /// The severity of the fault in terms of how it affects the usability of the equipment or the reliability of the data generated.
        /// </summary>
        [XmlElement("faultImpactSeverity", Namespace = "http://datex2.eu/schema/3/common")]
        public FaultSeverities?     FaultImpactSeverity      { get; set; } = FaultImpactSeverity;

        /// <summary>
        /// The urgency to rectify the fault.
        /// </summary>
        [XmlElement("faultUrgencyToRectify", Namespace = "http://datex2.eu/schema/3/common")]
        public FaultUrgencies?      FaultUrgencyToRectify    { get; set; } = FaultUrgencyToRectify;

        /// <summary>
        /// A manufacturer specific code for the fault.
        /// </summary>
        [XmlElement("manufacturerFaultCode", Namespace = "http://datex2.eu/schema/3/common")]
        public String?              ManufacturerFaultCode    { get; set; } = ManufacturerFaultCode;

        /// <summary>
        /// Optional extension element for additional fault information.
        /// </summary>
        [XmlElement("_faultExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            FaultExtension           { get; set; }

    }

}
