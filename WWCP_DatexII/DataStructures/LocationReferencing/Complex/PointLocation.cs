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
    /// Location representing a single geospatial point.
    /// </summary>
    [XmlType("PointLocation", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class PointLocation(IEnumerable<ExternalReferencing>?    ExternalReferencing                  = null,
                               PointCoordinates?                    CoordinatesForDisplay                = null,
                               FacilityLocation?                    FacilityLocation                     = null,
                               SupplementaryPositionalDescription?  SupplementaryPositionalDescription   = null,

                               PointByCoordinates?                  PointByCoordinates                   = null,
                               AOpenlrPointLocationReference?       OpenlrPointLocationReference         = null)

        : ANetworkLocation(ExternalReferencing,
                           CoordinatesForDisplay,
                           FacilityLocation,
                           SupplementaryPositionalDescription)

    {

        /// <summary>
        /// Specifies the point using coordinates.
        /// </summary>
        [XmlElement("pointByCoordinates",            Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PointByCoordinates?             PointByCoordinates              { get; set; } = PointByCoordinates;

        /// <summary>
        /// Reference to an OpenLR point location.
        /// </summary>
        [XmlElement("openlrPointLocationReference",  Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public AOpenlrPointLocationReference?  OpenlrPointLocationReference    { get; set; } = OpenlrPointLocationReference;

        /// <summary>
        /// Optional extension element for additional point location information.
        /// </summary>
        [XmlElement("_pointLocationExtension",       Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                       PointLocationExtension          { get; set; }

    }

}
