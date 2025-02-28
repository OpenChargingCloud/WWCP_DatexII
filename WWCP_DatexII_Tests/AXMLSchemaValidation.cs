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

using NUnit.Framework;

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationExtension;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;
using cloud.charging.open.protocols.DatexII.v3.D2Payload;
using System.Configuration;
using System.Data.SqlTypes;
using System.Xml.Schema;
using System.Xml;

#endregion

namespace cloud.charging.open.protocols.DatexII.Tests
{

    /// <summary>
    /// Unit tests for DatexII XML schema validation.
    /// </summary>
    [TestFixture]
    public abstract class AXMLSchemaValidation
    {

        #region Properties

        /// <summary>
        /// DatexII Status XML Schema Set.
        /// </summary>
        public XmlSchemaSet  StatusSchemaSet    { get; }

        /// <summary>
        /// DatexII Table XML Schema Set.
        /// </summary>
        public XmlSchemaSet  TableSchemaSet     { get; }

        #endregion

        #region Constructor(s)

        public AXMLSchemaValidation()
        {

            StatusSchemaSet = new XmlSchemaSet();
            StatusSchemaSet.Add("http://datex2.eu/schema/3/d2Payload",             "Documentation\\Status\\DATEXII_3_D2Payload.xsd");
            StatusSchemaSet.Add("http://datex2.eu/schema/3/common",                "Documentation\\Status\\DATEXII_3_Common.xsd");
            StatusSchemaSet.Add("http://datex2.eu/schema/3/energyInfrastructure",  "Documentation\\Status\\DATEXII_3_EnergyInfrastructure.xsd");
            StatusSchemaSet.Add("http://datex2.eu/schema/3/facilities",            "Documentation\\Status\\DATEXII_3_Facilities.xsd");

            TableSchemaSet = new XmlSchemaSet();
            TableSchemaSet.Add("http://datex2.eu/schema/3/d2Payload",             "Documentation\\Table\\DATEXII_3_D2Payload.xsd");
            TableSchemaSet.Add("http://datex2.eu/schema/3/common",                "Documentation\\Table\\DATEXII_3_Common.xsd");
            TableSchemaSet.Add("http://datex2.eu/schema/3/commonExtension",       "Documentation\\Table\\DATEXII_3_CommonExtension.xsd");
            TableSchemaSet.Add("http://datex2.eu/schema/3/energyInfrastructure",  "Documentation\\Table\\DATEXII_3_EnergyInfrastructure.xsd");
            TableSchemaSet.Add("http://datex2.eu/schema/3/facilities",            "Documentation\\Table\\DATEXII_3_Facilities.xsd");
            TableSchemaSet.Add("http://datex2.eu/schema/3/locationExtension",     "Documentation\\Table\\DATEXII_3_LocationExtension.xsd");
            TableSchemaSet.Add("http://datex2.eu/schema/3/locationReferencing",   "Documentation\\Table\\DATEXII_3_LocationReferencing.xsd");

        }

        #endregion


        #region (private)   ValidateStatusSchema (XML, SchemaSet)

        private static Boolean ValidateStatusSchema(String                   XML,
                                                    XmlSchemaSet             SchemaSet,
                                                    out IEnumerable<String>  Warnings,
                                                    out IEnumerable<String>  Errors)
        {

            var warnings  = new List<String>();
            var errors    = new List<String>();

            var settings  = new XmlReaderSettings {
                Schemas          = SchemaSet,
                ValidationType   = ValidationType.Schema,
                ValidationFlags  = XmlSchemaValidationFlags.ReportValidationWarnings   |
                                   XmlSchemaValidationFlags.ProcessIdentityConstraints |
                                   XmlSchemaValidationFlags.AllowXmlAttributes
            };

            settings.ValidationEventHandler += (s, e) => {

                switch (e.Severity)
                {

                    case XmlSeverityType.Warning:
                        warnings.Add(e.Message);
                        break;

                    case XmlSeverityType.Error:
                        errors.  Add(e.Message);
                        break;

                }

            };

            using (var stringReader = new StringReader(XML))
            using (var reader       = XmlReader.Create(stringReader, settings))
            {
                try
                {
                    while (reader.Read()) { }
                }
                catch (XmlException e)
                {
                    errors.Add("XML Exception: " + e.Message);
                }
            }

            Warnings  = warnings;
            Errors    = errors;

            return !Errors.Any();

        }

        #endregion


        #region (protected) ValidateStatusSchema (XML, out Warnings, out Errors)

        /// <summary>
        /// Validate the given XML against the DatexII Status XML schema.
        /// </summary>
        /// <param name="XML">The XML to validate.</param>
        /// <param name="Warnings">Optional warnings.</param>
        /// <param name="Errors">Optional errors.</param>
        protected Boolean ValidateStatusSchema(String                   XML,
                                               out IEnumerable<String>  Warnings,
                                               out IEnumerable<String>  Errors)

            => ValidateStatusSchema(
                   XML,
                   StatusSchemaSet,
                   out Warnings,
                   out Errors
               );

        #endregion

        #region (protected) ValidateTableSchema  (XML, out Warnings, out Errors)

        /// <summary>
        /// Validate the given XML against the DatexII Table XML schema.
        /// </summary>
        /// <param name="XML">A XML to validate.</param>
        /// <param name="Warnings">Optional warnings.</param>
        /// <param name="Errors">Optional errors.</param>
        protected Boolean ValidateTableSchema(String                   XML,
                                              out IEnumerable<String>  Warnings,
                                              out IEnumerable<String>  Errors)

            => ValidateStatusSchema(
                   XML,
                   TableSchemaSet,
                   out Warnings,
                   out Errors
               );

        #endregion

    }

}
