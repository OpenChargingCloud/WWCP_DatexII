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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.CommonExtension
{

    /// <summary>
    /// An extension for the Emissions class to provide a comparison operator.
    /// </summary>
    [XmlType("EmissionsExtension", Namespace = "http://datex2.eu/schema/3/commonExtension")]
    public class EmissionsExtension(ComparisonOperator ComparisonOperator)
    {

        #region Properties

        /// <summary>
        /// A comparison operator for the applicable emission classifications in correspondence to the specified value.
        /// The comparison applies to the Roman- or Arabic-numbered portion (e.g. euro6 > euro5b), but not crosswise (e.g. euro6 cannot be compared to euroV).
        /// </summary>
        [XmlElement("comparisonOperator", Namespace = "http://datex2.eu/schema/3/common")]
        public ComparisonOperator  ComparisonOperator    { get; } = ComparisonOperator;

        #endregion

    }

}
