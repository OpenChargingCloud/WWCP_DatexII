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
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A specific offer of energy, optionally described with its electrical mix and its rates.
    /// </summary>
    [XmlType("ElectricEnergy", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class ElectricEnergy(MultilingualString?                      EnergyProductName            = null,
                                Boolean?                                 IsGreenEnergy                = null,
                                Double?                                  CarbonDioxideImpact          = null,
                                Double?                                  NuclearWasteImpact           = null,
                                IEnumerable<EnergyRateReference>?        EnergyRateByReference        = null,
                                IEnumerable<ElectricEnergySourceRatio>?  ElectricEnergySourceRatios   = null,
                                IEnumerable<AOrganisation>?              MobilityServiceProviders     = null,
                                IEnumerable<EnergyRate>?                 EnergyRates                  = null,
                                XElement?                                ElectricEnergyExtension      = null)
    {

        #region Properties

        /// <summary>
        /// A name for this energy product
        /// </summary>
        [XmlElement(ElementName = "energyProductName",          Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public MultilingualString?                     EnergyProductName             { get; } = EnergyProductName;

        /// <summary>
        /// True if 100% from regenerative sources (CO2 and nuclear waste is zero)
        /// </summary>
        [XmlElement(ElementName = "isGreenEnergy",              Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Boolean?                                IsGreenEnergy                 { get; } = IsGreenEnergy;

        /// <summary>
        /// Exhausted carbon dioxide in grams per kilowatt hour.
        /// </summary>
        [XmlElement(ElementName = "carbonDioxideImpact",        Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Double?                                 CarbonDioxideImpact           { get; } = CarbonDioxideImpact;

        /// <summary>
        /// Produced nuclear waste in grams per kilowatt hour.
        /// </summary>
        [XmlElement(ElementName = "nuclearWasteImpact",         Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public Double?                                 NuclearWasteImpact            { get; } = NuclearWasteImpact;

        /// <summary>
        /// Specifies one or more rates for this type of electric energy by reference to existing EnergyRate specifications.
        /// </summary>
        [XmlElement(ElementName = "energyRateByReference",      Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyRateReference>        EnergyRateByReference         { get; } = EnergyRateByReference?.     Distinct() ?? [];

        /// <summary>
        /// Defines one or more energy source ratios.
        /// </summary>
        [XmlElement(ElementName = "electricEnergySourceRatio",  Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<ElectricEnergySourceRatio>  ElectricEnergySourceRatios    { get; } = ElectricEnergySourceRatios?.Distinct() ?? [];

        /// <summary>
        /// Mobility service providers offering contract-based recharging.
        /// </summary>
        [XmlElement(ElementName = "mobilityServiceProvider",    Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<AOrganisation>              MobilityServiceProviders      { get; } = MobilityServiceProviders?.  Distinct() ?? [];

        /// <summary>
        /// Specifies one or more rates for this type of electric energy.
        /// </summary>
        [XmlElement(ElementName = "energyRate",                 Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyRate>                 EnergyRates                   { get; } = EnergyRates?.               Distinct() ?? [];

        /// <summary>
        /// Optional extension element for additional content.
        /// </summary>
        [XmlElement(ElementName = "_electricEnergyExtension",   Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public XElement?                               ElectricEnergyExtension       { get; } = ElectricEnergyExtension;

        #endregion


        #region TryParseXML(XML, out ElectricEnergy, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an electric energy product.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ElectricEnergy">The parsed electric energy product.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                  XML,
                                          [NotNullWhen(true)]  out ElectricEnergy?  ElectricEnergy,
                                          [NotNullWhen(false)] out String?          ErrorResponse)
        {

            ElectricEnergy = null;

            if (!XML.TryParseOptionalXML   (DatexIINS.EnergyInfrastructure + "energyProductName",          "energy product name",           MultilingualString.         TryParseXML,  out MultilingualString?                     energyProductName,          out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "isGreenEnergy",              "is green energy",               DatexIIXMLExtensions.       TryParseBoolean, out Boolean?                         isGreenEnergy,              out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "carbonDioxideImpact",        "carbon dioxide impact",         DatexIIXMLExtensions.       TryParseDouble,  out Double?                          carbonDioxideImpact,        out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "nuclearWasteImpact",         "nuclear waste impact",          DatexIIXMLExtensions.       TryParseDouble,  out Double?                          nuclearWasteImpact,         out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "energyRateByReference",      "energy rate by reference",      EnergyRateReference.        TryParseXML,  out IEnumerable<EnergyRateReference>        energyRateByReference,      out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "electricEnergySourceRatio",  "electric energy source ratio",  ElectricEnergySourceRatio.  TryParseXML,  out IEnumerable<ElectricEnergySourceRatio>  electricEnergySourceRatios, out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "mobilityServiceProvider",    "mobility service provider",     AOrganisation.              TryParseXML,  out IEnumerable<AOrganisation>              mobilityServiceProviders,   out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "energyRate",                 "energy rate",                   EnergyRate.                 TryParseXML,  out IEnumerable<EnergyRate>                 energyRates,                out ErrorResponse))
            {
                return false;
            }

            ElectricEnergy = new ElectricEnergy(
                                 energyProductName,
                                 isGreenEnergy,
                                 carbonDioxideImpact,
                                 nuclearWasteImpact,
                                 energyRateByReference,
                                 electricEnergySourceRatios,
                                 mobilityServiceProviders,
                                 energyRates,
                                 XML.Element(DatexIINS.EnergyInfrastructure + "_electricEnergyExtension")
                             );

            return true;

        }

        #endregion


        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "electricEnergy",

                   EnergyProductName is not null
                       ? EnergyProductName.ToXML(DatexIINS.EnergyInfrastructure + "energyProductName")
                       : null,

                   IsGreenEnergy.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "isGreenEnergy",         IsGreenEnergy.      Value)
                       : null,

                   CarbonDioxideImpact.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "carbonDioxideImpact",   CarbonDioxideImpact.Value)
                       : null,

                   NuclearWasteImpact.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "nuclearWasteImpact",    NuclearWasteImpact. Value)
                       : null,

                   EnergyRateByReference.     Select(energyRateReference      => energyRateReference.ToXML(DatexIINS.EnergyInfrastructure + "energyRateByReference")),
                   ElectricEnergySourceRatios.Select(electricEnergySourceRatio => electricEnergySourceRatio.ToXML(DatexIINS.EnergyInfrastructure + "electricEnergySourceRatio")),
                   MobilityServiceProviders.  Select(mobilityServiceProvider   => mobilityServiceProvider.  ToXML(DatexIINS.EnergyInfrastructure + "mobilityServiceProvider")),
                   EnergyRates.               Select(energyRate                => energyRate.               ToXML(DatexIINS.EnergyInfrastructure + "energyRate")),

                   ElectricEnergyExtension is not null
                       ? new XElement(DatexIINS.EnergyInfrastructure + "_electricEnergyExtension", ElectricEnergyExtension)
                       : null

               );

        #endregion

    }

}
