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
    /// Specification of a special type of day, possibly also a public holiday. Can be country or region specific.
    /// </summary>
    [XmlType("SpecialDay", Namespace = "http://datex2.eu/schema/3/common")]
    public class SpecialDay(Boolean                   IntersectWithApplicableDays,
                            SpecialDayType            SpecialDayType,
                            PublicEventType?          PublicEvent           = null,
                            IEnumerable<ANamedArea>?  NamedAreas            = null,
                            XElement?                 SpecialDayExtension   = null)
    {

        #region Properties

        /// <summary>
        /// When true, the period is the intersection of applicable days and this special day.
        /// When false, the period is the union of applicable days and this special day.
        /// </summary>
        [XmlElement("intersectWithApplicableDays",  Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean                  IntersectWithApplicableDays    { get; } = IntersectWithApplicableDays;

        /// <summary>
        /// Specification of a special day, for example schoolDay, publicHoliday, etc.
        /// </summary>
        [XmlElement("specialDayType",               Namespace = "http://datex2.eu/schema/3/common")]
        public SpecialDayType           SpecialDayType                 { get; } = SpecialDayType;

        /// <summary>
        /// Type of public event on this special day.
        /// </summary>
        [XmlElement("publicEvent",                  Namespace = "http://datex2.eu/schema/3/common")]
        public PublicEventType?         PublicEvent                    { get; } = PublicEvent;

        /// <summary>
        /// One or more named areas (e.g. regions or localities) to which this special day applies.
        /// </summary>
        [XmlElement("namedArea",                    Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<ANamedArea>  NamedAreas                     { get; } = NamedAreas?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional special day information.
        /// </summary>
        [XmlElement("_specialDayExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                SpecialDayExtension            { get; } = SpecialDayExtension;

        #endregion


        #region TryParseXML(XML, out SpecialDay, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a SpecialDay.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="SpecialDay">The parsed SpecialDay.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                              XML,
                                          [NotNullWhen(true)]  out SpecialDay?  SpecialDay,
                                          [NotNullWhen(false)] out String?      ErrorResponse)
        {

            SpecialDay     = null;
            ErrorResponse  = null;

            #region TryParse IntersectWithApplicableDays    [mandatory]

            if (!XML.TryParseMandatoryBoolean(DatexIINS.Common + "intersectWithApplicableDays",
                                              "intersect with applicable days",
                                              out Boolean intersectWithApplicableDays,
                                              out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse SpecialDayType                 [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.Common + "intersectWithApplicableDays",
                                       "intersect with applicable days",
                                       SpecialDayType.TryParse,
                                       out SpecialDayType specialDayType,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse PublicEvent                    [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "publicEvent",
                                     "public event",
                                     PublicEventType.TryParse,
                                     out PublicEventType? publicEvent,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse NamedAreas                     [optional]

            if (XML.TryParseOptionalElements(DatexIINS.Common + "namedArea",
                                             "named area",
                                             ANamedArea.TryParseXML,
                                             out IEnumerable<ANamedArea> namedAreas,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            SpecialDay = new SpecialDay(

                             intersectWithApplicableDays,
                             specialDayType,
                             publicEvent,
                             namedAreas,

                             XML.Element(DatexIINS.Common + "_specialDayExtension")

                         );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null)

        public XElement ToXML(XName? XMLName = null)
        {

            var xml = new XElement(XMLName ?? DatexIINS.Common + "period",

                                new XElement(DatexIINS.Common + "intersectWithApplicableDays",   IntersectWithApplicableDays ? "true" : "false"),
                                new XElement(DatexIINS.Common + "specialDayType",                SpecialDayType.   ToString()),

                          PublicEvent.HasValue
                              ? new XElement(DatexIINS.Common + "publicEvent",                   PublicEvent.Value.ToString())
                              : null,

                          NamedAreas. Any()
                              ? new XElement(DatexIINS.Common + "namedArea",
                                    NamedAreas.Select(namedArea => namedArea.ToXML())
                                )
                              : null,

                          SpecialDayExtension

                      );

            return xml;

        }

        #endregion


    }

}
