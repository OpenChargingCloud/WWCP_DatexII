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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A specification of service types for the fuelling/charging and payment process.
    /// </summary>
    [XmlType("ServiceType", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class Service(ServiceType     ServiceTypeValue,
                         OverallPeriod?  OverallPeriod          = null,
                         XElement?       ServiceTypeExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Information on different service types for the fuelling/charging and payment process.
        /// </summary>
        [XmlElement("serviceType",            Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public ServiceType     ServiceTypeValue        { get; } = ServiceTypeValue;

        /// <summary>
        /// The overall period during which the service is available.
        /// </summary>
        [XmlElement("overallPeriod",          Namespace = "http://datex2.eu/schema/3/common")]
        public OverallPeriod?  OverallPeriod           { get; } = OverallPeriod;

        /// <summary>
        /// Optional extension element for additional service type information.
        /// </summary>
        [XmlElement("_serviceTypeExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?       ServiceTypeExtension    { get; } = ServiceTypeExtension;

        #endregion


        #region TryParseXML(XML, out Service, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a service type.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Service">The parsed service type.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                           XML,
                                          [NotNullWhen(true)]  out Service?  Service,
                                          [NotNullWhen(false)] out String?   ErrorResponse)
        {

            Service = null;

            if (!XML.TryParseMandatoryValue(DatexIINS.EnergyInfrastructure + "serviceType",
                                            "service type",
                                            ServiceType.TryParse,
                                            out ServiceType serviceType,
                                            out ErrorResponse) ||
                !XML.TryParseOptionalXML   (DatexIINS.EnergyInfrastructure + "overallPeriod",
                                            "overall period",
                                            Common.OverallPeriod.TryParseXML,
                                            out OverallPeriod? overallPeriod,
                                            out ErrorResponse))
            {
                return false;
            }

            Service = new Service(
                          serviceType,
                          overallPeriod,
                          XML.Element(DatexIINS.EnergyInfrastructure + "_serviceTypeExtension")
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

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "serviceType",

                   new XElement(DatexIINS.EnergyInfrastructure + "serviceType",   ServiceTypeValue.ToString()),

                   OverallPeriod is not null
                       ? OverallPeriod.ToXML(DatexIINS.EnergyInfrastructure + "overallPeriod")
                       : null,

                   ServiceTypeExtension is not null
                       ? new XElement(DatexIINS.EnergyInfrastructure + "_serviceTypeExtension", ServiceTypeExtension)
                       : null

               );

        #endregion

    }

}
