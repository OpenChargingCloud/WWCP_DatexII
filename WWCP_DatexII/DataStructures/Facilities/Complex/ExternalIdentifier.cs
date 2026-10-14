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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// An identifier of a facility object or an organisation in another system (DATEX II 3.7), e.g. an EVSE
    /// identification, the identification of a charging station at the Bundesnetzagentur, or an operator identification.
    /// </summary>
    /// <remarks>
    /// DATEX II 3.7 knows only the type "evseId", as an extended value of the type of identifier. Every other type is an
    /// "otherTypeOfIdentifier"; the Mobilithek profile writes stationIdBNetzA, operatorIdBNetzA, operatorId and providerId
    /// as extended values of the type, which its JSON dialect does (see DatexIIJSON).
    /// </remarks>
    /// <param name="Identifier">The identifier.</param>
    /// <param name="TypeOfIdentifier">The type of the identifier, as DATEX II 3.7 knows it.</param>
    /// <param name="OtherTypeOfIdentifier">Another type of the identifier.</param>
    /// <param name="ExternalIdentifierExtension">An optional extension.</param>
    public class ExternalIdentifier(String             Identifier,
                                    TypeOfIdentifier?  TypeOfIdentifier             = null,
                                    String?            OtherTypeOfIdentifier        = null,
                                    XElement?          ExternalIdentifierExtension  = null)
    {

        #region Properties

        /// <summary>
        /// The identifier.
        /// </summary>
        public String             Identifier                     { get; } = Identifier;

        /// <summary>
        /// The type of the identifier, as DATEX II 3.7 knows it: evseId.
        /// </summary>
        public TypeOfIdentifier?  TypeOfIdentifier               { get; } = TypeOfIdentifier;

        /// <summary>
        /// Another type of the identifier, e.g. operatorId.
        /// </summary>
        public String?            OtherTypeOfIdentifier          { get; } = OtherTypeOfIdentifier;

        /// <summary>
        /// An optional extension.
        /// </summary>
        public XElement?          ExternalIdentifierExtension    { get; } = ExternalIdentifierExtension;

        /// <summary>
        /// The type of the identifier, whichever way it is told.
        /// </summary>
        public TypeOfIdentifier?  Type

            => TypeOfIdentifier ??
               (OtherTypeOfIdentifier is not null && Facilities.TypeOfIdentifier.TryParse(OtherTypeOfIdentifier, out var type) ? type : null);

        #endregion


        #region (static) Of(Identifier, Type)

        /// <summary>
        /// An identifier of the given type, told as DATEX II 3.7 allows: evseId as the type of the identifier, every other
        /// type as another type.
        /// </summary>
        /// <param name="Identifier">The identifier.</param>
        /// <param name="Type">The type of the identifier.</param>
        public static ExternalIdentifier Of(String            Identifier,
                                            TypeOfIdentifier  Type)

            => Type == Facilities.TypeOfIdentifier.EVSEId
                   ? new ExternalIdentifier(Identifier, Type)
                   : new ExternalIdentifier(Identifier, OtherTypeOfIdentifier: Type.ToString());

        #endregion


        #region TryParseXML(XML, out ExternalIdentifier, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an external identifier.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ExternalIdentifier">The parsed external identifier.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                      XML,
                                          [NotNullWhen(true)]  out ExternalIdentifier?  ExternalIdentifier,
                                          [NotNullWhen(false)] out String?              ErrorResponse)
        {

            ExternalIdentifier = null;

            var identifier = XML.Element(DatexIINS.Facilities + "identifier")?.Value.Trim();

            if (identifier is null || identifier.Length == 0)
            {
                ErrorResponse = "The mandatory identifier of the external identifier is missing!";
                return false;
            }

            if (!XML.TryParseOptionalValue(DatexIINS.Facilities + "typeOfIdentifier",
                                           "type of identifier",
                                           Facilities.TypeOfIdentifier.TryParse,
                                           out TypeOfIdentifier? typeOfIdentifier,
                                           out ErrorResponse))
            {
                return false;
            }

            ExternalIdentifier = new ExternalIdentifier(
                                     identifier,
                                     typeOfIdentifier,
                                     XML.OptionalText(DatexIINS.Facilities + "otherTypeOfIdentifier"),
                                     XML.Element(DatexIINS.Facilities + "_externalIdentifierExtension")
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

            => new (XMLName ?? DatexIINS.Facilities + "externalIdentifier",

                   new XElement(DatexIINS.Facilities + "identifier",                Identifier),

                   TypeOfIdentifier.HasValue
                       ? new XElement(DatexIINS.Facilities + "typeOfIdentifier",   TypeOfIdentifier.Value.ToString())
                       : null,

                   OtherTypeOfIdentifier is not null
                       ? new XElement(DatexIINS.Facilities + "otherTypeOfIdentifier", OtherTypeOfIdentifier)
                       : null,

                   ExternalIdentifierExtension is not null
                       ? new XElement(DatexIINS.Facilities + "_externalIdentifierExtension", ExternalIdentifierExtension)
                       : null

               );

        #endregion

    }

}
