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

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    public enum EUVehicleCategories
    {

        /// <summary>
        /// Mopeds, Motorcycles, Motor Tricycles and Quadricycles.
        /// </summary>
        [XmlEnum("l")]
        L,

        /// <summary>
        /// Light two-wheel powered vehicle.
        /// </summary>
        [XmlEnum("l1")]
        L1,

        /// <summary>
        /// Three-wheel moped.
        /// </summary>
        [XmlEnum("l2")]
        L2,

        /// <summary>
        /// Two-wheel motorcycle.
        /// </summary>
        [XmlEnum("l3")]
        L3,

        /// <summary>
        /// Two-wheel motorcycle with side-car.
        /// </summary>
        [XmlEnum("l4")]
        L4,

        /// <summary>
        /// Powered tricycle.
        /// </summary>
        [XmlEnum("l5")]
        L5,

        /// <summary>
        /// Light quadricycle.
        /// </summary>
        [XmlEnum("l6")]
        L6,

        /// <summary>
        /// Heavy quadricycle.
        /// </summary>
        [XmlEnum("l7")]
        L7,

        /// <summary>
        /// Motor vehicles with at least four wheels designed and constructed for the carriage of passengers.
        /// </summary>
        [XmlEnum("m")]
        M,

        /// <summary>
        /// Trailers, the sum of the technically permissible masses per axle of which does not exceed 1,500 kg.
        /// </summary>
        [XmlEnum("r1")]
        R1,

        /// <summary>
        /// Trailers, the sum of the technically permissible masses per axle of which exceeds 1,500 kg but does not exceed 3,500 kg.
        /// </summary>
        [XmlEnum("r2")]
        R2,

        /// <summary>
        /// Trailers, the sum of the technically permissible masses per axle of which exceeds 3,500 kg but does not exceed 21,000 kg.
        /// </summary>
        [XmlEnum("r3")]
        R3,

        /// <summary>
        /// Trailers, the sum of the technically permissible masses per axle of which exceeds 21,000 kg.
        /// </summary>
        [XmlEnum("r4")]
        R4,

        /// <summary>
        /// Wheeled tractors with the closest axle to the driver having a minimum track width of not less than 1,500 mm; with an unladen mass in running order of more than 600 kg and with a ground clearance of not more than 1,000 mm.
        /// </summary>
        [XmlEnum("t1")]
        T1,

        /// <summary>
        /// Vehicle category t2 according to Regulation(EU) No 167/2013.
        /// </summary>
        [XmlEnum("t2")]
        T2,

        /// <summary>
        /// Wheeled tractors with unladen mass in running order of not more than 600 kg.
        /// </summary>
        [XmlEnum("t3")]
        T3,

        /// <summary>
        /// Special purpose wheeled tractors with a maximum design speed of not more than 40 km/h.
        /// </summary>
        [XmlEnum("t4")]
        T4,

        /// <summary>
        /// Category T4.1: High clearance tractors.
        /// </summary>
        [XmlEnum("t41")]
        T41,

        /// <summary>
        /// Category T4.2: Extra-wide tractors.
        /// </summary>
        [XmlEnum("t42")]
        T42,

        /// <summary>
        /// Category T4.3: Low clearance tractors.
        /// </summary>
        [XmlEnum("t43")]
        T43,

        /// <summary>
        /// Vehicles designed and constructed for the carriage of passengers and comprising no more than eight seats in addition to the driver’s seat.
        /// </summary>
        [XmlEnum("m1")]
        M1,

        /// <summary>
        /// Vehicles designed and constructed for the carriage of passengers, comprising more than eight seats in addition to the driver’s seat, and having a maximum mass not exceeding 5 tonnes.
        /// </summary>
        [XmlEnum("m2")]
        M2,

        /// <summary>
        /// Vehicles designed and constructed for the carriage of passengers, comprising more than eight seats in addition to the driver’s seat, and having a maximum mass exceeding 5 tonnes.
        /// </summary>
        [XmlEnum("m3")]
        M3,

        /// <summary>
        /// Motor vehicles with at least four wheels designed and constructed for the carriage of goods.
        /// </summary>
        [XmlEnum("n")]
        N,

        /// <summary>
        /// Vehicles designed and constructed for the carriage of goods and having a maximum mass not exceeding 3,5 tonnes.
        /// </summary>
        [XmlEnum("n1")]
        N1,

        /// <summary>
        /// Light commercial vehicles with ≤ 1,305 kg reference mass.
        /// </summary>
        [XmlEnum("n1ClassI")]
        N1ClassI,

        /// <summary>
        /// Light commercial vehicles with 1,305–1,760 kg reference mass.
        /// </summary>
        [XmlEnum("n1ClassII")]
        N1ClassII,

        /// <summary>
        /// N1 Class III.
        /// </summary>
        [XmlEnum("n1ClassIII")]
        N1ClassIII,

        /// <summary>
        /// Light commercial vehicles > 1,760 kg reference mass and max 3,500 kg.
        /// </summary>
        [XmlEnum("n1ClassIIIAndN2")]
        N1ClassIIIAndN2,

        /// <summary>
        /// Vehicles designed and constructed for the carriage of goods and having a maximum mass exceeding 3,5 tonnes but not exceeding 12 tonnes.
        /// </summary>
        [XmlEnum("n2")]
        N2,

        /// <summary>
        /// Vehicles designed and constructed for the carriage of goods and having a maximum mass exceeding 12 tonnes.
        /// </summary>
        [XmlEnum("n3")]
        N3,

        /// <summary>
        /// Trailers (including semi-trailers).
        /// </summary>
        [XmlEnum("o")]
        O,

        /// <summary>
        /// Trailers with a maximum mass not exceeding 0,75 tonnes.
        /// </summary>
        [XmlEnum("o1")]
        O1,

        /// <summary>
        /// Trailers with a maximum mass exceeding 0,75 tonnes but not exceeding 3,5 tonnes.
        /// </summary>
        [XmlEnum("o2")]
        O2,

        /// <summary>
        /// Trailers with a maximum mass exceeding 3,5 tonnes but not exceeding 10 tonnes.
        /// </summary>
        [XmlEnum("o3")]
        O3,

        /// <summary>
        /// Trailers with a maximum mass exceeding 10 tonnes.
        /// </summary>
        [XmlEnum("o4")]
        O4,

        /// <summary>
        /// Some other vehicle category.
        /// </summary>
        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
