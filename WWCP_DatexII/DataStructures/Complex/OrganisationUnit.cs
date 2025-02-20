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

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// A unit within the organisation, which has a separate location, operating hours, address and/or contacts.
    /// </summary>
    [XmlType("OrganisationUnit", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class OrganisationUnit(MultilingualString?               Name                 = null,
                                  IEnumerable<MultilingualString>?  Function             = null,
                                  LocationReference?                LocationReference    = null,
                                  IEnumerable<ContactInformation>?  ContactInformation   = null,
                                  AOperatingHours?                  OperatingHours       = null)
    {

        /// <summary>
        /// A name for this organisation unit.
        /// </summary>
        [XmlElement("name",                Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?              Name                  { get; set; } = Name;

        /// <summary>
        /// Functions this unit is responsible for or a specific type, e.g. headquarter or sales.
        /// </summary>
        [XmlElement("function",            Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<MultilingualString>  Function              { get; set; } = Function?.          Distinct() ?? [];

        /// <summary>
        /// Location reference for this organisation unit.
        /// </summary>
        [XmlElement("locationReference",   Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public LocationReference?               LocationReference     { get; set; } = LocationReference;

        /// <summary>
        /// Contact information for this organisation unit.
        /// </summary>
        [XmlElement("contactInformation",  Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<ContactInformation>  ContactInformation    { get; set; } = ContactInformation?.Distinct() ?? [];

        /// <summary>
        /// Operating hours of this organisation unit.
        /// </summary>
        [XmlElement("operatingHours",      Namespace = "http://datex2.eu/schema/3/facilities")]
        public AOperatingHours?                 OperatingHours        { get; set; } = OperatingHours;

        ///// <summary>
        ///// Optional extension element for additional organisation unit information.
        ///// </summary>
        //[XmlElement("_organisationUnitExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? OrganisationUnitExtension { get; set; }

    }

}
