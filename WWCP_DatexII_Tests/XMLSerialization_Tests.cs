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

using NUnit.Framework;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationExtension;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

#endregion

namespace cloud.charging.open.protocols.DatexII.Tests
{

    /// <summary>
    /// Unit tests for DatexII.
    /// </summary>
    [TestFixture]
    public class XMLSerialization_Tests
    {

        // <Period xmlns="com" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
        //
        //   <startOfPeriod>2025-01-01T08:00:00</startOfPeriod>
        //   <endOfPeriod>2025-01-07T20:00:00</endOfPeriod>
        //
        //   <periodName>
        //     <values>
        //       <value lang="en">Winter Holiday</value>
        //       <value lang="de">Winterferien</value>
        //     </values>
        //   </periodName>
        //
        //   <recurringTimePeriodOfDay>
        //     <!-- Example content for TimePeriodOfDay -->
        //   </recurringTimePeriodOfDay>

        //   <recurringDayWeekMonthPeriod>
        //     <!-- Example content for DayWeekMonth -->
        //   </recurringDayWeekMonthPeriod>

        //   <recurringSpecialDay>
        //     <!-- Example content for SpecialDay -->
        //   </recurringSpecialDay>

        //   <_periodExtension>
        //     <!-- Extension fields go here -->
        //   </_periodExtension>

        // </Period>


        #region Serialize_Period_Test1()

        /// <summary>
        /// A test for the serialization of a Period.
        /// </summary>
        [Test]
        public async Task Serialize_Period_Test1()
        {

            var period  = new Period(

                              StartOfPeriod:                 DateTimeOffset.Parse("2025-01-01T08:00:00Z"),
                              EndOfPeriod:                   DateTimeOffset.Parse("2025-06-30T23:59:59Z"),

                              PeriodName:                    new MultilingualString(
                                                                 new MultilingualStringValue(Languages.de, "Hallo Welt!"),
                                                                 new MultilingualStringValue(Languages.en, "Hello World!")
                                                             ),

                              RecurringTimePeriodOfDay:      [
                                                                 new TimePeriodOfDay(StartTimeOfPeriod: Time.Parse("00:00"),  EndTimeOfPeriod: Time.Parse("06:00")),
                                                                 new TimePeriodOfDay(StartTimeOfPeriod: Time.Parse("22:00"),  EndTimeOfPeriod: Time.Parse("23:59:59"))
                                                             ],

                              RecurringDayWeekMonthPeriod:   [
                                                                 new DayWeekMonth(
                                                                     [
                                                                         Day.Monday,
                                                                         Day.Friday
                                                                     ],
                                                                     [
                                                                         MonthOfYear.May,
                                                                         MonthOfYear.September
                                                                     ]
                                                                 ),
                                                             ],

                              RecurringSpecialDay:           [
                                                                 new SpecialDay(
                                                                     IntersectWithApplicableDays:   true,
                                                                     SpecialDayType:                SpecialDayType.PublicHoliday,
                                                                     PublicEvent:                   PublicEventType.WineFestival,
                                                                     NamedAreas:                    [
                                                                                                        new NamedArea(
                                                                                                            AreaName:          new MultilingualString(
                                                                                                                                   new MultilingualStringValue(Languages.de, "Weinberg"),
                                                                                                                                   new MultilingualStringValue(Languages.en, "Vineyard")
                                                                                                                               ),
                                                                                                            NamedAreaType:     NamedAreaType.RuralCounty,
                                                                                                            Country:           Country.Germany
                                                                                                        ),
                                                                                                        new ISONamedArea(
                                                                                                            AreaName:          new MultilingualString(
                                                                                                                                   new MultilingualStringValue(Languages.de, "ISO-Weinberg"),
                                                                                                                                   new MultilingualStringValue(Languages.en, "ISO-Vineyard")
                                                                                                                               ),
                                                                                                            SubdivisionType:   SubdivisionType.County,
                                                                                                            SubdivisionCode:   new SubdivisionCode("DE"),
                                                                                                            NamedAreaType:     NamedAreaType.TouristArea,
                                                                                                            Country:           Country.Germany
                                                                                                        )
                                                                                                    ]
                                                                 )
                                                             ]

                          );

            Assert.That(period.ToXML().ToString(),  Is.EqualTo(@"<period xmlns=""http://datex2.eu/schema/3/common"">
  <startOfPeriod>2025-01-01T08:00:00.000+00:00</startOfPeriod>
  <endOfPeriod>2025-06-30T23:59:59.000+00:00</endOfPeriod>
  <periodName>
    <values>
      <value lang=""de"">Hallo Welt!</value>
      <value lang=""en"">Hello World!</value>
    </values>
  </periodName>
  <recurringTimePeriodOfDay>
    <startTimeOfPeriod>00:00:00</startTimeOfPeriod>
    <endTimeOfPeriod>06:00:00</endTimeOfPeriod>
  </recurringTimePeriodOfDay>
  <recurringTimePeriodOfDay>
    <startTimeOfPeriod>22:00:00</startTimeOfPeriod>
    <endTimeOfPeriod>23:59:59</endTimeOfPeriod>
  </recurringTimePeriodOfDay>
  <recurringDayWeekMonthPeriod>
    <DayWeekMonth>
      <applicableDay>monday</applicableDay>
      <applicableDay>friday</applicableDay>
      <applicableMonth>may</applicableMonth>
      <applicableMonth>september</applicableMonth>
    </DayWeekMonth>
  </recurringDayWeekMonthPeriod>
  <recurringSpecialDay>
    <intersectWithApplicableDays>true</intersectWithApplicableDays>
    <specialDayType>publicHoliday</specialDayType>
    <publicEvent>wineFestival</publicEvent>
    <namedArea>
      <NamedArea xmlns=""http://datex2.eu/schema/3/locationReferencing"">
        <areaName>
          <MultilingualString xmlns=""http://datex2.eu/schema/3/common"">
            <values>
              <value lang=""de"">Weinberg</value>
              <value lang=""en"">Vineyard</value>
            </values>
          </MultilingualString>
        </areaName>
        <namedAreaType>ruralCounty</namedAreaType>
        <country>DE</country>
      </NamedArea>
      <IsoNamedArea xmlns=""http://datex2.eu/schema/3/locationReferencing"">
        <areaName>
          <MultilingualString xmlns=""http://datex2.eu/schema/3/common"">
            <values>
              <value lang=""de"">ISO-Weinberg</value>
              <value lang=""en"">ISO-Vineyard</value>
            </values>
          </MultilingualString>
        </areaName>
        <namedAreaType>touristArea</namedAreaType>
        <country>DE</country>
        <subdivisionType>county</subdivisionType>
        <subdivisionCode>DE</subdivisionCode>
      </IsoNamedArea>
    </namedArea>
  </recurringSpecialDay>
</period>"));

        }

        #endregion



    }

}
