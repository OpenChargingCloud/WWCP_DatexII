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

using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using System.Xml.Serialization;

using cloud.charging.open.protocols.DatexII.v3.Common;
using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Updates a rate defined in the static part of the model.
    /// </summary>
    [XmlType("EnergyRateUpdate", Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
    public class EnergyRateUpdate(DateTimeOffset             LastUpdated,
                                  EnergyRateReference        EnergyRateReference,
                                  MultilingualString?        AdditionalInformation       = null,
                                  IEnumerable<EnergyPrice>?  EnergyPrices                = null,
                                  XElement?                  EnergyRateUpdateExtension   = null)
    {

        #region Properties

        /// <summary>
        /// The date/time at which this information was last updated.
        /// </summary>
        [XmlElement("lastUpdated",                 Namespace = "http://datex2.eu/schema/3/common")]
        public DateTimeOffset            LastUpdated                  { get; } = LastUpdated;

        /// <summary>
        /// Specifies the EnergyRate that is updated here.
        /// </summary>
        [XmlElement("energyRateReference",         Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public EnergyRateReference       EnergyRateReference          { get; } = EnergyRateReference;

        /// <summary>
        /// Free text field for additional information regarding the energy rates.
        /// </summary>
        [XmlElement("additionalInformation",       Namespace = "http://datex2.eu/schema/3/common")]
        public MultilingualString?       AdditionalInformation        { get; } = AdditionalInformation;

        /// <summary>
        /// A collection of energy price definitions.
        /// </summary>
        [XmlElement("energyPrice",                 Namespace = "http://datex2.eu/schema/3/energyInfrastructure")]
        public IEnumerable<EnergyPrice>  EnergyPrices                 { get; } = EnergyPrices ?? [];

        /// <summary>
        /// Optional extension element for additional energy rate update information.
        /// </summary>
        [XmlElement("_energyRateUpdateExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                 EnergyRateUpdateExtension    { get; } = EnergyRateUpdateExtension;

        #endregion


        #region TryParseXML(XML, out EnergyRateUpdate, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of an EnergyRateUpdate.
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="EnergyRateUpdate">The parsed EnergyRateUpdate.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                    XML,
                                          [NotNullWhen(true)]  out EnergyRateUpdate?  EnergyRateUpdate,
                                          [NotNullWhen(false)] out String?            ErrorResponse)
        {

            EnergyRateUpdate  = null;
            ErrorResponse     = null;

            // <?xml version="1.0"?>
            // <energyRateUpdate xmlns="http://datex2.eu/schema/3/energyInfrastructure">
            //     <lastUpdated>2025-02-02T12:50:00+01:00</lastUpdated>
            //     <energyRateReference id="74034E3E-9D2F-4410-BE6F-CAA3176D69B4" targetClass="egi:EnergyRate"/>
            //     <energyPrice>
            //         <priceType>pricePerKWh</priceType>
            //         <value>0.37</value>
            //     </energyPrice>
            // </energyRateUpdate>

            #region TryParse LastUpdated              [mandatory]

            if (!XML.TryParseMandatoryTimestamp(DatexIINS.EnergyInfrastructure + "lastUpdated",
                                                "last updated",
                                                out DateTimeOffset lastUpdated,
                                                out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse EnergyRateReference      [mandatory]

            if (!XML.TryParseMandatory(DatexIINS.EnergyInfrastructure + "energyRateReference",
                                       "publication table references",
                                       EnergyRateReference.TryParseXML,
                                       out EnergyRateReference? energyRateReference,
                                       out ErrorResponse))
            {
                return false;
            }

            #endregion

            #region TryParse AdditionalInformation    [optional]

            if (XML.TryParseOptional(DatexIINS.Common + "additionalInformation",
                                     "overall period",
                                     MultilingualString.TryParseXML,
                                     out MultilingualString? additionalInformation,
                                     out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion

            #region TryParse EnergyPrices             [optional]

            if (XML.TryParseOptionalElements(DatexIINS.EnergyInfrastructure + "energyPrice",
                                             "energy prices",
                                             EnergyPrice.TryParseXML,
                                             out IEnumerable<EnergyPrice> energyPrices,
                                             out ErrorResponse))
            {
                if (ErrorResponse is not null)
                    return false;
            }

            #endregion


            EnergyRateUpdate = new EnergyRateUpdate(

                                   lastUpdated,
                                   energyRateReference,
                                   additionalInformation,
                                   energyPrices,

                                   XML.Element(DatexIINS.Common + "_energyRateUpdateExtension")

                               );

            return true;

        }

        #endregion


    }

}
