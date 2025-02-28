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

#endregion

namespace cloud.charging.open.protocols.DatexII.Tests
{

    /// <summary>
    /// Unit tests for validating DatexII Example XMLs via their XML schema.
    /// </summary>
    [TestFixture]
    public class LoadAndValidate_DatexIIExamples : AXMLSchemaValidation
    {

        #region EnergyInfrastructure_StatusPublication()

        /// <summary>
        /// A test for validating the Energy Infrastructure Status Publication example via the DatexII XML schema.
        /// </summary>
        [Test]
        public void EnergyInfrastructure_StatusPublication()
        {

            var xml        = XDocument.Parse(File.ReadAllText("Examples/EnergyInfrastructureStatusPublication.xml"));
            Assert.That(xml,       Is.Not.Null);
            Assert.That(xml.Root,  Is.Not.Null);

            var isValidXML = ValidateStatusSchema(xml.ToString(), out var warning, out var errors);
            Assert.That(isValidXML,       Is.True);
            Assert.That(warning.Count(),  Is.EqualTo(0));
            Assert.That(errors. Count(),  Is.EqualTo(0));

        }

        #endregion

        #region EnergyInfrastructure_TablePublication()

        /// <summary>
        /// A test for validating the Energy Infrastructure Table Publication example via the DatexII XML schema.
        /// </summary>
        [Test]
        public void EnergyInfrastructure_TablePublication()
        {

            var xml        = XDocument.Parse(File.ReadAllText("Examples/EnergyInfrastructureTablePublication.xml"));
            Assert.That(xml,       Is.Not.Null);
            Assert.That(xml.Root,  Is.Not.Null);

            var isValidXML = ValidateTableSchema(xml.ToString(), out var warning, out var errors);
            Assert.That(isValidXML,       Is.True);
            Assert.That(warning.Count(),  Is.EqualTo(0));
            Assert.That(errors. Count(),  Is.EqualTo(0));

        }

        #endregion

    }

}
