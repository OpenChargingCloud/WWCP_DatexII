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
    /// Dynamic information on the status of the energy supplying site.
    /// </summary>
    [XmlType("EnergyInfrastructureSiteStatus", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyInfrastructureSiteStatus(FacilityObjectVersionedReference                 Reference,
                                                DateTime?                                        LastUpdated                               = null,
                                                OpeningStatus?                                   OpeningStatus                             = null,
                                                OperationStatus?                                 OperationStatus                           = null,
                                                Boolean?                                         RegularOperatingHoursInForce              = null,
                                                MultilingualString?                              StatusDescription                         = null,
                                                AOperatingHours?                                 NewOperatingHours                         = null,
                                                Fault?                                           Fault                                     = null,
                                                XElement?                                        FacilityObjectStatusExtension             = null,

                                                IEnumerable<SupplementalFacilityStatus>?         SupplementalFacilityStatuses              = null,
                                                XElement?                                        FacilityStatusExtension                   = null,

                                                UInt16?                                          AvailableCarParkingPlaces                 = null,
                                                UInt16?                                          AvailableTruckParkingPlaces               = null,
                                                IEnumerable<EnergyInfrastructureStationStatus>?  EnergyInfrastructureStationStatus         = null,
                                                IEnumerable<ServiceType>?                        ServiceTypes                              = null,
                                                XElement?                                        EnergyInfrastructureSiteStatusExtension   = null)

        : FacilityStatus(Reference,
                         LastUpdated,
                         OpeningStatus,
                         OperationStatus,
                         RegularOperatingHoursInForce,
                         StatusDescription,
                         NewOperatingHours,
                         Fault,
                         FacilityObjectStatusExtension,

                         SupplementalFacilityStatuses,
                         FacilityStatusExtension)

    {

        #region Properties

        /// <summary>
        /// Parking places available for cars.
        /// </summary>
        [XmlElement("availableCarParkingPlaces",          Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16?                                         AvailableCarParkingPlaces                  { get; } = AvailableCarParkingPlaces;

        /// <summary>
        /// Parking places available for trucks.
        /// </summary>
        [XmlElement("availableTruckParkingPlaces",        Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16?                                         AvailableTruckParkingPlaces                { get; } = AvailableTruckParkingPlaces;

        /// <summary>
        /// Specify the status of a charging station with dynamic information.
        /// </summary>
        [XmlElement("energyInfrastructureStationStatus",  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyInfrastructureStationStatus>  EnergyInfrastructureStationStatus          { get; } = EnergyInfrastructureStationStatus?.Distinct() ?? [];

        /// <summary>
        /// The service type for the site. If no period is given, the currently available service is meant.
        /// </summary>
        [XmlElement("serviceType",                        Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<ServiceType>                        ServiceTypes                               { get; } = ServiceTypes?.                     Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional site status information.
        /// </summary>
        [XmlElement("_energyInfrastructureSiteStatusExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                       EnergyInfrastructureSiteStatusExtension    { get; } = EnergyInfrastructureSiteStatusExtension;

        #endregion


        #region TryParseXML(XML, out EnergyInfrastructureSiteStatus, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an EnergyInfrastructureSiteStatus.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyInfrastructureSiteStatus">The parsed EnergyInfrastructureSiteStatus.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                                  XML,
                                          [NotNullWhen(true)]  out EnergyInfrastructureSiteStatus?  EnergyInfrastructureSiteStatus,
                                          [NotNullWhen(false)] out String?                          ErrorResponse)
        {

            EnergyInfrastructureSiteStatus  = null;
            ErrorResponse                   = null;

            #region TryParse Reference                    [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.Facilities + "reference",
                                       "facility object versioned reference",
                                       FacilityObjectVersionedReference.TryParseXML,
                                       out FacilityObjectVersionedReference? reference,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion




            #region TryParse EnergyInfrastructureStationStatus    [optional]

            if (XML.TryParseOptionalElements(DatexIINS.EnergyInfrastructure + "energyInfrastructureStationStatus",
                                             "energy infrastructure station status",
                                             EnergyInfrastructure.EnergyInfrastructureStationStatus.TryParseXML,
                                             out IEnumerable<EnergyInfrastructureStationStatus> energyInfrastructureStationStatuses,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            EnergyInfrastructureSiteStatus = new EnergyInfrastructureSiteStatus(

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

                                                 null, //availableCarParkingPlaces,
                                                 null, //availableTruckParkingPlaces,
                                                 energyInfrastructureStationStatuses,
                                                 null, //serviceTypes,
                                                 null  //energyInfrastructureSiteStatusExtension

                                             );

            return true;

        }

        #endregion


    }

}
