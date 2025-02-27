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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A table of sites where vehicles can be supplied with energy.
    /// </summary>
    [XmlType("EnergyInfrastructureTable", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyInfrastructureTable(String                                 Id,
                                           String                                 Version,
                                           IEnumerable<EnergyInfrastructureSite>  EnergyInfrastructureSites,
                                           String?                                TableName                            = null,
                                           XElement?                              EnergyInfrastructureTableExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Unique identifier for the table.
        /// </summary>
        [XmlAttribute("id")]
        public String                                 Id                                    { get; } = Id;

        /// <summary>
        /// Version of the table.
        /// </summary>
        [XmlAttribute("version")]
        public String                                 Version                               { get; } = Version;

        /// <summary>
        /// A collection of EnergyInfrastructureSite instances.
        /// </summary>
        [XmlElement("energyInfrastructureSite",             Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyInfrastructureSite>  EnergyInfrastructureSites             { get; } = EnergyInfrastructureSites.Distinct();

        /// <summary>
        /// The name of the Energy Infrastructure Table.
        /// </summary>
        [XmlElement("tableName",                            Namespace = "http://datex2.eu/schema/3/common")]
        public String?                                TableName                             { get; } = TableName;

        /// <summary>
        /// Optional extension element for additional EnergyInfrastructureTable information.
        /// </summary>
        [XmlElement("_energyInfrastructureTableExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                              EnergyInfrastructureTableExtension    { get; } = EnergyInfrastructureTableExtension;

        #endregion


        #region TryParseXML(XML, out EnergyInfrastructureTable, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an EnergyInfrastructureTable.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyInfrastructureTable">The parsed EnergyInfrastructureTable.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                             XML,
                                          [NotNullWhen(true)]  out EnergyInfrastructureTable?  EnergyInfrastructureTable,
                                          [NotNullWhen(false)] out String?                     ErrorResponse)
        {

            EnergyInfrastructureTable  = null;
            ErrorResponse              = null;

            return true;

        }

        #endregion

    }

}
