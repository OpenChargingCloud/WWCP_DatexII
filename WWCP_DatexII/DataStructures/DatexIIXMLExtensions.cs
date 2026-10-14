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
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3
{

    /// <summary>
    /// Parses a text into a value.
    /// </summary>
    public delegate Boolean TryParseTextDelegate<T>(String Text, out T Value);


    /// <summary>
    /// Parsing helpers for the DATEX II classes.
    /// </summary>
    /// <remarks>
    /// Every method returns false with an error response when an element is there but can not be parsed, and true
    /// otherwise: so "if (!XML.TryParse...(...)) return false;" is all a parser needs. Values come as nullable value
    /// types, so a missing optional value is null rather than its default.
    /// </remarks>
    public static class DatexIIXMLExtensions
    {

        #region TryParseMandatoryXML  (XML, Name, Description, Parser, out Result, out ErrorResponse)

        /// <summary>
        /// Parse the given mandatory child element with the given parser.
        /// </summary>
        public static Boolean TryParseMandatoryXML<T>(this XElement                                XML,
                                                      XName                                        Name,
                                                      String                                       Description,
                                                      TryXMLElementParser2<T>                      Parser,
                                                      [NotNullWhen(true)]  out T?                  Result,
                                                      [NotNullWhen(false)] out String?             ErrorResponse)

            where T : class

        {

            Result         = null;
            ErrorResponse  = null;

            var element = XML.Element(Name);

            if (element is null)
            {
                ErrorResponse = $"The mandatory {Description} is missing!";
                return false;
            }

            if (!Parser(element, out Result, out var errorResponse))
            {
                ErrorResponse = $"Invalid {Description}: {errorResponse}";
                return false;
            }

            return true;

        }

        #endregion

        #region TryParseOptionalXML   (XML, Name, Description, Parser, out Result, out ErrorResponse)

        /// <summary>
        /// Parse the given optional child element with the given parser.
        /// </summary>
        public static Boolean TryParseOptionalXML<T>(this XElement                     XML,
                                                     XName                             Name,
                                                     String                            Description,
                                                     TryXMLElementParser2<T>           Parser,
                                                     out T?                            Result,
                                                     [NotNullWhen(false)] out String?  ErrorResponse)

            where T : class

        {

            Result         = null;
            ErrorResponse  = null;

            var element = XML.Element(Name);

            if (element is null)
                return true;

            if (!Parser(element, out Result, out var errorResponse))
            {
                ErrorResponse = $"Invalid {Description}: {errorResponse}";
                return false;
            }

            return true;

        }

        #endregion

        #region TryParseOptionalXMLs  (XML, Name, Description, Parser, out Results, out ErrorResponse)

        /// <summary>
        /// Parse all the given child elements with the given parser.
        /// </summary>
        public static Boolean TryParseOptionalXMLs<T>(this XElement                     XML,
                                                      XName                             Name,
                                                      String                            Description,
                                                      TryXMLElementParser2<T>           Parser,
                                                      out IEnumerable<T>                Results,
                                                      [NotNullWhen(false)] out String?  ErrorResponse)
        {

            var results    = new List<T>();
            Results        = results;
            ErrorResponse  = null;

            foreach (var element in XML.Elements(Name))
            {

                if (!Parser(element, out var result, out var errorResponse))
                {
                    ErrorResponse = $"Invalid {Description} #{results.Count + 1}: {errorResponse}";
                    return false;
                }

                results.Add(result);

            }

            return true;

        }

        #endregion


        #region TryParseMandatoryValue(XML, Name, Description, Parser, out Result, out ErrorResponse)

        /// <summary>
        /// Parse the text of the given mandatory child element with the given parser.
        /// </summary>
        public static Boolean TryParseMandatoryValue<T>(this XElement                     XML,
                                                        XName                             Name,
                                                        String                            Description,
                                                        TryParseTextDelegate<T>           Parser,
                                                        out T                             Result,
                                                        [NotNullWhen(false)] out String?  ErrorResponse)

            where T : struct

        {

            Result         = default;
            ErrorResponse  = null;

            var element = XML.Element(Name);

            if (element is null)
            {
                ErrorResponse = $"The mandatory {Description} is missing!";
                return false;
            }

            if (!Parser(element.Value.Trim(), out Result))
            {
                ErrorResponse = $"Invalid {Description} '{element.Value}'!";
                return false;
            }

            return true;

        }

        #endregion

        #region TryParseOptionalValue (XML, Name, Description, Parser, out Result, out ErrorResponse)

        /// <summary>
        /// Parse the text of the given optional child element with the given parser.
        /// </summary>
        public static Boolean TryParseOptionalValue<T>(this XElement                     XML,
                                                       XName                             Name,
                                                       String                            Description,
                                                       TryParseTextDelegate<T>           Parser,
                                                       out T?                            Result,
                                                       [NotNullWhen(false)] out String?  ErrorResponse)

            where T : struct

        {

            Result         = null;
            ErrorResponse  = null;

            var element = XML.Element(Name);

            if (element is null)
                return true;

            if (!Parser(element.Value.Trim(), out var result))
            {
                ErrorResponse = $"Invalid {Description} '{element.Value}'!";
                return false;
            }

            Result = result;
            return true;

        }

        #endregion

        #region TryParseOptionalValues(XML, Name, Description, Parser, out Results, out ErrorResponse)

        /// <summary>
        /// Parse the texts of all the given child elements with the given parser.
        /// </summary>
        public static Boolean TryParseOptionalValues<T>(this XElement                     XML,
                                                        XName                             Name,
                                                        String                            Description,
                                                        TryParseTextDelegate<T>           Parser,
                                                        out IEnumerable<T>                Results,
                                                        [NotNullWhen(false)] out String?  ErrorResponse)
        {

            var results    = new List<T>();
            Results        = results;
            ErrorResponse  = null;

            foreach (var element in XML.Elements(Name))
            {

                if (!Parser(element.Value.Trim(), out var result))
                {
                    ErrorResponse = $"Invalid {Description} '{element.Value}'!";
                    return false;
                }

                results.Add(result);

            }

            return true;

        }

        #endregion


        #region OptionalText(XML, Name)

        /// <summary>
        /// The trimmed text of the given optional child element, or null when it is missing or empty.
        /// </summary>
        public static String? OptionalText(this XElement  XML,
                                           XName          Name)
        {

            var text = XML.Element(Name)?.Value.Trim();

            return text is not null && text.Length > 0
                       ? text
                       : null;

        }

        #endregion

        #region XSIType(XML)

        /// <summary>
        /// The local name of the xsi:type of the given element, e.g. "ElectricChargingPoint" for "aegi:ElectricChargingPoint".
        /// </summary>
        public static String? XSIType(this XElement XML)
        {

            var type = XML.Attribute(DatexIINS.XSI + "type")?.Value.Trim();

            if (type is null || type.Length == 0)
                return null;

            var colon = type.IndexOf(':');

            return colon >= 0
                       ? type[(colon + 1)..]
                       : type;

        }

        #endregion


        #region Value parsers

        /// <summary>
        /// Parse an xs:boolean.
        /// </summary>
        public static Boolean TryParseBoolean(String Text, out Boolean Value)
        {

            switch (Text.Trim())
            {

                case "true":
                case "1":
                    Value = true;
                    return true;

                case "false":
                case "0":
                    Value = false;
                    return true;

                default:
                    Value = default;
                    return false;

            }

        }

        /// <summary>
        /// Parse an xs:decimal or xs:float, culture invariant.
        /// </summary>
        public static Boolean TryParseDecimal(String Text, out Decimal Value)

            => Decimal.TryParse(Text, NumberStyles.Float, CultureInfo.InvariantCulture, out Value);

        /// <summary>
        /// Parse an xs:double or xs:float, culture invariant.
        /// </summary>
        public static Boolean TryParseDouble(String Text, out Double Value)

            => Double.TryParse(Text, NumberStyles.Float, CultureInfo.InvariantCulture, out Value);

        /// <summary>
        /// Parse an xs:nonNegativeInteger that fits into 16 bits.
        /// </summary>
        public static Boolean TryParseUInt16(String Text, out UInt16 Value)

            => UInt16.TryParse(Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out Value);

        /// <summary>
        /// Parse an xs:dateTime.
        /// </summary>
        public static Boolean TryParseTimestamp(String Text, out DateTimeOffset Value)

            => DateTimeOffset.TryParse(Text, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out Value);

        /// <summary>
        /// Parse a number of watts.
        /// </summary>
        public static Boolean TryParseWatt(String Text, out Watt Value)

            => Watt.TryParse(Text, out Value);

        /// <summary>
        /// Parse a number of volts.
        /// </summary>
        public static Boolean TryParseVolt(String Text, out Volt Value)

            => Volt.TryParse(Text, out Value);

        /// <summary>
        /// Parse a number of amperes.
        /// </summary>
        public static Boolean TryParseAmpere(String Text, out Ampere Value)

            => Ampere.TryParse(Text, out Value);

        /// <summary>
        /// Parse a number of meters.
        /// </summary>
        public static Boolean TryParseMeter(String Text, out Meter Value)

            => Meter.TryParse(Text, out Value);

        /// <summary>
        /// Parse a number of tonnes.
        /// </summary>
        public static Boolean TryParseTonne(String Text, out Tonne Value)

            => Tonne.TryParse(Text, out Value);

        /// <summary>
        /// Parse a byte.
        /// </summary>
        public static Boolean TryParseByte(String Text, out Byte Value)

            => Byte.TryParse(Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out Value);

        /// <summary>
        /// Parse a language code, e.g. "de".
        /// </summary>
        public static Boolean TryParseLanguage(String Text, out Languages Value)

            => LanguagesExtensions.TryParse(Text, out Value);

        #endregion

    }

}
