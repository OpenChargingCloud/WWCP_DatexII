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

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// List of coordinates, space-separated, within the same coordinate reference system, defining a geometric entity.
    /// Modeled on DirectPositionListType in GML (EN ISO 19136), but constrained to represent a 2D or 3D polyline.
    /// </summary>
    [XmlType("GmlPosList", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public readonly struct GMLPosList
    {

        #region Data

        // Regular expression pattern: at least 4 numbers separated by spaces.
        // Pattern: "[-+]?[0-9]*\.?[0-9]+(\s[-+]?[0-9]*\.?[0-9]+){3,}"
        private static readonly Regex PatternRegex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+(\s[-+]?[0-9]*\.?[0-9]+){3,}$",
                                                               RegexOptions.Compiled);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [XmlText]
        public String Value { get; }

        #endregion

        #region Constructor(s)

        public GMLPosList(String Value)
        {

            if (!string.IsNullOrWhiteSpace(Value) && !PatternRegex.IsMatch(Value))
                throw new ArgumentException("The value does not match the required coordinate list pattern.", nameof(Value));

            this.Value = Value;

        }

        #endregion

        public override readonly String ToString()
            => Value;

    }


}
