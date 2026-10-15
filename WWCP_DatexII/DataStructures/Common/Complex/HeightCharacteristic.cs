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
    /// Height characteristic of a vehicle.
    /// </summary>
    [XmlType("HeightCharacteristic", Namespace = "http://datex2.eu/schema/3/common")]
    public class HeightCharacteristic(ComparisonOperator  ComparisonOperator,
                                      Meter               VehicleHeight,
                                      XElement?           HeightCharacteristicExtension   = null)
    {

        #region Properties

        /// <summary>
        /// The operator to be used in the vehicle characteristic comparison operation.
        /// </summary>
        [XmlElement("comparisonOperator", Namespace = "http://datex2.eu/schema/3/common")]
        public ComparisonOperator  ComparisonOperator               { get; } = ComparisonOperator;

        /// <summary>
        /// The height of the highest part, excluding antennae, of an individual vehicle above the road surface, in metres.
        /// </summary>
        [XmlElement("vehicleHeight",                   Namespace = "http://datex2.eu/schema/3/common")]
        public Meter               VehicleHeight                    { get; } = VehicleHeight;

        /// <summary>
        /// Optional extension element for additional height characteristic information.
        /// </summary>
        [XmlElement("_heightCharacteristicExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?           HeightCharacteristicExtension    { get; } = HeightCharacteristicExtension;

        #endregion


        #region TryParseXML(XML, out HeightCharacteristic, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a HeightCharacteristic.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="HeightCharacteristic">The parsed HeightCharacteristic.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                       XML,
                                          [NotNullWhen(true)]  out HeightCharacteristic?  HeightCharacteristic,
                                          [NotNullWhen(false)] out String?               ErrorResponse)
        {

            HeightCharacteristic = null;

            if (!XML.TryParseMandatoryValue(DatexIINS.Common + "comparisonOperator",
                                            "comparison operator",
                                            Common.ComparisonOperator.TryParse,
                                            out ComparisonOperator comparisonOperator,
                                            out ErrorResponse) ||
                !XML.TryParseMandatoryValue(DatexIINS.Common + "vehicleHeight",
                                            "vehicle height",
                                            DatexIIXMLExtensions.TryParseMeter,
                                            out Meter measure,
                                            out ErrorResponse))
            {
                return false;
            }

            HeightCharacteristic = new HeightCharacteristic(
                                        comparisonOperator,
                                                          measure,
                                        XML.Element(DatexIINS.Common + "_heightCharacteristicExtension")
                                    );

            return true;

        }

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.Common + "heightCharacteristic",

                   new XElement(DatexIINS.Common + "comparisonOperator",   ComparisonOperator.ToString()),
                   new XElement(DatexIINS.Common + "vehicleHeight",          VehicleHeight.m),

                   HeightCharacteristicExtension is not null
                       ? HeightCharacteristicExtension.ToExtensionXML(DatexIINS.Common + "_heightCharacteristicExtension")
                       : null

               );

        #endregion

    }

}
