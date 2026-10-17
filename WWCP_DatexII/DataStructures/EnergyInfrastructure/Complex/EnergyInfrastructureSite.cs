/*
 * Copyright (c) 2014-2026 GraphDefined GmbH <achim.friedland@graphdefined.com>
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
using System.Xml.Linq;
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A site where vehicles can be supplied with energy, including all buildings, stations,
    /// parking spaces and other associated services.
    /// </summary>
    [XmlType("EnergyInfrastructureSite", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public class EnergyInfrastructureSite(String                                     Id,
                                          String                                     Version,

                                          MultilingualString?                        Name                                = null,
                                          IEnumerable<MultilingualString>?           Alias                               = null,
                                          IEnumerable<ExternalIdentifier>?            ExternalIdentifier                  = null,
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
                                          AOrganisation?                             EnergyDistributor                   = null,
                                          IEnumerable<AOrganisation>?                MobilityServiceProviders            = null,
                                          IEnumerable<AOrganisation>?                RoamingPlatforms                    = null,

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
        [XmlElement("typeOfSite",                          Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public EnergyInfrastructureSiteType?             TypeOfSite                           { get; } = TypeOfSite;

        /// <summary>
        /// The brand of the site.
        /// </summary>
        [XmlElement("brand",                               Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public MultilingualString?                       Brand                                { get; } = Brand;

        /// <summary>
        /// Limitation to a set of users (exclusive).
        /// </summary>
        [XmlElement("exclusiveUsers",                      Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<UserType>                     ExclusiveUsers                       { get; } = ExclusiveUsers?.              Distinct() ?? [];

        /// <summary>
        /// Users that are preferred at this site (but not exclusive).
        /// </summary>
        [XmlElement("preferredUsers",                      Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<UserType>                     PreferredUsers                       { get; } = PreferredUsers?.              Distinct() ?? [];

        /// <summary>
        /// Specifies the type of service available at an EnergyInfrastructureSite.
        /// </summary>
        [XmlElement("serviceType",                         Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<Service>                      ServiceTypes                         { get; } = ServiceTypes?.                Distinct() ?? [];

        /// <summary>
        /// Possibility to specify the location of the site's entrance.
        /// </summary>
        [XmlElement("entrance",                            Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<ALocation>                    Entrances                            { get; } = Entrances?.                   Distinct() ?? [];

        /// <summary>
        /// Possibility to specify the location of the site's exit.
        /// </summary>
        [XmlElement("exit",                                Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<ALocation>                    Exits                                { get; } = Exits?.                       Distinct() ?? [];

        /// <summary>
        /// Specifications of charging stations on the site.
        /// </summary>
        [XmlElement("energyInfrastructureStation",         Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<EnergyInfrastructureStation>  EnergyInfrastructureStations         { get; } = EnergyInfrastructureStations?.Distinct() ?? [];

        /// <summary>
        /// The distributor of the energy (DATEX II 3.7).
        /// </summary>
        public AOrganisation?                            EnergyDistributor                    { get; } = EnergyDistributor;

        /// <summary>
        /// Mobility service providers offering contract-based recharging at the site (DATEX II 3.7).
        /// </summary>
        public IEnumerable<AOrganisation>                MobilityServiceProviders             { get; } = MobilityServiceProviders?.    Distinct() ?? [];

        /// <summary>
        /// Roaming platforms the site is connected to (DATEX II 3.7).
        /// </summary>
        public IEnumerable<AOrganisation>                RoamingPlatforms                     { get; } = RoamingPlatforms?.            Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional EnergyInfrastructureSite information.
        /// </summary>
        [XmlElement("_energyInfrastructureSiteExtension",  Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public XElement?                                 EnergyInfrastructureSiteExtension    { get; } = EnergyInfrastructureSiteExtension;

        #endregion


        #region TryParseXML(XML, out EnergyInfrastructureSite, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an energy infrastructure site.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyInfrastructureSite">The parsed energy infrastructure site.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                            XML,
                                          [NotNullWhen(true)]  out EnergyInfrastructureSite?  EnergyInfrastructureSite,
                                          [NotNullWhen(false)] out String?                    ErrorResponse)
        {

            EnergyInfrastructureSite = null;

            if (!FacilityParts.TryParseXML(XML, out var facility, out ErrorResponse))
                return false;

            if (!XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "typeOfSite",                   "type of site",                   EnergyInfrastructureSiteType.TryParse,  out EnergyInfrastructureSiteType?          typeOfSite,   out ErrorResponse) ||
                !XML.TryParseOptionalXML   (DatexIINS.EnergyInfrastructure + "brand",                        "brand",                          MultilingualString.TryParseXML,         out MultilingualString?                    brand,        out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.EnergyInfrastructure + "exclusiveUsers",               "exclusive users",                UserType.TryParse,                      out IEnumerable<UserType>                  exclusiveUsers, out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.EnergyInfrastructure + "preferredUsers",               "preferred users",                UserType.TryParse,                      out IEnumerable<UserType>                  preferredUsers, out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "serviceType",                  "service type",                   Service.TryParseXML,                    out IEnumerable<Service>                   serviceTypes, out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "energyInfrastructureStation",  "energy infrastructure station",  EnergyInfrastructureStation.TryParseXML, out IEnumerable<EnergyInfrastructureStation> stations,  out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "entrance",                     "entrance",                       ALocation.TryParseXML,                  out IEnumerable<ALocation>                 entrances,    out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "exit",                         "exit",                           ALocation.TryParseXML,                  out IEnumerable<ALocation>                 exits,        out ErrorResponse) ||
                !XML.TryParseOptionalXML   (DatexIINS.EnergyInfrastructure + "energyDistributor",            "energy distributor",             AOrganisation.TryParseXML,              out AOrganisation?                         energyDistributor,        out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "mobilityServiceProvider",      "mobility service provider",      AOrganisation.TryParseXML,              out IEnumerable<AOrganisation>             mobilityServiceProviders, out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "roamingPlatform",              "roaming platform",               AOrganisation.TryParseXML,              out IEnumerable<AOrganisation>             roamingPlatforms,         out ErrorResponse))
            {
                return false;
            }

            var parts = facility.Object;

            EnergyInfrastructureSite = new EnergyInfrastructureSite(
                                           parts.Id,
                                           parts.Version,
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
                                           facility.SupplementalFacilities,
                                           facility.DedicatedParkingSpaces,
                                           typeOfSite,
                                           brand,
                                           exclusiveUsers,
                                           preferredUsers,
                                           serviceTypes,
                                           entrances,
                                           exits,
                                           stations,
                                           energyDistributor,
                                           mobilityServiceProviders,
                                           roamingPlatforms,
                                           parts.FacilityObjectExtension,
                                           facility.FacilityExtension,
                                           XML.Element(DatexIINS.EnergyInfrastructure + "_energyInfrastructureSiteExtension")
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

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "energyInfrastructureSite",

                   new XAttribute("id",        Id),
                   new XAttribute("version",   Version),

                   // Everything a facility object and a facility contribute, in
                   // schema order, from the classes that declare it.
                   ToXMLElements(),

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

                   Entrances.Select(entrance => entrance.ToXML(DatexIINS.EnergyInfrastructure + "entrance")),
                   Exits.    Select(exit     => exit.    ToXML(DatexIINS.EnergyInfrastructure + "exit")),

                   EnergyInfrastructureStations.Select(energyInfrastructureStation =>
                       energyInfrastructureStation.ToXML(DatexIINS.EnergyInfrastructure + "energyInfrastructureStation")),

                   EnergyDistributor?.ToXML(DatexIINS.EnergyInfrastructure + "energyDistributor"),

                   MobilityServiceProviders.Select(mobilityServiceProvider => mobilityServiceProvider.ToXML(DatexIINS.EnergyInfrastructure + "mobilityServiceProvider")),
                   RoamingPlatforms.        Select(roamingPlatform         => roamingPlatform.        ToXML(DatexIINS.EnergyInfrastructure + "roamingPlatform")),

                   EnergyInfrastructureSiteExtension is not null
                       ? EnergyInfrastructureSiteExtension.ToExtensionXML(DatexIINS.EnergyInfrastructure + "_energyInfrastructureSiteExtension")
                       : null

               );

        #endregion

    }

}
