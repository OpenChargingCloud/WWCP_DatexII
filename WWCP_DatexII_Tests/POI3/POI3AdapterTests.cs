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

using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Aegir;
using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.POI3;
using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.D2Payload;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationExtension;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;
using cloud.charging.open.protocols.WWCP.POI.tests;

using POI     = cloud.charging.open.protocols.WWCP.POI;
using Address = org.GraphDefined.Vanaheimr.Illias.Address;

#endregion

namespace cloud.charging.open.protocols.DatexII.Tests.POI3
{

    /// <summary>
    /// A POI3 roaming network as DATEX II Energy Infrastructure publications, and back.
    /// Every publication made is validated against the schemas.
    /// </summary>
    [TestFixture]
    public class POI3AdapterTests : AXMLSchemaValidation
    {

        #region Data

        private static readonly DateTimeOffset                  Created   = DateTimeOffset.Parse("2026-01-01T00:00:00Z");
        private static readonly DateTimeOffset                  T0        = DateTimeOffset.Parse("2026-10-13T12:00:00Z");

        private static readonly POI.ChargingStationOperator_Id  GEF       = POI.ChargingStationOperator_Id.Parse("DE*GEF");
        private static readonly POI.ChargingPool_Id             P1        = POI.ChargingPool_Id.           Parse("DE*GEF*P1");
        private static readonly POI.ChargingStation_Id          S1        = POI.ChargingStation_Id.        Parse("DE*GEF*S1");
        private static readonly POI.EVSE_Id                     E1        = POI.EVSE_Id.                   Parse("DE*GEF*E1*1");
        private static readonly POI.ChargingTariff_Id           T1        = POI.ChargingTariff_Id.         Parse("DE*GEF*T1");

        private static readonly InternationalIdentifier         Creator   = new (Country.Germany, "DE-NAP-GEF");

        #endregion

        #region (private static) ATariff() / AnEVSE(MaxPower) / APool(...) / ANetwork(WithPools)

        private static POI.ChargingTariff ATariff()

            => new (T1,
                    Currency.EUR,
                    [
                        new POI.ChargingTariffElement([ new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.ENERGY, 0.39m, VAT: 19, StepSize: 1) ]),
                        new POI.ChargingTariffElement([ new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.TIME,   6,     VAT: 19, StepSize: 1),
                                                        new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.FLAT,   1) ],
                                                      new POI.ChargingTariffRestrictions(StartTime:   new TimeOnly(22, 0),
                                                                                         EndTime:     new TimeOnly(6, 30),
                                                                                         StartDate:   new DateOnly(2026, 1, 1),
                                                                                         DaysOfWeek:  [ DayOfWeek.Sunday ]))
                    ],
                    I18NString.Create(Languages.en, "Standard"),
                    Created: Created);

        private static POI.EVSE AnEVSE(Int32 MaxPower = 22000)

            => new (E1,
                    CurrentTypes:        POI.CurrentTypes.AC_ThreePhases,
                    MaxVoltage:          Volt.  FromV(400),
                    MaxCurrent:          Ampere.FromA(32),
                    MaxPower:            Watt.  FromW(MaxPower),
                    ChargingConnectors:  [ new POI.ChargingConnector(POI.ChargingConnector_Id.Parse("1"), POI.ChargingConnectorType.IEC_62196_T2) ],
                    ChargingTariffIds:   [ T1 ],
                    Created:             Created);

        private static POI.ChargingPool APool(Int32 MaxPower = 22000)

            => new (P1,
                    I18NString.Create(Languages.en, "Biberweg"),
                    Address:              new Address("Biberweg", "07749", I18NString.Create(Languages.de, "Jena"), Country.Germany, HouseNumber: "18"),
                    GeoLocation:          GeoCoordinate.Parse(50.92, 11.58),
                    ParkingType:          POI.ParkingType.PARKING_LOT,
                    TimeZone:             Time_Zone.Parse("Europe/Berlin"),
                    Facilities:           [ POI.Facility.CAFE ],
                    AuthenticationModes:  [ POI.AuthenticationMode.RFID(), POI.AuthenticationMode.REMOTE ],
                    LocationLanguages:    [ Languages.de ],
                    OpeningTimes:         new ImmutableOpeningTimes(OpeningTimes.Open24Hours),
                    ChargingStations:     [ new POI.ChargingStation(S1,
                                                                    Features:  [ POI.ChargingStationFeature.Reservable ],
                                                                    EVSEs:     [ AnEVSE(MaxPower) ],
                                                                    Created:   Created) ],
                    Created:              Created);

        private static POI.RoamingNetwork ANetwork(Boolean WithPools = true)

            => new POI.RoamingNetwork(POI.RoamingNetwork_Id.Parse("rn1"),
                                      Created:                   Created,
                                      ChargingStationOperators:  [ new POI.ChargingStationOperator(GEF,
                                                                                                   Name:            I18NString.Create(Languages.en, "GraphDefined"),
                                                                                                   Homepage:        URL.Parse("https://graphdefined.com"),
                                                                                                   ChargingTariffs: WithPools ? [ ATariff() ] : null,
                                                                                                   ChargingPools:   WithPools ? [ APool()   ] : null,
                                                                                                   Created:         Created) ]).WithKeys();

        #endregion

        #region (private) AssertValidTable(Publication) / AssertValidStatus(Publication)

        private XElement AssertValidTable(EnergyInfrastructureTablePublication Publication)
        {

            var xml = Publication.ToXML();

            Assert.That(ValidateTableSchema(xml.ToString(), out var warnings, out var errors), Is.True, String.Join(Environment.NewLine, errors));
            Assert.That(warnings, Is.Empty, String.Join(Environment.NewLine, warnings));

            return xml;

        }

        private XElement AssertValidStatus(EnergyInfrastructureStatusPublication Publication)
        {

            var xml = Publication.ToXML();

            Assert.That(ValidateStatusSchema(xml.ToString(), out var warnings, out var errors), Is.True, String.Join(Environment.NewLine, errors));
            Assert.That(warnings, Is.Empty, String.Join(Environment.NewLine, warnings));

            return xml;

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


        #region APoolBecomesASchemaValidSite()

        [Test]
        public void APoolBecomesASchemaValidSite()
        {

            var warnings     = new List<String>();
            var publication  = POI3Mapper.ToTablePublication(ANetwork(), Creator, Languages.de, "rn1", "1", warnings,
                                                             StationIdBNetzA: station => "BNetzA-4711");

            AssertValidTable(publication);

            var site         = publication.EnergyInfrastructureTables.Single().EnergyInfrastructureSites.Single();
            var location     = (PointLocation) site.LocationReference!;
            var station      = site.EnergyInfrastructureStations.Single();
            var point        = (ElectricChargingPoint) station.RefillPoints.Single();
            var connector    = point.Connector.Single();
            var rate         = point.ElectricEnergy.Single().EnergyRates.Single();

            Assert.Multiple(() => {

                Assert.That(warnings,                                                    Is.Empty);

                Assert.That(site.Id,                                                     Is.EqualTo("DE*GEF*P1"));
                Assert.That(site.Name?.Values.Single().Text,                             Is.EqualTo("Biberweg"));
                Assert.That(site.TypeOfSite?.ToString(),                                 Is.EqualTo("openSpace"));
                Assert.That(site.OperatingHours,                                         Is.InstanceOf<OpenAllHours>());
                Assert.That(((OrganisationSpecification) site.Operator!).OperatorId,     Is.EqualTo("DE*GEF"));
                Assert.That(((SupplementalServiceFacility) site.SupplementalFacilities.Single()).ServiceFacilityType.ToString(), Is.EqualTo("cafe"));
                Assert.That(location.CoordinatesForDisplay?.Latitude,                    Is.EqualTo(50.92));
                Assert.That(location.FacilityLocation?.TimeZone?.ToString(),             Is.EqualTo("+01:00"));
                Assert.That(location.FacilityLocation?.Address?.Postcode,                Is.EqualTo("07749"));
                Assert.That(location.FacilityLocation?.Address?.AddressLines.Select(line => line.Text.Values.Single().Text), Is.EqualTo(new[] { "Biberweg", "18" }));

                Assert.That(station.Id,                                                  Is.EqualTo("DE*GEF*S1"));
                Assert.That(station.StationIdBNetzA,                                     Is.EqualTo("BNetzA-4711"));
                Assert.That(station.TotalMaximumPower.Value,                             Is.EqualTo(22000));
                Assert.That(station.AuthenticationAndIdentificationMethods.Select(method => method.ToString()), Is.EquivalentTo(new[] { "rfid", "apps" }));
                Assert.That(station.UserInterfaceLanguages,                              Is.EqualTo(new[] { Languages.de }));

                Assert.That(point.Id,                                                    Is.EqualTo("DE*GEF*E1*1"));
                Assert.That(point.EVSEId,                                                Is.EqualTo("DE*GEF*E1*1"));
                Assert.That(point.Reservation?.ToString(),                               Is.EqualTo("optional"));
                Assert.That(point.AvailableChargingPower.Single().Value,                 Is.EqualTo(22000));

                Assert.That(connector.ConnectorType.ToString(),                          Is.EqualTo("iec62196T2"));
                Assert.That(connector.ChargingMode?.ToString(),                          Is.EqualTo("mode3AC3p"));
                Assert.That(connector.MaxPowerAtSocket.Value,                            Is.EqualTo(22000));
                Assert.That(connector.MaximumCurrent?.Value,                             Is.EqualTo(32));

                Assert.That(rate.Id,                                                     Is.EqualTo("DE*GEF*T1"));
                Assert.That(rate.RatePolicy.ToString(),                                  Is.EqualTo("adHoc"));
                Assert.That(rate.EnergyPrices.Select(price => price.PriceType.ToString()), Is.EqualTo(new[] { "pricePerKWh", "pricePerMinute", "basePrice" }));
                Assert.That(rate.EnergyPrices.First().Value.Value,                       Is.EqualTo(0.39m));
                Assert.That(rate.EnergyPrices.First().TaxRate?.Value,                    Is.EqualTo(19));
                Assert.That(rate.EnergyPrices.ElementAt(1).Value.Value,                  Is.EqualTo(0.1m));

            });

        }

        #endregion

        #region TheTableComesBackAsTheSameChargingPoolAndTariff()

        [Test]
        public void TheTableComesBackAsTheSameChargingPoolAndTariff()
        {

            var network      = ANetwork();
            var warnings     = new List<String>();
            var xml          = AssertValidTable(POI3Mapper.ToTablePublication(network, Creator, Languages.de, "rn1", "1", warnings, StationIdBNetzA: station => "BNetzA-4711"));

            // Through the parsers: what a consumer reads.
            Assert.That(Payload.TryParseXML(xml, out var payload, out var errorResponse), Is.True, errorResponse);

            var site         = ((EnergyInfrastructureTablePublication) payload!).EnergyInfrastructureTables.Single().EnergyInfrastructureSites.Single();

            Assert.That(POI3Mapper.OperatorIdOf(site), Is.EqualTo(GEF));

            var poolJSON     = site.ToPOI3(GEF, null, Languages.en, warnings);

            Assert.That(poolJSON, Is.Not.Null, String.Join(" ", warnings));
            Assert.That(POI.ChargingPool.TryParse(poolJSON!, out var pool, out var parseError), Is.True, parseError);

            var original     = network.GetChargingStationOperatorById(GEF)!.ChargingPools.Single();
            var evse         = pool!.ChargingStations.Single().EVSEs.Single();

            var tariff       = POI3Mapper.RatesOf(site).Single().ToPOI3(GEF, Languages.en, warnings);

            var expected     = ATariff().ToJSON();
            var actual       = tariff!.ToJSON();

            foreach (var json in new[] { expected, actual })
            {
                json.Remove("created");
                json.Remove("lastChange");
            }

            Assert.Multiple(() => {

                Assert.That(warnings,                                          Is.Empty);

                Assert.That(pool.Id,                                           Is.EqualTo(P1));
                Assert.That(pool.Name?.FirstText(),                            Is.EqualTo("Biberweg"));
                Assert.That(pool.Address?.Street,                              Is.EqualTo("Biberweg"));
                Assert.That(pool.Address?.HouseNumber,                         Is.EqualTo("18"));
                Assert.That(pool.Address?.PostalCode,                          Is.EqualTo("07749"));
                Assert.That(pool.Address?.City.FirstText(),                    Is.EqualTo("Jena"));
                Assert.That(pool.GeoLocation,                                  Is.EqualTo(original.GeoLocation));
                Assert.That(pool.ParkingType,                                  Is.EqualTo(POI.ParkingType.PARKING_LOT));
                Assert.That(pool.Facilities,                                   Is.EqualTo(new[] { POI.Facility.CAFE }));
                Assert.That(pool.OpeningTimes?.IsOpen24Hours,                  Is.True);
                Assert.That(pool.AuthenticationModes.Select(mode => mode.Type), Is.EquivalentTo(new[] { POI.AuthenticationMode.RFIDType, POI.AuthenticationMode.REMOTEType }));
                Assert.That(pool.LocationLanguages,                            Is.EqualTo(new[] { Languages.de }));

                Assert.That(pool.ChargingStations.Single().Id,                 Is.EqualTo(S1));
                Assert.That(pool.ChargingStations.Single().Features,           Is.EqualTo(new[] { POI.ChargingStationFeature.Reservable }));

                Assert.That(evse.Id,                                           Is.EqualTo(E1));
                Assert.That(evse.CurrentTypes,                                 Is.EqualTo(POI.CurrentTypes.AC_ThreePhases));
                Assert.That(evse.MaxPower?.Value,                              Is.EqualTo(22000));
                Assert.That(evse.MaxVoltage?.Value,                            Is.EqualTo(400));
                Assert.That(evse.MaxCurrent?.Value,                            Is.EqualTo(32));
                Assert.That(evse.ChargingConnectors.Single().Type,             Is.EqualTo(POI.ChargingConnectorType.IEC_62196_T2));
                Assert.That(evse.ChargingTariffIds,                            Is.EqualTo(new[] { T1 }));

                Assert.That(JToken.DeepEquals(expected, actual),               Is.True, $"{expected}\n{actual}");

            });

        }

        #endregion

        #region StatusMapsBothWays()

        [Test]
        public void StatusMapsBothWays()
        {
            Assert.Multiple(() => {
                Assert.That(POI3Mapper.ToRefillPointStatus(POI.EVSEStatusType.Charging,  POI.EVSEAdminStatusType.Operational). ToString(),  Is.EqualTo("charging"));
                Assert.That(POI3Mapper.ToRefillPointStatus(POI.EVSEStatusType.Available, POI.EVSEAdminStatusType.Planned).     ToString(),  Is.EqualTo("planned"));
                Assert.That(POI3Mapper.ToRefillPointStatus(POI.EVSEStatusType.Available, POI.EVSEAdminStatusType.OutOfService).ToString(),  Is.EqualTo("inoperative"));
                Assert.That(RefillPointStatusEnum.Parse("outOfOrder").ToPOI3Status().ToRefillPointStatus().ToString(),                      Is.EqualTo("outOfOrder"));
                Assert.That(RefillPointStatusEnum.Parse("reserved").  ToPOI3Status().ToRefillPointStatus().ToString(),                      Is.EqualTo("reserved"));
                Assert.That(RefillPointStatusEnum.Parse("occupied").  ToPOI3Status().ToRefillPointStatus().ToString(),                      Is.EqualTo("occupied"));
            });
        }

        #endregion

        #region TheStatusIsASchemaValidPublication()

        [Test]
        public void TheStatusIsASchemaValidPublication()
        {

            var network      = ANetwork();
            var publication  = POI3Mapper.ToStatusPublication(network, Creator, Languages.de, "rn1", "1");
            var xml          = AssertValidStatus(publication);

            Assert.That(Payload.TryParseXML(xml, out var payload, out var errorResponse), Is.True, errorResponse);

            var siteStatus   = ((EnergyInfrastructureStatusPublication) payload!).EnergyInfrastructureSiteStatus.Single();
            var pointStatus  = siteStatus.EnergyInfrastructureStationStatus.Single().RefillPointStatus.Single();
            var evse         = network.GetChargingStationOperatorById(GEF)!.EVSEs.Single();

            Assert.Multiple(() => {
                Assert.That(siteStatus. Reference.Id,                              Is.EqualTo("DE*GEF*P1"));
                Assert.That(pointStatus.Reference.Id,                              Is.EqualTo("DE*GEF*E1*1"));
                Assert.That(pointStatus.Reference.Version,                         Is.EqualTo(POI3Mapper.VersionOf(evse.LastChangeDate)));
                Assert.That(pointStatus.Status.ToString(),                         Is.EqualTo("available"));
                Assert.That(pointStatus,                                           Is.InstanceOf<ElectricChargingPointStatus>());
                Assert.That(siteStatus.EnergyInfrastructureStationStatus.Single().IsAvailable, Is.True);
            });

        }

        #endregion


        #region ThePublisherFollowsTheHistory()

        [Test]
        public async Task ThePublisherFollowsTheHistory()
        {

            var history   = POI.RoamingNetworkHistory.Start(ANetwork());
            var errors    = new List<String>();
            var tables    = new List<EnergyInfrastructureTablePublication>();
            var deltas    = new List<EnergyInfrastructureStatusPublication>();

            await using var publisher = new POI3ToDatexIIPublisher(history, Creator, Languages.de, StationIdBNetzA: station => "BNetzA-4711");

            publisher.OnError             += (sender, description, warnings) => { lock (errors) errors.Add(description + " " + String.Join(" ", warnings)); };
            publisher.OnTablePublication  += (sender, publication)           => { lock (tables) tables.Add(publication); return Task.CompletedTask; };
            publisher.OnStatusPublication += (sender, publication, isDelta)  => { if (isDelta) lock (deltas) deltas.Add(publication); return Task.CompletedTask; };

            await publisher.StartAsync();

            Assert.Multiple(() => {
                Assert.That(publisher.CurrentTablePublication,   Is.Not.Null);
                Assert.That(publisher.CurrentStatusPublication,  Is.Not.Null);
                Assert.That(publisher.TableVersion,              Is.EqualTo(history.Head.Commit.Revision.ToString()));
                Assert.That(tables,                              Has.Count.EqualTo(1));
            });

            AssertValidTable (publisher.CurrentTablePublication!);
            AssertValidStatus(publisher.CurrentStatusPublication!);

            // A change of the content: a new version of the table...
            var head = history.Head.RoamingNetwork;
            Assert.That(history.Commit(head.CreateSignedChangeSet([ POI.RoamingNetworkChange.UpdateProperty(head.GetChargingStationOperatorById(GEF)!.EVSEs.Single(), "maxPower", null, 11000) ],
                                                                  CreatedAt: T0)).IsCommitted);

            Assert.That(await Eventually(() => { lock (tables) return tables.Count == 2; }), Is.True, "The changed maximum power was not published.");

            var table = tables.Last();
            var point = (ElectricChargingPoint) table.EnergyInfrastructureTables.Single().EnergyInfrastructureSites.Single().EnergyInfrastructureStations.Single().RefillPoints.Single();

            Assert.Multiple(() => {
                Assert.That(table.EnergyInfrastructureTables.Single().Version,  Is.EqualTo(history.Head.Commit.Revision.ToString()));
                Assert.That(point.AvailableChargingPower.Single().Value,        Is.EqualTo(11000));
            });

            // ...and of the status: a delta with the EVSE.
            history.RuntimeStatus.TryUpdate(POI.POIRuntimeStatusKind.Status, nameof(POI.EVSE), E1.ToString(), "charging", T0.AddMinutes(1), out _, out _);

            Assert.That(await Eventually(() => { lock (deltas) return deltas.Count == 1; }), Is.True, "The changed status was not published.");

            var delta = deltas.Single();

            AssertValidStatus(delta);

            Assert.Multiple(() => {
                Assert.That(delta.EnergyInfrastructureSiteStatus.Single().EnergyInfrastructureStationStatus.Single().RefillPointStatus.Single().Status.ToString(), Is.EqualTo("charging"));
                Assert.That(delta.TableReferences.Single().Version,                                                                                                 Is.EqualTo(publisher.TableVersion));
            });

            lock (errors)
                Assert.That(errors, Is.Empty);

        }

        #endregion

        #region TheImporterCommitsWhatItReads()

        [Test]
        public async Task TheImporterCommitsWhatItReads()
        {

            var history   = POI.RoamingNetworkHistory.Start(ANetwork(WithPools: false));
            var errors    = new List<String>();

            await using var importer = new DatexIIToPOI3Importer(history, changeSet => changeSet.SignedByAll(history.Head.RoamingNetwork));

            importer.OnError += (sender, description, warnings) => { lock (errors) errors.Add(description + " " + String.Join(" ", warnings)); };

            // What a national access point would serve: the publications of another POI3 network, as XML.
            var source    = ANetwork();
            var warnings  = new List<String>();

            await importer.ImportAsync(POI3Mapper.ToTablePublication(source, Creator, Languages.de, "rn1", "1", warnings, StationIdBNetzA: station => "BNetzA-4711").ToXML());

            var imported  = history.Head.RoamingNetwork.GetChargingStationOperatorById(GEF)!;

            lock (errors)
                Assert.That(errors, Is.Empty);

            Assert.Multiple(() => {
                Assert.That(imported.ChargingTariffs.Single().Id,                Is.EqualTo(T1));
                Assert.That(imported.ChargingPools.Single().Id,                  Is.EqualTo(P1));
                Assert.That(imported.ChargingPools.Single().Address?.Street,     Is.EqualTo("Biberweg"));
                Assert.That(imported.ChargingStations.Single().Id,               Is.EqualTo(S1));
                Assert.That(imported.EVSEs.Single().Id,                          Is.EqualTo(E1));
                Assert.That(imported.EVSEs.Single().MaxPower?.Value,             Is.EqualTo(22000));
                Assert.That(imported.EVSEs.Single().ChargingTariffIds,           Is.EqualTo(new[] { T1 }));
            });

            // A changed site: only what changed is committed.
            var revision  = history.Head.Commit.Revision;
            var changed   = new POI.RoamingNetwork(source.Id, Created: Created,
                                                   ChargingStationOperators: [ new POI.ChargingStationOperator(GEF,
                                                                                                               Name:            I18NString.Create(Languages.en, "GraphDefined"),
                                                                                                               Homepage:        URL.Parse("https://graphdefined.com"),
                                                                                                               ChargingTariffs: [ ATariff() ],
                                                                                                               ChargingPools:   [ APool(MaxPower: 11000) ],
                                                                                                               Created:         Created) ]);

            await importer.ImportAsync(POI3Mapper.ToTablePublication(changed, Creator, Languages.de, "rn1", "2", warnings, StationIdBNetzA: station => "BNetzA-4711"));

            var changeSet = history.Head.Commit.ChangeSet!;

            Assert.Multiple(() => {
                Assert.That(history.Head.Commit.Revision,                              Is.EqualTo(revision + 1));
                Assert.That(changeSet.Changes.Select(change => change.PropertyName),   Is.EquivalentTo(new[] { "maxPower" }));
            });

            // A changed status.
            var status    = new EnergyInfrastructureStatusPublication(
                                T0.AddMinutes(5),
                                Creator,
                                Languages.de,
                                EnergyInfrastructureSiteStatus: [
                                    new EnergyInfrastructureSiteStatus(
                                        new FacilityObjectVersionedReference(P1.ToString()),
                                        EnergyInfrastructureStationStatus: [
                                            new EnergyInfrastructureStationStatus(
                                                new FacilityObjectVersionedReference(S1.ToString()),
                                                RefillPointStatus: [
                                                    new ElectricChargingPointStatus(RefillPointStatusEnum.Parse("charging"),
                                                                                    new FacilityObjectVersionedReference(E1.ToString()),
                                                                                    LastUpdated: T0.AddMinutes(5))
                                                ]
                                            )
                                        ]
                                    )
                                ]
                            );

            await importer.ImportAsync(AssertValidStatus(status));

            Assert.That(history.RuntimeStatus.TryGet(POI.POIRuntimeStatusKind.Status, nameof(POI.EVSE), E1.ToString(), out var current) &&
                        current.Value == "charging",
                        Is.True);

            lock (errors)
                Assert.That(errors, Is.Empty);

        }

        #endregion

        #region TheOfficialExampleBecomesAChargingPool()

        [Test]
        public void TheOfficialExampleBecomesAChargingPool()
        {

            var xml       = XDocument.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Examples", "EnergyInfrastructureTablePublication.xml"))).Root!;

            Assert.That(Payload.TryParseXML(xml, out var payload, out var errorResponse), Is.True, errorResponse);

            var site      = ((EnergyInfrastructureTablePublication) payload!).EnergyInfrastructureTables.Single().EnergyInfrastructureSites.First();
            var operatorId = POI3Mapper.OperatorIdOf(site);

            // The example has a placeholder as the operator identification, but EVSE identifications of DE*ABC.
            Assert.That(operatorId, Is.EqualTo(POI.ChargingStationOperator_Id.Parse("DE*ABC")));

            var warnings  = new List<String>();
            var poolJSON  = site.ToPOI3(operatorId!.Value, null, Languages.de, warnings);

            Assert.That(poolJSON, Is.Not.Null, String.Join(" ", warnings));
            Assert.That(POI.ChargingPool.TryParse(poolJSON!, out var pool, out var parseError), Is.True, parseError);

            Assert.Multiple(() => {
                Assert.That(pool!.Address?.City.FirstText(),               Is.EqualTo("Musterstadt"));
                Assert.That(pool.Address?.Street,                          Is.EqualTo("Hauptstraße"));
                Assert.That(pool.ChargingStations.Count(),                 Is.EqualTo(site.EnergyInfrastructureStations.Count()));
                Assert.That(pool.EVSEs.Select(evse => evse.Id.ToString()), Has.Member("DE*ABC*EXXXXXXXX12345"));
            });

        }

        #endregion

    }

}
