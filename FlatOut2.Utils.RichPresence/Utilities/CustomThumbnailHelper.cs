using System.Diagnostics.CodeAnalysis;

namespace FlatOut2.Utils.RichPresence.Utilities;

/// <summary>
/// Custom dictionary for mapping level names to icons.
/// </summary>
public static class CustomThumbnailHelper
{
    private static Dictionary<string, string> _dictionary = new()
    {
        // Forest
        { "Reversed Timberlands 1", "loading_bg_forest1a" },
        { "Reversed Timberlands 2", "loading_bg_forest1b" },
        { "Reversed Timberlands 3", "loading_bg_forest1c" },
        
        { "Reversed Pinegrove 1", "loading_bg_forest2a" },
        { "Reversed Pinegrove 2", "loading_bg_forest2b" },
        { "Reversed Pinegrove 3", "loading_bg_forest2c" },
        
        { "Suicide Gorge", "loading_bg_forest2a" },
        
        // RAM Ranch
        { "Reversed Midwest Ranch 1", "loading_bg_fields1a" },
        { "Reversed Midwest Ranch 2", "loading_bg_fields1b" },
        { "Reversed Midwest Ranch 3", "loading_bg_fields1c" },
        
        { "Reversed Farmlands 1", "loading_bg_fields2a" },
        { "Reversed Farmlands 2", "loading_bg_fields2b" },
        { "Reversed Farmlands 3", "loading_bg_fields2c" },
        
        { "Hazard Hollow", "loading_bg_fields2a" },
        { "Farmlands 4 F8", "loading_bg_fields2b" },
        { "Farmlands 5 F8", "loading_bg_fields2c" },
        { "Hazzard Dragstrip", "loading_bg_fields2b" },
        { "Junkyard", "loading_bg_fields2b" },
        
        // Desert
        { "Reversed Desert Oil Field", "loading_bg_desert_1a" },
        { "Reversed Desert Scrap Yard", "loading_bg_desert_1b" },
        { "Reversed Desert Town", "loading_bg_desert_1c" },
        
        // Canal
        { "Suicide Canal 1", "loading_bg_canal1a" },
        { "Suicide Canal 2", "loading_bg_canal1b" },
        { "Suicide Canal 3", "loading_bg_canal1c" },
        
        { "Reversed Water Canal 1", "loading_bg_canal1a" },
        { "Reversed Water Canal 2", "loading_bg_canal1b" },
        { "Reversed Water Canal 3", "loading_bg_canal1c" },
        
        // City
        { "Reversed City Central 1", "loading_bg_city1a" },
        { "Reversed City Central 2", "loading_bg_city1b" },
        { "Reversed City Central 3", "loading_bg_city1c" },
        
        { "Reversed Downtown 1", "loading_bg_city2a" },
        { "Reversed Downtown 2", "loading_bg_city2b" },
        { "Reversed Downtown 3", "loading_bg_city2c" },
        
        { "City3 Central 1 F8", "loading_bg_city1b" },
        { "City3 Kruger Central 1", "loading_bg_city1a" },
        { "Traffic Jam", "loading_bg_city1a" },
        { "Suicide Highway", "loading_bg_city1b" },
        
        // Race
        { "Reversed Riverbay Circuit 1", "loading_bg_racing1a" },
        { "Reversed Riverbay Circuit 2", "loading_bg_racing1b" },
        { "Reversed Riverbay Circuit 3", "loading_bg_racing1c" },
        
        { "Reversed Motor Raceway 1", "loading_bg_racing2a" },
        { "Reversed Motor Raceway 2", "loading_bg_racing2b" },
        { "Reversed Motor Raceway 3", "loading_bg_racing2c" },
        
        // Event
        { "GoCart Jump", "loading_bg_oval1a" },
        { "GoCart Hairpin", "loading_bg_oval1a" },
        { "Test F8 right-fast", "loading_bg_oval1a" },
        { "Test F8 right-bumpy", "loading_bg_oval1a" },
        { "Drag Strip", "loading_bg_oval1a" },
        
        { "Crash Alley F8", "loading_bg_arena6" },
        { "Crash Alley double loop", "loading_bg_arena6" },
        
        { "Obstacle course 1", "loading_bg_oval1c" },
        { "350 yd Derby Dash", "loading_bg_fieldgoal" },
        { "Soccer Derby in Tunnel", "loading_bg_city1b" },
        { "Hellbowl", "loading_bg_arena3" }
    };

    /// <summary>
    /// Tries to get thumbnail for given stage name.
    /// </summary>
    public static bool TryGet(string key, [MaybeNullWhen(false)] out string result)
    {
        return _dictionary.TryGetValue(key, out result);
    }
}