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

using System.Diagnostics.Metrics;
using System.Xml.Serialization;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// Confidence ellipse position defined in the shape of an ellipse with a predefined confidence level.
    /// The centre of the ellipse corresponds to the reference position point for which the position accuracy is evaluated.
    /// </summary>
    [XmlType("PositionConfidenceEllipse", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class PositionConfidenceEllipse
    {
        /// <summary>
        /// Half of length of the major axis, i.e. the distance between the centre point and the major axis point of the position accuracy ellipse.
        /// </summary>
        [XmlElement("semiMajorAxisLength", Namespace = "http://datex2.eu/schema/3/common")]
        public Meter? SemiMajorAxisLength { get; set; }

        /// <summary>
        /// Provides a coded error in case the semi-major axis length is not defined.
        /// </summary>
        [XmlElement("semiMajorAxisLengthCodedError", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PositionConfidenceCodedErrors? SemiMajorAxisLengthCodedError { get; set; }

        /// <summary>
        /// Half of length of the minor axis, i.e. the distance between the centre point and the minor axis point of the position accuracy ellipse.
        /// </summary>
        [XmlElement("semiMinorAxisLength", Namespace = "http://datex2.eu/schema/3/common")]
        public Meter? SemiMinorAxisLength { get; set; }

        /// <summary>
        /// Provides a coded error in case the semi-minor axis length is not defined.
        /// </summary>
        [XmlElement("semiMinorAxisLengthCodedError", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PositionConfidenceCodedErrors? SemiMinorAxisLengthCodedError { get; set; }

        /// <summary>
        /// Orientation direction of the ellipse's major axis with respect to geographic north, in degrees.
        /// </summary>
        [XmlElement("semiMajorAxisOrientation", Namespace = "http://datex2.eu/schema/3/common")]
        public AngleInDegrees? SemiMajorAxisOrientation { get; set; }

        /// <summary>
        /// Indicates whether the ellipse orientation is unavailable (True) or not (False).
        /// </summary>
        [XmlElement("semiMajorAxisOrientationError", Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean? SemiMajorAxisOrientationError { get; set; }

        ///// <summary>
        ///// Optional extension element for additional position confidence ellipse information.
        ///// </summary>
        //[XmlElement("_positionConfidenceEllipseExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? PositionConfidenceEllipseExtension { get; set; }

    }

}
