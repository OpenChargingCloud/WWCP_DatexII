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

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// An integer number representing an angle in whole degrees between 0 and 359.
    /// </summary>
    [XmlType("AngleInDegrees", Namespace = "http://datex2.eu/schema/3/common")]
    public readonly struct AngleInDegrees
    {

        #region Properties

        /// <summary>
        /// Gets or sets the AngleInDegrees value.
        /// </summary>
        [XmlText]
        public UInt16 Value { get; }

        #endregion

        #region Constructor(s)

        public AngleInDegrees(UInt16 Value)
        {

            if (Value < 0 || Value > 359)
                throw new ArgumentException("Angle must be between 0 and 359 degrees.", nameof(Value));

            this.Value = Value;

        }

        #endregion


        //public static implicit operator string(AngleInDegrees tz) => tz._value;
        //public static implicit operator AngleInDegrees(string s) => new AngleInDegrees(s);

        public override readonly String ToString()
            => Value.ToString();

    }

}
