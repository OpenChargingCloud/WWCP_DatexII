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
    /// Unit tests for creating and validating DatexII Example XMLs.
    /// </summary>
    [TestFixture]
    public class CreateAndValidate_DatexIIXMLs : AXMLSchemaValidation
    {

        #region EnergyInfrastructure_StatusPublication_Test1()

        /// <summary>
        /// A test creating and validating an Energy Infrastructure Status Publication.
        /// </summary>
        [Test]
        public void EnergyInfrastructure_StatusPublication_Test1()
        {

            var publication  = new EnergyInfrastructureStatusPublication(

                                   PublicationTime:                                  Timestamp.Now,

                                   PublicationCreator:                               new InternationalIdentifier(
                                                                                         Country:               Country.Germany,
                                                                                         NationalIdentifier:   "GraphDefined CPO"
                                                                                     ),

                                   Language:                                         Languages.en,

                                   TableReferences:                                  null,

                                   HeaderInformation:                                new HeaderInformation(
                                                                                         InformationStatus:        InformationStatus.Test,
                                                                                         Confidentiality:          Confidentiality.InternalUse,
                                                                                         AllowedDeliveryChannel:   [
                                                                                                                       InformationDeliveryService.AnyGeneralDeliveryService
                                                                                                                   ]
                                                                                     ),

                                   EnergyInfrastructureSiteStatus:                   null,
                                   EnergyInfrastructureStatusPublicationExtension:   null,

                                   ModelBaseVersion:                                 null,
                                   ExtensionName:                                    null,
                                   ExtensionVersion:                                 null,
                                   ProfileName:                                      null,
                                   ProfileVersion:                                   null,
                                   PayloadPublicationExtension:                      null

                               );

            var xml          = publication.ToXML();

            var isValidXML   = ValidateStatusSchema(xml.ToString(), out var warning, out var errors);
            Assert.That(isValidXML,       Is.True);
            Assert.That(warning.Count(),  Is.EqualTo(0));
            Assert.That(errors. Count(),  Is.EqualTo(0));

        }

        #endregion

        #region EnergyInfrastructure_TablePublication_Test1()

        /// <summary>
        /// A test creating and validating an Energy Infrastructure Table Publication.
        /// </summary>
        [Test]
        public void EnergyInfrastructure_TablePublication_Test1()
        {

            var publication  = new EnergyInfrastructureTablePublication(

                                   PublicationTime:              Timestamp.Now,

                                   PublicationCreator:           new InternationalIdentifier(
                                                                     Country:               Country.Germany,
                                                                     NationalIdentifier:   "GraphDefined CPO"
                                                                 ),

                                   Language:                     Languages.en,

                                   HeaderInformation:            new HeaderInformation(
                                                                     InformationStatus:        InformationStatus.Test,
                                                                     Confidentiality:          Confidentiality.InternalUse,
                                                                     AllowedDeliveryChannel:   [
                                                                                                   InformationDeliveryService.AnyGeneralDeliveryService
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
                                                                                                                                                      Languages.de,
                                                                                                                                                      "Ladestation 1"
                                                                                                                                                  )
                                                                                                                                              ),
                                                                                                              Alias:                          [
                                                                                                                                                  new MultilingualString(
                                                                                                                                                      new MultilingualStringValue(
                                                                                                                                                          Languages.de,
                                                                                                                                                          "Ladestation 1"
                                                                                                                                                      )
                                                                                                                                                  )
                                                                                                                                              ],
                                                                                                              ExternalIdentifier:             "Ext1",
                                                                                                              LastUpdated:                    Timestamp.Now,
                                                                                                              Description:                    new MultilingualString(
                                                                                                                                                  new MultilingualStringValue(
                                                                                                                                                      Languages.de,
                                                                                                                                                      "Dies ist Ladestation 1"
                                                                                                                                                  )
                                                                                                                                              ),
                                                                                                              Accessibility:                  [
                                                                                                                                                  Accessibility.BarrierFreeAccessible
                                                                                                                                              ],
                                                                                                              AdditionalInformation:          [
                                                                                                                                                  new MultilingualString(
                                                                                                                                                      new MultilingualStringValue(
                                                                                                                                                          Languages.de,
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
                                                                                                                                                      ImageFormat.JPEG
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
                                                                                                                                                                            PermanentlyClosed:        false,
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

                                                                                                              TypeOfSite:                     EnergyInfrastructureSiteType.OnStreet,
                                                                                                              Brand:                          new MultilingualString(
                                                                                                                                                  new MultilingualStringValue(
                                                                                                                                                      Languages.de,
                                                                                                                                                      "Schöner Laden (tm)"
                                                                                                                                                  )
                                                                                                                                              ),
                                                                                                              ExclusiveUsers:                 [ UserType.Customers ],
                                                                                                              PreferredUsers:                 [ UserType.Subscribers ],
                                                                                                              ServiceTypes:                   [
                                                                                                                                                  new Service(
                                                                                                                                                      ServiceTypeValue:   ServiceType.Unattended,
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

            var xml          = publication.ToXML();

            var isValidXML   = ValidateTableSchema(xml.ToString(), out var warning, out var errors);
            Assert.That(isValidXML,       Is.True);
            Assert.That(warning.Count(),  Is.EqualTo(0));
            Assert.That(errors. Count(),  Is.EqualTo(0));

        }

        #endregion

    }

}
