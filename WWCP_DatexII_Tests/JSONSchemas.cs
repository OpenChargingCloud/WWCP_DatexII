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

using System.Text.Json;

using Json.Schema;

using Newtonsoft.Json.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.Tests
{

    /// <summary>
    /// The JSON schemas of DATEX II 3.7 and of the Mobilithek profile 01-00-00, for validating what is written.
    /// </summary>
    public static class JSONSchemas
    {

        #region Paths

        /// <summary>
        /// The JSON schemas and examples of DATEX II 3.7 (docs.datex2.eu).
        /// </summary>
        public static String DATEXII_3_7          { get; } = Path.Combine(AppContext.BaseDirectory, "Documentation", "3.7");

        /// <summary>
        /// The JSON schemas and examples of the Mobilithek profile for static data.
        /// </summary>
        public static String MobilithekTable      { get; } = Path.Combine(AppContext.BaseDirectory, "Documentation", "Mobilithek-01-00-00", "EnergyInfrastructureTablePublication");

        /// <summary>
        /// The JSON schemas and examples of the Mobilithek profile for dynamic data.
        /// </summary>
        public static String MobilithekStatus     { get; } = Path.Combine(AppContext.BaseDirectory, "Documentation", "Mobilithek-01-00-00", "EnergyInfrastructureStatusPublication");

        #endregion

        #region Schemas

        private static readonly Lazy<JsonSchema> datexII37         = new (() => Load(DATEXII_3_7,      "DATEXII_3_D2Payload.json"));
        private static readonly Lazy<JsonSchema> mobilithekTable   = new (() => Load(MobilithekTable,  "DATEXII_3_D2Payload.json"));
        private static readonly Lazy<JsonSchema> mobilithekStatus  = new (() => Load(MobilithekStatus, "DATEXII_3_MessageContainer.json"));

        /// <summary>
        /// The payload schema of DATEX II 3.7.
        /// </summary>
        public static JsonSchema  DATEXII_3_7_Payload        => datexII37.Value;

        /// <summary>
        /// The payload schema of the Mobilithek profile for static data.
        /// </summary>
        public static JsonSchema  Mobilithek_Table           => mobilithekTable.Value;

        /// <summary>
        /// The message container schema of the Mobilithek profile for dynamic data.
        /// </summary>
        public static JsonSchema  Mobilithek_StatusMessage   => mobilithekStatus.Value;

        #endregion


        #region Validate(Schema, JSON, out Errors)

        /// <summary>
        /// Validate the given JSON against the given schema.
        /// </summary>
        public static Boolean Validate(JsonSchema               Schema,
                                       JObject                  JSON,
                                       out IEnumerable<String>  Errors)
        {

            using var document = JsonDocument.Parse(JSON.ToString(Newtonsoft.Json.Formatting.None));

            var results = Schema.Evaluate(document.RootElement, new EvaluationOptions { OutputFormat = OutputFormat.List });

            Errors = results.Details?.
                         Where     (detail => detail.Errors is not null).
                         SelectMany(detail => detail.Errors!.Select(error => $"{detail.InstanceLocation}: {error.Key}: {error.Value}")).
                         Distinct().
                         ToArray() ?? [];

            return results.IsValid;

        }

        #endregion

        #region (private) Load(Folder, Root)

        /// <summary>
        /// The given root schema, with every schema of its folder known by its file name, as the references name them.
        /// </summary>
        private static JsonSchema Load(String  Folder,
                                       String  Root)
        {

            var options  = new BuildOptions { SchemaRegistry = new SchemaRegistry() };
            var baseURI  = new Uri($"https://datex2.test/{Path.GetFileName(Folder)}/");

            foreach (var file in Directory.GetFiles(Folder, "DATEXII_3_*.json"))
            {
                var schema = JsonSchema.FromText(File.ReadAllText(file), options, new Uri(baseURI, Path.GetFileName(file)));
                options.SchemaRegistry.Register(new Uri(baseURI, Path.GetFileName(file)), schema);
            }

            return options.SchemaRegistry.Get(new Uri(baseURI, Root)) as JsonSchema
                       ?? throw new InvalidOperationException($"The schema '{Root}' is missing in '{Folder}'!");

        }

        #endregion

    }

}
