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
    /// From which and up to which minute of a charging session a price applies (DATEX II 3.7: aegi:TimeBasedApplicability),
    /// e.g. a blocking fee from minute 240 on.
    /// </summary>
    /// <param name="FromMinute">The minute from which the price applies.</param>
    /// <param name="ToMinute">The minute up to which the price applies.</param>
    /// <param name="TimeBasedApplicabilityExtension">An optional extension.</param>
    public class TimeBasedApplicability(UInt32?    FromMinute                        = null,
                                        UInt32?    ToMinute                          = null,
                                        XElement?  TimeBasedApplicabilityExtension   = null)
    {

        /// <summary>
        /// The minute from which the price applies.
        /// </summary>
        public UInt32?    FromMinute                         { get; } = FromMinute;

        /// <summary>
        /// The minute up to which the price applies.
        /// </summary>
        public UInt32?    ToMinute                           { get; } = ToMinute;

        /// <summary>
        /// An optional extension.
        /// </summary>
        public XElement?  TimeBasedApplicabilityExtension    { get; } = TimeBasedApplicabilityExtension;


        /// <summary>
        /// Try to parse the given XML representation of a time based applicability.
        /// </summary>
        public static Boolean TryParseXML(XElement                                          XML,
                                          [NotNullWhen(true)]  out TimeBasedApplicability?  TimeBasedApplicability,
                                          [NotNullWhen(false)] out String?                  ErrorResponse)
        {

            TimeBasedApplicability = null;

            if (!XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "fromMinute",  "from minute",  UInt32.TryParse,  out UInt32? fromMinute, out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "toMinute",    "to minute",    UInt32.TryParse,  out UInt32? toMinute,   out ErrorResponse))
            {
                return false;
            }

            TimeBasedApplicability = new TimeBasedApplicability(fromMinute, toMinute, XML.Element(DatexIINS.EnergyInfrastructure + "_timeBasedApplicabilityExtension"));
            return true;

        }

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "timeBasedApplicability",
                    FromMinute.HasValue ? new XElement(DatexIINS.EnergyInfrastructure + "fromMinute", FromMinute.Value) : null,
                    ToMinute.  HasValue ? new XElement(DatexIINS.EnergyInfrastructure + "toMinute",   ToMinute.  Value) : null,
                    TimeBasedApplicabilityExtension is not null
                        ? new XElement(DatexIINS.EnergyInfrastructure + "_timeBasedApplicabilityExtension", TimeBasedApplicabilityExtension)
                        : null);

    }


    /// <summary>
    /// From which and up to which kWh of a charging session a price applies (DATEX II 3.7: aegi:EnergyBasedApplicability).
    /// </summary>
    /// <param name="FromKWh">The kWh from which the price applies.</param>
    /// <param name="ToKWh">The kWh up to which the price applies.</param>
    /// <param name="EnergyBasedApplicabilityExtension">An optional extension.</param>
    public class EnergyBasedApplicability(UInt32?    FromKWh                             = null,
                                          UInt32?    ToKWh                               = null,
                                          XElement?  EnergyBasedApplicabilityExtension   = null)
    {

        /// <summary>
        /// The kWh from which the price applies.
        /// </summary>
        public UInt32?    FromKWh                              { get; } = FromKWh;

        /// <summary>
        /// The kWh up to which the price applies.
        /// </summary>
        public UInt32?    ToKWh                                { get; } = ToKWh;

        /// <summary>
        /// An optional extension.
        /// </summary>
        public XElement?  EnergyBasedApplicabilityExtension    { get; } = EnergyBasedApplicabilityExtension;


        /// <summary>
        /// Try to parse the given XML representation of an energy based applicability.
        /// </summary>
        public static Boolean TryParseXML(XElement                                            XML,
                                          [NotNullWhen(true)]  out EnergyBasedApplicability?  EnergyBasedApplicability,
                                          [NotNullWhen(false)] out String?                    ErrorResponse)
        {

            EnergyBasedApplicability = null;

            if (!XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "fromKWh",  "from kWh",  UInt32.TryParse,  out UInt32? fromKWh, out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "toKWh",    "to kWh",    UInt32.TryParse,  out UInt32? toKWh,   out ErrorResponse))
            {
                return false;
            }

            EnergyBasedApplicability = new EnergyBasedApplicability(fromKWh, toKWh, XML.Element(DatexIINS.EnergyInfrastructure + "_energyBasedApplicabilityExtension"));
            return true;

        }

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "energyBasedApplicability",
                    FromKWh.HasValue ? new XElement(DatexIINS.EnergyInfrastructure + "fromKWh", FromKWh.Value) : null,
                    ToKWh.  HasValue ? new XElement(DatexIINS.EnergyInfrastructure + "toKWh",   ToKWh.  Value) : null,
                    EnergyBasedApplicabilityExtension is not null
                        ? new XElement(DatexIINS.EnergyInfrastructure + "_energyBasedApplicabilityExtension", EnergyBasedApplicabilityExtension)
                        : null);

    }

}
