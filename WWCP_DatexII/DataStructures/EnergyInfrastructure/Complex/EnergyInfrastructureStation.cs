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

using System.Xml.Linq;
using System.Xml.Serialization;
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
    /// A collection of associated refill points (which can be of different type). An example would be a fuel dispenser that serves different types of fuel.
    /// Often the vehicle space of the station is shared between the different refill points.
    /// DATEX II 3.7: the BNetzA station identification is an external identifier of the type stationIdBNetzA,
    /// and the energy of the station are energy products.
    /// </summary>
    [XmlType("EnergyInfrastructureStation", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public class EnergyInfrastructureStation(String                                            Id,
                                             String                                            Version,
                                             Watt?                                             TotalMaximumPower                      = null,
                                             IEnumerable<AuthenticationAndIdentificationType>? AuthenticationAndIdentificationMethods = null,
                                             UInt16?                                           NumberOfRefillPoints                   = null,
                                             IEnumerable<Languages>?                           UserInterfaceLanguages                 = null,
                                             IEnumerable<Service>?                             ServiceTypes                           = null,

                                             MultilingualString?                               Name                                   = null,
                                             IEnumerable<MultilingualString>?                  Alias                                  = null,
                                             IEnumerable<ExternalIdentifier>?                   ExternalIdentifier                     = null,
                                             DateTimeOffset?                                   LastUpdated                            = null,
                                             MultilingualString?                               Description                            = null,
                                             IEnumerable<Accessibility>?                       Accessibility                          = null,
                                             IEnumerable<MultilingualString>?                  AdditionalInformation                  = null,
                                             IEnumerable<URL>?                                 InformationWebsites                    = null,
                                             IEnumerable<URL>?                                 PhotoURLs                              = null,
                                             IEnumerable<Image>?                               Photos                                 = null,
                                             AOperatingHours?                                  OperatingHours                         = null,
                                             ALocationReference?                               LocationReference                      = null,
                                             AOrganisation?                                    Owner                                  = null,
                                             AOrganisation?                                    Operator                               = null,
                                             AOrganisation?                                    Helpdesk                               = null,
                                             IEnumerable<VehicleCharacteristics>?              ApplicableForVehicles                  = null,
                                             Dimension?                                        Dimension                              = null,
                                             Amenities?                                        Amenities                              = null,

                                             IEnumerable<ASupplementalFacility>?               SupplementalFacilities                 = null,
                                             IEnumerable<DedicatedParkingSpaces>?              DedicatedParkingSpaces                 = null,

                                             Boolean?                                          RenewableSources                       = null,
                                             AOrganisation?                                    EnergyDistributor                      = null,
                                             IEnumerable<AOrganisation>?                       MobilityServiceProviders               = null,
                                             IEnumerable<AOrganisation>?                       RoamingPlatforms                       = null,
                                             Hydrogen?                                         Hydrogen                               = null,
                                             IEnumerable<RefillPoint>?                         RefillPoints                           = null,
                                             IEnumerable<EnergyProduct>?                       EnergyProducts                         = null,

                                             XElement?                                         FacilityObjectExtension                = null,
                                             XElement?                                         FacilityExtension                      = null,
                                             XElement?                                         EnergyInfrastructureStationExtension   = null)

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
        /// Ladeeinrichtungs-ID der Bundesnetzagentur: the external identifier of the type stationIdBNetzA.
        /// (See https://www.bundesnetzagentur.de/DE/Fachthemen/ElektrizitaetundGas/E-Mobilitaet/AnzeigeNEU/start.html)
        /// </summary>
        public String?                                           StationIdBNetzA

            => ExternalIdentifier.FirstOrDefault(identifier => identifier.Type == TypeOfIdentifier.StationIdBNetzA)?.Identifier;

        /// <summary>
        /// The total maximum power of the station.
        /// </summary>
        public Watt?                                             TotalMaximumPower                         { get; } = TotalMaximumPower;

        /// <summary>
        /// Information on what methods of identification and/or authentication are accepted.
        /// </summary>
        public IEnumerable<AuthenticationAndIdentificationType>  AuthenticationAndIdentificationMethods    { get; } = AuthenticationAndIdentificationMethods?.Distinct() ?? [];

        /// <summary>
        /// Information on the number of refill points at the station.
        /// </summary>
        public UInt16?                                           NumberOfRefillPoints                      { get; } = NumberOfRefillPoints;

        /// <summary>
        /// Languages in which a user interface is available.
        /// </summary>
        public IEnumerable<Languages>                            UserInterfaceLanguages                    { get; } = UserInterfaceLanguages?.               Distinct() ?? [];

        /// <summary>
        /// Whether the station is supplied by renewable sources only.
        /// </summary>
        public Boolean?                                          RenewableSources                          { get; } = RenewableSources;

        /// <summary>
        /// The distributor of the energy.
        /// </summary>
        public AOrganisation?                                    EnergyDistributor                         { get; } = EnergyDistributor;

        /// <summary>
        /// Mobility service providers offering contract-based recharging.
        /// </summary>
        public IEnumerable<AOrganisation>                        MobilityServiceProviders                  { get; } = MobilityServiceProviders?.             Distinct() ?? [];

        /// <summary>
        /// Supported roaming platforms.
        /// </summary>
        public IEnumerable<AOrganisation>                        RoamingPlatforms                          { get; } = RoamingPlatforms?.                     Distinct() ?? [];

        /// <summary>
        /// The hydrogen offered at the station.
        /// </summary>
        public Hydrogen?                                         Hydrogen                                  { get; } = Hydrogen;

        /// <summary>
        /// Specify the type of service available at the EnergyInfrastructureStation.
        /// </summary>
        public IEnumerable<Service>                              ServiceTypes                              { get; } = ServiceTypes?.                         Distinct() ?? [];

        /// <summary>
        /// Specifications of refill points at a charging station.
        /// </summary>
        public IEnumerable<RefillPoint>                          RefillPoints                              { get; } = RefillPoints?.                         Distinct() ?? [];

        /// <summary>
        /// The energy products offered at the station.
        /// </summary>
        public IEnumerable<EnergyProduct>                        EnergyProducts                            { get; } = EnergyProducts?.                       ToArray()  ?? [];

        /// <summary>
        /// The electric energy offered at the station: its energy products of that type.
        /// </summary>
        public IEnumerable<ElectricEnergy>                       ElectricEnergy

            => EnergyProducts.OfType<ElectricEnergy>();

        /// <summary>
        /// Optional extension element for additional EnergyInfrastructureStation information.
        /// </summary>
        public XElement?                                         EnergyInfrastructureStationExtension      { get; } = EnergyInfrastructureStationExtension;

        #endregion


        #region TryParseXML(XML, out EnergyInfrastructureStation, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an energy infrastructure station.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyInfrastructureStation">The parsed energy infrastructure station.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                               XML,
                                          [NotNullWhen(true)]  out EnergyInfrastructureStation?  EnergyInfrastructureStation,
                                          [NotNullWhen(false)] out String?                       ErrorResponse)
        {

            EnergyInfrastructureStation = null;

            if (!FacilityParts.TryParseXML(XML, out var facility, out ErrorResponse))
                return false;

            #region TryParse the station

            if (!XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "totalMaximumPower",                       "total maximum power",                       DatexIIXMLExtensions.TryParseWatt,             out Watt?                                             totalMaximumPower,                      out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.EnergyInfrastructure + "authenticationAndIdentificationMethods",  "authentication and identification method",  AuthenticationAndIdentificationType.TryParse,  out IEnumerable<AuthenticationAndIdentificationType>  authenticationAndIdentificationMethods, out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "numberOfRefillPoints",                    "number of refill points",                   DatexIIXMLExtensions.TryParseUInt16,           out UInt16?                                           numberOfRefillPoints,                   out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.EnergyInfrastructure + "userInterfaceLanguage",                   "user interface language",                   DatexIIXMLExtensions.TryParseLanguage,         out IEnumerable<Languages>                            userInterfaceLanguages,                 out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "renewableSources",                        "renewable sources",                         DatexIIXMLExtensions.TryParseBoolean,          out Boolean?                                          renewableSources,                       out ErrorResponse) ||
                !XML.TryParseOptionalXML   (DatexIINS.EnergyInfrastructure + "energyDistributor",                       "energy distributor",                        AOrganisation.TryParseXML,                     out AOrganisation?                                    energyDistributor,                      out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "mobilityServiceProvider",                 "mobility service provider",                 AOrganisation.TryParseXML,                     out IEnumerable<AOrganisation>                        mobilityServiceProviders,               out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "roamingPlatform",                         "roaming platform",                          AOrganisation.TryParseXML,                     out IEnumerable<AOrganisation>                        roamingPlatforms,                       out ErrorResponse) ||
                !XML.TryParseOptionalXML   (DatexIINS.EnergyInfrastructure + "hydrogen",                                "hydrogen",                                  EnergyInfrastructure.Hydrogen.TryParseXML,     out Hydrogen?                                         hydrogen,                               out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "serviceType",                             "service type",                              Service.TryParseXML,                           out IEnumerable<Service>                              serviceTypes,                           out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "refillPoint",                             "refill point",                              RefillPoint.TryParseXML,                       out IEnumerable<RefillPoint>                          refillPoints,                           out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "energyProduct",                           "energy product",                            EnergyProduct.TryParseXML,                     out IEnumerable<EnergyProduct>                        energyProducts,                         out ErrorResponse))
            {
                return false;
            }

            #endregion

            var parts = facility.Object;

            EnergyInfrastructureStation = new EnergyInfrastructureStation(
                                              parts.Id,
                                              parts.Version,
                                              totalMaximumPower,
                                              authenticationAndIdentificationMethods,
                                              numberOfRefillPoints,
                                              userInterfaceLanguages,
                                              serviceTypes,
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
                                              renewableSources,
                                              energyDistributor,
                                              mobilityServiceProviders,
                                              roamingPlatforms,
                                              hydrogen,
                                              refillPoints,
                                              energyProducts,
                                              parts.FacilityObjectExtension,
                                              facility.FacilityExtension,
                                              XML.Element(DatexIINS.EnergyInfrastructure + "_energyInfrastructureStationExtension")
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

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "energyInfrastructureStation",

                   new XAttribute("id",        Id),
                   new XAttribute("version",   Version),

                   ToXMLElements(),

                   TotalMaximumPower.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "totalMaximumPower",     TotalMaximumPower.Value.Value)
                       : null,

                   AuthenticationAndIdentificationMethods.Select(method => new XElement(DatexIINS.EnergyInfrastructure + "authenticationAndIdentificationMethods", method.ToString())),

                   NumberOfRefillPoints.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "numberOfRefillPoints",  NumberOfRefillPoints.Value)
                       : null,

                   UserInterfaceLanguages.Select(language => new XElement(DatexIINS.EnergyInfrastructure + "userInterfaceLanguage", language.AsText())),

                   RenewableSources.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "renewableSources",      RenewableSources.Value ? "true" : "false")
                       : null,

                   EnergyDistributor?.ToXML(DatexIINS.EnergyInfrastructure + "energyDistributor"),

                   MobilityServiceProviders.Select(mobilityServiceProvider => mobilityServiceProvider.ToXML(DatexIINS.EnergyInfrastructure + "mobilityServiceProvider")),
                   RoamingPlatforms.        Select(roamingPlatform         => roamingPlatform.        ToXML(DatexIINS.EnergyInfrastructure + "roamingPlatform")),

                   Hydrogen?.ToXML(DatexIINS.EnergyInfrastructure + "hydrogen"),

                   ServiceTypes.  Select(serviceType   => serviceType.  ToXML(DatexIINS.EnergyInfrastructure + "serviceType")),
                   RefillPoints.  Select(refillPoint   => refillPoint.  ToXML(DatexIINS.EnergyInfrastructure + "refillPoint")),
                   EnergyProducts.Select(energyProduct => energyProduct.ToXML(DatexIINS.EnergyInfrastructure + "energyProduct")),

                   EnergyInfrastructureStationExtension is not null
                       ? new XElement(DatexIINS.EnergyInfrastructure + "_energyInfrastructureStationExtension", EnergyInfrastructureStationExtension)
                       : null

               );

        #endregion

    }

}
