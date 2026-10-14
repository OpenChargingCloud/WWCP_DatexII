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
using System.Globalization;

using NUnit.Framework;

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.D2Payload;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

#endregion

namespace cloud.charging.open.protocols.DatexII.Tests
{

    /// <summary>
    /// The examples read in full, what is read written again as the schemas want it, and the fixes that took.
    /// </summary>
    [TestFixture]
    public class ParseAndWrite_Tests : AXMLSchemaValidation
    {

        #region (private static) Example(Name)

        private static XElement Example(String Name)

            => XDocument.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Examples", Name))).Root!;

        #endregion


        #region TheTableExampleIsReadInFull()

        [Test]
        public void TheTableExampleIsReadInFull()
        {

            Assert.That(Payload.TryParseXML(Example("EnergyInfrastructureTablePublication.xml"), out var payload, out var errorResponse), Is.True, errorResponse);

            var table     = ((EnergyInfrastructureTablePublication) payload!).EnergyInfrastructureTables.Single();
            var site      = table.EnergyInfrastructureSites.Single();
            var points    = site.EnergyInfrastructureStations.SelectMany(station => station.RefillPoints).OfType<ElectricChargingPoint>().ToArray();
            var rate      = points.First().ElectricEnergy.First().EnergyRates.First();

            Assert.Multiple(() => {
                Assert.That(table.Id,                                                    Is.EqualTo("2474A514-0E5D-48F9-A908-F185DD4177A2"));
                Assert.That(site.EnergyInfrastructureStations.Count(),                   Is.EqualTo(2));
                Assert.That(points.Length,                                               Is.EqualTo(4));
                Assert.That(points.First().EVSEId,                                       Is.EqualTo("DEABCEXXXXXXXX12345"));
                Assert.That(points.Sum(point => point.Connector.Count()),                Is.EqualTo(6));
                Assert.That(site.OperatingHours,                                         Is.InstanceOf<OpenAllHours>());
                // The helpdesk is open from 6 to 23, but not on Sundays and public holidays.
                Assert.That(((OperatingHoursSpecification) ((AnOrganisation) site.EnergyInfrastructureStations.First().Helpdesk!).OrganisationUnits.First().OperatingHours!).OverallPeriod.ValidPeriod, Is.Not.Empty);
                Assert.That(rate.RatePolicy.ToString(),                                  Is.EqualTo("adHoc"));
                Assert.That(rate.EnergyPrices.Single().Value.Value,                      Is.EqualTo(0.35m));
                Assert.That(site.EnergyInfrastructureStations.First().StationIdBNetzA,   Is.EqualTo("[Ladestations-ID der Bundesnetzagentur]"));
            });

        }

        #endregion

        #region TheTableExampleIsWrittenAsTheSchemaWantsIt()

        [Test]
        public void TheTableExampleIsWrittenAsTheSchemaWantsIt()
        {

            Assert.That(Payload.TryParseXML(Example("EnergyInfrastructureTablePublication.xml"), out var payload, out var errorResponse), Is.True, errorResponse);

            var xml = ((EnergyInfrastructureTablePublication) payload!).ToXML();

            Assert.That(ValidateTableSchema(xml.ToString(), out var warnings, out var errors), Is.True, String.Join(Environment.NewLine, errors));

            // And read again.
            Assert.That(Payload.TryParseXML(xml, out var again, out errorResponse), Is.True, errorResponse);
            Assert.That(((EnergyInfrastructureTablePublication) again!).EnergyInfrastructureTables.Single().EnergyInfrastructureSites.Single().EnergyInfrastructureStations.Count(), Is.EqualTo(2));

        }

        #endregion

        #region TheStatusExampleIsReadInFullAndWrittenAgain()

        [Test]
        public void TheStatusExampleIsReadInFullAndWrittenAgain()
        {

            Assert.That(Payload.TryParseXML(Example("EnergyInfrastructureStatusPublication.xml"), out var payload, out var errorResponse), Is.True, errorResponse);

            var publication  = (EnergyInfrastructureStatusPublication) payload!;
            var siteStatus   = publication.EnergyInfrastructureSiteStatus.Single();
            var status       = (ElectricChargingPointStatus) siteStatus.EnergyInfrastructureStationStatus.Single().RefillPointStatus.Single();

            Assert.Multiple(() => {
                Assert.That(siteStatus.Reference.Id,                                         Is.EqualTo("21F02723-CF84-4380-84D4-050917836C7C"));
                Assert.That(status.Reference.Id,                                             Is.EqualTo("73ABE928-707D-4A99-8043-4293EE685504"));
                Assert.That(status.Status.ToString(),                                        Is.EqualTo("charging"));
                Assert.That(status.LastUpdated,                                              Is.EqualTo(DateTimeOffset.Parse("2025-02-02T12:50:00+01:00")));
                Assert.That(status.NewOperatingHours,                                        Is.InstanceOf<OpenAllHours>());
                Assert.That(status.WaitingTime,                                              Is.EqualTo(TimeSpan.FromMinutes(30)));
                Assert.That(status.RemainingChargingTime,                                    Is.EqualTo(TimeSpan.FromMinutes(30)));
                Assert.That(status.CurrentVoltage?.Value,                                    Is.EqualTo(400));
                Assert.That(status.CurrentChargingPower?.Value,                              Is.EqualTo(11000));
                // Once read from fac:lastUpdated.
                Assert.That(status.NextAvailableChargingSlots,                               Is.EqualTo(new[] { DateTimeOffset.Parse("2025-02-02T13:20:00+01:00") }));
                Assert.That(status.PlannedRefillPointStatus.Single().Status.ToString(),      Is.EqualTo("reserved"));
                Assert.That(status.EnergyRateUpdates.Single().EnergyPrices.Single().Value.Value, Is.EqualTo(0.37m));
            });

            var xml = publication.ToXML();

            Assert.That(ValidateStatusSchema(xml.ToString(), out var warnings, out var errors), Is.True, String.Join(Environment.NewLine, errors));
            Assert.That(Payload.TryParseXML(xml, out var again, out errorResponse), Is.True, errorResponse);

            var statusAgain = (ElectricChargingPointStatus) ((EnergyInfrastructureStatusPublication) again!).EnergyInfrastructureSiteStatus.Single().EnergyInfrastructureStationStatus.Single().RefillPointStatus.Single();

            Assert.Multiple(() => {
                Assert.That(statusAgain.Status.ToString(),             Is.EqualTo("charging"));
                Assert.That(statusAgain.WaitingTime,                   Is.EqualTo(TimeSpan.FromMinutes(30)));
                Assert.That(statusAgain.NextAvailableChargingSlots,    Is.EqualTo(status.NextAvailableChargingSlots));
                Assert.That(statusAgain.EnergyRateUpdates.Single().EnergyRateReference.Id, Is.EqualTo("74034E3E-9D2F-4410-BE6F-CAA3176D69B4"));
            });

        }

        #endregion

        #region AnInvalidOptionalValueIsAnError()

        [Test]
        public void AnInvalidOptionalValueIsAnError()
        {

            var xml = Example("EnergyInfrastructureStatusPublication.xml");

            xml.Descendants(v3.DatexIINS.EnergyInfrastructure + "currentVoltage").Single().Value = "four hundred";

            // It was taken as missing before.
            Assert.That(Payload.TryParseXML(xml, out _, out var errorResponse), Is.False);
            Assert.That(errorResponse,                                          Does.Contain("four hundred"));

        }

        #endregion

        #region EnumerationValuesAreSpelledAsTheSchemaSpellsThem()

        [Test]
        public void EnumerationValuesAreSpelledAsTheSchemaSpellsThem()
        {
            Assert.Multiple(() => {
                Assert.That(AuthenticationAndIdentificationType.MifareDesfire.ToString(),  Is.EqualTo("mifareDesfire"));
                Assert.That(EUVehicleCategory.N1ClassII.ToString(),                        Is.EqualTo("n1ClassII"));
            });
        }

        #endregion

        #region AmountsAreReadWhateverTheCulture()

        [Test]
        public void AmountsAreReadWhateverTheCulture()
        {

            var culture = CultureInfo.CurrentCulture;

            try
            {

                // "." groups thousands in German.
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

                Assert.That(AmountOfMoney.TryParse("0.39", out var amount), Is.True);
                Assert.That(amount.Value,                                   Is.EqualTo(0.39m));

            }
            finally
            {
                CultureInfo.CurrentCulture = culture;
            }

        }

        #endregion

        #region AnOverallPeriodWritesItsValidPeriods()

        [Test]
        public void AnOverallPeriodWritesItsValidPeriods()
        {

            var overallPeriod = new OverallPeriod(
                                    DateTimeOffset.Parse("2026-01-01T00:00:00Z"),
                                    ValidPeriod: [
                                        new Period(
                                            RecurringTimePeriodOfDay:     [ new TimePeriodOfDay(Time.Parse("22:00:00"), Time.Parse("06:30:00")) ],
                                            RecurringDayWeekMonthPeriod:  [ new DayWeekMonth([ Day.Saturday, Day.Sunday ]) ]
                                        )
                                    ]
                                );

            Assert.That(OverallPeriod.TryParseXML(overallPeriod.ToXML(), out var again, out var errorResponse), Is.True, errorResponse);

            var period = again!.ValidPeriod.Single();

            Assert.Multiple(() => {
                Assert.That(period.RecurringTimePeriodOfDay.Single().StartTimeOfPeriod.ToString(),     Is.EqualTo("22:00:00"));
                Assert.That(period.RecurringDayWeekMonthPeriod.Single().ApplicableDays,                Is.EqualTo(new[] { Day.Saturday, Day.Sunday }));
            });

        }

        #endregion

    }

}
