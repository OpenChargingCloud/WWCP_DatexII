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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// The organisation for the specified association end (within the specified validity if applicable) is not defined.
    /// </summary>
    [XmlType("UndefinedOrganisation", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class UndefinedOrganisation(XElement?       UndefinedOrganisationExtension   = null,
                                       OverallPeriod?  GeneralTimeValidity              = null,
                                       XElement?       OrganisationExtension            = null)

        : AOrganisation(GeneralTimeValidity,
                        OrganisationExtension)

    {

        #region Properties

        /// <summary>
        /// Optional extension element for additional undefined organisation information.
        /// </summary>
        [XmlElement("_undefinedOrganisationExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  UndefinedOrganisationExtension    { get; } = UndefinedOrganisationExtension;

        #endregion

    }

}
