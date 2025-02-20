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

using org.GraphDefined.Vanaheimr.Hermod.HTTP;
using System.Xml.Serialization;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// Provides information about a facility object, which may represent any kind of site, building, or structure, including those which supplement a main facility.
    /// </summary>
    [XmlType("FacilityObject", Namespace = "http://datex2.eu/schema/3/facilities")]
    public abstract class AFacilityObject(String                                Id,
                                          String                                Version,

                                          MultilingualString?                   Name                    = null,
                                          IEnumerable<MultilingualString>?      Alias                   = null,
                                          String?                               ExternalIdentifier      = null,
                                          DateTime?                             LastUpdated             = null,
                                          MultilingualString?                   Description             = null,
                                          IEnumerable<Accessibilities>?         Accessibility           = null,
                                          IEnumerable<MultilingualString>?      AdditionalInformation   = null,
                                          IEnumerable<URL>?                     InformationWebsites     = null,
                                          IEnumerable<URL>?                     PhotoURLs               = null,
                                          IEnumerable<Image>?                   Photos                  = null,
                                          AOperatingHours?                      OperatingHours          = null,
                                          LocationReference?                    LocationReference       = null,
                                          AOrganisation?                        Owner                   = null,
                                          AOrganisation?                        Operator                = null,
                                          AOrganisation?                        Helpdesk                = null,
                                          IEnumerable<VehicleCharacteristics>?  ApplicableForVehicles   = null,
                                          Dimension?                            Dimension               = null,
                                          Amenities?                            Amenities               = null)
    {

        /// <summary>
        /// Unique identifier for the facility object.
        /// </summary>
        [XmlAttribute("id")]
        public String                               Id                       { get; set; } = Id;

        /// <summary>
        /// Version of the facility object.
        /// </summary>
        [XmlAttribute("version")]
        public String                               Version                  { get; set; } = Version;

        /// <summary>
        /// The name of this facility.
        /// </summary>
        [XmlElement("name",                   Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?                  Name                     { get; set; } = Name;

        /// <summary>
        /// An alternative name for this facility.
        /// </summary>
        [XmlElement("alias",                  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<MultilingualString>      Alias                    { get; set; } = Alias?.                Distinct() ?? [];

        /// <summary>
        /// An external identifier for this facility.
        /// </summary>
        [XmlElement("externalIdentifier",     Namespace = "http://datex2.eu/schema/3/common")]
        public String?                              ExternalIdentifier       { get; set; } = ExternalIdentifier;

        /// <summary>
        /// Information on the time when the information has been updated the last time.
        /// </summary>
        [XmlElement("lastUpdated",            Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime?                            LastUpdated              { get; set; } = LastUpdated;

        /// <summary>
        /// A description for this facility.
        /// </summary>
        [XmlElement("description",            Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?                  Description              { get; set; } = Description;

        /// <summary>
        /// Information on accessibility and facilities for persons with disabilities.
        /// </summary>
        [XmlElement("accessibility",          Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<Accessibilities>         Accessibility            { get; set; } = Accessibility?.        Distinct() ?? [];

        /// <summary>
        /// Some additional information.
        /// </summary>
        [XmlElement("additionalInformation",  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<MultilingualString>      AdditionalInformation    { get; set; } = AdditionalInformation?.Distinct() ?? [];

        /// <summary>
        /// URL of some internet site with additional information.
        /// </summary>
        [XmlElement("informationWebsite",     Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<URL>                     InformationWebsites      { get; set; } = InformationWebsites?.  Distinct() ?? [];

        /// <summary>
        /// Specifies a URL at which a photo of the facility or its surrounding can be found.
        /// </summary>
        [XmlElement("photoUrl",               Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<URL>                     PhotoURLs                { get; set; } = PhotoURLs?.            Distinct() ?? [];

        /// <summary>
        /// A binary photo of the facility or its surrounding.
        /// </summary>
        [XmlElement("photo",                  Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<Image>?                  Photos                   { get; set; } = Photos?.               Distinct() ?? [];

        /// <summary>
        /// Operating hours of the facility.
        /// </summary>
        [XmlElement("operatingHours",         Namespace = "http://datex2.eu/schema/3/facilities")]
        public AOperatingHours?                     OperatingHours           { get; set; } = OperatingHours;

        /// <summary>
        /// Location reference of the facility.
        /// </summary>
        [XmlElement("locationReference",      Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public LocationReference?                   LocationReference        { get; set; } = LocationReference;

        /// <summary>
        /// The owner of this facility.
        /// </summary>
        [XmlElement("owner",                  Namespace = "http://datex2.eu/schema/3/facilities")]
        public AOrganisation?                       Owner                    { get; set; } = Owner;

        /// <summary>
        /// The operator of this facility.
        /// </summary>
        [XmlElement("operator",               Namespace = "http://datex2.eu/schema/3/facilities")]
        public AOrganisation?                       Operator                 { get; set; } = Operator;

        /// <summary>
        /// A helpdesk service for the facility.
        /// </summary>
        [XmlElement("helpdesk",               Namespace = "http://datex2.eu/schema/3/facilities")]
        public AOrganisation?                       Helpdesk                 { get; set; } = Helpdesk;

        /// <summary>
        /// A set of vehicles, for which this facility is appropriate, specified by its characteristics.
        /// </summary>
        [XmlElement("applicableForVehicles",  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<VehicleCharacteristics>  ApplicableForVehicles    { get; set; } = ApplicableForVehicles?.Distinct() ?? [];

        /// <summary>
        /// Dimensions of the facility.
        /// </summary>
        [XmlElement("dimension",              Namespace = "http://datex2.eu/schema/3/facilities")]
        public Dimension?                           Dimension                { get; set; } = Dimension;

        /// <summary>
        /// Amenities available at the facility.
        /// </summary>
        [XmlElement("amenities",              Namespace = "http://datex2.eu/schema/3/facilities")]
        public Amenities?                           Amenities                { get; set; } = Amenities;

        ///// <summary>
        ///// Extension element for additional content.
        ///// </summary>
        //[XmlElement("_facilityObjectExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? FacilityObjectExtension { get; set; }

    }

}
