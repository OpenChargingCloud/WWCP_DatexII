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

namespace cloud.charging.open.protocols.DatexII.v3.LocationReferencing
{

    /// <summary>
    /// Extension methods for AreaPlaces.
    /// </summary>
    public static class AreaPlaceExtensions
    {

        /// <summary>
        /// Indicates whether this AreaPlace is null or empty.
        /// </summary>
        /// <param name="AreaPlace">An AreaPlace.</param>
        public static Boolean IsNullOrEmpty(this AreaPlace? AreaPlace)
            => !AreaPlace.HasValue || AreaPlace.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this AreaPlace is null or empty.
        /// </summary>
        /// <param name="AreaPlace">An AreaPlace.</param>
        public static Boolean IsNotNullOrEmpty(this AreaPlace? AreaPlace)
            => AreaPlace.HasValue && AreaPlace.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An AreaPlace.
    /// </summary>
    public readonly struct AreaPlace : IId,
                                       IEquatable<AreaPlace>,
                                       IComparable<AreaPlace>
    {

        #region Data

        private readonly static Dictionary<String, AreaPlace>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this AreaPlace is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this AreaPlace is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the AreaPlace.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<AreaPlace>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new AreaPlace based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an AreaPlace.</param>
        private AreaPlace(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static AreaPlace Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new AreaPlace(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an AreaPlace.
        /// </summary>
        /// <param name="Text">A text representation of an AreaPlace.</param>
        public static AreaPlace Parse(String Text)
        {

            if (TryParse(Text, out var areaPlace))
                return areaPlace;

            throw new ArgumentException($"Invalid text representation of an AreaPlace: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an AreaPlace.
        /// </summary>
        /// <param name="Text">A text representation of an AreaPlace.</param>
        public static AreaPlace? TryParse(String Text)
        {

            if (TryParse(Text, out var areaPlace))
                return areaPlace;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out AreaPlace)

        /// <summary>
        /// Try to parse the given text as AreaPlace.
        /// </summary>
        /// <param name="Text">A text representation of an AreaPlace.</param>
        /// <param name="AreaPlace">The parsed AreaPlace.</param>
        public static Boolean TryParse(String Text, out AreaPlace AreaPlace)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out AreaPlace))
                    AreaPlace = Register(Text);

                return true;

            }

            AreaPlace = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this AreaPlace.
        /// </summary>
        public AreaPlace Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// At national borders.
        /// </summary>
        public static AreaPlace  AtBorders                     { get; }
            = Register("atBorders");

        /// <summary>
        /// At high altitudes.
        /// </summary>
        public static AreaPlace  AtHighAltitudes               { get; }
            = Register("atHighAltitudes");

        /// <summary>
        /// In built up areas, i.e. villages, towns, and cities.
        /// </summary>
        public static AreaPlace  InBuiltUpAreas                { get; }
            = Register("inBuiltUpAreas");

        /// <summary>
        /// On sections of the road where it runs through or adjacent to forested areas.
        /// </summary>
        public static AreaPlace  InForestedAreas               { get; }
            = Register("inForestedAreas");

        /// <summary>
        /// In galleries.
        /// </summary>
        public static AreaPlace  InGalleries                   { get; }
            = Register("inGalleries");

        /// <summary>
        /// In low-lying areas.
        /// </summary>
        public static AreaPlace  InLowLyingAreas               { get; }
            = Register("inLowLyingAreas");

        /// <summary>
        /// In rural areas, i.e. outside villages, towns, and cities.
        /// </summary>
        public static AreaPlace  InRuralAreas                  { get; }
            = Register("inRuralAreas");

        /// <summary>
        /// In shaded areas.
        /// </summary>
        public static AreaPlace  InShadedAreas                 { get; }
            = Register("inShadedAreas");

        /// <summary>
        /// In the city centre areas.
        /// </summary>
        public static AreaPlace  InTheInnerCityAreas           { get; }
            = Register("inTheInnerCityAreas");

        /// <summary>
        /// In tunnels.
        /// </summary>
        public static AreaPlace  InTunnels                     { get; }
            = Register("inTunnels");

        /// <summary>
        /// On bridges.
        /// </summary>
        public static AreaPlace  OnBridges                     { get; }
            = Register("onBridges");

        /// <summary>
        /// On downhill sections of the road.
        /// </summary>
        public static AreaPlace  OnDownhillSections            { get; }
            = Register("onDownhillSections");

        /// <summary>
        /// On elevated sections of the road.
        /// </summary>
        public static AreaPlace  OnElevatedSections            { get; }
            = Register("onElevatedSections");

        /// <summary>
        /// On entering or leaving tunnels.
        /// </summary>
        public static AreaPlace  OnEnteringOrLeavingTunnels    { get; }
            = Register("onEnteringOrLeavingTunnels");

        /// <summary>
        /// On flyover sections of the road.
        /// </summary>
        public static AreaPlace  OnFlyovers                    { get; }
            = Register("onFlyovers");

        /// <summary>
        /// On mountain passes.
        /// </summary>
        public static AreaPlace  OnPasses                      { get; }
            = Register("onPasses");

        /// <summary>
        /// On underground sections of the road.
        /// </summary>
        public static AreaPlace  OnUndergroundSections         { get; }
            = Register("onUndergroundSections");

        /// <summary>
        /// On underpasses, i.e. sections of the road which pass under another road.
        /// </summary>
        public static AreaPlace  OnUnderpasses                 { get; }
            = Register("onUnderpasses");

        #endregion


        #region Operator overloading

        #region Operator == (AreaPlace1, AreaPlace2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AreaPlace1">An AreaPlace.</param>
        /// <param name="AreaPlace2">Another AreaPlace.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (AreaPlace AreaPlace1,
                                           AreaPlace AreaPlace2)

            => AreaPlace1.Equals(AreaPlace2);

        #endregion

        #region Operator != (AreaPlace1, AreaPlace2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AreaPlace1">An AreaPlace.</param>
        /// <param name="AreaPlace2">Another AreaPlace.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (AreaPlace AreaPlace1,
                                           AreaPlace AreaPlace2)

            => !AreaPlace1.Equals(AreaPlace2);

        #endregion

        #region Operator <  (AreaPlace1, AreaPlace2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AreaPlace1">An AreaPlace.</param>
        /// <param name="AreaPlace2">Another AreaPlace.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (AreaPlace AreaPlace1,
                                          AreaPlace AreaPlace2)

            => AreaPlace1.CompareTo(AreaPlace2) < 0;

        #endregion

        #region Operator <= (AreaPlace1, AreaPlace2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AreaPlace1">An AreaPlace.</param>
        /// <param name="AreaPlace2">Another AreaPlace.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (AreaPlace AreaPlace1,
                                           AreaPlace AreaPlace2)

            => AreaPlace1.CompareTo(AreaPlace2) <= 0;

        #endregion

        #region Operator >  (AreaPlace1, AreaPlace2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AreaPlace1">An AreaPlace.</param>
        /// <param name="AreaPlace2">Another AreaPlace.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (AreaPlace AreaPlace1,
                                          AreaPlace AreaPlace2)

            => AreaPlace1.CompareTo(AreaPlace2) > 0;

        #endregion

        #region Operator >= (AreaPlace1, AreaPlace2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AreaPlace1">An AreaPlace.</param>
        /// <param name="AreaPlace2">Another AreaPlace.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (AreaPlace AreaPlace1,
                                           AreaPlace AreaPlace2)

            => AreaPlace1.CompareTo(AreaPlace2) >= 0;

        #endregion

        #endregion

        #region IComparable<AreaPlace> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two AreaPlaces.
        /// </summary>
        /// <param name="Object">AreaPlace to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is AreaPlace AreaPlace
                   ? CompareTo(AreaPlace)
                   : throw new ArgumentException("The given object is not AreaPlace!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(AreaPlace)

        /// <summary>
        /// Compares two AreaPlaces.
        /// </summary>
        /// <param name="AreaPlace">AreaPlace to compare with.</param>
        public Int32 CompareTo(AreaPlace AreaPlace)

            => String.Compare(InternalId,
                              AreaPlace.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<AreaPlace> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two AreaPlaces for equality.
        /// </summary>
        /// <param name="Object">AreaPlace to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is AreaPlace AreaPlace &&
                   Equals(AreaPlace);

        #endregion

        #region Equals(AreaPlace)

        /// <summary>
        /// Compares two AreaPlaces for equality.
        /// </summary>
        /// <param name="AreaPlace">AreaPlace to compare with.</param>
        public Boolean Equals(AreaPlace AreaPlace)

            => String.Equals(InternalId,
                             AreaPlace.InternalId,
                             StringComparison.Ordinal);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
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
