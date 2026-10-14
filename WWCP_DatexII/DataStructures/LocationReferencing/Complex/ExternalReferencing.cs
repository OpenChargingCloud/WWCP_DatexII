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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// A location defined by reference to an external/other referencing system.
    /// </summary>
    [XmlType("ExternalReferencing", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class ExternalReferencing(String     ExternalLocationCode,
                                     String     ExternalReferencingSystem,
                                     XElement?  ExternalReferencingExtension   = null)
    {

        #region Properties

        /// <summary>
        /// A code in the external referencing system which defines the location.
        /// </summary>
        [XmlElement("externalLocationCode",           Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public String     ExternalLocationCode            { get; } = ExternalLocationCode;

        /// <summary>
        /// Identification of the external/other location referencing system.
        /// </summary>
        [XmlElement("externalReferencingSystem",      Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public String     ExternalReferencingSystem       { get; } = ExternalReferencingSystem;

        /// <summary>
        /// Optional extension element for additional external referencing information.
        /// </summary>
        [XmlElement("_externalReferencingExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  ExternalReferencingExtension    { get; } = ExternalReferencingExtension;

        #endregion


        #region TryParseXML(XML, out ExternalReferencing, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an external referencing.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ExternalReferencing">The parsed external referencing.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                       XML,
                                          [NotNullWhen(true)]  out ExternalReferencing?  ExternalReferencing,
                                          [NotNullWhen(false)] out String?               ErrorResponse)
        {

            ExternalReferencing  = null;
            ErrorResponse        = null;

            var code    = XML.OptionalText(DatexIINS.LocationReferencing + "externalLocationCode");
            var system  = XML.OptionalText(DatexIINS.LocationReferencing + "externalReferencingSystem");

            if (code is null || system is null)
            {
                ErrorResponse = "The mandatory external location code or referencing system is missing!";
                return false;
            }

            ExternalReferencing = new ExternalReferencing(
                                      code,
                                      system,
                                      XML.Element(DatexIINS.LocationReferencing + "_externalReferencingExtension")
                                  );

            return true;

        }

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.LocationReferencing + "externalReferencing",

                   new XElement(DatexIINS.LocationReferencing + "externalLocationCode",        ExternalLocationCode),
                   new XElement(DatexIINS.LocationReferencing + "externalReferencingSystem",   ExternalReferencingSystem),

                   ExternalReferencingExtension is not null
                       ? new XElement(DatexIINS.LocationReferencing + "_externalReferencingExtension", ExternalReferencingExtension)
                       : null

               );

        #endregion

    }

}
