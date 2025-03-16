using Physics.Core;

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
    }
}