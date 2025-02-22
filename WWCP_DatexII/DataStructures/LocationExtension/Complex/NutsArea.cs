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

using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationExtension
{

    /// <summary>
    /// The NUTS-Code representation for the area (Nomenclature of territorial units for statistics)
    /// or its LAU code representation (Local Administrative Unit).
    /// </summary>
    [XmlType("NutsArea", Namespace = "http://datex2.eu/schema/3/locationExtension")]
    public class NutsArea(NutsCode       NutsCode,
                          NutsCodeTypes  NutsCodeType)
    {

        /// <summary>
        /// The NUTS code for the named area.
        /// </summary>
        [XmlElement("nutsCode",            Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public NutsCode       NutsCode             { get; set; } = NutsCode;

        /// <summary>
        /// The NUTS code type for the named area.
        /// </summary>
        [XmlElement("nutsCodeType",        Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public NutsCodeTypes  NutsCodeType         { get; set; } = NutsCodeType;

        /// <summary>
        /// Optional extension element for additional NutsArea information.
        /// </summary>
        [XmlElement("_nutsAreaExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?      NutsAreaExtension    { get; set; }

    }

}
