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
    public class PointLocation(PointByCoordinates?                  PointByCoordinates                   = null,
                               AOpenLRPointLocationReference?       OpenLRPointLocationReference         = null,
                               XElement?                            PointLocationExtension               = null,

                               SupplementaryPositionalDescription?  SupplementaryPositionalDescription   = null,
                               XElement?                            NetworkLocationExtension             = null,

                               IEnumerable<ExternalReferencing>?    ExternalReferencing                  = null,
                               PointCoordinates?                    CoordinatesForDisplay                = null,
                               FacilityLocation?                    FacilityLocation                     = null,
                               XElement?                            LocationExtension                    = null,

                               XElement?                            LocationReferenceExtension           = null)

        : ANetworkLocation(SupplementaryPositionalDescription,
                           NetworkLocationExtension,

                           ExternalReferencing,
                           CoordinatesForDisplay,
                           FacilityLocation,
                           LocationExtension,

                           LocationReferenceExtension)
    {

        #region Properties

        /// <summary>
        /// Specifies the point using coordinates.
        /// </summary>
        [XmlElement("pointByCoordinates",            Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PointByCoordinates?             PointByCoordinates              { get; } = PointByCoordinates;

        /// <summary>
        /// Reference to an OpenLR point location.
        /// </summary>
        [XmlElement("openlrPointLocationReference",  Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public AOpenLRPointLocationReference?  OpenLRPointLocationReference    { get; } = OpenLRPointLocationReference;

        /// <summary>
        /// Optional extension element for additional point location information.
        /// </summary>
        [XmlElement("_pointLocationExtension",       Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                       PointLocationExtension          { get; } = PointLocationExtension;

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public override XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.Facilities + "locationReference",

                   // The schema declares locationReference with the abstract type.
                   new XAttribute(DatexIINS.XSI + "type",   "loc:PointLocation"),

                   ToXMLElements(),

                   PointByCoordinates is not null
                       ? PointByCoordinates.ToXML(DatexIINS.LocationReferencing + "pointByCoordinates")
                       : null,

                   OpenLRPointLocationReference is not null
                       ? throw new NotImplementedException("Serializing an OpenLR point location reference is not implemented yet!")
                       : null,

                   PointLocationExtension is not null
                       ? new XElement(DatexIINS.LocationReferencing + "_pointLocationExtension", PointLocationExtension)
                       : null

               );

        #endregion

    }

}
