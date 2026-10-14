/*
 * Copyright (c) 2014-2026 GraphDefined GmbH <achim.friedland@graphdefined.com>
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
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// What every facility object has, as parsed: the particles of fac:FacilityObject.
    /// </summary>
    /// <remarks>
    /// associatedFacility and rates are not parsed yet.
    /// </remarks>
    public sealed record FacilityObjectParts(String                           Id,
                                             String                           Version,
                                             MultilingualString?              Name,
                                             IEnumerable<MultilingualString>  Alias,
                                             IEnumerable<ExternalIdentifier>  ExternalIdentifier,
                                             DateTimeOffset?                  LastUpdated,
                                             MultilingualString?              Description,
                                             IEnumerable<Accessibility>       Accessibility,
                                             IEnumerable<MultilingualString>  AdditionalInformation,
                                             IEnumerable<URL>                 InformationWebsites,
                                             IEnumerable<URL>                 PhotoURLs,
                                             IEnumerable<Image>               Photos,
                                             AOperatingHours?                 OperatingHours,
                                             ALocationReference?              LocationReference,
                                             AOrganisation?                   Owner,
                                             AOrganisation?                   Operator,
                                             AOrganisation?                   Helpdesk,
                                             IEnumerable<VehicleCharacteristics> ApplicableForVehicles,
                                             Dimension?                       Dimension,
                                             Amenities?                       Amenities,
                                             XElement?                        FacilityObjectExtension)
    {

        #region TryParseXML(XML, out Parts, out ErrorResponse)

        /// <summary>
        /// Try to parse the particles every facility object has.
        /// </summary>
        /// <param name="XML">The XML of a facility object.</param>
        /// <param name="Parts">The parsed particles.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                       XML,
                                          [NotNullWhen(true)]  out FacilityObjectParts?  Parts,
                                          [NotNullWhen(false)] out String?               ErrorResponse)
        {

            Parts          = null;
            ErrorResponse  = null;

            #region TryParse Id, Version                  [mandatory]

            var id       = XML.Attribute("id")?.     Value.Trim();
            var version  = XML.Attribute("version")?.Value.Trim();

            if (id is null || id.Length == 0)
            {
                ErrorResponse = "The mandatory 'id' attribute is missing!";
                return false;
            }

            if (version is null)
            {
                ErrorResponse = "The mandatory 'version' attribute is missing!";
                return false;
            }

            #endregion

            #region TryParse Name, Alias, Description     [optional]

            if (!XML.TryParseOptionalXML (DatexIINS.Facilities + "name",
                                          "name",
                                          MultilingualString.TryParseXML,
                                          out MultilingualString? name,
                                          out ErrorResponse))
            {
                return false;
            }

            if (!XML.TryParseOptionalXMLs(DatexIINS.Facilities + "alias",
                                          "alias",
                                          MultilingualString.TryParseXML,
                                          out IEnumerable<MultilingualString> aliases,
                                          out ErrorResponse))
            {
                return false;
            }

            if (!XML.TryParseOptionalXML (DatexIINS.Facilities + "description",
                                          "description",
                                          MultilingualString.TryParseXML,
                                          out MultilingualString? description,
                                          out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse LastUpdated                  [optional]

            if (!XML.TryParseOptionalValue(DatexIINS.Facilities + "lastUpdated",
                                           "last updated",
                                           DatexIIXMLExtensions.TryParseTimestamp,
                                           out DateTimeOffset? lastUpdated,
                                           out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse Accessibility                [optional]

            if (!XML.TryParseOptionalValues(DatexIINS.Facilities + "accessibility",
                                            "accessibility",
                                            Facilities.Accessibility.TryParse,
                                            out IEnumerable<Accessibility> accessibility,
                                            out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse AdditionalInformation        [optional]

            if (!XML.TryParseOptionalXMLs(DatexIINS.Facilities + "additionalInformation",
                                          "additional information",
                                          MultilingualString.TryParseXML,
                                          out IEnumerable<MultilingualString> additionalInformation,
                                          out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse InformationWebsites, PhotoURLs  [optional]

            if (!TryParseURLLinks(XML, DatexIINS.Facilities + "informationWebsite", "information website", out var informationWebsites, out ErrorResponse))
                return false;

            if (!TryParseURLLinks(XML, DatexIINS.Facilities + "photoUrl",           "photo URL",           out var photoURLs,           out ErrorResponse))
                return false;

            #endregion

            #region TryParse Photos                       [optional]

            if (!XML.TryParseOptionalXMLs(DatexIINS.Facilities + "photo",
                                          "photo",
                                          Image.TryParseXML,
                                          out IEnumerable<Image> photos,
                                          out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse OperatingHours               [optional]

            if (!XML.TryParseOptionalXML(DatexIINS.Facilities + "operatingHours",
                                         "operating hours",
                                         AOperatingHours.TryParseXML,
                                         out AOperatingHours? operatingHours,
                                         out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse LocationReference            [optional]

            if (!XML.TryParseOptionalXML(DatexIINS.Facilities + "locationReference",
                                         "location reference",
                                         ALocationReference.TryParseXML,
                                         out ALocationReference? locationReference,
                                         out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse Owner, Operator, Helpdesk    [optional]

            if (!XML.TryParseOptionalXML(DatexIINS.Facilities + "owner",
                                         "owner",
                                         AOrganisation.TryParseXML,
                                         out AOrganisation? owner,
                                         out ErrorResponse))
            {
                return false;
            }

            if (!XML.TryParseOptionalXML(DatexIINS.Facilities + "operator",
                                         "operator",
                                         AOrganisation.TryParseXML,
                                         out AOrganisation? @operator,
                                         out ErrorResponse))
            {
                return false;
            }

            if (!XML.TryParseOptionalXML(DatexIINS.Facilities + "helpdesk",
                                         "helpdesk",
                                         AOrganisation.TryParseXML,
                                         out AOrganisation? helpdesk,
                                         out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse ExternalIdentifier, ApplicableForVehicles  [optional]

            if (!XML.TryParseOptionalXMLs(DatexIINS.Facilities + "externalIdentifier",
                                          "external identifier",
                                          Facilities.ExternalIdentifier.TryParseXML,
                                          out IEnumerable<ExternalIdentifier> externalIdentifiers,
                                          out ErrorResponse) ||
                !XML.TryParseOptionalXMLs(DatexIINS.Facilities + "applicableForVehicles",
                                          "applicable for vehicles",
                                          VehicleCharacteristics.TryParseXML,
                                          out IEnumerable<VehicleCharacteristics> applicableForVehicles,
                                          out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse Dimension, Amenities         [optional]

            if (!XML.TryParseOptionalXML(DatexIINS.Facilities + "dimension",
                                         "dimension",
                                         Facilities.Dimension.TryParseXML,
                                         out Dimension? dimension,
                                         out ErrorResponse))
            {
                return false;
            }

            if (!XML.TryParseOptionalXML(DatexIINS.Facilities + "amenities",
                                         "amenities",
                                         Facilities.Amenities.TryParseXML,
                                         out Amenities? amenities,
                                         out ErrorResponse))
            {
                return false;
            }

            #endregion


            Parts = new FacilityObjectParts(
                        id,
                        version,
                        name,
                        aliases,
                        externalIdentifiers,
                        lastUpdated,
                        description,
                        accessibility,
                        additionalInformation,
                        informationWebsites,
                        photoURLs,
                        photos,
                        operatingHours,
                        locationReference,
                        owner,
                        @operator,
                        helpdesk,
                        applicableForVehicles,
                        dimension,
                        amenities,
                        XML.Element(DatexIINS.Facilities + "_facilityObjectExtension")
                    );

            return true;

        }

        #endregion

        #region (private static) TryParseURLLinks(XML, Name, Description, out URLs, out ErrorResponse)

        /// <summary>
        /// The URLs of the given com:UrlLink elements. A link without an address is invalid; one whose address is no
        /// URL, e.g. a placeholder like "https://---.com" in the published examples, is left out: it is informative only.
        /// </summary>
        private static Boolean TryParseURLLinks(XElement                          XML,
                                                XName                             Name,
                                                String                            Description,
                                                out IEnumerable<URL>              URLs,
                                                [NotNullWhen(false)] out String?  ErrorResponse)
        {

            var urls       = new List<URL>();
            URLs           = urls;
            ErrorResponse  = null;

            foreach (var link in XML.Elements(Name))
            {

                var text = link.OptionalText(DatexIINS.Common + "urlLinkAddress");

                if (text is null)
                {
                    ErrorResponse = $"The {Description} has no address!";
                    return false;
                }

                if (URL.TryParse(text, out var url))
                    urls.Add(url);

            }

            return true;

        }

        #endregion

    }


    /// <summary>
    /// What every facility has, as parsed: the particles of fac:FacilityObject and fac:Facility.
    /// </summary>
    public sealed record FacilityParts(FacilityObjectParts                  Object,
                                       IEnumerable<ASupplementalFacility>   SupplementalFacilities,
                                       IEnumerable<DedicatedParkingSpaces>  DedicatedParkingSpaces,
                                       XElement?                            FacilityExtension)
    {

        #region TryParseXML(XML, out Parts, out ErrorResponse)

        /// <summary>
        /// Try to parse the particles every facility has.
        /// </summary>
        /// <param name="XML">The XML of a facility.</param>
        /// <param name="Parts">The parsed particles.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                 XML,
                                          [NotNullWhen(true)]  out FacilityParts?  Parts,
                                          [NotNullWhen(false)] out String?         ErrorResponse)
        {

            Parts = null;

            if (!FacilityObjectParts.TryParseXML(XML, out var facilityObject, out ErrorResponse))
                return false;

            if (!XML.TryParseOptionalXMLs(DatexIINS.Facilities + "supplementalFacility",
                                          "supplemental facility",
                                          ASupplementalFacility.TryParseXML,
                                          out IEnumerable<ASupplementalFacility> supplementalFacilities,
                                          out ErrorResponse))
            {
                return false;
            }

            if (!XML.TryParseOptionalXMLs(DatexIINS.Facilities + "dedicatedParkingSpaces",
                                          "dedicated parking spaces",
                                          EnergyInfrastructure.DedicatedParkingSpaces.TryParseXML,
                                          out IEnumerable<DedicatedParkingSpaces> dedicatedParkingSpaces,
                                          out ErrorResponse))
            {
                return false;
            }

            Parts = new FacilityParts(
                        facilityObject,
                        supplementalFacilities,
                        dedicatedParkingSpaces,
                        XML.Element(DatexIINS.Facilities + "_facilityExtension")
                    );

            return true;

        }

        #endregion

    }

}
