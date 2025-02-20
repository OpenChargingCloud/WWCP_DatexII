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

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// Classification of emission according to the Euro emission classification 
    /// (based on several amendments to the 1970 Directive 70/220/EEC). 
    /// Note that vehicleType as well as fuelType are mandatory to provide to make this classification explicit.
    /// </summary>
    public enum EmissionClassificationEuro
    {

        /// <summary>
        /// Euro 5.
        /// </summary>
        [XmlEnum("euro5")]
        Euro5,

        /// <summary>
        /// Euro 5a.
        /// </summary>
        [XmlEnum("euro5a")]
        Euro5a,

        /// <summary>
        /// Euro 5b.
        /// </summary>
        [XmlEnum("euro5b")]
        Euro5b,

        /// <summary>
        /// Euro 6.
        /// </summary>
        [XmlEnum("euro6")]
        Euro6,

        /// <summary>
        /// Euro 6a.
        /// </summary>
        [XmlEnum("euro6a")]
        Euro6a,

        /// <summary>
        /// Euro 6b.
        /// </summary>
        [XmlEnum("euro6b")]
        Euro6b,

        /// <summary>
        /// Euro 6c.
        /// </summary>
        [XmlEnum("euro6c")]
        Euro6c,

        /// <summary>
        /// Euro V.
        /// </summary>
        [XmlEnum("euroV")]
        EuroV,

        /// <summary>
        /// Euro VI.
        /// </summary>
        [XmlEnum("euroVI")]
        EuroVI,

        /// <summary>
        /// Any other level.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
