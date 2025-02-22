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

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A list of cable types used during the charging process.
    /// </summary>
    public enum ConnectorFormats
    {

        /// <summary>
        /// The connector is an attached cable; the EV user's car needs to have a fitting inlet for a mode 2 cable, common for most domestic sockets.
        /// </summary>
        [XmlEnum("cableMode2")]
        CableMode2,

        /// <summary>
        /// The connector is an attached cable; the EV user's car needs to have a fitting inlet for a mode 3 cable, can be used for Type 1 and Type 2 sockets.
        /// </summary>
        [XmlEnum("cableMode3")]
        CableMode3,

        /// <summary>
        /// The connector is an attached cable; the EV user's car needs to have a fitting inlet.
        /// </summary>
        [XmlEnum("otherCable")]
        OtherCable,

        /// <summary>
        /// The connector is a socket; the EV user needs to bring a fitting plug.
        /// </summary>
        [XmlEnum("socket")]
        Socket,

        [XmlEnum("_extended")]
        Extended

    }

}
