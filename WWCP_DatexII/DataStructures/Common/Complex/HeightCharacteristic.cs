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

    }

}
