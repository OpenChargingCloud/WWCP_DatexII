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
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A refill point that is no electric charging point: aegi:AlternativeFuelsRefillPoint, aegi:DieselRefillPoint or
    /// aegi:PetrolRefillPoint. What every refill point has is read; what its type adds (all simple values, e.g. the
    /// petrol type or the pressure) is kept as it was read and written again as it is.
    /// </summary>
    public class OtherRefillPoint(String                                Type,
                                  String                                Id,
                                  String                                Version,
                                  DeliveryUnit                          DeliveryUnit,

                                  MultilingualString?                   Name                      = null,
                                  IEnumerable<MultilingualString>?      Alias                     = null,
                                  IEnumerable<ExternalIdentifier>?      ExternalIdentifier        = null,
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
                                  IEnumerable<EnergyProduct>?           EnergyProducts            = null,
                                  IEnumerable<XElement>?                TypeElements              = null)

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

        #region Data

        /// <summary>
        /// The refill point types of the AFIR namespace other than aegi:ElectricChargingPoint.
        /// </summary>
        public static readonly IReadOnlySet<String> Types = new HashSet<String> {
                                                                "AlternativeFuelsRefillPoint",
                                                                "DieselRefillPoint",
                                                                "PetrolRefillPoint"
                                                            };

        /// <summary>
        /// The elements of the AFIR namespace every refill point has, before those its type adds.
        /// </summary>
        private static readonly HashSet<String> refillPointElements = [
                                                                          "deliveryUnit",
                                                                          "maximumDeliveryAmount",
                                                                          "minimumDeliveryAmount",
                                                                          "modelType",
                                                                          "reservation",
                                                                          "energyProduct",
                                                                          "_refillPointExtension"
                                                                      ];

        #endregion

        #region Properties

        /// <summary>
        /// The type of the refill point, e.g. "DieselRefillPoint" (the local name of its xsi:type).
        /// </summary>
        public String                 Type            { get; } = Type;

        /// <summary>
        /// The elements its type adds, in the order of the schema, e.g. aegi:petrolType and aegi:octane.
        /// </summary>
        public IEnumerable<XElement>  TypeElements    { get; } = TypeElements?.Select(element => new XElement(element)).ToArray() ?? [];

        #endregion


        #region TryParseXML(XML, out OtherRefillPoint, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a refill point that is no electric charging point.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="OtherRefillPoint">The parsed refill point.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                    XML,
                                          [NotNullWhen(true)]  out OtherRefillPoint?  OtherRefillPoint,
                                          [NotNullWhen(false)] out String?            ErrorResponse)
        {

            OtherRefillPoint = null;

            var type = XML.XSIType();

            if (type is null || !Types.Contains(type))
            {
                ErrorResponse = $"Refill points of the type '{type}' can not be parsed yet!";
                return false;
            }

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

            var parts = facility.Object;

            OtherRefillPoint = new OtherRefillPoint(
                                   type,
                                   parts.Id,
                                   parts.Version,
                                   deliveryUnit,
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
                                   parts.FacilityObjectExtension,
                                   facility.FacilityExtension,
                                   XML.Element(DatexIINS.EnergyInfrastructure + "_refillPointExtension"),
                                   energyProducts,
                                   XML.Elements().Where(element => element.Name.Namespace == DatexIINS.EnergyInfrastructure &&
                                                                   !refillPointElements.Contains(element.Name.LocalName))
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
                   new XAttribute(DatexIINS.XSI + "type",   "aegi:" + Type),
                   new XAttribute("id",                      Id),
                   new XAttribute("version",                 Version),

                   ToXMLElements(),

                   TypeElements.Select(element => new XElement(element))

               );

        #endregion

    }

}
