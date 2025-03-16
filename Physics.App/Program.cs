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
        config.Fullscreen = true;
        config.Width = 1920;
        config.Height = 1080;
        _log.Info($"Config changed: {config.Width}x{config.Height} {config.Fullscreen}");
        config.Save();
        _log.Info($"Config saved!");
        _log.Info("Physics Demo v0.1 ended");
    }
}