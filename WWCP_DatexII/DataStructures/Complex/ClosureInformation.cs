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
    /// Information about temporary or permanent closure.
    /// </summary>
    [XmlType("ClosureInformation", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class ClosureInformation(Boolean?   PermananentlyClosed      = null,
                                    Boolean?   TemporarilyClosed        = null,
                                    DateTime?  ClosedFrom               = null,
                                    DateTime?  TemporarilyClosedUntil   = null)
    {

        /// <summary>
        /// Permanently closed, i.e. it is not intended to open again.
        /// </summary>
        [XmlElement("permananentlyClosed", Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?  PermananentlyClosed        { get; set; } = PermananentlyClosed;

        /// <summary>
        /// Temporarily closed for an unspecified period.
        /// </summary>
        [XmlElement("temporarilyClosed", Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?  TemporarilyClosed          { get; set; } = TemporarilyClosed;

        /// <summary>
        /// Permanently or temporarily closed from the given date on. May lie in the future - in this case the scene is not closed now.
        /// </summary>
        [XmlElement("closedFrom", Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime?  ClosedFrom                { get; set; } = ClosedFrom;

        /// <summary>
        /// Temporarily closed until the given date (i.e. closure still includes this date).
        /// </summary>
        [XmlElement("temporarilyClosedUntil", Namespace = "http://datex2.eu/schema/3/common")]
        public DateTime?  TemporarilyClosedUntil    { get; set; } = TemporarilyClosedUntil;


        ///// <summary>
        ///// Optional extension element for additional closure information.
        ///// </summary>
        //[XmlElement("_closureInformationExtension", Namespace = "http://datex2.eu/schema/3/common")]
        //public ExtensionType? ClosureInformationExtension { get; set; }

    }

}
