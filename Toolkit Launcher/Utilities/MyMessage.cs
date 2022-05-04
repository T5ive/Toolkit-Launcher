namespace Toolkit_Launcher;

public static class MyMessage
{
    private const string Caption = "Toolkit Launcher";

    public static void ShowError(string text)
    {
        MessageBox.Show(text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public static void ShowWarning(string text)
    {
        MessageBox.Show(text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static void ShowInfo(string text)
    {
        MessageBox.Show(text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public static void Show(string text, MessageBoxIcon icon = MessageBoxIcon.None)
    {
        MessageBox.Show(text, Caption, MessageBoxButtons.OK, icon);
    }

    public static bool OkCancel(string text)
    {
        return MessageBox.Show(text, Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
    }

    public static DialogResult YesNoCancel(string text)
    {
        return MessageBox.Show(text, Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
    }

    public static DialogResult YesNo(string text)
    {
        return MessageBox.Show(text, Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }
}