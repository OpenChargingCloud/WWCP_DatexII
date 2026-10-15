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

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Dedicated parking spaces directly belonging to a facility (which is usually not a parking site itself).
    /// </summary>
    [XmlType("DedicatedParkingSpaces", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public class DedicatedParkingSpaces(String                                Id,
                                        String                                Version,
                                        UInt16                                NumberOfSpaces,

                                        MultilingualString?                   Name                              = null,
                                        IEnumerable<MultilingualString>?      Alias                             = null,
                                        IEnumerable<ExternalIdentifier>?       ExternalIdentifier                = null,
                                        DateTimeOffset?                       LastUpdated                       = null,
                                        MultilingualString?                   Description                       = null,
                                        IEnumerable<Accessibility>?           Accessibility                     = null,
                                        IEnumerable<MultilingualString>?      AdditionalInformation             = null,
                                        IEnumerable<URL>?                     InformationWebsites               = null,
                                        IEnumerable<URL>?                     PhotoURLs                         = null,
                                        IEnumerable<Image>?                   Photos                            = null,
                                        AOperatingHours?                      OperatingHours                    = null,
                                        ALocationReference?                   LocationReference                 = null,
                                        AOrganisation?                        Owner                             = null,
                                        AOrganisation?                        Operator                          = null,
                                        AOrganisation?                        Helpdesk                          = null,
                                        IEnumerable<VehicleCharacteristics>?  ApplicableForVehicles             = null,
                                        Dimension?                            Dimension                         = null,
                                        Amenities?                            Amenities                         = null,

                                        IEnumerable<UserType>?                UserSpecific                      = null,
                                        XElement?                             DedicatedParkingSpacesExtension   = null)

        : AFacilityObject(Id,
                          Version,

                          Name,
                          Alias,
                          ExternalIdentifier,
                          LastUpdated,
                          Description,
                          Accessibility,
                          AdditionalInformation,
                          InformationWebsites,
                          PhotoURLs,
                          Photos,
                          OperatingHours,
                          LocationReference,
                          Owner,
                          Operator,
                          Helpdesk,
                          ApplicableForVehicles,
                          Dimension,
                          Amenities,

                          DedicatedParkingSpacesExtension)

    {

        #region Properties

        /// <summary>
        /// The number of parking spaces.
        /// </summary>
        [XmlElement(ElementName = "numberOfSpaces",      Namespace = "http://datex2.eu/schema/3/afirFacilities")]
        public UInt16                 NumberOfSpaces                     { get; } = NumberOfSpaces;

        /// <summary>
        /// The indicated parking spaces are specifically for the given set of users.
        /// </summary>
        [XmlElement(ElementName = "userSpecific",        Namespace = "http://datex2.eu/schema/3/afirFacilities")]
        public IEnumerable<UserType>  UserSpecific                       { get; } = UserSpecific ?? [];

        /// <summary>
        /// Optional extension element for additional dedicated parking spaces information.
        /// </summary>
        [XmlElement("_dedicatedParkingSpacesExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?              DedicatedParkingSpacesExtension    { get; }

        #endregion


        #region TryParseXML(XML, out DedicatedParkingSpaces, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of dedicated parking spaces.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="DedicatedParkingSpaces">The parsed dedicated parking spaces.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                          XML,
                                          [NotNullWhen(true)]  out DedicatedParkingSpaces?  DedicatedParkingSpaces,
                                          [NotNullWhen(false)] out String?                  ErrorResponse)
        {

            DedicatedParkingSpaces = null;

            if (!FacilityObjectParts.TryParseXML(XML, out var parts, out ErrorResponse))
                return false;

            if (!XML.TryParseMandatoryValue (DatexIINS.EnergyInfrastructure + "numberOfSpaces",  "number of spaces",  DatexIIXMLExtensions.TryParseUInt16, out UInt16                numberOfSpaces, out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.EnergyInfrastructure + "userSpecific",    "user specific",     UserType.TryParse,                   out IEnumerable<UserType> userSpecific,   out ErrorResponse))
            {
                return false;
            }

            DedicatedParkingSpaces = new DedicatedParkingSpaces(
                                         parts.Id,
                                         parts.Version,
                                         numberOfSpaces,
                                         parts.Name,
                                         parts.Alias,
                                         parts.ExternalIdentifier,
                                         parts.LastUpdated,
                                         parts.Description,
                                         parts.Accessibility,
                                         parts.AdditionalInformation,
                                         parts.InformationWebsites,
                                         parts.PhotoURLs,
                                         parts.Photos,
                                         parts.OperatingHours,
                                         parts.LocationReference,
                                         parts.Owner,
                                         parts.Operator,
                                         parts.Helpdesk,
                                         parts.ApplicableForVehicles,
                                         parts.Dimension,
                                         parts.Amenities,
                                         userSpecific,
                                         XML.Element(DatexIINS.EnergyInfrastructure + "_dedicatedParkingSpacesExtension")
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

            => new (XMLName ?? DatexIINS.Facilities + "dedicatedParkingSpaces",

                   new XAttribute("id",        Id),
                   new XAttribute("version",   Version),

                   ToXMLElements(),

                   new XElement(DatexIINS.EnergyInfrastructure + "numberOfSpaces",   NumberOfSpaces),

                   UserSpecific.Select(userSpecific => new XElement(DatexIINS.EnergyInfrastructure + "userSpecific", userSpecific.ToString())),

                   DedicatedParkingSpacesExtension is not null
                       ? DedicatedParkingSpacesExtension.ToExtensionXML(DatexIINS.EnergyInfrastructure + "_dedicatedParkingSpacesExtension")
                       : null

               );

        #endregion

    }

}
