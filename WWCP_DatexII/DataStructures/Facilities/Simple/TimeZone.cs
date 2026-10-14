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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Identifies a time zone by specifying the difference to UTC in hours and minutes, as defined in ISO 8601.
    /// Valid formats are either "Z" or a signed offset like "+02:00" or "-05:00".
    /// </summary>
    [XmlType("TimeZone", Namespace = "http://datex2.eu/schema/3/afirFacilities")]
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


        #region (static) TryParse(Text, out TimeZone)

        /// <summary>
        /// Try to parse the given text as a time zone: "Z" or a signed offset like "+02:00".
        /// </summary>
        /// <param name="Text">A text representation of a time zone.</param>
        /// <param name="TimeZone">The parsed time zone.</param>
        public static Boolean TryParse(String Text, out TimeZone TimeZone)
        {

            Text = Text.Trim();

            if (Pattern.IsMatch(Text))
            {
                TimeZone = new TimeZone(Text);
                return true;
            }

            TimeZone = default;
            return false;

        }

        #endregion

        #region (static) FromOffset(Offset)

        /// <summary>
        /// The time zone of the given offset to UTC.
        /// </summary>
        /// <param name="Offset">An offset to UTC.</param>
        public static TimeZone FromOffset(TimeSpan Offset)

            => Offset == TimeSpan.Zero
                   ? new TimeZone("Z")
                   : new TimeZone($"{(Offset < TimeSpan.Zero ? "-" : "+")}{Offset.Duration().Hours:D2}:{Offset.Duration().Minutes:D2}");

        #endregion

        #region Offset

        /// <summary>
        /// The offset of this time zone to UTC.
        /// </summary>
        public TimeSpan Offset

            => Value == "Z"
                   ? TimeSpan.Zero
                   : (Value[0] == '-' ? -1 : 1) * new TimeSpan(Int32.Parse(Value[1..3]), Int32.Parse(Value[4..6]), 0);

        #endregion


        //public static implicit operator string(TimeZone tz) => tz._value;
        //public static implicit operator TimeZone(string s) => new TimeZone(s);

        public override readonly String ToString()
            => Value;

    }

}
