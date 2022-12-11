using System.Diagnostics;
using System.Text.Json.Nodes;

namespace FlatOut2.Utils.RichPresence.Utilities;

/// <summary>
/// Utilities related to Reloaded itself.
/// </summary>
public static class ReloadedUtils
{
    private static string? _rebootCommandLine;
    private static string _rebootCommandLineArgs = "";
    
    /// <summary>
    /// Gets the commandline parameters used to reboot the game.
    /// </summary>
    /// <param name="args">Arguments necessary to pass to application.</param>
    public static string GetRebootCommandline(out string args)
    {
        // Get cached.
        args = _rebootCommandLineArgs;
        if (_rebootCommandLine != null)
            return _rebootCommandLine;

        // Otherwise compute.
        var currentExePath = Process.GetCurrentProcess().MainModule!.FileName;
        if (HasBootstrapper(Path.GetDirectoryName(currentExePath)!))
        {
            _rebootCommandLine = currentExePath;
            _rebootCommandLineArgs = "";
            args = "";
            return currentExePath;
        }

        // Check if we have ASI Plugin installed by looking 
        var loaderConfigPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Reloaded-Mod-Loader-II"), "ReloadedII.json");
        var json = JsonNode.Parse(File.ReadAllText(loaderConfigPath));
        _rebootCommandLine = json!["LauncherPath"]!.ToString();
        _rebootCommandLineArgs = $"--launch \"{currentExePath}\"";
        args = _rebootCommandLineArgs;
        return _rebootCommandLine;
    }

    /// <summary>
    /// Checks if Reloaded Bootstrapper is present.
    /// </summary>
    /// <param name="directoryName">Directory where game is contained.</param>
    private static bool HasBootstrapper(string directoryName)
    {
        var files = Directory.GetFiles(directoryName, "*.asi", SearchOption.AllDirectories);
        return files.Any(x => Path.GetFileName(x).Equals("Reloaded.Mod.Loader.Bootstrapper.asi", StringComparison.OrdinalIgnoreCase));
    }
}