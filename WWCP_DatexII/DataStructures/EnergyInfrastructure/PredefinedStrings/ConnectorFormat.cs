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
    /// Extension methods for ConnectorFormats.
    /// </summary>
    public static class ConnectorFormatExtensions
    {

        /// <summary>
        /// Indicates whether this ConnectorFormat is null or empty.
        /// </summary>
        /// <param name="ConnectorFormat">A ConnectorFormat.</param>
        public static Boolean IsNullOrEmpty(this ConnectorFormat? ConnectorFormat)
            => !ConnectorFormat.HasValue || ConnectorFormat.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this ConnectorFormat is null or empty.
        /// </summary>
        /// <param name="ConnectorFormat">A ConnectorFormat.</param>
        public static Boolean IsNotNullOrEmpty(this ConnectorFormat? ConnectorFormat)
            => ConnectorFormat.HasValue && ConnectorFormat.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A ConnectorFormat.
    /// </summary>
    public readonly struct ConnectorFormat : IId,
                                             IEquatable<ConnectorFormat>,
                                             IComparable<ConnectorFormat>
    {

        #region Data

        private readonly static Dictionary<String, ConnectorFormat>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this ConnectorFormat is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this ConnectorFormat is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the ConnectorFormat.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered ConnectorFormats.
        /// </summary>
        public static    IEnumerable<ConnectorFormat>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ConnectorFormat based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a ConnectorFormat.</param>
        private ConnectorFormat(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ConnectorFormat Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ConnectorFormat(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a ConnectorFormat.
        /// </summary>
        /// <param name="Text">A text representation of a ConnectorFormat.</param>
        public static ConnectorFormat Parse(String Text)
        {

            if (TryParse(Text, out var connectorFormat))
                return connectorFormat;

            throw new ArgumentException($"Invalid text representation of a ConnectorFormat: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a ConnectorFormat.
        /// </summary>
        /// <param name="Text">A text representation of a ConnectorFormat.</param>
        public static ConnectorFormat? TryParse(String Text)
        {

            if (TryParse(Text, out var connectorFormat))
                return connectorFormat;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ConnectorFormat)

        /// <summary>
        /// Try to parse the given text as a ConnectorFormat.
        /// </summary>
        /// <param name="Text">A text representation of a ConnectorFormat.</param>
        /// <param name="ConnectorFormat">The parsed ConnectorFormat.</param>
        public static Boolean TryParse(String Text, out ConnectorFormat ConnectorFormat)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ConnectorFormat))
                    ConnectorFormat = Register(Text);

                return true;

            }

            ConnectorFormat = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this ConnectorFormat.
        /// </summary>
        public ConnectorFormat Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// The connector is an attached cable; the EV user's car needs to have a fitting inlet for a mode 2 cable, common for most domestic sockets.
        /// </summary>
        public static ConnectorFormat  CableMode2    { get; }
            = Register("cableMode2");

        /// <summary>
        /// The connector is an attached cable; the EV user's car needs to have a fitting inlet for a mode 3 cable, can be used for Type 1 and Type 2 sockets.
        /// </summary>
        public static ConnectorFormat  CableMode3    { get; }
            = Register("cableMode3");

        /// <summary>
        /// The connector is an attached cable; the EV user's car needs to have a fitting inlet.
        /// </summary>
        public static ConnectorFormat  OtherCable    { get; }
            = Register("otherCable");

        /// <summary>
        /// The connector is a socket; the EV user needs to bring a fitting plug.
        /// </summary>
        public static ConnectorFormat  Socket        { get; }
            = Register("socket");

        #endregion


        #region Operator overloading

        #region Operator == (ConnectorFormat1, ConnectorFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorFormat1">A ConnectorFormat.</param>
        /// <param name="ConnectorFormat2">Another ConnectorFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ConnectorFormat ConnectorFormat1,
                                           ConnectorFormat ConnectorFormat2)

            => ConnectorFormat1.Equals(ConnectorFormat2);

        #endregion

        #region Operator != (ConnectorFormat1, ConnectorFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorFormat1">A ConnectorFormat.</param>
        /// <param name="ConnectorFormat2">Another ConnectorFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ConnectorFormat ConnectorFormat1,
                                           ConnectorFormat ConnectorFormat2)

            => !ConnectorFormat1.Equals(ConnectorFormat2);

        #endregion

        #region Operator <  (ConnectorFormat1, ConnectorFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorFormat1">A ConnectorFormat.</param>
        /// <param name="ConnectorFormat2">Another ConnectorFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ConnectorFormat ConnectorFormat1,
                                          ConnectorFormat ConnectorFormat2)

            => ConnectorFormat1.CompareTo(ConnectorFormat2) < 0;

        #endregion

        #region Operator <= (ConnectorFormat1, ConnectorFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorFormat1">A ConnectorFormat.</param>
        /// <param name="ConnectorFormat2">Another ConnectorFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ConnectorFormat ConnectorFormat1,
                                           ConnectorFormat ConnectorFormat2)

            => ConnectorFormat1.CompareTo(ConnectorFormat2) <= 0;

        #endregion

        #region Operator >  (ConnectorFormat1, ConnectorFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorFormat1">A ConnectorFormat.</param>
        /// <param name="ConnectorFormat2">Another ConnectorFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ConnectorFormat ConnectorFormat1,
                                          ConnectorFormat ConnectorFormat2)

            => ConnectorFormat1.CompareTo(ConnectorFormat2) > 0;

        #endregion

        #region Operator >= (ConnectorFormat1, ConnectorFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ConnectorFormat1">A ConnectorFormat.</param>
        /// <param name="ConnectorFormat2">Another ConnectorFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ConnectorFormat ConnectorFormat1,
                                           ConnectorFormat ConnectorFormat2)

            => ConnectorFormat1.CompareTo(ConnectorFormat2) >= 0;

        #endregion

        #endregion

        #region IComparable<ConnectorFormat> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two ConnectorFormats.
        /// </summary>
        /// <param name="Object">A ConnectorFormat to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ConnectorFormat connectorFormat
                   ? CompareTo(connectorFormat)
                   : throw new ArgumentException("The given object is not a ConnectorFormat!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ConnectorFormat)

        /// <summary>
        /// Compares two ConnectorFormats.
        /// </summary>
        /// <param name="ConnectorFormat">A ConnectorFormat to compare with.</param>
        public Int32 CompareTo(ConnectorFormat ConnectorFormat)

            => String.Compare(InternalId,
                              ConnectorFormat.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<ConnectorFormat> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two ConnectorFormats for equality.
        /// </summary>
        /// <param name="Object">A ConnectorFormat to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ConnectorFormat connectorFormat &&
                   Equals(connectorFormat);

        #endregion

        #region Equals(ConnectorFormat)

        /// <summary>
        /// Compares two ConnectorFormats for equality.
        /// </summary>
        /// <param name="ConnectorFormat">A ConnectorFormat to compare with.</param>
        public Boolean Equals(ConnectorFormat ConnectorFormat)

            => String.Equals(InternalId,
                             ConnectorFormat.InternalId,
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
