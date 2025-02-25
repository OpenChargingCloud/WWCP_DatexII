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
    /// Dynamic information on the status of the charging point.
    /// </summary>
    [XmlType("ElectricChargingPointStatus", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class ElectricChargingPointStatus(RefillPointStatusEnum                   Status,
                                             FacilityObjectVersionedReference        Reference,

                                             Double?                                 UnitsInStock                           = null,
                                             IEnumerable<EnergyRateUpdate>?          EnergyRateUpdates                      = null,
                                             TimeSpan?                               WaitingTime                            = null,
                                             IEnumerable<PlannedRefillPointStatus>?  PlannedRefillPointStatus               = null,
                                             XElement?                               RefillPointStatusExtension             = null,

                                             DateTimeOffset?                         LastUpdated                            = null,
                                             OpeningStatus?                          OpeningStatus                          = null,
                                             OperationStatus?                        OperationStatus                        = null,
                                             Boolean?                                RegularOperatingHoursInForce           = null,
                                             MultilingualString?                     StatusDescription                      = null,
                                             AOperatingHours?                        NewOperatingHours                      = null,
                                             Fault?                                  Fault                                  = null,
                                             XElement?                               FacilityObjectStatusExtension          = null,

                                             TimeSpan?                               RemainingChargingTime                  = null,
                                             Volt?                                   CurrentVoltage                         = null,
                                             Watt?                                   CurrentChargingPower                   = null,
                                             IEnumerable<DateTimeOffset>?            NextAvailableChargingSlots             = null,
                                             XElement?                               ElectricChargingPointStatusExtension   = null)

        : RefillPointStatus(Status,
                            Reference,

                            UnitsInStock,
                            EnergyRateUpdates,
                            WaitingTime,
                            PlannedRefillPointStatus,
                            RefillPointStatusExtension,

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
        /// If known, the approximate remaining charging time (in seconds) for the current vehicle on this refill point.
        /// </summary>
        [XmlElement("remainingChargingTime",                  Namespace = "http://datex2.eu/schema/3/common")]
        public TimeSpan?                    RemainingChargingTime                   { get; } = RemainingChargingTime;

        /// <summary>
        /// The current used voltage.
        /// </summary>
        [XmlElement("currentVoltage",                         Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Volt?                        CurrentVoltage                          { get; } = CurrentVoltage;

        /// <summary>
        /// The current charging power in Watts.
        /// </summary>
        [XmlElement("currentChargingPower",                   Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Watt?                        CurrentChargingPower                    { get; } = CurrentChargingPower;

        /// <summary>
        /// One or more points of time in the future at which the charging point will be available (not occupied and not reserved).
        /// </summary>
        [XmlElement("nextAvailableChargingSlots",             Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<DateTimeOffset>  NextAvailableChargingSlots              { get; } = NextAvailableChargingSlots?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional electric charging point status information.
        /// </summary>
        [XmlElement("_electricChargingPointStatusExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                    ElectricChargingPointStatusExtension    { get; } = ElectricChargingPointStatusExtension;

        #endregion


        #region TryParseXML(XML, out ElectricChargingPointStatus, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an ElectricChargingPointStatus.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ElectricChargingPointStatus">The parsed ElectricChargingPointStatus.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                               XML,
                                          [NotNullWhen(true)]  out ElectricChargingPointStatus?  ElectricChargingPointStatus,
                                          [NotNullWhen(false)] out String?                       ErrorResponse)
        {

            ElectricChargingPointStatus  = null;
            ErrorResponse                = null;

            #region TryParse Status                          [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.EnergyInfrastructure + "status",
                                       "refill point status",
                                       RefillPointStatusEnum.TryParse,
                                       out RefillPointStatusEnum refillPointStatus,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse Reference                       [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.Facilities + "reference",
                                       "facility object versioned reference",
                                       FacilityObjectVersionedReference.TryParseXML,
                                       out FacilityObjectVersionedReference? reference,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion


            #region TryParse EnergyRateUpdates               [optional]

            if (XML.TryParseOptional(DatexIINS.EnergyInfrastructure + "unitsInStock",
                                     "units in stock",
                                     out Double? unitsInStock,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse EnergyRateUpdates               [optional]

            if (XML.TryParseOptionalElements(DatexIINS.EnergyInfrastructure + "energyRateUpdate",
                                             "energy rate updates",
                                             EnergyRateUpdate.TryParseXML,
                                             out IEnumerable<EnergyRateUpdate> energyRateUpdates,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse WaitingTime                     [optional]

            TimeSpan? waitingTime = null;

            var xml = XML.Element(DatexIINS.EnergyInfrastructure + "waitingTime");
            if (xml is not null)
            {

                if (!xml.TryParseMandatoryTimeSpan(DatexIINS.Facilities + "duration",
                                                   "waiting time",
                                                   out TimeSpan _waitingTime,
                                                   out ErrorResponse))
                {
                    return false;
                }

                waitingTime = _waitingTime;

            }

            #endregion

            #region TryParse PlannedRefillPointStatus        [optional]

            if (XML.TryParseOptionalElements(DatexIINS.EnergyInfrastructure + "plannedRefillPointStatus",
                                             "planned refill point status",
                                             EnergyInfrastructure.PlannedRefillPointStatus.TryParseXML,
                                             out IEnumerable<PlannedRefillPointStatus> plannedRefillPointStatus,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            #region TryParse LastUpdated                     [optional]

            if (XML.TryParseOptionalTimestamp(DatexIINS.Facilities + "lastUpdated",
                                              "last updated",
                                              out DateTimeOffset? lastUpdated,
                                              out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse OpeningStatus                   [optional]

            if (XML.TryParseOptional(DatexIINS.Facilities + "openingStatus",
                                     "opening status",
                                     Facilities.OpeningStatus.TryParse,
                                     out OpeningStatus? openingStatus,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse OperationStatus                 [optional]

            if (XML.TryParseOptional(DatexIINS.Facilities + "operationStatus",
                                     "operation status",
                                     Facilities.OperationStatus.TryParse,
                                     out OperationStatus? operationStatus,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse RegularOperatingHoursInForce    [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "regularOperatingHoursInForce",
                                     "regular operating hours in force",
                                     out Boolean? regularOperatingHoursInForce,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse StatusDescription               [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "statusDescription",
                                     "status description",
                                     MultilingualString.TryParseXML,
                                     out MultilingualString? statusDescription,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            //newOperatingHours
            //fault


            #region TryParse RemainingChargingTime           [optional]

            if (XML.TryParseOptionalTimeSpan(DatexIINS.EnergyInfrastructure + "remainingChargingTime",
                                             "remaining charging time",
                                             out TimeSpan? remainingChargingTime,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse CurrentVoltage                  [optional]

            if (XML.TryParseOptional(DatexIINS.EnergyInfrastructure + "currentVoltage",
                                     "current voltage",
                                     Volt.TryParse,
                                     out Volt? currentVoltage,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse CurrentChargingPower            [optional]

            if (XML.TryParseOptional(DatexIINS.EnergyInfrastructure + "currentChargingPower",
                                     "current charging power",
                                     Watt.TryParse,
                                     out Watt? currentChargingPower,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse NextAvailableChargingSlots      [optional]

            if (XML.TryParseOptionalTimestamps(DatexIINS.Facilities + "lastUpdated",
                                               "last updated",
                                               out IEnumerable<DateTimeOffset> nextAvailableChargingSlots,
                                               out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            ElectricChargingPointStatus = new ElectricChargingPointStatus(

                                              refillPointStatus,
                                              reference,

                                              unitsInStock,
                                              energyRateUpdates,
                                              waitingTime,
                                              plannedRefillPointStatus,
                                              XML.Element(DatexIINS.Common + "_refillPointStatusExtension"),

                                              lastUpdated,
                                              openingStatus,
                                              operationStatus,
                                              regularOperatingHoursInForce,
                                              statusDescription,
                                              null, //newOperatingHours,
                                              null, //fault,
                                              XML.Element(DatexIINS.Common + "_facilityObjectStatusExtension"),

                                              remainingChargingTime,
                                              currentVoltage,
                                              currentChargingPower,
                                              nextAvailableChargingSlots,
                                              XML.Element(DatexIINS.Common + "_electricChargingPointStatusExtension")

                                          );

            return true;

        }

        #endregion

    }

}
