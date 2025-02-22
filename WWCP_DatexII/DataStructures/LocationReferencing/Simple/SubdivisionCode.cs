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

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// The second part of an ISO 3166-2 country sub-division code (up to 3 characters) which
    /// may be used along with a CountryCode to make a full ISO 3166-2 subdivision code.
    /// </summary>
    [XmlType("SubdivisionCode", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public readonly struct SubdivisionCode
    {

        #region Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [XmlText]
        public String Value { get; }

        #endregion

        #region Constructor(s)

        public SubdivisionCode(String Value)
        {

            if (Value.Length > 3)
                throw new ArgumentException("SubdivisionCode must be 3 characters or less.", nameof(Value));

            this.Value = Value;

        }

        #endregion

        public override readonly String ToString()
            => Value;

    }


}
