namespace Toolkit_Launcher;

internal static class Program
{
    public static FrmMain FrmMain = null!;

    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        Application.Run(FrmMain = new FrmMain(args));
    }
}