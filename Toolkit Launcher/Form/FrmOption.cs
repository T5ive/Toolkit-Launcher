namespace Toolkit_Launcher;

public partial class FrmOption : Form
{
    private static bool _detected, _extended;

    public FrmOption()
    {
        InitializeComponent();
    }

    private void FrmOption_Load(object sender, EventArgs e)
    {
        if (Checker.IsAdministrator()) return;
        MyMessage.ShowError("Request to Run as Administrator");
        btnRegis.Enabled = false;
        btnDelete.Enabled = false;
    }

    #region Menu Detection

    private void listTarget_SelectedIndexChanged(object sender, EventArgs e)
    {
        DetectMenu((MyEnums.TargetType)listTarget.SelectedIndex);
    }

    private void DetectMenu(MyEnums.TargetType type)
    {
        var typeName = GetType(type);
        var menu = $"{typeName}\\shell\\Toolkit Launcher";
        try
        {
            using var toolMenu = Registry.ClassesRoot.OpenSubKey(menu);
            if (toolMenu != null)
            {
                _detected = true;
                var listName = toolMenu.GetValueNames();
                foreach (var name in listName)
                {
                    _extended = name == "Extended";
                }
            }
            else
            {
                _detected = false;
                _extended = false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        UpdateStatus();
    }

    #endregion Menu Detection

    #region Add Menu

    private void btnRegis_Click(object sender, EventArgs e)
    {
        RegisMenu((MyEnums.TargetType)listTarget.SelectedIndex, chkExtended.Checked);
        UpdateStatus();
    }

    private void RegisMenu(MyEnums.TargetType type, bool extended, bool background = false)
    {
        var target = type == MyEnums.TargetType.Directory ? "%V" : "%1";
        var appPath = $"\"{AppDomain.CurrentDomain.BaseDirectory + "Toolkit Launcher.exe"}\" \"{target}\"";
        var iconPath = $"\"{AppDomain.CurrentDomain.BaseDirectory + "Toolkit Launcher.dll"}\",0\"";
        var typeName = GetType(type);
        if (background)
        {
            typeName += "\\Background";
        }
        var menu = $"{typeName}\\shell\\Toolkit Launcher";
        var command = $"{typeName}\\shell\\Toolkit Launcher\\command";

        try
        {
            var regMenu = Registry.ClassesRoot.CreateSubKey(menu);
            regMenu.SetValue("", "Toolkit Launcher");
            regMenu.SetValue("Icon", iconPath);
            if (extended)
                regMenu.SetValue("Extended", "");

            var regCmd = Registry.ClassesRoot.CreateSubKey(command);
            regCmd.SetValue("", appPath);

            regMenu.Close();
            regCmd.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.ToString());
        }
        finally
        {
            if (type is MyEnums.TargetType.Directory && background is false)
            {
                RegisMenu(type, extended, true);
            }
            DetectMenu(type);
        }
    }

    #endregion Add Menu

    #region DeleteMenu

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (!_detected) return;
        DeleteMenu((MyEnums.TargetType)listTarget.SelectedIndex);
    }

    private void DeleteMenu(MyEnums.TargetType type, bool background = false)
    {
        var typeName = GetType(type);
        if (background)
        {
            typeName += "\\Background";
        }
        var menu = $"{typeName}\\shell";

        try
        {
            using var regMenu = Registry.ClassesRoot.OpenSubKey(menu, true);
            regMenu?.DeleteSubKeyTree("Toolkit Launcher", false);
            regMenu?.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.ToString());
        }
        finally
        {
            if (type is MyEnums.TargetType.Directory && background is false)
            {
                DeleteMenu(type, true);
            }
            DetectMenu((MyEnums.TargetType)listTarget.SelectedIndex);
        }
    }

    #endregion DeleteMenu

    #region Utility

    private void UpdateStatus()
    {
        var detected = _detected ? "Detected" : "Undetected";
        lbStatus.Text = $"Status: {detected} | Extended: {_extended}";
        chkExtended.Checked = _extended;
        if (!Checker.IsAdministrator()) return;
        btnRegis.Enabled = !_detected;
        btnDelete.Enabled = _detected;
    }

    private static string GetType(MyEnums.TargetType type)
    {
        return type switch
        {
            MyEnums.TargetType.Exe => "exefile",
            MyEnums.TargetType.Dll => "dllfile",
            MyEnums.TargetType.Directory => "Directory",
            MyEnums.TargetType.All => "*",
            _ => ""
        };
    }

    #endregion Utility
}