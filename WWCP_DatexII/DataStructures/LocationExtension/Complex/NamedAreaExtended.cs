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

using System.Xml.Serialization;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationExtension
{

    /// <summary>
    /// A named area with an additional code (that is not an ISO subdivision code).
    /// </summary>
    [XmlType("NamedAreaExtended", Namespace = "http://datex2.eu/schema/3/locationExtension")]
    public class NamedAreaExtended(NamedAreaCode NamedAreaCode)
    {

        #region Properties

        /// <summary>
        /// Code for the named area, such as a postal code or other administration-assigned code.
        /// </summary>
        [XmlElement("namedAreaCode", Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public NamedAreaCode  NamedAreaCode    { get; } = NamedAreaCode;

        #endregion

    }

}
