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
    public class EnergyInfrastructureStation
    {

        [XmlAttribute("id")]
        public String?                                  Id                                        { get; set; }


        [XmlAttribute("version")]
        public String?                                  Version                                   { get; set; }


        [XmlElement(ElementName = "lastUpdated",              Namespace = "http://datex2.eu/schema/3/facilities")]
        public DateTime?                                LastUpdated                               { get; set; }


        [XmlElement(ElementName = "description",              Namespace = "http://datex2.eu/schema/3/facilities")]
        public AdditionalInformation?                   Description                               { get; set; }


        [XmlElement(ElementName = "accessibility",            Namespace = "http://datex2.eu/schema/3/facilities")]
        public String?                                  Accessibility                             { get; set; }


        [XmlElement(ElementName = "additionalInformation",    Namespace = "http://datex2.eu/schema/3/facilities")]
        public AdditionalInformation?                   AdditionalInformation                     { get; set; }


        [XmlElement(ElementName = "informationWebsite",       Namespace = "http://datex2.eu/schema/3/facilities")]
        public InformationWebsite?                      InformationWebsite                        { get; set; }


        [XmlElement(ElementName = "photoUrl",                 Namespace = "http://datex2.eu/schema/3/facilities")]
        public InformationWebsite?                      PhotoUrl                                  { get; set; }


        [XmlElement(ElementName = "operatingHours",           Namespace = "http://datex2.eu/schema/3/facilities")]
        public OpenAllHours?                            OperatingHours                            { get; set; }


        [XmlElement(ElementName = "locationReference",        Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PointLocation?                           LocationReference                         { get; set; }


        [XmlElement(ElementName = "owner",                    Namespace = "http://datex2.eu/schema/3/facilities")]
        public OrganisationSpecification?               Owner                                     { get; set; }


        [XmlElement(ElementName = "operator",                 Namespace = "http://datex2.eu/schema/3/facilities")]
        public OrganisationSpecification?               Operator                                  { get; set; }


        [XmlElement(ElementName = "helpdesk",                 Namespace = "http://datex2.eu/schema/3/facilities")]
        public OrganisationSpecification?               Helpdesk                                  { get; set; }


        [XmlElement(ElementName = "applicableForVehicles",    Namespace = "http://datex2.eu/schema/3/facilities")]
        public ApplicableForVehicles?                   ApplicableForVehicles                     { get; set; }


        [XmlElement(ElementName = "amenities",                Namespace = "http://datex2.eu/schema/3/facilities")]
        public Amenities?                               Amenities                                 { get; set; }


        [XmlElement(ElementName = "stationIdBNetzA")]
        public String?                                  StationIdBNetzA                           { get; set; }


        [XmlElement(ElementName = "totalMaximumPower")]
        public Int32?                                   TotalMaximumPower                         { get; set; }


        [XmlElement(ElementName = "authenticationAndIdentificationMethods")]
        public IEnumerable<String>?                     AuthenticationAndIdentificationMethods    { get; set; }


        [XmlElement(ElementName = "numberOfRefillPoints")]
        public Int32?                                   NumberOfRefillPoints                      { get; set; }


        [XmlElement(ElementName = "userInterfaceLanguage")]
        public String?                                  UserInterfaceLanguage                     { get; set; }


        [XmlElement(ElementName = "mobilityServiceProvider",  Namespace = "http://datex2.eu/schema/3/facilities")]
        public OrganisationSpecification?               MobilityServiceProvider                   { get; set; }


        [XmlElement(ElementName = "roamingPlatform",          Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<OrganisationSpecification>?  RoamingPlatforms                          { get; set; }


        [XmlElement(ElementName = "serviceType")]
        public ServiceType?                             ServiceType                               { get; set; }


        [XmlElement(ElementName = "refillPoint",              Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<RefillPoint>?                RefillPoints                              { get; set; }


        [XmlElement(ElementName = "electricEnergy",           Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<ElectricEnergy>?             ElectricEnergies                          { get; set; }

    }

}
