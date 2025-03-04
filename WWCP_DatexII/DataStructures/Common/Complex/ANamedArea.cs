﻿/*
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

using System.Xml.Linq;
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// An abstract hook class to hook in a model for a named area.
    /// </summary>
    [XmlType("NamedArea", Namespace = "http://datex2.eu/schema/3/common")]
    public abstract class ANamedArea(XElement? ANamedAreaExtension = null)
    {

        #region Properties

        /// <summary>
        /// Optional extension element for additional named area information.
        /// </summary>
        [XmlElement("_namedAreaExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?  ANamedAreaExtension    { get; } = ANamedAreaExtension;

        #endregion


        #region TryParseXML(XML, out ANamedArea, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an ANamedArea.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ANamedArea">The parsed ANamedArea.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                              XML,
                                          [NotNullWhen(true)]  out ANamedArea?  ANamedArea,
                                          [NotNullWhen(false)] out String?      ErrorResponse)
        {

            ANamedArea     = null;
            ErrorResponse  = null;


            return true;

        }

        #endregion


        public abstract XElement ToXML(XName? XName = null);

    }

}
