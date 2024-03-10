using System;
using System.Numerics;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace WakeUp.Windows;

public class MainWindow : Window, IDisposable
{
    private IDalamudTextureWrap SheepImage;
    private Plugin Plugin;

    public MainWindow(Plugin plugin, IDalamudTextureWrap sheepImage) : base(
        "Wake Up", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoResize)
    {

        this.Size = new Vector2(650, 360);
        this.SheepImage = sheepImage;
        this.Plugin = plugin;
    }

    public void Dispose()
    {
        this.SheepImage.Dispose();
    }

    public override void Draw()
    {
        if (ImGui.Button("Show Settings")) {
            this.Plugin.DrawConfigUI();
        }

        ImGui.Text("Type @@ to send British William's beautiful voice to all of your friends, reminding them to tab into their games.");


        ImGui.Spacing();

        ImGui.Indent(190);
        ImGui.Image(this.SheepImage.ImGuiHandle, new Vector2(this.SheepImage.Width/2, this.SheepImage.Height/2));

        ImGui.Indent(55);
        ImGui.Text("made with <3 by sheep");
        ImGui.Unindent(190);
    }
}
