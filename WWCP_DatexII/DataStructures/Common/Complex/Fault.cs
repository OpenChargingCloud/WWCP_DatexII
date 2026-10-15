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

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Information about a fault relating to a specific piece of equipment or process.
    /// </summary>
    [XmlType("Fault", Namespace = "http://datex2.eu/schema/3/common")]
    public class Fault(DateTime             FaultLastUpdateTime,
                       String?              FaultIdentifier         = null,
                       MultilingualString?  FaultDescription        = null,
                       DateTime?            FaultCreationTime       = null,
                       FaultSeverity?       FaultImpactSeverity     = null,
                       FaultUrgency?        FaultUrgencyToRectify   = null,
                       String?              ManufacturerFaultCode   = null,
                       XElement?            FaultExtension          = null)

    {

        #region Properties

        /// <summary>
        /// The date and time at which the fault information as specified in this instance was last updated.
        /// </summary>
        [XmlElement("faultLastUpdateTime", Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime             FaultLastUpdateTime      { get; } = FaultLastUpdateTime;

        /// <summary>
        /// Unique identifier of the fault.
        /// </summary>
        [XmlElement("faultIdentifier", Namespace = "http://datex2.eu/schema/3/common")]
        public String?              FaultIdentifier          { get; } = FaultIdentifier;

        /// <summary>
        /// Textual description of the fault.
        /// </summary>
        [XmlElement("faultDescription", Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?  FaultDescription         { get; } = FaultDescription;

        /// <summary>
        /// The date and time at which the fault was originally recorded/reported.
        /// </summary>
        [XmlElement("faultCreationTime", Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime?            FaultCreationTime        { get; } = FaultCreationTime;

        /// <summary>
        /// The severity of the fault in terms of how it affects the usability of the equipment or the reliability of the data generated.
        /// </summary>
        [XmlElement("faultImpactSeverity", Namespace = "http://datex2.eu/schema/3/common")]
        public FaultSeverity?       FaultImpactSeverity      { get; } = FaultImpactSeverity;

        /// <summary>
        /// The urgency to rectify the fault.
        /// </summary>
        [XmlElement("faultUrgencyToRectify", Namespace = "http://datex2.eu/schema/3/common")]
        public FaultUrgency?        FaultUrgencyToRectify    { get; } = FaultUrgencyToRectify;

        /// <summary>
        /// A manufacturer specific code for the fault.
        /// </summary>
        [XmlElement("manufacturerFaultCode", Namespace = "http://datex2.eu/schema/3/common")]
        public String?              ManufacturerFaultCode    { get; } = ManufacturerFaultCode;

        /// <summary>
        /// Optional extension element for additional fault information.
        /// </summary>
        [XmlElement("_faultExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?            FaultExtension           { get; } = FaultExtension;

        #endregion


        #region TryParseXML(XML, out Fault, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a fault.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Fault">The parsed fault.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                          XML,
                                          [NotNullWhen(true)]  out Fault?   Fault,
                                          [NotNullWhen(false)] out String?  ErrorResponse)
        {

            Fault = null;

            if (!XML.TryParseMandatoryValue(DatexIINS.Common + "faultLastUpdateTime",    "fault last update time",     DatexIIXMLExtensions.TryParseTimestamp,  out DateTimeOffset       faultLastUpdateTime,   out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.Common + "faultDescription",       "fault description",          MultilingualString.TryParseXML,          out MultilingualString?  faultDescription,      out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Common + "faultCreationTime",      "fault creation time",        DatexIIXMLExtensions.TryParseTimestamp,  out DateTimeOffset?      faultCreationTime,     out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Common + "faultImpactSeverity",    "fault impact severity",      FaultSeverity.TryParse,                  out FaultSeverity?       faultImpactSeverity,   out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.Common + "faultUrgencyToRectify",  "fault urgency to rectify",   FaultUrgency.TryParse,                   out FaultUrgency?        faultUrgencyToRectify, out ErrorResponse))
            {
                return false;
            }

            Fault = new Fault(
                        faultLastUpdateTime.UtcDateTime,
                        XML.OptionalText(DatexIINS.Common + "faultIdentifier"),
                        faultDescription,
                        faultCreationTime?.UtcDateTime,
                        faultImpactSeverity,
                        faultUrgencyToRectify,
                        XML.OptionalText(DatexIINS.Common + "manufacturerFaultCode"),
                        XML.Element(DatexIINS.Common + "_faultExtension")
                    );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.Common + "fault",

                   FaultIdentifier is not null && FaultIdentifier.Length > 0
                       ? new XElement(DatexIINS.Common + "faultIdentifier",         FaultIdentifier)
                       : null,

                   FaultDescription is not null
                       ? FaultDescription.ToXML(DatexIINS.Common + "faultDescription")
                       : null,

                   FaultCreationTime.HasValue
                       ? new XElement(DatexIINS.Common + "faultCreationTime",       FaultCreationTime.Value.ToISO8601WithOffset())
                       : null,

                   new XElement(DatexIINS.Common + "faultLastUpdateTime",           FaultLastUpdateTime.ToISO8601WithOffset()),

                   FaultImpactSeverity.HasValue
                       ? new XElement(DatexIINS.Common + "faultImpactSeverity",     FaultImpactSeverity.  Value.ToString())
                       : null,

                   FaultUrgencyToRectify.HasValue
                       ? new XElement(DatexIINS.Common + "faultUrgencyToRectify",   FaultUrgencyToRectify.Value.ToString())
                       : null,

                   ManufacturerFaultCode is not null && ManufacturerFaultCode.Length > 0
                       ? new XElement(DatexIINS.Common + "manufacturerFaultCode",   ManufacturerFaultCode)
                       : null,

                   FaultExtension is not null
                       ? FaultExtension.ToExtensionXML(DatexIINS.Common + "_faultExtension")
                       : null

               );

        #endregion

    }

}
