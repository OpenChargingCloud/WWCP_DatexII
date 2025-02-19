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

    public class EnergyInfrastructureSite
    {

        [XmlAttribute("id")]
        public String?                                    Id                              { get; set; }


        [XmlAttribute("version")]
        public String?                                    Version                         { get; set; }


        [XmlElement(ElementName = "accessibility",                Namespace = "http://datex2.eu/schema/3/facilities")]
        public String?                                    Accessibility                   { get; set; }


        [XmlElement(ElementName = "additionalInformation",        Namespace = "http://datex2.eu/schema/3/facilities")]
        public AdditionalInformation?                     AdditionalInformation           { get; set; }


        [XmlElement(ElementName = "informationWebsite",           Namespace = "http://datex2.eu/schema/3/facilities")]
        public InformationWebsite?                        InformationWebsite              { get; set; }


        [XmlElement(ElementName = "operatingHours",               Namespace = "http://datex2.eu/schema/3/facilities")]
        public OpenAllHours?                              OperatingHours                  { get; set; }


        [XmlElement(ElementName = "locationReference",            Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public AreaLocation?                              LocationReference               { get; set; }


        [XmlElement(ElementName = "operator",                     Namespace = "http://datex2.eu/schema/3/facilities")]
        public OrganisationSpecification?                 Operator                        { get; set; }


        [XmlElement(ElementName = "dedicatedParkingSpaces",       Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<DedicatedParkingSpaces>?       DedicatedParkingSpaces          { get; set; }


        [XmlElement(ElementName = "typeOfSite")]
        public String?                                    TypeOfSite                      { get; set; }


        [XmlElement(ElementName = "brand")]
        public Brand?                                     Brand                           { get; set; }


        [XmlElement(ElementName = "exclusiveUsers")]
        public String?                                    ExclusiveUsers                  { get; set; }


        [XmlElement(ElementName = "serviceType")]
        public ServiceType?                               ServiceType                     { get; set; }


        [XmlElement(ElementName = "entrance",                     Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public Entrance?                                  Entrance                        { get; set; }


        [XmlElement(ElementName = "energyInfrastructureStation",  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyInfrastructureStation>?  EnergyInfrastructureStations    { get; set; }

    }

}
