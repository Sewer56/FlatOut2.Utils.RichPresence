﻿using System.Runtime.InteropServices;
using FlatOut2.SDK.Enums;
using FlatOut2.SDK.Functions;
using FlatOut2.SDK.Structs;

namespace FlatOut2.SDK.API;

/// <summary>
/// High level APIs that provide various bits of information about the game.
/// </summary>
public static class Info
{
    /// <summary>
    /// Multiplayer related info.
    /// </summary>
    public static class Multiplayer
    {
        /// <summary>
        /// True if the player is inside an online lobby or race.
        /// </summary>
        public static unsafe bool IsMultiplayer()
        {
            return LinkSession.Instance->VTable == (void*)LinkSession.MultiplayerVTable;
        }
        
        /// <summary>
        /// Gets the player count for multiplayer room/lobby.
        /// </summary>
        public static unsafe int GetPlayerCount()
        {
            var info = LinkSession.Instance->SessionInfo;
            if (info == null)
                return 1;

            return info->GetPlayerCount();
        }
        
        /// <summary>
        /// Gets the name of the current FlatOut lobby.
        /// </summary>
        public static unsafe string GetLobbyName()
        {
            var info = LinkSession.Instance->SessionInfo;
            if (info == null)
                return "";

            // Lobby is named after 1st player, even after host migration.
            var playerData = info->PlayerData;
            if (playerData == null)
                return "";

            var sessionPlayer = playerData->SessionPlayer;
            if (playerData->SessionPlayer == null)
                return "";
            
            return sessionPlayer->GetPlayerName();
        }

        /// <summary>
        /// Returns maximum allowed players in multiplayer session.
        /// </summary>
        public static unsafe int GetMaxPlayers() => *(int*)0x893DBC;
        
        // Technically not GameSpy player count but is synced ^^
    }

    /// <summary>
    /// General race related info,
    /// </summary>
    public static class Race
    {
        /// <summary>
        /// Gets the name of the current race track.
        /// </summary>
        public static string GetCurrentLevelName()
        {
            var id = GetCurrentLevelId();
            return Marshal.PtrToStringAnsi(Level.GetLevelName.GetWrapper()(id));
        }
        
        /// <summary>
        /// Gets the name of the current race track.
        /// </summary>
        public static unsafe string GetCurrentLevelLoadingScreenPath()
        {
            var id = GetCurrentLevelId();
            var screen = *LoadScreenInfo.Instance;
            if (screen == null)
                return "";

            var tuple = screen->LevelAndBackgroundImageTuples;
            for (int x = 0; x < screen->NumLevelAndBackgroundImageTuples; x++)
            {
                if (tuple->LevelId == id)
                    return tuple->BackgroundImagePath;
                
                tuple++;
            }

            return "";
        }
        
        /// <summary>
        /// Gets the name of the current race track.
        /// </summary>
        public static unsafe int GetCurrentLevelId()
        {
            var racePtr = *RaceInfo.Instance;
            return racePtr == null ? 0 : racePtr->LevelId;
        }

        /// <summary>
        /// Retrieves the current race mode as a readable string.
        /// </summary>
        /// <returns></returns>
        public static unsafe GameMode GetCurrentGameMode()
        {
            var racePtr = *RaceInfo.Instance;
            return racePtr == null ? GameMode.None : racePtr->GameMode;
        }
        
        /// <summary>
        /// Retrieves the current game rules as a readable string.
        /// </summary>
        public static unsafe GameRules GetCurrentGameRules()
        {
            var racePtr = *RaceInfo.Instance;
            return racePtr == null ? GameRules.Default : racePtr->GameRules;
        }
        
        /// <summary>
        /// Retrieves the current game rules as a readable string.
        /// </summary>
        public static string GetCurrentGameRulesString() => GetCurrentGameRulesString(GetCurrentGameRules());

        /// <summary>
        /// Retrieves the current game rules as a readable string.
        /// </summary>
        public static string GetCurrentGameRulesString(GameRules rules)
        {
            return rules switch
            {
                GameRules.Race => "Race",
                GameRules.Stunt => "Stunt",
                GameRules.Derby => "Derby",
                GameRules.HunterPrey => "Hunter, Prey",
                GameRules.ArcadeAdventure => "Arcade Adventure",
                GameRules.PongRace => "Pong Race",
                GameRules.TimeAttack => "Time Attack",
                GameRules.Test => "Test Mode",
                _ => "Race",
            };
        }

        /// <summary>
        /// Retrieves the current race mode as a readable string.
        /// </summary>
        public static string GetCurrentGameModeStr(GameMode mode)
        {
            return mode switch
            {
                GameMode.Career => "Career Mode",
                GameMode.TimeAttack => "Time Attack",
                GameMode.SingleRace => "Single Race",
                GameMode.InstantAction => "Instant Action",
                GameMode.CrashTestDummy => "Crash Test",
                GameMode.OnlineMultiplayer => "Online Multiplayer",
                GameMode.PartyMode => "Party Mode",
                GameMode.Developer => "Developer Mode",
                GameMode.Tournament => "Tournament",
                GameMode.Splitscreen => "Split Screen",
                GameMode.Test => "Test",
                _ => ""
            };
        }

        /// <summary>
        /// Retrieves the current race mode as a readable string.
        /// </summary>
        public static string GetCurrentGameModeStr() => GetCurrentGameModeStr(GetCurrentGameMode());

        /// <summary>
        /// True if game is currently paused.
        /// </summary>
        public static unsafe bool IsPaused()
        {
            var racePtr = *RaceInfo.Instance;
            if (racePtr == null)
                return false;

            if (racePtr->HostObject == null)
                return false;

            return racePtr->HostObject->IsPaused;
        }

        /// <summary>
        /// True if player is currently inside a race. (not in menus)
        /// </summary>
        /// <returns>True if racing, else false.</returns>
        public static bool IsRacing()
        {
            return State.GetCurrentGameState() == SessionType.Race;
        }

        /// <summary>
        /// Gets current in-game timer as TimeSpan.
        /// </summary>
        /// <returns></returns>
        public static unsafe TimeSpan GetCurrentTimer()
        {
            var racePtr = *RaceInfo.Instance;
            if (racePtr == null)
                return TimeSpan.Zero;

            if (racePtr->HostObject == null)
                return TimeSpan.Zero;

            return racePtr->HostObject->TimerAsTimeSpan;
        }
    }
    
    /// <summary>
    /// General state related info,
    /// </summary>
    public static class State
    {
        /// <summary>
        /// Gets the name of the current game state.
        /// </summary>
        public static unsafe SessionType GetCurrentGameState()
        {
            var racePtr = *RaceInfo.Instance;
            return racePtr == null ? SessionType.None : racePtr->SessionType;
        }
    
        /// <summary>
        /// Gets the name of the current game state as a string.
        /// </summary>
        public static string GetCurrentGameStateString(SessionType sessionType)
        {
            return sessionType switch
            {
                SessionType.Menu => "In Menu",
                SessionType.Race => "In Race",
                _ => "Unknown State"
            };
        }
        
        /// <summary>
        /// Gets the name of the current game state as a string.
        /// </summary>
        public static string GetCurrentGameStateString() => GetCurrentGameStateString(GetCurrentGameState());
    }
}