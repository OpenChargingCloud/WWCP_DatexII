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
    /// Weight characteristic of the heaviest axle on the vehicle.
    /// </summary>
    [XmlType("HeaviestAxleWeightCharacteristic", Namespace = "http://datex2.eu/schema/3/common")]
    public class HeaviestAxleWeightCharacteristic(ComparisonOperator  ComparisonOperator,
                                                  Tonne               HeaviestAxleWeight,
                                                  XElement?           HeaviestAxleWeightCharacteristicExtension = null)
    {

        #region Properties

        /// <summary>
        /// The operator to be used in the vehicle characteristic comparison operation.
        /// </summary>
        [XmlElement("comparisonOperator", Namespace = "http://datex2.eu/schema/3/common")]
        public ComparisonOperator  ComparisonOperator                           { get; } = ComparisonOperator;

        /// <summary>
        /// The weight of the heaviest axle on the vehicle.
        /// </summary>
        [XmlElement("heaviestAxleWeight", Namespace = "http://datex2.eu/schema/3/common")]
        public Tonne               HeaviestAxleWeight                           { get; } = HeaviestAxleWeight;

        /// <summary>
        /// Optional extension element for additional heaviest axle weight characteristic information.
        /// </summary>
        [XmlElement("_heaviestAxleWeightCharacteristicExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?           HeaviestAxleWeightCharacteristicExtension    { get; } = HeaviestAxleWeightCharacteristicExtension;

        #endregion


        #region TryParseXML(XML, out HeaviestAxleWeightCharacteristic, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a HeaviestAxleWeightCharacteristic.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="HeaviestAxleWeightCharacteristic">The parsed HeaviestAxleWeightCharacteristic.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                       XML,
                                          [NotNullWhen(true)]  out HeaviestAxleWeightCharacteristic?  HeaviestAxleWeightCharacteristic,
                                          [NotNullWhen(false)] out String?               ErrorResponse)
        {

            HeaviestAxleWeightCharacteristic = null;

            if (!XML.TryParseMandatoryValue(DatexIINS.Common + "comparisonOperator",
                                            "comparison operator",
                                            Common.ComparisonOperator.TryParse,
                                            out ComparisonOperator comparisonOperator,
                                            out ErrorResponse) ||
                !XML.TryParseMandatoryValue(DatexIINS.Common + "heaviestAxleWeight",
                                            "heaviest axle weight",
                                            DatexIIXMLExtensions.TryParseTonne,
                                            out Tonne measure,
                                            out ErrorResponse))
            {
                return false;
            }

            HeaviestAxleWeightCharacteristic = new HeaviestAxleWeightCharacteristic(
                                                    comparisonOperator,
                                                                      measure,
                                                    XML.Element(DatexIINS.Common + "_heaviestAxleWeightCharacteristicExtension")
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

            => new (XMLName ?? DatexIINS.Common + "heaviestAxleWeightCharacteristic",

                   new XElement(DatexIINS.Common + "comparisonOperator",   ComparisonOperator.ToString()),
                   new XElement(DatexIINS.Common + "heaviestAxleWeight",          HeaviestAxleWeight.Value),

                   HeaviestAxleWeightCharacteristicExtension is not null
                       ? HeaviestAxleWeightCharacteristicExtension.ToExtensionXML(DatexIINS.Common + "_heaviestAxleWeightCharacteristicExtension")
                       : null

               );

        #endregion

    }

}
