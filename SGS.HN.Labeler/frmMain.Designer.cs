namespace SGS.HN.Labeler
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            tabMain = new TabControl();
            tabPrint = new TabPage();
            tabConfig = new TabPage();
            statusBarMain = new StatusStrip();
            progressBarMain = new ProgressBar();
            dgvConfig = new DataGridView();
            IsDeafult = new DataGridViewCheckBoxColumn();
            ConfigName = new DataGridViewTextBoxColumn();
            ConfigPath = new DataGridViewTextBoxColumn();
            btnImport = new Button();
            btnSetToDefault = new Button();
            btnDelete = new Button();
            tabMain.SuspendLayout();
            tabPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConfig).BeginInit();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabMain.Controls.Add(tabPrint);
            tabMain.Controls.Add(tabConfig);
            tabMain.Location = new Point(0, 6);
            tabMain.Margin = new Padding(0, 3, 0, 3);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(524, 470);
            tabMain.TabIndex = 0;
            // 
            // tabPrint
            // 
            tabPrint.Controls.Add(btnDelete);
            tabPrint.Controls.Add(btnSetToDefault);
            tabPrint.Controls.Add(btnImport);
            tabPrint.Controls.Add(dgvConfig);
            tabPrint.Location = new Point(4, 24);
            tabPrint.Name = "tabPrint";
            tabPrint.Padding = new Padding(10);
            tabPrint.Size = new Size(516, 442);
            tabPrint.TabIndex = 0;
            tabPrint.Text = "標籤列印";
            tabPrint.UseVisualStyleBackColor = true;
            // 
            // tabConfig
            // 
            tabConfig.Location = new Point(4, 24);
            tabConfig.Name = "tabConfig";
            tabConfig.Padding = new Padding(3);
            tabConfig.Size = new Size(500, 528);
            tabConfig.TabIndex = 1;
            tabConfig.Text = "設定檔管理";
            tabConfig.UseVisualStyleBackColor = true;
            // 
            // statusBarMain
            // 
            statusBarMain.Location = new Point(0, 479);
            statusBarMain.Name = "statusBarMain";
            statusBarMain.Size = new Size(524, 22);
            statusBarMain.TabIndex = 1;
            statusBarMain.Text = "statusStrip1";
            // 
            // progressBarMain
            // 
            progressBarMain.Dock = DockStyle.Top;
            progressBarMain.Location = new Point(0, 0);
            progressBarMain.Margin = new Padding(0);
            progressBarMain.Name = "progressBarMain";
            progressBarMain.Size = new Size(524, 3);
            progressBarMain.TabIndex = 2;
            progressBarMain.Value = 100;
            // 
            // dgvConfig
            // 
            dgvConfig.AllowUserToAddRows = false;
            dgvConfig.AllowUserToDeleteRows = false;
            dgvConfig.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Microsoft JhengHei UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvConfig.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvConfig.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvConfig.Columns.AddRange(new DataGridViewColumn[] { IsDeafult, ConfigName, ConfigPath });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Microsoft JhengHei UI", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvConfig.DefaultCellStyle = dataGridViewCellStyle5;
            dgvConfig.Location = new Point(13, 13);
            dgvConfig.Margin = new Padding(3, 3, 3, 10);
            dgvConfig.Name = "dgvConfig";
            dgvConfig.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Microsoft JhengHei UI", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgvConfig.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvConfig.RowHeadersVisible = false;
            dgvConfig.Size = new Size(490, 380);
            dgvConfig.TabIndex = 1;
            // 
            // IsDeafult
            // 
            IsDeafult.HeaderText = "";
            IsDeafult.Name = "IsDeafult";
            IsDeafult.ReadOnly = true;
            IsDeafult.Width = 30;
            // 
            // ConfigName
            // 
            ConfigName.HeaderText = "Name";
            ConfigName.Name = "ConfigName";
            ConfigName.ReadOnly = true;
            // 
            // ConfigPath
            // 
            ConfigPath.HeaderText = "Path";
            ConfigPath.Name = "ConfigPath";
            ConfigPath.ReadOnly = true;
            ConfigPath.Visible = false;
            // 
            // btnImport
            // 
            btnImport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnImport.Location = new Point(428, 406);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(75, 23);
            btnImport.TabIndex = 2;
            btnImport.Text = "匯入";
            btnImport.UseVisualStyleBackColor = true;
            // 
            // btnSetToDefault
            // 
            btnSetToDefault.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSetToDefault.Location = new Point(347, 406);
            btnSetToDefault.Name = "btnSetToDefault";
            btnSetToDefault.Size = new Size(75, 23);
            btnSetToDefault.TabIndex = 3;
            btnSetToDefault.Text = "設定為預設";
            btnSetToDefault.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDelete.Location = new Point(266, 406);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "刪除";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(524, 501);
            Controls.Add(progressBarMain);
            Controls.Add(statusBarMain);
            Controls.Add(tabMain);
            Font = new Font("Microsoft JhengHei UI", 9F);
            MinimumSize = new Size(540, 540);
            Name = "frmMain";
            Text = "Main";
            tabMain.ResumeLayout(false);
            tabPrint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvConfig).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabMain;
        private TabPage tabPrint;
        private TabPage tabConfig;
        private StatusStrip statusBarMain;
        private ProgressBar progressBarMain;
        private DataGridView dgvConfig;
        private Button btnDelete;
        private Button btnSetToDefault;
        private Button btnImport;
        private DataGridViewCheckBoxColumn IsDeafult;
        private DataGridViewTextBoxColumn ConfigName;
        private DataGridViewTextBoxColumn ConfigPath;
    }
}
