using System.Runtime.InteropServices;
using AWB.Stream.Emulator.System.IO.Hashing;

namespace FlatOut2.Utils.RichPresence.Utilities;

/// <summary>
/// Checks patch file for mod types which require special handling.
/// </summary>
public class PatchChecker
{
    public static ModType GetModType(byte[] patchFile)
    {
        ulong hash = 0;
        XxHash3.Hash(patchFile, MemoryMarshal.Cast<ulong, byte>(MemoryMarshal.CreateSpan(ref hash, 1)));

        return hash switch
        {
            0x9A8E82661161531A => ModType.FlatoutJoint,
            _ => ModType.Vanilla,
        };
    }
}

public enum ModType
{
    Vanilla,
    
    // FOJ Community Mod w/ Update 3 (all 5 parts)
    FlatoutJoint
}