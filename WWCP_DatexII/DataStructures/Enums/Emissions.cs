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

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// Emission characteristics of vehicles.
    /// </summary>
    [XmlType("Emissions", Namespace = "http://datex2.eu/schema/3/common")]
    public class Emissions
    {

        /// <summary>
        /// The minimum Euro emission classification the vehicle(s) must comply with.
        /// Note: vehicleType and fuelType should be provided to make this explicit.
        /// </summary>
        [XmlElement("emissionClassificationEuro", Namespace = "http://datex2.eu/schema/3/common")]
        public EmissionClassificationEuro?  EmissionClassificationEuro     { get; set; }

        /// <summary>
        /// Some other (probably locally defined) values for emission classification.
        /// </summary>
        [XmlElement("emissionClassificationOther", Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<String>?         EmissionClassificationOther    { get; set; }

        /// <summary>
        /// The low emission level of a vehicle.
        /// </summary>
        [XmlElement("emissionLevel", Namespace = "http://datex2.eu/schema/3/common")]
        public LowEmissionLevels?           EmissionLevel                  { get; set; }

        ///// <summary>
        ///// Optional extension element for additional emissions information.
        ///// </summary>
        //[XmlElement("_emissionsExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public EmissionsExtensionType? EmissionsExtension { get; set; }

    }

}
