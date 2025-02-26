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
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// A specification of operating hours (e.g. for a parking site, a service facility, an access or the availability for equipment).
    /// </summary>
    [XmlType("OperatingHoursSpecification", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class OperatingHoursSpecification(String               Id,
                                             String               Version,
                                             OverallPeriod        OverallPeriod,

                                             DateTimeOffset?      LastUpdated                            = null,
                                             String?              Label                                  = null,
                                             Boolean?             OperatingAllYear                       = null,
                                             URL?                 URLLinkAddress                         = null,
                                             XElement?            OperatingHoursSpecificationExtension   = null,

                                             ClosureInformation?  ClosureInformation                     = null,
                                             XElement?            OperatingHoursExtension                = null)

        : AOperatingHours(ClosureInformation,
                          OperatingHoursExtension)

    {

        #region Properties

        /// <summary>
        /// Identifier attribute.
        /// </summary>
        [XmlAttribute("id")]
        public String           Id                                      { get; } = Id;

        /// <summary>
        /// Version attribute.
        /// </summary>
        [XmlAttribute("version")]
        public String           Version                                 { get; } = Version;

        /// <summary>
        /// The specification of operating hours by an overall period combined with valid or exceptional periods.
        /// </summary>
        [XmlElement("overallPeriod",                          Namespace = "http://datex2.eu/schema/3/common")]
        public OverallPeriod    OverallPeriod                           { get; } = OverallPeriod;

        /// <summary>
        /// The date/time at which this information was last updated.
        /// </summary>
        [XmlElement("lastUpdated",                            Namespace = "http://datex2.eu/schema/3/common")]
        public DateTimeOffset?  LastUpdated                             { get; } = LastUpdated;

        /// <summary>
        /// A label, name or identifier for this operating hours specification.
        /// </summary>
        [XmlElement("label",                                  Namespace = "http://datex2.eu/schema/3/common")]
        public String?          Label                                   { get; } = Label;

        /// <summary>
        /// Indicates that the facility or organisation is not closed on a seasonal basis.
        /// </summary>
        [XmlElement("operatingAllYear",                       Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?         OperatingAllYear                        { get; } = OperatingAllYear;

        /// <summary>
        /// A Uniform Resource Locator (URL) address pointing to a resource available on the Internet from where further relevant information may be obtained.
        /// </summary>
        [XmlElement("urlLinkAddress",                         Namespace = "http://datex2.eu/schema/3/common")]
        public URL?             URLLinkAddress                          { get; } = URLLinkAddress;

        /// <summary>
        /// Optional extension element for additional operating hours specification information.
        /// </summary>
        [XmlElement("_operatingHoursSpecificationExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?        OperatingHoursSpecificationExtension    { get; } = OperatingHoursSpecificationExtension;

        #endregion


        #region TryParseXML(XML, out OperatingHoursSpecification, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an OperatingHoursSpecification.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="OperatingHoursSpecification">The parsed OperatingHoursSpecification.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                                XML,
                                          [NotNullWhen(true)]   out OperatingHoursSpecification?  OperatingHoursSpecification,
                                          [NotNullWhen(false)]  out String?                       ErrorResponse)
        {

            OperatingHoursSpecification = null;


            #region TryParse Id                 [mandatory]

            if (!XML.TryParseMandatoryText("id",
                                           "id",
                                           out String? id,
                                           out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse Version            [mandatory]

            if (!XML.TryParseMandatoryText("version",
                                           "version",
                                           out String? version,
                                           out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse OverallPeriod      [mandatory]

            if (!XML.TryParseMandatory("overallPeriod",
                                       "overall period",
                                       OverallPeriod.TryParseXML,
                                       out OverallPeriod? overallPeriod,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion


            #region TryParse LastUpdated         [optional]

            if (XML.TryParseOptionalTimestamp("lastUpdated",
                                              "last updated",
                                              out DateTimeOffset? lastUpdated,
                                              out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse Label               [optional]

            if (XML.TryParseOptionalText("label",
                                         "label",
                                         out String? label,
                                         out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse OperatingAllYear    [optional]

            if (XML.TryParseOptional("operatingAllYear",
                                     "operating all year",
                                     out Boolean? operatingAllYear,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse URLLinkAddress      [optional]

            if (XML.TryParseOptional("urlLinkAddress",
                                     "url link address",
                                     URL.TryParse,
                                     out URL? urlLinkAddress,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


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


            OperatingHoursSpecification = new OperatingHoursSpecification(

                                              id,
                                              version,
                                              overallPeriod,

                                              lastUpdated,
                                              label,
                                              operatingAllYear,
                                              urlLinkAddress,
                                              XML.Element(DatexIINS.Common + "_operatingHoursSpecificationExtension"),

                                              closureInformation,
                                              XML.Element(DatexIINS.Common + "_operatingHoursExtension")

                                          );

            return true;

        }

        #endregion


    }

}
