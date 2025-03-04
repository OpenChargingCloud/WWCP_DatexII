﻿/*
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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Operating hours, either by reference or by specification.
    /// </summary>
    [XmlType("OperatingHours", Namespace = "http://datex2.eu/schema/3/facilities")]
    public abstract class AOperatingHours(ClosureInformation?  ClosureInformation        = null,
                                          XElement?            OperatingHoursExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Optional closure information.
        /// </summary>
        [XmlElement("closureInformation", Namespace = "http://datex2.eu/schema/3/facilities")]
        public ClosureInformation?  ClosureInformation         { get; } = ClosureInformation;

        /// <summary>
        /// Optional extension element for additional operating hours information.
        /// </summary>
        [XmlElement("_operatingHoursExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            OperatingHoursExtension    { get; } = OperatingHoursExtension;

        #endregion

    }

}
