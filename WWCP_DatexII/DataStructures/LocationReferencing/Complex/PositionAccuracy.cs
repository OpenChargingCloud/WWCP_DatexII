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

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// Horizontal position accuracy parameters defined according to EN 16803-1.
    /// </summary>
    [XmlType("PositionAccuracy", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class PositionAccuracy(Double?    AccuracyPercentile50        = null,
                                  Double?    AccuracyPercentile75        = null,
                                  Double?    AccuracyPercentile95        = null,
                                  XElement?  PositionAccuracyExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Accuracy defined by the 50th percentile of the cumulative distribution of position errors.
        /// </summary>
        [XmlElement("accuracyPercentile50",        Namespace = "http://datex2.eu/schema/3/common")]
        public Double?    AccuracyPercentile50         { get; } = AccuracyPercentile50;

        /// <summary>
        /// Accuracy defined by the 75th percentile of the cumulative distribution of position errors.
        /// </summary>
        [XmlElement("accuracyPercentile75",        Namespace = "http://datex2.eu/schema/3/common")]
        public Double?    AccuracyPercentile75         { get; } = AccuracyPercentile75;

        /// <summary>
        /// Accuracy defined by the 95th percentile of the cumulative distribution of position errors.
        /// </summary>
        [XmlElement("accuracyPercentile95",        Namespace = "http://datex2.eu/schema/3/common")]
        public Double?    AccuracyPercentile95         { get; } = AccuracyPercentile95;

        /// <summary>
        /// Optional extension element for additional position accuracy information.
        /// </summary>
        [XmlElement("_positionAccuracyExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  PositionAccuracyExtension    { get; } = PositionAccuracyExtension;

        #endregion

    }

}
