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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Open or available all the time (24/7).
    /// </summary>
    [XmlType("OpenAllHours", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class OpenAllHours(XElement?            OpenAllHoursExtension     = null,
                              ClosureInformation?  ClosureInformation        = null,
                              XElement?            OperatingHoursExtension   = null)

        : AOperatingHours(ClosureInformation,
                          OperatingHoursExtension)

    {

        #region Properties

        /// <summary>
        /// Optional extension element for additional open-all-hours information.
        /// </summary>
        [XmlElement("_openAllHoursExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  OpenAllHoursExtension    { get; } = OpenAllHoursExtension;

        #endregion


        #region TryParseXML(XML, out OpenAllHours, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an OpenAllHours.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="OpenAllHours">The parsed OpenAllHours.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                 XML,
                                          [NotNullWhen(true)]   out OpenAllHours?  OpenAllHours,
                                          [NotNullWhen(false)]  out String?        ErrorResponse)
        {

            OpenAllHours = null;


            #region TryParse ClosureInformation    [optional]

            if (XML.TryParseOptional("closureInformation",
                                     "closure information",
                                     ClosureInformation.TryParseXML,
                                     out ClosureInformation? closureInformation,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            OpenAllHours = new OpenAllHours(
                               XML.Element(DatexIINS.Common + "_openAllHoursExtension"),
                               closureInformation,
                               XML.Element(DatexIINS.Common + "_payloadPublicationExtension")
                           );

            return true;

        }

        #endregion


    }

}
