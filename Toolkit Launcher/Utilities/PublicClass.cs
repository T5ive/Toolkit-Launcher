namespace Toolkit_Launcher;

public static class PublicClass
{
    public static ListInfo ListInfo = new ListInfo(), TempListInfo = new ListInfo();
    public static readonly string PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setting.json");
    public static ImageList ImageList = new ImageList();
}