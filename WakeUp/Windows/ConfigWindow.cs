using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace WakeUp.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;

    public ConfigWindow(Plugin plugin) : base(
        "Configuration",
        ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
        ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.Size = new Vector2(232, 740);
        this.SizeCondition = ImGuiCond.Always;

        this.Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void Draw()
    {
        ImGui.Text("Affected Chat Channels");
        ImGui.Separator();
    
        var chatChannelValues = this.Configuration.chatChannels;

        #if DEBUG
        var echo = this.Configuration.echo;
        if (ImGui.Checkbox("Echo", ref echo)) {
            this.Configuration.echo = echo;
            this.Configuration.Save();
        }
        #endif

        // Chat channels
        if (ImGui.Checkbox("Say", ref chatChannelValues[0][0])) {
            this.Configuration.chatChannels[0][0] = chatChannelValues[0][0];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Tell", ref chatChannelValues[0][2])) {
            // TellOutgoing and TellIncoming should match, so we set both
            this.Configuration.chatChannels[0][2] = chatChannelValues[0][2];
            this.Configuration.chatChannels[0][3] = chatChannelValues[0][2];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Party", ref chatChannelValues[0][3])) {
            // Party and CW Party should match, so we set both
            this.Configuration.chatChannels[0][3] = chatChannelValues[0][3];
            this.Configuration.chatChannels[1][2] = chatChannelValues[0][3];
            this.Configuration.Save();
        } 

        if (ImGui.Checkbox("Free Company", ref chatChannelValues[0][14])) { 
            this.Configuration.chatChannels[0][14] = chatChannelValues[0][14];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Linkshell [1]", ref chatChannelValues[0][5])) {
            this.Configuration.chatChannels[0][5] = chatChannelValues[0][5];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Linkshell [2]", ref chatChannelValues[0][6])) { 
            this.Configuration.chatChannels[0][6] = chatChannelValues[0][6];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Linkshell [3]", ref chatChannelValues[0][7])) { 
            this.Configuration.chatChannels[0][7] = chatChannelValues[0][7];
            this.Configuration.Save();
        } 

        if (ImGui.Checkbox("Linkshell [4]", ref chatChannelValues[0][8])) { 
            this.Configuration.chatChannels[0][8] = chatChannelValues[0][8];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Linkshell [5]", ref chatChannelValues[0][9])) { 
            this.Configuration.chatChannels[0][9] = chatChannelValues[0][9];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Linkshell [6]", ref chatChannelValues[0][10])) { 
            this.Configuration.chatChannels[0][10] = chatChannelValues[0][10];
            this.Configuration.Save();
        } 

        if (ImGui.Checkbox("Linkshell [7]", ref chatChannelValues[0][11])) { 
            this.Configuration.chatChannels[0][11] = chatChannelValues[0][11];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Linkshell [8]", ref chatChannelValues[0][12])) { 
            this.Configuration.chatChannels[0][12] = chatChannelValues[0][12];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Cross-world Linkshell [1]", ref chatChannelValues[2][1])) { 
            this.Configuration.chatChannels[2][1] = chatChannelValues[2][1];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Cross-world Linkshell [2]", ref chatChannelValues[3][0])) { 
            this.Configuration.chatChannels[3][0] = chatChannelValues[3][0];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Cross-world Linkshell [3]", ref chatChannelValues[3][1])) { 
            this.Configuration.chatChannels[3][1] = chatChannelValues[3][1];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Cross-world Linkshell [4]", ref chatChannelValues[3][2])) { 
            this.Configuration.chatChannels[3][2] = chatChannelValues[3][2];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Cross-world Linkshell [5]", ref chatChannelValues[3][3])) { 
            this.Configuration.chatChannels[3][3] = chatChannelValues[3][3];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Cross-world Linkshell [6]", ref chatChannelValues[3][4])) { 
            this.Configuration.chatChannels[3][4] = chatChannelValues[3][4];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Cross-world Linkshell [7]", ref chatChannelValues[3][5])) { 
            this.Configuration.chatChannels[3][5] = chatChannelValues[3][5];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Cross-world Linkshell [8]", ref chatChannelValues[3][6])) { 
            this.Configuration.chatChannels[3][6] = chatChannelValues[3][6];
            this.Configuration.Save();
        }

        // Now we're getting into the weird stuff
        // I am not sure why you would want these but...for completeness ig
        // not putting NN tho don't be weird
        if (ImGui.Checkbox("Yell", ref chatChannelValues[1][0])) { 
            this.Configuration.chatChannels[1][0] = chatChannelValues[1][0];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Shout", ref chatChannelValues[0][1])) {
            this.Configuration.chatChannels[0][1] = chatChannelValues[0][1];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("Alliance", ref chatChannelValues[0][4])) {
            this.Configuration.chatChannels[0][4] = chatChannelValues[0][4];
            this.Configuration.Save();
        }

        if (ImGui.Checkbox("PVP Team", ref chatChannelValues[2][0])) { 
            this.Configuration.chatChannels[2][0] = chatChannelValues[2][0];
            this.Configuration.Save();
        }
    }
}
