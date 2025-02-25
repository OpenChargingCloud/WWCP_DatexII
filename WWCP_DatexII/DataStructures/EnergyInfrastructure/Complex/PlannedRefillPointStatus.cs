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
using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Planned status information for a refill point, for example reservations.
    /// </summary>
    [XmlType("PlannedRefillPointStatus", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class PlannedRefillPointStatus(RefillPointStatusEnum  Status,
                                          OverallPeriod          OverallPeriod,
                                          XElement?              PlannedRefillPointStatusExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Status of the refill point.
        /// </summary>
        [XmlElement("status",         Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public RefillPointStatusEnum  Status                               { get; } = Status;

        /// <summary>
        /// Overall period during which this planned status is effective.
        /// </summary>
        [XmlElement("overallPeriod",  Namespace = "http://datex2.eu/schema/3/common")]
        public OverallPeriod          OverallPeriod                        { get; } = OverallPeriod;

        /// <summary>
        /// Optional extension element for additional planned refill point status information.
        /// </summary>
        [XmlElement("_plannedRefillPointStatusExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?              PlannedRefillPointStatusExtension    { get; } = PlannedRefillPointStatusExtension;

        #endregion


        #region TryParseXML(XML, out PlannedRefillPointStatus, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an PlannedRefillPointStatus.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="PlannedRefillPointStatus">The parsed PlannedRefillPointStatus.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                            XML,
                                          [NotNullWhen(true)]  out PlannedRefillPointStatus?  PlannedRefillPointStatus,
                                          [NotNullWhen(false)] out String?                    ErrorResponse)
        {

            PlannedRefillPointStatus  = null;
            ErrorResponse             = null;

            // <?xml version="1.0"?>
            // <plannedRefillPointStatus xmlns="http://datex2.eu/schema/3/energyInfrastructure">
            //     <status>reserved</status>
            //     <overallPeriod>
            //         <com:overallStartTime xmlns:com="http://datex2.eu/schema/3/common">2025-02-02T15:00:00+01:00</com:overallStartTime>
            //         <com:overallEndTime   xmlns:com="http://datex2.eu/schema/3/common">2025-02-02T17:00:00+01:00</com:overallEndTime>
            //     </overallPeriod>
            // </plannedRefillPointStatus>

            #region TryParse Status           [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.EnergyInfrastructure + "status",
                                       "planned refill point status",
                                       RefillPointStatusEnum.TryParse,
                                       out RefillPointStatusEnum status,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse OverallPeriod    [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.EnergyInfrastructure + "overallPeriod",
                                       "overall period",
                                       OverallPeriod.TryParseXML,
                                       out OverallPeriod? overallPeriod,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion


            PlannedRefillPointStatus = new PlannedRefillPointStatus(
                                           status,
                                           overallPeriod,
                                           XML.Element(DatexIINS.Common + "_plannedRefillPointStatusExtension")
                                       );

            return true;

        }

        #endregion


    }

}
