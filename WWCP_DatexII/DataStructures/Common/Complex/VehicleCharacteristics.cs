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

using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// The characteristics of a vehicle, e.g. lorry of gross weight greater than 30 tonnes.
    /// </summary>
    [XmlType("VehicleCharacteristics", Namespace = "http://datex2.eu/schema/3/common")]
    public class VehicleCharacteristics(IEnumerable<FuelType>?                          FuelType                           = null,
                                        LoadType?                                       LoadType                           = null,
                                        VehicleEquipment?                               VehicleEquipment                   = null,
                                        IEnumerable<VehicleType>?                       VehicleType                        = null,
                                        IEnumerable<EUVehicleCategory>?                 EUVehicleCategory                  = null,
                                        VehicleUsage?                                   VehicleUsage                       = null,
                                        UInt16?                                         YearOfFirstRegistration            = null,
                                        IEnumerable<GrossWeightCharacteristic>?         GrossWeightCharacteristic          = null,
                                        IEnumerable<HeightCharacteristic>?              HeightCharacteristic               = null,
                                        IEnumerable<LengthCharacteristic>?              LengthCharacteristic               = null,
                                        IEnumerable<WidthCharacteristic>?               WidthCharacteristic                = null,
                                        IEnumerable<HeaviestAxleWeightCharacteristic>?  HeaviestAxleWeightCharacteristic   = null,
                                        IEnumerable<NumberOfAxlesCharacteristic>?       NumberOfAxlesCharacteristic        = null,
                                        Emissions?                                      Emissions                          = null,

                                        XElement?                                       VehicleCharacteristicsExtension    = null)
    {

        #region Properties

        /// <summary>
        /// The type of fuel used by the vehicle.
        /// </summary>
        [XmlElement("fuelType",                          Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<FuelType>                          FuelType                            { get; } = FuelType?.                        Distinct() ?? [];

        /// <summary>
        /// The type of load carried by the vehicle, especially in respect of hazardous loads.
        /// </summary>
        [XmlElement("loadType",                          Namespace = "http://datex2.eu/schema/3/common")]
        public LoadType?                                      LoadType                            { get; } = LoadType;

        /// <summary>
        /// The type of equipment in use or on board the vehicle.
        /// </summary>
        [XmlElement("vehicleEquipment",                  Namespace = "http://datex2.eu/schema/3/common")]
        public VehicleEquipment?                              VehicleEquipment                    { get; } = VehicleEquipment;

        /// <summary>
        /// Vehicle type.
        /// </summary>
        [XmlElement("vehicleType",                       Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<VehicleType>                       VehicleType                         { get; } = VehicleType?.                     Distinct() ?? [];

        /// <summary>
        /// Vehicle categories as defined in EU Directive 2007/46/EG and Regulation (EU) No 168/2013.
        /// </summary>
        [XmlElement("euVehicleCategory",                 Namespace = "http://datex2.eu/schema/3/afirEnergyInfrastructure")]
        public IEnumerable<EUVehicleCategory>                 EUVehicleCategory                   { get; } = EUVehicleCategory?.               Distinct() ?? [];

        /// <summary>
        /// The type of usage of the vehicle (i.e. for what purpose is the vehicle being used).
        /// </summary>
        [XmlElement("vehicleUsage",                      Namespace = "http://datex2.eu/schema/3/common")]
        public VehicleUsage?                                  VehicleUsage                        { get; } = VehicleUsage;

        /// <summary>
        /// Year of first registration of the vehicle.
        /// </summary>
        [XmlElement("yearOfFirstRegistration",           Namespace = "http://datex2.eu/schema/3/common")]
        public UInt16?                                        YearOfFirstRegistration             { get; } = YearOfFirstRegistration;

        /// <summary>
        /// Characteristic representing the gross weight of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("grossWeightCharacteristic",         Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<GrossWeightCharacteristic>         GrossWeightCharacteristic           { get; } = GrossWeightCharacteristic?.       Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the height of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("heightCharacteristic",              Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<HeightCharacteristic>              HeightCharacteristic                { get; } = HeightCharacteristic?.            Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the length of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("lengthCharacteristic",              Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<LengthCharacteristic>              LengthCharacteristic                { get; } = LengthCharacteristic?.            Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the width of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("widthCharacteristic",               Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<WidthCharacteristic>               WidthCharacteristic                 { get; } = WidthCharacteristic?.             Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the heaviest axle weight of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("heaviestAxleWeightCharacteristic",  Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<HeaviestAxleWeightCharacteristic>  HeaviestAxleWeightCharacteristic    { get; } = HeaviestAxleWeightCharacteristic?.Distinct() ?? [];

        /// <summary>
        /// Characteristic representing the number of axles of the vehicle.
        /// May occur up to two times.
        /// </summary>
        [XmlElement("numberOfAxlesCharacteristic",       Namespace = "http://datex2.eu/schema/3/common")]
        public IEnumerable<NumberOfAxlesCharacteristic>       NumberOfAxlesCharacteristic         { get; } = NumberOfAxlesCharacteristic?.     Distinct() ?? [];

        /// <summary>
        /// Emissions information for the vehicle.
        /// </summary>
        [XmlElement("emissions",                         Namespace = "http://datex2.eu/schema/3/common")]
        public Emissions?                                     Emissions                           { get; } = Emissions;

        /// <summary>
        /// Optional extension element for additional vehicle characteristics.
        /// </summary>
        [XmlElement("_vehicleCharacteristicsExtension",  Namespace = "http://datex2.eu/schema/3/common")]
        public XElement?                                      VehicleCharacteristicsExtension     { get; } = VehicleCharacteristicsExtension;

        #endregion


        #region TryParseXML(XML, out VehicleCharacteristics, out ErrorResponse)

        /// <summary>
        /// Try to parse the given XML representation of vehicle characteristics, with the EU vehicle categories of its
        /// regulated characteristics (DATEX II 3.7).
        /// </summary>
        /// <param name="XML">The XML to be parsed.</param>
        /// <param name="VehicleCharacteristics">The parsed vehicle characteristics.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParseXML(XElement                                          XML,
                                          [NotNullWhen(true)]  out VehicleCharacteristics?  VehicleCharacteristics,
                                          [NotNullWhen(false)] out String?                  ErrorResponse)
        {

            VehicleCharacteristics = null;

            if (!XML.TryParseOptionalValues(DatexIINS.Common + "fuelType",                          "fuel type",                     Common.FuelType.TryParse,                    out IEnumerable<FuelType>                          fuelTypes,          out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.Common + "loadType",                          "load type",                     Common.LoadType.TryParse,                    out LoadType?                                      loadType,           out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.Common + "vehicleEquipment",                  "vehicle equipment",             Common.VehicleEquipment.TryParse,            out VehicleEquipment?                              vehicleEquipment,   out ErrorResponse) ||
                !XML.TryParseOptionalValues(DatexIINS.Common + "vehicleType",                       "vehicle type",                  Common.VehicleType.TryParse,                 out IEnumerable<VehicleType>                       vehicleTypes,       out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.Common + "vehicleUsage",                      "vehicle usage",                 Common.VehicleUsage.TryParse,                out VehicleUsage?                                  vehicleUsage,       out ErrorResponse) ||
                !XML.TryParseOptionalValue (DatexIINS.Common + "yearOfFirstRegistration",           "year of first registration",    DatexIIXMLExtensions.TryParseUInt16,         out UInt16?                                        year,               out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.Common + "grossWeightCharacteristic",         "gross weight characteristic",   Common.GrossWeightCharacteristic.TryParseXML,        out IEnumerable<GrossWeightCharacteristic>        grossWeight,        out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.Common + "heightCharacteristic",              "height characteristic",         Common.HeightCharacteristic.TryParseXML,             out IEnumerable<HeightCharacteristic>             height,             out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.Common + "lengthCharacteristic",              "length characteristic",         Common.LengthCharacteristic.TryParseXML,             out IEnumerable<LengthCharacteristic>             length,             out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.Common + "widthCharacteristic",               "width characteristic",          Common.WidthCharacteristic.TryParseXML,              out IEnumerable<WidthCharacteristic>              width,              out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.Common + "heaviestAxleWeightCharacteristic",  "heaviest axle weight",          Common.HeaviestAxleWeightCharacteristic.TryParseXML, out IEnumerable<HeaviestAxleWeightCharacteristic> heaviestAxleWeight, out ErrorResponse) ||
                !XML.TryParseOptionalXMLs  (DatexIINS.Common + "numberOfAxlesCharacteristic",       "number of axles",               Common.NumberOfAxlesCharacteristic.TryParseXML,      out IEnumerable<NumberOfAxlesCharacteristic>      numberOfAxles,      out ErrorResponse))
            {
                return false;
            }

            #region The EU vehicle categories of the typed extension

            var euVehicleCategories  = new List<EUVehicleCategory>();
            XElement? extension      = null;

            if (XML.Element(DatexIINS.Common + "_vehicleCharacteristicsExtension") is XElement extensionXML)
            {

                foreach (var category in extensionXML.Elements(DatexIINS.Common + "vehicleCharacteristicsExtended").
                                                      Elements(DatexIINS.CommonExtension + "regulatedCharacteristics").
                                                      Elements(DatexIINS.CommonExtension + "euVehicleCategory"))
                {

                    if (!EnergyInfrastructure.EUVehicleCategory.TryParse(category.Value, out var euVehicleCategory))
                    {
                        ErrorResponse = $"Invalid EU vehicle category '{category.Value}'!";
                        return false;
                    }

                    euVehicleCategories.Add(euVehicleCategory);

                }

                var others = extensionXML.Elements().Where(element => element.Name != DatexIINS.Common + "vehicleCharacteristicsExtended").ToArray();

                if (others.Length > 0)
                    extension = others.Length == 1 ? others[0] : new XElement(DatexIINS.Common + "_vehicleCharacteristicsExtension", others);

            }

            #endregion

            VehicleCharacteristics = new VehicleCharacteristics(
                                         fuelTypes,
                                         loadType,
                                         vehicleEquipment,
                                         vehicleTypes,
                                         euVehicleCategories,
                                         vehicleUsage,
                                         year,
                                         grossWeight,
                                         height,
                                         length,
                                         width,
                                         heaviestAxleWeight,
                                         numberOfAxles,
                                         null,
                                         extension
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

            => new (XMLName ?? DatexIINS.Common + "vehicleCharacteristics",

                   FuelType.         Select(fuelType         => new XElement(DatexIINS.Common + "fuelType",          fuelType.        ToString())),

                   LoadType.HasValue
                       ? new XElement(DatexIINS.Common + "loadType",                       LoadType.        Value.ToString())
                       : null,

                   VehicleEquipment.HasValue
                       ? new XElement(DatexIINS.Common + "vehicleEquipment",               VehicleEquipment.Value.ToString())
                       : null,

                   VehicleType.      Select(vehicleType      => new XElement(DatexIINS.Common + "vehicleType",       vehicleType.     ToString())),

                   VehicleUsage.HasValue
                       ? new XElement(DatexIINS.Common + "vehicleUsage",                   VehicleUsage.Value.ToString())
                       : null,

                   YearOfFirstRegistration.HasValue
                       ? new XElement(DatexIINS.Common + "yearOfFirstRegistration",        YearOfFirstRegistration.Value)
                       : null,

                   GrossWeightCharacteristic.       Select(characteristic => characteristic.ToXML(DatexIINS.Common + "grossWeightCharacteristic")),
                   HeightCharacteristic.            Select(characteristic => characteristic.ToXML(DatexIINS.Common + "heightCharacteristic")),
                   LengthCharacteristic.            Select(characteristic => characteristic.ToXML(DatexIINS.Common + "lengthCharacteristic")),
                   WidthCharacteristic.             Select(characteristic => characteristic.ToXML(DatexIINS.Common + "widthCharacteristic")),
                   HeaviestAxleWeightCharacteristic.Select(characteristic => characteristic.ToXML(DatexIINS.Common + "heaviestAxleWeightCharacteristic")),
                   NumberOfAxlesCharacteristic.     Select(characteristic => characteristic.ToXML(DatexIINS.Common + "numberOfAxlesCharacteristic")),

                   Emissions is not null
                       ? Emissions.ToXML(DatexIINS.Common + "emissions")
                       : null,

                   // DATEX II 3.7: the EU vehicle categories are regulated characteristics of the typed extension.
                   EUVehicleCategory.Any() || VehicleCharacteristicsExtension is not null
                       ? new XElement(DatexIINS.Common + "_vehicleCharacteristicsExtension",
                             EUVehicleCategory.Any()
                                 ? new XElement(DatexIINS.Common + "vehicleCharacteristicsExtended",
                                       new XElement(DatexIINS.CommonExtension + "regulatedCharacteristics",
                                           EUVehicleCategory.Select(euVehicleCategory => new XElement(DatexIINS.CommonExtension + "euVehicleCategory", euVehicleCategory.ToString()))))
                                 : null,
                             VehicleCharacteristicsExtension)
                       : null

               );

        #endregion

    }

}
