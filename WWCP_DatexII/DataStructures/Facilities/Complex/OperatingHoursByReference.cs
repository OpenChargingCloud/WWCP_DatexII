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

using System.Xml.Linq;
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Operating hours information that is addressed via a reference.
    /// </summary>
    [XmlType("OperatingHoursByReference", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class OperatingHoursByReference(OperatingHoursSpecificationVersionedReference  OperatingHoursReference,
                                           OperatingHoursTableVersionedReference?         OperatingHoursTableReference         = null,
                                           XElement?                                      OperatingHoursByReferenceExtension   = null,

                                           ClosureInformation?                            ClosureInformation                   = null,
                                           XElement?                                      OperatingHoursExtension              = null)

        : AOperatingHours(ClosureInformation,
                          OperatingHoursExtension)

    {

        #region Properties

        /// <summary>
        /// The reference to an operating hours specification.
        /// </summary>
        [XmlElement("operatingHoursReference",              Namespace = "http://datex2.eu/schema/3/facilities")]
        public OperatingHoursSpecificationVersionedReference  OperatingHoursReference               { get; } = OperatingHoursReference;

        /// <summary>
        /// Operation hours table in question defined by a reference.
        /// </summary>
        [XmlElement("operatingHoursTableReference",         Namespace = "http://datex2.eu/schema/3/facilities")]
        public OperatingHoursTableVersionedReference?         OperatingHoursTableReference          { get; } = OperatingHoursTableReference;

        /// <summary>
        /// Optional extension element for additional operating hours by reference information.
        /// </summary>
        [XmlElement("_operatingHoursByReferenceExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                      OperatingHoursByReferenceExtension    { get; } = OperatingHoursByReferenceExtension;

        #endregion


        #region TryParseXML(XML, out OperatingHoursByReference, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an OperatingHoursByReference.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="OperatingHoursByReference">The parsed OperatingHoursByReference.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                             XML,
                                          [NotNullWhen(true)]  out OperatingHoursByReference?  OperatingHoursByReference,
                                          [NotNullWhen(false)] out String?                     ErrorResponse)
        {

            OperatingHoursByReference   = null;
            ErrorResponse               = null;

            // <?xml version="1.0" encoding="UTF-8"?>
            // <operatingHoursByReference xmlns     = "http://datex2.eu/schema/3/facilities" 
            //                            xmlns:com = "http://datex2.eu/schema/3/common">
            //
            //   <operatingHoursReference      id="spec123"  version="1" targetClass="fac:OperatingHoursSpecification" />
            //   <operatingHoursTableReference id="table456" version="2" targetClass="fac:OperatingHoursTable" />
            //
            //   <_operatingHoursByReferenceExtension>
            //     <!-- Additional extension details go here -->
            //   </_operatingHoursByReferenceExtension>
            //
            // </operatingHoursByReference>

            #region TryParse OperatingHoursReference         [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.Facilities + "operatingHoursReference",
                                       "operating hours reference",
                                       OperatingHoursSpecificationVersionedReference.TryParseXML,
                                       out OperatingHoursSpecificationVersionedReference? operatingHoursReference,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse OperatingHoursTableReference    [optional]

            if (XML.TryParseOptional(DatexIINS.Facilities + "operatingHoursTableReference",
                                     "operating hours table reference",
                                     OperatingHoursTableVersionedReference.TryParseXML,
                                     out OperatingHoursTableVersionedReference? operatingHoursTableReference,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ClosureInformation              [optional]

            if (XML.TryParseOptional("closureInformation",
                                     "closure information",
                                     ClosureInformation.TryParseXML,
                                     out ClosureInformation? closureInformation,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            OperatingHoursByReference = new OperatingHoursByReference(

                                            operatingHoursReference,
                                            operatingHoursTableReference,
                                            XML.Element(DatexIINS.Common + "_operatingHoursByReferenceExtension"),

                                            closureInformation,
                                            XML.Element(DatexIINS.Common + "_operatingHoursExtension")

                                        );

            return true;

        }

        #endregion


    }

}
