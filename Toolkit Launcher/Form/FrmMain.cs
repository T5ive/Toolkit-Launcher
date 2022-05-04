namespace Toolkit_Launcher;

public partial class FrmMain : Form
{
    public bool Leaver = true;
    private static string[] _args = { };
    private static MyEnums.TargetType _targetType;

    public FrmMain(string[] args)
    {
        if (args == null) throw new ArgumentNullException(nameof(args));
        InitializeComponent();
#if DEBUG
        args = new[]
        {
            @"D:\Visual Studio 2022\C#\Toolkit Launcher\Toolkit Launcher\bin\Debug\net6.0-windows\Toolkit Launcher.dll"
        };
#endif

        if (args is null || args.Length == 0) return;

        _args = args;
        _targetType = _args[0].GetTarget();
    }

    #region Load

    private void FrmMain_Load(object sender, EventArgs e)
    {
        PublicClass.ImageList = imageList1;
        InitializeList();
        AddItems();
        AddSetting();
        AddOption();
        contextMenuStrip1.Show(Cursor.Position);
    }

    private static void InitializeList()
    {
        if (!File.Exists(PublicClass.PATH)) return;
        LoadFile(PublicClass.PATH);
    }

    private static void LoadFile(string fileName)
    {
        try
        {
            PublicClass.ListInfo = LoadJson<ListInfo>(fileName);
        }
        catch (Exception ex)
        {
            MyMessage.ShowError(ex.Message);
        }
        finally
        {
            PublicClass.TempListInfo = PublicClass.ListInfo;
        }
    }

    private static T LoadJson<T>(string fileName)
    {
        if (fileName.IsEmpty()) return default!;

        var objectOut = default(T);

        try
        {
            objectOut = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
        }
        catch (Exception ex)
        {
            MyMessage.ShowError(ex.Message);
        }

        return objectOut!;
    }

    private void AddItems()
    {
        if (PublicClass.ListInfo == null!) return;
        AddAll(contextMenuStrip1, PublicClass.ListInfo);
    }

    private void AddSetting()
    {
        if (contextMenuStrip1.Items.Count > 0)
        {
            contextMenuStrip1.Items.Add(new ToolStripSeparator());
        }
        var option = contextMenuStrip1.Items.Add("Items Setting");
        option.Click += (_, _) =>
        {
            Leaver = false;
            contextMenuStrip1.Hide();
            var frmSetting = new FrmSetting();
            frmSetting.ShowDialog();
            frmSetting.Dispose();
            Application.Exit();
        };
    }

    private void AddOption()
    {
        var option = contextMenuStrip1.Items.Add("Option");
        option.Click += (_, _) =>
        {
            Leaver = false;
            contextMenuStrip1.Hide();
            var frmOption = new FrmOption();
            frmOption.ShowDialog();
            frmOption.Dispose();
            Application.Exit();
        };
    }

    #endregion Load

    #region Add Group Items

    private void AddAll(ToolStrip menu, ListInfo listInfo)
    {
        foreach (var group in listInfo.ListGroup.Where(group => _targetType.IsTarget(group.Advance.TargetTypes)))
        {
            switch (group.Type)
            {
                case MyEnums.GroupType.Header when group.ListGroup.Count <= 0:
                    continue;
                case MyEnums.GroupType.Header:
                    {
                        var item = new ToolStripMenuItem(group.Identify.Name);
                        var subItem = AddAll(group);
                        if (subItem != null!)
                        {
                            item.DropDownItems.AddRange(subItem.ToArray());
                        }

                        menu.Items.Add(item);
                        break;
                    }
                case MyEnums.GroupType.Separator:
                    menu.Items.Add(new ToolStripSeparator());
                    break;

                case MyEnums.GroupType.Item:
                case MyEnums.GroupType.Null:
                default:
                    {
                        if (string.IsNullOrWhiteSpace(group.Value.FilePath)) continue;
                        var image = group.Value.IconPath.GetImage();
                        if (image == null!)
                        {
                            image = group.Type.GetImage();
                            menu.Items.Add(group.Type.ToString(), image);
                            return;
                        }

                        var result = new ToolStripMenuItem(group.Identify.Name, image);
                        result.Click += (_, _) =>
                        {
                            Leaver = false;
                            Process.Start(group.Value.FilePath, _args);
                            Application.Exit();
                        };
                        menu.Items.Add(result);
                        break;
                    }
            }
        }
    }

    private List<ToolStripItem> AddAll(GroupInfo groupInfo)
    {
        var result = new List<ToolStripItem>();
        foreach (var group in groupInfo.ListGroup.Where(group => _targetType.IsTarget(group.Advance.TargetTypes)))
        {
            switch (group.Type)
            {
                case MyEnums.GroupType.Header when group.ListGroup.Count <= 0:
                    continue;
                case MyEnums.GroupType.Header:
                    {
                        var item = new ToolStripMenuItem(group.Identify.Name);
                        var subItem = AddAll(group);
                        if (subItem != null!)
                        {
                            item.DropDownItems.AddRange(subItem.ToArray());
                        }

                        result.Add(item);
                        break;
                    }
                case MyEnums.GroupType.Separator:
                    return new List<ToolStripItem>();

                case MyEnums.GroupType.Item:
                case MyEnums.GroupType.Null:
                default:
                    {
                        if (string.IsNullOrWhiteSpace(group.Value.FilePath)) continue;
                        var image = group.Value.IconPath.GetImage();
                        if (image == null!)
                        {
                            image = group.Type.GetImage();
                        }

                        var item = new ToolStripMenuItem(group.Identify.Name, image);
                        item.Click += (_, _) =>
                        {
                            Leaver = false;
                            Process.Start(group.Value.FilePath, _args);
                            Application.Exit();
                        };
                        result.Add(item);
                        break;
                    }
            }
        }

        return result;
    }

    #endregion Add Group Items

    private void FrmMain_Deactivate(object sender, EventArgs e)
    {
        if (!Leaver) return;
        Application.Exit();
    }
}