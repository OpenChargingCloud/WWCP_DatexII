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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Organisation information that is based on a reference.
    /// </summary>
    [XmlType("OrganisationByReference", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class OrganisationByReference(OrganisationSpecificationVersionedReference  OrganisationReference,
                                         OrganisationTableVersionedReference?         OrganisationTableReference   = null) : AOrganisation
    {

        /// <summary>
        /// Organisation information provided by a reference.
        /// </summary>
        [XmlElement("organisationReference",              Namespace = "http://datex2.eu/schema/3/facilities")]
        public OrganisationSpecificationVersionedReference  OrganisationReference               { get; set; } = OrganisationReference;


        /// <summary>
        /// Organisation table in question defined by a reference.
        /// </summary>
        [XmlElement("organisationTableReference",         Namespace = "http://datex2.eu/schema/3/facilities")]
        public OrganisationTableVersionedReference?         OrganisationTableReference          { get; set; } = OrganisationTableReference;


        /// <summary>
        /// Optional extension element for additional organisation-by-reference information.
        /// </summary>
        [XmlElement("_organisationByReferenceExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                    OrganisationByReferenceExtension    { get; set; }

    }

}
