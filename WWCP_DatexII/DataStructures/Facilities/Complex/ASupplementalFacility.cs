﻿/*
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

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;
using System.Xml.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// One type of supplemental facility which can be supplemental equipment or a supplemental service facility.
    /// </summary>
    [XmlType("SupplementalFacility", Namespace = "http://datex2.eu/schema/3/facilities")]
    public abstract class ASupplementalFacility(String                                Id,
                                                String                                Version,

                                                MultilingualString?                   Name                            = null,
                                                IEnumerable<MultilingualString>?      Alias                           = null,
                                                String?                               ExternalIdentifier              = null,
                                                DateTime?                             LastUpdated                     = null,
                                                MultilingualString?                   Description                     = null,
                                                IEnumerable<Accessibility>?           Accessibility                   = null,
                                                IEnumerable<MultilingualString>?      AdditionalInformation           = null,
                                                IEnumerable<URL>?                     InformationWebsites             = null,
                                                IEnumerable<URL>?                     PhotoURLs                       = null,
                                                IEnumerable<Image>?                   Photos                          = null,
                                                AOperatingHours?                      OperatingHours                  = null,
                                                ALocationReference?                   LocationReference               = null,
                                                AOrganisation?                        Owner                           = null,
                                                AOrganisation?                        Operator                        = null,
                                                AOrganisation?                        Helpdesk                        = null,
                                                IEnumerable<VehicleCharacteristics>?  ApplicableForVehicles           = null,
                                                Dimension?                            Dimension                       = null,
                                                Amenities?                            Amenities                       = null,

                                                Availability?                         Availability                    = null,
                                                UInt64?                               Quantity                        = null,
                                                Boolean?                              RegularlyCleaned                = null,
                                                IEnumerable<UserType>?                ApplicableForUser               = null,
                                                Boolean?                              Nearby                          = null,

                                                XElement?                             FacilityObjectExtension         = null,
                                                XElement?                             SupplementalFacilityExtension   = null)

        : AFacilityObject(Id,
                          Version,

                          Name,
                          Alias,
                          ExternalIdentifier,
                          LastUpdated,
                          Description,
                          Accessibility,
                          AdditionalInformation,
                          InformationWebsites,
                          PhotoURLs,
                          Photos,
                          OperatingHours,
                          LocationReference,
                          Owner,
                          Operator,
                          Helpdesk,
                          ApplicableForVehicles,
                          Dimension,
                          Amenities,

                          FacilityObjectExtension)

    {

        #region Properties

        /// <summary>
        /// Specifies whether the supplemental facility is available or not.
        /// Note that this is no dynamic information!
        /// </summary>
        [XmlElement("availability",                    Namespace = "http://datex2.eu/schema/3/facilities")]
        public Availability?          Availability                     { get; } = Availability;

        /// <summary>
        /// Number of the supplemental facility (e.g. number of toilets, restaurants, park &amp; ride places, etc.)
        /// with respect to given restrictions. Dynamic overridable.
        /// </summary>
        [XmlElement("quantity",                        Namespace = "http://datex2.eu/schema/3/common")]
        public UInt64?                Quantity                         { get; } = Quantity;

        /// <summary>
        /// Indicates whether the supplemental facility is cleaned on a regular basis.
        /// </summary>
        [XmlElement("regularlyCleaned",                Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?               RegularlyCleaned                 { get; } = RegularlyCleaned;

        /// <summary>
        /// Limitation to a set of special users.
        /// </summary>
        [XmlElement("applicableForUser",               Namespace = "http://datex2.eu/schema/3/facilities")]
        public IEnumerable<UserType>  ApplicableForUser                { get; } = ApplicableForUser?.Distinct() ?? [];

        /// <summary>
        /// If true, the Supplemental Facility is not located on the site of the original facility but nearby.
        /// They do not necessarily belong together.
        /// </summary>
        [XmlElement("nearby",                          Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?               Nearby                           { get; } = Nearby;

        /// <summary>
        /// Optional extension element for additional information.
        /// </summary>
        [XmlElement("_supplementalFacilityExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?              SupplementalFacilityExtension    { get; } = SupplementalFacilityExtension;

        #endregion

    }

}
