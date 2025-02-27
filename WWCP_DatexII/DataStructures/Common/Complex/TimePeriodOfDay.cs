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
    /// Specification of a continuous period of time within a 24 hour period.
    /// </summary>
    [XmlType("TimePeriodOfDay", Namespace = "http://datex2.eu/schema/3/common")]
    public class TimePeriodOfDay(Time       StartTimeOfPeriod,
                                 Time       EndTimeOfPeriod,
                                 XElement?  TimePeriodOfDayExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Start of time period.
        /// </summary>
        [XmlElement("startTimeOfPeriod",          Namespace = "http://datex2.eu/schema/3/common")]
        public Time       StartTimeOfPeriod           { get; } = StartTimeOfPeriod;

        /// <summary>
        /// End of time period.
        /// </summary>
        [XmlElement("endTimeOfPeriod",            Namespace = "http://datex2.eu/schema/3/common")]
        public Time       EndTimeOfPeriod             { get; } = EndTimeOfPeriod;

        /// <summary>
        /// Optional extension element for additional time period information.
        /// </summary>
        [XmlElement("_timePeriodOfDayExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  TimePeriodOfDayExtension    { get; } = TimePeriodOfDayExtension;

        #endregion


        #region TryParseXML(XML, out TimePeriodOfDay, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a TimePeriodOfDay.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="TimePeriodOfDay">The parsed TimePeriodOfDay.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                   XML,
                                          [NotNullWhen(true)]  out TimePeriodOfDay?  TimePeriodOfDay,
                                          [NotNullWhen(false)] out String?           ErrorResponse)
        {

            TimePeriodOfDay  = null;
            ErrorResponse    = null;

            #region TryParse StartTimeOfPeriod    [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.Common + "startTimeOfPeriod",
                                       "start time of period",
                                       Time.TryParse,
                                       out Time? startTimeOfPeriod,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse EndTimeOfPeriod      [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.Common + "endTimeOfPeriod",
                                       "end time of period",
                                       Time.TryParse,
                                       out Time? endTimeOfPeriod,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion


            TimePeriodOfDay = new TimePeriodOfDay(

                                  startTimeOfPeriod.Value,
                                  endTimeOfPeriod.  Value,

                                  XML.Element(DatexIINS.Common + "_timePeriodOfDayExtension")

                              );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null)

        public XElement ToXML(XName? XMLName = null)
        {

            var xml = new XElement(XMLName ?? DatexIINS.Common + "period",

                          new XElement(DatexIINS.Common + "startTimeOfPeriod",   StartTimeOfPeriod),
                          new XElement(DatexIINS.Common + "endTimeOfPeriod",     EndTimeOfPeriod),

                          TimePeriodOfDayExtension

                      );

            return xml;

        }

        #endregion


    }

}
