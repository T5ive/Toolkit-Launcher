namespace Toolkit_Launcher;

public static class StringHelper
{
    public static bool EndWith(this string str, string endWith)
    {
        return str.EndsWith(endWith, StringComparison.CurrentCultureIgnoreCase);
    }
}