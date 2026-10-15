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
using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.Facilities;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Electric energy: the energy product of electric charging points, with its sources and impact
    /// (DATEX II 3.7: aegi:ElectricEnergy, written as an energyProduct of that type).
    /// </summary>
    public class ElectricEnergy(MultilingualString?                      EnergyProductName            = null,
                                Boolean?                                 IsGreenEnergy                = null,
                                Double?                                  CarbonDioxideImpact          = null,
                                Double?                                  NuclearWasteImpact           = null,
                                IEnumerable<EnergyRateReference>?        EnergyRateByReference        = null,
                                IEnumerable<ElectricEnergySourceRatio>?  ElectricEnergySourceRatios   = null,
                                IEnumerable<AOrganisation>?              MobilityServiceProviders     = null,
                                IEnumerable<EnergyRate>?                 EnergyRates                  = null,
                                XElement?                                ElectricEnergyExtension      = null,
                                URL?                                     EnergyProductInformation     = null,
                                URL?                                     RenewableEnergyEvidence      = null,
                                XElement?                                EnergyProductExtension       = null)

        : EnergyProduct(EnergyProductName,
                        IsGreenEnergy,
                        EnergyRateByReference,
                        EnergyProductInformation,
                        RenewableEnergyEvidence,
                        MobilityServiceProviders,
                        EnergyRates,
                        EnergyProductExtension)

    {

        #region Properties

        /// <summary>
        /// The carbon dioxide impact, in g/kWh.
        /// </summary>
        public Double?                                 CarbonDioxideImpact           { get; } = CarbonDioxideImpact;

        /// <summary>
        /// The nuclear waste impact, in g/kWh.
        /// </summary>
        public Double?                                 NuclearWasteImpact            { get; } = NuclearWasteImpact;

        /// <summary>
        /// The sources of the energy and their ratios.
        /// </summary>
        public IEnumerable<ElectricEnergySourceRatio>  ElectricEnergySourceRatios    { get; } = ElectricEnergySourceRatios?.Distinct() ?? [];

        /// <summary>
        /// An optional extension.
        /// </summary>
        public XElement?                               ElectricEnergyExtension       { get; } = ElectricEnergyExtension;

        #endregion


        #region TryParseXML(XML, out ElectricEnergy, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of electric energy.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ElectricEnergy">The parsed electric energy.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                  XML,
                                          [NotNullWhen(true)]  out ElectricEnergy?  ElectricEnergy,
                                          [NotNullWhen(false)] out String?          ErrorResponse)
        {

            ElectricEnergy = null;

            if (!TryParseParts(XML, out var parts, out ErrorResponse))
                return false;

            if (!XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "carbonDioxideImpact",        "carbon dioxide impact",         DatexIIXMLExtensions.TryParseDouble,  out Double?                                 carbonDioxideImpact, out ErrorResponse) ||
                !XML.TryParseOptionalValue(DatexIINS.EnergyInfrastructure + "nuclearWasteImpact",         "nuclear waste impact",          DatexIIXMLExtensions.TryParseDouble,  out Double?                                 nuclearWasteImpact,  out ErrorResponse) ||
                !XML.TryParseOptionalXMLs (DatexIINS.EnergyInfrastructure + "electricEnergySourceRatio",  "electric energy source ratio",  ElectricEnergySourceRatio.TryParseXML, out IEnumerable<ElectricEnergySourceRatio> sourceRatios,       out ErrorResponse))
            {
                return false;
            }

            ElectricEnergy = new ElectricEnergy(
                                 parts.EnergyProductName,
                                 parts.IsGreenEnergy,
                                 carbonDioxideImpact,
                                 nuclearWasteImpact,
                                 parts.EnergyRateByReference,
                                 sourceRatios,
                                 parts.MobilityServiceProviders,
                                 parts.EnergyRates,
                                 XML.Element(DatexIINS.EnergyInfrastructure + "_electricEnergyExtension"),
                                 parts.EnergyProductInformation,
                                 parts.RenewableEnergyEvidence,
                                 parts.EnergyProductExtension
                             );

            return true;

        }

        #endregion

        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object: an energy product of the type electric energy.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public override XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "energyProduct",

                   new XAttribute(DatexIINS.XSI + "type", "aegi:ElectricEnergy"),

                   ToXMLElements(),

                   CarbonDioxideImpact.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "carbonDioxideImpact",   CarbonDioxideImpact.Value)
                       : null,

                   NuclearWasteImpact.HasValue
                       ? new XElement(DatexIINS.EnergyInfrastructure + "nuclearWasteImpact",    NuclearWasteImpact. Value)
                       : null,

                   ElectricEnergySourceRatios.Select(ratio => ratio.ToXML(DatexIINS.EnergyInfrastructure + "electricEnergySourceRatio")),

                   ElectricEnergyExtension is not null
                       ? ElectricEnergyExtension.ToExtensionXML(DatexIINS.EnergyInfrastructure + "_electricEnergyExtension")
                       : null

               );

        #endregion

    }

}
