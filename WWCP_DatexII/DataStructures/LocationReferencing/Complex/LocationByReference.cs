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

using cloud.charging.open.protocols.DatexII.v3.LocationExtension;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// A location defined by reference to a predefined location.
    /// </summary>
    [XmlType("LocationByReference", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class LocationByReference(PredefinedLocationVersionedReference  PredefinedLocationReference,

                                     IEnumerable<ExternalReferencing>?     ExternalReferencing     = null,
                                     PointCoordinates?                     CoordinatesForDisplay   = null,
                                     FacilityLocation?                     FacilityLocation        = null)

        : ALocation(ExternalReferencing,
                    CoordinatesForDisplay,
                    FacilityLocation)

    {

        /// <summary>
        /// A reference to a versioned predefined location.
        /// </summary>
        [XmlElement("predefinedLocationReference",    Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PredefinedLocationVersionedReference  PredefinedLocationReference     { get; set; } = PredefinedLocationReference;

        /// <summary>
        /// Optional extension element for additional location by reference information.
        /// </summary>
        [XmlElement("_locationByReferenceExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                             LocationByReferenceExtension    { get; set; }

    }

}
