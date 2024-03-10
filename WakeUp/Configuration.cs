using Dalamud.Configuration;
using Dalamud.Plugin;
using System;

namespace WakeUp
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;

        // Kind of a confusing one but this is all the usable chat channels, separated into 4 clusters
        public bool[][] chatChannels { get; set; } = [
            [true, true, true, true, true, true, true, true, true, true, true, true, true, true, true], // Types 10-24 (note: 12/13 are TellOutgoing/TellIncoming, 14 should be the same as 32)
            [true, false, true], // Types 30-32 (note: 31 is ignored, 32 should be the same as 14)
            [true, true], // Types 36, 37
            [true, true, true, true, true, true, true] // Types 101-107
        ];

        #if DEBUG
        public bool echo { get; set; } = true;
        private Dalamud.Plugin.Services.IPluginLog ?log;
        #endif

        public bool messagePassesFilter(Dalamud.Game.Text.XivChatType type) {
            // Boundaries, nothing outside these is even considered
            if (((int)type > 108) || ((int)type < 10)) {
                return false; 
            }

            if((int)type < 25) {
                // 1st cluster
                #if DEBUG
                    this.log.Debug("TYPE [{0}] RETURNING [[{1}] -> [{2}]]", (int)type, (int)type - 10, chatChannels[0][(int)type - 10]);
                #endif
                return chatChannels[0][(int)type - 10];
            }

            if((int) type > 100) {
                // 4th cluster
                return chatChannels[3][(int)type - 100];
            }

            #if DEBUG
            if((int) type == 56) {
                return echo;
            }
            #endif

            // Now it's just kind of funky
            return (int)type switch {
                30 => chatChannels[1][0],
                32 => chatChannels[0][3],
                36 => chatChannels[2][0],
                37 => chatChannels[2][1],
                _ => false,
            };
        }

        // the below exist just to make saving less cumbersome
        [NonSerialized]
        private DalamudPluginInterface? PluginInterface;

        public void Initialize(DalamudPluginInterface pluginInterface, Dalamud.Plugin.Services.IPluginLog _pluginLog)
        {
            this.PluginInterface = pluginInterface;

            #if DEBUG
                this.log = _pluginLog;
            #endif
        }

        public void Save()
        {
            this.PluginInterface!.SavePluginConfig(this);
        }
    }
}
