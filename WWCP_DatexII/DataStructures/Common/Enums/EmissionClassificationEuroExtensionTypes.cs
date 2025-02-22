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
    /// Extension type for Euro emission classification, providing additional detailed classification levels.
    /// </summary>
    public enum EmissionClassificationEuroExtensionTypes
    {

        [XmlEnum("euroUnknown")]
        EuroUnknown,

        [XmlEnum("euroI")]
        EuroI,

        [XmlEnum("euroII")]
        EuroII,

        [XmlEnum("euroIII")]
        EuroIII,

        [XmlEnum("euro0")]
        Euro0,

        [XmlEnum("euro4")]
        Euro4,

        [XmlEnum("euro6d")]
        Euro6d,

        [XmlEnum("euro6dTemp")]
        Euro6dTemp,

        [XmlEnum("euroIV")]
        EuroIV,

        [XmlEnum("euro1")]
        Euro1,

        [XmlEnum("euro2")]
        Euro2,

        [XmlEnum("euro3")]
        Euro3

    }

}
