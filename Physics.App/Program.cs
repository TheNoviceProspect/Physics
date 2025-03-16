using OpenTK.Windowing.GraphicsLibraryFramework;
using Physics.App;
using Physics.Core;
using Physics.Core.Configuration;

internal class Program
{
#if DEBUG
    internal static Logger _log = new Logger("log.txt", true);
#else
        internal static Logger _log = new Logger("log.txt", false);
#endif

    private static void Main(string[] args)
    {
        _log.Info("Physics Demo v0.1 started");
        var config = AppConfig.Load();
        _log.Info($"Config loaded: {config.Width}x{config.Height} {config.Fullscreen}");
        _log.Info("Initializing App Window");
        using (App app = new App(config.Width, config.Height, "Physics Demo"))
        {
            app.Run();
        }
        _log.Info("Physics Demo v0.1 ended");
    }
}