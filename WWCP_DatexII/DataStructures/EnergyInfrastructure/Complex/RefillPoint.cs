﻿/*
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
    [XmlType("RefillPoint", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public abstract class RefillPoint(String                                Id,
                                      String                                Version,
                                      DeliveryUnit                          DeliveryUnit,

                                      MultilingualString?                   Name                      = null,
                                      IEnumerable<MultilingualString>?      Alias                     = null,
                                      String?                               ExternalIdentifier        = null,
                                      DateTime?                             LastUpdated               = null,
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
                                      XElement?                             RefillPointExtension      = null)

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
        [XmlElement("deliveryUnit",           Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public DeliveryUnit         DeliveryUnit             { get; } = DeliveryUnit;

        /// <summary>
        /// Minimum delivery amount.
        /// </summary>
        [XmlElement("minimumDeliveryAmount",  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Units?               MinimumDeliveryAmount    { get; } = MinimumDeliveryAmount;

        /// <summary>
        /// Maximum delivery amount.
        /// </summary>
        [XmlElement("maximumDeliveryAmount",  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Units?               MaximumDeliveryAmount    { get; } = MaximumDeliveryAmount;

        /// <summary>
        /// A description of the refill point model type.
        /// </summary>
        [XmlElement("modelType",              Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?  ModelType                { get; } = ModelType;

        /// <summary>
        /// Information regarding options for reservation of a time frame.
        /// </summary>
        [XmlElement("reservation",            Namespace = "http://datex2.eu/schema/3/facilities")]
        public ReservationType?     Reservation              { get; } = Reservation;

        /// <summary>
        /// Optional extension element for additional refill point information.
        /// </summary>
        [XmlElement("_refillPointExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            RefillPointExtension     { get; } = RefillPointExtension;

        #endregion

    }

}
