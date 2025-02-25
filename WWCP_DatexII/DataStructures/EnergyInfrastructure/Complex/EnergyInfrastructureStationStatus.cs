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
    [XmlType("EnergyInfrastructureStationStatus", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyInfrastructureStationStatus(FacilityObjectVersionedReference          Reference,
                                                   DateTime?                                 LastUpdated                                  = null,
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
                                                   IEnumerable<ServiceType>?                 ServiceTypes                                 = null,
                                                   IEnumerable<EnergyRateUpdate>?            EnergyRateUpdates                            = null,
                                                   XElement?                                 EnergyInfrastructureStationStatusExtension   = null)

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
        /// Specifies the dynamic status information of one or more refill points.
        /// </summary>
        [XmlElement("refillPointStatus",                            Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<RefillPointStatus>  RefillPointStatus                             { get; } = RefillPointStatus?.Distinct() ?? [];

        /// <summary>
        /// The service type for the station.
        /// If no period is given, the currently available service is meant.
        /// </summary>
        [XmlElement("serviceType",                                  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<ServiceType>        ServiceTypes                                  { get; } = ServiceTypes?.     Distinct() ?? [];

        /// <summary>
        /// Updates to the energy rate applicable at the station.
        /// </summary>
        [XmlElement("energyRateUpdate",                             Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
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

            EnergyInfrastructureStationStatus  = null;
            ErrorResponse                      = null;

            #region TryParse Reference            [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.Facilities + "reference",
                                       "facility object versioned reference",
                                       FacilityObjectVersionedReference.TryParseXML,
                                       out FacilityObjectVersionedReference? reference,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion



            #region TryParse RefillPointStatus    [optional]

            if (XML.TryParseOptionalElements(DatexIINS.EnergyInfrastructure + "refillPointStatus",
                                             "energy infrastructure station status",
                                             EnergyInfrastructure.RefillPointStatus.TryParseXML,
                                             out IEnumerable<RefillPointStatus> refillPointStatus,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            EnergyInfrastructureStationStatus = new EnergyInfrastructureStationStatus(

                                                    reference,
                                                    null, //lastUpdated,
                                                    null, //openingStatus,
                                                    null, //operationStatus,
                                                    null, //regularOperatingHoursInForce,
                                                    null, //statusDescription,
                                                    null, //newOperatingHours,
                                                    null, //fault,
                                                    null,

                                                    null, //supplementalFacilityStatuses,
                                                    null,

                                                    null, //isAvailable,
                                                    refillPointStatus,
                                                    null, //serviceTypes,
                                                    null  //energyRateUpdates

                                                );

            return true;

        }

        #endregion

    }

}
