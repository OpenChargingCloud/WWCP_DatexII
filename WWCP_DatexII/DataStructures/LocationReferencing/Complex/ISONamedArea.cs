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

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// The ISO 3166-2 representation for the named area.
    /// </summary>
    [XmlType("IsoNamedArea", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class ISONamedArea(MultilingualString  AreaName,
                              SubdivisionType     SubdivisionType,
                              SubdivisionCode     SubdivisionCode,
                              NamedAreaType?      NamedAreaType           = null,
                              Country?            Country                 = null,
                              XElement?           NamedAreaExtension      = null,
                              XElement?           ISONamedAreaExtension   = null,

                              XElement?           ANamedAreaExtension     = null)

        : NamedArea(AreaName,
                    NamedAreaType,
                    Country,
                    NamedAreaExtension,
                    ANamedAreaExtension)

    {

        #region Properties

        /// <summary>
        /// The ISO 3166-2 subdivision type for the named area.
        /// </summary>
        [XmlElement("subdivisionType",         Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public SubdivisionType  SubdivisionType          { get; } = SubdivisionType;

        /// <summary>
        /// The second part of an ISO 3166-2 subdivision code for the named area.
        /// This may be used along with the country attribute from the parent class to form a full ISO 3166-2 subdivision code.
        /// </summary>
        [XmlElement("subdivisionCode",         Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public SubdivisionCode  SubdivisionCode          { get; } = SubdivisionCode;

        /// <summary>
        /// Optional extension element for additional ISO named area information.
        /// </summary>
        [XmlElement("_isoNamedAreaExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?        ISONamedAreaExtension    { get; } = ISONamedAreaExtension;

        #endregion


        #region ToXML(XMLName = null)

        public override XElement ToXML(XName? XMLName = null)
        {

            var xml = new XElement(XMLName ?? DatexIINS.LocationReferencing + "IsoNamedArea",

                                new XElement(DatexIINS.LocationReferencing + "areaName",
                                    AreaName.ToXML()
                                ),

                          NamedAreaType.HasValue
                              ? new XElement(DatexIINS.LocationReferencing + "namedAreaType",
                                    NamedAreaType.ToString()
                                )
                              : null,

                          Country is not null
                              ? new XElement(DatexIINS.LocationReferencing + "country",
                                    Country.Alpha2Code
                                )
                              : null,

                          NamedAreaExtension is not null
                              ? new XElement(DatexIINS.Common + "_namedAreaExtension",   NamedAreaExtension)
                              : null,

                                new XElement(DatexIINS.LocationReferencing + "subdivisionType",   SubdivisionType.ToString()),
                                new XElement(DatexIINS.LocationReferencing + "subdivisionCode",   SubdivisionCode.ToString()),

                          ISONamedAreaExtension is not null
                              ? new XElement(DatexIINS.Common + "_isoNamedAreaExtension",   ISONamedAreaExtension)
                              : null

                      );

            return xml;

        }

        #endregion

    }

}
