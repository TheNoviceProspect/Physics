using Physics.App;
using Physics.Core;
using Physics.Core.Configuration;

public class Program
{
#if DEBUG
    public static Logger _log = new Logger("log.txt", true);
#else
    public static Logger _log = new Logger("log.txt", false);
#endif

    private static void Main(string[] args)
    {
        _log.Info("Physics Demo v0.1 started");
        _log.Info("Loading Config");
        var config = AppConfig.Load();
        _log.Info($"Config loaded: [Size:{config.Width}x{config.Height}] [Fullscreen:{config.Fullscreen}] [Logs to keep: {config.LogFilesToKeep}]");
        _log.Info("Initializing App Window");
        using (App app = new App(config.Width, config.Height, "Physics Demo"))
        {
            app.Run();
        }
        _log.Info("Physics Demo v0.1 ended");
    }
}