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
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Organisation information that is based on a reference.
    /// </summary>
    [XmlType("OrganisationByReference", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class OrganisationByReference(OrganisationSpecificationVersionedReference  OrganisationReference,
                                         OrganisationTableVersionedReference?         OrganisationTableReference         = null,
                                         XElement?                                    OrganisationByReferenceExtension   = null,

                                         OverallPeriod?                               GeneralTimeValidity                = null,
                                         XElement?                                    OrganisationExtension              = null)


        : AOrganisation(GeneralTimeValidity,
                        OrganisationExtension)

    {

        #region Properties

        /// <summary>
        /// Organisation information provided by a reference.
        /// </summary>
        [XmlElement("organisationReference",              Namespace = "http://datex2.eu/schema/3/facilities")]
        public OrganisationSpecificationVersionedReference  OrganisationReference               { get; } = OrganisationReference;

        /// <summary>
        /// Organisation table in question defined by a reference.
        /// </summary>
        [XmlElement("organisationTableReference",         Namespace = "http://datex2.eu/schema/3/facilities")]
        public OrganisationTableVersionedReference?         OrganisationTableReference          { get; } = OrganisationTableReference;

        /// <summary>
        /// Optional extension element for additional organisation-by-reference information.
        /// </summary>
        [XmlElement("_organisationByReferenceExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                    OrganisationByReferenceExtension    { get; } = OrganisationByReferenceExtension;

        #endregion


        #region TryParseXML(XML, out OrganisationByReference, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an OrganisationByReference.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="OrganisationByReference">The parsed OrganisationByReference.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                            XML,
                                          [NotNullWhen(true)]   out OrganisationByReference?  OrganisationByReference,
                                          [NotNullWhen(false)]  out String?                   ErrorResponse)
        {

            OrganisationByReference = null;

            #region TryParse OrganisationReference         [mandatory]

            if (!XML.TryParseMandatory("organisationReference",
                                       "organisation reference",
                                       OrganisationSpecificationVersionedReference.TryParseXML,
                                       out OrganisationSpecificationVersionedReference? organisationReference,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse OrganisationTableReference    [optional]

            if (XML.TryParseOptional("organisationTableReference",
                                     "organisation table reference",
                                     OrganisationTableVersionedReference.TryParseXML,
                                     out OrganisationTableVersionedReference? organisationTableReference,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse GeneralTimeValidity           [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "generalTimeValidity",
                                     "general time validity",
                                     OverallPeriod.TryParseXML,
                                     out OverallPeriod? generalTimeValidity,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            OrganisationByReference = new OrganisationByReference(

                                              organisationReference,
                                              organisationTableReference,
                                              XML.Element(DatexIINS.Common + "_organisationByReferenceExtension"),

                                              generalTimeValidity,
                                              XML.Element(DatexIINS.Common + "_organisationExtension")

                                          );

            return true;

        }

        #endregion


    }

}
