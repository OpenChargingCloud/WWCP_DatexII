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
using System.Diagnostics.CodeAnalysis;

using cloud.charging.open.protocols.DatexII.v3.LocationExtension;
using cloud.charging.open.protocols.DatexII.v3.Facilities;

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
                              AfirFacilityLocation?              FacilityLocation              = null,
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
        [XmlElement("namedArea",                    Namespace = "http://datex2.eu/schema/3/locationReferencing")]
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
        [XmlElement("_areaLocationExtension",       Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public XElement?                      AreaLocationExtension          { get; } = AreaLocationExtension;

        #endregion


        #region TryParseXML(XML, out AreaLocation, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an area location.
        /// An OpenLR reference is not parsed yet.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="AreaLocation">The parsed area location.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                XML,
                                          [NotNullWhen(true)]  out AreaLocation?  AreaLocation,
                                          [NotNullWhen(false)] out String?        ErrorResponse)
        {

            AreaLocation = null;

            if (!TryParseLocationParts(XML,
                                       out var externalReferencing,
                                       out var coordinatesForDisplay,
                                       out var facilityLocation,
                                       out var locationExtension,
                                       out ErrorResponse))
            {
                return false;
            }

            if (!XML.TryParseOptionalValue(DatexIINS.LocationReferencing + "areasAtWhichApplicable",  "areas at which applicable",  LocationReferencing.AreaPlace.TryParse,           out AreaPlace?        areasAtWhichApplicable, out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.LocationReferencing + "namedArea",               "named area",                 LocationReferencing.NamedArea.TryParseXML,        out NamedArea?        namedArea,              out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.LocationReferencing + "gmlMultiPolygon",         "GML multi polygon",          LocationReferencing.GMLMultiPolygon.TryParseXML,  out GMLMultiPolygon?  gmlMultiPolygon,        out ErrorResponse))
            {
                return false;
            }

            AreaLocation = new AreaLocation(
                               areasAtWhichApplicable,
                               namedArea,
                               gmlMultiPolygon,
                               null,
                               XML.Element(DatexIINS.LocationReferencing + "_areaLocationExtension"),
                               externalReferencing,
                               coordinatesForDisplay,
                               facilityLocation,
                               locationExtension,
                               XML.Element(DatexIINS.LocationReferencing + "_locationReferenceExtension")
                           );

            return true;

        }

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public override XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.Facilities + "locationReference",

                   new XAttribute(DatexIINS.XSI + "type",   "loc:AreaLocation"),

                   ToXMLElements(),

                   AreasAtWhichApplicable.HasValue
                       ? new XElement(DatexIINS.LocationReferencing + "areasAtWhichApplicable",   AreasAtWhichApplicable.Value.ToString())
                       : null,

                   NamedArea is not null
                       ? NamedArea.ToXML(DatexIINS.LocationReferencing + "namedArea")
                       : null,

                   GMLMultiPolygon is not null
                       ? GMLMultiPolygon.ToXML(DatexIINS.LocationReferencing + "gmlMultiPolygon")
                       : null,

                   OpenLRAreaLocationReference is not null
                       ? throw new NotImplementedException("Serializing an OpenLR area location reference is not implemented yet!")
                       : null,

                   AreaLocationExtension is not null
                       ? AreaLocationExtension.ToExtensionXML(DatexIINS.LocationReferencing + "_areaLocationExtension")
                       : null

               );

        #endregion

    }

}
