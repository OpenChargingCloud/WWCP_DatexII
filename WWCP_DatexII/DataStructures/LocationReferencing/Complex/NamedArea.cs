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

using System.Xml.Serialization;

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using System.Xml.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// An area defined by a name and/or in terms of known boundaries, such as country or county boundaries or an allocated control area.
    /// The attributes do not form a union; instead, the smallest intersection forms the resulting area.
    /// </summary>
    [XmlType("NamedArea", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class NamedArea(MultilingualString  AreaName,
                           NamedAreaTypes?     NamedAreaType         = null,
                           Country?            Country               = null,
                           XElement?           NamedAreaExtension    = null,

                           XElement?           ANamedAreaExtension   = null)

        : ANamedArea(ANamedAreaExtension)

    {

        #region Properties

        /// <summary>
        /// The name of the area.
        /// </summary>
        [XmlElement("areaName",       Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString  AreaName              { get; } = AreaName;

        /// <summary>
        /// The type of the area.
        /// </summary>
        [XmlElement("namedAreaType",  Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public NamedAreaTypes?     NamedAreaType         { get; } = NamedAreaType;

        /// <summary>
        /// EN ISO 3166-1 two-character country code.
        /// </summary>
        [XmlElement("country",        Namespace = "http://datex2.eu/schema/3/common")]
        public Country?            Country               { get; } = Country;

        /// <summary>
        /// Optional extension element for additional named area information.
        /// </summary>
        [XmlElement("_namedAreaExtension", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public XElement?           NamedAreaExtension    { get; } = NamedAreaExtension;

        #endregion




        #region ToXML(XMLName = null)

        public override XElement ToXML(XName? XMLName = null)
        {

            var xml = new XElement(XMLName ?? DatexIINS.LocationReferencing + "NamedArea",

                           new XElement(DatexIINS.LocationReferencing + "areaName",
                               AreaName.ToXML()
                           )

                      );

            return xml;

        }

        #endregion

    }

}
