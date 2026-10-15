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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Ratio for the specified energy source.
    /// </summary>
    [XmlType("ElectricEnergySourceRatio", Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
    public class ElectricEnergySourceRatio(ElectricEnergySourceType  EnergySource,
                                           PercentageDouble          SourceRatioValue,
                                           String?                   OtherEnergySource                    = null,
                                           XElement?                 ElectricEnergySourceRatioExtension   = null)
    {

        #region Properties

        /// <summary>
        /// An electric energy source.
        /// </summary>
        [XmlElement(ElementName = "energySource",       Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public ElectricEnergySourceType  EnergySource                          { get; } = EnergySource;

        /// <summary>
        /// Some other energy source.
        /// </summary>
        [XmlElement(ElementName = "otherEnergySource",  Namespace = "http://datex2.eu/schema/3/common")]
        public String?                   OtherEnergySource                     { get; } = OtherEnergySource;

        /// <summary>
        /// The percentage ratio value of this energy source.
        /// </summary>
        [XmlElement(ElementName = "sourceRatioValue",   Namespace = "http://datex2.eu/schema/3/common")]
        public PercentageDouble          SourceRatioValue                      { get; } = SourceRatioValue;

        /// <summary>
        /// Optional extension element for additional content.
        /// </summary>
        [XmlElement(ElementName = "_electricEnergySourceRatioExtension", Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                 ElectricEnergySourceRatioExtension    { get; } = ElectricEnergySourceRatioExtension;

        #endregion


        #region TryParseXML(XML, out ElectricEnergySourceRatio, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an electric energy source ratio.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="ElectricEnergySourceRatio">The parsed electric energy source ratio.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                             XML,
                                          [NotNullWhen(true)]  out ElectricEnergySourceRatio?  ElectricEnergySourceRatio,
                                          [NotNullWhen(false)] out String?                     ErrorResponse)
        {

            ElectricEnergySourceRatio = null;

            if (!XML.TryParseMandatoryValue(DatexIINS.EnergyInfrastructure + "energySource",
                                            "energy source",
                                            ElectricEnergySourceType.TryParse,
                                            out ElectricEnergySourceType energySource,
                                            out ErrorResponse))
            {
                return false;
            }

            // A PercentageValue: the figure sits in a common percentage child.
            var ratioXML = XML.Element(DatexIINS.EnergyInfrastructure + "sourceRatioValue");

            if (ratioXML is null ||
               !ratioXML.TryParseMandatoryValue(DatexIINS.Common + "percentage",
                                                "source ratio value",
                                                PercentageDouble.TryParse,
                                                out PercentageDouble sourceRatioValue,
                                                out ErrorResponse))
            {
                ErrorResponse ??= "The mandatory source ratio value is missing!";
                return false;
            }

            ElectricEnergySourceRatio = new ElectricEnergySourceRatio(
                                            energySource,
                                            sourceRatioValue,
                                            XML.OptionalText(DatexIINS.EnergyInfrastructure + "otherEnergySource"),
                                            XML.Element(DatexIINS.EnergyInfrastructure + "_electricEnergySourceRatioExtension")
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

            => new (XMLName ?? DatexIINS.EnergyInfrastructure + "electricEnergySourceRatio",

                   new XElement(DatexIINS.EnergyInfrastructure + "energySource",              EnergySource.ToString()),

                   OtherEnergySource is not null && OtherEnergySource.Length > 0
                       ? new XElement(DatexIINS.EnergyInfrastructure + "otherEnergySource",   OtherEnergySource)
                       : null,

                   // PercentageValue is a DataValue wrapper, not a bare number: the
                   // figure sits in a common percentage child.
                   new XElement(DatexIINS.EnergyInfrastructure + "sourceRatioValue",
                       new XElement(DatexIINS.Common + "percentage",   SourceRatioValue.Value)),

                   ElectricEnergySourceRatioExtension is not null
                       ? ElectricEnergySourceRatioExtension.ToExtensionXML(DatexIINS.EnergyInfrastructure + "_electricEnergySourceRatioExtension")
                       : null

               );

        #endregion

    }

}
