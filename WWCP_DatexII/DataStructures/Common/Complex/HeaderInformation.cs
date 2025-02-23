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
        public InformationStatus                        InformationStatus             { get; set; } = InformationStatus;

        /// <summary>
        /// The extent to which the related information may be circulated, according to the recipient type.
        /// </summary>
        [XmlElement("confidentiality",              Namespace = "http://datex2.eu/schema/3/common")]
        public Confidentiality?                         Confidentiality               { get; set; } = Confidentiality;

        /// <summary>
        /// The allowed delivery channels.
        /// </summary>
        [XmlElement("allowedDeliveryChannel",       Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<InformationDeliveryService>  AllowedDeliveryChannel        { get; set; } = AllowedDeliveryChannel?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional header information.
        /// </summary>
        [XmlElement("_headerInformationExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                HeaderInformationExtension    { get; set; } = HeaderInformationExtension;

        #endregion


        #region TryParseXML(XML, out HeaderInformation, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a HeaderInformation.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="HeaderInformation">The parsed HeaderInformation.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                XML,
                                          out HeaderInformation?  HeaderInformation,
                                          out String?             ErrorResponse)
        {

            ErrorResponse = null;


            var aa = XML.MapValueOrFail(XML_IO.nsCom + "confidentiality", Common.Confidentiality.Parse);



            Confidentiality? confidentiality = null;
            var confidentialityEl = XML.Element(XML_IO.nsCom + "confidentiality");
            if (confidentialityEl is not null)
            {
                if (Common.Confidentiality.TryParse(confidentialityEl.Value, out var cVal))
                    confidentiality = cVal;
                else
                {
                    HeaderInformation  = null;
                    ErrorResponse      = $"Invalid 'confidentiality' value '{confidentialityEl.Value}'!";
                    return false;
                }
            }


            var channels         = new List<InformationDeliveryService>();
            var allowedChannels  = XML.Elements(XML_IO.nsCom + "allowedDeliveryChannel");
            foreach (var chEl in allowedChannels)
            {
                if (InformationDeliveryService.TryParse(chEl.Value, out var channelVal))
                    channels.Add(channelVal);
                else
                {
                    HeaderInformation  = null;
                    ErrorResponse      = $"Invalid 'allowedDeliveryChannel' value '{chEl.Value}'!";
                    return false;
                }
            }


            var statusEl = XML.Element(XML_IO.nsCom + "informationStatus");
            if (statusEl is null)
            {
                HeaderInformation  = null;
                ErrorResponse      = "Missing required 'informationStatus' XML element!";
                return false;
            }

            if (!InformationStatus.TryParse(statusEl.Value, out var infoStatus))
            {
                HeaderInformation  = null;
                ErrorResponse      = $"Invalid 'informationStatus' value: '{statusEl.Value}'!";
                return false;
            }


            HeaderInformation = new HeaderInformation(
                                    infoStatus,
                                    confidentiality,
                                    channels,
                                    XML.Element(XML_IO.nsCom + "_headerInformationExtension")
                                );

            return true;

        }

        #endregion

        #region ToXML()

        public XElement ToXML()
        {

            var xml = new XElement(XML_IO.nsCom + "headerInformation",

                          Confidentiality.HasValue
                              ? new XElement(XML_IO.nsCom + "confidentiality",          Confidentiality.       ToString())
                              : null,

                          AllowedDeliveryChannel?.Select(allowedDeliveryChannel =>
                                new XElement(XML_IO.nsCom + "allowedDeliveryChannel",   allowedDeliveryChannel.ToString())),

                                new XElement(XML_IO.nsCom + "informationStatus",        InformationStatus.     ToString())

                      );

            return xml;

        }

        #endregion


    }

}
