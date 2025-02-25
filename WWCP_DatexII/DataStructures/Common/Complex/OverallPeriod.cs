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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// A continuous or discontinuous period of validity defined by overall bounding start and end times and
    /// the possible intersection of valid periods (potentially recurring) with the complement of exception
    /// periods (also potentially recurring).
    /// </summary>
    [XmlType("OverallPeriod", Namespace = "http://datex2.eu/schema/3/common")]
    public class OverallPeriod(DateTimeOffset        OverallStartTime,
                               DateTimeOffset?       OverallEndTime           = null,
                               IEnumerable<Period>?  ValidPeriod              = null,
                               IEnumerable<Period>?  ExceptionPeriod          = null,
                               XElement?             OverallPeriodExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Start of bounding period of validity defined by date and time.
        /// </summary>
        [XmlElement("overallStartTime",  Namespace = "http://datex2.eu/schema/3/common")]
        public DateTimeOffset       OverallStartTime          { get; } = OverallStartTime;

        /// <summary>
        /// End of bounding period of validity defined by date and time.
        /// </summary>
        [XmlElement("overallEndTime",    Namespace = "http://datex2.eu/schema/3/common")]
        public DateTimeOffset?      OverallEndTime            { get; } = OverallEndTime;

        /// <summary>
        /// A single time period, a recurring time period or a set of different recurring time periods during which validity is true.
        /// </summary>
        [XmlElement("validPeriod",       Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Period>  ValidPeriod               { get; } = ValidPeriod     ?? [];

        /// <summary>
        /// A single time period, a recurring time period or a set of different recurring time periods during which validity is false.
        /// </summary>
        [XmlElement("exceptionPeriod",   Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Period>  ExceptionPeriod           { get; } = ExceptionPeriod ?? [];

        /// <summary>
        /// Optional extension element for additional overall period information.
        /// </summary>
        [XmlElement("_overallPeriodExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            OverallPeriodExtension    { get; } = OverallPeriodExtension;

        #endregion


        #region TryParseXML(XML, out OverallPeriod, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an OverallPeriod.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="OverallPeriod">The parsed OverallPeriod.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                 XML,
                                          [NotNullWhen(true)]  out OverallPeriod?  OverallPeriod,
                                          [NotNullWhen(false)] out String?         ErrorResponse)
        {

            OverallPeriod = null;

            // <overallPeriod xmlns="http://datex2.eu/schema/3/energyInfrastructure">
            //     <com:overallStartTime xmlns:com="http://datex2.eu/schema/3/common">2025-02-02T15:00:00+01:00</com:overallStartTime>
            //     <com:overallEndTime   xmlns:com="http://datex2.eu/schema/3/common">2025-02-02T17:00:00+01:00</com:overallEndTime>
            // </overallPeriod>

            #region TryParse OverallStartTime    [mandatory]

            if (!XML.TryParseMandatoryTimestamp(DatexIINS.Common + "overallStartTime",
                                                "start time",
                                                out DateTimeOffset overallStartTime,
                                                out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse OverallEndTime      [optional]

            if (XML.TryParseOptionalTimestamp(DatexIINS.Common + "overallEndTime",
                                              "end time",
                                              out DateTimeOffset? overallEndTime,
                                              out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ValidPeriod         [optional]

            if (XML.TryParseOptionalElements(DatexIINS.Common + "validPeriod",
                                             "valid period",
                                             Period.TryParseXML,
                                             out IEnumerable<Period> validPeriod,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ExceptionPeriod     [optional]

            if (XML.TryParseOptionalElements(DatexIINS.Common + "exceptionPeriod",
                                             "exception period",
                                             Period.TryParseXML,
                                             out IEnumerable<Period> exceptionPeriod,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            OverallPeriod = new OverallPeriod(

                                overallStartTime,
                                overallEndTime,
                                validPeriod,
                                exceptionPeriod,
                                XML.Element(DatexIINS.Common + "_overallPeriodExtension")

                            );

            return true;

        }

        #endregion

    }

}
