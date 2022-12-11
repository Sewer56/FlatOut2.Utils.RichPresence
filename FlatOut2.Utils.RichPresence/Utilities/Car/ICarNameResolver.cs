namespace FlatOut2.Utils.RichPresence.Utilities.Car;

/// <summary>
/// Resolves car names for FlatOut.
/// </summary>
public interface ICarNameResolver
{
    /// <summary>
    /// Gets the name of the car given the ID.
    /// </summary>
    public string GetName(int id);
}