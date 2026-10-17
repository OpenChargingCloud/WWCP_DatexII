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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// An area defined by a set of polygons according to GML (EN ISO 19136).
    /// </summary>
    [XmlType("GmlMultiPolygon", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class GMLMultiPolygon(MultilingualString?       GMLAreaName                = null,
                                 IEnumerable<GMLPolygon>?  GMLPolygons                = null,
                                 XElement?                 GMLMultiPolygonExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Name of the multi-polygon area.
        /// </summary>
        [XmlElement("gmlAreaName",                Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public MultilingualString?      GMLAreaName                 { get; } = GMLAreaName;

        /// <summary>
        /// A collection of GML polygons that define the area.
        /// </summary>
        [XmlElement("gmlPolygon",                 Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public IEnumerable<GMLPolygon>  GMLPolygons                 { get; } = GMLPolygons?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional multi-polygon information.
        /// </summary>
        [XmlElement("_gmlMultiPolygonExtension",  Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public XElement?                GMLMultiPolygonExtension    { get; } = GMLMultiPolygonExtension;

        #endregion


        #region TryParseXML(XML, out GMLMultiPolygon, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a GML multi polygon.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="GMLMultiPolygon">The parsed GML multi polygon.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                   XML,
                                          [NotNullWhen(true)]  out GMLMultiPolygon?  GMLMultiPolygon,
                                          [NotNullWhen(false)] out String?           ErrorResponse)
        {

            GMLMultiPolygon = null;

            if (!XML.TryParseOptionalXML (DatexIINS.LocationReferencing + "gmlAreaName",  "GML area name",  MultilingualString.TryParseXML,  out MultilingualString?    gmlAreaName,  out ErrorResponse) ||
                !XML.TryParseOptionalXMLs(DatexIINS.LocationReferencing + "gmlPolygon",   "GML polygon",    GMLPolygon.TryParseXML,          out IEnumerable<GMLPolygon> gmlPolygons, out ErrorResponse))
            {
                return false;
            }

            if (!gmlPolygons.Any())
            {
                ErrorResponse = "A GML multi polygon must have at least one polygon!";
                return false;
            }

            GMLMultiPolygon = new GMLMultiPolygon(
                                  gmlAreaName,
                                  gmlPolygons,
                                  XML.Element(DatexIINS.LocationReferencing + "_gmlMultiPolygonExtension")
                              );

            return true;

        }

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.LocationReferencing + "gmlMultiPolygon",

                   GMLAreaName is not null
                       ? GMLAreaName.ToXML(DatexIINS.LocationReferencing + "gmlAreaName")
                       : null,

                   GMLPolygons.Select(gmlPolygon => gmlPolygon.ToXML(DatexIINS.LocationReferencing + "gmlPolygon")),

                   GMLMultiPolygonExtension is not null
                       ? GMLMultiPolygonExtension.ToExtensionXML(DatexIINS.LocationReferencing + "_gmlMultiPolygonExtension")
                       : null

               );

        #endregion

    }

}
