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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Provides information about an organisation including its units and contact information.
    /// </summary>
    [XmlType("Organisation", Namespace = "http://datex2.eu/schema/3/facilities")]
    public abstract class AOrganisation(OverallPeriod?  GeneralTimeValidity   = null)
    {

        /// <summary>
        /// Specification of a time validity, for which this organisation information is valid in general.
        /// This is not a specification of the operating hours as such.
        /// </summary>
        [XmlElement("generalTimeValidity", Namespace = "http://datex2.eu/schema/3/common")]
        public OverallPeriod?  GeneralTimeValidity      { get; set; } = GeneralTimeValidity;

        /// <summary>
        /// Optional extension element for additional organisation information.
        /// </summary>
        [XmlElement("_organisationExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?       OrganisationExtension    { get; set; }

    }

}
