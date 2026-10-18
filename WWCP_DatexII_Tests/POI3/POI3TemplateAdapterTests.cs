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
    /// Parking spaces whose charging station, dimensions and roof, and an EVSE whose tariff, only their templates give:
    /// the DATEX II station has them, as they are at runtime.
    /// </summary>
    [TestFixture]
    public class POI3TemplateAdapterTests
    {

        #region Data

        private static readonly DateTimeOffset                  Created  = DateTimeOffset.Parse("2026-01-01T00:00:00Z");

        private static readonly POI.ChargingStationOperator_Id  GEF      = POI.ChargingStationOperator_Id.Parse("DE*GEF");
        private static readonly POI.ChargingPool_Id             P1       = POI.ChargingPool_Id.           Parse("DE*GEF*P1");
        private static readonly POI.ChargingStation_Id          S1       = POI.ChargingStation_Id.        Parse("DE*GEF*S1");
        private static readonly POI.ChargingTariff_Id           T1       = POI.ChargingTariff_Id.         Parse("DE*GEF*T1");
        private static readonly POI.ChargingTariff_Id           T2       = POI.ChargingTariff_Id.         Parse("DE*GEF*T2");
        private static readonly POI.EVSE_Id                     E1       = POI.EVSE_Id.                   Parse("DE*GEF*E1*1");
        private static readonly POI.EVSE_Id                     E2       = POI.EVSE_Id.                   Parse("DE*GEF*E1*2");

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
                                                                                                TemplateIds:       [ POI.ChargingPoolTemplate_Id.Parse("DE*GEF*PT1") ],
                                                                                                Created:           Created,
                                                                                                ChargingStations:  [ new POI.ChargingStation(S1,
                                                                                                                                             Created:  Created,
                                                                                                                                             EVSEs:    [ new POI.EVSE(POI.EVSE_Id.Parse("DE*GEF*E1*1"),
                                                                                                                                                                      CurrentTypes:        POI.CurrentTypes.AC_ThreePhases,
                                                                                                                                                                      MaxPower:            Watt.FromW(22000),
                                                                                                                                                                      ChargingConnectors:  [ new POI.ChargingConnector(POI.ChargingConnector_Id.Parse("1"), POI.ChargingConnectorType.IEC_62196_T2, Format: POI.ChargingConnectorFormat.CABLE) ],
                                                                                                                                                                      Created:             Created) ]) ]) ],
                                                    ChargingPoolTemplates:  [ new POI.ChargingPoolTemplate(POI.ChargingPoolTemplate_Id.Parse("DE*GEF*PT1"),
                                                                                                           ChargingTariffId:  T1,
                                                                                                           Created:           Created) ]) ],
                   ParkingOperators:          [ new POI.ParkingOperator(POI.ParkingOperator_Id.Parse("DE*PAR"),
                                                    Created:                Created,
                                                    ParkingSpaces:          [ new POI.ParkingSpace(POI.ParkingSpace_Id.Parse("space-1"), TemplateIds: [ POI.ParkingSpaceTemplate_Id.Parse("level-1") ], Created: Created),
                                                                              new POI.ParkingSpace(POI.ParkingSpace_Id.Parse("space-2"), TemplateIds: [ POI.ParkingSpaceTemplate_Id.Parse("level-1") ], Created: Created) ],
                                                    ParkingSpaceTemplates:  [ new POI.ParkingSpaceTemplate(POI.ParkingSpaceTemplate_Id.Parse("level-1"),
                                                                                                     ChargingStationIds:  [ S1 ],
                                                                                                     Length:              Meter.From_m(5),
                                                                                                     Width:               Meter.From_m(3.5m),
                                                                                                     Roofed:              true,
                                                                                                     Created:             Created) ]) ]).WithKeys();

        #endregion

        #region TwoEVSEs(TemplateOpeningTimes = null)

        /// <summary>
        /// A charging pool whose template gives the tariff T1, and the opening times, if given: the first EVSE takes the
        /// tariff of the template, the second one has T2 of its own.
        /// </summary>
        private static POI.RoamingNetwork TwoEVSEs(ImmutableOpeningTimes? TemplateOpeningTimes = null)
        {

            static POI.ChargingTariff Tariff(POI.ChargingTariff_Id Id)

                => new (Id, Currency.EUR, [ new POI.ChargingTariffElement([ new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.ENERGY, 0.39m, VAT: 19, StepSize: 1) ]) ], Created: Created);

            static POI.EVSE EVSE(POI.EVSE_Id Id, POI.ChargingTariff_Id? TariffId)

                => new (Id,
                        CurrentTypes:        POI.CurrentTypes.AC_ThreePhases,
                        MaxPower:            Watt.FromW(22000),
                        ChargingConnectors:  [ new POI.ChargingConnector(POI.ChargingConnector_Id.Parse("1"), POI.ChargingConnectorType.IEC_62196_T2, Format: POI.ChargingConnectorFormat.CABLE) ],
                        ChargingTariffIds:   TariffId.HasValue ? [ TariffId.Value ] : null,
                        Created:             Created);

            return new POI.RoamingNetwork(POI.RoamingNetwork_Id.Parse("rn1"),
                       Created:                   Created,
                       ChargingStationOperators:  [ new POI.ChargingStationOperator(GEF,
                                                        Created:             Created,
                                                        ChargingTariffs:     [ Tariff(T1), Tariff(T2) ],
                                                        ChargingPools:       [ new POI.ChargingPool(P1,
                                                                                                    I18NString.Create(Languages.en, "Biberweg"),
                                                                                                    Address:           new Address("Biberweg", "07749", I18NString.Create(Languages.de, "Jena"), Country.Germany, HouseNumber: "18"),
                                                                                                    GeoLocation:       GeoCoordinate.Parse(50.92, 11.58),
                                                                                                    TimeZone:          Time_Zone.Parse("Europe/Berlin"),
                                                                                                    TemplateIds:       [ POI.ChargingPoolTemplate_Id.Parse("DE*GEF*PT1") ],
                                                                                                    Created:           Created,
                                                                                                    ChargingStations:  [ new POI.ChargingStation(S1, Created: Created, EVSEs: [ EVSE(E1, null), EVSE(E2, T2) ]) ]) ],
                                                        ChargingPoolTemplates:  [ new POI.ChargingPoolTemplate(POI.ChargingPoolTemplate_Id.Parse("DE*GEF*PT1"),
                                                                                                               ChargingTariffId:  T1,
                                                                                                               OpeningTimes:      TemplateOpeningTimes,
                                                                                                               Created:           Created) ]) ]).WithKeys();

        }

        #endregion

        #region (private static) Eventually(Condition)

        private static async Task<Boolean> Eventually(Func<Boolean> Condition)
        {

            for (var i = 0; i < 200; i++)
            {
                if (Condition())
                    return true;
                await Task.Delay(25);
            }

            return Condition();

        }

        #endregion


        #region WhatTemplatesGiveIsPublished()

        [Test]
        public void WhatTemplatesGiveIsPublished()
        {

            var warnings     = new List<String>();
            var publication  = POI3Mapper.ToTablePublication(NewNetwork(), Creator, Languages.de, "rn1", "1", warnings, StationIdBNetzA: station => "BNetzA-4711");

            var station      = publication.EnergyInfrastructureTables.Single().EnergyInfrastructureSites.Single().EnergyInfrastructureStations.Single();
            var point        = (ElectricChargingPoint) station.RefillPoints.Single();
            var parking      = station.DedicatedParkingSpaces.Single();

            Assert.Multiple(() => {

                Assert.That(warnings,                                                     Is.Empty);

                // The tariff of the charging pool template, as the EVSE has none of its own.
                Assert.That(point.ElectricEnergy.Single().EnergyRates.Single().Id,       Is.EqualTo(T1.ToString()));

                // The parking spaces name the charging station by their template only, and are alike.
                Assert.That(parking.NumberOfSpaces,                                       Is.EqualTo(2));
                Assert.That(parking.Dimension?.Length?.m,                                 Is.EqualTo(5));
                Assert.That(parking.Dimension?.Width?.m,                                  Is.EqualTo(3.5m));

            });

        }

        #endregion

        #region EnergyRateUpdatesAreOfTheTariffsAnEVSETakes()

        /// <summary>
        /// New prices of the charging pool for both tariffs: each EVSE tells those of the tariff it takes, the first one
        /// the one of the template of its pool, the second one its own; not those of the other one, which none of its energy
        /// rates in the table names.
        /// </summary>
        [Test]
        public void EnergyRateUpdatesAreOfTheTariffsAnEVSETakes()
        {

            var history  = POI.RoamingNetworkHistory.Start(TwoEVSEs());

            static POI.ChargingTariffPrices Prices(POI.ChargingTariff_Id TariffId, Decimal PerKWh)

                => new (TariffId, [ new POI.ChargingTariffElement([ new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.ENERGY, PerKWh, VAT: 19, StepSize: 1) ]) ]);

            Assert.That(history.RuntimeStatus.TryUpdate(POI.POIRuntimeStatusKind.PriceStatus, nameof(POI.ChargingPool), P1.ToString(),
                                                        new POI.ChargingPrices([ Prices(T1, 0.59m), Prices(T2, 0.69m) ]).Text,
                                                        Created.AddDays(1), out _, out var error),
                        Is.EqualTo(POI.POIStatusUpdateResult.Updated), error);

            var points   = POI3Mapper.ToStatusPublication(history.Head.RoamingNetwork, Creator, Languages.de, "rn1", "1").
                               EnergyInfrastructureSiteStatus.Single().
                               EnergyInfrastructureStationStatus.Single().
                               RefillPointStatus.
                               Cast<ElectricChargingPointStatus>().
                               ToDictionary(point => point.Reference.Id);

            Assert.Multiple(() => {
                Assert.That(points[E1.ToString()].EnergyRateUpdates.Select(update => update.EnergyRateReference.Id),  Is.EqualTo(new[] { T1.ToString() }));
                Assert.That(points[E2.ToString()].EnergyRateUpdates.Select(update => update.EnergyRateReference.Id),  Is.EqualTo(new[] { T2.ToString() }));
            });

        }

        #endregion

        #region ExceptionalClosingsOfATemplateArePublishedInTime()

        /// <summary>
        /// An exceptional closing the template of a charging pool gives it: the publisher publishes a delta when it begins
        /// and when it ends, as of one of its own.
        /// </summary>
        [Test]
        public async Task ExceptionalClosingsOfATemplateArePublishedInTime()
        {

            var now        = Timestamp.Now;
            var history    = POI.RoamingNetworkHistory.Start(TwoEVSEs(new ImmutableOpeningTimes(new OpeningTimes().AddExceptionalClosing(now.AddSeconds(1.5), now.AddSeconds(3)))));
            var deltas     = new List<EnergyInfrastructureStatusPublication>();

            await using var publisher = new POI3ToDatexIIPublisher(history, Creator, Languages.de, StationIdBNetzA: station => "BNetzA-4711");

            publisher.OnStatusPublication += (sender, publication, isDelta) => { if (isDelta) lock (deltas) deltas.Add(publication); return Task.CompletedTask; };

            await publisher.StartAsync();

            Assert.That(await Eventually(() => { lock (deltas) return deltas.Count >= 1; }), Is.True, "The beginning closing was not published.");
            Assert.That(await Eventually(() => { lock (deltas) return deltas.Count >= 2; }), Is.True, "The ending closing was not published.");

            lock (deltas)
                Assert.Multiple(() => {
                    Assert.That(deltas[0].EnergyInfrastructureSiteStatus.Single().OpeningStatus?.ToString(),  Is.EqualTo("temporarilyClosed"));
                    Assert.That(deltas[1].EnergyInfrastructureSiteStatus.Single().OpeningStatus,              Is.Null);
                });

        }

        #endregion

    }

}
