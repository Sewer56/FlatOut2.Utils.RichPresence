using FlatOut2.SDK.API;

namespace FlatOut2.Utils.RichPresence.Utilities.Car;

/// <summary>
/// Resolver for default cars.
/// </summary>
public class DefaultCarNameResolver : ICarNameResolver
{
    public string GetName(int id)
    {
        return Info.Race.GetCarName(id);
    }
}