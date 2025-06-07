namespace Toolkit_Launcher;

internal static class Program
{
    public static FrmMain FrmMain = null!;

    [STAThread]
    private static void Main(string[] args)
    {
        var settingsService = new Toolkit_Launcher.Services.SettingsService();
        var imageCacheService = new Toolkit_Launcher.Services.ImageCacheService();
        ApplicationConfiguration.Initialize();
        Application.Run(FrmMain = new FrmMain(args, settingsService, imageCacheService));
        Application.Run(FrmMain = new FrmMain(args, settingsService, imageCacheService));
    }
}
