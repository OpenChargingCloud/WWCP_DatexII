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

using System.Xml.Linq;

using NUnit.Framework;

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.D2Payload;
using cloud.charging.open.protocols.DatexII.v3.Facilities;
using cloud.charging.open.protocols.DatexII.v3.LocationExtension;
using cloud.charging.open.protocols.DatexII.v3.LocationReferencing;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

#endregion

namespace cloud.charging.open.protocols.DatexII.Tests
{

    /// <summary>
    /// Unit tests for loading and parsing DatexII Example XMLs.
    /// </summary>
    [TestFixture]
    public class LoadAndParse_DatexIIExamples
    {

        #region EnergyInfrastructure_StatusPublication()

        /// <summary>
        /// A test loading and parsing the Energy Infrastructure Status Publication example.
        /// </summary>
        [Test]
        public void EnergyInfrastructure_StatusPublication()
        {

            var xml = XDocument.Parse(File.ReadAllText("Examples/EnergyInfrastructureStatusPublication.xml"));
            Assert.That(xml,       Is.Not.Null);
            Assert.That(xml.Root,  Is.Not.Null);

            

            if (xml.Root is not null)
            {

                //var success = EnergyInfrastructureStatusPublication.TryParseXML(
                //                  xml.Root,
                //                  out var energyInfrastructureStatusPublication,
                //                  out var errorResponse
                //              );

                var success = Payload.TryParseXML(
                                  xml.Root,
                                  out var payload,  //ToDo: Payload vs. APayloadPublication!
                                  out var errorResponse
                              );

                Assert.That(success,                                Is.True);
                Assert.That(payload,                                Is.Not.Null);
                Assert.That(errorResponse,                          Is.Null);

                var energyInfrastructureStatusPublication = payload as EnergyInfrastructureStatusPublication;

                Assert.That(energyInfrastructureStatusPublication,  Is.Not.Null);

                if (energyInfrastructureStatusPublication is not null)
                {

                    // APayloadPublication members
                    Assert.That(energyInfrastructureStatusPublication.PublicationTime.ToIso8601WithOffset(false),   Is.EqualTo("2025-02-02T12:50:00+01:00"));
                    Assert.That(energyInfrastructureStatusPublication.Language,                                     Is.EqualTo(Languages.de));
                    Assert.That(energyInfrastructureStatusPublication.ModelBaseVersion,                             Is.EqualTo("3"));
                    Assert.That(energyInfrastructureStatusPublication.ExtensionName,                                Is.Null);
                    Assert.That(energyInfrastructureStatusPublication.ExtensionVersion,                             Is.Null);
                    Assert.That(energyInfrastructureStatusPublication.ProfileName,                                  Is.EqualTo("Level C profile Energy Infrastructure"));
                    Assert.That(energyInfrastructureStatusPublication.ProfileVersion,                               Is.EqualTo("00-01-00"));
                    Assert.That(energyInfrastructureStatusPublication.PayloadPublicationExtension,                  Is.Null);


                    Assert.That(energyInfrastructureStatusPublication.PublicationCreator,                           Is.Not.Null);

                    var publicationCreator = energyInfrastructureStatusPublication.PublicationCreator;
                    if (publicationCreator is not null)
                    {
                        Assert.That(publicationCreator.Country,                                                     Is.EqualTo(Country.Germany));
                        Assert.That(publicationCreator.NationalIdentifier,                                          Is.EqualTo("DE-NAP-OrganisationXY"));
                        Assert.That(publicationCreator.InternationalIdentifierExtension,                            Is.Null);
                    }


                    Assert.That(energyInfrastructureStatusPublication.HeaderInformation,                            Is.Not.Null);

                    var headerInformation = energyInfrastructureStatusPublication.HeaderInformation;
                    if (headerInformation is not null)
                    {
                        Assert.That(headerInformation. InformationStatus,                                           Is.EqualTo(InformationStatus.Test));
                        Assert.That(headerInformation. Confidentiality,                                             Is.EqualTo(Confidentiality.  NoRestriction));
                        Assert.That(headerInformation. AllowedDeliveryChannel.Count(),                              Is.EqualTo(1));
                        Assert.That(headerInformation. AllowedDeliveryChannel.First(),                              Is.EqualTo(InformationDeliveryService.AnyGeneralDeliveryService));
                        Assert.That(headerInformation. HeaderInformationExtension,                                  Is.Null);
                    }


                    Assert.That(energyInfrastructureStatusPublication.TableReferences,                              Is.Not.Null);

                    var tableReferences = energyInfrastructureStatusPublication.TableReferences;
                    if (tableReferences is not null)
                    {
                        Assert.That(tableReferences.   Count(),                                                     Is.EqualTo(1));
                        Assert.That(tableReferences.   First().Id,                                                  Is.EqualTo("2474A514-0E5D-48F9-A908-F185DD4177A2"));
                        Assert.That(tableReferences.   First().Version,                                             Is.EqualTo("2"));
                        Assert.That(tableReferences.   First().TargetClass,                                         Is.EqualTo("egi:EnergyInfrastructureTable"));
                    }


                    Assert.That(energyInfrastructureStatusPublication.EnergyInfrastructureSiteStatus,               Is.Not.Null);

                    var energyInfrastructureSiteStatus = energyInfrastructureStatusPublication.EnergyInfrastructureSiteStatus;
                    if (energyInfrastructureSiteStatus is not null)
                    {
                    //    //Assert.That(energyInfrastructureSiteStatus.InformationStatus,                             Is.EqualTo(InformationStatus.Test));
                    //    //Assert.That(energyInfrastructureSiteStatus.Confidentiality,                               Is.EqualTo(Confidentiality.  NoRestriction));
                    //    //Assert.That(energyInfrastructureSiteStatus.AllowedDeliveryChannel.Count(),                Is.EqualTo(1));
                    //    //Assert.That(energyInfrastructureSiteStatus.AllowedDeliveryChannel.First(),                Is.EqualTo(InformationDeliveryService.AnyGeneralDeliveryService));
                    //    //Assert.That(energyInfrastructureSiteStatus.HeaderInformationExtension,                    Is.Null);
                    }

                }

            }

        }

        #endregion

        #region EnergyInfrastructure_TablePublication()

        /// <summary>
        /// A test loading and parsing the Energy Infrastructure Table Publication example.
        /// </summary>
        [Test]
        public void EnergyInfrastructure_TablePublication()
        {

            var xml = XDocument.Parse(File.ReadAllText("Examples/EnergyInfrastructureTablePublication.xml"));
            Assert.That(xml,       Is.Not.Null);
            Assert.That(xml.Root,  Is.Not.Null);

            if (xml.Root is not null)
            {

                //var success = EnergyInfrastructureTablePublication.TryParseXML(
                //                  xml.Root,
                //                  out var energyInfrastructureTablePublication,
                //                  out var errorResponse
                //              );

                var success = Payload.TryParseXML(
                                  xml.Root,
                                  out var payload,  //ToDo: Payload vs. APayloadPublication!
                                  out var errorResponse
                              );

                Assert.That(success,                               Is.True);
                Assert.That(payload,                               Is.Not.Null);
                Assert.That(errorResponse,                         Is.Null);

                var energyInfrastructureTablePublication = payload as EnergyInfrastructureTablePublication;

                Assert.That(energyInfrastructureTablePublication,  Is.Not.Null);


                Assert.That(success,                               Is.True);
                Assert.That(energyInfrastructureTablePublication,  Is.Not.Null);
                Assert.That(errorResponse,                         Is.Null);

                if (energyInfrastructureTablePublication is not null)
                {

                    // APayloadPublication members
                    Assert.That(energyInfrastructureTablePublication.PublicationTime.ToIso8601(),   Is.EqualTo("2025-01-10T10:13:51.000Z"));
                    Assert.That(energyInfrastructureTablePublication.Language,                      Is.EqualTo(Languages.de));
                    Assert.That(energyInfrastructureTablePublication.ModelBaseVersion,              Is.EqualTo("3"));
                    Assert.That(energyInfrastructureTablePublication.ExtensionName,                 Is.Null);
                    Assert.That(energyInfrastructureTablePublication.ExtensionVersion,              Is.Null);
                    Assert.That(energyInfrastructureTablePublication.ProfileName,                   Is.EqualTo("Level C profile Energy Infrastructure"));
                    Assert.That(energyInfrastructureTablePublication.ProfileVersion,                Is.EqualTo("00-01-00"));
                    Assert.That(energyInfrastructureTablePublication.PayloadPublicationExtension,   Is.Null);


                    Assert.That(energyInfrastructureTablePublication.PublicationCreator,            Is.Not.Null);

                    var publicationCreator = energyInfrastructureTablePublication.PublicationCreator;
                    if (publicationCreator is not null)
                    {
                        Assert.That(publicationCreator.Country,                            Is.EqualTo(Country.Germany));
                        Assert.That(publicationCreator.NationalIdentifier,                 Is.EqualTo("DE-NAP-OrganisationXY"));
                        Assert.That(publicationCreator.InternationalIdentifierExtension,   Is.Null);
                    }


                    Assert.That(energyInfrastructureTablePublication.HeaderInformation,             Is.Not.Null);

                    var headerInformation = energyInfrastructureTablePublication.HeaderInformation;
                    if (headerInformation is not null)
                    {
                        Assert.That(headerInformation. InformationStatus,                  Is.EqualTo(InformationStatus.Test));
                        Assert.That(headerInformation. Confidentiality,                    Is.EqualTo(Confidentiality.  NoRestriction));
                        Assert.That(headerInformation. AllowedDeliveryChannel.Count(),     Is.EqualTo(1));
                        Assert.That(headerInformation. AllowedDeliveryChannel.First(),     Is.EqualTo(InformationDeliveryService.AnyGeneralDeliveryService));
                        Assert.That(headerInformation. HeaderInformationExtension,         Is.Null);
                    }


                    Assert.That(energyInfrastructureTablePublication.EnergyInfrastructureTables,    Is.Not.Null);

                    var energyInfrastructureTables = energyInfrastructureTablePublication.EnergyInfrastructureTables;
                    if (energyInfrastructureTables is not null)
                    {
                        //Assert.That(headerInformation. InformationStatus,                  Is.EqualTo(InformationStatus.Test));
                        //Assert.That(headerInformation. Confidentiality,                    Is.EqualTo(Confidentiality.  NoRestriction));
                        //Assert.That(headerInformation. AllowedDeliveryChannel.Count(),     Is.EqualTo(1));
                        //Assert.That(headerInformation. AllowedDeliveryChannel.First(),     Is.EqualTo(InformationDeliveryService.AnyGeneralDeliveryService));
                        //Assert.That(headerInformation. HeaderInformationExtension,         Is.Null);
                    }

                }

            }

        }

        #endregion

    }

}
