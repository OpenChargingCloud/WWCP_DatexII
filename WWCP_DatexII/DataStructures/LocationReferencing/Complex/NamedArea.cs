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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// An area defined by a name and/or in terms of known boundaries, such as country or county boundaries or an allocated control area.
    /// The attributes do not form a union; instead, the smallest intersection forms the resulting area.
    /// </summary>
    [XmlType("NamedArea", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class NamedArea(MultilingualString  AreaName,
                           NamedAreaType?      NamedAreaType         = null,
                           Country?            Country               = null,
                           XElement?           NamedAreaExtension    = null,
                           XElement?           ANamedAreaExtension   = null)

        : ANamedArea(ANamedAreaExtension)

    {

        #region Properties

        /// <summary>
        /// The name of the area.
        /// </summary>
        [XmlElement("areaName",             Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString  AreaName              { get; } = AreaName;

        /// <summary>
        /// The type of the area.
        /// </summary>
        [XmlElement("namedAreaType",        Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public NamedAreaType?      NamedAreaType         { get; } = NamedAreaType;

        /// <summary>
        /// EN ISO 3166-1 two-character country code.
        /// </summary>
        [XmlElement("country",              Namespace = "http://datex2.eu/schema/3/common")]
        public Country?            Country               { get; } = Country;

        /// <summary>
        /// Optional extension element for additional named area information.
        /// </summary>
        [XmlElement("_namedAreaExtension",  Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public XElement?           NamedAreaExtension    { get; } = NamedAreaExtension;

        #endregion


        #region TryParseXML(XML, out NamedArea, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an NamedArea.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="NamedArea">The parsed NamedArea.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                             XML,
                                          [NotNullWhen(true)]  out NamedArea?  NamedArea,
                                          [NotNullWhen(false)] out String?     ErrorResponse)
        {

            NamedArea      = null;
            ErrorResponse  = null;

            #region TryParse AreaName         [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.Common + "areaName",
                                       "area name",
                                       MultilingualString.TryParseXML,
                                       out MultilingualString? areaName,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse NamedAreaType    [optional]

            if (XML.TryParseOptional(DatexIINS.LocationReferencing + "namedAreaType",
                                     "named area type",
                                     LocationReferencing.NamedAreaType.TryParse,
                                     out NamedAreaType? namedAreaType,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse Country          [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "country",
                                     "closure information",
                                     Country.TryParse,
                                     out Country? country,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            NamedArea = new NamedArea(

                            areaName,
                            namedAreaType,
                            country,

                            XML.Element(DatexIINS.Common + "_namedAreaExtension")

                        );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null)

        public override XElement ToXML(XName? XMLName = null)
        {

            var xml = new XElement(XMLName ?? DatexIINS.LocationReferencing + "NamedArea",

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
                              : null

                      );

            return xml;

        }

        #endregion

    }

}
