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

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Dynamic information on the status of the station.
    /// </summary>
    [XmlType("EnergyInfrastructureStationStatus", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public class EnergyInfrastructureStationStatus(FacilityObjectVersionedReference          Reference,
                                                   DateTimeOffset?                           LastUpdated                                  = null,
                                                   OpeningStatus?                            OpeningStatus                                = null,
                                                   OperationStatus?                          OperationStatus                              = null,
                                                   Boolean?                                  RegularOperatingHoursInForce                 = null,
                                                   MultilingualString?                       StatusDescription                            = null,
                                                   AOperatingHours?                          NewOperatingHours                            = null,
                                                   Fault?                                    Fault                                        = null,
                                                   XElement?                                 FacilityObjectStatusExtension                = null,

                                                   IEnumerable<SupplementalFacilityStatus>?  SupplementalFacilityStatus                   = null,
                                                   XElement?                                 FacilityStatusExtension                      = null,

                                                   Boolean?                                  IsAvailable                                  = null,
                                                   IEnumerable<RefillPointStatus>?           RefillPointStatus                            = null,
                                                   IEnumerable<Service>?                     Services                                     = null,
                                                   IEnumerable<EnergyRateUpdate>?            EnergyRateUpdates                            = null,
                                                   XElement?                                 EnergyInfrastructureStationStatusExtension   = null,
                                                   Boolean?                                  LimitedHydrogenAmount                        = null)

        : FacilityStatus(Reference,
                         LastUpdated,
                         OpeningStatus,
                         OperationStatus,
                         RegularOperatingHoursInForce,
                         StatusDescription,
                         NewOperatingHours,
                         Fault,
                         FacilityObjectStatusExtension,

                         SupplementalFacilityStatus,
                         FacilityStatusExtension)

    {

        #region Properties

        /// <summary>
        /// Information whether the specific station is available or not.
        /// It might be unavailable, for example, because of a fault, damage, or maintenance.
        /// It does not inform if the corresponding refill points are currently occupied or not.
        /// </summary>
        [XmlElement("isAvailable",                                  Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?                        IsAvailable                                   { get; } = IsAvailable;

        /// <summary>
        /// Whether only a limited amount of hydrogen is available (DATEX II 3.7).
        /// </summary>
        public Boolean?                        LimitedHydrogenAmount                         { get; } = LimitedHydrogenAmount;

        /// <summary>
        /// Specifies the dynamic status information of one or more refill points.
        /// </summary>
        [XmlElement("refillPointStatus",                            Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<RefillPointStatus>  RefillPointStatus                             { get; } = RefillPointStatus?.Distinct() ?? [];

        /// <summary>
        /// The service type for the station.
        /// If no period is given, the currently available service is meant.
        /// </summary>
        [XmlElement("serviceType",                                  Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<Service>            Services                                      { get; } = Services?.         Distinct() ?? [];

        /// <summary>
        /// Updates to the energy rate applicable at the station.
        /// </summary>
        [XmlElement("energyRateUpdate",                             Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<EnergyRateUpdate>   EnergyRateUpdates                             { get; } = EnergyRateUpdates?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional station status information.
        /// </summary>
        [XmlElement("_energyInfrastructureStationStatusExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                       EnergyInfrastructureStationStatusExtension    { get; } = EnergyInfrastructureStationStatusExtension;

        #endregion


        #region TryParseXML(XML, out EnergyInfrastructureStationStatus, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an EnergyInfrastructureStationStatus.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyInfrastructureStationStatus">The parsed EnergyInfrastructureStationStatus.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                                     XML,
                                          [NotNullWhen(true)]  out EnergyInfrastructureStationStatus?  EnergyInfrastructureStationStatus,
                                          [NotNullWhen(false)] out String?                             ErrorResponse)
        {

            EnergyInfrastructureStationStatus = null;

            if (!FacilityStatusParts.TryParseXML(XML, out var facilityStatus, out ErrorResponse))
                return false;

            if (!XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "isAvailable",        "is available",         DatexIIXMLExtensions.TryParseBoolean,                 out Boolean?                        isAvailable,       out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "limitedHydrogenAmount", "limited hydrogen amount", DatexIIXMLExtensions.TryParseBoolean,         out Boolean?                        limitedHydrogenAmount, out ErrorResponse) ||
                !XML.TryParseOptionalXMLs (DatexIINS.EnergyInfrastructure + "refillPointStatus",  "refill point status",  EnergyInfrastructure.RefillPointStatus.TryParseXML,   out IEnumerable<RefillPointStatus>  refillPointStatus, out ErrorResponse) ||
                !XML.TryParseOptionalXMLs (DatexIINS.EnergyInfrastructure + "serviceType",        "service type",         Service.TryParseXML,                                  out IEnumerable<Service>            services,          out ErrorResponse) ||
                !XML.TryParseOptionalXMLs (DatexIINS.EnergyInfrastructure + "energyRateUpdate",   "energy rate update",   EnergyRateUpdate.TryParseXML,                         out IEnumerable<EnergyRateUpdate>   energyRateUpdates, out ErrorResponse))
            {
                return false;
            }

            var parts = facilityStatus.Object;

            EnergyInfrastructureStationStatus = new EnergyInfrastructureStationStatus(
                                                    parts.Reference,
                                                    parts.LastUpdated,
                                                    parts.OpeningStatus,
                                                    parts.OperationStatus,
                                                    parts.RegularOperatingHoursInForce,
                                                    parts.StatusDescription,
                                                    parts.NewOperatingHours,
                                                    parts.Fault,
                                                    parts.FacilityObjectStatusExtension,
                                                    facilityStatus.SupplementalFacilityStatus,
                                                    facilityStatus.FacilityStatusExtension,
                                                    isAvailable,
                                                    refillPointStatus,
                                                    services,
                                                    energyRateUpdates,
                                                    XML.Element(DatexIINS.EnergyInfrastructure + "_energyInfrastructureStationStatusExtension"),
                                                    limitedHydrogenAmount
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

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "energyInfrastructureStationStatus",

                   ToXMLElements(),

                   IsAvailable.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "isAvailable",   IsAvailable.Value ? "true" : "false")
                       : null,

                   LimitedHydrogenAmount.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "limitedHydrogenAmount", LimitedHydrogenAmount.Value ? "true" : "false")
                       : null,

                   RefillPointStatus.Select(refillPointStatus => refillPointStatus.ToXML(DatexIINS.EnergyInfrastructure + "refillPointStatus")),
                   Services.         Select(service           => service.          ToXML(DatexIINS.EnergyInfrastructure + "serviceType")),
                   EnergyRateUpdates.Select(energyRateUpdate  => energyRateUpdate. ToXML(DatexIINS.EnergyInfrastructure + "energyRateUpdate")),

                   EnergyInfrastructureStationStatusExtension is not null
                       ? EnergyInfrastructureStationStatusExtension.ToExtensionXML(DatexIINS.EnergyInfrastructure + "_energyInfrastructureStationStatusExtension")
                       : null

               );

        #endregion

    }

}
