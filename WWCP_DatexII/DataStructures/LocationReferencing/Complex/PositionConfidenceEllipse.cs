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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// Confidence ellipse position defined in the shape of an ellipse with a predefined confidence level.
    /// The centre of the ellipse corresponds to the reference position point for which the position accuracy is evaluated.
    /// </summary>
    [XmlType("PositionConfidenceEllipse", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class PositionConfidenceEllipse(Meter?                          SemiMajorAxisLength             = null,
                                           PositionConfidenceCodedErrors?  SemiMajorAxisLengthCodedError   = null,
                                           Meter?                          SemiMinorAxisLength             = null,
                                           PositionConfidenceCodedErrors?  SemiMinorAxisLengthCodedError   = null,
                                           AngleInDegrees?                 SemiMajorAxisOrientation        = null,
                                           Boolean?                        SemiMajorAxisOrientationError   = null)
    {

        /// <summary>
        /// Half of length of the major axis, i.e. the distance between the centre point and the major axis point of the position accuracy ellipse.
        /// </summary>
        [XmlElement("semiMajorAxisLength",                  Namespace = "http://datex2.eu/schema/3/common")]
        public Meter?                          SemiMajorAxisLength                   { get; set; } = SemiMajorAxisLength;

        /// <summary>
        /// Provides a coded error in case the semi-major axis length is not defined.
        /// </summary>
        [XmlElement("semiMajorAxisLengthCodedError",        Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PositionConfidenceCodedErrors?  SemiMajorAxisLengthCodedError         { get; set; } = SemiMajorAxisLengthCodedError;

        /// <summary>
        /// Half of length of the minor axis, i.e. the distance between the centre point and the minor axis point of the position accuracy ellipse.
        /// </summary>
        [XmlElement("semiMinorAxisLength",                  Namespace = "http://datex2.eu/schema/3/common")]
        public Meter?                          SemiMinorAxisLength                   { get; set; } = SemiMinorAxisLength;

        /// <summary>
        /// Provides a coded error in case the semi-minor axis length is not defined.
        /// </summary>
        [XmlElement("semiMinorAxisLengthCodedError",        Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PositionConfidenceCodedErrors?  SemiMinorAxisLengthCodedError         { get; set; } = SemiMinorAxisLengthCodedError;

        /// <summary>
        /// Orientation direction of the ellipse's major axis with respect to geographic north, in degrees.
        /// </summary>
        [XmlElement("semiMajorAxisOrientation",             Namespace = "http://datex2.eu/schema/3/common")]
        public AngleInDegrees?                 SemiMajorAxisOrientation              { get; set; } = SemiMajorAxisOrientation;

        /// <summary>
        /// Indicates whether the ellipse orientation is unavailable (True) or not (False).
        /// </summary>
        [XmlElement("semiMajorAxisOrientationError",        Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?                        SemiMajorAxisOrientationError         { get; set; } = SemiMajorAxisOrientationError;

        /// <summary>
        /// Optional extension element for additional position confidence ellipse information.
        /// </summary>
        [XmlElement("_positionConfidenceEllipseExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                       PositionConfidenceEllipseExtension    { get; set; }

    }

}
