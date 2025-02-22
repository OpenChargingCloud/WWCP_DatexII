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

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// Evaluation of the altitude confidence assessed according to ETSI ISO 102894-2.
    /// </summary>
    public enum AltitudeAccuracies
    {

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 1 centimetre.
        /// </summary>
        [XmlEnum("equalToOrLessThan1Centimetre")]
        EqualToOrLessThan1Centimetre,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 2 centimetres.
        /// </summary>
        [XmlEnum("equalToOrLessThan2Centimetres")]
        EqualToOrLessThan2Centimetres,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 5 centimetres.
        /// </summary>
        [XmlEnum("equalToOrLessThan5Centimetres")]
        EqualToOrLessThan5Centimetres,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 10 centimetres.
        /// </summary>
        [XmlEnum("equalToOrLessThan10Centimetres")]
        EqualToOrLessThan10Centimetres,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 20 centimetres.
        /// </summary>
        [XmlEnum("equalToOrLessThan20Centimetres")]
        EqualToOrLessThan20Centimetres,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 50 centimetres.
        /// </summary>
        [XmlEnum("equalToOrLessThan50Centimetres")]
        EqualToOrLessThan50Centimetres,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 1 metre.
        /// </summary>
        [XmlEnum("equalToOrLessThan1Metre")]
        EqualToOrLessThan1Metre,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 2 metres.
        /// </summary>
        [XmlEnum("equalToOrLessThan2Metres")]
        EqualToOrLessThan2Metres,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 5 metres.
        /// </summary>
        [XmlEnum("equalToOrLessThan5Metres")]
        EqualToOrLessThan5Metres,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 10 metres.
        /// </summary>
        [XmlEnum("equalToOrLessThan10Metres")]
        EqualToOrLessThan10Metres,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 20 metres.
        /// </summary>
        [XmlEnum("equalToOrLessThan20Metres")]
        EqualToOrLessThan20Metres,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 50 metres.
        /// </summary>
        [XmlEnum("equalToOrLessThan50Metres")]
        EqualToOrLessThan50Metres,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 100 metres.
        /// </summary>
        [XmlEnum("equalToOrLessThan100Metres")]
        EqualToOrLessThan100Metres,

        /// <summary>
        /// Indicates if the altitude accuracy is equal to or less than 200 metres.
        /// </summary>
        [XmlEnum("equalToOrLessThan200Metres")]
        EqualToOrLessThan200Metres,

        [XmlEnum("_extended")]
        Extended

    }

}
