namespace Toolkit_Launcher;

public partial class FrmSetting : Form
{
    public FrmSetting()
    {
        InitializeComponent();
        imageList1 = PublicClass.ImageList;
        treeOption.ImageList = imageList1;
    }

    #region Load

    private void FrmSetting_Load(object sender, EventArgs e)
    {
        if (PublicClass.TempListInfo == null!) return;
        Utility.LoadAll(treeOption, PublicClass.TempListInfo);
    }

    #endregion Load

    #region Save

    private void btnSaveAll_Click(object sender, EventArgs e)
    {
        SaveFile();
    }

    private static void SaveFile()
    {
        if (PublicClass.TempListInfo.IsEmpty())
        {
            MyMessage.ShowWarning("List is Empty, Please Check it again!!!");
            return;
        }

        if (!MyMessage.OkCancel("Save Settings.\n\n" +
                                "Click \"OK\" to confirm.\n\n" +
                                "Click \"Cancel\" to cancel.")) return;
        var save = SaveJson(PublicClass.TempListInfo, PublicClass.PATH);
        if (!save.Item1)
            MyMessage.ShowError(save.message);
        Application.Exit();
    }

    private static (bool, string message) SaveJson<T>(T serializableObject, string fileName)
    {
        if (serializableObject == null) return (false, "incorrect settings.json");

        try
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(serializableObject, Formatting.Indented));
            return (true, "");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    #endregion Save

    #region Add Items

    private void btnAddHeader_Click(object sender, EventArgs e)
    {
        ItemsManager.AddGroup(MyEnums.GroupType.Header, treeOption);
    }

    private void btnAddSeparator_Click(object sender, EventArgs e)
    {
        ItemsManager.AddGroup(MyEnums.GroupType.Separator, treeOption);
    }

    private void btnAddItem_Click(object sender, EventArgs e)
    {
        ItemsManager.AddGroup(MyEnums.GroupType.Item, treeOption);
    }

    #endregion Add Items

    #region TreeView Selected

    private void treeOption_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (SelectedNull()) return;
        var selected = treeOption.SelectedNode;
        var type = ItemsManager.GetType(selected.FullPath);
        GetDetail(type);
        EnableType(type);
    }

    private void EnableType(MyEnums.GroupType type)
    {
        switch (type)
        {
            case MyEnums.GroupType.Item:
                gbDetail.Enabled = true;
                panelAdd.Enabled = false;
                break;

            case MyEnums.GroupType.Separator:
                gbDetail.Enabled = false;
                panelAdd.Enabled = false;
                break;

            case MyEnums.GroupType.Header:
                gbDetail.Enabled = true;
                panelAdd.Enabled = true;
                break;

            default:
                gbDetail.Enabled = false;
                panelAdd.Enabled = true;
                break;
        }
    }

    #endregion TreeView Selected

    #region Get Detail

    private void GetDetail(MyEnums.GroupType type)
    {
        switch (type)
        {
            case MyEnums.GroupType.Separator:
                ClearDetail();
                break;

            default:
                GroupDetail();
                break;
        }
    }

    private void GroupDetail()
    {
        var detail = ItemsManager.GetGroupDetail(treeOption);
        txtName.Text = detail.Identify.Name;
        txtPath.Text = detail.Value.FilePath;
        txtIcon.Text = detail.Value.IconPath;
        txtCommand.Text = detail.Advance.Commands;
        chkAdmin.Checked = detail.Advance.Admin;
        chkCMD.Checked = detail.Advance.CMD;
        if (detail.Advance.CmdType == MyEnums.CMDType.C)
        {
            radC.Checked = true;
        }
        else
        {
            radK.Checked = true;
        }

        listTarget.ClearSelected();
        for (var i = 0; i < detail.Advance.TargetTypes.Count; i++)
        {
            if (detail.Advance.TargetTypes[i])
            {
                listTarget.SelectedIndex = i;
            }
        }

        btnPath.Enabled = detail.Type != MyEnums.GroupType.Header;
    }

    private void ClearDetail()
    {
        txtName.Clear();
        txtPath.Clear();
        txtIcon.Clear();
        txtCommand.Clear();
        chkAdmin.Checked = false;
        chkCMD.Checked = false;
        radK.Checked = false;
        radC.Checked = true;
        listTarget.ClearSelected();
    }

    #endregion Get Detail

    #region Menu

    private void treeOption_MouseDown(object sender, MouseEventArgs e)
    {
        var hit = treeOption.HitTest(e.X, e.Y);
        if (hit.Node == null)
        {
            treeOption.SelectedNode = null;
            MenuController(false, e);
            gbDetail.Enabled = true;
            panelAdd.Enabled = true;
            ClearDetail();
            EnableType(MyEnums.GroupType.Null);
            return;
        }
        treeOption.SelectedNode = hit.Node;
        MenuController(true, e);
    }

    private void MenuController(bool detected, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) return;
        removeToolStripMenuItem.Enabled = detected;
        moveUpToolStripMenuItem.Enabled = detected;
        moveDownToolStripMenuItem.Enabled = detected;
        if (SelectedNull()) return;
        int itemCount;
        if (treeOption.SelectedNode.Parent == null)
        {
            itemCount = treeOption.Nodes.Count - 1;
        }
        else
        {
            itemCount = treeOption.SelectedNode.Nodes.Count - 1;
        }

        if (treeOption.SelectedNode.Index == itemCount)
        {
            moveDownToolStripMenuItem.Enabled = false;
        }

        if (treeOption.SelectedNode.Index == 0)
        {
            moveUpToolStripMenuItem.Enabled = false;
        }
    }

    #endregion Menu

    #region Menu Button

    private void treeOption_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Delete)
        {
            ItemsManager.Remove(treeOption);
        }
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ItemsManager.Remove(treeOption);
    }

    private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ItemsManager.MoveUpDown(MyEnums.MoveType.MoveUp, treeOption);
    }

    private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ItemsManager.MoveUpDown(MyEnums.MoveType.MoveDown, treeOption);
    }

    #endregion Menu Button

    #region Button Events

    private void btnName_Click(object sender, EventArgs e)
    {
        ItemsManager.Rename(treeOption, txtName.Text);
    }

    private void btnPath_Click(object sender, EventArgs e)
    {
        if (MyMessage.YesNo("Press \"Yes\" to select target file\n" +
                            "Press \"No\" to select directory path") == DialogResult.Yes)
        {
            var openFile = new OpenFileDialog
            {
                Filter = "All files(*.*)|*.*",
                Title = Text
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFile.FileName;
                txtPath.Text = filePath;
                if (txtName.Text.StartsWith("Header: ") | txtName.Text.StartsWith("Item: ") && string.IsNullOrWhiteSpace(txtIcon.Text))
                {
                    var fileName = Path.GetFileNameWithoutExtension(filePath);
                    txtName.Text = fileName;
                    txtPath.Text = filePath;
                    txtIcon.Text = filePath;
                    ItemsManager.UpdateIdentify(treeOption, fileName, filePath, filePath);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtIcon.Text))
                {
                    txtIcon.Text = filePath;
                    ItemsManager.ChangeBothPath(treeOption, filePath, filePath);
                    return;
                }

                ItemsManager.ChangePath(treeOption, filePath);
            }
            return;
        }

        var openFolder = new FolderBrowserDialog
        {
            ShowNewFolderButton = true
        };

        if (openFolder.ShowDialog() == DialogResult.OK)
        {
            var folderPath = openFolder.SelectedPath;
            txtPath.Text = folderPath;
            ItemsManager.ChangePath(treeOption, folderPath);
        }
    }

    private void btnIcon_Click(object sender, EventArgs e)
    {
        var openFile = new OpenFileDialog
        {
            Filter = "All files(*.*)|*.*",
            Title = Text
        };

        if (openFile.ShowDialog() == DialogResult.OK)
        {
            var filePath = openFile.FileName;
            txtIcon.Text = filePath;
            ItemsManager.ChangeIconPath(treeOption, filePath);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    #endregion Button Events

    #region Utility

    private bool SelectedNull()
    {
        return treeOption.SelectedNode == null;
    }

    #endregion Utility

    #region Speacial Events

    private void txtName_TextChanged(object sender, EventArgs e)
    {
        btnName.Text = treeOption.SelectedNode.Text == txtName.Text ? "Change" : "Change*";
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var target = new List<bool>
        {
            true,
            true,
            true,
            false
        };

        for (var i = 0; i < listTarget.Items.Count; i++)
        {
            var selected = listTarget.SelectedIndices;
            if (selected.Contains(i))
            {
                target[i] = true;
            }
            else
            {
                target[i] = false;
            }
        }

        var type = radC.Checked ? MyEnums.CMDType.C : MyEnums.CMDType.K;

        ItemsManager.UpdateInfo(treeOption, txtCommand.Text, target, chkAdmin.Checked, chkCMD.Checked, type);
    }

    private void All_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCMD.Checked)
        {
            radC.Enabled = true;
            radK.Enabled = true;
        }
        else
        {
            radC.Enabled = false;
            radK.Enabled = false;
        }
    }

    #endregion Speacial Events

    
}