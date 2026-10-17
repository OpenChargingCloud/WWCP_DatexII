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

using org.GraphDefined.Vanaheimr.Aegir;
using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.POI3;
using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;
using cloud.charging.open.protocols.WWCP.POI.tests;

using POI     = cloud.charging.open.protocols.WWCP.POI;
using Address = org.GraphDefined.Vanaheimr.Illias.Address;

#endregion

namespace cloud.charging.open.protocols.DatexII.Tests.POI3
{

    /// <summary>
    /// Parking spaces whose charging station, dimensions and roof, and an EVSE whose tariff, only their groups give:
    /// the DATEX II station has them, as they are at runtime.
    /// </summary>
    [TestFixture]
    public class POI3GroupAdapterTests
    {

        #region Data

        private static readonly DateTimeOffset                  Created  = DateTimeOffset.Parse("2026-01-01T00:00:00Z");

        private static readonly POI.ChargingStationOperator_Id  GEF      = POI.ChargingStationOperator_Id.Parse("DE*GEF");
        private static readonly POI.ChargingPool_Id             P1       = POI.ChargingPool_Id.           Parse("DE*GEF*P1");
        private static readonly POI.ChargingStation_Id          S1       = POI.ChargingStation_Id.        Parse("DE*GEF*S1");
        private static readonly POI.ChargingTariff_Id           T1       = POI.ChargingTariff_Id.         Parse("DE*GEF*T1");

        private static readonly InternationalIdentifier         Creator  = new (Country.Germany, "DE-NAP-GEF");

        #endregion

        #region NewNetwork()

        private static POI.RoamingNetwork NewNetwork()

            => new POI.RoamingNetwork(POI.RoamingNetwork_Id.Parse("rn1"),
                   Created:                   Created,
                   ChargingStationOperators:  [ new POI.ChargingStationOperator(GEF,
                                                    Created:             Created,
                                                    ChargingTariffs:     [ new POI.ChargingTariff(T1,
                                                                                                  Currency.EUR,
                                                                                                  [ new POI.ChargingTariffElement([ new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.ENERGY, 0.39m, VAT: 19, StepSize: 1) ]) ],
                                                                                                  Created: Created) ],
                                                    ChargingPools:       [ new POI.ChargingPool(P1,
                                                                                                I18NString.Create(Languages.en, "Biberweg"),
                                                                                                Address:           new Address("Biberweg", "07749", I18NString.Create(Languages.de, "Jena"), Country.Germany, HouseNumber: "18"),
                                                                                                GeoLocation:       GeoCoordinate.Parse(50.92, 11.58),
                                                                                                TimeZone:          Time_Zone.Parse("Europe/Berlin"),
                                                                                                Created:           Created,
                                                                                                ChargingStations:  [ new POI.ChargingStation(S1,
                                                                                                                                             Created:  Created,
                                                                                                                                             EVSEs:    [ new POI.EVSE(POI.EVSE_Id.Parse("DE*GEF*E1*1"),
                                                                                                                                                                      CurrentTypes:        POI.CurrentTypes.AC_ThreePhases,
                                                                                                                                                                      MaxPower:            Watt.FromW(22000),
                                                                                                                                                                      ChargingConnectors:  [ new POI.ChargingConnector(POI.ChargingConnector_Id.Parse("1"), POI.ChargingConnectorType.IEC_62196_T2, Format: POI.ChargingConnectorFormat.CABLE) ],
                                                                                                                                                                      Created:             Created) ]) ]) ],
                                                    ChargingPoolGroups:  [ new POI.ChargingPoolGroup(POI.ChargingPoolGroup_Id.Parse("DE*GEF*PG1"),
                                                                                                     ChargingPoolIds:   [ P1 ],
                                                                                                     ChargingTariffId:  T1,
                                                                                                     Created:           Created) ]) ],
                   ParkingOperators:          [ new POI.ParkingOperator(POI.ParkingOperator_Id.Parse("DE*PAR"),
                                                    Created:             Created,
                                                    ParkingSpaces:       [ new POI.ParkingSpace(POI.ParkingSpace_Id.Parse("space-1"), Created: Created),
                                                                           new POI.ParkingSpace(POI.ParkingSpace_Id.Parse("space-2"), Created: Created) ],
                                                    ParkingSpaceGroups:  [ new POI.ParkingSpaceGroup(POI.ParkingSpaceGroup_Id.Parse("level-1"),
                                                                                                     ParkingSpaceIds:     [ POI.ParkingSpace_Id.Parse("space-1"), POI.ParkingSpace_Id.Parse("space-2") ],
                                                                                                     ChargingStationIds:  [ S1 ],
                                                                                                     Length:              Meter.From_m(5),
                                                                                                     Width:               Meter.From_m(3.5m),
                                                                                                     Roofed:              true,
                                                                                                     Created:             Created) ]) ]).WithKeys();

        #endregion


        #region WhatGroupsGiveIsPublished()

        [Test]
        public void WhatGroupsGiveIsPublished()
        {

            var warnings     = new List<String>();
            var publication  = POI3Mapper.ToTablePublication(NewNetwork(), Creator, Languages.de, "rn1", "1", warnings, StationIdBNetzA: station => "BNetzA-4711");

            var station      = publication.EnergyInfrastructureTables.Single().EnergyInfrastructureSites.Single().EnergyInfrastructureStations.Single();
            var point        = (ElectricChargingPoint) station.RefillPoints.Single();
            var parking      = station.DedicatedParkingSpaces.Single();

            Assert.Multiple(() => {

                Assert.That(warnings,                                                     Is.Empty);

                // The tariff of the charging pool group, as the EVSE has none of its own.
                Assert.That(point.ElectricEnergy.Single().EnergyRates.Single().Id,       Is.EqualTo(T1.ToString()));

                // The parking spaces name the charging station by their group only, and are alike.
                Assert.That(parking.NumberOfSpaces,                                       Is.EqualTo(2));
                Assert.That(parking.Dimension?.Length?.m,                                 Is.EqualTo(5));
                Assert.That(parking.Dimension?.Width?.m,                                  Is.EqualTo(3.5m));

            });

        }

        #endregion

    }

}
