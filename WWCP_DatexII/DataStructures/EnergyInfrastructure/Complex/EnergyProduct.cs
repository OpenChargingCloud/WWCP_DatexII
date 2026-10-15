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
    /// An energy product offered at a station or refill point, with its rates (DATEX II 3.7: aegi:EnergyProduct).
    /// Electric energy is the energy product of electric charging points.
    /// </summary>
    public class EnergyProduct(MultilingualString?                 EnergyProductName          = null,
                               Boolean?                            IsGreenEnergy              = null,
                               IEnumerable<EnergyRateReference>?   EnergyRateByReference      = null,
                               URL?                                EnergyProductInformation   = null,
                               URL?                                RenewableEnergyEvidence    = null,
                               IEnumerable<AOrganisation>?         MobilityServiceProviders   = null,
                               IEnumerable<EnergyRate>?            EnergyRates                = null,
                               XElement?                           EnergyProductExtension     = null)
    {

        #region Properties

        /// <summary>
        /// The name of the energy product.
        /// </summary>
        public MultilingualString?               EnergyProductName           { get; } = EnergyProductName;

        /// <summary>
        /// Whether the energy is produced from renewable sources only.
        /// </summary>
        public Boolean?                          IsGreenEnergy               { get; } = IsGreenEnergy;

        /// <summary>
        /// The rates of the energy product defined elsewhere, by reference.
        /// </summary>
        public IEnumerable<EnergyRateReference>  EnergyRateByReference       { get; } = EnergyRateByReference?.Distinct() ?? [];

        /// <summary>
        /// A link to information on the energy product.
        /// </summary>
        public URL?                              EnergyProductInformation    { get; } = EnergyProductInformation;

        /// <summary>
        /// A link to the evidence that the energy is renewable.
        /// </summary>
        public URL?                              RenewableEnergyEvidence     { get; } = RenewableEnergyEvidence;

        /// <summary>
        /// The mobility service providers offering the energy product.
        /// </summary>
        public IEnumerable<AOrganisation>        MobilityServiceProviders    { get; } = MobilityServiceProviders?.Distinct() ?? [];

        /// <summary>
        /// The rates of the energy product.
        /// </summary>
        public IEnumerable<EnergyRate>           EnergyRates                 { get; } = EnergyRates?.Distinct() ?? [];

        /// <summary>
        /// An optional extension.
        /// </summary>
        public XElement?                         EnergyProductExtension      { get; } = EnergyProductExtension;

        #endregion


        #region TryParseXML(XML, out EnergyProduct, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an energy product, by its xsi:type: electric energy, or an energy product.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyProduct">The parsed energy product.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                 XML,
                                          [NotNullWhen(true)]  out EnergyProduct?  EnergyProduct,
                                          [NotNullWhen(false)] out String?         ErrorResponse)
        {

            EnergyProduct = null;

            if (XML.XSIType() == "ElectricEnergy")
            {
                if (ElectricEnergy.TryParseXML(XML, out var electricEnergy, out ErrorResponse))
                {
                    EnergyProduct = electricEnergy;
                    return true;
                }
                return false;
            }

            if (!TryParseParts(XML, out var parts, out ErrorResponse))
                return false;

            EnergyProduct = new EnergyProduct(
                                parts.EnergyProductName,
                                parts.IsGreenEnergy,
                                parts.EnergyRateByReference,
                                parts.EnergyProductInformation,
                                parts.RenewableEnergyEvidence,
                                parts.MobilityServiceProviders,
                                parts.EnergyRates,
                                parts.EnergyProductExtension
                            );

            return true;

        }

        #endregion

        #region (protected static) TryParseParts(XML, out Parts, out ErrorResponse)

        /// <summary>
        /// The particles of an energy product, as parsed.
        /// </summary>
        protected internal sealed record Parts(MultilingualString?               EnergyProductName,
                                               Boolean?                          IsGreenEnergy,
                                               IEnumerable<EnergyRateReference>  EnergyRateByReference,
                                               URL?                              EnergyProductInformation,
                                               URL?                              RenewableEnergyEvidence,
                                               IEnumerable<AOrganisation>        MobilityServiceProviders,
                                               IEnumerable<EnergyRate>           EnergyRates,
                                               XElement?                         EnergyProductExtension);

        /// <summary>
        /// Try to parse the particles of an energy product.
        /// </summary>
        protected internal static Boolean TryParseParts(XElement                          XML,
                                                        [NotNullWhen(true)]  out Parts?   Parts,
                                                        [NotNullWhen(false)] out String?  ErrorResponse)
        {

            Parts = null;

            if (!XML.TryParseOptionalXML   (DatexIINS.EnergyInfrastructure + "energyProductName",         "energy product name",          MultilingualString.  TryParseXML,     out MultilingualString?               energyProductName,        out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.EnergyInfrastructure + "isGreenEnergy",             "is green energy",              DatexIIXMLExtensions.TryParseBoolean, out Boolean?                          isGreenEnergy,            out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "energyRateByReference",     "energy rate by reference",     EnergyRateReference. TryParseXML,     out IEnumerable<EnergyRateReference>  energyRateByReference,    out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "mobilityServiceProvider",   "mobility service provider",    AOrganisation.       TryParseXML,     out IEnumerable<AOrganisation>        mobilityServiceProviders, out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.EnergyInfrastructure + "energyRate",                "energy rate",                  EnergyRate.          TryParseXML,     out IEnumerable<EnergyRate>           energyRates,              out ErrorResponse))
            {
                return false;
            }

            Parts = new Parts(
                        energyProductName,
                        isGreenEnergy,
                        energyRateByReference,
                        XML.OptionalURL(DatexIINS.EnergyInfrastructure + "energyProductInformation"),
                        XML.OptionalURL(DatexIINS.EnergyInfrastructure + "renewableEnergyEvidence"),
                        mobilityServiceProviders,
                        energyRates,
                        XML.Element(DatexIINS.EnergyInfrastructure + "_energyProductExtension")
                    );

            return true;

        }

        #endregion


        #region (protected) ToXMLElements()

        /// <summary>
        /// The particles of an energy product, in schema order.
        /// </summary>
        protected virtual IEnumerable<Object?> ToXMLElements()
        {

            if (EnergyProductName is not null)
                yield return EnergyProductName.ToXML(DatexIINS.EnergyInfrastructure + "energyProductName");

            if (IsGreenEnergy.HasValue)
                yield return new XElement(DatexIINS.EnergyInfrastructure + "isGreenEnergy",             IsGreenEnergy.Value ? "true" : "false");

            foreach (var energyRateReference in EnergyRateByReference)
                yield return energyRateReference.ToXML(DatexIINS.EnergyInfrastructure + "energyRateByReference");

            if (EnergyProductInformation.HasValue)
                yield return new XElement(DatexIINS.EnergyInfrastructure + "energyProductInformation",  EnergyProductInformation.Value.ToString());

            if (RenewableEnergyEvidence.HasValue)
                yield return new XElement(DatexIINS.EnergyInfrastructure + "renewableEnergyEvidence",   RenewableEnergyEvidence. Value.ToString());

            foreach (var mobilityServiceProvider in MobilityServiceProviders)
                yield return mobilityServiceProvider.ToXML(DatexIINS.EnergyInfrastructure + "mobilityServiceProvider");

            foreach (var energyRate in EnergyRates)
                yield return energyRate.ToXML(DatexIINS.EnergyInfrastructure + "energyRate");

            if (EnergyProductExtension is not null)
                yield return EnergyProductExtension.ToExtensionXML(DatexIINS.EnergyInfrastructure + "_energyProductExtension");

        }

        #endregion

        #region ToXML(XMLName = null)

        /// <summary>
        /// Return an XML representation of this object.
        /// </summary>
        /// <param name="XMLName">An alternative XML element name.</param>
        public virtual XElement ToXML(XName? XMLName = null)

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "energyProduct",
                    ToXMLElements());

        #endregion

    }

}
