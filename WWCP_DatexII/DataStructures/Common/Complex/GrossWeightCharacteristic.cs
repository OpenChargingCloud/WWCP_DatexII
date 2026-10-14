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
    /// Gross weight characteristic of a vehicle.
    /// </summary>
    [XmlType("GrossWeightCharacteristic", Namespace = "http://datex2.eu/schema/3/common")]
    public class GrossWeightCharacteristic(ComparisonOperator  ComparisonOperator,
                                           Tonne               GrossVehicleWeight,
                                           WeightType          TypeOfWeight,
                                           XElement?           GrossWeightCharacteristicExtension   = null)
    {

        #region Properties

        /// <summary>
        /// The operator to be used in the vehicle characteristic comparison operation.
        /// </summary>
        [XmlElement("comparisonOperator",                   Namespace = "http://datex2.eu/schema/3/common")]
        public ComparisonOperator  ComparisonOperator                    { get; } = ComparisonOperator;

        /// <summary>
        /// The gross weight of the vehicle and its load, including any trailers.
        /// </summary>
        [XmlElement("grossVehicleWeight",                   Namespace = "http://datex2.eu/schema/3/common")]
        public Tonne               GrossVehicleWeight                    { get; } = GrossVehicleWeight;

        /// <summary>
        /// The meaning of the weight value.
        /// </summary>
        [XmlElement("typeOfWeight",                         Namespace = "http://datex2.eu/schema/3/common")]
        public WeightType          TypeOfWeight                          { get; } = TypeOfWeight;

        /// <summary>
        /// Optional extension element for additional information.
        /// </summary>
        [XmlElement("_grossWeightCharacteristicExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?           GrossWeightCharacteristicExtension    { get; } = GrossWeightCharacteristicExtension;

        #endregion


        #region TryParseXML(XML, out GrossWeightCharacteristic, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a GrossWeightCharacteristic.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="GrossWeightCharacteristic">The parsed GrossWeightCharacteristic.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                       XML,
                                          [NotNullWhen(true)]  out GrossWeightCharacteristic?  GrossWeightCharacteristic,
                                          [NotNullWhen(false)] out String?               ErrorResponse)
        {

            GrossWeightCharacteristic = null;

            if (!XML.TryParseMandatoryValue(DatexIINS.Common + "comparisonOperator",
                                            "comparison operator",
                                            Common.ComparisonOperator.TryParse,
                                            out ComparisonOperator comparisonOperator,
                                            out ErrorResponse) ||
                !XML.TryParseMandatoryValue(DatexIINS.Common + "grossVehicleWeight",
                                            "gross vehicle weight",
                                            DatexIIXMLExtensions.TryParseTonne,
                                            out Tonne measure,
                                            out ErrorResponse))
            {
                return false;
            }

            if (!XML.TryParseMandatoryValue(DatexIINS.Common + "typeOfWeight",
                                            "type of weight",
                                            WeightType.TryParse,
                                            out WeightType typeOfWeight,
                                            out ErrorResponse))
            {
                return false;
            }

            GrossWeightCharacteristic = new GrossWeightCharacteristic(
                                             comparisonOperator,
                                                               measure,
                                                               typeOfWeight,
                                             XML.Element(DatexIINS.Common + "_grossWeightCharacteristicExtension")
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

            => new (XMLName ?? DatexIINS.Common + "grossWeightCharacteristic",

                   new XElement(DatexIINS.Common + "comparisonOperator",   ComparisonOperator.ToString()),
                   new XElement(DatexIINS.Common + "grossVehicleWeight",          GrossVehicleWeight.Value),

                   new XElement(DatexIINS.Common + "typeOfWeight",         TypeOfWeight.ToString()),

                   GrossWeightCharacteristicExtension is not null
                       ? new XElement(DatexIINS.Common + "_grossWeightCharacteristicExtension", GrossWeightCharacteristicExtension)
                       : null

               );

        #endregion

    }

}
