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
    /// The organisation for the specified association end (within the specified validity if applicable) is unknown.
    /// </summary>
    [XmlType("UnknownOrganisation", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class UnknownOrganisation(XElement?       UnknownOrganisationExtension   = null,
                                     OverallPeriod?  GeneralTimeValidity            = null,
                                     XElement?       OrganisationExtension          = null)

        : AOrganisation(GeneralTimeValidity,
                        OrganisationExtension)

    {

        #region Properties

        /// <summary>
        /// Optional extension element for additional unknown organisation information.
        /// </summary>
        [XmlElement("_unknownOrganisationExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  UnknownOrganisationExtension    { get; } = UnknownOrganisationExtension;

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public override XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.Facilities + "organisation",

                   new XAttribute(DatexIINS.XSI + "type",   "fac:UnknownOrganisation"),

                   ToXMLElements(),

                   UnknownOrganisationExtension is not null
                       ? new XElement(DatexIINS.Facilities + "_unknownOrganisationExtension", UnknownOrganisationExtension)
                       : null

               );

        #endregion

    }

}
