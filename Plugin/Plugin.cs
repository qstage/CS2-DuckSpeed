﻿using System.Runtime.InteropServices;
using CounterStrikeSharp.API.Core;

namespace DuckSpeed;

public class Plugin : BasePlugin
{
    public override string ModuleName { get; } = "DuckSpeed";
    public override string ModuleVersion { get; } = "1.0.0";
    public override string ModuleAuthor { get; } = "xstage";

    private readonly MemoryPatch memoryPatch_ = new MemoryPatch();

    public override void Load(bool hotReload)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            memoryPatch_.Init("E8 ? ? ? ? 49 8B 44 24 ? 4D 8B 6C 24");
            memoryPatch_.Apply("90 90 90 90 90");
        }
        else
        {
            memoryPatch_.Init("74 ? 48 83 C1 ? BA ? ? ? ? 41 B9 ? ? ? ? 41 B8 ? ? ? ? E8 ? ? ? ? 88 9E");
            memoryPatch_.Apply("EB");
        }
    }

    public override void Unload(bool hotReload) => memoryPatch_.Restore();
}
