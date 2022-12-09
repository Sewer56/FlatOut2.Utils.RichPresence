using System.Runtime.InteropServices;
using FlatOut2.SDK.Enums;

namespace FlatOut2.SDK.Structs;

[StructLayout(LayoutKind.Explicit)]
public unsafe struct SessionPlayer
{
    [FieldOffset(0x96)]
    public ushort NetworkId;
    
    [FieldOffset(0xC8)]
    public long QUID; // need to ask Zolika what this is
    
    [FieldOffset(0xEC)]
    public int NumReady;
    
    [FieldOffset(0x10C)]
    public int CarSkinId;
    
    [FieldOffset(0x13C)]
    public int CarId;
    
    [FieldOffset(0x160)]
    public void* PlayerNamePtr;

    public string GetPlayerName() => PlayerNamePtr == null ? "" : Marshal.PtrToStringUni((nint)PlayerNamePtr);

    /// <summary>
    /// This'll be 0 if the wchar_t is a pointer
    /// </summary>
    [FieldOffset(0x165)]
    public void* PlayerNamePtrRemains;

    [FieldOffset(0x1F4)]
    public SessionPlayerStatus PlayerStatus;
}

[StructLayout(LayoutKind.Explicit, Size = 0xC)]
public unsafe struct SessionPlayerData
{
    [FieldOffset(0x0)]
    public SessionPlayer* SessionPlayer;
    
    // Padding?
}