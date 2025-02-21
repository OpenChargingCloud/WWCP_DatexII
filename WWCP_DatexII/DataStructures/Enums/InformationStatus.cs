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

using System.Xml.Serialization;

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// Status of the related information (i.e. real, test or exercise).
    /// </summary>
    public enum InformationStatus
    {

        /// <summary>
        /// The information is real. It is not a test or exercise.
        /// </summary>
        [XmlEnum("real")]
        Real,

        /// <summary>
        /// The information is part of an exercise which is for testing security.
        /// </summary>
        [XmlEnum("securityExercise")]
        SecurityExercise,

        /// <summary>
        /// The information is part of an exercise which includes tests of associated technical subsystems.
        /// </summary>
        [XmlEnum("technicalExercise")]
        TechnicalExercise,

        /// <summary>
        /// The information is part of a test for checking the exchange of this type of information.
        /// </summary>
        [XmlEnum("test")]
        Test,

        [XmlEnum("_extended")]
        Extended

    }

}
