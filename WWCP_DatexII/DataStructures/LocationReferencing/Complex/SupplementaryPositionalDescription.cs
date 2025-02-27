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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// A collection of supplementary positional information which improves the precision of the location.
    /// </summary>
    [XmlType("SupplementaryPositionalDescription", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class SupplementaryPositionalDescription(MultilingualString?  LocationDescription                           = null,
                                                    UInt32?              LocationPrecision                             = null,
                                                    XElement?            SupplementaryPositionalDescriptionExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Supplementary human-readable description of the location.
        /// </summary>
        [XmlElement("locationDescription",                           Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?  LocationDescription                            { get; } = LocationDescription;

        /// <summary>
        /// Indicates that the location is given with a precision which is better than the stated value in metres.
        /// </summary>
        [XmlAttribute("locationPrecision")]
        public UInt32?              LocationPrecision                              { get; } = LocationPrecision;

        /// <summary>
        /// Optional extension element for additional supplementary positional information.
        /// </summary>
        [XmlElement("_supplementaryPositionalDescriptionExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            SupplementaryPositionalDescriptionExtension    { get; } = SupplementaryPositionalDescriptionExtension;

        #endregion

    }

}
