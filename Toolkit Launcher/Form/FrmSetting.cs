using Toolkit_Launcher.UI.Helpers;
using Toolkit_Launcher.Models;
namespace Toolkit_Launcher; // Keep namespace for Form
using Toolkit_Launcher.Services;
using Toolkit_Launcher.ItemManager; // The class, not the old Helper namespace
using Toolkit_Launcher.Utilities; // For TreeViewHelper and MyMessage
using System;
using System.Collections.Generic;
using System.IO; // For Path
using System.Linq; // For Linq extensions
using System.Windows.Forms;


public partial class FrmSetting : Form
{
    #region Variable
    private bool _editor; // To prevent event-driven updates during programmatic changes
    private readonly SettingsService _settingsService;
    private readonly ImageCacheService _imageCacheService;
    private readonly ItemsManager _itemsManager; // Instance of ItemsManager
    #endregion

    public FrmSetting(SettingsService settingsService, ImageCacheService imageCacheService)
    {
        InitializeComponent();
        _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        _imageCacheService = imageCacheService ?? throw new ArgumentNullException(nameof(imageCacheService));

        _settingsService.ReloadTempSettings();
        _itemsManager = new ItemsManager(_settingsService.TempSettings);

        _itemsManager.ItemsChanged += ItemsManager_ItemsChanged;

        // Setup ImageList for the TreeView
        // The ImageList from the designer (imageList1) can be used if its properties (ColorDepth, ImageSize) are suitable.
        // Or, create a new one if more control is needed. Let's use the designer's imageList1.
        // Ensure it's cleared if it persists images from designer.
        imageList1.Images.Clear();
        treeOption.ImageList = imageList1;
    }

    private void ItemsManager_ItemsChanged(object sender, EventArgs e)
    {
        string previouslySelectedPath = treeOption.SelectedNode?.Tag as string;
        TreeViewHelper.LoadAll(treeOption, _settingsService.TempSettings, _imageCacheService);
        if (previouslySelectedPath != null)
        {
            TreeViewHelper.SelectNodeByPath(treeOption, previouslySelectedPath);
        }
    }

    #region Load
    private void FrmSetting_Load(object sender, EventArgs e)
    {
        if (_settingsService.TempSettings == null)
        {
            // Consider initializing TempSettings with a default ListInfo if null
             _settingsService.TempSettings = new ListInfo(); // Ensure it's not null
            _itemsManager = new ItemsManager(_settingsService.TempSettings); // Re-assign if TempSettings was null
            _itemsManager.ItemsChanged += ItemsManager_ItemsChanged; // Re-subscribe
        }
        TreeViewHelper.LoadAll(treeOption, _settingsService.TempSettings, _imageCacheService);
    }
    #endregion

    #region Save
    private void btnSaveAll_Click(object sender, EventArgs e)
    {
        SaveFile();
    }

    private void SaveFile()
    {
        if (_settingsService.TempSettings.ListGroup == null || !_settingsService.TempSettings.ListGroup.Any())
        {
            // MyMessage.ShowWarning("List is Empty, Please Check it again!!!");
            // return;
            // Allow saving an empty list if user cleared everything.
        }
        if (!MyMessage.OkCancel("Save Settings.\n\nClick \"OK\" to confirm.\n\nClick \"Cancel\" to cancel."))
        {
            return;
        }
        _settingsService.SaveSettings();
        Application.Exit();
    }
    #endregion

    #region Add Items
    private void HandleAddItem(MyEnums.GroupType type)
    {
        string parentPath = null;
        if (treeOption.SelectedNode != null) {
            var selectedGroupType = _itemsManager.GetGroupType(treeOption.SelectedNode.Tag as string);
            if (selectedGroupType == MyEnums.GroupType.Header) {
                 parentPath = treeOption.SelectedNode.Tag as string;
            } else {
                // If selected is not a header, add to its parent, or root if no parent
                 var parentNode = treeOption.SelectedNode.Parent;
                 if (parentNode != null) {
                     parentPath = parentNode.Tag as string;
                 }
            }
        }

        GroupInfo newGroup = _itemsManager.AddGroup(type, parentPath);
        // TreeViewHelper.LoadAll is called by ItemsManager_ItemsChanged event.
        // Select the new node:
        TreeViewHelper.SelectNodeByPath(treeOption, newGroup.Identify.FullPath);
        treeOption.Focus();
        // If it's not a separator, allow immediate rename
        if (type != MyEnums.GroupType.Separator) {
            treeOption.SelectedNode?.BeginEdit();
        }
    }

    private void btnAddHeader_Click(object sender, EventArgs e) => HandleAddItem(MyEnums.GroupType.Header);
    private void btnAddSeparator_Click(object sender, EventArgs e) => HandleAddItem(MyEnums.GroupType.Separator);
    private void btnAddItem_Click(object sender, EventArgs e) => HandleAddItem(MyEnums.GroupType.Item);
    #endregion

    #region TreeView Selected
    private void treeOption_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (SelectedNull() || e.Node == null || e.Node.Tag == null) {
            EnableType(MyEnums.GroupType.Null); // Treat as no valid selection
            ClearDetail();
            return;
        }
        var selectedPath = e.Node.Tag as string;
        var type = _itemsManager.GetGroupType(selectedPath);
        GetDetail(selectedPath, type);
        EnableType(type);
    }

    private void EnableType(MyEnums.GroupType type)
    {
        gbDetail.Enabled = type == MyEnums.GroupType.Item || type == MyEnums.GroupType.Header;
        panelAdd.Enabled = true; // Always allow adding (to root or as child of header)
        addToolStripMenuItem.Enabled = true;

        renameToolStripMenuItem.Enabled = type == MyEnums.GroupType.Item || type == MyEnums.GroupType.Header;
        btnPath.Enabled = type == MyEnums.GroupType.Item;

        // Context menu add items should be enabled based on what can be added
        // e.g. cannot add a header inside an item.
        bool canBeParent = type == MyEnums.GroupType.Header || type == MyEnums.GroupType.Null;
        headerToolStripMenuItem.Enabled = canBeParent;
        itemToolStripMenuItem.Enabled = canBeParent;
        separatorToolStripMenuItem.Enabled = canBeParent;
    }
    #endregion

    #region Get Detail
    private void GetDetail(string itemPath, MyEnums.GroupType type)
    {
        _editor = true; // Prevent SaveTemp during population
        if (type == MyEnums.GroupType.Separator || type == MyEnums.GroupType.Null)
        {
            ClearDetail();
            _editor = false;
            return;
        }

        var detail = _itemsManager.GetGroupInfoByPath(itemPath);
        if (detail == null)
        {
            ClearDetail();
             _editor = false;
            return;
        }

        txtName.Text = detail.Identify.Name;
        txtPath.Text = detail.Value.FilePath;
        txtIcon.Text = detail.Value.IconPath;
        txtCommand.Text = detail.Advance.Commands;
        chkAdmin.Checked = detail.Advance.Admin;
        chkCMD.Checked = detail.Advance.CMD;
        radC.Checked = detail.Advance.CmdType == MyEnums.CMDType.C;
        radK.Checked = detail.Advance.CmdType == MyEnums.CMDType.K;

        listTarget.ClearSelected();
        if (detail.Advance.TargetTypes != null)
        {
            for (var i = 0; i < detail.Advance.TargetTypes.Count && i < listTarget.Items.Count; i++)
            {
                if (detail.Advance.TargetTypes[i])
                {
                    listTarget.SetSelected(i, true);
                }
            }
        }
        _editor = false;
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
    #endregion

    #region Menu & MouseDown
    private void treeOption_MouseDown(object sender, MouseEventArgs e)
    {
        var hitNode = treeOption.GetNodeAt(e.X, e.Y);
        if (e.Button == MouseButtons.Right) // Only change selection on right click if a node is hit
        {
            if (hitNode != null)  treeOption.SelectedNode = hitNode;
        } else { // For left click, always update selection
             treeOption.SelectedNode = hitNode;
        }


        if (hitNode == null)
        {
            EnableType(MyEnums.GroupType.Null);
            ClearDetail();
            if (e.Button == MouseButtons.Right) MenuController(false, e);
            return;
        }
         if (e.Button == MouseButtons.Right) MenuController(true, e);
    }

    private void MenuController(bool nodeSelected, MouseEventArgs e)
    {
        // This method is for context menu population based on selection.
        // EnableType already handles some of this for the main panel.
        MyEnums.GroupType type = MyEnums.GroupType.Null;
        if (nodeSelected && treeOption.SelectedNode != null && treeOption.SelectedNode.Tag != null) {
            type = _itemsManager.GetGroupType(treeOption.SelectedNode.Tag as string);
        }

        removeToolStripMenuItem.Enabled = nodeSelected && type != MyEnums.GroupType.Null;
        renameToolStripMenuItem.Enabled = nodeSelected && (type == MyEnums.GroupType.Item || type == MyEnums.GroupType.Header);

        bool canBeParent = type == MyEnums.GroupType.Header || type == MyEnums.GroupType.Null; // Null means root
        addToolStripMenuItem.Enabled = canBeParent; // This is the main "Add" menu item
        headerToolStripMenuItem.Enabled = canBeParent;
        itemToolStripMenuItem.Enabled = canBeParent;
        separatorToolStripMenuItem.Enabled = canBeParent;

        // Move Up/Down logic
        moveUpToolStripMenuItem.Enabled = false;
        moveDownToolStripMenuItem.Enabled = false;
        if (nodeSelected && type != MyEnums.GroupType.Null) {
            var selectedNode = treeOption.SelectedNode;
            var parentCollection = selectedNode.Parent?.Nodes ?? treeOption.Nodes;
            int currentIndex = parentCollection.IndexOf(selectedNode);
            if (currentIndex > 0) moveUpToolStripMenuItem.Enabled = true;
            if (currentIndex < parentCollection.Count - 1) moveDownToolStripMenuItem.Enabled = true;
        }
    }
    #endregion

    #region Menu Button Actions
    private void treeOption_KeyDown(object sender, KeyEventArgs e)
    {
        if (SelectedNull() || treeOption.SelectedNode.Tag == null) return;
        var selectedPath = treeOption.SelectedNode.Tag as string;

        if (e.KeyCode == Keys.Delete)
        {
            if (MyMessage.OkCancel($"Press \"OK\" to confirm the deletion \"{treeOption.SelectedNode.Text}\"\nPress \"Cancel\" to confirm cancellation"))
            {
                _itemsManager.RemoveGroup(selectedPath);
            }
        }
        else if (e.KeyCode == Keys.F2 && renameToolStripMenuItem.Enabled)
        {
            treeOption.LabelEdit = true;
            treeOption.SelectedNode.BeginEdit();
        }
    }

    private void treeOption_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
        treeOption.LabelEdit = false; // Ensure LabelEdit is reset
        if (string.IsNullOrWhiteSpace(e.Label)) // Edit cancelled or empty
        {
            e.CancelEdit = true;
            return;
        }

        var originalPath = e.Node.Tag as string;
        if (string.IsNullOrEmpty(originalPath))
        {
            e.CancelEdit = true;
            return;
        }

        // Check if new name is same as old name (derived from path)
        var oldName = originalPath.Contains("\") ? originalPath.Substring(originalPath.LastIndexOf("\") + 1) : originalPath;
        if (e.Label == oldName) {
            e.CancelEdit = true; // No change
            return;
        }


        if (!_itemsManager.RenameGroup(originalPath, e.Label))
        {
            e.CancelEdit = true;
            MyMessage.ShowError("Failed to rename. Name might be a duplicate or invalid.");
        }
        else
        {
            // Success. ItemsManager_ItemsChanged will refresh.
            // After refresh, try to reselect the item with its new path.
            var parentPath = originalPath.Contains("\") ? originalPath.Substring(0, originalPath.LastIndexOf("\")) : "";
            var newPath = string.IsNullOrEmpty(parentPath) ? e.Label : $"{parentPath}\{e.Label}";
            // The event handler will call LoadAll, then try to reselect using the old path.
            // We need to ensure the selection is updated to the new path.
            // So, FrmSetting.ItemsManager_ItemsChanged needs to be smarter about selection after rename.
            // For now, the event handler reloads and tries to select original path, which won't exist.
            // A quick fix: update tag here, so that re-selection logic might find it IF name is path component.
            // e.Node.Tag = newPath; // This is tricky because tree is about to be rebuilt.
            // The ItemsChanged event handler should ideally handle selection restoration.
        }
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (SelectedNull() || treeOption.SelectedNode.Tag == null) return;
        var selectedPath = treeOption.SelectedNode.Tag as string;
        if (MyMessage.OkCancel($"Press \"OK\" to confirm the deletion \"{treeOption.SelectedNode.Text}\"\nPress \"Cancel\" to confirm cancellation"))
        {
            _itemsManager.RemoveGroup(selectedPath);
        }
    }

    private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (SelectedNull() || treeOption.SelectedNode.Tag == null) return;
        var selectedPath = treeOption.SelectedNode.Tag as string;
        _itemsManager.MoveGroup(selectedPath, MyEnums.MoveType.MoveUp);
    }

    private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (SelectedNull() || treeOption.SelectedNode.Tag == null) return;
        var selectedPath = treeOption.SelectedNode.Tag as string;
        _itemsManager.MoveGroup(selectedPath, MyEnums.MoveType.MoveDown);
    }

    private void ContextMenuAdd_Click(object sender, EventArgs e)
    {
        MyEnums.GroupType typeToAdd = MyEnums.GroupType.Item;
        if (sender == headerToolStripMenuItem) typeToAdd = MyEnums.GroupType.Header;
        else if (sender == separatorToolStripMenuItem) typeToAdd = MyEnums.GroupType.Separator;

        string parentPath = null;
        if(!SelectedNull() && treeOption.SelectedNode.Tag != null && _itemsManager.GetGroupType(treeOption.SelectedNode.Tag as string) == MyEnums.GroupType.Header)
        {
            parentPath = treeOption.SelectedNode.Tag as string;
        }
        HandleAddItem(typeToAdd); // Let HandleAddItem figure out parent based on current selection if needed
    }


    private void renameToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (SelectedNull() || !renameToolStripMenuItem.Enabled) return;
        treeOption.LabelEdit = true;
        treeOption.SelectedNode.BeginEdit();
    }
    #endregion

    #region Button Events (Detail Panel)
    private void btnName_Click(object sender, EventArgs e)
    {
        if (SelectedNull() || treeOption.SelectedNode.Tag == null) return;
        var selectedPath = treeOption.SelectedNode.Tag as string;
        if (string.IsNullOrWhiteSpace(txtName.Text)) {
            MyMessage.ShowError("Name cannot be empty.");
            return;
        }

        if (_itemsManager.RenameGroup(selectedPath, txtName.Text))
        {
            btnName.Text = "Change";
            // Selection path will change, ItemsChanged event handles tree refresh & re-selection attempt.
        }
        else
        {
            MyMessage.ShowError("Failed to rename. Name might be a duplicate or invalid.");
        }
    }

    private void btnPath_Click(object sender, EventArgs e)
    {
        if (SelectedNull() || treeOption.SelectedNode.Tag == null) return;
        var selectedPath = treeOption.SelectedNode.Tag as string;
        var group = _itemsManager.GetGroupInfoByPath(selectedPath);
        if (group == null || group.Type != MyEnums.GroupType.Item) return;

        string newFilePath = group.Value.FilePath;
        DialogResult choice = MyMessage.YesNoCancel("Select target type:\n" +
                                      "Yes = File\n" +
                                      "No = Directory\n" +
                                      "Cancel = Do Nothing");
        if (choice == DialogResult.Cancel) return;

        if (choice == DialogResult.Yes)
        {
            using (var openFile = new OpenFileDialog { Filter = "All files(*.*)|*.*", Title = "Select Target File", FileName = newFilePath ?? "" })
            {
                if (openFile.ShowDialog() != DialogResult.OK) return;
                newFilePath = openFile.FileName;
            }
        }
        else
        {
            using (var openFolder = new FolderBrowserDialog { ShowNewFolderButton = true, SelectedPath = newFilePath ?? "" })
            {
                if (openFolder.ShowDialog() != DialogResult.OK) return;
                newFilePath = openFolder.SelectedPath;
            }
        }

        txtPath.Text = newFilePath;
        string newIconPath = txtIcon.Text;
        if (string.IsNullOrWhiteSpace(newIconPath) || newIconPath == group.Value.FilePath) // If icon was same as old path or empty
        {
            newIconPath = newFilePath; // Update icon to new path
            txtIcon.Text = newIconPath;
        }
        _itemsManager.UpdateGroupValuePaths(selectedPath, newFilePath, newIconPath);
    }

    private void btnIcon_Click(object sender, EventArgs e)
    {
        if (SelectedNull() || treeOption.SelectedNode.Tag == null) return;
        var selectedPath = treeOption.SelectedNode.Tag as string;
        var group = _itemsManager.GetGroupInfoByPath(selectedPath);
        if (group == null) return;

        using (var openFile = new OpenFileDialog { Filter = "Image/Icon/Exe/Dll|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.ico;*.exe;*.dll|All files (*.*)|*.*", Title = "Select Icon File", FileName = group.Value.IconPath ?? "" })
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtIcon.Text = openFile.FileName;
                _itemsManager.UpdateGroupValuePaths(selectedPath, group.Value.FilePath, openFile.FileName);
            }
        }
    }

    private void btnCancel_Click(object sender, EventArgs e) {
        _settingsService.ReloadTempSettings(); // Revert any changes in TempSettings
        Application.Exit();
    }
    #endregion

    #region Utility & Speacial Events (UI State or Detail Panel changes)
    private bool SelectedNull() => treeOption.SelectedNode == null;

    private void txtName_TextChanged(object sender, EventArgs e)
    {
        if (SelectedNull() || treeOption.SelectedNode == null || treeOption.SelectedNode.Tag == null || string.IsNullOrWhiteSpace(txtName.Text)) return;
        btnName.Text = treeOption.SelectedNode.Text == txtName.Text ? "Change" : "Change*";
    }

    private void All_CheckedChanged(object sender, EventArgs e)
    {
        radC.Enabled = radK.Enabled = chkCMD.Checked;
        SaveTempAdvanceInfo();
    }

    private void txtCommand_TextChanged(object sender, EventArgs e) => SaveTempAdvanceInfo();
    private void listTarget_SelectedIndexChanged(object sender, EventArgs e) => SaveTempAdvanceInfo();

    private void SaveTempAdvanceInfo()
    {
        if (_editor || SelectedNull() || treeOption.SelectedNode.Tag == null) return;
        var selectedPath = treeOption.SelectedNode.Tag as string;
        if (string.IsNullOrEmpty(selectedPath)) return;

        var targetTypes = new List<bool>();
        for (int i = 0; i < listTarget.Items.Count; i++)
        {
            targetTypes.Add(listTarget.GetSelected(i));
        }
        var cmdType = radC.Checked ? MyEnums.CMDType.C : MyEnums.CMDType.K;
        _itemsManager.UpdateGroupAdvanceInfo(selectedPath, txtCommand.Text, targetTypes, chkAdmin.Checked, chkCMD.Checked, cmdType);
    }
    #endregion

    private void FrmSetting_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Clean up event subscriptions
        if (_itemsManager != null)
        {
            _itemsManager.ItemsChanged -= ItemsManager_ItemsChanged;
        }
    }
}
