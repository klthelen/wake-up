using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using WakeUp.Windows;
using NAudio.Wave;
namespace WakeUp
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Wake Up";
        private const string CommandName = "/wakeup";

        private DalamudPluginInterface PluginInterface { get; init; }
        private ICommandManager CommandManager { get; init; }
        public Configuration Configuration { get; init; }
        public WindowSystem WindowSystem = new("WakeUp");

        private ConfigWindow ConfigWindow { get; init; }
        private MainWindow MainWindow { get; init; }

        private readonly IPluginLog log;
        private readonly IChatGui chatGui;

        private readonly string imagePath;
        private readonly string soundPath;
        private DirectSoundOut? directSoundOut;

        public Plugin([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface, [RequiredVersion("1.0")] ICommandManager commandManager, IPluginLog _pluginLog, IChatGui _chatGui)
        {
            this.PluginInterface = pluginInterface;
            this.CommandManager = commandManager;
            log = _pluginLog; 
            chatGui = _chatGui;

            this.Configuration = this.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            this.Configuration.Initialize(this.PluginInterface);

            // you might normally want to embed resources and load them from the manifest stream
            imagePath = Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "sheep.png");
            soundPath = Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "wakeup.wav");
            var sheepImage = this.PluginInterface.UiBuilder.LoadImage(imagePath);

            ConfigWindow = new ConfigWindow(this);
            MainWindow = new MainWindow(this, sheepImage);
            
            WindowSystem.AddWindow(ConfigWindow);
            WindowSystem.AddWindow(MainWindow);

            this.CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Configure settings"
            });

            this.PluginInterface.UiBuilder.Draw += DrawUI;
            this.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;

            // Event: ChatMessage
            this.chatGui.ChatMessage += HandleMessage;
        }

        public void HandleMessage(Dalamud.Game.Text.XivChatType type, uint senderId, ref Dalamud.Game.Text.SeStringHandling.SeString sender, ref Dalamud.Game.Text.SeStringHandling.SeString message, ref bool isHandled) {
           if (((int)type < 57) || (((int)type > 100) && ((int)type < 108))) {
                this.log.Debug("[{0}][{1}]: {2}", type, sender, message.TextValue);

                if (message.TextValue == "@@") {
                    PlayWakeUp();
                }
           }
        }

        // Code below heavily referenced from Frogworks Interactive's OOF plugin: https://github.com/Frogworks-Interactive/OOF/blob/ded50f6adbff7d51326bcecbf592b1dac61fa7bf/OofPlugin/OofPlugin.cs
        public void PlayWakeUp() {
            WaveStream stream; 
            try {
                stream = new MediaFoundationReader(soundPath);
            }
            catch (System.Exception e) {
                log.Error("Exception in [PlayWakeUp()] [{0}]", e);
                return;
            }

            var audio = new WaveChannel32(stream) { 
                Volume = 2,
                PadWithZeroes = false
            };

            directSoundOut = new DirectSoundOut();

            using (stream) {
                try {
                    directSoundOut.Init(audio);
                    directSoundOut.Play();
                }
                catch (System.Exception e) {
                    log.Error("Exception in [PlayWakeUp()] [{0}]", e);
                    return;
                }
            }
        }

        public void Dispose()
        {
            this.WindowSystem.RemoveAllWindows();
            
            ConfigWindow.Dispose();
            MainWindow.Dispose();
            chatGui.ChatMessage -= HandleMessage;
            
            this.CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            // in response to the slash command, just display our main ui
            MainWindow.IsOpen = true;
        }

        private void DrawUI()
        {
            this.WindowSystem.Draw();
        }

        public void DrawConfigUI()
        {
            ConfigWindow.IsOpen = true;
        }
    }
}
