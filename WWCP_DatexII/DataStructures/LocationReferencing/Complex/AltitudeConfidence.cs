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
    /// Evaluation of the altitude confidence assessed according to ETSI ISO 102894-2.
    /// </summary>
    [XmlType("AltitudeConfidence", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class AltitudeConfidence(AltitudeAccuracy?              AltitudeAccuracyCodedValue    = null,
                                    PositionConfidenceCodedError?  AltitudeAccuracyCodedError    = null,
                                    XElement?                      AltitudeConfidenceExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Absolute accuracy of reported value of a geographical point for a confidence level expressed by a coded scale.
        /// </summary>
        [XmlElement("altitudeAccuracyCodedValue",    Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public AltitudeAccuracy?              AltitudeAccuracyCodedValue     { get; } = AltitudeAccuracyCodedValue;

        /// <summary>
        /// Error code in case the altitude confidence is out of range or cannot be determined.
        /// </summary>
        [XmlElement("altitudeAccuracyCodedError",    Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PositionConfidenceCodedError?  AltitudeAccuracyCodedError     { get; } = AltitudeAccuracyCodedError;

        /// <summary>
        /// Optional extension element for additional altitude confidence information.
        /// </summary>
        [XmlElement("_altitudeConfidenceExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                      AltitudeConfidenceExtension    { get; } = AltitudeConfidenceExtension;

        #endregion

    }

}
