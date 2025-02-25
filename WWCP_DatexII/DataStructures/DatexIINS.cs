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

using System.Xml.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3
{

    /// <summary>
    /// DatexII Namespaces.
    /// </summary>
    public static class DatexIINS
    {

        /// <summary>
        /// DatexII http://www.w3.org/2001/XMLSchema-instance
        /// </summary>
        public static readonly XNamespace XSI                   = "http://www.w3.org/2001/XMLSchema-instance";


        /// <summary>
        /// DatexII D2 Payload
        /// http://datex2.eu/schema/3/d2Payload
        /// </summary>
        public static readonly XNamespace D2Payload             = "http://datex2.eu/schema/3/d2Payload";

        /// <summary>
        /// DatexII Common
        /// http://datex2.eu/schema/3/common
        /// </summary>
        public static readonly XNamespace Common                = "http://datex2.eu/schema/3/common";

        /// <summary>
        /// DatexII Location Extension
        /// http://datex2.eu/schema/3/locationExtension
        /// </summary>
        public static readonly XNamespace LocationExtension     = "http://datex2.eu/schema/3/locationExtension";

        /// <summary>
        /// DatexII Location Referencing
        /// http://datex2.eu/schema/3/locationReferencing
        /// </summary>
        public static readonly XNamespace LocationReferencing   = "http://datex2.eu/schema/3/locationReferencing";

        /// <summary>
        /// DatexII Facilities
        /// http://datex2.eu/schema/3/facilities
        /// </summary>
        public static readonly XNamespace Facilities            = "http://datex2.eu/schema/3/facilities";

        /// <summary>
        /// DatexII Parking
        /// http://datex2.eu/schema/3/parking
        /// </summary>
        public static readonly XNamespace Parking               = "http://datex2.eu/schema/3/parking";

        /// <summary>
        /// DatexII Energy Infrastructure
        /// http://datex2.eu/schema/3/energyInfrastructure
        /// </summary>
        public static readonly XNamespace EnergyInfrastructure  = "http://datex2.eu/schema/3/energyInfrastructure";

    }

}
