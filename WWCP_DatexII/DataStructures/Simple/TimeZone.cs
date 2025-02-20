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
using System.Text.RegularExpressions;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// Identifies a time zone by specifying the difference to UTC in hours and minutes, as defined in ISO 8601.
    /// Valid formats are either "Z" or a signed offset like "+02:00" or "-05:00".
    /// </summary>
    [XmlType("TimeZone", Namespace = "http://datex2.eu/schema/3/common")]
    public readonly struct TimeZone
    {

        #region Data

        private static readonly Regex Pattern = new Regex(@"^([-+][0-9]{2}:[0-9]{2}|Z)$", RegexOptions.Compiled);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the time zone value.
        /// </summary>
        [XmlText]
        public String Value { get; }

        #endregion

        #region Constructor(s)

        public TimeZone(String Value)
        {

            if (!Pattern.IsMatch(Value))
                throw new ArgumentException("Invalid time zone format. Expected format is either 'Z' or a signed offset like +02:00 or -05:00.", nameof(Value));

            this.Value = Value;

        }

        #endregion


        //public static implicit operator string(TimeZone tz) => tz._value;
        //public static implicit operator TimeZone(string s) => new TimeZone(s);

        public override readonly String ToString()
            => Value;

    }

}
