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
            Assert.That(isValidXML,       Is.True,            String.Join(Environment.NewLine, errors));
            Assert.That(warning.Count(),  Is.EqualTo(0),      String.Join(Environment.NewLine, warning));
            Assert.That(errors. Count(),  Is.EqualTo(0),      String.Join(Environment.NewLine, errors));

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
                                                                                                              LocationReference:              new PointLocation(
                                                                                                                                                  PointByCoordinates:      new PointByCoordinates(
                                                                                                                                                                               new PointCoordinates(
                                                                                                                                                                                   Latitude:    50.779599,
                                                                                                                                                                                   Longitude:    6.104507
                                                                                                                                                                               )
                                                                                                                                                                           ),
                                                                                                                                                  CoordinatesForDisplay:   new PointCoordinates(
                                                                                                                                                                               Latitude:    50.779599,
                                                                                                                                                                               Longitude:    6.104507
                                                                                                                                                                           ),
                                                                                                                                                  FacilityLocation:        new FacilityLocation(
                                                                                                                                                                               Address:   new cloud.charging.open.protocols.DatexII.v3.LocationExtension.Address(
                                                                                                                                                                                              Postcode:       "52078",
                                                                                                                                                                                              City:           new MultilingualString(
                                                                                                                                                                                                                  new MultilingualStringValue(
                                                                                                                                                                                                                      Languages.de,
                                                                                                                                                                                                                      "Aachen"
                                                                                                                                                                                                                  )
                                                                                                                                                                                                              ),
                                                                                                                                                                                              CountryCode:    Country.Germany,
                                                                                                                                                                                              AddressLines:   [
                                                                                                                                                                                                                  new AddressLine(
                                                                                                                                                                                                                      Order:   0,
                                                                                                                                                                                                                      Text:    new MultilingualString(
                                                                                                                                                                                                                                   new MultilingualStringValue(
                                                                                                                                                                                                                                       Languages.de,
                                                                                                                                                                                                                                       "Hauptstraße 1"
                                                                                                                                                                                                                                   )
                                                                                                                                                                                                                               ),
                                                                                                                                                                                                                      Type:    AddressLineType.Street
                                                                                                                                                                                                                  )
                                                                                                                                                                                                              ]
                                                                                                                                                                                          )
                                                                                                                                                                           )
                                                                                                                                              ),
                                                                                                              Owner:                          new OrganisationSpecification(

                                                                                                                                                  Id:                         "1",
                                                                                                                                                  Version:                    "1",
                                                                                                                                                  Name:                       new MultilingualString(
                                                                                                                                                                                  new MultilingualStringValue(
                                                                                                                                                                                      Languages.de,
                                                                                                                                                                                      "GraphDefined GmbH"
                                                                                                                                                                                  )
                                                                                                                                                                              ),
                                                                                                                                                  OperatorId:                 "DE*GEF",
                                                                                                                                                  LastUpdated:                DateTime.UtcNow,
                                                                                                                                                  Available24hours:           true,
                                                                                                                                                  PublishingAgreement:        true,
                                                                                                                                                  LinkToGeneralInformation:   URL.Parse("https://example.org/about"),

                                                                                                                                                  OrganisationUnits:          [
                                                                                                                                                                                  new OrganisationUnit(
                                                                                                                                                                                      Name:                 new MultilingualString(
                                                                                                                                                                                                                new MultilingualStringValue(
                                                                                                                                                                                                                    Languages.de,
                                                                                                                                                                                                                    "Kundendienst"
                                                                                                                                                                                                                )
                                                                                                                                                                                                            ),
                                                                                                                                                                                      ContactInformation:   [
                                                                                                                                                                                                                new ContactInformation(
                                                                                                                                                                                                                    Languages:         [ Languages.de ],
                                                                                                                                                                                                                    TelephoneNumber:   "+49 241 1234567",
                                                                                                                                                                                                                    EMail:             "info@example.org"
                                                                                                                                                                                                                )
                                                                                                                                                                                                            ],
                                                                                                                                                                                      OperatingHours:       new OpenAllHours()
                                                                                                                                                                                  )
                                                                                                                                                                              ]

                                                                                                                                              ),

                                                                                                              Operator:                       new UnknownOrganisation(),
                                                                                                              Helpdesk:                       null,
                                                                                                              ApplicableForVehicles:          [
                                                                                                new VehicleCharacteristics(
                                                                                                    LoadType:                null,
                                                                                                    YearOfFirstRegistration: 2024,
                                                                                                    HeightCharacteristic:    [
                                                                                                                                 new HeightCharacteristic(
                                                                                                                                     ComparisonOperator:   ComparisonOperator.LessThanOrEqualTo,
                                                                                                                                     VehicleHeight:        Meter.Parse("2.6")
                                                                                                                                 )
                                                                                                                             ],
                                                                                                    GrossWeightCharacteristic: [
                                                                                                                                 new GrossWeightCharacteristic(
                                                                                                                                     ComparisonOperator:   ComparisonOperator.LessThanOrEqualTo,
                                                                                                                                     GrossVehicleWeight:   Tonne.Parse("3.5"),
                                                                                                                                     TypeOfWeight:         WeightType.MaximumPermitted
                                                                                                                                 )
                                                                                                                             ]
                                                                                                )
                                                                                            ],
                                                                                                              Dimension:                      new Dimension(
                                                                                                                                                  Length:       Meter.Parse("12"),
                                                                                                                                                  Width:        Meter.Parse("5"),
                                                                                                                                                  Height:       Meter.Parse("4")
                                                                                                                                              ),
                                                                                                              Amenities:                      new Amenities(
                                                                                                                                                  Illuminated:   true,
                                                                                                                                                  Roofed:        true
                                                                                                                                              ),

                                                                                                              SupplementalFacilities:         null,
                                                                                                              DedicatedParkingSpaces:         [
                                                                                                new DedicatedParkingSpaces(
                                                                                                    Id:               "1",
                                                                                                    Version:          "1",
                                                                                                    NumberOfSpaces:   4,
                                                                                                    UserSpecific:     [ UserType.Customers ]
                                                                                                )
                                                                                            ],

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
                                                                                                              EnergyInfrastructureStations:   [
                                                                                                                                                  new EnergyInfrastructureStation(

                                                                                                                                                      Id:                                       "1",
                                                                                                                                                      Version:                                  "1",
                                                                                                                                                      StationIdBNetzA:                          "E0001",
                                                                                                                                                      TotalMaximumPower:                        Watt.ParseKW("300"),
                                                                                                                                                      AuthenticationAndIdentificationMethods:   [
                                                                                                                                                                                                    AuthenticationAndIdentificationType.ActiveRFIDChip
                                                                                                                                                                                                ],
                                                                                                                                                      NumberOfRefillPoints:                     1,
                                                                                                                                                      UserInterfaceLanguages:                   [ Languages.de, Languages.en ],
                                                                                                                                                      ServiceTypes:                             [
                                                                                                                                                                                                    new Service(ServiceType.Unattended)
                                                                                                                                                                                                ],

                                                                                                                                                      Name:                                     new MultilingualString(
                                                                                                                                                                                                    new MultilingualStringValue(
                                                                                                                                                                                                        Languages.de,
                                                                                                                                                                                                        "Ladepunktgruppe 1"
                                                                                                                                                                                                    )
                                                                                                                                                                                                ),

                                                                                                                                                      RefillPoints:                             [
                                                                                                                                                                                                    new ElectricChargingPoint(

                                                                                                                                                                                                        Id:                        "1",
                                                                                                                                                                                                        Version:                   "1",
                                                                                                                                                                                                        DeliveryUnit:              DeliveryUnit.kWh,

                                                                                                                                                                                                        EVSEId:                    "DE*GEF*E12345678*1",
                                                                                                                                                                                                        UsageType:                 [ ChargingPointUsage.ElectricBike ],
                                                                                                                                                                                                        NumberOfConnectors:        2,
                                                                                                                                                                                                        AvailableVoltage:          [ Volt.ParseV("400") ],
                                                                                                                                                                                                        AvailableChargingPower:    [ Watt.ParseKW("150") ],

                                                                                                                                                                                                        Connector:                 [

                                                                                                                                                                                                                                       new Connector(
                                                                                                                                                                                                                                           ConnectorType:      ConnectorType.Chademo,
                                                                                                                                                                                                                                           MaxPowerAtSocket:   Watt.ParseKW("150"),
                                                                                                                                                                                                                                           ChargingMode:       ChargingMode.Mode1AC1p,
                                                                                                                                                                                                                                           ConnectorFormat:    ConnectorFormat.CableMode2,
                                                                                                                                                                                                                                           Voltage:            Volt. ParseV("400"),
                                                                                                                                                                                                                                           MaximumCurrent:     Ampere.ParseA("375")
                                                                                                                                                                                                                                       ),

                                                                                                                                                                                                                                       new Connector(
                                                                                                                                                                                                                                           ConnectorType:      ConnectorType.CEE3,
                                                                                                                                                                                                                                           MaxPowerAtSocket:   Watt.ParseKW("22")
                                                                                                                                                                                                                                       )

                                                                                                                                                                                                                                   ],

                                                                                                                       ElectricEnergy:            [
                                                                                                                                                      new ElectricEnergy(
                                                                                                                                                          EnergyProductName:            new MultilingualString(
                                                                                                                                                                                            new MultilingualStringValue(
                                                                                                                                                                                                Languages.de,
                                                                                                                                                                                                "Naturstrom"
                                                                                                                                                                                            )
                                                                                                                                                                                        ),
                                                                                                                                                          IsGreenEnergy:                true,
                                                                                                                                                          CarbonDioxideImpact:          12.5,
                                                                                                                                                          ElectricEnergySourceRatios:   [
                                                                                                                                                                                            new ElectricEnergySourceRatio(
                                                                                                                                                                                                EnergySource:       ElectricEnergySourceType.Wind,
                                                                                                                                                                                                SourceRatioValue:   PercentageDouble.Parse(60)
                                                                                                                                                                                            )
                                                                                                                                                                                        ]
                                                                                                                                                      )
                                                                                                                                                  ]

                                                                                                                                                                                                    )
                                                                                                                                                                                                ]

                                                                                                                                                  )
                                                                                                                                              ]

                                                                                                          )
                                                                                                      ]
                                                                     )
                                                                 ]

                               );

            var xml          = publication.ToXML();

            var isValidXML   = ValidateTableSchema(xml.ToString(), out var warning, out var errors);
            Assert.That(isValidXML,       Is.True,            String.Join(Environment.NewLine, errors));
            Assert.That(warning.Count(),  Is.EqualTo(0),      String.Join(Environment.NewLine, warning));
            Assert.That(errors. Count(),  Is.EqualTo(0),      String.Join(Environment.NewLine, errors));

        }

        #endregion

    }

}
