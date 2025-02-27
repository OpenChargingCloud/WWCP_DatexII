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

namespace cloud.charging.open.protocols.DatexII.v3.LocationExtension
{

    /// <summary>
    /// A location for which a time zone and an address can be specified.
    /// </summary>
    [XmlType("FacilityLocation", Namespace = "http://datex2.eu/schema/3/locationExtension")]
    public class FacilityLocation(Facilities.TimeZone?    TimeZone                    = null,
                                  Address?                Address                     = null,
                                  IEnumerable<NutsArea>?  NutsAreas                   = null,
                                  XElement?               FacilityLocationExtension   = null)
    {

        #region Properties

        /// <summary>
        /// The time zone the facility is located in.
        /// </summary>
        [XmlElement("timeZone",                    Namespace = "http://datex2.eu/schema/3/facilities")]
        public Facilities.TimeZone?   TimeZone                     { get; } = TimeZone;

        /// <summary>
        /// An address specification following ISO 19160-4.
        /// </summary>
        [XmlElement("address",                     Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public Address?               Address                      { get; } = Address;

        /// <summary>
        /// One or more NUTS areas.
        /// </summary>
        [XmlElement("nutsArea",                    Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public IEnumerable<NutsArea>  NutsAreas                    { get; } = NutsAreas?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional facility location information.
        /// </summary>
        [XmlElement("_facilityLocationExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?              FacilityLocationExtension    { get; } = FacilityLocationExtension;

        #endregion

    }

}
