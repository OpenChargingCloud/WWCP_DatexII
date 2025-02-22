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

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using System.Xml.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A publication of static information on the infrastructure for vehicle energy supply.
    /// </summary>
    [XmlType("EnergyInfrastructureTablePublication", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyInfrastructureTablePublication(DateTime                                 PublicationTime,
                                                      InternationalIdentifier                  PublicationCreator,
                                                      Languages                                Language,

                                                      HeaderInformation?                       HeaderInformation            = null,
                                                      IEnumerable<EnergyInfrastructureTable>?  EnergyInfrastructureTables   = null)

        : APayloadPublication(PublicationTime,
                              PublicationCreator,
                              Language)

    {

        /// <summary>
        /// Management information relating to the publication.
        /// </summary>
        [XmlElement("headerInformation",          Namespace = "http://datex2.eu/schema/3/common")]
        public HeaderInformation?                      HeaderInformation                                { get; set; } = HeaderInformation;

        /// <summary>
        /// A collection of EnergyInfrastructureTable instances.
        /// </summary>
        [XmlElement("energyInfrastructureTable",  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyInfrastructureTable>  EnergyInfrastructureTables                       { get; set; } = EnergyInfrastructureTables?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional publication information.
        /// </summary>
        [XmlElement("_energyInfrastructureTablePublicationExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                               EnergyInfrastructureTablePublicationExtension    { get; set; }

    }

}
