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

using System.Xml.Serialization;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;
using System.Xml.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A site where vehicles can be supplied with energy, including all buildings, stations,
    /// parking spaces and other associated services.
    /// </summary>
    [XmlType("EnergyInfrastructureSite", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyInfrastructureSite(String                                     Id,
                                          String                                     Version,

                                          MultilingualString?                        Name                                = null,
                                          IEnumerable<MultilingualString>?           Alias                               = null,
                                          String?                                    ExternalIdentifier                  = null,
                                          DateTimeOffset?                            LastUpdated                         = null,
                                          MultilingualString?                        Description                         = null,
                                          IEnumerable<Accessibility>?                Accessibility                       = null,
                                          IEnumerable<MultilingualString>?           AdditionalInformation               = null,
                                          IEnumerable<URL>?                          InformationWebsites                 = null,
                                          IEnumerable<URL>?                          PhotoURLs                           = null,
                                          IEnumerable<Image>?                        Photos                              = null,
                                          AOperatingHours?                           OperatingHours                      = null,
                                          ALocationReference?                        LocationReference                   = null,
                                          AOrganisation?                             Owner                               = null,
                                          AOrganisation?                             Operator                            = null,
                                          AOrganisation?                             Helpdesk                            = null,
                                          IEnumerable<VehicleCharacteristics>?       ApplicableForVehicles               = null,
                                          Dimension?                                 Dimension                           = null,
                                          Amenities?                                 Amenities                           = null,

                                          IEnumerable<ASupplementalFacility>?        SupplementalFacilities              = null,
                                          IEnumerable<DedicatedParkingSpaces>?       DedicatedParkingSpaces              = null,

                                          EnergyInfrastructureSiteType?              TypeOfSite                          = null,
                                          MultilingualString?                        Brand                               = null,
                                          IEnumerable<UserType>?                     ExclusiveUsers                      = null,
                                          IEnumerable<UserType>?                     PreferredUsers                      = null,
                                          IEnumerable<Service>?                      ServiceTypes                        = null,
                                          IEnumerable<ALocation>?                    Entrances                           = null,
                                          IEnumerable<ALocation>?                    Exits                               = null,
                                          IEnumerable<EnergyInfrastructureStation>?  EnergyInfrastructureStations        = null,

                                          XElement?                                  FacilityObjectExtension             = null,
                                          XElement?                                  FacilityExtension                   = null,
                                          XElement?                                  EnergyInfrastructureSiteExtension   = null)

        : Facility(Id,
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

                   SupplementalFacilities,
                   DedicatedParkingSpaces,

                   FacilityObjectExtension,
                   FacilityExtension)

    {

        #region Properties

        /// <summary>
        /// Specifies the type of the site.
        /// </summary>
        [XmlElement("typeOfSite",                          Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public EnergyInfrastructureSiteType?             TypeOfSite                           { get; } = TypeOfSite;

        /// <summary>
        /// The brand of the site.
        /// </summary>
        [XmlElement("brand",                               Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?                       Brand                                { get; } = Brand;

        /// <summary>
        /// Limitation to a set of users (exclusive).
        /// </summary>
        [XmlElement("exclusiveUsers",                      Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<UserType>                     ExclusiveUsers                       { get; } = ExclusiveUsers?.              Distinct() ?? [];

        /// <summary>
        /// Users that are preferred at this site (but not exclusive).
        /// </summary>
        [XmlElement("preferredUsers",                      Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<UserType>                     PreferredUsers                       { get; } = PreferredUsers?.              Distinct() ?? [];

        /// <summary>
        /// Specifies the type of service available at an EnergyInfrastructureSite.
        /// </summary>
        [XmlElement("serviceType",                         Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<Service>                      ServiceTypes                         { get; } = ServiceTypes?.                Distinct() ?? [];

        /// <summary>
        /// Possibility to specify the location of the site's entrance.
        /// </summary>
        [XmlElement("entrance",                            Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public IEnumerable<ALocation>                    Entrances                            { get; } = Entrances?.                   Distinct() ?? [];

        /// <summary>
        /// Possibility to specify the location of the site's exit.
        /// </summary>
        [XmlElement("exit",                                Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public IEnumerable<ALocation>                    Exits                                { get; } = Exits?.                       Distinct() ?? [];

        /// <summary>
        /// Specifications of charging stations on the site.
        /// </summary>
        [XmlElement("energyInfrastructureStation",         Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyInfrastructureStation>  EnergyInfrastructureStations         { get; } = EnergyInfrastructureStations?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional EnergyInfrastructureSite information.
        /// </summary>
        [XmlElement("_energyInfrastructureSiteExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                 EnergyInfrastructureSiteExtension    { get; } = EnergyInfrastructureSiteExtension;

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "energyInfrastructureSite",

                   new XAttribute("id",        Id),
                   new XAttribute("version",   Version),

                   // --- FacilityObject ---------------------------------------

                   Name is not null
                       ? Name.ToXML(DatexIINS.Facilities + "name")
                       : null,

                   Alias.Select(alias => alias.ToXML(DatexIINS.Facilities + "alias")),

                   ExternalIdentifier is not null && ExternalIdentifier.Length > 0
                       ? new XElement(DatexIINS.Facilities + "externalIdentifier",   ExternalIdentifier)
                       : null,

                   LastUpdated.HasValue
                       ? new XElement(DatexIINS.Facilities + "lastUpdated",          LastUpdated.Value.ToISO8601WithOffset())
                       : null,

                   Description is not null
                       ? Description.ToXML(DatexIINS.Facilities + "description")
                       : null,

                   Accessibility.Select(accessibility => new XElement(DatexIINS.Facilities + "accessibility", accessibility.ToString())),

                   AdditionalInformation.Select(additionalInformation => additionalInformation.ToXML(DatexIINS.Facilities + "additionalInformation")),

                   // An UrlLink is a complex type, not a bare URL.
                   InformationWebsites.Select(informationWebsite => new XElement(DatexIINS.Facilities + "informationWebsite",
                                                                        new XElement(DatexIINS.Common + "urlLinkAddress", informationWebsite.ToString()))),

                   PhotoURLs.          Select(photoURL           => new XElement(DatexIINS.Facilities + "photoUrl",
                                                                        new XElement(DatexIINS.Common + "urlLinkAddress", photoURL.          ToString()))),

                   Photos?.Select(photo => photo.ToXML(DatexIINS.Facilities + "photo")),

                   OperatingHours is not null
                       ? OperatingHours.ToXML(DatexIINS.Facilities + "operatingHours")
                       : null,

                   LocationReference is not null
                       ? throw new NotImplementedException("Serializing a LocationReference is not implemented yet!")
                       : null,

                   Owner is not null
                       ? throw new NotImplementedException("Serializing an Organisation is not implemented yet!")
                       : null,

                   Operator is not null
                       ? throw new NotImplementedException("Serializing an Organisation is not implemented yet!")
                       : null,

                   Helpdesk is not null
                       ? throw new NotImplementedException("Serializing an Organisation is not implemented yet!")
                       : null,

                   ApplicableForVehicles.Any()
                       ? throw new NotImplementedException("Serializing VehicleCharacteristics is not implemented yet!")
                       : null,

                   Dimension is not null
                       ? throw new NotImplementedException("Serializing a Dimension is not implemented yet!")
                       : null,

                   Amenities is not null
                       ? throw new NotImplementedException("Serializing Amenities is not implemented yet!")
                       : null,

                   FacilityObjectExtension is not null
                       ? new XElement(DatexIINS.Facilities + "_facilityObjectExtension", FacilityObjectExtension)
                       : null,

                   // --- Facility ---------------------------------------------

                   SupplementalFacilities.Any()
                       ? throw new NotImplementedException("Serializing a SupplementalFacility is not implemented yet!")
                       : null,

                   DedicatedParkingSpaces.Any()
                       ? throw new NotImplementedException("Serializing DedicatedParkingSpaces is not implemented yet!")
                       : null,

                   FacilityExtension is not null
                       ? new XElement(DatexIINS.Facilities + "_facilityExtension", FacilityExtension)
                       : null,

                   // --- EnergyInfrastructureSite -----------------------------

                   TypeOfSite.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "typeOfSite",       TypeOfSite.Value.ToString())
                       : null,

                   Brand is not null
                       ? Brand.ToXML(DatexIINS.EnergyInfrastructure + "brand")
                       : null,

                   ExclusiveUsers.Select(exclusiveUser => new XElement(DatexIINS.EnergyInfrastructure + "exclusiveUsers", exclusiveUser.ToString())),
                   PreferredUsers.Select(preferredUser => new XElement(DatexIINS.EnergyInfrastructure + "preferredUsers", preferredUser.ToString())),

                   ServiceTypes.  Select(serviceType   => serviceType.ToXML(DatexIINS.EnergyInfrastructure + "serviceType")),

                   Entrances.Any()
                       ? throw new NotImplementedException("Serializing a Location is not implemented yet!")
                       : null,

                   Exits.Any()
                       ? throw new NotImplementedException("Serializing a Location is not implemented yet!")
                       : null,

                   EnergyInfrastructureStations.Any()
                       ? throw new NotImplementedException("Serializing an EnergyInfrastructureStation is not implemented yet!")
                       : null,

                   EnergyInfrastructureSiteExtension is not null
                       ? new XElement(DatexIINS.EnergyInfrastructure + "_energyInfrastructureSiteExtension", EnergyInfrastructureSiteExtension)
                       : null

               );

        #endregion

    }

}
