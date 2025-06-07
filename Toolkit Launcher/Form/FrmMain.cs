using Toolkit_Launcher.Models;
using Toolkit_Launcher.Services;
namespace Toolkit_Launcher;

public partial class FrmMain : Form
    private readonly SettingsService _settingsService;
    private readonly ImageCacheService _imageCacheService;
{
    public bool Leaver = true;
    private static string[] _args = { };
    private static MyEnums.TargetType _targetType;

    public FrmMain(string[] args, SettingsService settingsService, ImageCacheService imageCacheService)
    {
        if (args == null) throw new ArgumentNullException(nameof(args));
        InitializeComponent();
        _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        _imageCacheService = imageCacheService ?? throw new ArgumentNullException(nameof(imageCacheService));
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
        // PublicClass.ImageList = imageList1; // Removed: ImageCacheService now handles images
        // InitializeList(); // Removed: SettingsService handles loading
        AddItems();
        AddSetting();
        AddOption();
        contextMenuStrip1.Show(Cursor.Position);
    }

    // Removed InitializeList, LoadFile, LoadJson as SettingsService handles this

    private void AddItems()
    {
        if (_settingsService.CurrentSettings == null!) return; // Use SettingsService
        contextMenuStrip1.SuspendLayout();
        AddAll(contextMenuStrip1, _settingsService.CurrentSettings); // Use SettingsService
        contextMenuStrip1.ResumeLayout(false);
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
            var frmSetting = new FrmSetting(_settingsService, _imageCacheService);
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
                        var image = _imageCacheService.GetImage(group.Value.IconPath); // Use ImageCacheService
                        if (image == null || image == _imageCacheService.GetImage(MyEnums.GroupType.Null)) // Check against default null image
                        {
                            image = _imageCacheService.GetImage(group.Type); // Use ImageCacheService
                            // menu.Items.Add(group.Type.ToString(), image); // This line seems to be an early exit, might need review
                            // return; // This line seems to be an early exit, might need review
                        }
                        // Ensure image is not null before creating ToolStripMenuItem, or handle it
                        if (image == null) image = _imageCacheService.GetImage(MyEnums.GroupType.Null);


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
                        var image = _imageCacheService.GetImage(group.Value.IconPath); // Use ImageCacheService
                        if (image == null || image == _imageCacheService.GetImage(MyEnums.GroupType.Null)) // Check against default null image
                        {
                            image = _imageCacheService.GetImage(group.Type); // Use ImageCacheService
                        }
                        // Ensure image is not null before creating ToolStripMenuItem, or handle it
                        if (image == null) image = _imageCacheService.GetImage(MyEnums.GroupType.Null);

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
