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

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// Represents one or more physically separate locations.
    /// Multiple locations may be related, as in an itinerary or route, or may be unrelated.
    /// One LocationReference should not use multiple Location objects to represent the same physical location.
    /// </summary>
    [XmlType("LocationReference", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public abstract class ALocationReference(XElement? LocationReferenceExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Optional extension element for additional location reference information.
        /// </summary>
        [XmlElement("_locationReferenceExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  LocationReferenceExtension    { get; } = LocationReferenceExtension;

        #endregion

    }

}
