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

using System.Globalization;
using System.Xml.Serialization;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// A monetary value expressed to two decimal places with a maximum
    /// of 8 total digits (including 2 fractional digits).
    /// </summary>
    [XmlType("AmountOfMoney", Namespace = "http://datex2.eu/schema/3/facilities")]
    public readonly struct AmountOfMoney(Decimal Value)
    {

        #region Properties

        /// <summary>
        /// Gets or sets the AmountOfMoney value.
        /// </summary>
        [XmlText]
        public Decimal  Value    { get; } = Value;

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an AmountOfMoney.
        /// </summary>
        /// <param name="Text">A text representation of an AmountOfMoney.</param>
        public static AmountOfMoney Parse(String Text)
        {

            if (TryParse(Text, out var amountOfMoney))
                return amountOfMoney;

            throw new ArgumentException($"Invalid text representation of an AmountOfMoney: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an AmountOfMoney.
        /// </summary>
        /// <param name="Text">A text representation of an AmountOfMoney.</param>
        public static AmountOfMoney? TryParse(String Text)
        {

            if (TryParse(Text, out var amountOfMoney))
                return amountOfMoney;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out AmountOfMoney)

        /// <summary>
        /// Try to parse the given text as an AmountOfMoney.
        /// </summary>
        /// <param name="Text">A text representation of an AmountOfMoney.</param>
        /// <param name="AmountOfMoney">The parsed AmountOfMoney.</param>
        public static Boolean TryParse(String Text, out AmountOfMoney AmountOfMoney)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {
                if (Decimal.TryParse(Text, out var value))
                {
                    AmountOfMoney = new AmountOfMoney(value);
                    return true;
                }
            }

            AmountOfMoney = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this AmountOfMoney.
        /// </summary>
        public AmountOfMoney Clone()

            => new (
                   Value
               );

        #endregion


        public override readonly String ToString()
            => Value.ToString("F2", CultureInfo.InvariantCulture);

    }

}
