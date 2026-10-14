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
using System.Reflection;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3
{

    /// <summary>
    /// The DATEX II 3.7 schemas, embedded in this library: for validating documents, and for the knowledge the JSON
    /// encoding needs (which elements repeat, which types have subtypes, which values an enumeration has).
    /// </summary>
    public static class DatexIISchemas
    {

        #region Data

        private const String ResourcePrefix = "DatexII.Schemas.3.7.";

        private static readonly Lazy<XmlSchemaSet> schemas = new (Load, LazyThreadSafetyMode.ExecutionAndPublication);

        private static readonly Lazy<IReadOnlyDictionary<XmlQualifiedName, IReadOnlyList<XmlSchemaComplexType>>> derivedTypes
            = new (FindDerivedTypes, LazyThreadSafetyMode.ExecutionAndPublication);

        #endregion

        #region Properties

        /// <summary>
        /// The DATEX II version of the schemas.
        /// </summary>
        public const String Version = "3.7";

        /// <summary>
        /// The compiled DATEX II 3.7 schemas.
        /// </summary>
        public static XmlSchemaSet  Schemas
            => schemas.Value;

        /// <summary>
        /// The namespace prefixes DATEX II uses, e.g. in the names of the JSON encoding.
        /// </summary>
        public static IReadOnlyDictionary<String, XNamespace> Prefixes { get; }

            = new Dictionary<String, XNamespace>() {
                  { "d2",    DatexIINS.D2Payload           },
                  { "com",   DatexIINS.Common              },
                  { "comx",  DatexIINS.CommonExtension     },
                  { "loc",   DatexIINS.LocationReferencing },
                  { "locx",  DatexIINS.LocationExtension   },
                  { "aegi",  DatexIINS.EnergyInfrastructure },
                  { "afac",  DatexIINS.Facilities          },
                  { "egi",   "http://datex2.eu/schema/3/energyInfrastructure"        },
                  { "fac",   "http://datex2.eu/schema/3/facilities"                  },
                  { "prk",   DatexIINS.Parking             },
                  { "sit",   "http://datex2.eu/schema/3/situation"                   },
                  { "roa",   "http://datex2.eu/schema/3/roadTrafficData"             },
                  { "vms",   "http://datex2.eu/schema/3/vms"                         },
                  { "fst",   "http://datex2.eu/schema/3/faultAndStatus"              },
                  { "cz",    "http://datex2.eu/schema/3/controlledZone"              },
                  { "tro",   "http://datex2.eu/schema/3/trafficRegulation"           },
                  { "tmp",   "http://datex2.eu/schema/3/trafficManagementPlan"       },
                  { "rer",   "http://datex2.eu/schema/3/reroutingManagementEnhanced" },
                  { "ubx",   "http://datex2.eu/schema/3/urbanExtensions"             }
              };

        #endregion


        #region Validate(XML, out Errors, out Warnings)

        /// <summary>
        /// Validate the given XML against the DATEX II 3.7 schemas.
        /// </summary>
        /// <param name="XML">A DATEX II document or element, e.g. a d2:payload.</param>
        /// <param name="Errors">The errors found.</param>
        /// <param name="Warnings">The warnings found.</param>
        public static Boolean Validate(XElement                  XML,
                                       out IEnumerable<String>   Errors,
                                       out IEnumerable<String>   Warnings)
        {

            var errors    = new List<String>();
            var warnings  = new List<String>();

            var settings  = new XmlReaderSettings {
                                Schemas          = Schemas,
                                ValidationType   = ValidationType.Schema,
                                ValidationFlags  = XmlSchemaValidationFlags.ReportValidationWarnings |
                                                   XmlSchemaValidationFlags.ProcessIdentityConstraints
                            };

            settings.ValidationEventHandler += (sender, e) => {
                var position = e.Exception is not null ? $" (line {e.Exception.LineNumber}, position {e.Exception.LinePosition})" : "";
                (e.Severity == XmlSeverityType.Error ? errors : warnings).Add(e.Message + position);
            };

            using (var reader = XmlReader.Create(new StringReader(XML.ToString(SaveOptions.DisableFormatting)), settings))
            {
                try
                {
                    while (reader.Read()) { }
                }
                catch (XmlException e)
                {
                    errors.Add($"XML exception: {e.Message}");
                }
            }

            Errors    = errors;
            Warnings  = warnings;

            return errors.Count == 0;

        }

        #endregion

        #region GetType(Name)

        /// <summary>
        /// The schema type of the given qualified name, e.g. aegi:ElectricChargingPoint.
        /// </summary>
        public static XmlSchemaType? GetType(XName Name)

            => Schemas.GlobalTypes[new XmlQualifiedName(Name.LocalName, Name.NamespaceName)] as XmlSchemaType;

        #endregion

        #region GetElement(Name)

        /// <summary>
        /// The global schema element of the given qualified name, e.g. d2:payload.
        /// </summary>
        public static XmlSchemaElement? GetElement(XName Name)

            => Schemas.GlobalElements[new XmlQualifiedName(Name.LocalName, Name.NamespaceName)] as XmlSchemaElement;

        #endregion

        #region DerivedTypesOf(Type)

        /// <summary>
        /// The concrete types derived from the given complex type, directly or not.
        /// </summary>
        public static IReadOnlyList<XmlSchemaComplexType> DerivedTypesOf(XmlSchemaType Type)

            => derivedTypes.Value.TryGetValue(Type.QualifiedName, out var derived)
                   ? derived
                   : [];

        #endregion

        #region Particles(Type)

        /// <summary>
        /// The element particles of the given complex type, inherited ones included, in the order of the schema.
        /// </summary>
        public static IReadOnlyList<XmlSchemaElement> Particles(XmlSchemaComplexType Type)
        {

            var elements = new List<XmlSchemaElement>();

            void Collect(XmlSchemaParticle? Particle)
            {
                switch (Particle)
                {

                    case XmlSchemaElement element:
                        elements.Add(element);
                        break;

                    case XmlSchemaGroupBase group:
                        foreach (var item in group.Items)
                            Collect(item as XmlSchemaParticle);
                        break;

                }
            }

            Collect(Type.ContentTypeParticle);

            return elements;

        }

        #endregion

        #region EnumerationValues(Type)

        /// <summary>
        /// The values of the given enumeration type, or of the enumeration a "_XxxEnum" type extends; empty for other types.
        /// </summary>
        public static IReadOnlySet<String> EnumerationValues(XmlSchemaType Type)
        {

            var simpleType = Type switch {
                                 XmlSchemaSimpleType simple                                            => simple,
                                 XmlSchemaComplexType complex when complex.ContentModel is XmlSchemaSimpleContent
                                                                                                     => complex.BaseXmlSchemaType as XmlSchemaSimpleType,
                                 _                                                                     => null
                             };

            var values = new HashSet<String>();

            while (simpleType?.Content is XmlSchemaSimpleTypeRestriction restriction)
            {

                foreach (var facet in restriction.Facets.OfType<XmlSchemaEnumerationFacet>())
                    if (facet.Value is not null)
                        values.Add(facet.Value);

                if (values.Count > 0)
                    break;

                simpleType = simpleType.BaseXmlSchemaType as XmlSchemaSimpleType;

            }

            return values;

        }

        #endregion


        #region TypeOf(XML, DeclaredType)

        /// <summary>
        /// The schema type of the given element: its xsi:type, else the given declared type.
        /// </summary>
        public static XmlSchemaType? TypeOf(XElement        XML,
                                            XmlSchemaType?  DeclaredType = null)
        {

            var text = XML.Attribute(DatexIINS.XSI + "type")?.Value.Trim();

            if (text is null || text.Length == 0)
                return DeclaredType;

            var colon  = text.IndexOf(':');
            var ns     = colon >= 0
                             ? XML.GetNamespaceOfPrefix(text[..colon])
                             : XML.GetDefaultNamespace();

            return GetType((ns ?? XNamespace.None) + (colon >= 0 ? text[(colon + 1)..] : text)) ?? DeclaredType;

        }

        #endregion

        #region ExtendUnknownValues(Payload)

        /// <summary>
        /// Write every enumeration value the schemas do not know as an extended one: "_extended", with the value as
        /// "_extendedValue", e.g. the EVSE identifier type "evseId". What a document says stays as it is.
        /// </summary>
        /// <param name="Payload">A d2:payload with an xsi:type.</param>
        public static XElement ExtendUnknownValues(XElement Payload)
        {

            if (TypeOf(Payload) is XmlSchemaComplexType type)
                Extend(Payload, type);

            return Payload;

        }

        private static void Extend(XElement              XML,
                                   XmlSchemaComplexType  Type)
        {

            if (Type.ContentModel is XmlSchemaSimpleContent)
            {

                var values = EnumerationValues(Type);
                var text   = XML.Value;

                if (values.Count > 0 && text != "_extended" && !values.Contains(text) &&
                    Type.AttributeUses.Values.OfType<XmlSchemaAttribute>().Any(attribute => attribute.QualifiedName.Name == "_extendedValue"))
                {
                    XML.SetAttributeValue("_extendedValue", text);
                    XML.Value = "_extended";
                }

                return;

            }

            var particles = Particles(Type);

            foreach (var child in XML.Elements())
            {

                var particle = particles.FirstOrDefault(particle => particle.QualifiedName.Name      == child.Name.LocalName &&
                                                                     particle.QualifiedName.Namespace == child.Name.NamespaceName);

                if (TypeOf(child, particle?.ElementSchemaType) is XmlSchemaComplexType childType)
                    Extend(child, childType);

            }

        }

        #endregion

        #region ResolveExtendedValues(XML)

        /// <summary>
        /// Read every extended enumeration value as the value itself: "_extended" with "_extendedValue" "evseId" becomes "evseId".
        /// </summary>
        /// <param name="XML">A DATEX II document or element.</param>
        public static XElement ResolveExtendedValues(XElement XML)
        {

            foreach (var element in XML.DescendantsAndSelf().Where(element => !element.HasElements && element.Value == "_extended"))
            {
                if (element.Attribute("_extendedValue") is XAttribute extendedValue)
                {
                    element.Value = extendedValue.Value;
                    extendedValue.Remove();
                }
            }

            return XML;

        }

        #endregion


        #region (private) Load()

        private static XmlSchemaSet Load()
        {

            var assembly  = typeof(DatexIISchemas).Assembly;
            var resolver  = new ResourceResolver(assembly);
            var set       = new XmlSchemaSet { XmlResolver = resolver };

            // The payload schema imports every module, so the rest comes through the resolver.
            using var stream = (Stream) resolver.GetEntity(new Uri("datex2:///DATEXII_3_D2Payload.xsd"), null, typeof(Stream))!;
            using var reader = XmlReader.Create(stream, new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit }, "datex2:///DATEXII_3_D2Payload.xsd");

            set.Add(null, reader);
            set.Compile();

            return set;

        }

        #endregion

        #region (private) FindDerivedTypes()

        private static IReadOnlyDictionary<XmlQualifiedName, IReadOnlyList<XmlSchemaComplexType>> FindDerivedTypes()
        {

            var derived = new Dictionary<XmlQualifiedName, List<XmlSchemaComplexType>>();

            foreach (var type in Schemas.GlobalTypes.Values.OfType<XmlSchemaComplexType>())
            {

                if (type.IsAbstract)
                    continue;

                for (var baseType = type.BaseXmlSchemaType as XmlSchemaComplexType; baseType is not null; baseType = baseType.BaseXmlSchemaType as XmlSchemaComplexType)
                {

                    if (baseType.QualifiedName.IsEmpty)
                        break;

                    if (!derived.TryGetValue(baseType.QualifiedName, out var list))
                        derived[baseType.QualifiedName] = list = [];

                    list.Add(type);

                }

            }

            return derived.ToDictionary(pair => pair.Key, pair => (IReadOnlyList<XmlSchemaComplexType>) pair.Value);

        }

        #endregion

        #region (private class) ResourceResolver

        /// <summary>
        /// Resolves the schema locations of the imports to the embedded schemas, and nothing else.
        /// </summary>
        private sealed class ResourceResolver(Assembly Assembly) : XmlResolver
        {

            public override Object? GetEntity(Uri AbsoluteUri, String? Role, Type? OfObjectToReturn)
            {

                var name = ResourcePrefix + Path.GetFileName(AbsoluteUri.AbsolutePath);

                return Assembly.GetManifestResourceStream(name)
                           ?? throw new XmlSchemaException($"The schema '{AbsoluteUri}' is not embedded!");

            }

            public override Uri ResolveUri(Uri? BaseUri, String? RelativeUri)

                => new ("datex2:///" + Path.GetFileName(RelativeUri ?? ""));

        }

        #endregion

    }

}
