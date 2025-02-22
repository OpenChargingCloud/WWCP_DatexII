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

namespace cloud.charging.open.protocols.DatexII.v3.LocationExtension
{

    /// <summary>
    /// Type for a short numeric or alphanumeric code identifying an area.
    /// </summary>
    [XmlType("NamedAreaCode", Namespace = "http://datex2.eu/schema/3/locationExtension")]
    public readonly struct NamedAreaCode
    {

        #region Properties

        /// <summary>
        /// Gets or sets the nuts code.
        /// </summary>
        [XmlText]
        public String Value { get; }

        #endregion

        #region Constructor(s)

        public NamedAreaCode(String Value)
        {

            if (Value.Length > 8)
                throw new ArgumentException("NamedAreaCode must be 8 characters or less.", nameof(Value));

            this.Value = Value;

        }

        #endregion


        //public static implicit operator string(NamedAreaCode tz) => tz._value;
        //public static implicit operator NamedAreaCode(string s) => new NamedAreaCode(s);

        public override readonly String ToString()
            => Value;

    }

}
