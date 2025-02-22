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

namespace cloud.charging.open.protocols.DatexII.v3.LocationExtension
{

    /// <summary>
    /// A list of supported address line types.
    /// </summary>
    public enum AddressLineTypes
    {

        /// <summary>
        /// Element indicating a discrete element of a building forming the address.
        /// </summary>
        [XmlEnum("apartment")]
        Apartment,

        /// <summary>
        /// Element identifying the number or name and type of the edifice or construction relevant for the address.
        /// </summary>
        [XmlEnum("building")]
        Building,

        /// <summary>
        /// A postal delivery location identifier, not necessarily a physical location.
        /// </summary>
        [XmlEnum("poBox")]
        PoBox,

        /// <summary>
        /// An element representing a section of a building or organisation.
        /// </summary>
        [XmlEnum("unit")]
        Unit,

        /// <summary>
        /// Element indicating the name of the area within or adjacent to the town in which delivery address is.
        /// </summary>
        [XmlEnum("region")]
        Region,

        /// <summary>
        /// Element indicating the name of the populated place in which a delivery point is located, or near to or via which the delivery point is accessed.
        /// </summary>
        [XmlEnum("town")]
        Town,

        /// <summary>
        /// Element specifying the geographic or administrative area of the country for the address.
        /// </summary>
        [XmlEnum("districtTerritory")]
        DistrictTerritory,

        /// <summary>
        /// Element indicating the floor or level on which a delivery point is located in a multi-storey building.
        /// </summary>
        [XmlEnum("floor")]
        Floor,

        /// <summary>
        /// Element indicating road or street identifier or name.
        /// </summary>
        [XmlEnum("street")]
        Street,

        /// <summary>
        /// Element indicating a house number.
        /// </summary>
        [XmlEnum("houseNumber")]
        HouseNumber,

        /// <summary>
        /// A non-predefined text line for general purpose.
        /// </summary>
        [XmlEnum("generalTextLine")]
        GeneralTextLine,

        [XmlEnum("_extended")]
        Extended

    }

}
