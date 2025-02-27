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
using System.Xml.Serialization;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// A group of (i.e. more than one) physically separate locations which have
    /// no specific order that are defined by reference to a predefined
    /// non-ordered location group.
    /// </summary>
    [XmlType("LocationGroupByReference", Namespace = "http://datex2.eu/schema/3/locationReferencing")]
    public class LocationGroupByReference(PredefinedLocationGroupVersionedReference  PredefinedLocationGroupReference,
                                          XElement?                                  LocationGroupByReferenceExtension   = null,

                                          XElement?                                  LocationGroupExtension              = null,
                                          XElement?                                  LocationReferenceExtension          = null)

        : ALocationGroup(LocationGroupExtension,
                         LocationReferenceExtension)

    {

        #region Properties

        /// <summary>
        /// A reference to a versioned instance of a predefined location group as specified in a PredefinedLocationsPublication.
        /// </summary>
        [XmlElement("predefinedLocationGroupReference",    Namespace = "http://datex2.eu/schema/3/locationReferencing")]
        public PredefinedLocationGroupVersionedReference  PredefinedLocationGroupReference     { get; } = PredefinedLocationGroupReference;

        /// <summary>
        /// Optional extension element for additional LocationGroupByReference information.
        /// </summary>
        [XmlElement("_locationGroupByReferenceExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                  LocationGroupByReferenceExtension    { get; } = LocationGroupByReferenceExtension;

        #endregion

    }

}
