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
    /// Management information relating to the data contained within a publication.
    /// </summary>
    [XmlType("HeaderInformation", Namespace = "http://datex2.eu/schema/3/common")]
    public class HeaderInformation(InformationStatus                         InformationStatus,
                                   Confidentiality?                          Confidentiality              = null,
                                   IEnumerable<InformationDeliveryService>?  AllowedDeliveryChannel       = null,
                                   XElement?                                 HeaderInformationExtension   = null)
    {

        #region Properties

        /// <summary>
        /// The status of the related information (real, test, exercise, etc.).
        /// </summary>
        [XmlElement("informationStatus",            Namespace = "http://datex2.eu/schema/3/common")]
        public InformationStatus                        InformationStatus             { get; } = InformationStatus;

        /// <summary>
        /// The extent to which the related information may be circulated, according to the recipient type.
        /// </summary>
        [XmlElement("confidentiality",              Namespace = "http://datex2.eu/schema/3/common")]
        public Confidentiality?                         Confidentiality               { get; } = Confidentiality;

        /// <summary>
        /// The allowed delivery channels.
        /// </summary>
        [XmlElement("allowedDeliveryChannel",       Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<InformationDeliveryService>  AllowedDeliveryChannel        { get; } = AllowedDeliveryChannel?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional header information.
        /// </summary>
        [XmlElement("_headerInformationExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                HeaderInformationExtension    { get; } = HeaderInformationExtension;

        #endregion


        #region TryParseXML(XML, out HeaderInformation, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a HeaderInformation.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="HeaderInformation">The parsed HeaderInformation.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                     XML,
                                          [NotNullWhen(true)]  out HeaderInformation?  HeaderInformation,
                                          [NotNullWhen(false)] out String?             ErrorResponse)
        {

            HeaderInformation  = null;
            ErrorResponse      = null;

            #region TryParse InformationStatus          [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.Common + "informationStatus",
                                       "information status",
                                       InformationStatus.TryParse,
                                       out InformationStatus informationStatus,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse Confidentiality            [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "confidentiality",
                                     "confidentiality",
                                     Common.Confidentiality.TryParse,
                                     out Confidentiality? confidentiality,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse AllowedDeliveryChannels    [optional]

            if (XML.TryParseOptionalElements(DatexIINS.Common + "allowedDeliveryChannel",
                                             "allowed delivery channels",
                                             InformationDeliveryService.TryParse,
                                             out IEnumerable<InformationDeliveryService> allowedDeliveryChannels,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            HeaderInformation = new HeaderInformation(
                                    informationStatus,
                                    confidentiality,
                                    allowedDeliveryChannels,
                                    XML.Element(DatexIINS.Common + "_headerInformationExtension")
                                );

            return true;

        }

        #endregion

        #region ToXML()

        public XElement ToXML()
        {

            var xml = new XElement(DatexIINS.Common + "headerInformation",

                          Confidentiality.HasValue
                              ? new XElement(DatexIINS.Common + "confidentiality",               Confidentiality.       ToString())
                              : null,

                          AllowedDeliveryChannel?.Select(allowedDeliveryChannel =>
                                new XElement(DatexIINS.Common + "allowedDeliveryChannel",        allowedDeliveryChannel.ToString())),

                                new XElement(DatexIINS.Common + "informationStatus",             InformationStatus.     ToString()),

                          HeaderInformationExtension is not null
                              ? new XElement(DatexIINS.Common + "_headerInformationExtension",   HeaderInformationExtension)
                              : null

                      );

            return xml;

        }

        #endregion


    }

}
