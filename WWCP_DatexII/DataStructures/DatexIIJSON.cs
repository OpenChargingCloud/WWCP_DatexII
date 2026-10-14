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

using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3
{

    /// <summary>
    /// The JSON dialects of DATEX II.
    /// </summary>
    public enum DatexIIJSONDialect
    {

        /// <summary>
        /// The JSON encoding of DATEX II 3.7, as its JSON schemas define it (docs.datex2.eu).
        /// </summary>
        DATEXII_3_7,

        /// <summary>
        /// The German Mobilithek profile "AFIR Energy Infrastructure" 01-00-00 (rev2), which predates 3.7: electricEnergy
        /// instead of energyProduct, no priceGroupIndex, domesticA instead of domesticAType, FacilityLocation instead of
        /// AfirFacilityLocation, euVehicleCategory directly in the extension of vehicle characteristics rather than in its
        /// regulated characteristics, roamingPlatfom on sites, the identifier types stationIdBNetzA, operatorIdBNetzA,
        /// operatorId and providerId as extended values (an otherTypeOfIdentifier in 3.7, which knows only evseId), and
        /// status publications in a message container.
        /// </summary>
        Mobilithek_01_00_00

    }


    /// <summary>
    /// The JSON encoding of DATEX II: a d2:payload converted to and from JSON, driven by the DATEX II 3.7 schemas.
    /// </summary>
    /// <remarks>
    /// The rules are those of the DATEX II 3.7 JSON schemas:
    /// - the document is {"payload": {modelBaseVersionG, profileNameG, profileVersionG, "&lt;prefix&gt;&lt;Publication&gt;": {lang, …}}};
    /// - the attributes id and version become idG and versionG, the others keep their names;
    /// - an element whose type is abstract or has subtypes becomes {"&lt;prefix&gt;&lt;ConcreteType&gt;": {…}};
    /// - an element that may repeat becomes an array, also with one item;
    /// - an enumeration becomes {"value": …, "extendedValueG": …}, with _extended as extendedG;
    /// - an extension element _xxxExtension becomes "&lt;prefix&gt;&lt;DeclaringType&gt;ExtensionG", whose typed children are capitalised;
    /// - values/value of a multilingual string becomes {"values": [{"lang", "value"}]}, a value without a language in the one of the publication;
    /// - numbers and booleans are JSON numbers and booleans.
    /// From JSON, the elements come back in the order of the schema, so the XML validates.
    /// </remarks>
    public static class DatexIIJSON
    {

        #region Data

        private static readonly XName Payload = DatexIINS.D2Payload + "payload";

        private static readonly IReadOnlyDictionary<String, String> PayloadAttributes = new Dictionary<String, String> {
            { "modelBaseVersion",  "modelBaseVersionG"  },
            { "extensionName",     "extensionNameG"     },
            { "extensionVersion",  "extensionVersionG"  },
            { "profileName",       "profileNameG"       },
            { "profileVersion",    "profileVersionG"    }
        };

        #endregion


        #region Parse(Text)

        /// <summary>
        /// Parse the given JSON text, with timestamps kept as they are written: DATEX II needs their offsets.
        /// </summary>
        /// <param name="Text">A JSON text.</param>
        public static JObject Parse(String Text)
        {

            using var reader = new Newtonsoft.Json.JsonTextReader(new StringReader(Text)) {
                                   DateParseHandling   = Newtonsoft.Json.DateParseHandling.None,
                                   FloatParseHandling  = Newtonsoft.Json.FloatParseHandling.Decimal
                               };

            return JObject.Load(reader);

        }

        #endregion


        #region ToJSON(PayloadXML, Dialect = DATEXII_3_7, IsDelta = false)

        /// <summary>
        /// The JSON of the given d2:payload.
        /// </summary>
        /// <param name="PayloadXML">A d2:payload, valid against the DATEX II 3.7 schemas.</param>
        /// <param name="Dialect">The JSON dialect.</param>
        /// <param name="IsDelta">Mobilithek status publications only: whether it has only what changed (deltaPush) or everything (snapshotPush).</param>
        public static JObject ToJSON(XElement            PayloadXML,
                                     DatexIIJSONDialect  Dialect   = DatexIIJSONDialect.DATEXII_3_7,
                                     Boolean             IsDelta   = false)
        {

            if (PayloadXML.Name != Payload)
                throw new ArgumentException($"A d2:payload is expected, not '{PayloadXML.Name}'!", nameof(PayloadXML));

            var typeName     = TypeNameOf(PayloadXML)
                                   ?? throw new ArgumentException("The d2:payload has no xsi:type!", nameof(PayloadXML));

            var type         = DatexIISchemas.GetType(typeName) as XmlSchemaComplexType
                                   ?? throw new ArgumentException($"The publication type '{typeName}' is unknown!", nameof(PayloadXML));

            // A value of a multilingual string without a language is in the language of the publication, which JSON wants said.
            PayloadXML = new XElement(PayloadXML);

            if (PayloadXML.Attribute("lang")?.Value is String language)
            {
                foreach (var value in PayloadXML.Descendants(DatexIINS.Common + "value").
                                                 Where(value => value.Parent?.Name == DatexIINS.Common + "values" && value.Attribute("lang") is null))
                {
                    value.SetAttributeValue("lang", language);
                }
            }

            var publication  = ComplexToJSON(PayloadXML, type);

            // lang goes into the publication, the other attributes of the payload are those of the payload itself.
            foreach (var name in PayloadAttributes.Keys)
                publication.Remove(name);

            var payload      = new JObject();

            foreach (var (attribute, jsonName) in PayloadAttributes)
                if (PayloadXML.Attribute(attribute) is XAttribute value)
                    payload[jsonName] = value.Value;

            payload[JSONNameOf(typeName)] = publication;

            var json         = new JObject(new JProperty("payload", payload));

            return Dialect == DatexIIJSONDialect.Mobilithek_01_00_00
                       ? MobilithekDialect.FromDATEXII37(json, typeName.LocalName == "EnergyInfrastructureStatusPublication", IsDelta)
                       : json;

        }

        #endregion

        #region TryParse(JSON, out PayloadXML, out ErrorResponse, Dialect = DATEXII_3_7)

        /// <summary>
        /// Try to convert the given DATEX II JSON into a d2:payload.
        /// </summary>
        /// <param name="JSON">A DATEX II JSON document.</param>
        /// <param name="PayloadXML">The d2:payload.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="Dialect">The JSON dialect.</param>
        public static Boolean TryParse(JObject                                JSON,
                                       [NotNullWhen(true)]  out XElement?     PayloadXML,
                                       [NotNullWhen(false)] out String?       ErrorResponse,
                                       DatexIIJSONDialect                     Dialect   = DatexIIJSONDialect.DATEXII_3_7)
        {

            PayloadXML     = null;
            ErrorResponse  = null;

            try
            {

                if (Dialect == DatexIIJSONDialect.Mobilithek_01_00_00)
                    JSON = MobilithekDialect.ToDATEXII37(JSON);

                if (JSON["payload"] is not JObject payload)
                {
                    ErrorResponse = "The JSON has no payload object!";
                    return false;
                }

                var publications = payload.Properties().Where(property => !property.Name.EndsWith('G')).ToArray();

                if (publications.Length != 1 || publications[0].Value is not JObject publication)
                {
                    ErrorResponse = "The payload must have exactly one publication!";
                    return false;
                }

                var typeName = TypeNameOf(publications[0].Name);

                if (typeName is null || DatexIISchemas.GetType(typeName) is not XmlSchemaComplexType type)
                {
                    ErrorResponse = $"The publication '{publications[0].Name}' is unknown!";
                    return false;
                }

                var xml = ComplexFromJSON(publication, type, Payload, "payload");

                xml.Add(new XAttribute(DatexIINS.XSI + "type", QualifiedTextOf(typeName)));

                foreach (var (attribute, jsonName) in PayloadAttributes)
                    if (payload[jsonName] is JValue value)
                        xml.SetAttributeValue(attribute, value.ToString(CultureInfo.InvariantCulture));

                foreach (var (prefix, ns) in DatexIISchemas.Prefixes)
                    xml.SetAttributeValue(XNamespace.Xmlns + prefix, ns.NamespaceName);

                xml.SetAttributeValue(XNamespace.Xmlns + "xsi", DatexIINS.XSI.NamespaceName);

                PayloadXML = xml;
                return true;

            }
            catch (Exception e)
            {
                ErrorResponse = e.Message;
                return false;
            }

        }

        #endregion


        #region (private) XML → JSON

        private static JObject ComplexToJSON(XElement              XML,
                                             XmlSchemaComplexType  Type)
        {

            var json = new JObject();

            #region Attributes

            foreach (var attribute in XML.Attributes().Where(attribute => !attribute.IsNamespaceDeclaration && attribute.Name.Namespace != DatexIINS.XSI))
            {

                var schemaAttribute = Type.AttributeUses.Values.OfType<XmlSchemaAttribute>().FirstOrDefault(schemaAttribute => schemaAttribute.QualifiedName.Name == attribute.Name.LocalName);

                json[AttributeJSONName(attribute.Name.LocalName)] = schemaAttribute?.AttributeSchemaType is XmlSchemaSimpleType simpleType
                                                                        ? SimpleToJSON(attribute.Value, simpleType)
                                                                        : attribute.Value;

            }

            #endregion

            #region Simple content, e.g. an enumeration or a multilingual string value

            if (Type.ContentModel is XmlSchemaSimpleContent)
            {

                var text = XML.Value;

                json["value"] = Type.BaseXmlSchemaType is XmlSchemaSimpleType baseType && DatexIISchemas.EnumerationValues(baseType).Count == 0
                                    ? SimpleToJSON(text, baseType)
                                    : text == "_extended" ? "extendedG" : text;

                return json;

            }

            #endregion

            #region Elements

            var particles = DatexIISchemas.Particles(Type);

            foreach (var group in XML.Elements().GroupBy(element => element.Name))
            {

                var particle = particles.FirstOrDefault(particle => particle.QualifiedName.Name      == group.Key.LocalName &&
                                                                     particle.QualifiedName.Namespace == group.Key.NamespaceName);

                // Content of an extension point that the schemas do not know.
                if (particle is null)
                {
                    var any = group.Select(AnyToJSON).ToArray();
                    json[group.Key.LocalName] = any.Length == 1 ? any[0] : new JArray(any);
                    continue;
                }

                var values = group.Select(element => ElementToJSON(element, particle)).ToArray();

                json[ElementJSONName(particle, Type)] = particle.MaxOccurs > 1
                                                            ? new JArray(values)
                                                            : values[0];

            }

            #endregion

            return json;

        }

        private static JToken ElementToJSON(XElement          XML,
                                            XmlSchemaElement  Particle)
        {

            var declaredType = Particle.ElementSchemaType;

            if (declaredType is XmlSchemaSimpleType simpleType)
                return SimpleToJSON(XML.Value, simpleType);

            if (declaredType is not XmlSchemaComplexType complexType)
                return XML.Value;

            #region An extension point: its typed children are capitalised

            if (IsExtension(Particle))
            {

                var extension = new JObject();

                foreach (var child in XML.Elements())
                {

                    var childParticle = DatexIISchemas.Particles(complexType).FirstOrDefault(particle => particle.QualifiedName.Name      == child.Name.LocalName &&
                                                                                                          particle.QualifiedName.Namespace == child.Name.NamespaceName);

                    extension[Capitalise(child.Name.LocalName)] = childParticle is not null
                                                                      ? ElementToJSON(child, childParticle)
                                                                      : AnyToJSON(child);

                }

                return extension;

            }

            #endregion

            #region A type with subtypes: wrapped in the name of the concrete type

            if (HasSubtypes(complexType))
            {

                var concreteName  = TypeNameOf(XML) ?? XName.Get(complexType.QualifiedName.Name, complexType.QualifiedName.Namespace);
                var concreteType  = DatexIISchemas.GetType(concreteName) as XmlSchemaComplexType ?? complexType;

                return new JObject(new JProperty(JSONNameOf(concreteName), ComplexToJSON(XML, concreteType)));

            }

            #endregion

            #region An anonymous wrapper of one repeated element, e.g. the values of a multilingual string

            if (complexType.QualifiedName.IsEmpty &&
                DatexIISchemas.Particles(complexType) is [ var only ] &&
                only.MaxOccurs > 1)
            {
                return new JArray(XML.Elements().Select(element => ElementToJSON(element, only)));
            }

            #endregion

            return ComplexToJSON(XML, complexType);

        }

        private static JToken SimpleToJSON(String               Text,
                                           XmlSchemaSimpleType  Type)
        {

            switch (Type.Datatype?.TypeCode)
            {

                case XmlTypeCode.Boolean:
                    return XmlConvert.ToBoolean(Text.Trim());

                case XmlTypeCode.Integer:
                case XmlTypeCode.NonNegativeInteger:
                case XmlTypeCode.PositiveInteger:
                case XmlTypeCode.NegativeInteger:
                case XmlTypeCode.NonPositiveInteger:
                case XmlTypeCode.Int:
                case XmlTypeCode.Long:
                case XmlTypeCode.Short:
                case XmlTypeCode.Byte:
                case XmlTypeCode.UnsignedInt:
                case XmlTypeCode.UnsignedLong:
                case XmlTypeCode.UnsignedShort:
                case XmlTypeCode.UnsignedByte:
                    return Int64.Parse(Text.Trim(), CultureInfo.InvariantCulture);

                case XmlTypeCode.Decimal:
                case XmlTypeCode.Float:
                case XmlTypeCode.Double:
                    var number = Decimal.Parse(Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture);
                    // 22000 rather than 22000.0
                    return number == Decimal.Truncate(number) && Math.Abs(number) < Int64.MaxValue
                               ? (Int64) number
                               : number;

                default:
                    return Text;

            }

        }

        private static JToken AnyToJSON(XElement XML)
        {

            if (!XML.HasElements && !XML.Attributes().Any(attribute => !attribute.IsNamespaceDeclaration))
                return XML.Value;

            var json = new JObject();

            foreach (var attribute in XML.Attributes().Where(attribute => !attribute.IsNamespaceDeclaration))
                json[attribute.Name.LocalName] = attribute.Value;

            foreach (var group in XML.Elements().GroupBy(element => element.Name.LocalName))
            {
                var values = group.Select(AnyToJSON).ToArray();
                json[group.Key] = values.Length == 1 ? values[0] : new JArray(values);
            }

            if (!XML.HasElements && XML.Value.Length > 0)
                json["value"] = XML.Value;

            return json;

        }

        #endregion

        #region (private) JSON → XML

        private static XElement ComplexFromJSON(JObject               JSON,
                                                XmlSchemaComplexType  Type,
                                                XName                 Name,
                                                String                Path)
        {

            var xml       = new XElement(Name);
            var consumed  = new HashSet<String>();

            #region Attributes

            foreach (var attribute in Type.AttributeUses.Values.OfType<XmlSchemaAttribute>())
            {

                var jsonName = AttributeJSONName(attribute.QualifiedName.Name);

                if (JSON[jsonName] is JValue value)
                {

                    consumed.Add(jsonName);

                    // A fixed value, e.g. a target class, is the schema's, whatever the JSON says: it says it without a prefix, or with another one.
                    xml.SetAttributeValue(attribute.QualifiedName.Name, attribute.FixedValue ?? SimpleFromJSON(value));

                }

                else if (attribute.Use == XmlSchemaUse.Required && attribute.FixedValue is not null)
                    xml.SetAttributeValue(attribute.QualifiedName.Name, attribute.FixedValue);

            }

            #endregion

            #region Simple content

            if (Type.ContentModel is XmlSchemaSimpleContent)
            {

                if (JSON["value"] is JValue value)
                {

                    consumed.Add("value");

                    var text    = SimpleFromJSON(value);
                    var values  = DatexIISchemas.EnumerationValues(Type);

                    // A value the enumeration does not know is an extended one.
                    if (values.Count > 0 && text != "extendedG" && !values.Contains(text) &&
                        Type.AttributeUses.Values.OfType<XmlSchemaAttribute>().Any(attribute => attribute.QualifiedName.Name == "_extendedValue"))
                    {
                        xml.SetAttributeValue("_extendedValue", text);
                        text = "extendedG";
                    }

                    xml.Add(text == "extendedG" ? "_extended" : text);

                }

                CheckConsumed(JSON, consumed, Path);
                return xml;

            }

            #endregion

            #region Elements, in the order of the schema

            foreach (var particle in DatexIISchemas.Particles(Type))
            {

                var jsonName = ElementJSONName(particle, Type);

                if (JSON[jsonName] is not JToken token)
                    continue;

                consumed.Add(jsonName);

                var elementName = XName.Get(particle.QualifiedName.Name, particle.QualifiedName.Namespace);

                // A repeating element is an array; a single one may still be an array, e.g. the values of a multilingual string.
                foreach (var item in token is JArray array && particle.MaxOccurs > 1 ? array.AsEnumerable() : [ token ])
                    xml.Add(ElementFromJSON(item, particle, elementName, $"{Path}.{jsonName}"));

            }

            #endregion

            CheckConsumed(JSON, consumed, Path);

            return xml;

        }

        private static XElement ElementFromJSON(JToken            JSON,
                                                XmlSchemaElement  Particle,
                                                XName             Name,
                                                String            Path)
        {

            var declaredType = Particle.ElementSchemaType;

            if (declaredType is XmlSchemaSimpleType)
                return new XElement(Name, SimpleFromJSON(JSON));

            if (declaredType is not XmlSchemaComplexType complexType)
                return new XElement(Name, JSON.ToString());

            if (JSON is not JObject jsonObject)
            {

                // An anonymous wrapper of one repeated element: its items.
                if (JSON is JArray items &&
                    complexType.QualifiedName.IsEmpty &&
                    DatexIISchemas.Particles(complexType) is [ var only ])
                {
                    var wrapper = new XElement(Name);
                    foreach (var item in items)
                        wrapper.Add(ElementFromJSON(item, only, XName.Get(only.QualifiedName.Name, only.QualifiedName.Namespace), Path));
                    return wrapper;
                }

                throw new FormatException($"'{Path}' must be an object!");

            }

            #region An extension point: its typed children are capitalised

            if (IsExtension(Particle))
            {

                var extension = new XElement(Name);

                foreach (var particle in DatexIISchemas.Particles(complexType))
                {
                    if (jsonObject[Capitalise(particle.QualifiedName.Name)] is JToken value)
                        extension.Add(ElementFromJSON(value, particle, XName.Get(particle.QualifiedName.Name, particle.QualifiedName.Namespace), $"{Path}.{particle.QualifiedName.Name}"));
                }

                return extension;

            }

            #endregion

            #region A type with subtypes: unwrapped from the name of the concrete type

            if (HasSubtypes(complexType))
            {

                if (jsonObject.Properties().ToArray() is not [ var concrete ] || concrete.Value is not JObject concreteJSON)
                    throw new FormatException($"'{Path}' must have exactly one property naming its type!");

                var concreteName = TypeNameOf(concrete.Name)
                                       ?? throw new FormatException($"'{Path}.{concrete.Name}' names no DATEX II type!");

                if (DatexIISchemas.GetType(concreteName) is not XmlSchemaComplexType concreteType)
                    throw new FormatException($"'{Path}.{concrete.Name}' names an unknown DATEX II type!");

                var element = ComplexFromJSON(concreteJSON, concreteType, Name, $"{Path}.{concrete.Name}");

                if (concreteType.QualifiedName != complexType.QualifiedName)
                    element.SetAttributeValue(DatexIINS.XSI + "type", QualifiedTextOf(concreteName));

                return element;

            }

            #endregion

            return ComplexFromJSON(jsonObject, complexType, Name, Path);

        }

        private static String SimpleFromJSON(JToken JSON)

            => JSON switch {
                   JValue { Type: Newtonsoft.Json.Linq.JTokenType.Boolean } value  => (Boolean) value ? "true" : "false",
                   JValue { Type: Newtonsoft.Json.Linq.JTokenType.Integer } value  => Convert.ToString(value.Value, CultureInfo.InvariantCulture) ?? "",
                   JValue { Type: Newtonsoft.Json.Linq.JTokenType.Float   } value  => value.Value is Double number
                                                                                          ? number.ToString("R", CultureInfo.InvariantCulture)
                                                                                          : Convert.ToString(value.Value, CultureInfo.InvariantCulture) ?? "",
                   JValue { Type: Newtonsoft.Json.Linq.JTokenType.Date    } value  => value.Value is DateTimeOffset offset
                                                                                          ? offset.ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFFzzz", CultureInfo.InvariantCulture)
                                                                                          : ((DateTime) value.Value!).ToString("o", CultureInfo.InvariantCulture),
                   JValue value                                                       => Convert.ToString(value.Value, CultureInfo.InvariantCulture) ?? "",
                   _                                                                  => JSON.ToString()
               };

        private static void CheckConsumed(JObject              JSON,
                                          IReadOnlySet<String> Consumed,
                                          String               Path)
        {

            var unknown = JSON.Properties().Select(property => property.Name).Where(name => !Consumed.Contains(name)).ToArray();

            if (unknown.Length > 0)
                throw new FormatException($"'{Path}' has properties DATEX II 3.7 does not know: {String.Join(", ", unknown)}");

        }

        #endregion

        #region (private) Names

        private static String AttributeJSONName(String Attribute)

            => Attribute switch {
                   "id"              => "idG",
                   "version"         => "versionG",
                   "_extendedValue"  => "extendedValueG",
                   _                 => Attribute
               };

        /// <summary>
        /// The JSON name of the given element: its name, or for an extension point the prefixed name of the type that declares it + "ExtensionG".
        /// </summary>
        private static String ElementJSONName(XmlSchemaElement      Particle,
                                              XmlSchemaComplexType  Type)
        {

            if (!IsExtension(Particle))
                return Particle.QualifiedName.Name;

            // The type of the inheritance chain that declares the extension point.
            for (var type = Type; type is not null; type = type.BaseXmlSchemaType as XmlSchemaComplexType)
            {

                var declared = type.ContentModel is XmlSchemaComplexContent { Content: XmlSchemaComplexContentExtension extension }
                                   ? Declares(extension.Particle, Particle)
                                   : Declares(type.Particle, Particle);

                if (declared)
                    return JSONNameOf(XName.Get(type.QualifiedName.Name, type.QualifiedName.Namespace)) + "ExtensionG";

            }

            return Particle.QualifiedName.Name;

        }

        private static Boolean Declares(XmlSchemaParticle? Particle, XmlSchemaElement Element)

            => Particle switch {
                   XmlSchemaElement element    => element.QualifiedName == Element.QualifiedName,
                   XmlSchemaGroupBase group    => group.Items.OfType<XmlSchemaParticle>().Any(item => Declares(item, Element)),
                   _                           => false
               };

        private static Boolean IsExtension(XmlSchemaElement Particle)

            => Particle.QualifiedName.Name.StartsWith('_') &&
               Particle.QualifiedName.Name.EndsWith("Extension", StringComparison.Ordinal);

        private static Boolean HasSubtypes(XmlSchemaComplexType Type)

            => Type.IsAbstract || DatexIISchemas.DerivedTypesOf(Type).Count > 0;

        private static String Capitalise(String Name)

            => Name.Length > 0 ? Char.ToUpperInvariant(Name[0]) + Name[1..] : Name;

        /// <summary>
        /// "aegiElectricChargingPoint" for aegi:ElectricChargingPoint.
        /// </summary>
        private static String JSONNameOf(XName TypeName)

            => DatexIISchemas.Prefixes.FirstOrDefault(prefix => prefix.Value == TypeName.Namespace).Key + TypeName.LocalName;

        /// <summary>
        /// aegi:ElectricChargingPoint for "aegiElectricChargingPoint".
        /// </summary>
        private static XName? TypeNameOf(String JSONName)
        {

            foreach (var (prefix, ns) in DatexIISchemas.Prefixes.OrderByDescending(prefix => prefix.Key.Length))
                if (JSONName.StartsWith(prefix, StringComparison.Ordinal) &&
                    JSONName.Length > prefix.Length &&
                    Char.IsUpper(JSONName[prefix.Length]))
                {
                    return ns + JSONName[prefix.Length..];
                }

            return null;

        }

        /// <summary>
        /// The type of the given element from its xsi:type, if it has one.
        /// </summary>
        private static XName? TypeNameOf(XElement XML)
        {

            var text = XML.Attribute(DatexIINS.XSI + "type")?.Value.Trim();

            if (text is null || text.Length == 0)
                return null;

            var colon = text.IndexOf(':');

            var ns    = colon >= 0
                            ? XML.GetNamespaceOfPrefix(text[..colon])
                            : XML.GetDefaultNamespace();

            return (ns ?? XNamespace.None) + (colon >= 0 ? text[(colon + 1)..] : text);

        }

        /// <summary>
        /// "aegi:ElectricChargingPoint" for aegi:ElectricChargingPoint, with the prefixes DATEX II uses.
        /// </summary>
        private static String QualifiedTextOf(XName TypeName)

            => DatexIISchemas.Prefixes.FirstOrDefault(prefix => prefix.Value == TypeName.Namespace).Key + ":" + TypeName.LocalName;

        #endregion


        #region (private static class) MobilithekDialect

        /// <summary>
        /// The differences of the Mobilithek profile 01-00-00 to the JSON of DATEX II 3.7.
        /// </summary>
        private static class MobilithekDialect
        {

            /// <summary>
            /// The types of identifiers the profile adds as extended values; DATEX II 3.7 knows only evseId, the others
            /// are an otherTypeOfIdentifier there.
            /// </summary>
            private static readonly IReadOnlySet<String> ProfileIdentifierTypes = new HashSet<String> {
                "stationIdBNetzA", "operatorIdBNetzA", "operatorId", "providerId"
            };

            private static readonly IReadOnlyDictionary<String, String> ConnectorTypes
                = Enumerable.Range('A', 15).ToDictionary(letter => $"domestic{(Char) letter}Type", letter => $"domestic{(Char) letter}");

            #region (private) TimeProperties / LocalTime

            /// <summary>
            /// The properties of the type com:Time.
            /// </summary>
            private static readonly HashSet<String> TimeProperties = [
                "startTimeOfPeriod", "endTimeOfPeriod", "durationStart", "durationEnd", "earliestStart", "earliestEnd",
                "latestStart", "latestEnd", "nextDepartures", "resetTime"
            ];

            /// <summary>
            /// A time of day without an offset.
            /// </summary>
            private static readonly System.Text.RegularExpressions.Regex LocalTime = new (@"^\d{2}:\d{2}:\d{2}(\.\d+)?$");

            #endregion

            #region FromDATEXII37(JSON, IsStatus, IsDelta)

            public static JObject FromDATEXII37(JObject  JSON,
                                                Boolean  IsStatus,
                                                Boolean  IsDelta)
            {

                var json = (JObject) JSON.DeepClone();

                Walk(json, (obj, parentKey) => {

                    // energyProduct: [{aegiElectricEnergy: {…}}] → electricEnergy: […]
                    if (obj["energyProduct"] is JArray products)
                    {
                        obj["electricEnergy"] = new JArray(products.OfType<JObject>().Select(product => product.Properties().First().Value));
                        obj.Remove("energyProduct");
                    }

                    // EU vehicle categories DATEX II 3.7 does not know are extended values there, but values in the profile.
                    if (parentKey == "euVehicleCategory" && obj["value"]?.ToString() == "extendedG" && obj["extendedValueG"] is JValue category)
                    {
                        obj["value"] = category.ToString();
                        obj.Remove("extendedValueG");
                    }

                    // domesticAType → domesticA
                    if (parentKey == "connectorType" && obj["value"] is JValue value && ConnectorTypes.TryGetValue(value.ToString(), out var connectorType))
                        obj["value"] = connectorType;

                    // No price group index.
                    if (obj["priceType"] is not null)
                        obj.Remove("priceGroupIndex");

                    // otherTypeOfIdentifier operatorId → typeOfIdentifier extendedG/operatorId
                    if (obj["identifier"] is not null && obj["typeOfIdentifier"] is null &&
                        obj["otherTypeOfIdentifier"] is JValue otherType && ProfileIdentifierTypes.Contains(otherType.ToString()))
                    {
                        obj["typeOfIdentifier"] = new JObject(new JProperty("value", "extendedG"), new JProperty("extendedValueG", otherType.ToString()));
                        obj.Remove("otherTypeOfIdentifier");
                    }

                    // AfirFacilityLocation → FacilityLocation
                    if (parentKey == "locLocationExtensionG" && obj["AfirFacilityLocation"] is JToken facilityLocation)
                    {
                        obj["FacilityLocation"] = facilityLocation;
                        obj.Remove("AfirFacilityLocation");
                    }

                    // VehicleCharacteristicsExtended.regulatedCharacteristics[].euVehicleCategory → euVehicleCategory
                    if (parentKey == "comVehicleCharacteristicsExtensionG" && obj["VehicleCharacteristicsExtended"] is JObject extended)
                    {
                        obj.Remove("VehicleCharacteristicsExtended");
                        var categories = (extended["regulatedCharacteristics"] as JArray ?? []).
                                             OfType<JObject>().
                                             SelectMany(regulated => regulated["euVehicleCategory"] as JArray ?? []).
                                             ToArray();
                        if (categories.Length > 0)
                            obj["euVehicleCategory"] = new JArray(categories);
                    }

                    // roamingPlatfom on sites.
                    if (parentKey == "energyInfrastructureSite" && obj["roamingPlatform"] is JToken roamingPlatform)
                    {
                        obj["roamingPlatfom"] = roamingPlatform;
                        obj.Remove("roamingPlatform");
                    }

                    // Times of day with a "Z": the profile wants RFC 3339 times, and means the local time by them.
                    foreach (var property in obj.Properties().Where(property => TimeProperties.Contains(property.Name)).ToArray())
                    {
                        if (property.Value is JValue { Type: JTokenType.String } time && LocalTime.IsMatch(time.Value<String>()!))
                            property.Value = time.Value<String>() + "Z";
                    }

                });

                if (!IsStatus)
                    return json;

                return new JObject(
                           new JProperty("messageContainer", new JObject(
                               new JProperty("payload", new JArray(json["payload"]!)),
                               new JProperty("exchangeInformation", new JObject(
                                   new JProperty("exchangeContext", new JObject(
                                       new JProperty("codedExchangeProtocol",        new JObject(new JProperty("value", IsDelta ? "deltaPush" : "snapshotPush"))),
                                       new JProperty("exchangeSpecificationVersion", "3.0"),
                                       new JProperty("supplierOrCisRequester",       new JObject())
                                   )),
                                   new JProperty("dynamicInformation", new JObject(
                                       new JProperty("exchangeStatus",               new JObject(new JProperty("value", "online"))),
                                       new JProperty("messageGenerationTimestamp",   DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", CultureInfo.InvariantCulture))
                                   ))
                               ))
                           ))
                       );

            }

            #endregion

            #region ToDATEXII37(JSON)

            public static JObject ToDATEXII37(JObject JSON)
            {

                var json = (JObject) JSON.DeepClone();

                // A status publication comes in a message container.
                if (json["messageContainer"] is JObject container)
                {

                    if (container["payload"] is not JArray { Count: 1 } payloads)
                        throw new FormatException("The message container must have exactly one payload!");

                    json = new JObject(new JProperty("payload", payloads[0]));

                }

                Walk(json, (obj, parentKey) => {

                    if (obj["electricEnergy"] is JArray energies)
                    {
                        obj["energyProduct"] = new JArray(energies.Select(energy => new JObject(new JProperty("aegiElectricEnergy", energy))));
                        obj.Remove("electricEnergy");
                    }

                    if (parentKey == "connectorType" && obj["value"] is JValue value)
                    {
                        var official = ConnectorTypes.FirstOrDefault(pair => pair.Value == value.ToString()).Key;
                        if (official is not null)
                            obj["value"] = official;
                    }

                    if (obj["identifier"] is not null &&
                        obj["typeOfIdentifier"] is JObject { } typeOfIdentifier &&
                        typeOfIdentifier["extendedValueG"] is JValue extendedValue &&
                        ProfileIdentifierTypes.Contains(extendedValue.ToString()))
                    {
                        obj.Remove("typeOfIdentifier");
                        obj["otherTypeOfIdentifier"] = extendedValue.ToString();
                    }

                    // All prices of a rate apply together in the profile: one group.
                    if (obj["priceType"] is not null && obj["priceGroupIndex"] is null)
                        obj.AddFirst(new JProperty("priceGroupIndex", 1));

                    if (parentKey == "locLocationExtensionG" && obj["FacilityLocation"] is JToken facilityLocation)
                    {
                        obj["AfirFacilityLocation"] = facilityLocation;
                        obj.Remove("FacilityLocation");
                    }

                    if (parentKey == "comVehicleCharacteristicsExtensionG" && obj["euVehicleCategory"] is JArray categories)
                    {
                        obj.Remove("euVehicleCategory");
                        obj["VehicleCharacteristicsExtended"] = new JObject(
                                                                    new JProperty("regulatedCharacteristics", new JArray(
                                                                        new JObject(new JProperty("euVehicleCategory", categories))
                                                                    ))
                                                                );
                    }

                    if (parentKey == "energyInfrastructureSite" && obj["roamingPlatfom"] is JToken roamingPlatform)
                    {
                        obj["roamingPlatform"] = roamingPlatform;
                        obj.Remove("roamingPlatfom");
                    }

                });

                return json;

            }

            #endregion

            #region (private) Walk(Token, Visit, ParentKey = null)

            /// <summary>
            /// Visit every object with the name of the property it is (in an array: of the array).
            /// </summary>
            private static void Walk(JToken                    Token,
                                     Action<JObject, String?>  Visit,
                                     String?                   ParentKey = null)
            {
                switch (Token)
                {

                    case JObject obj:
                        Visit(obj, ParentKey);
                        foreach (var property in obj.Properties().ToArray())
                            Walk(property.Value, Visit, property.Name);
                        break;

                    case JArray array:
                        foreach (var item in array.ToArray())
                            Walk(item, Visit, ParentKey);
                        break;

                }
            }

            #endregion

        }

        #endregion

    }

}
