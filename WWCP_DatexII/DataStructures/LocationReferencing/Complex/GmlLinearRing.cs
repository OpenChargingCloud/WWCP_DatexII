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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// Closed line string not self-intersecting (i.e. having as last point the first point)
    /// </summary>
    [XmlType("GmlLinearRing", Namespace = "http://datex2.eu/schema/3/locationExtension")]
    public class GMLLinearRing(GMLPosList  PosList,
                               UInt32?     SrsDimension             = null,
                               String?     SrsName                  = null,
                               XElement?   GMLLineStringExtension   = null,
                               XElement?   GMLLinearRingExtension   = null)

        : GMLLineString(PosList,
                        SrsDimension,
                        SrsName,
                        GMLLineStringExtension)

    {

        #region Properties

        /// <summary>
        /// Optional extension element for additional linear ring information.
        /// </summary>
        [XmlElement("_gmlLinearRingExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  GMLLinearRingExtension    { get; } = GMLLinearRingExtension;

        #endregion


        #region TryParseXML(XML, out GMLLinearRing, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a GML linear ring.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="GMLLinearRing">The parsed GML linear ring.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                 XML,
                                          [NotNullWhen(true)]  out GMLLinearRing?  GMLLinearRing,
                                          [NotNullWhen(false)] out String?         ErrorResponse)
        {

            GMLLinearRing  = null;
            ErrorResponse  = null;

            var posList = XML.Element(DatexIINS.LocationReferencing + "posList")?.Value;

            if (posList is null)
            {
                ErrorResponse = "The mandatory position list of the GML linear ring is missing!";
                return false;
            }

            UInt32? srsDimension = null;

            if (XML.Attribute("srsDimension") is XAttribute dimension)
            {
                if (!UInt32.TryParse(dimension.Value, out var value))
                {
                    ErrorResponse = $"Invalid SRS dimension '{dimension.Value}'!";
                    return false;
                }
                srsDimension = value;
            }

            try
            {
                GMLLinearRing = new GMLLinearRing(
                                    new GMLPosList(posList),
                                    srsDimension,
                                    XML.Attribute("srsName")?.Value,
                                    XML.Element(DatexIINS.LocationReferencing + "_gmlLineStringExtension"),
                                    XML.Element(DatexIINS.LocationReferencing + "_gmlLinearRingExtension")
                                );
            }
            catch (ArgumentException e)
            {
                ErrorResponse = e.Message;
                return false;
            }

            return true;

        }

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public override XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.LocationReferencing + "gmlLinearRing",

                   base.ToXML().Attributes(),
                   base.ToXML().Elements(),

                   GMLLinearRingExtension is not null
                       ? GMLLinearRingExtension.ToExtensionXML(DatexIINS.LocationReferencing + "_gmlLinearRingExtension")
                       : null

               );

        #endregion

    }

}
