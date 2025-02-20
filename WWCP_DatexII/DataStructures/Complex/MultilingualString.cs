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

#endregion

namespace cloud.charging.open.protocols.DatexII
{

    /// <summary>
    /// Provides a multilingual string using a set of language-specific values.
    /// </summary>
    [XmlType("MultilingualString", Namespace = "http://datex2.eu/schema/3/common")]
    public class MultilingualString
    {
        [XmlElement("values")]
        public MultilingualStringValues? Values { get; set; }
    }

    /// <summary>
    /// Container for multiple multilingual string values.
    /// </summary>
    [XmlType("MultilingualStringValues", Namespace = "http://datex2.eu/schema/3/common")]
    public class MultilingualStringValues
    {
        [XmlElement("value")]
        public List<MultilingualStringValue> ValueList { get; set; } = new List<MultilingualStringValue>();
    }

    /// <summary>
    /// Represents a multilingual string value with an optional language attribute.
    /// The content is based on a simple string type (MultilingualStringValueType).
    /// </summary>
    [XmlType("MultilingualStringValue", Namespace = "http://datex2.eu/schema/3/common")]
    public class MultilingualStringValue
    {

        [XmlText]
        public String?  Value    { get; set; }


        [XmlAttribute("lang")]
        public String?  Lang     { get; set; }

    }

}
