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

namespace cloud.charging.open.protocols.DatexII.v3.Facilities
{

    /// <summary>
    /// Extension methods for ImageFormats.
    /// </summary>
    public static class ImageFormatExtensions
    {

        /// <summary>
        /// Indicates whether this ImageFormat is null or empty.
        /// </summary>
        /// <param name="ImageFormat">An ImageFormat.</param>
        public static Boolean IsNullOrEmpty(this ImageFormat? ImageFormat)
            => !ImageFormat.HasValue || ImageFormat.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this ImageFormat is null or empty.
        /// </summary>
        /// <param name="ImageFormat">An ImageFormat.</param>
        public static Boolean IsNotNullOrEmpty(this ImageFormat? ImageFormat)
            => ImageFormat.HasValue && ImageFormat.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An ImageFormat.
    /// </summary>
    public readonly struct ImageFormat : IId,
                                              IEquatable<ImageFormat>,
                                              IComparable<ImageFormat>
    {

        #region Data

        private readonly static Dictionary<String, ImageFormat>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this ImageFormat is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this ImageFormat is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the ImageFormat.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered ImageFormats.
        /// </summary>
        public static    IEnumerable<ImageFormat>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ImageFormat based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an ImageFormat.</param>
        private ImageFormat(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ImageFormat Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ImageFormat(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an ImageFormat.
        /// </summary>
        /// <param name="Text">A text representation of an ImageFormat.</param>
        public static ImageFormat Parse(String Text)
        {

            if (TryParse(Text, out var imageFormat))
                return imageFormat;

            throw new ArgumentException($"Invalid text representation of an ImageFormat: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an ImageFormat.
        /// </summary>
        /// <param name="Text">A text representation of an ImageFormat.</param>
        public static ImageFormat? TryParse(String Text)
        {

            if (TryParse(Text, out var imageFormat))
                return imageFormat;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ImageFormat)

        /// <summary>
        /// Try to parse the given text as an ImageFormat.
        /// </summary>
        /// <param name="Text">A text representation of an ImageFormat.</param>
        /// <param name="ImageFormat">The parsed ImageFormat.</param>
        public static Boolean TryParse(String Text, out ImageFormat ImageFormat)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ImageFormat))
                    ImageFormat = Register(Text);

                return true;

            }

            ImageFormat = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this ImageFormat.
        /// </summary>
        public ImageFormat Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// BMP (Bitmap)
        /// </summary>
        public static ImageFormat  BMP     { get; }
            = Register("bmp");

        /// <summary>
        /// GIF (Graphics Interchange Format)
        /// </summary>
        public static ImageFormat  GIF     { get; }
            = Register("gif");

        /// <summary>
        /// JPEG (Joint Photographic Experts Group)
        /// </summary>
        public static ImageFormat  JPEG    { get; }
            = Register("jpeg");

        /// <summary>
        /// PNG (Portable Network Graphics)
        /// </summary>
        public static ImageFormat  PNG     { get; }
            = Register("png");

        /// <summary>
        /// TIFF (Tagged Image File Format)
        /// </summary>
        public static ImageFormat  TIFF    { get; }
            = Register("tiff");

        /// <summary>
        /// WebP (Web Picture format)
        /// </summary>
        public static ImageFormat  WEBP    { get; }
            = Register("webp");

        /// <summary>
        /// HEIF (High Efficiency Image Format)
        /// </summary>
        public static ImageFormat  HEIF    { get; }
            = Register("heif");

        /// <summary>
        /// HEIC (High Efficiency Image Coding)
        /// </summary>
        public static ImageFormat  HEIC    { get; }
            = Register("heic");

        /// <summary>
        /// ICO (Icon format)
        /// </summary>
        public static ImageFormat  ICO     { get; }
            = Register("ico");

        /// <summary>
        /// AVIF (AV1 Image File Format)
        /// </summary>
        public static ImageFormat  AVIF    { get; }
            = Register("avif");

        /// <summary>
        /// JP2 (JPEG 2000)
        /// </summary>
        public static ImageFormat  JP2     { get; }
            = Register("jp2");

        /// <summary>
        /// PNM (Portable Any Map, includes PBM, PGM, and PPM)
        /// </summary>
        public static ImageFormat  PNM     { get; }
            = Register("pnm");

        /// <summary>
        /// SVG (Scalable Vector Graphics)
        /// </summary>
        public static ImageFormat  SVG     { get; }
            = Register("svg");

        #endregion


        #region Operator overloading

        #region Operator == (ImageFormat1, ImageFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ImageFormat1">An ImageFormat.</param>
        /// <param name="ImageFormat2">Another ImageFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ImageFormat ImageFormat1,
                                           ImageFormat ImageFormat2)

            => ImageFormat1.Equals(ImageFormat2);

        #endregion

        #region Operator != (ImageFormat1, ImageFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ImageFormat1">An ImageFormat.</param>
        /// <param name="ImageFormat2">Another ImageFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ImageFormat ImageFormat1,
                                           ImageFormat ImageFormat2)

            => !ImageFormat1.Equals(ImageFormat2);

        #endregion

        #region Operator <  (ImageFormat1, ImageFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ImageFormat1">An ImageFormat.</param>
        /// <param name="ImageFormat2">Another ImageFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ImageFormat ImageFormat1,
                                          ImageFormat ImageFormat2)

            => ImageFormat1.CompareTo(ImageFormat2) < 0;

        #endregion

        #region Operator <= (ImageFormat1, ImageFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ImageFormat1">An ImageFormat.</param>
        /// <param name="ImageFormat2">Another ImageFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ImageFormat ImageFormat1,
                                           ImageFormat ImageFormat2)

            => ImageFormat1.CompareTo(ImageFormat2) <= 0;

        #endregion

        #region Operator >  (ImageFormat1, ImageFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ImageFormat1">An ImageFormat.</param>
        /// <param name="ImageFormat2">Another ImageFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ImageFormat ImageFormat1,
                                          ImageFormat ImageFormat2)

            => ImageFormat1.CompareTo(ImageFormat2) > 0;

        #endregion

        #region Operator >= (ImageFormat1, ImageFormat2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ImageFormat1">An ImageFormat.</param>
        /// <param name="ImageFormat2">Another ImageFormat.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ImageFormat ImageFormat1,
                                           ImageFormat ImageFormat2)

            => ImageFormat1.CompareTo(ImageFormat2) >= 0;

        #endregion

        #endregion

        #region IComparable<ImageFormat> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two ImageFormats.
        /// </summary>
        /// <param name="Object">An ImageFormat to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ImageFormat imageFormat
                   ? CompareTo(imageFormat)
                   : throw new ArgumentException("The given object is not an ImageFormat!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ImageFormat)

        /// <summary>
        /// Compares two ImageFormats.
        /// </summary>
        /// <param name="ImageFormat">An ImageFormat to compare with.</param>
        public Int32 CompareTo(ImageFormat ImageFormat)

            => String.Compare(InternalId,
                              ImageFormat.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<ImageFormat> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two ImageFormats for equality.
        /// </summary>
        /// <param name="Object">An ImageFormat to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ImageFormat imageFormat &&
                   Equals(imageFormat);

        #endregion

        #region Equals(ImageFormat)

        /// <summary>
        /// Compares two ImageFormats for equality.
        /// </summary>
        /// <param name="ImageFormat">An ImageFormat to compare with.</param>
        public Boolean Equals(ImageFormat ImageFormat)

            => String.Equals(InternalId,
                             ImageFormat.InternalId,
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
