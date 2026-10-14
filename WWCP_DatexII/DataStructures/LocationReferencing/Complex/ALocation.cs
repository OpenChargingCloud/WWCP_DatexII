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
    /// The specification of a location either on a network (as a point or a linear location) or as an area.
    /// This may be provided in one or more referencing systems.
    /// </summary>
    [XmlType("Location", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public abstract class ALocation(IEnumerable<ExternalReferencing>?  ExternalReferencing          = null,
                                    PointCoordinates?                  CoordinatesForDisplay        = null,
                                    AfirFacilityLocation?              FacilityLocation             = null,
                                    XElement?                          LocationExtension            = null,

                                    XElement?                          LocationReferenceExtension   = null)

        : ALocationReference(LocationReferenceExtension)

    {

        #region Properties

        /// <summary>
        /// External referencing information (zero or more entries).
        /// </summary>
        [XmlElement("externalReferencing",    Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public IEnumerable<ExternalReferencing>  ExternalReferencing      { get; } = ExternalReferencing?.Distinct() ?? [];

        /// <summary>
        /// Coordinates that may be used by clients for visual display on user interfaces.
        /// </summary>
        [XmlElement("coordinatesForDisplay",  Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PointCoordinates?                 CoordinatesForDisplay    { get; } = CoordinatesForDisplay;

        /// <summary>
        /// A facility location specification.
        /// </summary>
        [XmlElement("afirFacilityLocation",   Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public AfirFacilityLocation?             FacilityLocation         { get; } = FacilityLocation;

        /// <summary>
        /// Optional extension element for additional location information.
        /// </summary>
        [XmlElement("_locationExtension",     Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                         LocationExtension        { get; } = LocationExtension;

        #endregion


        #region (static) TryParseXML(XML, out Location, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a location, by its xsi:type.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Location">The parsed location.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                             XML,
                                          [NotNullWhen(true)]  out ALocation?  Location,
                                          [NotNullWhen(false)] out String?     ErrorResponse)
        {

            Location = null;

            if (!ALocationReference.TryParseXML(XML, out var locationReference, out ErrorResponse))
                return false;

            if (locationReference is not ALocation location)
            {
                ErrorResponse = $"The location reference of the type '{XML.XSIType()}' is not a location!";
                return false;
            }

            Location = location;
            return true;

        }

        #endregion


        #region (protected static) TryParseLocationParts(XML, out ExternalReferencing, out CoordinatesForDisplay, out FacilityLocation, out LocationExtension, out ErrorResponse)

        /// <summary>
        /// Try to parse the particles every location has: its external referencing, its coordinates for display, and
        /// its typed extension with the facility location of AFIR; other content of the extension is kept as it is.
        /// </summary>
        protected static Boolean TryParseLocationParts(XElement                                          XML,
                                                       out IEnumerable<ExternalReferencing>              ExternalReferencing,
                                                       out PointCoordinates?                             CoordinatesForDisplay,
                                                       out AfirFacilityLocation?                         FacilityLocation,
                                                       out XElement?                                     LocationExtension,
                                                       [NotNullWhen(false)] out String?                  ErrorResponse)
        {

            CoordinatesForDisplay  = null;
            FacilityLocation       = null;
            LocationExtension      = null;

            if (!XML.TryParseOptionalXMLs(DatexIINS.LocationReferencing + "externalReferencing",    "external referencing",     LocationReferencing.ExternalReferencing.TryParseXML, out ExternalReferencing,   out ErrorResponse) ||
                !XML.TryParseOptionalXML (DatexIINS.LocationReferencing + "coordinatesForDisplay",  "coordinates for display",  PointCoordinates.TryParseXML,                        out CoordinatesForDisplay, out ErrorResponse))
            {
                return false;
            }

            if (XML.Element(DatexIINS.LocationReferencing + "_locationExtension") is XElement extension)
            {

                if (!extension.TryParseOptionalXML(DatexIINS.LocationReferencing + "afirFacilityLocation",
                                                   "facility location",
                                                   AfirFacilityLocation.TryParseXML,
                                                   out FacilityLocation,
                                                   out ErrorResponse))
                {
                    return false;
                }

                var others = extension.Elements().Where(element => element.Name != DatexIINS.LocationReferencing + "afirFacilityLocation").ToArray();

                if (others.Length > 0)
                    LocationExtension = others.Length == 1 ? others[0] : new XElement(DatexIINS.LocationReferencing + "_locationExtension", others);

            }

            return true;

        }

        #endregion


        #region (protected) ToXMLElements()

        /// <summary>
        /// The location reference part, followed by what a location adds to it.
        /// </summary>
        protected override IEnumerable<Object?> ToXMLElements()
        {

            foreach (var element in base.ToXMLElements())
                yield return element;

            foreach (var externalReferencing in ExternalReferencing)
                yield return externalReferencing.ToXML(DatexIINS.LocationReferencing + "externalReferencing");

            if (CoordinatesForDisplay is not null)
                yield return CoordinatesForDisplay.ToXML(DatexIINS.LocationReferencing + "coordinatesForDisplay");

            // DATEX II 3.7: the facility location of AFIR is the typed extension of a location.
            if (FacilityLocation is not null || LocationExtension is not null)
                yield return new XElement(DatexIINS.LocationReferencing + "_locationExtension",
                                 FacilityLocation?.ToXML(DatexIINS.LocationReferencing + "afirFacilityLocation"),
                                 LocationExtension);

        }

        #endregion

    }

}
