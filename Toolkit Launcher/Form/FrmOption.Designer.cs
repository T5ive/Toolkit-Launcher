namespace Toolkit_Launcher
{
    partial class FrmOption
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
            this.listTarget = new System.Windows.Forms.ListBox();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnRegis = new System.Windows.Forms.Button();
            this.chkExtended = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            this.listTarget.Location = new System.Drawing.Point(12, 12);
            this.listTarget.Name = "listTarget";
            this.listTarget.Size = new System.Drawing.Size(290, 64);
            this.listTarget.TabIndex = 2;
            this.listTarget.SelectedIndexChanged += new System.EventHandler(this.listTarget_SelectedIndexChanged);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(12, 90);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(196, 15);
            this.lbStatus.TabIndex = 6;
            this.lbStatus.Text = "Status: Undetected | Extended: False\r\n";
            // 
            // btnRegis
            // 
            this.btnRegis.Enabled = false;
            this.btnRegis.Location = new System.Drawing.Point(12, 113);
            this.btnRegis.Name = "btnRegis";
            this.btnRegis.Size = new System.Drawing.Size(209, 23);
            this.btnRegis.TabIndex = 7;
            this.btnRegis.Text = "Add to Content Menu";
            this.btnRegis.UseVisualStyleBackColor = true;
            this.btnRegis.Click += new System.EventHandler(this.btnRegis_Click);
            // 
            // chkExtended
            // 
            this.chkExtended.AutoSize = true;
            this.chkExtended.Location = new System.Drawing.Point(227, 89);
            this.chkExtended.Name = "chkExtended";
            this.chkExtended.Size = new System.Drawing.Size(75, 19);
            this.chkExtended.TabIndex = 8;
            this.chkExtended.Text = "Extended";
            this.chkExtended.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(227, 113);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // FrmOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 148);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.chkExtended);
            this.Controls.Add(this.btnRegis);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.listTarget);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Option";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmOption_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ListBox listTarget;
        private Label lbStatus;
        private Button btnRegis;
        private CheckBox chkExtended;
        private Button btnDelete;
    }
}