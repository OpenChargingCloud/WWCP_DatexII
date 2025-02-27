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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Specification of a specific public holiday in case specialDayType is set to 'publicHoliday'.
    /// </summary>
    [XmlType("PublicHoliday", Namespace = "http://datex2.eu/schema/3/common")]
    public class PublicHoliday(MultilingualString        PublicHolidayName,

                               Boolean                   IntersectWithApplicableDays,
                               SpecialDayType            SpecialDayType,
                               PublicEventType?          PublicEvent              = null,
                               IEnumerable<ANamedArea>?  NamedAreas               = null,
                               XElement?                 SpecialDayExtension      = null,
                               XElement?                 PublicHolidayExtension   = null)


        : SpecialDay(IntersectWithApplicableDays,
                     SpecialDayType,
                     PublicEvent,
                     NamedAreas,
                     SpecialDayExtension)

    {

        #region Properties

        /// <summary>
        /// Specification of a specific public holiday by its name.
        /// </summary>
        [XmlElement("publicHolidayName", Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString  PublicHolidayName         { get; } = PublicHolidayName;

        /// <summary>
        /// Optional extension element for additional public holiday information.
        /// </summary>
        [XmlElement("_publicHolidayExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?           PublicHolidayExtension    { get; } = PublicHolidayExtension;

        #endregion

    }

}
