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

namespace cloud.charging.open.protocols.DatexII.v3.D2Payload
{

    /// <summary>
    /// A payload publication of traffic related information or associated management information
    /// created at a specific point in time that can be exchanged via a DATEX II interface.
    /// </summary>
    [XmlType("PayloadPublication", Namespace = "http://datex2.eu/schema/3/common")]
    [XmlRoot("payload",            Namespace = "http://datex2.eu/schema/3/d2Payload")]
    public class Payload(DateTime                 PublicationTime,
                         InternationalIdentifier  PublicationCreator,
                         Languages                Language)

        : APayloadPublication(PublicationTime,
                              PublicationCreator,
                              Language)

    {


        // <?xml version="1.0" encoding="utf-8" standalone="no"?>
        // <xs:schema elementFormDefault="qualified" attributeFormDefault="unqualified"
        //            xmlns:d2        = "http://datex2.eu/schema/3/d2Payload"
        //            version         = "3.5"
        //            targetNamespace = "http://datex2.eu/schema/3/d2Payload"
        //            xmlns:com       = "http://datex2.eu/schema/3/common"
        //            xmlns:comx      = "http://datex2.eu/schema/3/commonExtension"
        //            xmlns:locx      = "http://datex2.eu/schema/3/locationExtension"
        //            xmlns:loc       = "http://datex2.eu/schema/3/locationReferencing"
        //            xmlns:egi       = "http://datex2.eu/schema/3/energyInfrastructure"
        //            xmlns:fac       = "http://datex2.eu/schema/3/facilities"
        //            xmlns:xs        = "http://www.w3.org/2001/XMLSchema">
        //
        //   <xs:import namespace="http://datex2.eu/schema/3/facilities"           schemaLocation="DATEXII_3_Facilities.xsd" />
        //   <xs:import namespace="http://datex2.eu/schema/3/energyInfrastructure" schemaLocation="DATEXII_3_EnergyInfrastructure.xsd" />
        //   <xs:import namespace="http://datex2.eu/schema/3/locationReferencing"  schemaLocation="DATEXII_3_LocationReferencing.xsd" />
        //   <xs:import namespace="http://datex2.eu/schema/3/locationExtension"    schemaLocation="DATEXII_3_LocationExtension.xsd" />
        //   <xs:import namespace="http://datex2.eu/schema/3/commonExtension"      schemaLocation="DATEXII_3_CommonExtension.xsd" />
        //   <xs:import namespace="http://datex2.eu/schema/3/common"               schemaLocation="DATEXII_3_Common.xsd" />
        //
        //   <xs:element name="payload" type="com:PayloadPublication">
        //
        //     <xs:unique name="_payloadOperatingHoursSpecificationConstraint">
        //       <xs:selector xpath=".//fac:operatingHoursSpecification" />
        //       <xs:field xpath="@id" />
        //       <xs:field xpath="@version" />
        //     </xs:unique>
        //
        //     <xs:unique name="_payloadOrganisationSpecificationConstraint">
        //       <xs:selector xpath=".//fac:organisationSpecification" />
        //       <xs:field xpath="@id" />
        //       <xs:field xpath="@version" />
        //     </xs:unique>
        //
        //     <xs:unique name="_payloadFacilityObjectConstraint">
        //       <xs:selector xpath=".//fac:facilityObject" />
        //       <xs:field xpath="@id" />
        //       <xs:field xpath="@version" />
        //     </xs:unique>
        //
        //     <xs:unique name="_payloadEnergyRateConstraint">
        //       <xs:selector xpath=".//egi:energyRate" />
        //       <xs:field xpath="@id" />
        //     </xs:unique>
        //
        //     <xs:unique name="_payloadEnergyInfrastructureTableConstraint">
        //       <xs:selector xpath=".//egi:energyInfrastructureTable" />
        //       <xs:field xpath="@id" />
        //       <xs:field xpath="@version" />
        //     </xs:unique>
        //
        //   </xs:element>
        //
        // </xs:schema>

    }

}
