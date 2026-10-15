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
using System.Xml.Linq;
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// One type of supplemental facility which can be supplemental equipment or a supplemental service facility.
    /// </summary>
    [XmlType("SupplementalFacility", Namespace = "http://datex2.eu/schema/3/afirFacilities")]
    public abstract class ASupplementalFacility(String                                Id,
                                                String                                Version,

                                                MultilingualString?                   Name                            = null,
                                                IEnumerable<MultilingualString>?      Alias                           = null,
                                                IEnumerable<ExternalIdentifier>?       ExternalIdentifier              = null,
                                                DateTimeOffset?                       LastUpdated                     = null,
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
        [XmlElement("availability",                    Namespace = "http://datex2.eu/schema/3/afirFacilities")]
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
        [XmlElement("applicableForUser",               Namespace = "http://datex2.eu/schema/3/afirFacilities")]
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


        #region TryParseXML(XML, out SupplementalFacility, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a supplemental facility, by its xsi:type.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="SupplementalFacility">The parsed supplemental facility.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                         XML,
                                          [NotNullWhen(true)]  out ASupplementalFacility?  SupplementalFacility,
                                          [NotNullWhen(false)] out String?                 ErrorResponse)
        {

            SupplementalFacility = null;

            var type = XML.XSIType();

            if (type is not "SupplementalServiceFacility" and not "SupplementalEquipment")
            {
                ErrorResponse = $"Unknown supplemental facility type '{type}'!";
                return false;
            }

            if (!FacilityObjectParts.TryParseXML(XML, out var parts, out ErrorResponse))
                return false;

            if (!XML.TryParseOptionalValue (DatexIINS.Facilities + "availability",       "availability",         Facilities.Availability.TryParse,       out Availability?       availability,      out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.Facilities + "quantity",           "quantity",             UInt64.TryParse,                        out UInt64?             quantity,          out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.Facilities + "regularlyCleaned",   "regularly cleaned",    DatexIIXMLExtensions.TryParseBoolean,   out Boolean?            regularlyCleaned,  out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.Facilities + "applicableForUser",  "applicable for user",  UserType.TryParse,                      out IEnumerable<UserType> applicableForUser, out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.Facilities + "nearby",             "nearby",               DatexIIXMLExtensions.TryParseBoolean,   out Boolean?            nearby,            out ErrorResponse))
            {
                return false;
            }

            if (type == "SupplementalServiceFacility")
            {

                if (!XML.TryParseMandatoryValue(DatexIINS.Facilities + "serviceFacilityType",
                                                "service facility type",
                                                ServiceFacilityType.TryParse,
                                                out ServiceFacilityType serviceFacilityType,
                                                out ErrorResponse) ||
                    !XML.TryParseOptionalValue (DatexIINS.Facilities + "numberOfSubitems",            "number of subitems",             UInt64.TryParse, out UInt64? numberOfSubitems,           out ErrorResponse) ||
                    !XML.TryParseOptionalValue (DatexIINS.Facilities + "distanceFromOriginFacility",  "distance from origin facility",  UInt64.TryParse, out UInt64? distanceFromOriginFacility, out ErrorResponse))
                {
                    return false;
                }

                SupplementalFacility = new SupplementalServiceFacility(
                                           parts.Id,
                                           parts.Version,
                                           serviceFacilityType,
                                           parts.Name,
                                           parts.Alias,
                                           parts.ExternalIdentifier,
                                           parts.LastUpdated,
                                           parts.Description,
                                           parts.Accessibility,
                                           parts.AdditionalInformation,
                                           parts.InformationWebsites,
                                           parts.PhotoURLs,
                                           parts.Photos,
                                           parts.OperatingHours,
                                           parts.LocationReference,
                                           parts.Owner,
                                           parts.Operator,
                                           parts.Helpdesk,
                                           parts.ApplicableForVehicles,
                                           parts.Dimension,
                                           parts.Amenities,
                                           availability,
                                           quantity,
                                           regularlyCleaned,
                                           applicableForUser,
                                           nearby,
                                           numberOfSubitems,
                                           distanceFromOriginFacility,
                                           parts.FacilityObjectExtension,
                                           XML.Element(DatexIINS.Facilities + "_supplementalFacilityExtension"),
                                           XML.Element(DatexIINS.Facilities + "_supplementalServiceFacilityExtension")
                                       );

                return true;

            }

            if (!XML.TryParseMandatoryValue(DatexIINS.Facilities + "equipmentType",
                                            "equipment type",
                                            EquipmentType.TryParse,
                                            out EquipmentType equipmentType,
                                            out ErrorResponse))
            {
                return false;
            }

            SupplementalFacility = new SupplementalEquipment(
                                       parts.Id,
                                       parts.Version,
                                       equipmentType,
                                       parts.Name,
                                       parts.Alias,
                                       parts.ExternalIdentifier,
                                       parts.LastUpdated,
                                       parts.Description,
                                       parts.Accessibility,
                                       parts.AdditionalInformation,
                                       parts.InformationWebsites,
                                       parts.PhotoURLs,
                                       parts.Photos,
                                       parts.OperatingHours,
                                       parts.LocationReference,
                                       parts.Owner,
                                       parts.Operator,
                                       parts.Helpdesk,
                                       parts.ApplicableForVehicles,
                                       parts.Dimension,
                                       parts.Amenities,
                                       availability,
                                       quantity,
                                       regularlyCleaned,
                                       applicableForUser,
                                       nearby,
                                       FacilityObjectExtension:         parts.FacilityObjectExtension,
                                       SupplementalFacilityExtension:   XML.Element(DatexIINS.Facilities + "_supplementalFacilityExtension"),
                                       SupplementalEquipmentExtension:  XML.Element(DatexIINS.Facilities + "_supplementalEquipmentExtension")
                                   );

            return true;

        }

        #endregion


        #region (protected) ToXMLElements()

        /// <summary>
        /// The facility object part, followed by what a supplemental facility adds.
        /// </summary>
        protected override IEnumerable<Object?> ToXMLElements()
        {

            foreach (var element in base.ToXMLElements())
                yield return element;

            if (Availability.HasValue)
                yield return new XElement(DatexIINS.Facilities + "availability",        Availability.Value.ToString());

            if (Quantity.HasValue)
                yield return new XElement(DatexIINS.Facilities + "quantity",            Quantity.        Value);

            if (RegularlyCleaned.HasValue)
                yield return new XElement(DatexIINS.Facilities + "regularlyCleaned",    RegularlyCleaned.Value);

            foreach (var applicableForUser in ApplicableForUser)
                yield return new XElement(DatexIINS.Facilities + "applicableForUser",   applicableForUser.ToString());

            if (Nearby.HasValue)
                yield return new XElement(DatexIINS.Facilities + "nearby",              Nearby.Value);

            if (SupplementalFacilityExtension is not null)
                yield return SupplementalFacilityExtension.ToExtensionXML(DatexIINS.Facilities + "_supplementalFacilityExtension");

        }

        #endregion

        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public abstract XElement ToXML(XName? XMLName = null);

        #endregion

    }

}
