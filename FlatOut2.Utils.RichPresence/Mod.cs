﻿using FlatOut2.Utils.RichPresence.Configuration;
using FlatOut2.Utils.RichPresence.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;

namespace FlatOut2.Utils.RichPresence;

/// <summary>
/// Your mod logic goes here.
/// </summary>
public class Mod : ModBase // <= Do not Remove.
{
    /// <summary>
    /// Provides access to the Reloaded.Hooks API.
    /// </summary>
    /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
    private readonly IReloadedHooks? _hooks;

    /// <summary>
    /// Provides access to the Reloaded logger.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Provides access to this mod's configuration.
    /// </summary>
    private Config _configuration;

    /// <summary>
    /// The configuration of the currently executing mod.
    /// </summary>
    private readonly IModConfig _modConfig;

    private readonly RPC _rpc;
    
    public Mod(ModContext context)
    {
        _hooks = context.Hooks;
        _logger = context.Logger;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;
        
        // For more information about this template, please see
        // https://reloaded-project.github.io/Reloaded-II/ModTemplate/

        // If you want to implement e.g. unload support in your mod,
        // and some other neat features, override the methods in ModBase.

        SDK.SDK.Init(_hooks!);
        _rpc = new RPC(_logger, _configuration);
    }

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        // Apply settings from configuration.
        // ... your code here.
        _configuration = configuration;
        _rpc.SetConfiguration(_configuration);
        _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
    }
    #endregion

    #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod() { }
#pragma warning restore CS8618
    #endregion
}