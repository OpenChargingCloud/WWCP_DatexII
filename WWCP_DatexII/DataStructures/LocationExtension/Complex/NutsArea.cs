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

using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationExtension
{

    /// <summary>
    /// The NUTS-Code representation for the area (Nomenclature of territorial units for statistics)
    /// or its LAU code representation (Local Administrative Unit).
    /// </summary>
    [XmlType("NutsArea", Namespace = "http://datex2.eu/schema/3/locationExtension")]
    public class NutsArea(NutsCode      NutsCode,
                          NutsCodeType  NutsCodeType,
                          XElement?     NutsAreaExtension   = null)
    {

        #region Properties

        /// <summary>
        /// The NUTS code for the named area.
        /// </summary>
        [XmlElement("nutsCode",            Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public NutsCode      NutsCode             { get; } = NutsCode;

        /// <summary>
        /// The NUTS code type for the named area.
        /// </summary>
        [XmlElement("nutsCodeType",        Namespace = "http://datex2.eu/schema/3/locationExtension")]
        public NutsCodeType  NutsCodeType         { get; } = NutsCodeType;

        /// <summary>
        /// Optional extension element for additional NutsArea information.
        /// </summary>
        [XmlElement("_nutsAreaExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?     NutsAreaExtension    { get; } = NutsAreaExtension;

        #endregion


        #region TryParseXML(XML, Namespace, out NutsArea, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a NUTS area.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Namespace">The namespace of its elements: afac in DATEX II 3.7, locx before.</param>
        /// <param name="NutsArea">The parsed NUTS area.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                            XML,
                                          XNamespace                          Namespace,
                                          [NotNullWhen(true)]  out NutsArea?  NutsArea,
                                          [NotNullWhen(false)] out String?    ErrorResponse)
        {

            NutsArea = null;

            if (!XML.TryParseMandatoryValue(Namespace + "nutsCodeType",
                                            "NUTS code type",
                                            LocationReferencing.NutsCodeType.TryParse,
                                            out NutsCodeType nutsCodeType,
                                            out ErrorResponse))
            {
                return false;
            }

            var nutsCode = XML.OptionalText(Namespace + "nutsCode");

            if (nutsCode is null)
            {
                ErrorResponse = "The mandatory NUTS code is missing!";
                return false;
            }

            NutsArea = new NutsArea(
                           new NutsCode(nutsCode),
                           nutsCodeType,
                           XML.Element(Namespace + "_nutsAreaExtension")
                       );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null, Namespace = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        /// <param name="Namespace">The namespace of its elements: afac in DATEX II 3.7, locx before.</param>
        public XElement ToXML(XName?       XMLName    = null,
                              XNamespace?  Namespace  = null)
        {

            var ns = Namespace ?? DatexIINS.LocationExtension;

            return new (XMLName ?? ns + "nutsArea",
                        new XElement(ns + "nutsCodeType",   NutsCodeType.ToString()),
                        new XElement(ns + "nutsCode",       NutsCode.    ToString()),
                        NutsAreaExtension is not null
                            ? new XElement(ns + "_nutsAreaExtension", NutsAreaExtension)
                            : null);

        }

        #endregion

    }

}
