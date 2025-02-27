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
    /// The NUTS-Code representation for the named area (Nomenclature of territorial units for statistics)
    /// or its LAU code representation (Local Administrative Unit).
    /// </summary>
    [XmlType("NutsNamedArea", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class NutsNamedArea(MultilingualString  AreaName,
                               NutsCodeType        NutsCodeType,
                               NutsCode            NutsCode,
                               NamedAreaType?      NamedAreaType            = null,
                               Country?            Country                  = null,
                               XElement?           NutsNamedAreaExtension   = null,
                               XElement?           NamedAreaExtension       = null,
                               XElement?           ANamedAreaExtension      = null)

        : NamedArea(AreaName,
                    NamedAreaType,
                    Country,
                    NamedAreaExtension,
                    ANamedAreaExtension)

    {

        #region Properties

        /// <summary>
        /// The NUTS code type for the named area.
        /// </summary>
        [XmlElement("nutsCodeType",             Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public NutsCodeType  NutsCodeType              { get; } = NutsCodeType;

        /// <summary>
        /// The NUTS code for the named area.
        /// </summary>
        [XmlElement("nutsCode",                 Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public NutsCode      NutsCode                  { get; } = NutsCode;

        /// <summary>
        /// Optional extension element for additional NutsNamedArea information.
        /// </summary>
        [XmlElement("_nutsNamedAreaExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?     NutsNamedAreaExtension    { get; } = NutsNamedAreaExtension;

        #endregion

    }

}
