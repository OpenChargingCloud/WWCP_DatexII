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

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Number of axles characteristic of a vehicle.
    /// </summary>
    [XmlType("NumberOfAxlesCharacteristic", Namespace = "http://datex2.eu/schema/3/common")]
    public class NumberOfAxlesCharacteristic(ComparisonOperator  ComparisonOperator,
                                             Byte                NumberOfAxles,
                                             XElement?           NumberOfAxlesCharacteristicExtension   = null)
    {

        #region Properties

        /// <summary>
        /// The operator to be used in the vehicle characteristic comparison operation.
        /// </summary>
        [XmlElement("comparisonOperator",  Namespace = "http://datex2.eu/schema/3/common")]
        public ComparisonOperator  ComparisonOperator                      { get; } = ComparisonOperator;

        /// <summary>
        /// The total number of axles of an individual vehicle.
        /// </summary>
        [XmlElement("numberOfAxles",       Namespace = "http://datex2.eu/schema/3/common")]
        public Byte                NumberOfAxles                           { get; } = NumberOfAxles;

        /// <summary>
        /// Optional extension element for additional number of axles characteristic information.
        /// </summary>
        [XmlElement("_numberOfAxlesCharacteristicExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?           NumberOfAxlesCharacteristicExtension    { get; } = NumberOfAxlesCharacteristicExtension;

        #endregion


        #region TryParseXML(XML, out NumberOfAxlesCharacteristic, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a NumberOfAxlesCharacteristic.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="NumberOfAxlesCharacteristic">The parsed NumberOfAxlesCharacteristic.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                       XML,
                                          [NotNullWhen(true)]  out NumberOfAxlesCharacteristic?  NumberOfAxlesCharacteristic,
                                          [NotNullWhen(false)] out String?               ErrorResponse)
        {

            NumberOfAxlesCharacteristic = null;

            if (!XML.TryParseMandatoryValue(DatexIINS.Common + "comparisonOperator",
                                            "comparison operator",
                                            Common.ComparisonOperator.TryParse,
                                            out ComparisonOperator comparisonOperator,
                                            out ErrorResponse) ||
                !XML.TryParseMandatoryValue(DatexIINS.Common + "numberOfAxles",
                                            "number of axles",
                                            DatexIIXMLExtensions.TryParseByte,
                                            out Byte measure,
                                            out ErrorResponse))
            {
                return false;
            }

            NumberOfAxlesCharacteristic = new NumberOfAxlesCharacteristic(
                                               comparisonOperator,
                                                                 measure,
                                               XML.Element(DatexIINS.Common + "_numberOfAxlesCharacteristicExtension")
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

            => new (XMLName ?? DatexIINS.Common + "numberOfAxlesCharacteristic",

                   new XElement(DatexIINS.Common + "comparisonOperator",   ComparisonOperator.ToString()),
                   new XElement(DatexIINS.Common + "numberOfAxles",          NumberOfAxles),

                   NumberOfAxlesCharacteristicExtension is not null
                       ? NumberOfAxlesCharacteristicExtension.ToExtensionXML(DatexIINS.Common + "_numberOfAxlesCharacteristicExtension")
                       : null

               );

        #endregion

    }

}
