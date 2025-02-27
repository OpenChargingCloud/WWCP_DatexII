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
    /// Extension methods for PublicEventTypes.
    /// </summary>
    public static class PublicEventTypeExtensions
    {

        /// <summary>
        /// Indicates whether this PublicEventType is null or empty.
        /// </summary>
        /// <param name="PublicEventType">A PublicEventType.</param>
        public static Boolean IsNullOrEmpty(this PublicEventType? PublicEventType)
            => !PublicEventType.HasValue || PublicEventType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this PublicEventType is null or empty.
        /// </summary>
        /// <param name="PublicEventType">A PublicEventType.</param>
        public static Boolean IsNotNullOrEmpty(this PublicEventType? PublicEventType)
            => PublicEventType.HasValue && PublicEventType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A PublicEventType.
    /// </summary>
    public readonly struct PublicEventType : IId,
                                             IEquatable<PublicEventType>,
                                             IComparable<PublicEventType>
    {

        #region Data

        private readonly static Dictionary<String, PublicEventType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                               InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this PublicEventType is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this PublicEventType is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the PublicEventType.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered confidentialities.
        /// </summary>
        public static    IEnumerable<PublicEventType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new PublicEventType based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a PublicEventType.</param>
        private PublicEventType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static PublicEventType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new PublicEventType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a PublicEventType.
        /// </summary>
        /// <param name="Text">A text representation of a PublicEventType.</param>
        public static PublicEventType Parse(String Text)
        {

            if (TryParse(Text, out var publicEventType))
                return publicEventType;

            throw new ArgumentException($"Invalid text representation of a PublicEventType: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a PublicEventType.
        /// </summary>
        /// <param name="Text">A text representation of a PublicEventType.</param>
        public static PublicEventType? TryParse(String Text)
        {

            if (TryParse(Text, out var publicEventType))
                return publicEventType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out PublicEventType)

        /// <summary>
        /// Try to parse the given text as PublicEventType.
        /// </summary>
        /// <param name="Text">A text representation of a PublicEventType.</param>
        /// <param name="PublicEventType">The parsed PublicEventType.</param>
        public static Boolean TryParse(String Text, out PublicEventType PublicEventType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out PublicEventType))
                    PublicEventType = Register(Text);

                return true;

            }

            PublicEventType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this PublicEventType.
        /// </summary>
        public PublicEventType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Agricultural Show
        /// </summary>
        public static PublicEventType  AgriculturalShow              { get; }
            = Register("agriculturalShow");

        /// <summary>
        /// Air Show
        /// </summary>
        public static PublicEventType  AirShow                       { get; }
            = Register("airShow");

        /// <summary>
        /// Art Event
        /// </summary>
        public static PublicEventType  ArtEvent                      { get; }
            = Register("artEvent");

        /// <summary>
        /// Athletics Meeting
        /// </summary>
        public static PublicEventType  AthleticsMeeting              { get; }
            = Register("athleticsMeeting");

        /// <summary>
        /// Commercial Event
        /// </summary>
        public static PublicEventType  CommercialEvent               { get; }
            = Register("commercialEvent");

        /// <summary>
        /// Cultural Event
        /// </summary>
        public static PublicEventType  CulturalEvent                 { get; }
            = Register("culturalEvent");

        /// <summary>
        /// Ball Game
        /// </summary>
        public static PublicEventType  BallGame                      { get; }
            = Register("ballGame");

        /// <summary>
        /// Baseball Game
        /// </summary>
        public static PublicEventType  BaseballGame                  { get; }
            = Register("baseballGame");

        /// <summary>
        /// Basketball Game
        /// </summary>
        public static PublicEventType  BasketballGame                { get; }
            = Register("basketballGame");

        /// <summary>
        /// Beer Festival
        /// </summary>
        public static PublicEventType  BeerFestival                  { get; }
            = Register("beerFestival");

        /// <summary>
        /// BicycleRace
        /// </summary>
        public static PublicEventType  BicycleRace                   { get; }
            = Register("bicycleRace");

        /// <summary>
        /// Boat Race
        /// </summary>
        public static PublicEventType  BoatRace                      { get; }
            = Register("boatRace");

        /// <summary>
        /// Boat Show
        /// </summary>
        public static PublicEventType  BoatShow                      { get; }
            = Register("boatShow");

        /// <summary>
        /// Boxing Tournament
        /// </summary>
        public static PublicEventType  BoxingTournament              { get; }
            = Register("boxingTournament");

        /// <summary>
        /// Bull Fight
        /// </summary>
        public static PublicEventType  BullFight                     { get; }
            = Register("bullFight");

        /// <summary>
        /// Ceremonial Event
        /// </summary>
        public static PublicEventType  CeremonialEvent               { get; }
            = Register("ceremonialEvent");

        /// <summary>
        /// Concert
        /// </summary>
        public static PublicEventType  Concert                       { get; }
            = Register("concert");

        /// <summary>
        /// Cricket Match
        /// </summary>
        public static PublicEventType  CricketMatch                  { get; }
            = Register("cricketMatch");

        /// <summary>
        /// Exhibition
        /// </summary>
        public static PublicEventType  Exhibition                    { get; }
            = Register("exhibition");

        /// <summary>
        /// Fair
        /// </summary>
        public static PublicEventType  Fair                          { get; }
            = Register("fair");

        /// <summary>
        /// Festival
        /// </summary>
        public static PublicEventType  Festival                      { get; }
            = Register("festival");

        /// <summary>
        /// Film Festival
        /// </summary>
        public static PublicEventType  FilmFestival                  { get; }
            = Register("filmFestival");

        /// <summary>
        /// Film/TV Making
        /// </summary>
        public static PublicEventType  FilmTVMaking                  { get; }
            = Register("filmTVMaking");

        /// <summary>
        /// Firework Display
        /// </summary>
        public static PublicEventType  FireworkDisplay               { get; }
            = Register("fireworkDisplay");

        /// <summary>
        /// Flower Event
        /// </summary>
        public static PublicEventType  FlowerEvent                   { get; }
            = Register("flowerEvent");

        /// <summary>
        /// Food Festival
        /// </summary>
        public static PublicEventType  FoodFestival                  { get; }
            = Register("foodFestival");

        /// <summary>
        /// Football Match
        /// </summary>
        public static PublicEventType  FootballMatch                 { get; }
            = Register("footballMatch");

        /// <summary>
        /// Funfair
        /// </summary>
        public static PublicEventType  Funfair                       { get; }
            = Register("funfair");

        /// <summary>
        /// Gardening or Flower Show
        /// </summary>
        public static PublicEventType  GardeningOrFlowerShow         { get; }
            = Register("gardeningOrFlowerShow");

        /// <summary>
        /// Golf Tournament
        /// </summary>
        public static PublicEventType  GolfTournament                { get; }
            = Register("golfTournament");

        /// <summary>
        /// Hockey Game
        /// </summary>
        public static PublicEventType  HockeyGame                    { get; }
            = Register("hockeyGame");

        /// <summary>
        /// Horse Race Meeting
        /// </summary>
        public static PublicEventType  HorseRaceMeeting              { get; }
            = Register("horseRaceMeeting");

        /// <summary>
        /// International Sports Meeting
        /// </summary>
        public static PublicEventType  InternationalSportsMeeting    { get; }
            = Register("internationalSportsMeeting");

        /// <summary>
        /// Major Event
        /// </summary>
        public static PublicEventType  MajorEvent                    { get; }
            = Register("majorEvent");

        /// <summary>
        /// Marathon
        /// </summary>
        public static PublicEventType  Marathon                      { get; }
            = Register("marathon");

        /// <summary>
        /// Market
        /// </summary>
        public static PublicEventType  Market                        { get; }
            = Register("market");

        /// <summary>
        /// Match
        /// </summary>
        public static PublicEventType  Match                         { get; }
            = Register("match");

        /// <summary>
        /// Motor Show
        /// </summary>
        public static PublicEventType  MotorShow                     { get; }
            = Register("motorShow");

        /// <summary>
        /// Motor Sport Race Meeting
        /// </summary>
        public static PublicEventType  MotorSportRaceMeeting         { get; }
            = Register("motorSportRaceMeeting");

        /// <summary>
        /// Open Air Concert
        /// </summary>
        public static PublicEventType  OpenAirConcert                { get; }
            = Register("openAirConcert");

        /// <summary>
        /// Parade
        /// </summary>
        public static PublicEventType  Parade                        { get; }
            = Register("parade");

        /// <summary>
        /// Procession
        /// </summary>
        public static PublicEventType  Procession                    { get; }
            = Register("procession");

        /// <summary>
        /// Race Meeting
        /// </summary>
        public static PublicEventType  RaceMeeting                   { get; }
            = Register("raceMeeting");

        /// <summary>
        /// Rugby Match
        /// </summary>
        public static PublicEventType  RugbyMatch                    { get; }
            = Register("rugbyMatch");

        /// <summary>
        /// Several Major Events
        /// </summary>
        public static PublicEventType  SeveralMajorEvents            { get; }
            = Register("severalMajorEvents");

        /// <summary>
        /// Show
        /// </summary>
        public static PublicEventType  Show                          { get; }
            = Register("show");

        /// <summary>
        /// Show Jumping
        /// </summary>
        public static PublicEventType  ShowJumping                   { get; }
            = Register("showJumping");

        /// <summary>
        /// Sound And Light Show
        /// </summary>
        public static PublicEventType  SoundAndLightShow             { get; }
            = Register("soundAndLightShow");

        /// <summary>
        /// Sports Meeting
        /// </summary>
        public static PublicEventType  SportsMeeting                 { get; }
            = Register("sportsMeeting");

        /// <summary>
        /// State Occasion
        /// </summary>
        public static PublicEventType  StateOccasion                 { get; }
            = Register("stateOccasion");

        /// <summary>
        /// Street Festival
        /// </summary>
        public static PublicEventType  StreetFestival                { get; }
            = Register("streetFestival");

        /// <summary>
        /// Tennis Tournament
        /// </summary>
        public static PublicEventType  TennisTournament              { get; }
            = Register("tennisTournament");

        /// <summary>
        /// Theatrical Event
        /// </summary>
        public static PublicEventType  TheatricalEvent               { get; }
            = Register("theatricalEvent");

        /// <summary>
        /// Tournament
        /// </summary>
        public static PublicEventType  Tournament                    { get; }
            = Register("tournament");

        /// <summary>
        /// Trade Fair
        /// </summary>
        public static PublicEventType  TradeFair                     { get; }
            = Register("tradeFair");

        /// <summary>
        /// Water Sports Meeting
        /// </summary>
        public static PublicEventType  WaterSportsMeeting            { get; }
            = Register("waterSportsMeeting");

        /// <summary>
        /// Wine Festival
        /// </summary>
        public static PublicEventType  WineFestival                  { get; }
            = Register("wineFestival");

        /// <summary>
        /// Winter Sports Meeting
        /// </summary>
        public static PublicEventType  WinterSportsMeeting           { get; }
            = Register("winterSportsMeeting");

        /// <summary>
        /// Unknown
        /// </summary>
        public static PublicEventType  Unknown                       { get; }
            = Register("unknown");

        /// <summary>
        /// Other
        /// </summary>
        public static PublicEventType  Other                         { get; }
            = Register("other");

        #endregion


        #region Operator overloading

        #region Operator == (PublicEventType1, PublicEventType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PublicEventType1">A PublicEventType.</param>
        /// <param name="PublicEventType2">Another PublicEventType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (PublicEventType PublicEventType1,
                                           PublicEventType PublicEventType2)

            => PublicEventType1.Equals(PublicEventType2);

        #endregion

        #region Operator != (PublicEventType1, PublicEventType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PublicEventType1">A PublicEventType.</param>
        /// <param name="PublicEventType2">Another PublicEventType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (PublicEventType PublicEventType1,
                                           PublicEventType PublicEventType2)

            => !PublicEventType1.Equals(PublicEventType2);

        #endregion

        #region Operator <  (PublicEventType1, PublicEventType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PublicEventType1">A PublicEventType.</param>
        /// <param name="PublicEventType2">Another PublicEventType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (PublicEventType PublicEventType1,
                                          PublicEventType PublicEventType2)

            => PublicEventType1.CompareTo(PublicEventType2) < 0;

        #endregion

        #region Operator <= (PublicEventType1, PublicEventType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PublicEventType1">A PublicEventType.</param>
        /// <param name="PublicEventType2">Another PublicEventType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (PublicEventType PublicEventType1,
                                           PublicEventType PublicEventType2)

            => PublicEventType1.CompareTo(PublicEventType2) <= 0;

        #endregion

        #region Operator >  (PublicEventType1, PublicEventType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PublicEventType1">A PublicEventType.</param>
        /// <param name="PublicEventType2">Another PublicEventType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (PublicEventType PublicEventType1,
                                          PublicEventType PublicEventType2)

            => PublicEventType1.CompareTo(PublicEventType2) > 0;

        #endregion

        #region Operator >= (PublicEventType1, PublicEventType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PublicEventType1">A PublicEventType.</param>
        /// <param name="PublicEventType2">Another PublicEventType.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (PublicEventType PublicEventType1,
                                           PublicEventType PublicEventType2)

            => PublicEventType1.CompareTo(PublicEventType2) >= 0;

        #endregion

        #endregion

        #region IComparable<PublicEventType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two PublicEventTypes.
        /// </summary>
        /// <param name="Object">PublicEventType to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is PublicEventType PublicEventType
                   ? CompareTo(PublicEventType)
                   : throw new ArgumentException("The given object is not PublicEventType!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(PublicEventType)

        /// <summary>
        /// Compares two PublicEventTypes.
        /// </summary>
        /// <param name="PublicEventType">PublicEventType to compare with.</param>
        public Int32 CompareTo(PublicEventType PublicEventType)

            => String.Compare(InternalId,
                              PublicEventType.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<PublicEventType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two PublicEventTypes for equality.
        /// </summary>
        /// <param name="Object">PublicEventType to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is PublicEventType PublicEventType &&
                   Equals(PublicEventType);

        #endregion

        #region Equals(PublicEventType)

        /// <summary>
        /// Compares two PublicEventTypes for equality.
        /// </summary>
        /// <param name="PublicEventType">PublicEventType to compare with.</param>
        public Boolean Equals(PublicEventType PublicEventType)

            => String.Equals(InternalId,
                             PublicEventType.InternalId,
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
