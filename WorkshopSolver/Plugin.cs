using System.IO;
using Dalamud.Data;
using Dalamud.Game;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using WorkshopSolver.Windows;

namespace WorkshopSolver;

public sealed class Plugin : IDalamudPlugin {
    public string Name => "WorkshopSolver";
    private const string CommandName = "/psanctuary";

    [PluginService] public static DalamudPluginInterface PluginInterface { get; private set; }
    [PluginService] public static CommandManager CommandManager { get; private set; }
    [PluginService] public static DataManager DataManager { get; private set; }
    [PluginService] public static SigScanner SigScanner { get; private set; }

    public Configuration Configuration { get; private set; }
    public WindowSystem WindowSystem = new("WorkshopSolver");

    public Plugin() {
        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        Configuration.Initialize(PluginInterface);

        WindowSystem.AddWindow(new ConfigWindow(this));
        WindowSystem.AddWindow(new MainWindow(this));
        WindowSystem.AddWindow(new WidgetWindow(this));

        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand) {
            HelpMessage = "Opens the main WorkshopSolver interface."
        });

        PluginInterface.UiBuilder.Draw += DrawUI;
        PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
    }

    public void Dispose() {
        WindowSystem.RemoveAllWindows();
        CommandManager.RemoveHandler(CommandName);
    }

    private void OnCommand(string command, string args) {
        switch (args) {
            case "settings":
            case "config":
                DrawConfigUI();
                break;
            case "widget":
                WindowSystem.GetWindow("WorkshopSolver Widget").IsOpen = true;
                break;
            default:
                WindowSystem.GetWindow("WorkshopSolver").IsOpen = true;
                break;
        }
    }

    private void DrawUI() {
        WindowSystem.Draw();
    }

    public void DrawConfigUI() {
        WindowSystem.GetWindow("WorkshopSolver Config").IsOpen = true;
    }
}
