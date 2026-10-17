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
    /// Represents one or more physically separate locations.
    /// Multiple locations may be related, as in an itinerary or route, or may be unrelated.
    /// One LocationReference should not use multiple Location objects to represent the same physical location.
    /// </summary>
    [XmlType("LocationReference", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public abstract class ALocationReference(XElement? LocationReferenceExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Optional extension element for additional location reference information.
        /// </summary>
        [XmlElement("_locationReferenceExtension", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public XElement?  LocationReferenceExtension    { get; } = LocationReferenceExtension;

        #endregion


        #region TryParseXML(XML, out LocationReference, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a location reference, by its xsi:type.
        /// Only point and area locations can be parsed so far.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="LocationReference">The parsed location reference.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                      XML,
                                          [NotNullWhen(true)]  out ALocationReference?  LocationReference,
                                          [NotNullWhen(false)] out String?              ErrorResponse)
        {

            LocationReference = null;

            switch (XML.XSIType())
            {

                case "PointLocation":
                    if (PointLocation.TryParseXML(XML, out var pointLocation, out ErrorResponse)) { LocationReference = pointLocation; return true; }
                    return false;

                case "AreaLocation":
                    if (AreaLocation. TryParseXML(XML, out var areaLocation,  out ErrorResponse)) { LocationReference = areaLocation;  return true; }
                    return false;

                default:
                    ErrorResponse = $"Location references of the type '{XML.XSIType()}' can not be parsed yet!";
                    return false;

            }

        }

        #endregion


        #region (protected) ToXMLElements()

        /// <summary>
        /// The part every location reference shares.
        /// </summary>
        protected virtual IEnumerable<Object?> ToXMLElements()
        {

            if (LocationReferenceExtension is not null)
                yield return LocationReferenceExtension.ToExtensionXML(DatexIINS.LocationReferencing + "_locationReferenceExtension");

        }

        #endregion

        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public abstract XElement ToXML(XName? XMLName = null);

        #endregion

    }

}
