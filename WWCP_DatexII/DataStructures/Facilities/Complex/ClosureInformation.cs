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
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Information about temporary or permanent closure.
    /// </summary>
    [XmlType("ClosureInformation", Namespace = "http://datex2.eu/schema/3/facilities")]
    public class ClosureInformation(Boolean?         PermananentlyClosed           = null,
                                    Boolean?         TemporarilyClosed             = null,
                                    DateTimeOffset?  ClosedFrom                    = null,
                                    DateTimeOffset?  TemporarilyClosedUntil        = null,
                                    XElement?        ClosureInformationExtension   = null)
    {

        #region Properties

        /// <summary>
        /// Permanently closed, i.e. it is not intended to open again.
        /// </summary>
        [XmlElement("permananentlyClosed",           Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?         PermananentlyClosed            { get; } = PermananentlyClosed;

        /// <summary>
        /// Temporarily closed for an unspecified period.
        /// </summary>
        [XmlElement("temporarilyClosed",             Namespace = "http://datex2.eu/schema/3/common")]
        public Boolean?         TemporarilyClosed              { get; } = TemporarilyClosed;

        /// <summary>
        /// Permanently or temporarily closed from the given date on. May lie in the future - in this case the scene is not closed now.
        /// </summary>
        [XmlElement("closedFrom",                    Namespace = "http://datex2.eu/schema/3/common")]
        public DateTimeOffset?  ClosedFrom                     { get; } = ClosedFrom;

        /// <summary>
        /// Temporarily closed until the given date (i.e. closure still includes this date).
        /// </summary>
        [XmlElement("temporarilyClosedUntil",        Namespace = "http://datex2.eu/schema/3/common")]
        public DateTimeOffset?  TemporarilyClosedUntil         { get; } = TemporarilyClosedUntil;

        /// <summary>
        /// Optional extension element for additional closure information.
        /// </summary>
        [XmlElement("_closureInformationExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?        ClosureInformationExtension    { get; } = ClosureInformationExtension;

        #endregion


        #region TryParseXML(XML, out ClosureInformation, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of a ClosureInformation.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ClosureInformation">The parsed ClosureInformation.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                      XML,
                                          [NotNullWhen(true)]  out ClosureInformation?  ClosureInformation,
                                          [NotNullWhen(false)] out String?              ErrorResponse)
        {

            ClosureInformation  = null;
            ErrorResponse       = null;

            #region TryParse PermananentlyClosed       [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "permananentlyClosed",
                                     "permananently closed",
                                     out Boolean? permananentlyClosed,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse TemporarilyClosed         [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "temporarilyClosed",
                                     "temporarily closed",
                                     out Boolean? temporarilyClosed,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse ClosedFrom                [optional]

            if (XML.TryParseOptionalTimestamp(DatexIINS.Common + "closedFrom",
                                              "closed from",
                                              out DateTimeOffset? closedFrom,
                                              out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse TemporarilyClosedUntil    [optional]

            if (XML.TryParseOptionalTimestamp(DatexIINS.Common + "temporarilyClosedUntil",
                                              "temporarily closed until",
                                              out DateTimeOffset? temporarilyClosedUntil,
                                              out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            ClosureInformation = new ClosureInformation(

                                     permananentlyClosed,
                                     temporarilyClosed,
                                     closedFrom,
                                     temporarilyClosedUntil,

                                     XML.Element(DatexIINS.Common + "_closureInformationExtension")

                                 );

            return true;

        }

        #endregion


    }

}
