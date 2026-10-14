/*
 * Copyright (c) 2014-2026 GraphDefined GmbH <achim.friedland@graphdefined.com>
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
using System.Diagnostics.CodeAnalysis;

using cloud.charging.open.protocols.DatexII.v3.LocationExtension;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// The time zone, address and NUTS areas of a facility (DATEX II 3.7): the typed extension of a location,
    /// written as loc:_locationExtension/loc:afirFacilityLocation.
    /// </summary>
    /// <param name="TimeZone">The time zone, as an offset to UTC.</param>
    /// <param name="Address">The address.</param>
    /// <param name="NutsAreas">The NUTS areas, e.g. the NUTS 1 area the Mobilithek profile requires.</param>
    /// <param name="AfirFacilityLocationExtension">An optional extension.</param>
    public class AfirFacilityLocation(TimeZone?               TimeZone                        = null,
                                      Address?                Address                         = null,
                                      IEnumerable<NutsArea>?  NutsAreas                       = null,
                                      XElement?               AfirFacilityLocationExtension   = null)
    {

        #region Properties

        /// <summary>
        /// The time zone, as an offset to UTC.
        /// </summary>
        public TimeZone?              TimeZone                         { get; } = TimeZone;

        /// <summary>
        /// The address.
        /// </summary>
        public Address?               Address                          { get; } = Address;

        /// <summary>
        /// The NUTS areas.
        /// </summary>
        public IEnumerable<NutsArea>  NutsAreas                        { get; } = NutsAreas?.Distinct() ?? [];

        /// <summary>
        /// An optional extension.
        /// </summary>
        public XElement?              AfirFacilityLocationExtension    { get; } = AfirFacilityLocationExtension;

        #endregion


        #region TryParseXML(XML, out AfirFacilityLocation, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a facility location.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="AfirFacilityLocation">The parsed facility location.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                        XML,
                                          [NotNullWhen(true)]  out AfirFacilityLocation?  AfirFacilityLocation,
                                          [NotNullWhen(false)] out String?                ErrorResponse)
        {

            AfirFacilityLocation = null;

            if (!XML.TryParseOptionalValue(DatexIINS.Facilities + "timeZone",  "time zone",  Facilities.TimeZone.TryParse,                     out TimeZone?              timeZone,  out ErrorResponse) ||
                !XML.TryParseOptionalXML  (DatexIINS.Facilities + "address",   "address",    LocationExtension.Address.TryParseXML,            out Address?               address,   out ErrorResponse) ||
                !XML.TryParseOptionalXMLs (DatexIINS.Facilities + "nutsArea",  "NUTS area",  (XElement xml, [NotNullWhen(true)] out NutsArea? area, [NotNullWhen(false)] out String? error) => NutsArea.TryParseXML(xml, DatexIINS.Facilities, out area, out error),
                                                                                                                                                  out IEnumerable<NutsArea>  nutsAreas, out ErrorResponse))
            {
                return false;
            }

            AfirFacilityLocation = new AfirFacilityLocation(
                                       timeZone,
                                       address,
                                       nutsAreas,
                                       XML.Element(DatexIINS.Facilities + "_afirFacilityLocationExtension")
                                   );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.LocationReferencing + "afirFacilityLocation",

                   TimeZone.HasValue
                       ? new XElement(DatexIINS.Facilities + "timeZone",   TimeZone.Value.ToString())
                       : null,

                   Address?.ToXML(DatexIINS.Facilities + "address"),

                   NutsAreas.Select(nutsArea => nutsArea.ToXML(DatexIINS.Facilities + "nutsArea", DatexIINS.Facilities)),

                   AfirFacilityLocationExtension is not null
                       ? new XElement(DatexIINS.Facilities + "_afirFacilityLocationExtension", AfirFacilityLocationExtension)
                       : null

               );

        #endregion

    }

}
