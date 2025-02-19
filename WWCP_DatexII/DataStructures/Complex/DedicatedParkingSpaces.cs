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

    public class DedicatedParkingSpaces
    {
        [XmlAttribute("id")]
        public String?  Id { get; set; }

        [XmlAttribute("version")]
        public String?  Version { get; set; }

        [XmlElement(ElementName = "accessibility", Namespace = "http://datex2.eu/schema/3/facilities")]
        public String?  Accessibility { get; set; }

        [XmlElement(ElementName = "applicableForVehicles", Namespace = "http://datex2.eu/schema/3/facilities")]
        public ApplicableForVehicles? ApplicableForVehicles { get; set; }

        [XmlElement(ElementName = "amenities", Namespace = "http://datex2.eu/schema/3/facilities")]
        public Amenities? Amenities { get; set; }

        [XmlElement(ElementName = "numberOfSpaces", Namespace = "http://datex2.eu/schema/3/facilities")]
        public int? NumberOfSpaces { get; set; }

        [XmlElement(ElementName = "dimension", Namespace = "http://datex2.eu/schema/3/facilities")]
        public Dimension? Dimension { get; set; }

        [XmlElement(ElementName = "userSpecific", Namespace = "http://datex2.eu/schema/3/facilities")]
        public String?  UserSpecific { get; set; }

        [XmlElement(ElementName = "locationReference", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PointLocation? LocationReference { get; set; }
    }

}
