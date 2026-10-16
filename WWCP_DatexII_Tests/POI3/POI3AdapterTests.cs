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
using cloud.charging.open.protocols.DatexII.v3;

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
                                                                                         MinEnergy:   2000,
                                                                                         MaxDuration: 3600,
                                                                                         DaysOfWeek:  [ DayOfWeek.Sunday ]))
                    ],
                    I18NString.Create(Languages.en, "Standard"),
                    PaymentMeans:   [ POI.PaymentMeans.QR_CODE, POI.PaymentMeans.CREDIT_CARD, POI.PaymentMeans.Parse("SMS") ],
                    PaymentBrands:  [ POI.PaymentBrand.VISA, POI.PaymentBrand.Parse("Google Pay") ],
                    ValidFrom:      DateTimeOffset.Parse("2026-02-01T00:00:00Z"),
                    ValidUntil:     DateTimeOffset.Parse("2027-01-01T00:00:00Z"),
                    Created:        Created);

        private static POI.EVSE AnEVSE(Int32 MaxPower = 22000)

            => new (E1,
                    CurrentTypes:        POI.CurrentTypes.AC_ThreePhases,
                    MaxVoltage:          Volt.  FromV(400),
                    MaxCurrent:          Ampere.FromA(32),
                    MaxPower:            Watt.  FromW(MaxPower),
                    ChargingConnectors:  [ new POI.ChargingConnector(POI.ChargingConnector_Id.Parse("1"), POI.ChargingConnectorType.IEC_62196_T2, Format: POI.ChargingConnectorFormat.CABLE) ],
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
                    Services:             [ POI.LocationService.ACCESSIBLE_CHARGING, POI.LocationService.ACCESSIBLE_TOILETS ],
                    NUTSCodes:            [ POI.NUTSCode.Parse("DE"), POI.NUTSCode.Parse("DEG"), POI.NUTSCode.Parse("DEG03") ],
                    Features:             [ POI.ChargingPoolFeature.Roofed, POI.ChargingPoolFeature.IlluminatedParkingNearby ],
                    ChargingStations:     [ new POI.ChargingStation(S1,
                                                                    Features:             [ POI.ChargingStationFeature.Reservable, POI.ChargingStationFeature.Illuminated ],
                                                                    EVSEs:                [ AnEVSE(MaxPower) ],
                                                                    EUVehicleCategories:  [ POI.EUVehicleCategory.M1, POI.EUVehicleCategory.N1, POI.EUVehicleCategory.L ],
                                                                    Created:              Created) ],
                    Created:              Created);

        private static POI.RoamingNetwork ANetwork(Boolean WithPools = true)

            => new POI.RoamingNetwork(POI.RoamingNetwork_Id.Parse("rn1"),
                                      Created:                   Created,
                                      ChargingStationOperators:  [ new POI.ChargingStationOperator(GEF,
                                                                                                   Name:            I18NString.Create(Languages.en, "GraphDefined"),
                                                                                                   Homepage:        URL.Parse("https://graphdefined.com"),
                                                                                                   ChargingTariffs: WithPools ? [ ATariff() ] : null,
                                                                                                   ChargingPools:   WithPools ? [ APool()   ] : null,
                                                                                                   Created:         Created) ],
                                      ParkingOperators:          WithPools
                                                                     ? [ new POI.ParkingOperator(POI.ParkingOperator_Id.Parse("DE*PAR"),
                                                                                                 I18NString.Create(Languages.en, "Parking"),
                                                                                                 ParkingSpaces: [
                                                                                                     new POI.ParkingSpace(POI.ParkingSpace_Id.Parse("space-1"),
                                                                                                                          ChargingStationIds:   [ S1 ],
                                                                                                                          ParkingRestrictions:  [ POI.ParkingRestrictionGroup.DISABLED, POI.ParkingRestrictionGroup.EV_ONLY ],
                                                                                                                          EUVehicleCategories:  [ POI.EUVehicleCategory.M1 ],
                                                                                                                          GeoLocation:          GeoCoordinate.Parse(50.9201, 11.5801),
                                                                                                                          Length:               Meter.From_m(5),
                                                                                                                          Width:                Meter.From_m(3.5m),
                                                                                                                          MaxVehicleHeight:     Meter.From_m(2.1m),
                                                                                                                          Roofed:               true,
                                                                                                                          Created:              Created),
                                                                                                     new POI.ParkingSpace(POI.ParkingSpace_Id.Parse("space-2"),
                                                                                                                          ChargingStationIds:   [ S1 ],
                                                                                                                          Created:              Created),
                                                                                                     new POI.ParkingSpace(POI.ParkingSpace_Id.Parse("space-3"),
                                                                                                                          ChargingStationIds:   [ S1 ],
                                                                                                                          Created:              Created),
                                                                                                     new POI.ParkingSpace(POI.ParkingSpace_Id.Parse("space-4"),
                                                                                                                          Created:              Created)
                                                                                                 ],
                                                                                                 Created:       Created) ]
                                                                     : null).WithKeys();

        #endregion

        #region (private) AssertValidTable(Publication) / AssertValidStatus(Publication)

        private XElement AssertValidTable(EnergyInfrastructureTablePublication Publication)
        {

            var xml = Publication.ToXML();

            Assert.That(ValidateTableSchema(xml.ToString(), out var warnings, out var errors), Is.True, String.Join(Environment.NewLine, errors));
            Assert.That(warnings, Is.Empty, String.Join(Environment.NewLine, warnings));

            AssertValidJSON(Publication.ToJSON(), Publication.ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00), JSONSchemas.Mobilithek_Table);

            return xml;

        }

        private XElement AssertValidStatus(EnergyInfrastructureStatusPublication Publication)
        {

            var xml = Publication.ToXML();

            Assert.That(ValidateStatusSchema(xml.ToString(), out var warnings, out var errors), Is.True, String.Join(Environment.NewLine, errors));
            Assert.That(warnings, Is.Empty, String.Join(Environment.NewLine, warnings));

            AssertValidJSON(Publication.ToJSON(), Publication.ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00), JSONSchemas.Mobilithek_StatusMessage);

            return xml;

        }

        /// <summary>
        /// The JSON of a publication: valid DATEX II 3.7 JSON, and valid JSON of the Mobilithek profile.
        /// </summary>
        private static void AssertValidJSON(JObject                       JSON37,
                                            JObject                       MobilithekJSON,
                                            Json.Schema.JsonSchema        MobilithekSchema)
        {

            Assert.That(JSONSchemas.Validate(JSONSchemas.DATEXII_3_7_Payload, JSON37, out var errors),
                        Is.True,
                        String.Join(Environment.NewLine, errors) + Environment.NewLine + JSON37);

            Assert.That(JSONSchemas.Validate(MobilithekSchema, MobilithekJSON, out errors),
                        Is.True,
                        String.Join(Environment.NewLine, errors.Where(error => !error.Contains("did not match") && !error.Contains("do not match"))) + Environment.NewLine + MobilithekJSON);

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

            var xml          = AssertValidTable(publication);

            // DATEX II 3.7 knows only evseId as an identifier type, as an extended value; the others are other types.
            Assert.That(xml.Descendants(v3.DatexIINS.Facilities + "typeOfIdentifier").Select(type => type.Attribute("_extendedValue")?.Value), Is.EqualTo(new[] { "evseId" }));
            Assert.That(xml.Descendants(v3.DatexIINS.Facilities + "otherTypeOfIdentifier").Select(type => type.Value),                        Is.EquivalentTo(new[] { "operatorId", "stationIdBNetzA" }));

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
                Assert.That(POI3Mapper.OperatorIdOf(site.Operator),                     Is.EqualTo("DE*GEF"));
                Assert.That(site.SupplementalFacilities.OfType<SupplementalServiceFacility>().Single().ServiceFacilityType.ToString(), Is.EqualTo("cafe"));
                Assert.That(site.SupplementalFacilities.OfType<SupplementalEquipment>().Single().EquipmentType.ToString(),             Is.EqualTo("toilet"));
                Assert.That(site.Accessibility.Select(accessibility => accessibility.ToString()),                                       Is.EqualTo(new[] { "barrierFreeAccessible" }));
                Assert.That(location.CoordinatesForDisplay?.Latitude,                    Is.EqualTo(50.92));
                Assert.That(location.FacilityLocation?.TimeZone?.ToString(),             Is.EqualTo("+01:00"));
                Assert.That(location.FacilityLocation?.Address?.Postcode,                Is.EqualTo("07749"));
                Assert.That(location.FacilityLocation?.Address?.AddressLines.Select(line => line.Text.Values.Single().Text), Is.EqualTo(new[] { "Biberweg", "18" }));

                Assert.That(station.Id,                                                  Is.EqualTo("DE*GEF*S1"));
                Assert.That(station.StationIdBNetzA,                                     Is.EqualTo("BNetzA-4711"));
                Assert.That(station.TotalMaximumPower?.Value,                            Is.EqualTo(22000));
                Assert.That(station.AuthenticationAndIdentificationMethods.Select(method => method.ToString()), Is.EquivalentTo(new[] { "rfid", "apps" }));
                Assert.That(station.UserInterfaceLanguages,                              Is.EqualTo(new[] { Languages.de }));

                Assert.That(point.Id,                                                    Is.EqualTo("DE*GEF*E1*1"));
                Assert.That(point.EVSEId,                                                Is.EqualTo("DE*GEF*E1*1"));
                Assert.That(point.Reservation?.ToString(),                               Is.EqualTo("optional"));
                Assert.That(point.CurrentType.ToString(),                                Is.EqualTo("ac"));
                Assert.That(point.ChargingMode?.ToString(),                              Is.EqualTo("mode3AC3p"));
                Assert.That(point.AvailableChargingPower.Single().Value,                 Is.EqualTo(22000));

                Assert.That(connector.ConnectorType.ToString(),                          Is.EqualTo("iec62196T2"));
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

        #region ThePricesOfATariffElementAreAPriceGroup()

        /// <summary>
        /// The prices of a tariff element are a price group: two elements apply apart, even when they have the same
        /// restrictions, and come back as two.
        /// </summary>
        [Test]
        public void ThePricesOfATariffElementAreAPriceGroup()
        {

            var warnings  = new List<String>();

            var tariff    = new POI.ChargingTariff(T1,
                                                   Currency.EUR,
                                                   [
                                                       new POI.ChargingTariffElement([ new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.ENERGY, 0.39m, StepSize: 1) ]),
                                                       new POI.ChargingTariffElement([ new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.ENERGY, 0.49m, StepSize: 1),
                                                                                       new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.FLAT,   1) ])
                                                   ],
                                                   Created: Created);

            var rate      = tariff.ToDatexII(Languages.de, warnings)!;

            Assert.That(rate.EnergyPrices.Select(price => price.PriceGroupIndex), Is.EqualTo(new[] { 1U, 2U, 2U }));

            var again     = rate.ToPOI3(GEF, Languages.de, warnings)!;

            Assert.Multiple(() => {
                Assert.That(warnings,                                                         Is.Empty);
                Assert.That(again.Elements.Select(element => element.PriceComponents.Count()), Is.EqualTo(new[] { 1, 2 }));
            });

        }

        #endregion

        #region PricesWithoutGroupsAreGroupedByTheirPeriod()

        /// <summary>
        /// The Mobilithek profile has no price groups: all prices of a rate are of one group, and those with the same
        /// overall period make one tariff element.
        /// </summary>
        [Test]
        public void PricesWithoutGroupsAreGroupedByTheirPeriod()
        {

            var warnings  = new List<String>();
            var night     = new OverallPeriod(DateTimeOffset.Parse("2026-01-01T00:00:00Z"),
                                              ValidPeriod: [ new Period(RecurringTimePeriodOfDay: [ new TimePeriodOfDay(Time.Parse("22:00:00"), Time.Parse("06:00:00")) ]) ]);

            var rate      = new EnergyRate("DE*GEF*T1",
                                           Created,
                                           RatePolicy.Parse("adHoc"),
                                           [ Currency.EUR ],
                                           EnergyPrices: [
                                               new EnergyPrice(PriceType.Parse("pricePerKWh"), new AmountOfMoney(0.39m)),
                                               new EnergyPrice(PriceType.Parse("pricePerKWh"), new AmountOfMoney(0.29m), OverallPeriod: night),
                                               new EnergyPrice(PriceType.Parse("basePrice"),   new AmountOfMoney(1m))
                                           ]);

            var tariff    = rate.ToPOI3(GEF, Languages.de, warnings)!;

            Assert.Multiple(() => {
                Assert.That(warnings,                                                            Is.Empty);
                Assert.That(tariff.Elements.Select(element => element.PriceComponents.Count()),  Is.EqualTo(new[] { 2, 1 }));
                Assert.That(tariff.Elements.Last().Restrictions?.StartTime,                       Is.EqualTo(new TimeOnly(22, 0)));
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

            // The EU vehicle categories of the station, and the payment of the rate: what DATEX II knows as such, the rest as other.
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "euVehicleCategory" &&
                                                           !element.Ancestors().Any(ancestor => ancestor.Name.LocalName == "dedicatedParkingSpaces")).
                            Select(element => element.Attribute("_extendedValue")?.Value ?? element.Value),
                        Is.EquivalentTo(new[] { "m1", "n1", "l" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "paymentMeans").       Select(element => element.Value), Is.EquivalentTo(new[] { "paymentCreditCard", "qrCode" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "otherPaymentMeans").  Select(element => element.Value), Is.EquivalentTo(new[] { "SMS" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "brandsAccepted").     Select(element => element.Value), Is.EquivalentTo(new[] { "visa" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "otherBrandsAccepted").Select(element => element.Value), Is.EquivalentTo(new[] { "Google Pay" }));

            // Accessible charging is the accessibility of the site, accessible toilets a barrier free toilet; a cable without a charging mode is an other cable.
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "energyInfrastructureSite").
                            Elements().Where(element => element.Name.LocalName == "accessibility").Select(element => element.Value),            Is.EqualTo(new[] { "barrierFreeAccessible" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "equipmentType").Select(element => element.Value),        Is.EqualTo(new[] { "toilet" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "connectorFormat").Select(element => element.Value),      Is.EqualTo(new[] { "otherCable" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "energyRate").Elements().
                            Where(element => element.Name.LocalName == "overallPeriod").Elements().Select(element => DateTimeOffset.Parse(element.Value)),
                        Is.EqualTo(new[] { DateTimeOffset.Parse("2026-02-01T00:00:00Z"), DateTimeOffset.Parse("2027-01-01T00:00:00Z") }));

            // NUTS 1 to 3 of the site, illuminated and roofed as amenities, and the parking spaces of the station by their restrictions.
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "nutsCode").Select(element => element.Value),            Is.EqualTo(new[] { "DEG", "DEG03" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "nutsCodeType").Select(element => element.Value),        Is.EqualTo(new[] { "nuts1Code", "nuts3Code" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "roofed").Select(element => element.Value),              Is.EqualTo(new[] { "true", "true" }), "The site, and the parking space for persons with disabilities.");
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "illuminatedRechargingParkingNearby").Select(element => element.Value), Is.EqualTo(new[] { "true" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "dedicatedParkingSpaces").First().
                            Elements().Where(element => element.Name.LocalName == "dimension").Elements().Select(element => Decimal.Parse(element.Value, System.Globalization.CultureInfo.InvariantCulture)),
                        Is.EqualTo(new[] { 5m, 3.5m, 2.1m }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "dedicatedParkingSpaces").
                            Select(spaces => spaces.Elements().Any(element => element.Name.LocalName == "locationReference")),                 Is.EqualTo(new[] { true, false }), "Only a single space tells its location.");
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "illuminated").Select(element => element.Value),         Is.EqualTo(new[] { "true" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "numberOfSpaces").Select(element => element.Value),      Is.EqualTo(new[] { "1", "2" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "userSpecific").Select(element => element.Value),        Is.EqualTo(new[] { "personsWithDisabilities" }));
            Assert.That(xml.Descendants().Where(element => element.Name.LocalName == "dedicatedParkingSpaces").
                            Descendants().Where(element => element.Name.LocalName == "fuelType").Select(element => element.Value),             Is.EqualTo(new[] { "battery" }));

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
                Assert.That(pool.Services,                                     Is.EqualTo(new[] { POI.LocationService.ACCESSIBLE_CHARGING, POI.LocationService.ACCESSIBLE_TOILETS }));

                Assert.That(pool.ChargingStations.Single().Id,                 Is.EqualTo(S1));
                Assert.That(pool.ChargingStations.Single().Features,           Is.EquivalentTo(new[] { POI.ChargingStationFeature.Reservable, POI.ChargingStationFeature.Illuminated }));
                Assert.That(pool.Features,                                     Is.EquivalentTo(new[] { POI.ChargingPoolFeature.Roofed, POI.ChargingPoolFeature.IlluminatedParkingNearby }));
                Assert.That(pool.NUTSCodes,                                    Is.EqualTo(new[] { POI.NUTSCode.Parse("DEG"), POI.NUTSCode.Parse("DEG03") }));
                Assert.That(pool.ChargingStations.Single().EUVehicleCategories, Is.EqualTo(new[] { POI.EUVehicleCategory.L, POI.EUVehicleCategory.M1, POI.EUVehicleCategory.N1 }));

                Assert.That(evse.Id,                                           Is.EqualTo(E1));
                Assert.That(evse.CurrentTypes,                                 Is.EqualTo(POI.CurrentTypes.AC_ThreePhases));
                Assert.That(evse.MaxPower?.Value,                              Is.EqualTo(22000));
                Assert.That(evse.MaxVoltage?.Value,                            Is.EqualTo(400));
                Assert.That(evse.MaxCurrent?.Value,                            Is.EqualTo(32));
                Assert.That(evse.ChargingConnectors.Single().Type,             Is.EqualTo(POI.ChargingConnectorType.IEC_62196_T2));
                Assert.That(evse.ChargingConnectors.Single().Format,           Is.EqualTo(POI.ChargingConnectorFormat.CABLE));
                Assert.That(evse.ChargingTariffIds,                            Is.EqualTo(new[] { T1 }));

                Assert.That(JToken.DeepEquals(expected, actual),               Is.True, $"{expected}\n{actual}");

            });

        }

        #endregion

        #region EnergyAndDurationRestrictionsAreApplicabilities()

        /// <summary>
        /// The energy and duration restrictions of a tariff element are the energy and time based applicability of its
        /// prices, in whole kWh and minutes: one of no whole unit is rounded, with a warning. Without price groups, as in
        /// the Mobilithek profile, prices of different applicability are different elements.
        /// </summary>
        [Test]
        public void EnergyAndDurationRestrictionsAreApplicabilities()
        {

            var warnings  = new List<String>();

            var tariff    = new POI.ChargingTariff(T1,
                                                   Currency.EUR,
                                                   [
                                                       new POI.ChargingTariffElement([ new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.ENERGY, 0.39m, StepSize: 1) ],
                                                                                     new POI.ChargingTariffRestrictions(MaxEnergy: 20000, MaxDuration: 1800)),
                                                       new POI.ChargingTariffElement([ new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.ENERGY, 0.29m, StepSize: 1) ],
                                                                                     new POI.ChargingTariffRestrictions(MinEnergy: 20500))
                                                   ],
                                                   Created: Created);

            var rate      = tariff.ToDatexII(Languages.de, warnings)!;

            var first     = rate.EnergyPrices.First();
            var second    = rate.EnergyPrices.Last();

            // As the Mobilithek profile reads it: without price groups.
            var ungrouped = new EnergyRate(rate.Id,
                                           rate.LastUpdated,
                                           rate.RatePolicy,
                                           rate.ApplicableCurrency,
                                           EnergyPrices: rate.EnergyPrices.Select(price => new EnergyPrice(price.PriceType,
                                                                                                           price.Value,
                                                                                                           EnergyBasedApplicability:  price.EnergyBasedApplicability,
                                                                                                           TimeBasedApplicability:    price.TimeBasedApplicability)));

            var again     = ungrouped.ToPOI3(GEF, Languages.de, new List<String>())!;

            Assert.Multiple(() => {

                Assert.That(first. EnergyBasedApplicability?.ToKWh,     Is.EqualTo(20));
                Assert.That(first. TimeBasedApplicability?.  ToMinute,  Is.EqualTo(30));
                Assert.That(first. EnergyBasedApplicability?.FromKWh,   Is.Null);
                Assert.That(second.EnergyBasedApplicability?.FromKWh,   Is.EqualTo(21), "20.5 kWh are rounded.");
                Assert.That(second.TimeBasedApplicability,              Is.Null);
                Assert.That(warnings.Single(),                          Does.Contain("minimum energy of a restriction of the charging tariff 'DE*GEF*T1' is no whole kWh"));

                Assert.That(again.Elements.Select(element => element.Restrictions?.MaxEnergy),    Is.EqualTo(new Decimal?[] { 20000, null }));
                Assert.That(again.Elements.Select(element => element.Restrictions?.MinEnergy),    Is.EqualTo(new Decimal?[] { null, 21000 }));
                Assert.That(again.Elements.Select(element => element.Restrictions?.MaxDuration),  Is.EqualTo(new Decimal?[] { 1800, null }));

            });

        }

        #endregion

        #region AReservationIsNoPriceOfCharging()

        /// <summary>
        /// DATEX II prices charging, not a reservation: an element pricing a reservation is left out.
        /// </summary>
        [Test]
        public void AReservationIsNoPriceOfCharging()
        {

            var warnings  = new List<String>();

            var tariff    = new POI.ChargingTariff(T1,
                                                   Currency.EUR,
                                                   [
                                                       new POI.ChargingTariffElement([ new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.ENERGY, 0.39m, StepSize: 1) ]),
                                                       new POI.ChargingTariffElement([ new POI.ChargingPriceComponent(POI.ChargingDimensionTypes.FLAT,   2) ],
                                                                                     new POI.ChargingTariffRestrictions(Reservation: POI.ChargingReservationRestriction.RESERVATION))
                                                   ],
                                                   Created: Created);

            var rate      = tariff.ToDatexII(Languages.de, warnings)!;

            Assert.Multiple(() => {
                Assert.That(rate.EnergyPrices.Select(price => price.PriceType.ToString()),  Is.EqualTo(new[] { "pricePerKWh" }));
                Assert.That(rate.OverallPeriod,                                              Is.Null, "Without a validity, the rate has no overall period.");
                Assert.That(warnings.Single(),                                               Does.Contain("prices a reservation (RESERVATION)"));
            });

        }

        #endregion

        #region TheServicesDatexIICanNotTellAreKept()

        /// <summary>
        /// DATEX II tells only accessible charging and accessible toilets: the other services of an existing charging
        /// pool are kept, those two are set as DATEX II tells them.
        /// </summary>
        [Test]
        public void TheServicesDatexIICanNotTellAreKept()
        {

            var network   = ANetwork();
            var warnings  = new List<String>();
            var original  = network.GetChargingStationOperatorById(GEF)!.ChargingPools.Single();
            var site      = original.ToDatexII(network, Languages.de, warnings)!;

            var existing  = original.ToJSON();
            existing["services"] = new JArray("ACCESSIBLE_TOILETS", "CAMERA_SURVEILLANCE");

            var withoutToilets = new EnergyInfrastructureSite(site.Id,
                                                              site.Version,
                                                              Name:                    site.Name,
                                                              LastUpdated:             site.LastUpdated,
                                                              Accessibility:           site.Accessibility,
                                                              LocationReference:       site.LocationReference,
                                                              Operator:                site.Operator,
                                                              EnergyInfrastructureStations:  site.EnergyInfrastructureStations);

            var poolJSON  = withoutToilets.ToPOI3(GEF, existing, Languages.en, warnings);

            Assert.That(poolJSON, Is.Not.Null, String.Join(" ", warnings));
            Assert.That(poolJSON!["services"]!.Values<String>(), Is.EquivalentTo(new[] { "ACCESSIBLE_CHARGING", "CAMERA_SURVEILLANCE" }));

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
                Assert.That(delta.IsDelta,                                                                                                                          Is.True);
                Assert.That(delta.ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00).SelectToken("$..codedExchangeProtocol.value")?.Value<String>(),                    Is.EqualTo("deltaPush"));
                Assert.That(publisher.CurrentStatusPublication!.IsDelta,                                                                                            Is.False);
                Assert.That(delta.EnergyInfrastructureSiteStatus.Single().EnergyInfrastructureStationStatus.Single().RefillPointStatus.Single().Status.ToString(), Is.EqualTo("charging"));
                Assert.That(delta.TableReferences.Single().Version,                                                                                                 Is.EqualTo(publisher.TableVersion));
            });

            lock (errors)
                Assert.That(errors, Is.Empty);

        }

        #endregion

        #region TheEnergyStatusIsPublishedThrottled()

        /// <summary>
        /// The energy status of an EVSE is the current charging power, voltage and remaining charging time of its
        /// charging point. A change publishes a status delta at once; further changes within the energy status delta
        /// interval one more when it is over, with the energy status as it is then.
        /// </summary>
        [Test]
        public async Task TheEnergyStatusIsPublishedThrottled()
        {

            var history   = POI.RoamingNetworkHistory.Start(ANetwork());
            var deltas    = new List<EnergyInfrastructureStatusPublication>();

            await using var publisher = new POI3ToDatexIIPublisher(history, Creator, Languages.de,
                                                                   StationIdBNetzA:            station => "BNetzA-4711",
                                                                   EnergyStatusDeltaInterval:  TimeSpan.FromSeconds(1));

            publisher.OnStatusPublication += (sender, publication, isDelta) => { if (isDelta) lock (deltas) deltas.Add(publication); return Task.CompletedTask; };

            await publisher.StartAsync();

            static String Energy(Int32 Used, Int32 Volt, Int32 Minutes)

                => POI.POIEnergyStatus.ToText(new POI.EnergyInfo(Watt.FromW(Used),
                                                                 Watt.FromW(22000),
                                                                 CurrentVoltage:         org.GraphDefined.Vanaheimr.Illias.Volt.FromV(Volt),
                                                                 RemainingChargingTime:  TimeSpan.FromMinutes(Minutes)));

            ElectricChargingPointStatus PointOf(EnergyInfrastructureStatusPublication Delta)

                => (ElectricChargingPointStatus) Delta.EnergyInfrastructureSiteStatus.Single().EnergyInfrastructureStationStatus.Single().RefillPointStatus.Single();

            history.RuntimeStatus.TryUpdate(POI.POIRuntimeStatusKind.EnergyStatus, nameof(POI.EVSE), E1.ToString(), Energy(11000, 400, 30), T0.AddMinutes(1), out _, out _);

            Assert.That(await Eventually(() => { lock (deltas) return deltas.Count == 1; }), Is.True, "The first energy status was not published at once.");

            history.RuntimeStatus.TryUpdate(POI.POIRuntimeStatusKind.EnergyStatus, nameof(POI.EVSE), E1.ToString(), Energy(7400,  400, 40), T0.AddMinutes(2), out _, out _);
            history.RuntimeStatus.TryUpdate(POI.POIRuntimeStatusKind.EnergyStatus, nameof(POI.EVSE), E1.ToString(), Energy(3700,  230, 50), T0.AddMinutes(3), out _, out _);

            lock (deltas)
                Assert.That(deltas, Has.Count.EqualTo(1), "Within the interval, a changed energy status must wait.");

            Assert.That(await Eventually(() => { lock (deltas) return deltas.Count == 2; }), Is.True, "The changed energy status was not published after the interval.");

            await Task.Delay(1500);

            lock (deltas)
            {

                Assert.That(deltas, Has.Count.EqualTo(2), "Two changes within the interval need one delta, not two.");

                foreach (var delta in deltas)
                    AssertValidStatus(delta);

                Assert.Multiple(() => {
                    Assert.That(PointOf(deltas[0]).CurrentChargingPower?.Value,    Is.EqualTo(11000));
                    Assert.That(PointOf(deltas[0]).CurrentVoltage?.Value,          Is.EqualTo(400));
                    Assert.That(PointOf(deltas[0]).RemainingChargingTime,          Is.EqualTo(TimeSpan.FromMinutes(30)));
                    Assert.That(PointOf(deltas[1]).CurrentChargingPower?.Value,    Is.EqualTo(3700), "The delta after the interval has the energy status as it is then.");
                    Assert.That(PointOf(deltas[1]).CurrentVoltage?.Value,          Is.EqualTo(230));
                    Assert.That(PointOf(deltas[1]).RemainingChargingTime,          Is.EqualTo(TimeSpan.FromMinutes(50)));
                    Assert.That(PointOf(publisher.CurrentStatusPublication!).CurrentChargingPower?.Value, Is.EqualTo(3700));
                });

            }

            // A change of the status is published at once, also within the interval.
            history.RuntimeStatus.TryUpdate(POI.POIRuntimeStatusKind.Status, nameof(POI.EVSE), E1.ToString(), "charging", T0.AddMinutes(4), out _, out _);

            Assert.That(await Eventually(() => { lock (deltas) return deltas.Count == 3; }), Is.True, "The changed status was not published at once.");

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

        #region TheImporterLeavesOutRefillPointsThatAreNoChargingPoints()

        /// <summary>
        /// A diesel refill point next to the EVSE: the table is imported with a warning about it, and its status is
        /// skipped rather than taken for the status of an unknown EVSE.
        /// </summary>
        [Test]
        public async Task TheImporterLeavesOutRefillPointsThatAreNoChargingPoints()
        {

            var history  = POI.RoamingNetworkHistory.Start(ANetwork(WithPools: false));
            var errors   = new List<String>();

            await using var importer = new DatexIIToPOI3Importer(history, changeSet => changeSet.SignedByAll(history.Head.RoamingNetwork));

            importer.OnError += (sender, description, warnings) => { lock (errors) errors.Add(description + " " + String.Join(" ", warnings)); };

            var aegi     = DatexIINS.EnergyInfrastructure;
            var table    = POI3Mapper.ToTablePublication(ANetwork(), Creator, Languages.de, "rn1", "1", [], StationIdBNetzA: station => "BNetzA-4711").ToXML();

            table.Descendants(aegi + "refillPoint").Last().AddAfterSelf(
                new XElement(aegi + "refillPoint",
                    new XAttribute(DatexIINS.XSI + "type",  "aegi:DieselRefillPoint"),
                    new XAttribute("id",                     "D1"),
                    new XAttribute("version",                "1"),
                    new XElement(aegi + "deliveryUnit",      "litre")
                )
            );

            Assert.That(ValidateTableSchema(table.ToString(), out _, out var tableErrors), Is.True, String.Join(Environment.NewLine, tableErrors));

            await importer.ImportAsync(table);

            Assert.That(history.Head.RoamingNetwork.GetChargingStationOperatorById(GEF)!.EVSEs.Single().Id, Is.EqualTo(E1));

            lock (errors)
                Assert.That(errors.Single(), Does.Contain("'D1'").And.Contain("no electric charging point"));

            // The statuses of both.
            var status   = new EnergyInfrastructureStatusPublication(
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
                                                                                   LastUpdated: T0.AddMinutes(5)),
                                                   new RefillPointStatus          (RefillPointStatusEnum.Parse("outOfStock"),
                                                                                   new FacilityObjectVersionedReference("D1"),
                                                                                   LastUpdated: T0.AddMinutes(5))
                                               ]
                                           )
                                       ]
                                   )
                               ]
                           ).ToXML();

            Assert.That(ValidateStatusSchema(status.ToString(), out _, out var statusErrors), Is.True, String.Join(Environment.NewLine, statusErrors));

            await importer.ImportAsync(status);

            Assert.That(history.RuntimeStatus.TryGet(POI.POIRuntimeStatusKind.Status, nameof(POI.EVSE), E1.ToString(), out var current) &&
                        current.Value == "charging",
                        Is.True);

            lock (errors)
                Assert.That(errors, Has.Count.EqualTo(1), String.Join(Environment.NewLine, errors));

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
