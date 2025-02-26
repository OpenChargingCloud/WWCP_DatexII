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
    /// Specification of periods defined by the intersection of days or instances of them, calendar weeks and months.
    /// </summary>
    [XmlType("DayWeekMonth", Namespace = "http://datex2.eu/schema/3/common")]
    public class DayWeekMonth(IEnumerable<Day>?          ApplicableDays          = null,
                              IEnumerable<MonthOfYear>?  ApplicableMonths        = null,
                              XElement?                  DayWeekMonthExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Applicable day of the week.
        /// "All days of the week" is expressed by non-inclusion of this element.
        /// </summary>
        [XmlElement("applicableDay",           Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<Day>          ApplicableDays           { get; } = ApplicableDays?.  Distinct() ?? [];

        /// <summary>
        /// Applicable month of the year.
        /// "All months of the year" is expressed by non-inclusion of this element.
        /// </summary>
        [XmlElement("applicableMonth",         Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<MonthOfYear>  ApplicableMonths         { get; } = ApplicableMonths?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional information.
        /// </summary>
        [XmlElement("_dayWeekMonthExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                 DayWeekMonthExtension    { get; } = DayWeekMonthExtension;

        #endregion


        #region TryParseXML(XML, out DayWeekMonth, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a DayWeekMonth.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="DayWeekMonth">The parsed DayWeekMonth.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                XML,
                                          [NotNullWhen(true)]  out DayWeekMonth?  DayWeekMonth,
                                          [NotNullWhen(false)] out String?        ErrorResponse)
        {

            DayWeekMonth  = null;
            ErrorResponse    = null;

            #region TryParse ApplicableDays      [optional]

            if (XML.TryParseOptionalElements(DatexIINS.Common + "applicableDay",
                                             "applicable days",
                                             Day.TryParse,
                                             out IEnumerable<Day> applicableDays,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ApplicableMonths    [optional]

            if (!XML.TryParseOptionalElements(DatexIINS.Common + "applicableMonth",
                                              "applicable month",
                                              MonthOfYear.TryParse,
                                              out IEnumerable<MonthOfYear> applicableMonths,
                                              out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            DayWeekMonth = new DayWeekMonth(

                                  applicableDays,
                                  applicableMonths,

                                  XML.Element(DatexIINS.Common + "_timePeriodOfDayExtension")

                              );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null)

        public XElement ToXML(XName? XMLName = null)
        {

            // C# is very strict with XML namespaces!
            var xmlElement    = XMLName ?? DatexIINS.Common + "DayWeekMonth";
            var xmlNamespace  = xmlElement.Namespace;

            var xml = new XElement(XMLName ?? DatexIINS.Common + "period",

                          new XElement(xmlNamespace + "DayWeekMonth",

                              ApplicableDays.  Any()
                                  ? ApplicableDays.  Select(day   => new XElement(xmlNamespace + "applicableDay",    day.  ToString()))
                                  : null,

                              ApplicableMonths.Any()
                                  ? ApplicableMonths.Select(month => new XElement(xmlNamespace + "applicableMonth",  month.ToString()))
                                  : null

                          )

                      );

            return xml;

        }

        #endregion


    }

}
