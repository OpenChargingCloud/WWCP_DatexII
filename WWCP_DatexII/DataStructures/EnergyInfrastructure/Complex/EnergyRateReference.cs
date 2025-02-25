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

using cloud.charging.open.protocols.DatexII.v3.Common;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// A reference to an EnergyRate.
    /// </summary>
    [XmlType("_EnergyRateReference", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyRateReference(String Id) : Reference(Id)
    {

        #region Properties

        /// <summary>
        /// Fixed attribute indicating the target class.
        /// </summary>
        [XmlAttribute("targetClass")]
        public String  TargetClass    { get; } = "egi:EnergyRate";

        #endregion


        #region TryParseXML(XML, out EnergyRateReference, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an EnergyRateReference.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyRateReference">The parsed EnergyRateReference.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                                               XML,
                                          [NotNullWhen(true)]  out EnergyRateReference?  EnergyRateReference,
                                          [NotNullWhen(false)] out String?                                       ErrorResponse)
        {

            EnergyRateReference = null;

            // <energyRateReference xmlns       = "http://datex2.eu/schema/3/energyInfrastructure"
            //                      id          = "74034E3E-9D2F-4410-BE6F-CAA3176D69B4"
            //                      targetClass = "egi:EnergyRate" />

            #region TryParse Id             [mandatory]

            if (!XML.TryParseMandatoryTextAttribute("id",
                                                    "id",
                                                    out var id,
                                                    out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse TargetClass    [mandatory]

            if (!XML.TryParseMandatoryTextAttribute("targetClass",
                                                    "target class",
                                                    out var targetClass,
                                                    out ErrorResponse))
            {
                return false;
            }

            var nsPrefix = XML.GetPrefixOfNamespace(DatexIINS.EnergyInfrastructure);

            if (targetClass != $"{nsPrefix}:EnergyRate")
            {
                ErrorResponse = $"Invalid target class '{targetClass}'!";
                return false;
            }

            #endregion


            EnergyRateReference = new EnergyRateReference(
                                      id
                                  );

            return true;

        }

        #endregion

        #region ToXML()

        public XElement ToXML()
        {

            // <energyRateReference xmlns       = "http://datex2.eu/schema/3/energyInfrastructure"
            //                      id          = "74034E3E-9D2F-4410-BE6F-CAA3176D69B4"
            //                      targetClass = "egi:EnergyRate" />

            var xml = new XElement(DatexIINS.EnergyInfrastructure + "tableReference",
                          new XAttribute("targetClass",   TargetClass),
                          new XAttribute("id",            Id)
                      );

            return xml;

        }

        #endregion


    }

}
