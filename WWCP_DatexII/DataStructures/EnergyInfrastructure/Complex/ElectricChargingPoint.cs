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
    /// A refill point for electric vehicles: an EVSE (DATEX II 3.7: aegi:ElectricChargingPoint).
    /// Its EVSE identification is an external identifier of the type evseId, its energy and rates are energy products.
    /// </summary>
    [XmlType("ElectricChargingPoint", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public class ElectricChargingPoint(String                                    Id,
                                       String                                    Version,
                                       DeliveryUnit                              DeliveryUnit,
                                       CurrentType                               CurrentType,
                                       MultilingualString?                       Name                             = null,
                                       IEnumerable<MultilingualString>?          Alias                            = null,
                                       IEnumerable<ExternalIdentifier>?          ExternalIdentifier               = null,
                                       DateTimeOffset?                           LastUpdated                      = null,
                                       MultilingualString?                       Description                      = null,
                                       IEnumerable<Accessibility>?               Accessibility                    = null,
                                       IEnumerable<MultilingualString>?          AdditionalInformation            = null,
                                       IEnumerable<URL>?                         InformationWebsites              = null,
                                       IEnumerable<URL>?                         PhotoURLs                        = null,
                                       IEnumerable<Image>?                       Photos                           = null,
                                       AOperatingHours?                          OperatingHours                   = null,
                                       ALocationReference?                       LocationReference                = null,
                                       AOrganisation?                            Owner                            = null,
                                       AOrganisation?                            Operator                         = null,
                                       AOrganisation?                            Helpdesk                         = null,
                                       IEnumerable<VehicleCharacteristics>?      ApplicableForVehicles            = null,
                                       Dimension?                                Dimension                        = null,
                                       Amenities?                                Amenities                        = null,
                                       IEnumerable<ASupplementalFacility>?       SupplementalFacilities           = null,
                                       IEnumerable<DedicatedParkingSpaces>?      DedicatedParkingSpaces           = null,
                                       Units?                                    MinimumDeliveryAmount            = null,
                                       Units?                                    MaximumDeliveryAmount            = null,
                                       MultilingualString?                       ModelType                        = null,
                                       ReservationType?                          Reservation                      = null,
                                       IEnumerable<EnergyProduct>?               EnergyProducts                   = null,
                                       IEnumerable<ChargingPointUsage>?          UsageType                        = null,
                                       IEnumerable<VehicleToGridCommunication>?  VehicleToGridCommunicationType   = null,
                                       Byte?                                     NumberOfConnectors               = null,
                                       IEnumerable<Volt>?                        AvailableVoltage                 = null,
                                       IEnumerable<Watt>?                        AvailableChargingPower           = null,
                                       ChargingMode?                             ChargingMode                     = null,
                                       IEnumerable<SmartRechargingService>?      SmartRechargingServices          = null,
                                       IEnumerable<MultilingualString>?          OtherSmartRechargingServices     = null,
                                       IEnumerable<Connector>?                   Connector                        = null,
                                       XElement?                                 FacilityObjectExtension          = null,
                                       XElement?                                 FacilityExtension                = null,
                                       XElement?                                 RefillPointExtension             = null,
                                       XElement?                                 ElectricChargingPointExtension   = null)

        : RefillPoint(Id,
                      Version,
                      DeliveryUnit,
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
                      MinimumDeliveryAmount,
                      MaximumDeliveryAmount,
                      ModelType,
                      Reservation,
                      FacilityObjectExtension,
                      FacilityExtension,
                      RefillPointExtension,
                      EnergyProducts)

    {

        #region Properties

        /// <summary>
        /// The type of current: AC or DC.
        /// </summary>
        public CurrentType                              CurrentType                       { get; } = CurrentType;

        /// <summary>
        /// Usage type of the electric charging point.
        /// </summary>
        public IEnumerable<ChargingPointUsage>          UsageType                         { get; } = UsageType?.                     Distinct() ?? [];

        /// <summary>
        /// Type of vehicle to grid communication used.
        /// </summary>
        public IEnumerable<VehicleToGridCommunication>  VehicleToGridCommunicationType    { get; } = VehicleToGridCommunicationType?.Distinct() ?? [];

        /// <summary>
        /// Information on the number of connectors at this charging point.
        /// </summary>
        public Byte?                                    NumberOfConnectors                { get; } = NumberOfConnectors;

        /// <summary>
        /// Possible degrees of voltage.
        /// </summary>
        public IEnumerable<Volt>                        AvailableVoltage                  { get; } = AvailableVoltage?.              ToArray()  ?? [];

        /// <summary>
        /// Possible degrees of charging power in Watts; the first is the maximum (Mobilithek profile).
        /// </summary>
        public IEnumerable<Watt>                        AvailableChargingPower            { get; } = AvailableChargingPower?.        ToArray()  ?? [];

        /// <summary>
        /// The charging mode of the charging point (moved here from the connector in DATEX II 3.7).
        /// </summary>
        public ChargingMode?                            ChargingMode                      { get; } = ChargingMode;

        /// <summary>
        /// Smart recharging services that are available.
        /// </summary>
        public IEnumerable<SmartRechargingService>      SmartRechargingServices           { get; } = SmartRechargingServices?.       Distinct() ?? [];

        /// <summary>
        /// Other smart recharging services that are available.
        /// </summary>
        public IEnumerable<MultilingualString>          OtherSmartRechargingServices      { get; } = OtherSmartRechargingServices?.  Distinct() ?? [];

        /// <summary>
        /// Specify the connector(s).
        /// </summary>
        public IEnumerable<Connector>                   Connector                         { get; } = Connector?.                     ToArray()  ?? [];

        /// <summary>
        /// The EVSE identification: the external identifier of the type evseId.
        /// </summary>
        public String?                                  EVSEId

            => ExternalIdentifier.FirstOrDefault(identifier => identifier.Type == TypeOfIdentifier.EVSEId)?.Identifier;

        /// <summary>
        /// The electric energy of the charging point: its energy products of that type.
        /// </summary>
        public IEnumerable<ElectricEnergy>              ElectricEnergy

            => EnergyProducts.OfType<ElectricEnergy>();

        /// <summary>
        /// Optional extension element for additional electric charging point information.
        /// </summary>
        public XElement?                                ElectricChargingPointExtension    { get; } = ElectricChargingPointExtension;

        #endregion


        #region TryParseXML(XML, out ElectricChargingPoint, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an electric charging point.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ElectricChargingPoint">The parsed electric charging point.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                         XML,
                                          [NotNullWhen(true)]  out ElectricChargingPoint?  ElectricChargingPoint,
                                          [NotNullWhen(false)] out String?                 ErrorResponse)
        {

            ElectricChargingPoint = null;

            if (!FacilityParts.TryParseXML(XML, out var facility, out ErrorResponse))
                return false;

            #region TryParse the refill point                   [deliveryUnit mandatory]

            if (!XML.TryParseMandatoryValue(DatexIINS.EnergyInfrastructure + "deliveryUnit",           "delivery unit",            EnergyInfrastructure.DeliveryUnit.TryParse,  out DeliveryUnit      deliveryUnit,          out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "maximumDeliveryAmount",  "maximum delivery amount",  Units.TryParse,                               out Units?            maximumDeliveryAmount, out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "minimumDeliveryAmount",  "minimum delivery amount",  Units.TryParse,                               out Units?            minimumDeliveryAmount, out ErrorResponse) ||
                !XML.TryParseOptionalXML   (DatexIINS.EnergyInfrastructure + "modelType",              "model type",               MultilingualString.TryParseXML,               out MultilingualString? modelType,           out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "reservation",            "reservation",              ReservationType.TryParse,                     out ReservationType?  reservation,           out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "energyProduct",          "energy product",           EnergyProduct.TryParseXML,                    out IEnumerable<EnergyProduct> energyProducts, out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse the electric charging point        [currentType mandatory]

            if (!XML.TryParseMandatoryValue(DatexIINS.EnergyInfrastructure + "currentType",                     "current type",                       EnergyInfrastructure.CurrentType.TryParse,   out CurrentType                              currentType,                    out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.EnergyInfrastructure + "usageType",                       "usage type",                         ChargingPointUsage.TryParse,          out IEnumerable<ChargingPointUsage>          usageType,                      out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.EnergyInfrastructure + "vehicleToGridCommunicationType",  "vehicle to grid communication type", VehicleToGridCommunication.TryParse,  out IEnumerable<VehicleToGridCommunication>  vehicleToGridCommunicationType, out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "numberOfConnectors",              "number of connectors",               Byte.TryParse,                        out Byte?                                    numberOfConnectors,             out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.EnergyInfrastructure + "availableVoltage",                "available voltage",                  DatexIIXMLExtensions.TryParseVolt,    out IEnumerable<Volt>                        availableVoltage,               out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.EnergyInfrastructure + "availableChargingPower",          "available charging power",           DatexIIXMLExtensions.TryParseWatt,    out IEnumerable<Watt>                        availableChargingPower,         out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "chargingMode",                    "charging mode",                      EnergyInfrastructure.ChargingMode.TryParse,  out ChargingMode?                     chargingMode,                   out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.EnergyInfrastructure + "smartRechargingServices",         "smart recharging services",          SmartRechargingService.TryParse,      out IEnumerable<SmartRechargingService>      smartRechargingServices,        out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "otherSmartRechargingServices",    "other smart recharging services",    MultilingualString.TryParseXML,       out IEnumerable<MultilingualString>          otherSmartRechargingServices,   out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "connector",                       "connector",                          EnergyInfrastructure.Connector.TryParseXML, out IEnumerable<Connector>             connectors,                     out ErrorResponse))
            {
                return false;
            }

            #endregion

            var parts = facility.Object;

            ElectricChargingPoint = new ElectricChargingPoint(
                                        parts.Id,
                                        parts.Version,
                                        deliveryUnit,
                                        currentType,
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
                                        minimumDeliveryAmount,
                                        maximumDeliveryAmount,
                                        modelType,
                                        reservation,
                                        energyProducts,
                                        usageType,
                                        vehicleToGridCommunicationType,
                                        numberOfConnectors,
                                        availableVoltage,
                                        availableChargingPower,
                                        chargingMode,
                                        smartRechargingServices,
                                        otherSmartRechargingServices,
                                        connectors,
                                        parts.FacilityObjectExtension,
                                        facility.FacilityExtension,
                                        XML.Element(DatexIINS.EnergyInfrastructure + "_refillPointExtension"),
                                        XML.Element(DatexIINS.EnergyInfrastructure + "_electricChargingPointExtension")
                                    );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public override XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "refillPoint",

                   // The schema declares refillPoint with the abstract type.
                   new XAttribute(DatexIINS.XSI + "type",   "aegi:ElectricChargingPoint"),
                   new XAttribute("id",                      Id),
                   new XAttribute("version",                 Version),

                   ToXMLElements(),

                   new XElement(DatexIINS.EnergyInfrastructure + "currentType",                       CurrentType.ToString()),
                   UsageType.                     Select(usageType     => new XElement(DatexIINS.EnergyInfrastructure + "usageType",                      usageType.    ToString())),
                   VehicleToGridCommunicationType.Select(communication => new XElement(DatexIINS.EnergyInfrastructure + "vehicleToGridCommunicationType", communication.ToString())),

                   NumberOfConnectors.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "numberOfConnectors",           NumberOfConnectors.Value)
                       : null,

                   AvailableVoltage.      Select(availableVoltage       => new XElement(DatexIINS.EnergyInfrastructure + "availableVoltage",       availableVoltage.      Value)),
                   AvailableChargingPower.Select(availableChargingPower => new XElement(DatexIINS.EnergyInfrastructure + "availableChargingPower", availableChargingPower.Value)),

                   ChargingMode.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "chargingMode",                 ChargingMode.Value.ToString())
                       : null,

                   SmartRechargingServices.     Select(smartRechargingService => new XElement(DatexIINS.EnergyInfrastructure + "smartRechargingServices", smartRechargingService.ToString())),
                   OtherSmartRechargingServices.Select(other                  => other.ToXML(DatexIINS.EnergyInfrastructure + "otherSmartRechargingServices")),

                   Connector.Select(connector => connector.ToXML(DatexIINS.EnergyInfrastructure + "connector")),

                   ElectricChargingPointExtension is not null
                       ? ElectricChargingPointExtension.ToExtensionXML(DatexIINS.EnergyInfrastructure + "_electricChargingPointExtension")
                       : null

               );

        #endregion

    }

}
