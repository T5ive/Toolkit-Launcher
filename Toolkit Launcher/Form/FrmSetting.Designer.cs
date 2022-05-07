namespace Toolkit_Launcher
{
    partial class FrmSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetting));
            this.menuDetect = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.headerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelAdd = new System.Windows.Forms.Panel();
            this.btnAddSeparator = new System.Windows.Forms.Button();
            this.btnAddHeader = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.treeOption = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbDetail = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelCustom = new System.Windows.Forms.Panel();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelSpecialBot = new System.Windows.Forms.Panel();
            this.listTarget = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.radK = new System.Windows.Forms.RadioButton();
            this.radC = new System.Windows.Forms.RadioButton();
            this.chkCMD = new System.Windows.Forms.CheckBox();
            this.chkAdmin = new System.Windows.Forms.CheckBox();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.txtIcon = new System.Windows.Forms.TextBox();
            this.btnIcon = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPath = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnName = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.menuDetect.SuspendLayout();
            this.panelAdd.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbDetail.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelCustom.SuspendLayout();
            this.panelSpecialBot.SuspendLayout();
            this.panelDetail.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuDetect
            // 
            this.menuDetect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.toolStripSeparator2,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem});
            this.menuDetect.Name = "menuDetect";
            this.menuDetect.Size = new System.Drawing.Size(181, 142);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.headerToolStripMenuItem,
            this.separatorToolStripMenuItem,
            this.itemToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // headerToolStripMenuItem
            // 
            this.headerToolStripMenuItem.Name = "headerToolStripMenuItem";
            this.headerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.headerToolStripMenuItem.Text = "Header";
            this.headerToolStripMenuItem.Click += new System.EventHandler(this.headerToolStripMenuItem_Click);
            // 
            // separatorToolStripMenuItem
            // 
            this.separatorToolStripMenuItem.Name = "separatorToolStripMenuItem";
            this.separatorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.separatorToolStripMenuItem.Text = "Separator";
            this.separatorToolStripMenuItem.Click += new System.EventHandler(this.separatorToolStripMenuItem_Click);
            // 
            // itemToolStripMenuItem
            // 
            this.itemToolStripMenuItem.Name = "itemToolStripMenuItem";
            this.itemToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.itemToolStripMenuItem.Text = "Item";
            this.itemToolStripMenuItem.Click += new System.EventHandler(this.itemToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.moveUpToolStripMenuItem.Text = "Move Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.moveDownToolStripMenuItem.Text = "Move Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // panelAdd
            // 
            this.panelAdd.Controls.Add(this.btnAddSeparator);
            this.panelAdd.Controls.Add(this.btnAddHeader);
            this.panelAdd.Controls.Add(this.btnAddItem);
            this.panelAdd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAdd.Location = new System.Drawing.Point(0, 386);
            this.panelAdd.Name = "panelAdd";
            this.panelAdd.Size = new System.Drawing.Size(334, 40);
            this.panelAdd.TabIndex = 4;
            // 
            // btnAddSeparator
            // 
            this.btnAddSeparator.Location = new System.Drawing.Point(124, 6);
            this.btnAddSeparator.Name = "btnAddSeparator";
            this.btnAddSeparator.Size = new System.Drawing.Size(100, 23);
            this.btnAddSeparator.TabIndex = 6;
            this.btnAddSeparator.Text = "Add Separator";
            this.btnAddSeparator.UseVisualStyleBackColor = true;
            this.btnAddSeparator.Click += new System.EventHandler(this.btnAddSeparator_Click);
            // 
            // btnAddHeader
            // 
            this.btnAddHeader.Location = new System.Drawing.Point(12, 6);
            this.btnAddHeader.Name = "btnAddHeader";
            this.btnAddHeader.Size = new System.Drawing.Size(106, 23);
            this.btnAddHeader.TabIndex = 4;
            this.btnAddHeader.Text = "Add Header";
            this.btnAddHeader.UseVisualStyleBackColor = true;
            this.btnAddHeader.Click += new System.EventHandler(this.btnAddHeader_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(230, 6);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(100, 23);
            this.btnAddItem.TabIndex = 5;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // treeOption
            // 
            this.treeOption.ContextMenuStrip = this.menuDetect;
            this.treeOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeOption.HideSelection = false;
            this.treeOption.ImageIndex = 0;
            this.treeOption.ImageList = this.imageList1;
            this.treeOption.Location = new System.Drawing.Point(0, 0);
            this.treeOption.Name = "treeOption";
            this.treeOption.SelectedImageIndex = 0;
            this.treeOption.Size = new System.Drawing.Size(334, 386);
            this.treeOption.TabIndex = 6;
            this.treeOption.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeOption_AfterLabelEdit);
            this.treeOption.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeOption_AfterSelect);
            this.treeOption.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeOption_KeyDown);
            this.treeOption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeOption_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSaveAll);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 386);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(295, 40);
            this.panel2.TabIndex = 5;
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Location = new System.Drawing.Point(6, 6);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(195, 23);
            this.btnSaveAll.TabIndex = 7;
            this.btnSaveAll.Text = "Save All";
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(207, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbDetail
            // 
            this.gbDetail.Controls.Add(this.groupBox2);
            this.gbDetail.Controls.Add(this.panelDetail);
            this.gbDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetail.Enabled = false;
            this.gbDetail.Location = new System.Drawing.Point(0, 0);
            this.gbDetail.Name = "gbDetail";
            this.gbDetail.Size = new System.Drawing.Size(295, 386);
            this.gbDetail.TabIndex = 7;
            this.gbDetail.TabStop = false;
            this.gbDetail.Text = "Detail";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelCustom);
            this.groupBox2.Controls.Add(this.panelSpecialBot);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 264);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Special";
            // 
            // panelCustom
            // 
            this.panelCustom.Controls.Add(this.txtCommand);
            this.panelCustom.Controls.Add(this.label4);
            this.panelCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCustom.Location = new System.Drawing.Point(3, 19);
            this.panelCustom.Name = "panelCustom";
            this.panelCustom.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.panelCustom.Size = new System.Drawing.Size(283, 118);
            this.panelCustom.TabIndex = 9;
            // 
            // txtCommand
            // 
            this.txtCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCommand.Location = new System.Drawing.Point(6, 15);
            this.txtCommand.Multiline = true;
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(271, 103);
            this.txtCommand.TabIndex = 1;
            this.txtCommand.TextChanged += new System.EventHandler(this.txtCommand_TextChanged);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(271, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Custom Command: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelSpecialBot
            // 
            this.panelSpecialBot.Controls.Add(this.listTarget);
            this.panelSpecialBot.Controls.Add(this.label5);
            this.panelSpecialBot.Controls.Add(this.radK);
            this.panelSpecialBot.Controls.Add(this.radC);
            this.panelSpecialBot.Controls.Add(this.chkCMD);
            this.panelSpecialBot.Controls.Add(this.chkAdmin);
            this.panelSpecialBot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSpecialBot.Location = new System.Drawing.Point(3, 137);
            this.panelSpecialBot.Name = "panelSpecialBot";
            this.panelSpecialBot.Size = new System.Drawing.Size(283, 124);
            this.panelSpecialBot.TabIndex = 10;
            // 
            // listTarget
            // 
            this.listTarget.FormattingEnabled = true;
            this.listTarget.ItemHeight = 15;
            this.listTarget.Items.AddRange(new object[] {
            "exe",
            "dll",
            "Directory",
            "*.*"});
            this.listTarget.Location = new System.Drawing.Point(88, 6);
            this.listTarget.Name = "listTarget";
            this.listTarget.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listTarget.Size = new System.Drawing.Size(189, 64);
            this.listTarget.TabIndex = 14;
            this.listTarget.SelectedIndexChanged += new System.EventHandler(this.listTarget_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Target Type: ";
            // 
            // radK
            // 
            this.radK.AutoSize = true;
            this.radK.Enabled = false;
            this.radK.Location = new System.Drawing.Point(161, 93);
            this.radK.Name = "radK";
            this.radK.Size = new System.Drawing.Size(37, 19);
            this.radK.TabIndex = 12;
            this.radK.Text = "/K";
            this.radK.UseVisualStyleBackColor = true;
            this.radK.CheckedChanged += new System.EventHandler(this.All_CheckedChanged);
            // 
            // radC
            // 
            this.radC.AutoSize = true;
            this.radC.Checked = true;
            this.radC.Enabled = false;
            this.radC.Location = new System.Drawing.Point(117, 93);
            this.radC.Name = "radC";
            this.radC.Size = new System.Drawing.Size(38, 19);
            this.radC.TabIndex = 11;
            this.radC.TabStop = true;
            this.radC.Text = "/C";
            this.radC.UseVisualStyleBackColor = true;
            this.radC.CheckedChanged += new System.EventHandler(this.All_CheckedChanged);
            // 
            // chkCMD
            // 
            this.chkCMD.AutoSize = true;
            this.chkCMD.Location = new System.Drawing.Point(8, 94);
            this.chkCMD.Name = "chkCMD";
            this.chkCMD.Size = new System.Drawing.Size(103, 19);
            this.chkCMD.TabIndex = 10;
            this.chkCMD.Text = "Run with CMD";
            this.chkCMD.UseVisualStyleBackColor = true;
            this.chkCMD.CheckedChanged += new System.EventHandler(this.All_CheckedChanged);
            // 
            // chkAdmin
            // 
            this.chkAdmin.AutoSize = true;
            this.chkAdmin.Location = new System.Drawing.Point(8, 69);
            this.chkAdmin.Name = "chkAdmin";
            this.chkAdmin.Size = new System.Drawing.Size(137, 19);
            this.chkAdmin.TabIndex = 9;
            this.chkAdmin.Text = "Run as Administrator";
            this.chkAdmin.UseVisualStyleBackColor = true;
            this.chkAdmin.CheckedChanged += new System.EventHandler(this.All_CheckedChanged);
            // 
            // panelDetail
            // 
            this.panelDetail.Controls.Add(this.txtIcon);
            this.panelDetail.Controls.Add(this.btnIcon);
            this.panelDetail.Controls.Add(this.label1);
            this.panelDetail.Controls.Add(this.btnPath);
            this.panelDetail.Controls.Add(this.txtName);
            this.panelDetail.Controls.Add(this.btnName);
            this.panelDetail.Controls.Add(this.txtPath);
            this.panelDetail.Controls.Add(this.label2);
            this.panelDetail.Controls.Add(this.label3);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetail.Location = new System.Drawing.Point(3, 19);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(289, 100);
            this.panelDetail.TabIndex = 10;
            // 
            // txtIcon
            // 
            this.txtIcon.Location = new System.Drawing.Point(62, 62);
            this.txtIcon.Name = "txtIcon";
            this.txtIcon.ReadOnly = true;
            this.txtIcon.Size = new System.Drawing.Size(189, 23);
            this.txtIcon.TabIndex = 5;
            // 
            // btnIcon
            // 
            this.btnIcon.Location = new System.Drawing.Point(257, 62);
            this.btnIcon.Name = "btnIcon";
            this.btnIcon.Size = new System.Drawing.Size(30, 23);
            this.btnIcon.TabIndex = 8;
            this.btnIcon.Text = "...";
            this.btnIcon.UseVisualStyleBackColor = true;
            this.btnIcon.Click += new System.EventHandler(this.btnIcon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name: ";
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(257, 32);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(30, 23);
            this.btnPath.TabIndex = 7;
            this.btnPath.Text = "...";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(62, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 23);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // btnName
            // 
            this.btnName.Location = new System.Drawing.Point(217, 4);
            this.btnName.Name = "btnName";
            this.btnName.Size = new System.Drawing.Size(69, 23);
            this.btnName.TabIndex = 6;
            this.btnName.Text = "Change";
            this.btnName.UseVisualStyleBackColor = true;
            this.btnName.Click += new System.EventHandler(this.btnName_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(62, 33);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(189, 23);
            this.txtPath.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Path: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Icon: ";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.treeOption);
            this.panelLeft.Controls.Add(this.panelAdd);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(334, 426);
            this.panelLeft.TabIndex = 8;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.gbDetail);
            this.panelRight.Controls.Add(this.panel2);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(334, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(295, 426);
            this.panelRight.TabIndex = 9;
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 426);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSetting";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            this.menuDetect.ResumeLayout(false);
            this.panelAdd.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gbDetail.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panelCustom.ResumeLayout(false);
            this.panelCustom.PerformLayout();
            this.panelSpecialBot.ResumeLayout(false);
            this.panelSpecialBot.PerformLayout();
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ContextMenuStrip menuDetect;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem moveUpToolStripMenuItem;
        private ToolStripMenuItem moveDownToolStripMenuItem;
        private Panel panelAdd;
        private Button btnAddHeader;
        private Button btnAddItem;
        private TreeView treeOption;
        private Panel panel2;
        private Button btnSaveAll;
        private Button btnCancel;
        private GroupBox gbDetail;
        private Button btnIcon;
        private Button btnPath;
        private Button btnName;
        private TextBox txtIcon;
        private Label label3;
        private Label label2;
        private TextBox txtPath;
        private TextBox txtName;
        private Label label1;
        private Panel panelLeft;
        private Panel panelRight;
        private GroupBox groupBox2;
        private TextBox txtCommand;
        private Label label4;
        private Button btnAddSeparator;
        private Panel panelCustom;
        private Panel panelSpecialBot;
        private ListBox listTarget;
        private Label label5;
        private RadioButton radK;
        private RadioButton radC;
        private CheckBox chkCMD;
        private CheckBox chkAdmin;
        private Panel panelDetail;
        private ImageList imageList1;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem headerToolStripMenuItem;
        private ToolStripMenuItem separatorToolStripMenuItem;
        private ToolStripMenuItem itemToolStripMenuItem;
    }
}