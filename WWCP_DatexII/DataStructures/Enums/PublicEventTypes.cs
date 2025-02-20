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
    /// Types of public events.
    /// </summary>
    public enum PublicEventTypes
    {

        [XmlEnum("agriculturalShow")]
        AgriculturalShow,

        [XmlEnum("airShow")]
        AirShow,

        [XmlEnum("artEvent")]
        ArtEvent,

        [XmlEnum("athleticsMeeting")]
        AthleticsMeeting,

        [XmlEnum("commercialEvent")]
        CommercialEvent,

        [XmlEnum("culturalEvent")]
        CulturalEvent,

        [XmlEnum("ballGame")]
        BallGame,

        [XmlEnum("baseballGame")]
        BaseballGame,

        [XmlEnum("basketballGame")]
        BasketballGame,

        [XmlEnum("beerFestival")]
        BeerFestival,

        [XmlEnum("bicycleRace")]
        BicycleRace,

        [XmlEnum("boatRace")]
        BoatRace,

        [XmlEnum("boatShow")]
        BoatShow,

        [XmlEnum("boxingTournament")]
        BoxingTournament,

        [XmlEnum("bullFight")]
        BullFight,

        [XmlEnum("ceremonialEvent")]
        CeremonialEvent,

        [XmlEnum("concert")]
        Concert,

        [XmlEnum("cricketMatch")]
        CricketMatch,

        [XmlEnum("exhibition")]
        Exhibition,

        [XmlEnum("fair")]
        Fair,

        [XmlEnum("festival")]
        Festival,

        [XmlEnum("filmFestival")]
        FilmFestival,

        [XmlEnum("filmTVMaking")]
        FilmTVMaking,

        [XmlEnum("fireworkDisplay")]
        FireworkDisplay,

        [XmlEnum("flowerEvent")]
        FlowerEvent,

        [XmlEnum("foodFestival")]
        FoodFestival,

        [XmlEnum("footballMatch")]
        FootballMatch,

        [XmlEnum("funfair")]
        Funfair,

        [XmlEnum("gardeningOrFlowerShow")]
        GardeningOrFlowerShow,

        [XmlEnum("golfTournament")]
        GolfTournament,

        [XmlEnum("hockeyGame")]
        HockeyGame,

        [XmlEnum("horseRaceMeeting")]
        HorseRaceMeeting,

        [XmlEnum("internationalSportsMeeting")]
        InternationalSportsMeeting,

        [XmlEnum("majorEvent")]
        MajorEvent,

        [XmlEnum("marathon")]
        Marathon,

        [XmlEnum("market")]
        Market,

        [XmlEnum("match")]
        Match,

        [XmlEnum("motorShow")]
        MotorShow,

        [XmlEnum("motorSportRaceMeeting")]
        MotorSportRaceMeeting,

        [XmlEnum("openAirConcert")]
        OpenAirConcert,

        [XmlEnum("parade")]
        Parade,

        [XmlEnum("procession")]
        Procession,

        [XmlEnum("raceMeeting")]
        RaceMeeting,

        [XmlEnum("rugbyMatch")]
        RugbyMatch,

        [XmlEnum("severalMajorEvents")]
        SeveralMajorEvents,

        [XmlEnum("show")]
        Show,

        [XmlEnum("showJumping")]
        ShowJumping,

        [XmlEnum("soundAndLightShow")]
        SoundAndLightShow,

        [XmlEnum("sportsMeeting")]
        SportsMeeting,

        [XmlEnum("stateOccasion")]
        StateOccasion,

        [XmlEnum("streetFestival")]
        StreetFestival,

        [XmlEnum("tennisTournament")]
        TennisTournament,

        [XmlEnum("theatricalEvent")]
        TheatricalEvent,

        [XmlEnum("tournament")]
        Tournament,

        [XmlEnum("tradeFair")]
        TradeFair,

        [XmlEnum("waterSportsMeeting")]
        WaterSportsMeeting,

        [XmlEnum("wineFestival")]
        WineFestival,

        [XmlEnum("winterSportsMeeting")]
        WinterSportsMeeting,

        [XmlEnum("unknown")]
        Unknown,

        [XmlEnum("other")]
        Other,

        [XmlEnum("_extended")]
        Extended

    }

}
