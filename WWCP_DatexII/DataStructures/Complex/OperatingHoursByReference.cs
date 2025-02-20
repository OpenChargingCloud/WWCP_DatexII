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
    /// Operating hours information that is addressed via a reference.
    /// </summary>
    [XmlType("OperatingHoursByReference", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class OperatingHoursByReference(OperatingHoursSpecificationVersionedReference  OperatingHoursReference,
                                           OperatingHoursTableVersionedReference?         OperatingHoursTableReference   = null,

                                           ClosureInformation?                            ClosureInformation             = null)

        : AOperatingHours(ClosureInformation)

    {

        /// <summary>
        /// The reference to an operating hours specification.
        /// </summary>
        [XmlElement("operatingHoursReference",       Namespace = "http://datex2.eu/schema/3/facilities")]
        public OperatingHoursSpecificationVersionedReference  OperatingHoursReference         { get; set; } = OperatingHoursReference;

        /// <summary>
        /// Operation hours table in question defined by a reference.
        /// </summary>
        [XmlElement("operatingHoursTableReference",  Namespace = "http://datex2.eu/schema/3/facilities")]
        public OperatingHoursTableVersionedReference?         OperatingHoursTableReference    { get; set; } = OperatingHoursTableReference;

        ///// <summary>
        ///// Optional extension element for additional operating hours by reference information.
        ///// </summary>
        //[XmlElement("_operatingHoursByReferenceExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? OperatingHoursByReferenceExtension { get; set; }





        // Example:

        // <?xml version="1.0" encoding="UTF-8"?>
        // <operatingHoursByReference xmlns     = "http://datex2.eu/schema/3/facilities" 
        //                            xmlns:com = "http://datex2.eu/schema/3/common">
        //
        //   <operatingHoursReference      id="spec123"  version="1" targetClass="fac:OperatingHoursSpecification" />
        //   <operatingHoursTableReference id="table456" version="2" targetClass="fac:OperatingHoursTable" />
        //
        //   <_operatingHoursByReferenceExtension>
        //     <!-- Additional extension details go here -->
        //   </_operatingHoursByReferenceExtension>
        //
        // </operatingHoursByReference>


    }

}
