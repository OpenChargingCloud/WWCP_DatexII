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

using System.Xml.Serialization;

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using System.Xml.Linq;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A publication of static information on the infrastructure for vehicle energy supply.
    /// </summary>
    [XmlType("EnergyInfrastructureTablePublication", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyInfrastructureTablePublication(DateTime                                 PublicationTime,
                                                      InternationalIdentifier                  PublicationCreator,
                                                      Languages                                Language,

                                                      HeaderInformation?                       HeaderInformation            = null,
                                                      IEnumerable<EnergyInfrastructureTable>?  EnergyInfrastructureTables   = null)

        : APayloadPublication(PublicationTime,
                              PublicationCreator,
                              Language)

    {

        /// <summary>
        /// Management information relating to the publication.
        /// </summary>
        [XmlElement("headerInformation",                               Namespace = "http://datex2.eu/schema/3/common")]
        public HeaderInformation?                      HeaderInformation                                { get; set; } = HeaderInformation;

        /// <summary>
        /// A collection of EnergyInfrastructureTable instances.
        /// </summary>
        [XmlElement("energyInfrastructureTable",                       Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyInfrastructureTable>  EnergyInfrastructureTables                       { get; set; } = EnergyInfrastructureTables?.Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional publication information.
        /// </summary>
        [XmlElement("_energyInfrastructureTablePublicationExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                               EnergyInfrastructureTablePublicationExtension    { get; set; }




        // <?xml version="1.0" encoding="UTF-8"?>
        // <d2:payload xmlns:d2 = "http://datex2.eu/schema/3/d2Payload"
        //  xmlns:com           = "http://datex2.eu/schema/3/common"
        //  xmlns:locx          = "http://datex2.eu/schema/3/locationExtension"
        //  xmlns:loc           = "http://datex2.eu/schema/3/locationReferencing"
        //  xmlns               = "http://datex2.eu/schema/3/energyInfrastructure"
        //  xmlns:fac           = "http://datex2.eu/schema/3/facilities"
        //  xmlns:prk           = "http://datex2.eu/schema/3/parking"
        //  xmlns:xsi           = "http://www.w3.org/2001/XMLSchema-instance"
        //  xsi:schemaLocation  = "http://datex2.eu/schema/3/d2Payload DATEXII_3_D2Payload.xsd"
        //  xmlns:egi           = "http://datex2.eu/schema/3/energyInfrastructure"
        //  xsi:type            = "egi:EnergyInfrastructureTablePublication"
        //  lang                = "de"
        //  modelBaseVersion    = "3"
        //  profileName         = "Level C profile Energy Infrastructure"
        //  profileVersion      = "00-01-00">
        //
        //     <com:publicationTime>2025-01-10T11:13:51+01:00</com:publicationTime>
        //
        //     <com:publicationCreator>
        //         <com:country>de</com:country>
        //         <com:nationalIdentifier>DE-NAP-OrganisationXY</com:nationalIdentifier>
        //     </com:publicationCreator>
        //
        //     <headerInformation>
        //         <com:confidentiality>noRestriction</com:confidentiality>
        //         <com:allowedDeliveryChannel>anyGeneralDeliveryService</com:allowedDeliveryChannel>
        //         <com:informationStatus>test</com:informationStatus>
        //     </headerInformation>
        // 
        //     <energyInfrastructureTable id="2474A514-0E5D-48F9-A908-F185DD4177A2" version="2">
        //       ...
        //     </energyInfrastructureTable>
        // 
        // </d2:payload>


        public XElement ToXML()
        {

            //XNamespace nsD2   = "http://datex2.eu/schema/3/d2Payload";
            //XNamespace nsCom  = "http://datex2.eu/schema/3/common";
            //XNamespace nsLocx = "http://datex2.eu/schema/3/locationExtension";
            //XNamespace nsLoc  = "http://datex2.eu/schema/3/locationReferencing";
            //XNamespace nsFac  = "http://datex2.eu/schema/3/facilities";
            //XNamespace nsPrk  = "http://datex2.eu/schema/3/parking";
            //// Default-Namespace (xmlns="http://datex2.eu/schema/3/energyInfrastructure")
            ////XNamespace ns     = "http://datex2.eu/schema/3/energyInfrastructure";
            //// Der "egi"-Namespace verweist ebenfalls auf "http://datex2.eu/schema/3/energyInfrastructure"
            //XNamespace nsEgi  = "http://datex2.eu/schema/3/energyInfrastructure";
            //// Schema-Instance Namespace
            //XNamespace nsXsi  = "http://www.w3.org/2001/XMLSchema-instance";

            var xml = new XElement(XML_IO.nsD2 + "payload",

                          new XAttribute(XNamespace.Xmlns + "d2",   XML_IO.nsD2),
                          new XAttribute(XNamespace.Xmlns + "com",  XML_IO.nsCom),
                          new XAttribute(XNamespace.Xmlns + "locx", XML_IO.nsLocx),
                          new XAttribute(XNamespace.Xmlns + "loc",  XML_IO.nsLoc),
                          new XAttribute(XNamespace.Xmlns + "fac",  XML_IO.nsFac),
                          new XAttribute(XNamespace.Xmlns + "prk",  XML_IO.nsPrk),
                          new XAttribute(XNamespace.Xmlns + "xsi",  XML_IO.nsXsi),

                          new XAttribute(XML_IO.nsXsi + "schemaLocation",  "http://datex2.eu/schema/3/d2Payload DATEXII_3_D2Payload.xsd"),
                          new XAttribute(XML_IO.nsXsi + "type",            "egi:EnergyInfrastructureTablePublication"),
                          new XAttribute("lang",                    Language.AsText()),
                          new XAttribute("modelBaseVersion",        "3"),
                          new XAttribute("profileName",             "Level C profile Energy Infrastructure"),
                          new XAttribute("profileVersion",          "00-01-00"),

                          new XElement(XML_IO.nsCom + "publicationTime",   PublicationTime.ToIso8601()),

                          PublicationCreator.ToXML(),
                          HeaderInformation?.ToXML(),

                          new XElement(XML_IO.nsEgi + "energyInfrastructureTable", "xxx"
                          )

                      );

            return xml;

        }


        public XDocument ToXDoc()
        {

            return new XDocument(
                       new XDeclaration("1.0", "UTF-8", null),
                       ToXML()
                   );

        }


    }

}
