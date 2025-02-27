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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure
{

    /// <summary>
    /// Extension methods for ConnectorType.
    /// </summary>
    public static class ConnectorTypeExtensions
    {

        /// <summary>
        /// Indicates whether this ConnectorType is null or empty.
        /// </summary>
        /// <param name="ConnectorType">A ConnectorType.</param>
        public static Boolean IsNullOrEmpty(this ConnectorType? ConnectorType)
            => !ConnectorType.HasValue || ConnectorType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this ConnectorType is null or empty.
        /// </summary>
        /// <param name="ConnectorType">A ConnectorType.</param>
        public static Boolean IsNotNullOrEmpty(this ConnectorType? ConnectorType)
            => ConnectorType.HasValue && ConnectorType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A ConnectorType.
    /// </summary>
    public readonly struct ConnectorType : IId,
                                           IEquatable<ConnectorType>,
                                           IComparable<ConnectorType>
    {

        #region Data

        private readonly static Dictionary<String, ConnectorType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this ConnectorType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this ConnectorType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the ConnectorType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered ConnectorTypes.
        /// </summary>
        public static    IEnumerable<ConnectorType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ConnectorType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a ConnectorType.</param>
        private ConnectorType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ConnectorType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ConnectorType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a ConnectorType.
        /// </summary>
        /// <param name="Text">A text representation of a ConnectorType.</param>
        public static ConnectorType Parse(String Text)
        {

            if (TryParse(Text, out var connectorType))
                return connectorType;

            throw new ArgumentException($"Invalid text representation of a ConnectorType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a ConnectorType.
        /// </summary>
        /// <param name="Text">A text representation of a ConnectorType.</param>
        public static ConnectorType? TryParse(String Text)
        {

            if (TryParse(Text, out var connectorType))
                return connectorType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ConnectorType)

        /// <summary>
        /// Try to parse the given text as a ConnectorType.
        /// </summary>
        /// <param name="Text">A text representation of a ConnectorType.</param>
        /// <param name="ConnectorType">The parsed ConnectorType.</param>
        public static Boolean TryParse(String Text, out ConnectorType ConnectorType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ConnectorType))
                    ConnectorType = Register(Text);

                return true;

            }

            ConnectorType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this ConnectorType.
        /// </summary>
        public ConnectorType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// CHAdeMO, 600 V DC. Used mostly in Japan.
        /// </summary>
        public static ConnectorType  Chademo                  { get; }
            = Register("chademo");

        /// <summary>
        /// CEE3, 230 V, 16 A.
        /// </summary>
        public static ConnectorType  CEE3                     { get; }
            = Register("cee3");

        /// <summary>
        /// CEE5, 400 V, 16-63 A.
        /// </summary>
        public static ConnectorType  CEE                      { get; }
            = Register("cee5");

        /// <summary>
        /// Yazaki, 400 VDC, 125 A, Asian standard.
        /// </summary>
        public static ConnectorType  Yazaki                   { get; }
            = Register("yazaki");

        /// <summary>
        /// A domestic socket of unspecified type. Applicable countries should be specified in separate attribute.
        /// </summary>
        public static ConnectorType  Domestic                 { get; }
            = Register("domestic");

        /// <summary>
        /// Domestic socket type A (mainly used in the USA, Canada, Mexico &amp; Japan).
        /// </summary>
        public static ConnectorType  DomesticA                { get; }
            = Register("domesticA");

        /// <summary>
        /// Domestic socket type B (mainly used in the USA, Canada &amp; Mexico).
        /// </summary>
        public static ConnectorType  DomesticB                { get; }
            = Register("domesticB");

        /// <summary>
        /// Domestic socket type C (commonly used in Europe, South America &amp; Asia).
        /// </summary>
        public static ConnectorType  DomesticC                { get; }
            = Register("domesticC");

        /// <summary>
        /// Domestic socket type D (mainly used in India).
        /// </summary>
        public static ConnectorType  DomesticD                { get; }
            = Register("domesticD");

        /// <summary>
        /// Domestic socket type E (primarily used in France, Belgium, Poland, Slovakia &amp; Czechia).
        /// </summary>
        public static ConnectorType  DomesticE                { get; }
            = Register("domesticE");

        /// <summary>
        /// Domestic socket type F (used almost everywhere in Europe &amp; Russia, except for the UK &amp; Ireland).
        /// </summary>
        public static ConnectorType  DomesticF                { get; }
            = Register("domesticF");

        /// <summary>
        /// Domestic socket type G (mainly used in the United Kingdom, Ireland, Malta, Malaysia, Singapore &amp; the Arabian Peninsula).
        /// </summary>
        public static ConnectorType  DomesticG                { get; }
            = Register("domesticG");

        /// <summary>
        /// Domestic socket type H (used exclusively in Israel, the West Bank &amp; the Gaza Strip).
        /// </summary>
        public static ConnectorType  DomesticH                { get; }
            = Register("domesticH");

        /// <summary>
        /// Domestic socket type I (mainly used in Australia, New Zealand, China &amp; Argentina).
        /// </summary>
        public static ConnectorType  DomesticI                { get; }
            = Register("domesticI");

        /// <summary>
        /// Domestic socket type J (used almost exclusively in Switzerland &amp; Liechtenstein).
        /// </summary>
        public static ConnectorType  DomesticJ                { get; }
            = Register("domesticJ");

        /// <summary>
        /// Domestic socket type K (used almost exclusively in Denmark &amp; Greenland).
        /// </summary>
        public static ConnectorType  DomesticK                { get; }
            = Register("domesticK");

        /// <summary>
        /// Domestic socket type L (used almost exclusively in Italy &amp; Chile).
        /// </summary>
        public static ConnectorType  DomesticL                { get; }
            = Register("domesticL");

        /// <summary>
        /// Domestic socket type M (mainly used in South Africa).
        /// </summary>
        public static ConnectorType  DomesticM                { get; }
            = Register("domesticM");

        /// <summary>
        /// Domestic socket type N (used in Brazil and South Africa).
        /// </summary>
        public static ConnectorType  DomesticN                { get; }
            = Register("domesticN");

        /// <summary>
        /// Domestic socket type O (used exclusively in Thailand).
        /// </summary>
        public static ConnectorType  DomesticO                { get; }
            = Register("domesticO");

        /// <summary>
        /// IEC 60309-2 Industrial Connector single phase 16 amperes (usually blue).
        /// </summary>
        public static ConnectorType  IEC60309x2Single16       { get; }
            = Register("iec60309x2single16");

        /// <summary>
        /// IEC 60309-2 Industrial Connector three phase 16 amperes (usually red).
        /// </summary>
        public static ConnectorType  IEC60309x2Three16        { get; }
            = Register("iec60309x2three16");

        /// <summary>
        /// IEC 60309-2 Industrial Connector three phase 32 amperes (usually red).
        /// </summary>
        public static ConnectorType  IEC60309x2Three32        { get; }
            = Register("iec60309x2three32");

        /// <summary>
        /// IEC 60309-2 Industrial Connector three phase 64 amperes (usually red).
        /// </summary>
        public static ConnectorType  IEC60309x2Three64        { get; }
            = Register("iec60309x2three64");

        /// <summary>
        /// IEC 62196 Type 1 "SAE J1772". Mostly used in USA and Asia.
        /// </summary>
        public static ConnectorType  IEC62196T1               { get; }
            = Register("iec62196T1");

        /// <summary>
        /// Combo Type 1 based, DC.
        /// </summary>
        public static ConnectorType  IEC62196T1COMBO          { get; }
            = Register("iec62196T1COMBO");

        /// <summary>
        /// IEC 62196 Type 2 "Mennekes" - 400 V, 16-63 A. Mandatory in Europe; EN 17186 identifier "C".
        /// </summary>
        public static ConnectorType  IEC62196T2               { get; }
            = Register("iec62196T2");

        /// <summary>
        /// Combo Type 2 based, DC, Type 2 connector with extension for simultaneous DC-charging.
        /// </summary>
        public static ConnectorType  IEC62196T2COMBO          { get; }
            = Register("iec62196T2COMBO");

        /// <summary>
        /// IEC 62196 Type 3A; EN 17186 identifier "D".
        /// </summary>
        public static ConnectorType  IEC62196T3A              { get; }
            = Register("iec62196T3A");

        /// <summary>
        /// IEC 62196 Type 3C "Scame"; EN 17186 identifier "E".
        /// </summary>
        public static ConnectorType  IEC62196T3C              { get; }
            = Register("iec62196T3C");

        /// <summary>
        /// MCS.
        /// </summary>
        public static ConnectorType  MCS                      { get; }
            = Register("mcs");

        /// <summary>
        /// On-board Bottom-up-Pantograph typically for bus charging.
        /// </summary>
        public static ConnectorType  PantographBottomUp       { get; }
            = Register("pantographBottomUp");

        /// <summary>
        /// Off-board Top-down-Pantograph typically for bus charging.
        /// </summary>
        public static ConnectorType  PantographTopDown        { get; }
            = Register("pantographTopDown");

        /// <summary>
        /// Tesla Connector EU (modification of the Type 2 connector).
        /// </summary>
        public static ConnectorType  TeslaConnectorEurope     { get; }
            = Register("teslaConnectorEurope");

        /// <summary>
        /// Tesla connector used in America (Tesla specific).
        /// </summary>
        public static ConnectorType  TeslaConnectorAmerica    { get; }
            = Register("teslaConnectorAmerica");

        /// <summary>
        /// Tesla Connector "Roadster"-type (round, 4 pin).
        /// </summary>
        public static ConnectorType  TeslaR                   { get; }
            = Register("teslaR");

        /// <summary>
        /// Tesla Connector "Model-S"-type (oval, 5 pin).
        /// </summary>
        public static ConnectorType  TeslaS                   { get; }
            = Register("teslaS");

        /// <summary>
        /// Other charging interface.
        /// </summary>
        public static ConnectorType  Other                    { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (ConnectorType1, ConnectorType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorType1">A ConnectorType.</param>
        /// <param name="ConnectorType2">Another ConnectorType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ConnectorType ConnectorType1,
                                           ConnectorType ConnectorType2)

            => ConnectorType1.Equals(ConnectorType2);

        #endregion

        #region Operator != (ConnectorType1, ConnectorType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorType1">A ConnectorType.</param>
        /// <param name="ConnectorType2">Another ConnectorType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ConnectorType ConnectorType1,
                                           ConnectorType ConnectorType2)

            => !ConnectorType1.Equals(ConnectorType2);

        #endregion

        #region Operator <  (ConnectorType1, ConnectorType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorType1">A ConnectorType.</param>
        /// <param name="ConnectorType2">Another ConnectorType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ConnectorType ConnectorType1,
                                          ConnectorType ConnectorType2)

            => ConnectorType1.CompareTo(ConnectorType2) < 0;

        #endregion

        #region Operator <= (ConnectorType1, ConnectorType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorType1">A ConnectorType.</param>
        /// <param name="ConnectorType2">Another ConnectorType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ConnectorType ConnectorType1,
                                           ConnectorType ConnectorType2)

            => ConnectorType1.CompareTo(ConnectorType2) <= 0;

        #endregion

        #region Operator >  (ConnectorType1, ConnectorType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorType1">A ConnectorType.</param>
        /// <param name="ConnectorType2">Another ConnectorType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ConnectorType ConnectorType1,
                                          ConnectorType ConnectorType2)

            => ConnectorType1.CompareTo(ConnectorType2) > 0;

        #endregion

        #region Operator >= (ConnectorType1, ConnectorType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorType1">A ConnectorType.</param>
        /// <param name="ConnectorType2">Another ConnectorType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ConnectorType ConnectorType1,
                                           ConnectorType ConnectorType2)

            => ConnectorType1.CompareTo(ConnectorType2) >= 0;

        #endregion

        #endregion

        #region IComparable<ConnectorType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two ConnectorTypes.
        /// </summary>
        /// <param name="Object">A ConnectorType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ConnectorType connectorType
                   ? CompareTo(connectorType)
                   : throw new ArgumentException("The given object is not a ConnectorType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ConnectorType)

        /// <summary>
        /// Compares two ConnectorTypes.
        /// </summary>
        /// <param name="ConnectorType">A ConnectorType to compare with.</param>
        public Int32 CompareTo(ConnectorType ConnectorType)

            => String.Compare(InternalId,
                              ConnectorType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<ConnectorType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two ConnectorTypes for equality.
        /// </summary>
        /// <param name="Object">A ConnectorType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ConnectorType connectorType &&
                   Equals(connectorType);

        #endregion

        #region Equals(ConnectorType)

        /// <summary>
        /// Compares two ConnectorTypes for equality.
        /// </summary>
        /// <param name="ConnectorType">A ConnectorType to compare with.</param>
        public Boolean Equals(ConnectorType ConnectorType)

            => String.Equals(InternalId,
                             ConnectorType.InternalId,
                             StringComparison.Ordinal);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()

            => InternalId?.ToLower().GetHashCode() ?? 0;

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => InternalId ?? "";

        #endregion

    }

}
