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

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// The hydrogen offered at a station (DATEX II 3.7: aegi:Hydrogen).
    /// </summary>
    /// <param name="RefuellingModes">The refuelling modes offered (at least one).</param>
    /// <param name="CumulativeCapacityPerDay">The cumulative capacity of the station in kg per day.</param>
    /// <param name="HydrogenExtension">An optional extension.</param>
    public class Hydrogen(IEnumerable<HydrogenRefuellingMode>  RefuellingModes,
                          Double                               CumulativeCapacityPerDay,
                          XElement?                            HydrogenExtension   = null)
    {

        #region Properties

        /// <summary>
        /// The refuelling modes offered.
        /// </summary>
        public IEnumerable<HydrogenRefuellingMode>  RefuellingModes             { get; } = RefuellingModes.Distinct().ToArray();

        /// <summary>
        /// The cumulative capacity of the station in kg per day.
        /// </summary>
        public Double                               CumulativeCapacityPerDay    { get; } = CumulativeCapacityPerDay;

        /// <summary>
        /// An optional extension.
        /// </summary>
        public XElement?                            HydrogenExtension           { get; } = HydrogenExtension;

        #endregion


        #region TryParseXML(XML, out Hydrogen, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of hydrogen.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="Hydrogen">The parsed hydrogen.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                            XML,
                                          [NotNullWhen(true)]  out Hydrogen?  Hydrogen,
                                          [NotNullWhen(false)] out String?    ErrorResponse)
        {

            Hydrogen = null;

            if (!XML.TryParseOptionalValues(DatexIINS.EnergyInfrastructure + "refuellingMode",            "refuelling mode",              HydrogenRefuellingMode.TryParse,      out IEnumerable<HydrogenRefuellingMode> refuellingModes, out ErrorResponse) ||
                !XML.TryParseMandatoryValue(DatexIINS.EnergyInfrastructure + "cumulativeCapacityperDay",  "cumulative capacity per day",  DatexIIXMLExtensions.TryParseDouble,  out Double                              capacity,        out ErrorResponse))
            {
                return false;
            }

            if (!refuellingModes.Any())
            {
                ErrorResponse = "The mandatory refuelling mode of the hydrogen is missing!";
                return false;
            }

            Hydrogen = new Hydrogen(
                           refuellingModes,
                           capacity,
                           XML.Element(DatexIINS.EnergyInfrastructure + "_hydrogenExtension")
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

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "hydrogen",

                   RefuellingModes.Select(mode => new XElement(DatexIINS.EnergyInfrastructure + "refuellingMode", mode.ToString())),

                   // The schema spells it with a lower case 'per'.
                   new XElement(DatexIINS.EnergyInfrastructure + "cumulativeCapacityperDay", CumulativeCapacityPerDay),

                   HydrogenExtension is not null
                       ? new XElement(DatexIINS.EnergyInfrastructure + "_hydrogenExtension", HydrogenExtension)
                       : null

               );

        #endregion

    }

}
