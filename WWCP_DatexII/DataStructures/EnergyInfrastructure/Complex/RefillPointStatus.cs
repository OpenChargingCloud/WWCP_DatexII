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

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Dynamic information on the status of the refill point.
    /// </summary>
    [XmlType("RefillPointStatus", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class RefillPointStatus(RefillPointStatusEnum                   Status,
                                   FacilityObjectVersionedReference        Reference,

                                   Double?                                 UnitsInStock                    = null,
                                   IEnumerable<EnergyRateUpdate>?          EnergyRateUpdates               = null,
                                   TimeSpan?                               WaitingTime                     = null,
                                   IEnumerable<PlannedRefillPointStatus>?  PlannedRefillPointStatus        = null,
                                   XElement?                               RefillPointStatusExtension      = null,

                                   DateTimeOffset?                         LastUpdated                     = null,
                                   OpeningStatus?                          OpeningStatus                   = null,
                                   OperationStatus?                        OperationStatus                 = null,
                                   Boolean?                                RegularOperatingHoursInForce    = null,
                                   MultilingualString?                     StatusDescription               = null,
                                   AOperatingHours?                        NewOperatingHours               = null,
                                   Fault?                                  Fault                           = null,
                                   XElement?                               FacilityObjectStatusExtension   = null)

        : FacilityStatus(Reference,
                         LastUpdated,
                         OpeningStatus,
                         OperationStatus,
                         RegularOperatingHoursInForce,
                         StatusDescription,
                         NewOperatingHours,
                         Fault,
                         FacilityObjectStatusExtension)

    {

        #region Properties

        /// <summary>
        /// Status of the refill point.
        /// </summary>
        [XmlElement("status",                       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public RefillPointStatusEnum                  Status                        { get; } = Status;

        /// <summary>
        /// Amount of delivery units still in stock (with delivery units as defined in the static information model).
        /// </summary>
        [XmlElement("unitsInStock",                 Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Double?                                UnitsInStock                  { get; } = UnitsInStock;

        /// <summary>
        /// Updates to the energy rate applicable at this refill point.
        /// </summary>
        [XmlElement("energyRateUpdate",             Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyRateUpdate>          EnergyRateUpdates             { get; } = EnergyRateUpdates?.         Distinct() ?? [];

        /// <summary>
        /// Estimated waiting time for customers without reservation.
        /// </summary>
        [XmlElement("waitingTime",                  Namespace = "http://datex2.eu/schema/3/facilities")]
        public TimeSpan?                              WaitingTime                   { get; } = WaitingTime;

        /// <summary>
        /// Planned status information for the refill point (e.g. reservations).
        /// </summary>
        [XmlElement("plannedRefillPointStatus",     Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<PlannedRefillPointStatus>  PlannedRefillPointStatus      { get; } = PlannedRefillPointStatus?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional refill point status information.
        /// </summary>
        [XmlElement("_refillPointStatusExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                              RefillPointStatusExtension    { get; } = RefillPointStatusExtension;

        #endregion


        #region TryParseXML(XML, out RefillPointStatus, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a RefillPointStatus.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="RefillPointStatus">The parsed RefillPointStatus.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                     XML,
                                          [NotNullWhen(true)]  out RefillPointStatus?  RefillPointStatus,
                                          [NotNullWhen(false)] out String?             ErrorResponse)
        {

            RefillPointStatus  = null;
            ErrorResponse      = null;

            var concreteTypeAttribute        = XML.Attribute(DatexIINS.XSI + "type");
            var concreteTypeNamespace        = concreteTypeAttribute?.Name.NamespaceName;
            var concerteTypeName             = concreteTypeAttribute?.Value.Split(':');

            var concreteTypeNamespacePrefix  = concerteTypeName?.Length == 2 ? XML.GetNamespaceOfPrefix(concerteTypeName?[0] ?? "") : DatexIINS.EnergyInfrastructure.NamespaceName;
            var concreteTypeNamespaceSuffix  = concerteTypeName?.Length == 2 ? concerteTypeName?[1] ?? "" : concerteTypeName?[0] ?? "";


            if (concreteTypeNamespacePrefix?.NamespaceName == DatexIINS.EnergyInfrastructure.NamespaceName)
            {

                switch (concreteTypeNamespaceSuffix)
                {

                    case "ElectricChargingPointStatus":
                        if (ElectricChargingPointStatus.TryParseXML(XML,
                                                                    out var electricChargingPointStatus,
                                                                    out ErrorResponse))
                        {
                            RefillPointStatus = electricChargingPointStatus;
                            return true;
                        }
                        break;

                }

            }

            return RefillPointStatus is not null;

        }

        #endregion

    }

}
