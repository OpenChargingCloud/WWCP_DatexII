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

using NUnit.Framework;

using cloud.charging.open.protocols.DatexII.v3;

#endregion

namespace cloud.charging.open.protocols.DatexII.Tests
{

    /// <summary>
    /// Validation against the DATEX II 3.7 schemas the library embeds: table and status publications alike.
    /// </summary>
    [TestFixture]
    public abstract class AXMLSchemaValidation
    {

        #region (protected) ValidateStatusSchema (XML, out Warnings, out Errors)

        /// <summary>
        /// Validate the given XML of a status publication against the DATEX II 3.7 schemas.
        /// </summary>
        /// <param name="XML">The XML to validate.</param>
        /// <param name="Warnings">Optional warnings.</param>
        /// <param name="Errors">Optional errors.</param>
        protected static Boolean ValidateStatusSchema(String                   XML,
                                                      out IEnumerable<String>  Warnings,
                                                      out IEnumerable<String>  Errors)

            => DatexIISchemas.Validate(XElement.Parse(XML), out Errors, out Warnings);

        #endregion

        #region (protected) ValidateTableSchema  (XML, out Warnings, out Errors)

        /// <summary>
        /// Validate the given XML of a table publication against the DATEX II 3.7 schemas.
        /// </summary>
        /// <param name="XML">A XML to validate.</param>
        /// <param name="Warnings">Optional warnings.</param>
        /// <param name="Errors">Optional errors.</param>
        protected static Boolean ValidateTableSchema(String                   XML,
                                                     out IEnumerable<String>  Warnings,
                                                     out IEnumerable<String>  Errors)

            => DatexIISchemas.Validate(XElement.Parse(XML), out Errors, out Warnings);

        #endregion

    }

}
