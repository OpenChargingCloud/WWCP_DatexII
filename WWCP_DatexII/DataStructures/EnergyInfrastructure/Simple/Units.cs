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
    /// Amount in units, which are specified by unitTypeEnum.
    /// </summary>
    [XmlType("Units", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public readonly struct Units
    {

        #region Properties

        /// <summary>
        /// Gets or sets the Units value.
        /// </summary>
        [XmlText]
        public Double  Value    { get; }

        #endregion

        #region Constructor(s)

        public Units(UInt16 Value)
        {
            this.Value = Value;
        }

        private Units(Double Value)
        {
            this.Value = Value;
        }

        #endregion

        #region (static) TryParse(Text, out Units)

        /// <summary>
        /// Try to parse the given text as a non-negative number of units (an xs:float).
        /// </summary>
        /// <param name="Text">A text representation of units.</param>
        /// <param name="Units">The parsed units.</param>
        public static Boolean TryParse(String Text, out Units Units)
        {

            if (DatexII.v3.DatexIIXMLExtensions.TryParseDouble(Text, out var value) && value >= 0)
            {
                Units = new Units(value);
                return true;
            }

            Units = default;
            return false;

        }

        #endregion


        public override readonly String ToString()
            => Value.ToString();

    }

}
