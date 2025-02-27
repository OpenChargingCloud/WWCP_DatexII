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

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Extension methods for URLLinkTypes.
    /// </summary>
    public static class URLLinkTypeExtensions
    {

        /// <summary>
        /// Indicates whether this URLLinkType is null or empty.
        /// </summary>
        /// <param name="URLLinkType">An URLLinkType.</param>
        public static Boolean IsNullOrEmpty(this URLLinkType? URLLinkType)
            => !URLLinkType.HasValue || URLLinkType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this URLLinkType is null or empty.
        /// </summary>
        /// <param name="URLLinkType">An URLLinkType.</param>
        public static Boolean IsNotNullOrEmpty(this URLLinkType? URLLinkType)
            => URLLinkType.HasValue && URLLinkType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An URLLinkType.
    /// </summary>
    public readonly struct URLLinkType : IId,
                                         IEquatable<URLLinkType>,
                                         IComparable<URLLinkType>
    {

        #region Data

        private readonly static Dictionary<String, URLLinkType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                                          InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this URLLinkType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this URLLinkType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the URLLinkType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered URLLinkTypes.
        /// </summary>
        public static    IEnumerable<URLLinkType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new URLLinkType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an URLLinkType.</param>
        private URLLinkType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static URLLinkType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new URLLinkType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an URLLinkType.
        /// </summary>
        /// <param name="Text">A text representation of an URLLinkType.</param>
        public static URLLinkType Parse(String Text)
        {

            if (TryParse(Text, out var urlLinkType))
                return urlLinkType;

            throw new ArgumentException($"Invalid text representation of an URLLinkType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an URLLinkType.
        /// </summary>
        /// <param name="Text">A text representation of an URLLinkType.</param>
        public static URLLinkType? TryParse(String Text)
        {

            if (TryParse(Text, out var urlLinkType))
                return urlLinkType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out URLLinkType)

        /// <summary>
        /// Try to parse the given text as an URLLinkType.
        /// </summary>
        /// <param name="Text">A text representation of an URLLinkType.</param>
        /// <param name="URLLinkType">The parsed URLLinkType.</param>
        public static Boolean TryParse(String Text, out URLLinkType URLLinkType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out URLLinkType))
                    URLLinkType = Register(Text);

                return true;

            }

            URLLinkType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this URLLinkType.
        /// </summary>
        public URLLinkType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// URL link to a PDF document.
        /// </summary>
        public static URLLinkType  DocumentPdf    { get; }
            = Register("documentPdf");

        /// <summary>
        /// URL link to an HTML page.
        /// </summary>
        public static URLLinkType  HTML           { get; }
            = Register("html");

        /// <summary>
        /// URL link to an image.
        /// </summary>
        public static URLLinkType  Image          { get; }
            = Register("image");

        /// <summary>
        /// URL link to an RSS feed.
        /// </summary>
        public static URLLinkType  RSS            { get; }
            = Register("rss");

        /// <summary>
        /// URL link to a video stream.
        /// </summary>
        public static URLLinkType  VideoStream    { get; }
            = Register("videoStream");

        /// <summary>
        /// URL link to a voice stream.
        /// </summary>
        public static URLLinkType  VoiceStream    { get; }
            = Register("voiceStream");

        /// <summary>
        /// Other than as defined in this enumeration.
        /// </summary>
        public static URLLinkType  Other          { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (URLLinkType1, URLLinkType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="URLLinkType1">An URLLinkType.</param>
        /// <param name="URLLinkType2">Another URLLinkType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (URLLinkType URLLinkType1,
                                           URLLinkType URLLinkType2)

            => URLLinkType1.Equals(URLLinkType2);

        #endregion

        #region Operator != (URLLinkType1, URLLinkType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="URLLinkType1">An URLLinkType.</param>
        /// <param name="URLLinkType2">Another URLLinkType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (URLLinkType URLLinkType1,
                                           URLLinkType URLLinkType2)

            => !URLLinkType1.Equals(URLLinkType2);

        #endregion

        #region Operator <  (URLLinkType1, URLLinkType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="URLLinkType1">An URLLinkType.</param>
        /// <param name="URLLinkType2">Another URLLinkType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (URLLinkType URLLinkType1,
                                          URLLinkType URLLinkType2)

            => URLLinkType1.CompareTo(URLLinkType2) < 0;

        #endregion

        #region Operator <= (URLLinkType1, URLLinkType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="URLLinkType1">An URLLinkType.</param>
        /// <param name="URLLinkType2">Another URLLinkType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (URLLinkType URLLinkType1,
                                           URLLinkType URLLinkType2)

            => URLLinkType1.CompareTo(URLLinkType2) <= 0;

        #endregion

        #region Operator >  (URLLinkType1, URLLinkType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="URLLinkType1">An URLLinkType.</param>
        /// <param name="URLLinkType2">Another URLLinkType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (URLLinkType URLLinkType1,
                                          URLLinkType URLLinkType2)

            => URLLinkType1.CompareTo(URLLinkType2) > 0;

        #endregion

        #region Operator >= (URLLinkType1, URLLinkType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="URLLinkType1">An URLLinkType.</param>
        /// <param name="URLLinkType2">Another URLLinkType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (URLLinkType URLLinkType1,
                                           URLLinkType URLLinkType2)

            => URLLinkType1.CompareTo(URLLinkType2) >= 0;

        #endregion

        #endregion

        #region IComparable<URLLinkType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two URLLinkTypes.
        /// </summary>
        /// <param name="Object">An URLLinkType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is URLLinkType urlLinkType
                   ? CompareTo(urlLinkType)
                   : throw new ArgumentException("The given object is not an URLLinkType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(URLLinkType)

        /// <summary>
        /// Compares two URLLinkTypes.
        /// </summary>
        /// <param name="URLLinkType">An URLLinkType to compare with.</param>
        public Int32 CompareTo(URLLinkType URLLinkType)

            => String.Compare(InternalId,
                              URLLinkType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<URLLinkType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two URLLinkTypes for equality.
        /// </summary>
        /// <param name="Object">An URLLinkType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is URLLinkType urlLinkType &&
                   Equals(urlLinkType);

        #endregion

        #region Equals(URLLinkType)

        /// <summary>
        /// Compares two URLLinkTypes for equality.
        /// </summary>
        /// <param name="URLLinkType">An URLLinkType to compare with.</param>
        public Boolean Equals(URLLinkType URLLinkType)

            => String.Equals(InternalId,
                             URLLinkType.InternalId,
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
