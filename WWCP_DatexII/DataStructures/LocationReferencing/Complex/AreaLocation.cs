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
    /// Location representing a geographic or geometrically defined area which may be qualified by height information 
    /// to provide additional geospatial discrimination (e.g. for snow in an area but only above a certain altitude).
    /// </summary>
    [XmlType("AreaLocation", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class AreaLocation(AreaPlace?                         AreasAtWhichApplicable        = null,
                              NamedArea?                         NamedArea                     = null,
                              GMLMultiPolygon?                   GMLMultiPolygon               = null,
                              AOpenLRAreaLocationReference?      OpenLRAreaLocationReference   = null,
                              XElement?                          AreaLocationExtension         = null,

                              IEnumerable<ExternalReferencing>?  ExternalReferencing           = null,
                              PointCoordinates?                  CoordinatesForDisplay         = null,
                              FacilityLocation?                  FacilityLocation              = null,
                              XElement?                          LocationExtension             = null,

                              XElement?                          LocationReferenceExtension    = null)

        : ALocation(ExternalReferencing,
                    CoordinatesForDisplay,
                    FacilityLocation,
                    LocationExtension,
                    LocationReferenceExtension)

    {

        #region Properties

        /// <summary>
        /// Places, in generic terms, at which the corresponding information applies.
        /// </summary>
        [XmlElement("areasAtWhichApplicable",       Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public AreaPlace?                     AreasAtWhichApplicable         { get; } = AreasAtWhichApplicable;

        /// <summary>
        /// A named area defining boundaries or region.
        /// </summary>
        [XmlElement("namedArea",                    Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public NamedArea?                     NamedArea                      { get; } = NamedArea;

        /// <summary>
        /// A GML MultiPolygon representing the area.
        /// </summary>
        [XmlElement("gmlMultiPolygon",              Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public GMLMultiPolygon?               GMLMultiPolygon                { get; } = GMLMultiPolygon;

        /// <summary>
        /// A reference to an area location defined using OpenLR.
        /// </summary>
        [XmlElement("openlrAreaLocationReference",  Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public AOpenLRAreaLocationReference?  OpenLRAreaLocationReference    { get; } = OpenLRAreaLocationReference;

        /// <summary>
        /// Optional extension element for additional area location information.
        /// </summary>
        [XmlElement("_areaLocationExtension",       Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                      AreaLocationExtension          { get; } = AreaLocationExtension;

        #endregion

    }

}
