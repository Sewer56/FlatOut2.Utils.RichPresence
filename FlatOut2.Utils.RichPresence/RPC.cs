using System.Diagnostics;
using System.Text;
using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using FlatOut2.SDK.API;
using FlatOut2.SDK.Enums;
using FlatOut2.Utils.RichPresence.Configuration;
using FlatOut2.Utils.RichPresence.Utilities;
using ILogger = Reloaded.Mod.Interfaces.ILogger;

namespace FlatOut2.Utils.RichPresence;

/// <summary>
/// Runs the Rich Presence Logic.
/// </summary>
public class RPC
{
    /*
        Implementation is based on Riders Tweakbox.
    */

    private DiscordRpcClient _discordRpc;
    private bool _enableRpc = true;

    private bool _isJoining = false;
    private readonly ILogger _logger;
    private Config _configuration;
    private readonly Timer _timer;

    public RPC(ILogger logger, Config configuration)
    {
        _logger = logger;
        _configuration = configuration;

        // Not like you could get this from decompiling anyway. Obfuscation? That sucks.
        _discordRpc = new DiscordRpcClient("1050028179185737728", -1, new NullLogger(), true, null);
        _discordRpc.Initialize();

        _discordRpc.RegisterUriScheme(null, null);
        _discordRpc.Subscribe(EventType.Join);
        _discordRpc.OnJoinRequested += OnJoinRequested;
        _discordRpc.OnJoin += OnJoin;
        _timer = new Timer(OnTick, null, 0, 5000);
    }

    internal void SetConfiguration(Config config) => _configuration = config;

    private void OnJoinRequested(object sender, JoinRequestMessage args)
    {
        _logger.WriteLineAsync("Auto-accepting Join Request");
        _discordRpc.Respond(args, true);
    }

    private void OnJoin(object sender, JoinMessage args)
    {
        if (_isJoining)
            return;

        _isJoining = true;
        try
        {
            var lobbyName = Encoding.UTF8.GetString(Convert.FromBase64String(args.Secret));
            _logger.WriteLineAsync($"Joining Lobby {lobbyName} via Discord");
            JoinGameViaReboot(lobbyName);
        }
        finally
        {
            _isJoining = false;
        }
    }
    
    private void JoinGameViaReboot(string lobbyName)
    {
        var path = ReloadedUtils.GetRebootCommandline(out var args);
        if (!string.IsNullOrEmpty(args)) // reboot via r2 launcher.
            Process.Start(path, $"{args} --arguments \"-join=\"\"{lobbyName}\"\"\"");
        else
            Process.Start(path, $"-join=\"{lobbyName}\"");

        Environment.Exit(0);
    }

    private void OnTick(object state)
    {
        var richPresence = new DiscordRPC.RichPresence
        {
            Details = GetCurrentDetails(),
            State = GetCurrentState(),
            Assets = new Assets()
        };

        // Add Race Details
        if (Info.Race.IsRacing())
        {
            var timeStamps = new Timestamps();
            
            // In MultiPlayer Pause doesn't stop timer, so we can ke can still show time.
            if (!Info.Race.IsPaused() || Info.Multiplayer.IsMultiplayer())
            {
                DateTime levelStartTime = DateTime.UtcNow.Subtract(Info.Race.GetCurrentTimer());
                timeStamps.Start = levelStartTime;
                richPresence.Timestamps = timeStamps;
            }
            
            // Add car
            if (_configuration.IncludeCar)
                richPresence.State += $" with {Info.Race.GetCurrentCarId()}";

            // Add level and image
            var levelName = Info.Race.GetCurrentLevelName();
            var iconName = Path.GetFileNameWithoutExtension(Info.Race.GetCurrentLevelLoadingScreenPath());
            richPresence.Assets.LargeImageText = levelName;

            if (string.IsNullOrEmpty(iconName))
                CustomThumbnailHelper.TryGet(levelName, out iconName);
            
            richPresence.Assets.LargeImageKey  = iconName;
        }

        // Add Multiplayer Info
        if (Info.Multiplayer.IsMultiplayer())
        {
            var serverId = Info.Multiplayer.GetLobbyName();
            var partyId = "Party_" + serverId;
            richPresence.Party = new Party()
            {
                ID = partyId,
                Max = Info.Multiplayer.GetMaxPlayers(),
                Privacy = Party.PrivacySetting.Public,
                Size = Info.Multiplayer.GetPlayerCount()
            };

            richPresence.Secrets = new Secrets()
            {
                // Discord screws up the secret if sent in plaintext, I have no idea why.
                JoinSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(serverId))
            };
        }

        // Send to Discord
        _discordRpc.SetPresence(richPresence);
    }

    private string GetCurrentState()
    {
        var gameState = Info.State.GetCurrentGameState();
        if (gameState == SessionType.Menu)
            return Info.State.GetCurrentGameStateString(gameState);

        return Info.Race.GetCurrentGameRulesString();
    }

    /// <summary>
    /// Gets text set directly under game name on Discord.
    /// </summary>
    private string GetCurrentDetails() => Info.Race.GetCurrentGameModeStr();
}