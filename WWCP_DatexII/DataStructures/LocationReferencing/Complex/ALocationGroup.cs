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
    /// Multiple (i.e. more than one) physically separate locations which have no specific order.
    /// </summary>
    [XmlType("LocationGroup", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public abstract class ALocationGroup(XElement? LocationGroupExtension       = null,
                                         XElement? LocationReferenceExtension   = null)

        : ALocationReference(LocationReferenceExtension)

    {

        #region Properties

        /// <summary>
        /// Optional extension element for additional location group information.
        /// </summary>
        [XmlElement("_locationGroupExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  LocationGroupExtension    { get; } = LocationGroupExtension;

        #endregion

    }

}
