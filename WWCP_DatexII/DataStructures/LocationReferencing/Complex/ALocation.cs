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
    /// The specification of a location either on a network (as a point or a linear location) or as an area.
    /// This may be provided in one or more referencing systems.
    /// </summary>
    [XmlType("Location", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public abstract class ALocation(IEnumerable<ExternalReferencing>?  ExternalReferencing          = null,
                                    PointCoordinates?                  CoordinatesForDisplay        = null,
                                    FacilityLocation?                  FacilityLocation             = null,
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
        [XmlElement("facilityLocation",       Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public FacilityLocation?                 FacilityLocation         { get; } = FacilityLocation;

        /// <summary>
        /// Optional extension element for additional location information.
        /// </summary>
        [XmlElement("_locationExtension",     Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                         LocationExtension        { get; } = LocationExtension;

        #endregion


        #region (protected) ToXMLElements()

        /// <summary>
        /// The location reference part, followed by what a location adds to it.
        /// </summary>
        protected override IEnumerable<Object?> ToXMLElements()
        {

            foreach (var element in base.ToXMLElements())
                yield return element;

            if (ExternalReferencing.Any())
                throw new NotImplementedException("Serializing ExternalReferencing is not implemented yet!");

            if (CoordinatesForDisplay is not null)
                yield return CoordinatesForDisplay.ToXML(DatexIINS.LocationReferencing + "coordinatesForDisplay");

            if (FacilityLocation is not null)
                yield return FacilityLocation.ToXML(DatexIINS.LocationReferencing + "facilityLocation");

            if (LocationExtension is not null)
                yield return new XElement(DatexIINS.LocationReferencing + "_locationExtension", LocationExtension);

        }

        #endregion

    }

}
