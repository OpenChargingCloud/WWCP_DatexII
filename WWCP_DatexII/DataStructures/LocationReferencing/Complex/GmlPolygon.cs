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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// Planar surface defined by one exterior boundary and zero or more interior boundaries.
    /// </summary>
    [XmlType("GmlPolygon", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class GMLPolygon(GMLLinearRing                Exterior,
                            IEnumerable<GMLLinearRing>?  Interior              = null,
                            XElement?                    GMLPolygonExtension   = null)
    {

        #region Properties

        /// <summary>
        /// A boundary of a polygonal surface consisting of a ring, i.e. in the normal 2D case, a closed polygonal line distinguished as exterior.
        /// Such a polygonal line must have at least 4 pairs of coordinates.
        /// </summary>
        [XmlElement("exterior",              Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public GMLLinearRing                Exterior              { get; } = Exterior;

        /// <summary>
        /// A boundary of internal patches of a polygonal surface consisting of a ring feature.
        /// </summary>
        [XmlElement("interior",              Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public IEnumerable<GMLLinearRing>  Interior               { get; } = Interior?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional polygon information.
        /// </summary>
        [XmlElement("_gmlPolygonExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                   GMLPolygonExtension    { get; } = GMLPolygonExtension;

        #endregion


        #region TryParseXML(XML, out GMLPolygon, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a GML polygon.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="GMLPolygon">The parsed GML polygon.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                              XML,
                                          [NotNullWhen(true)]  out GMLPolygon?  GMLPolygon,
                                          [NotNullWhen(false)] out String?      ErrorResponse)
        {

            GMLPolygon = null;

            if (!XML.TryParseMandatoryXML(DatexIINS.LocationReferencing + "exterior",  "exterior",  GMLLinearRing.TryParseXML,  out GMLLinearRing?               exterior,  out ErrorResponse) ||
                !XML.TryParseOptionalXMLs(DatexIINS.LocationReferencing + "interior",  "interior",  GMLLinearRing.TryParseXML,  out IEnumerable<GMLLinearRing>  interior,  out ErrorResponse))
            {
                return false;
            }

            GMLPolygon = new GMLPolygon(
                             exterior,
                             interior,
                             XML.Element(DatexIINS.LocationReferencing + "_gmlPolygonExtension")
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

            => new (XMLName ?? DatexIINS.LocationReferencing + "gmlPolygon",

                   Exterior.ToXML(DatexIINS.LocationReferencing + "exterior"),

                   Interior.Select(interior => interior.ToXML(DatexIINS.LocationReferencing + "interior")),

                   GMLPolygonExtension is not null
                       ? GMLPolygonExtension.ToExtensionXML(DatexIINS.LocationReferencing + "_gmlPolygonExtension")
                       : null

               );

        #endregion

    }

}
