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
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// A continuous time period or a set of discontinuous time periods defined by the
    /// intersection of a set of criteria all within an overall delimiting interval.
    /// </summary>
    [XmlType("Period", Namespace = "http://datex2.eu/schema/3/common")]
    public class Period(DateTimeOffset?                StartOfPeriod                 = null,
                        DateTimeOffset?                EndOfPeriod                   = null,
                        MultilingualString?            PeriodName                    = null,
                        IEnumerable<TimePeriodOfDay>?  RecurringTimePeriodOfDay      = null,
                        IEnumerable<DayWeekMonth>?     RecurringDayWeekMonthPeriod   = null,
                        IEnumerable<SpecialDay>?       RecurringSpecialDay           = null,
                        XElement?                      PeriodExtension               = null)
    {

        #region Properties

        /// <summary>
        /// Start of period.
        /// </summary>
        [XmlElement("startOfPeriod",                Namespace = "http://datex2.eu/schema/3/common")]
        public DateTimeOffset?               StartOfPeriod                  { get; } = StartOfPeriod;

        /// <summary>
        /// End of a period.
        /// </summary>
        [XmlElement("endOfPeriod",                  Namespace = "http://datex2.eu/schema/3/common")]
        public DateTimeOffset?               EndOfPeriod                    { get; } = EndOfPeriod;

        /// <summary>
        /// The name of the period.
        /// </summary>
        [XmlElement("periodName",                   Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?           PeriodName                     { get; } = PeriodName;

        /// <summary>
        /// A recurring period of a day.
        /// </summary>
        [XmlElement("recurringTimePeriodOfDay",     Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<TimePeriodOfDay>  RecurringTimePeriodOfDay       { get; } = RecurringTimePeriodOfDay?.   Distinct() ?? [];

        /// <summary>
        /// A recurring period defined in terms of days of the week, weeks of the month and months of the year.
        /// </summary>
        [XmlElement("recurringDayWeekMonthPeriod",  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<DayWeekMonth>     RecurringDayWeekMonthPeriod    { get; } = RecurringDayWeekMonthPeriod?.Distinct() ?? [];

        /// <summary>
        /// A recurring period in terms of special days.
        /// </summary>
        [XmlElement("recurringSpecialDay",          Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<SpecialDay>       RecurringSpecialDay            { get; } = RecurringSpecialDay?.        Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional period information.
        /// </summary>
        [XmlElement("_periodExtension",             Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                     PeriodExtension                { get; } = PeriodExtension;

        #endregion


        #region TryParseXML(XML, out Period, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a Period.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Period">The parsed Period.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                          XML,
                                          [NotNullWhen(true)]  out Period?  Period,
                                          [NotNullWhen(false)] out String?  ErrorResponse)
        {

            Period         = null;
            ErrorResponse  = null;

            #region TryParse StartOfPeriod                  [optional]

            if (XML.TryParseOptionalTimestamp(DatexIINS.Common + "startOfPeriod",
                                              "start of period",
                                              out DateTimeOffset? startOfPeriod,
                                              out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse EndOfPeriod                    [optional]

            if (XML.TryParseOptionalTimestamp(DatexIINS.Common + "endOfPeriod",
                                              "end of period",
                                              out DateTimeOffset? endOfPeriod,
                                              out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse PeriodName                     [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "periodName",
                                     "period name",
                                     MultilingualString.TryParseXML,
                                     out MultilingualString? periodName,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse RecurringTimePeriodOfDay       [optional]

            if (XML.TryParseOptionalElements(DatexIINS.Common + "recurringTimePeriodOfDay",
                                             "recurring time period of day",
                                             TimePeriodOfDay.TryParseXML,
                                             out IEnumerable<TimePeriodOfDay>? recurringTimePeriodOfDay,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse RecurringDayWeekMonthPeriod    [optional]

            if (XML.TryParseOptionalElements(DatexIINS.Common + "recurringDayWeekMonthPeriod",
                                             "recurring day week month period",
                                             DayWeekMonth.TryParseXML,
                                             out IEnumerable<DayWeekMonth>? recurringDayWeekMonthPeriod,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse RecurringSpecialDay            [optional]

            if (XML.TryParseOptionalElements(DatexIINS.Common + "recurringSpecialDay",
                                             "recurring day week month period",
                                             SpecialDay.TryParseXML,
                                             out IEnumerable<SpecialDay>? recurringSpecialDay,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            Period = new Period(

                         startOfPeriod,
                         endOfPeriod,
                         periodName,
                         recurringTimePeriodOfDay,
                         recurringDayWeekMonthPeriod,
                         recurringSpecialDay,

                         XML.Element(DatexIINS.Common + "_periodExtension")

                     );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null)

        public XElement ToXML(XName? XMLName = null)
        {

            var xml = new XElement(XMLName ?? DatexIINS.Common + "period",

                          StartOfPeriod.HasValue
                              ? new XElement(DatexIINS.Common + "startOfPeriod",   StartOfPeriod.Value.ToISO8601WithOffset())
                              : null,

                          EndOfPeriod.  HasValue
                              ? new XElement(DatexIINS.Common + "endOfPeriod",     EndOfPeriod.  Value.ToISO8601WithOffset())
                              : null,

                          PeriodName?.ToXML(DatexIINS.Common + "periodName"),

                          RecurringTimePeriodOfDay?.   Select(timePeriodOfDay => timePeriodOfDay.ToXML(DatexIINS.Common + "recurringTimePeriodOfDay")),

                          RecurringDayWeekMonthPeriod?.Select(dayWeekMonth    => dayWeekMonth.   ToXML(DatexIINS.Common + "recurringDayWeekMonthPeriod")),

                          RecurringSpecialDay?.        Select(specialDay      => specialDay.     ToXML(DatexIINS.Common + "recurringSpecialDay")),

                          PeriodExtension

                      );

            return xml;

        }

        #endregion

    }

}
