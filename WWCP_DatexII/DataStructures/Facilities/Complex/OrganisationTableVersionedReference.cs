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
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// A versioned reference to an OrganisationTable. The targetClass attribute is fixed to "fac:OrganisationTable".
    /// </summary>
    [XmlType("_OrganisationTableVersionedReference", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class OrganisationTableVersionedReference(String  Id,
                                                     String? Version = null)

        : VersionedReference(Id, Version)

    {

        #region Properties

        /// <summary>
        /// Fixed attribute indicating the target class.
        /// </summary>
        [XmlAttribute("targetClass")]
        public String  TargetClass    { get; } = "fac:OrganisationTable";

        #endregion


        #region TryParseXML(XML, out OrganisationTableVersionedReference, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an OrganisationTableVersionedReference.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="OrganisationTableVersionedReference">The parsed OrganisationTableVersionedReference.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                                       XML,
                                          [NotNullWhen(true)]  out OrganisationTableVersionedReference?  OrganisationTableVersionedReference,
                                          [NotNullWhen(false)] out String?                               ErrorResponse)
        {

            OrganisationTableVersionedReference = null;

            #region TryParse Id             [mandatory]

            if (!XML.TryParseMandatoryTextAttribute("id",
                                                    "id",
                                                    out var id,
                                                    out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse Version        [optional]

            if (XML.TryParseOptionalTextAttribute("version",
                                                  "version",
                                                  out var version,
                                                  out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse TargetClass    [mandatory]

            if (!XML.TryParseMandatoryTextAttribute("targetClass",
                                                    "target class",
                                                    out var targetClass,
                                                    out ErrorResponse))
            {
                return false;
            }

            var nsPrefix = XML.GetPrefixOfNamespace(DatexIINS.Facilities);

            if (targetClass != $"{nsPrefix}:OrganisationTable")
            {
                ErrorResponse = $"Invalid target class '{targetClass}'!";
                return false;
            }

            #endregion


            OrganisationTableVersionedReference = new OrganisationTableVersionedReference(
                                                      id,
                                                      version
                                                  );

            return true;

        }

        #endregion

        #region ToXML()

        public XElement ToXML()
        {

            var xml = new XElement(DatexIINS.EnergyInfrastructure + "organisationTableReference",

                                new XAttribute("targetClass",   TargetClass),
                                new XAttribute("id",            Id),

                          Version.IsNotNullOrEmpty()
                              ? new XAttribute("version",       Version)
                              : null

                      );

            return xml;

        }

        #endregion


    }

}
