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

using NUnit.Framework;

using Newtonsoft.Json.Linq;

using cloud.charging.open.protocols.DatexII.v3;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

#endregion

namespace cloud.charging.open.protocols.DatexII.Tests
{

    /// <summary>
    /// The JSON encoding: the Mobilithek examples through XML and both JSON dialects, against all schemas.
    /// </summary>
    [TestFixture]
    public class JSON_Tests
    {

        #region (private static) Example(Folder, Name)

        private static JObject Example(String Folder, String Name)

            => DatexIIJSON.Parse(File.ReadAllText(Path.Combine(Folder, Name)));

        private static IEnumerable<TestCaseData> MobilithekExamples()
        {
            yield return new TestCaseData(JSONSchemas.MobilithekTable,  "EnergyInfrastructureTablePublication_rev2.json",  false).SetName("Table");
            yield return new TestCaseData(JSONSchemas.MobilithekStatus, "EnergyInfrastructureStatusPublication_rev2.json", true ).SetName("Status");
        }

        #endregion


        #region TheMobilithekExamplesAreValidProfileJSON(Folder, Name, IsStatus)

        [TestCaseSource(nameof(MobilithekExamples))]
        public void TheMobilithekExamplesAreValidProfileJSON(String Folder, String Name, Boolean IsStatus)
        {

            var json = Example(Folder, Name);

            Assert.That(JSONSchemas.Validate(IsStatus ? JSONSchemas.Mobilithek_StatusMessage : JSONSchemas.Mobilithek_Table, json, out var errors),
                        Is.True,
                        String.Join(Environment.NewLine, errors));

        }

        #endregion

        #region TheMobilithekExamplesBecomeValidXML(Folder, Name, IsStatus)

        [TestCaseSource(nameof(MobilithekExamples))]
        public void TheMobilithekExamplesBecomeValidXML(String Folder, String Name, Boolean IsStatus)
        {

            Assert.That(DatexIIJSON.TryParse(Example(Folder, Name), out var xml, out var errorResponse, DatexIIJSONDialect.Mobilithek_01_00_00),
                        Is.True,
                        errorResponse);

            Assert.That(DatexIISchemas.Validate(xml!, out var errors, out var warnings),
                        Is.True,
                        String.Join(Environment.NewLine, errors) + Environment.NewLine + xml);

        }

        #endregion

        #region TheMobilithekExamplesBecomeValidDATEXII37JSON(Folder, Name, IsStatus)

        [TestCaseSource(nameof(MobilithekExamples))]
        public void TheMobilithekExamplesBecomeValidDATEXII37JSON(String Folder, String Name, Boolean IsStatus)
        {

            Assert.That(DatexIIJSON.TryParse(Example(Folder, Name), out var xml, out var errorResponse, DatexIIJSONDialect.Mobilithek_01_00_00),
                        Is.True,
                        errorResponse);

            var json = DatexIIJSON.ToJSON(xml!);

            Assert.That(JSONSchemas.Validate(JSONSchemas.DATEXII_3_7_Payload, json, out var errors),
                        Is.True,
                        String.Join(Environment.NewLine, errors) + Environment.NewLine + json);

            // And back.
            Assert.That(DatexIIJSON.TryParse(json, out var again, out errorResponse), Is.True, errorResponse);
            Assert.That(again!.ToString(), Is.EqualTo(xml!.ToString()));

        }

        #endregion

        #region TheMobilithekExamplesComeBackAsTheyWere(Folder, Name, IsStatus)

        [TestCaseSource(nameof(MobilithekExamples))]
        public void TheMobilithekExamplesComeBackAsTheyWere(String Folder, String Name, Boolean IsStatus)
        {

            var original = Example(Folder, Name);

            Assert.That(DatexIIJSON.TryParse(original, out var xml, out var errorResponse, DatexIIJSONDialect.Mobilithek_01_00_00),
                        Is.True,
                        errorResponse);

            var json = DatexIIJSON.ToJSON(xml!, DatexIIJSONDialect.Mobilithek_01_00_00);

            Assert.That(JSONSchemas.Validate(IsStatus ? JSONSchemas.Mobilithek_StatusMessage : JSONSchemas.Mobilithek_Table, json, out var errors),
                        Is.True,
                        String.Join(Environment.NewLine, errors) + Environment.NewLine + json);

            // The same, but for the target classes, which the profile names freely and DATEX II fixes,
            // the message generation timestamp of a status, and empty arrays, which XML has no way to say.
            Normalise(original);
            Normalise(json);

            Assert.That(Differences(original, json), Is.Empty);

        }

        #endregion

        #region TheMobilithekExamplesComeBackThroughTheModel(Folder, Name, IsStatus)

        /// <summary>
        /// The Mobilithek examples read into the model, and written as XML, as DATEX II 3.7 JSON and as Mobilithek JSON
        /// again: valid, and with what they said.
        /// </summary>
        [TestCaseSource(nameof(MobilithekExamples))]
        public void TheMobilithekExamplesComeBackThroughTheModel(String Folder, String Name, Boolean IsStatus)
        {

            var original = Example(Folder, Name);

            // The example has placeholders for its links, e.g. "https://___" or "linkToGeneralInformation", which are no
            // URLs the model takes as they are.
            foreach (var link in original.Descendants().
                                          OfType<JProperty>().
                                          Where (property => LinkProperties.Contains(property.Name) && property.Value is JValue { Type: JTokenType.String }).
                                          Select(property => (JValue) property.Value).
                                          Where (value    => !org.GraphDefined.Vanaheimr.Hermod.HTTP.URL.TryParse(value.Value<String>()!, out var url) ||
                                                             url.ToString() != value.Value<String>()).
                                          ToArray())
            {
                link.Value = "https://example.org/placeholder";
            }

            System.Xml.Linq.XElement  xml;
            JObject                   json37;
            JObject                   json;

            if (IsStatus)
            {

                Assert.That(EnergyInfrastructureStatusPublication.TryParseJSON(original, out var publication, out var errorResponse, DatexIIJSONDialect.Mobilithek_01_00_00),
                            Is.True,
                            errorResponse);

                Assert.That(publication!.IsDelta, Is.False);

                xml     = publication.ToXML();
                json37  = publication.ToJSON();
                json    = publication.ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00);

                Assert.That(publication.AsDelta().ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00).SelectToken("$..codedExchangeProtocol.value")?.Value<String>(),
                            Is.EqualTo("deltaPush"));

            }
            else
            {

                Assert.That(EnergyInfrastructureTablePublication.TryParseJSON(original, out var publication, out var errorResponse, DatexIIJSONDialect.Mobilithek_01_00_00),
                            Is.True,
                            errorResponse);

                xml     = publication!.ToXML();
                json37  = publication.ToJSON();
                json    = publication.ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00);

            }

            Assert.That(DatexIISchemas.Validate(xml, out var xmlErrors, out _),
                        Is.True,
                        String.Join(Environment.NewLine, xmlErrors) + Environment.NewLine + xml);

            Assert.That(JSONSchemas.Validate(JSONSchemas.DATEXII_3_7_Payload, json37, out var errors),
                        Is.True,
                        String.Join(Environment.NewLine, errors) + Environment.NewLine + json37);

            Assert.That(JSONSchemas.Validate(IsStatus ? JSONSchemas.Mobilithek_StatusMessage : JSONSchemas.Mobilithek_Table, json, out errors),
                        Is.True,
                        String.Join(Environment.NewLine, errors) + Environment.NewLine + json);

            Normalise(original);
            Normalise(json);

            Assert.That(Differences(original, json), Is.Empty);

        }

        #endregion

        #region TheExamplesBecomeValidJSON(Name)

        /// <summary>
        /// The XML examples, read into the model, as valid DATEX II 3.7 JSON and as valid Mobilithek JSON.
        /// </summary>
        [TestCase("EnergyInfrastructureTablePublication.xml")]
        [TestCase("EnergyInfrastructureStatusPublication.xml")]
        public void TheExamplesBecomeValidJSON(String Name)
        {

            var xml = System.Xml.Linq.XElement.Load(Path.Combine(AppContext.BaseDirectory, "Examples", Name));

            Assert.That(v3.D2Payload.Payload.TryParseXML(xml, out var payload, out var errorResponse), Is.True, errorResponse);

            var (json37, json, isStatus) = payload switch {
                EnergyInfrastructureTablePublication  table   => (table. ToJSON(), table. ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00), false),
                EnergyInfrastructureStatusPublication status  => (status.ToJSON(), status.ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00), true),
                _                                             => throw new AssertionException($"Unexpected payload '{payload?.GetType().Name}'!")
            };

            Assert.That(JSONSchemas.Validate(JSONSchemas.DATEXII_3_7_Payload, json37, out var errors),
                        Is.True,
                        String.Join(Environment.NewLine, errors) + Environment.NewLine + json37);

            // The examples use more of DATEX II than the Mobilithek profile allows, so its schema would fail them:
            // they only have to come out in its form.
            Assert.That(json[isStatus ? "messageContainer" : "payload"], Is.Not.Null);

        }

        #endregion

        #region (private) Differences / Normalise / LinkProperties

        /// <summary>
        /// The properties of the type com:Url.
        /// </summary>
        private static readonly HashSet<String> LinkProperties = [
            "urlLinkAddress", "linkToGeneralInformation", "linkToLogo", "linkToWebform", "energyProductInformation", "renewableEnergyEvidence"
        ];

        /// <summary>
        /// The paths where the given JSON differ, numbers compared by value.
        /// </summary>
        private static IEnumerable<String> Differences(JToken Expected, JToken Actual, String Path = "$")
        {

            switch (Expected, Actual)
            {

                case (JObject expected, JObject actual):
                    foreach (var name in expected.Properties().Select(property => property.Name).Union(actual.Properties().Select(property => property.Name)))
                    {
                        if      (actual  [name] is null) yield return $"{Path}.{name}: missing";
                        else if (expected[name] is null) yield return $"{Path}.{name}: unexpected {actual[name]!.ToString(Newtonsoft.Json.Formatting.None)}";
                        else foreach (var difference in Differences(expected[name]!, actual[name]!, $"{Path}.{name}"))
                                 yield return difference;
                    }
                    break;

                case (JArray expected, JArray actual):
                    if (expected.Count != actual.Count)
                        yield return $"{Path}: {expected.Count} items, not {actual.Count}";
                    for (var i = 0; i < Math.Min(expected.Count, actual.Count); i++)
                        foreach (var difference in Differences(expected[i], actual[i], $"{Path}[{i}]"))
                            yield return difference;
                    break;

                // Timestamps are the same when they are the same instant, whatever the offset they are written with.
                case (JValue { Type: JTokenType.String } expected, JValue { Type: JTokenType.String } actual)
                    when expected.Value<String>()!.Contains('T') &&
                         DateTimeOffset.TryParse(expected.Value<String>(), System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var expectedTime) &&
                         DateTimeOffset.TryParse(actual.  Value<String>(), System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var actualTime):
                    if (expectedTime != actualTime)
                        yield return $"{Path}: {expected.ToString(Newtonsoft.Json.Formatting.None)}, not {actual.ToString(Newtonsoft.Json.Formatting.None)}";
                    break;

                case (JValue expected, JValue actual):
                    if (!(expected.Value is IConvertible && actual.Value is IConvertible &&
                          expected.Type is JTokenType.Integer or JTokenType.Float && actual.Type is JTokenType.Integer or JTokenType.Float
                              ? Convert.ToDecimal(expected.Value) == Convert.ToDecimal(actual.Value)
                              : JToken.DeepEquals(expected, actual)))
                        yield return $"{Path}: {expected.ToString(Newtonsoft.Json.Formatting.None)}, not {actual.ToString(Newtonsoft.Json.Formatting.None)}";
                    break;

                default:
                    yield return $"{Path}: {Expected.ToString(Newtonsoft.Json.Formatting.None)}, not {Actual.ToString(Newtonsoft.Json.Formatting.None)}";
                    break;

            }

        }

        private static void Normalise(JToken JSON)
        {
            foreach (var targetClass in JSON.SelectTokens("$..targetClass").ToArray())
                targetClass.Replace("*");
            foreach (var timestamp in JSON.SelectTokens("$..messageGenerationTimestamp").ToArray())
                timestamp.Replace("*");
            foreach (var empty in JSON.SelectTokens("$..*").OfType<JArray>().Where(array => array.Count == 0).ToArray())
                empty.Parent?.Remove();
        }

        #endregion

    }

}
