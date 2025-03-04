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
    /// A two-dimensional part of the surface of the earth which is bounded by a closed curve.
    /// An area location may cover parts of the road network but does not necessarily need to.
    /// It is represented according to the OpenLR standard for Area Locations.
    /// </summary>
    [XmlType("OpenlrAreaLocationReference", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public abstract class AOpenLRAreaLocationReference(XElement?  OpenLRAreaLocationReferenceExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Optional extension element for additional OpenLR Area location reference information.
        /// </summary>
        [XmlElement("_openlrAreaLocationReferenceExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  OpenLRAreaLocationReferenceExtension    { get; } = OpenLRAreaLocationReferenceExtension;

        #endregion

    }

}
