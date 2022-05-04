namespace Toolkit_Launcher;

public static class Checker
{
    public static bool IsAdministrator()
    {
        return new WindowsPrincipal(WindowsIdentity.GetCurrent())
            .IsInRole(WindowsBuiltInRole.Administrator);
    }

    #region Check Empty

    public static bool IsEmpty(this object? str)
    {
        return str is null || string.IsNullOrWhiteSpace(str.ToString());
    }

    #endregion Check Empty

    public static bool IsDuplicate(this string newName, ListInfo listInfo)
    {
        return listInfo.ListGroup.Any(group => newName == group.Identify.Name);
    }

    public static bool IsDuplicate(this string newName, GroupInfo groupInfo)
    {
        return groupInfo.ListGroup.Any(group => newName == group.Identify.Name);
    }
}