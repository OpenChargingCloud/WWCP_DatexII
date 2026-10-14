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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// What every facility object status has, as parsed: the particles of fac:FacilityObjectStatus.
    /// </summary>
    public sealed record FacilityObjectStatusParts(FacilityObjectVersionedReference  Reference,
                                                   DateTimeOffset?                   LastUpdated,
                                                   OpeningStatus?                    OpeningStatus,
                                                   OperationStatus?                  OperationStatus,
                                                   Boolean?                          RegularOperatingHoursInForce,
                                                   MultilingualString?               StatusDescription,
                                                   AOperatingHours?                  NewOperatingHours,
                                                   Fault?                            Fault,
                                                   XElement?                         FacilityObjectStatusExtension)
    {

        #region TryParseXML(XML, out Parts, out ErrorResponse)

        /// <summary>
        /// Try to parse the particles every facility object status has.
        /// </summary>
        /// <param name="XML">The XML of a facility object status.</param>
        /// <param name="Parts">The parsed particles.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                             XML,
                                          [NotNullWhen(true)]  out FacilityObjectStatusParts?  Parts,
                                          [NotNullWhen(false)] out String?                     ErrorResponse)
        {

            Parts = null;

            if (!XML.TryParseMandatoryXML (DatexIINS.Facilities + "reference",                     "reference",                        FacilityObjectVersionedReference.TryParseXML,  out FacilityObjectVersionedReference? reference,                    out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "lastUpdated",                   "last updated",                     DatexIIXMLExtensions.TryParseTimestamp,        out DateTimeOffset?                   lastUpdated,                  out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "openingStatus",                 "opening status",                   Facilities.OpeningStatus.TryParse,             out OpeningStatus?                    openingStatus,                out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "operationStatus",               "operation status",                 Facilities.OperationStatus.TryParse,           out OperationStatus?                  operationStatus,              out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Facilities + "regularOperatingHoursInForce",  "regular operating hours in force", DatexIIXMLExtensions.TryParseBoolean,          out Boolean?                          regularOperatingHoursInForce, out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.Facilities + "statusDescription",             "status description",               MultilingualString.TryParseXML,                out MultilingualString?               statusDescription,            out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.Facilities + "newOperatingHours",             "new operating hours",              AOperatingHours.TryParseXML,                   out AOperatingHours?                  newOperatingHours,            out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.Facilities + "fault",                         "fault",                            Common.Fault.TryParseXML,                      out Fault?                            fault,                        out ErrorResponse))
            {
                return false;
            }

            Parts = new FacilityObjectStatusParts(
                        reference,
                        lastUpdated,
                        openingStatus,
                        operationStatus,
                        regularOperatingHoursInForce,
                        statusDescription,
                        newOperatingHours,
                        fault,
                        XML.Element(DatexIINS.Facilities + "_facilityObjectStatusExtension")
                    );

            return true;

        }

        #endregion

    }


    /// <summary>
    /// What every facility status has, as parsed: the particles of fac:FacilityObjectStatus and fac:FacilityStatus.
    /// </summary>
    public sealed record FacilityStatusParts(FacilityObjectStatusParts                Object,
                                             IEnumerable<SupplementalFacilityStatus>  SupplementalFacilityStatus,
                                             XElement?                                FacilityStatusExtension)
    {

        #region TryParseXML(XML, out Parts, out ErrorResponse)

        /// <summary>
        /// Try to parse the particles every facility status has.
        /// </summary>
        /// <param name="XML">The XML of a facility status.</param>
        /// <param name="Parts">The parsed particles.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                       XML,
                                          [NotNullWhen(true)]  out FacilityStatusParts?  Parts,
                                          [NotNullWhen(false)] out String?               ErrorResponse)
        {

            Parts = null;

            if (!FacilityObjectStatusParts.TryParseXML(XML, out var facilityObjectStatus, out ErrorResponse))
                return false;

            if (!XML.TryParseOptionalXMLs(DatexIINS.Facilities + "supplementalFacilityStatus",
                                          "supplemental facility status",
                                          Facilities.SupplementalFacilityStatus.TryParseXML,
                                          out IEnumerable<SupplementalFacilityStatus> supplementalFacilityStatus,
                                          out ErrorResponse))
            {
                return false;
            }

            Parts = new FacilityStatusParts(
                        facilityObjectStatus,
                        supplementalFacilityStatus,
                        XML.Element(DatexIINS.Facilities + "_facilityStatusExtension")
                    );

            return true;

        }

        #endregion

    }

}
