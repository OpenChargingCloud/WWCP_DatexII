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
    /// Technical infrastructure at a specific location that facilitates an energy refilling process
    /// being connected to at most one vehicle at a time.
    /// </summary>
    [XmlType("RefillPoint", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public abstract class RefillPoint(String                                Id,
                                      String                                Version,
                                      DeliveryUnit                          DeliveryUnit,

                                      MultilingualString?                   Name                      = null,
                                      IEnumerable<MultilingualString>?      Alias                     = null,
                                      IEnumerable<ExternalIdentifier>?       ExternalIdentifier        = null,
                                      DateTimeOffset?                       LastUpdated               = null,
                                      MultilingualString?                   Description               = null,
                                      IEnumerable<Accessibility>?           Accessibility             = null,
                                      IEnumerable<MultilingualString>?      AdditionalInformation     = null,
                                      IEnumerable<URL>?                     InformationWebsites       = null,
                                      IEnumerable<URL>?                     PhotoURLs                 = null,
                                      IEnumerable<Image>?                   Photos                    = null,
                                      AOperatingHours?                      OperatingHours            = null,
                                      ALocationReference?                   LocationReference         = null,
                                      AOrganisation?                        Owner                     = null,
                                      AOrganisation?                        Operator                  = null,
                                      AOrganisation?                        Helpdesk                  = null,
                                      IEnumerable<VehicleCharacteristics>?  ApplicableForVehicles     = null,
                                      Dimension?                            Dimension                 = null,
                                      Amenities?                            Amenities                 = null,

                                      IEnumerable<ASupplementalFacility>?   SupplementalFacilities    = null,
                                      IEnumerable<DedicatedParkingSpaces>?  DedicatedParkingSpaces    = null,

                                      Units?                                MinimumDeliveryAmount     = null,
                                      Units?                                MaximumDeliveryAmount     = null,
                                      MultilingualString?                   ModelType                 = null,
                                      ReservationType?                      Reservation               = null,

                                      XElement?                             FacilityObjectExtension   = null,
                                      XElement?                             FacilityExtension         = null,
                                      XElement?                             RefillPointExtension      = null,
                                      IEnumerable<EnergyProduct>?           EnergyProducts            = null)

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
        /// Measurement unit used for delivery and accounting of the energy provided at this refill point.
        /// </summary>
        [XmlElement("deliveryUnit",           Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public DeliveryUnit         DeliveryUnit             { get; } = DeliveryUnit;

        /// <summary>
        /// Minimum delivery amount.
        /// </summary>
        [XmlElement("minimumDeliveryAmount",  Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public Units?               MinimumDeliveryAmount    { get; } = MinimumDeliveryAmount;

        /// <summary>
        /// Maximum delivery amount.
        /// </summary>
        [XmlElement("maximumDeliveryAmount",  Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public Units?               MaximumDeliveryAmount    { get; } = MaximumDeliveryAmount;

        /// <summary>
        /// A description of the refill point model type.
        /// </summary>
        [XmlElement("modelType",              Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?  ModelType                { get; } = ModelType;

        /// <summary>
        /// Information regarding options for reservation of a time frame.
        /// </summary>
        [XmlElement("reservation",            Namespace = "http://datex2.eu/schema/3/afirFacilities")]
        public ReservationType?     Reservation              { get; } = Reservation;

        /// <summary>
        /// Optional extension element for additional refill point information.
        /// </summary>
        [XmlElement("_refillPointExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            RefillPointExtension     { get; } = RefillPointExtension;

        /// <summary>
        /// The energy products of the refill point, e.g. its electric energy with its rates (DATEX II 3.7).
        /// </summary>
        public IEnumerable<EnergyProduct>  EnergyProducts    { get; } = EnergyProducts?.Distinct() ?? [];

        #endregion


        #region TryParseXML(XML, out RefillPoint, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a refill point, by its xsi:type: an electric charging point,
        /// or another refill point of the AFIR namespace (see OtherRefillPoint).
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="RefillPoint">The parsed refill point.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                               XML,
                                          [NotNullWhen(true)]  out RefillPoint?  RefillPoint,
                                          [NotNullWhen(false)] out String?       ErrorResponse)
        {

            RefillPoint = null;

            switch (XML.XSIType())
            {

                case "ElectricChargingPoint":
                    if (ElectricChargingPoint.TryParseXML(XML, out var electricChargingPoint, out ErrorResponse)) { RefillPoint = electricChargingPoint; return true; }
                    return false;

                default:
                    if (OtherRefillPoint.TryParseXML(XML, out var otherRefillPoint, out ErrorResponse)) { RefillPoint = otherRefillPoint; return true; }
                    return false;

            }

        }

        #endregion


        #region (protected) ToXMLElements()

        /// <summary>
        /// The facility part, followed by what a refill point adds to it.
        /// </summary>
        protected override IEnumerable<Object?> ToXMLElements()
        {

            foreach (var element in base.ToXMLElements())
                yield return element;

            yield return new XElement(DatexIINS.EnergyInfrastructure + "deliveryUnit",                 DeliveryUnit.ToString());

            if (MaximumDeliveryAmount.HasValue)
                yield return new XElement(DatexIINS.EnergyInfrastructure + "maximumDeliveryAmount",    MaximumDeliveryAmount.Value.Value);

            if (MinimumDeliveryAmount.HasValue)
                yield return new XElement(DatexIINS.EnergyInfrastructure + "minimumDeliveryAmount",    MinimumDeliveryAmount.Value.Value);

            if (ModelType is not null)
                yield return ModelType.ToXML(DatexIINS.EnergyInfrastructure + "modelType");

            if (Reservation.HasValue)
                yield return new XElement(DatexIINS.EnergyInfrastructure + "reservation",              Reservation.Value.ToString());

            foreach (var energyProduct in EnergyProducts)
                yield return energyProduct.ToXML(DatexIINS.EnergyInfrastructure + "energyProduct");

            if (RefillPointExtension is not null)
                yield return RefillPointExtension.ToExtensionXML(DatexIINS.EnergyInfrastructure + "_refillPointExtension");

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
