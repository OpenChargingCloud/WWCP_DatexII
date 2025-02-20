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

using System.Xml.Serialization;

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// A specification of operating hours (e.g. for a parking site, a service facility, an access or the availability for equipment).
    /// </summary>
    [XmlType("OperatingHoursSpecification", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class OperatingHoursSpecification(String               Id,
                                             String               Version,
                                             OverallPeriod        OverallPeriod,

                                             DateTime?            LastUpdated          = null,
                                             String?              Label                = null,
                                             Boolean?             OperatingAllYear     = null,
                                             URL?                 URLLinkAddress       = null,

                                             ClosureInformation?  ClosureInformation   = null)

        : AOperatingHours(ClosureInformation)

    {

        /// <summary>
        /// Identifier attribute.
        /// </summary>
        [XmlAttribute("id")]
        public String         Id                  { get; set; } = Id;

        /// <summary>
        /// Version attribute.
        /// </summary>
        [XmlAttribute("version")]
        public String         Version             { get; set; } = Version;

        /// <summary>
        /// The specification of operating hours by an overall period combined with valid or exceptional periods.
        /// </summary>
        [XmlElement("overallPeriod",     Namespace = "http://datex2.eu/schema/3/common")]
        public OverallPeriod  OverallPeriod       { get; set; } = OverallPeriod;

        /// <summary>
        /// The date/time at which this information was last updated.
        /// </summary>
        [XmlElement("lastUpdated",       Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime?      LastUpdated         { get; set; } = LastUpdated;

        /// <summary>
        /// A label, name or identifier for this operating hours specification.
        /// </summary>
        [XmlElement("label",             Namespace = "http://datex2.eu/schema/3/common")]
        public String?        Label               { get; set; } = Label;

        /// <summary>
        /// Indicates that the facility or organisation is not closed on a seasonal basis.
        /// </summary>
        [XmlElement("operatingAllYear",  Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?       OperatingAllYear    { get; set; } = OperatingAllYear;

        /// <summary>
        /// A Uniform Resource Locator (URL) address pointing to a resource available on the Internet from where further relevant information may be obtained.
        /// </summary>
        [XmlElement("urlLinkAddress",    Namespace = "http://datex2.eu/schema/3/common")]
        public URL?           URLLinkAddress      { get; set; } = URLLinkAddress;

        ///// <summary>
        ///// Optional extension element for additional operating hours specification information.
        ///// </summary>
        //[XmlElement("_operatingHoursSpecificationExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? OperatingHoursSpecificationExtension { get; set; }

    }

}
