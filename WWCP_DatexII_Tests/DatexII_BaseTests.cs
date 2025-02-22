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

using Newtonsoft.Json.Linq;

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
    public class DatexII_BaseTests
    {

        #region Reset_ChargingStation_Test()

        /// <summary>
        /// A test...
        /// </summary>
        [Test]
        public async Task Base_Test1()
        {

            var publication = new EnergyInfrastructureTablePublication(

                                  PublicationTime:              Timestamp.Now,

                                  PublicationCreator:           new InternationalIdentifier(
                                                                    Country:              Country.Germany,
                                                                    NationalIdentifier:   "GraphDefined CPO"
                                                                ),

                                  Language:                     Languages.en,

                                  HeaderInformation:            new HeaderInformation(
                                                                    InformationStatus:        InformationStatus.Test,
                                                                    Confidentiality:          Confidentialities.InternalUse,
                                                                    AllowedDeliveryChannel:   [
                                                                                                  InformationDeliveryServices.AnyGeneralDeliveryService
                                                                                              ]
                                                                ),

                                  EnergyInfrastructureTables:   [
                                                                    new EnergyInfrastructureTable(
                                                                        Id:                          "GraphDefined CPO 2025-02-21-0001",
                                                                        Version:                     "1",
                                                                        TableName:                   "AllData",
                                                                        EnergyInfrastructureSites:   [
                                                                                                         new EnergyInfrastructureSite(

                                                                                                             Id:                             "1",
                                                                                                             Version:                        "1",

                                                                                                             Name:                           new MultilingualString(
                                                                                                                                                 new MultilingualStringValue(
                                                                                                                                                     "de",
                                                                                                                                                     "Ladestation 1"
                                                                                                                                                 )
                                                                                                                                             ),
                                                                                                             Alias:                          [
                                                                                                                                                 new MultilingualString(
                                                                                                                                                     new MultilingualStringValue(
                                                                                                                                                         "de",
                                                                                                                                                         "Ladestation 1"
                                                                                                                                                     )
                                                                                                                                                 )
                                                                                                                                             ],
                                                                                                             ExternalIdentifier:             "Ext1",
                                                                                                             LastUpdated:                    Timestamp.Now,
                                                                                                             Description:                    new MultilingualString(
                                                                                                                                                 new MultilingualStringValue(
                                                                                                                                                     "de",
                                                                                                                                                     "Dies ist Ladestation 1"
                                                                                                                                                 )
                                                                                                                                             ),
                                                                                                             Accessibility:                  [
                                                                                                                                                 Accessibilities.BarrierFreeAccessible
                                                                                                                                             ],
                                                                                                             AdditionalInformation:          [
                                                                                                                                                 new MultilingualString(
                                                                                                                                                     new MultilingualStringValue(
                                                                                                                                                         "de",
                                                                                                                                                         "Ladestation mit RGB-Licht!"
                                                                                                                                                     )
                                                                                                                                                 )
                                                                                                                                             ],
                                                                                                             InformationWebsites:            [
                                                                                                                                                 URL.Parse("https://example.org/info")
                                                                                                                                             ],
                                                                                                             PhotoURLs:                      [
                                                                                                                                                 URL.Parse("https://example.org/photo1"),
                                                                                                                                                 URL.Parse("https://example.org/photo2")
                                                                                                                                             ],
                                                                                                             Photos:                         [
                                                                                                                                                 new Image(
                                                                                                                                                     new Byte[100],
                                                                                                                                                     ImageFormats.JPEG
                                                                                                                                                 )
                                                                                                                                             ],
                                                                                                             OperatingHours:                 new OperatingHoursSpecification(

                                                                                                                                                 Id:                   "",
                                                                                                                                                 Version:              "",
                                                                                                                                                 OverallPeriod:        new OverallPeriod(
                                                                                                                                                                           OverallStartTime:   Timestamp.Now,
                                                                                                                                                                           OverallEndTime:     null,
                                                                                                                                                                           ValidPeriod:        null,
                                                                                                                                                                           ExceptionPeriod:    null
                                                                                                                                                                       ),
                                                                                                                                                 LastUpdated:          Timestamp.Now,
                                                                                                                                                 Label:                "Label1",
                                                                                                                                                 OperatingAllYear:     true,
                                                                                                                                                 URLLinkAddress:       URL.Parse("https://example.org/operatinghours"),

                                                                                                                                                 ClosureInformation:   new ClosureInformation(
                                                                                                                                                                           PermananentlyClosed:      false,
                                                                                                                                                                           TemporarilyClosed:        false,
                                                                                                                                                                           ClosedFrom:               null,
                                                                                                                                                                           TemporarilyClosedUntil:   null
                                                                                                                                                                       )

                                                                                                                                             ),
                                                                                                             LocationReference:              null,
                                                                                                             Owner:                          null,
                                                                                                             Operator:                       null,
                                                                                                             Helpdesk:                       null,
                                                                                                             ApplicableForVehicles:          null,
                                                                                                             Dimension:                      null,
                                                                                                             Amenities:                      null,

                                                                                                             SupplementalFacilities:         null,
                                                                                                             DedicatedParkingSpaces:         null,

                                                                                                             TypeOfSite:                     EnergyInfrastructureSiteTypes.Onstreet,
                                                                                                             Brand:                          new MultilingualString(
                                                                                                                                                 new MultilingualStringValue(
                                                                                                                                                     "de",
                                                                                                                                                     "SchönerLaden"
                                                                                                                                                 )
                                                                                                                                             ),
                                                                                                             ExclusiveUsers:                 [ UserTypes.Customers ],
                                                                                                             PreferredUsers:                 [ UserTypes.Subscribers ],
                                                                                                             ServiceTypes:                   [
                                                                                                                                                 new ServiceType(
                                                                                                                                                     ServiceTypeValue:   ServiceTypes.Unattended,
                                                                                                                                                     OverallPeriod:      new OverallPeriod(
                                                                                                                                                                             OverallStartTime:   Timestamp.Now,
                                                                                                                                                                             OverallEndTime:     null,
                                                                                                                                                                             ValidPeriod:        null,
                                                                                                                                                                             ExceptionPeriod:    null
                                                                                                                                                                         )
                                                                                                                                                 )
                                                                                                                                             ],
                                                                                                             Entrances:                      [  ],
                                                                                                             Exits:                          [  ],
                                                                                                             EnergyInfrastructureStations:   [  ]

                                                                                                         )
                                                                                                     ]
                                                                    )
                                                                ]

                              );

        }

        #endregion

    }

}
