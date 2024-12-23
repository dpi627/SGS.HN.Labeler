﻿
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            tabMain = new TabControl();
            tabPrint = new TabPage();
            labPrinter = new Label();
            cbbPrinter = new ComboBox();
            txtOutputMessage = new TextBox();
            labAutoPrint = new Label();
            radPrintOnce = new RadioButton();
            radioButton1 = new RadioButton();
            btnPrint = new Button();
            txtOrderNoEnd = new TextBox();
            txtOrderNoStart = new TextBox();
            labOrderNo = new Label();
            cbbExcelConfig = new ComboBox();
            labConfig = new Label();
            tabConfig = new TabPage();
            btnDelete = new Button();
            btnSetToDefault = new Button();
            btnImport = new Button();
            dgvConfig = new DataGridView();
            IsDeafult = new DataGridViewCheckBoxColumn();
            ConfigName = new DataGridViewTextBoxColumn();
            ConfigPath = new DataGridViewTextBoxColumn();
            statusBarMain = new StatusStrip();
            progressBarMain = new ProgressBar();
            tabMain.SuspendLayout();
            tabPrint.SuspendLayout();
            tabConfig.SuspendLayout();
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
            tabPrint.Controls.Add(labPrinter);
            tabPrint.Controls.Add(cbbPrinter);
            tabPrint.Controls.Add(txtOutputMessage);
            tabPrint.Controls.Add(labAutoPrint);
            tabPrint.Controls.Add(radPrintOnce);
            tabPrint.Controls.Add(radioButton1);
            tabPrint.Controls.Add(btnPrint);
            tabPrint.Controls.Add(txtOrderNoEnd);
            tabPrint.Controls.Add(txtOrderNoStart);
            tabPrint.Controls.Add(labOrderNo);
            tabPrint.Controls.Add(cbbExcelConfig);
            tabPrint.Controls.Add(labConfig);
            tabPrint.Location = new Point(4, 24);
            tabPrint.Name = "tabPrint";
            tabPrint.Padding = new Padding(10);
            tabPrint.Size = new Size(516, 442);
            tabPrint.TabIndex = 1;
            tabPrint.Text = "標籤列印";
            tabPrint.UseVisualStyleBackColor = true;
            // 
            // labPrinter
            // 
            labPrinter.AutoSize = true;
            labPrinter.Location = new Point(59, 16);
            labPrinter.Name = "labPrinter";
            labPrinter.Size = new Size(43, 15);
            labPrinter.TabIndex = 12;
            labPrinter.Text = "印表機";
            // 
            // cbbPrinter
            // 
            cbbPrinter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbbPrinter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbPrinter.FormattingEnabled = true;
            cbbPrinter.Location = new Point(108, 13);
            cbbPrinter.Name = "cbbPrinter";
            cbbPrinter.Size = new Size(395, 23);
            cbbPrinter.TabIndex = 11;
            // 
            // txtOutputMessage
            // 
            txtOutputMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtOutputMessage.Font = new Font("Microsoft JhengHei UI", 8F);
            txtOutputMessage.Location = new Point(13, 129);
            txtOutputMessage.Multiline = true;
            txtOutputMessage.Name = "txtOutputMessage";
            txtOutputMessage.ReadOnly = true;
            txtOutputMessage.ScrollBars = ScrollBars.Vertical;
            txtOutputMessage.Size = new Size(490, 300);
            txtOutputMessage.TabIndex = 10;
            // 
            // labAutoPrint
            // 
            labAutoPrint.AutoSize = true;
            labAutoPrint.Location = new Point(47, 104);
            labAutoPrint.Name = "labAutoPrint";
            labAutoPrint.Size = new Size(55, 15);
            labAutoPrint.TabIndex = 9;
            labAutoPrint.Text = "自動列印";
            // 
            // radPrintOnce
            // 
            radPrintOnce.AutoSize = true;
            radPrintOnce.Location = new Point(108, 102);
            radPrintOnce.Name = "radPrintOnce";
            radPrintOnce.Size = new Size(49, 19);
            radPrintOnce.TabIndex = 0;
            radPrintOnce.TabStop = true;
            radPrintOnce.Text = "單張";
            radPrintOnce.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(163, 102);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(49, 19);
            radioButton1.TabIndex = 1;
            radioButton1.TabStop = true;
            radioButton1.Text = "連續";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPrint.Location = new Point(428, 100);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(75, 23);
            btnPrint.TabIndex = 8;
            btnPrint.Text = "列印";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // txtOrderNoEnd
            // 
            txtOrderNoEnd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtOrderNoEnd.Location = new Point(304, 71);
            txtOrderNoEnd.Name = "txtOrderNoEnd";
            txtOrderNoEnd.PlaceholderText = "(迄)";
            txtOrderNoEnd.Size = new Size(199, 23);
            txtOrderNoEnd.TabIndex = 4;
            // 
            // txtOrderNoStart
            // 
            txtOrderNoStart.Location = new Point(108, 71);
            txtOrderNoStart.Name = "txtOrderNoStart";
            txtOrderNoStart.PlaceholderText = "(起)";
            txtOrderNoStart.Size = new Size(190, 23);
            txtOrderNoStart.TabIndex = 3;
            // 
            // labOrderNo
            // 
            labOrderNo.AutoSize = true;
            labOrderNo.Location = new Point(47, 74);
            labOrderNo.Name = "labOrderNo";
            labOrderNo.Size = new Size(55, 15);
            labOrderNo.TabIndex = 2;
            labOrderNo.Text = "訂單編號";
            // 
            // cbbExcelConfig
            // 
            cbbExcelConfig.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbbExcelConfig.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbExcelConfig.FormattingEnabled = true;
            cbbExcelConfig.Location = new Point(108, 42);
            cbbExcelConfig.Name = "cbbExcelConfig";
            cbbExcelConfig.Size = new Size(395, 23);
            cbbExcelConfig.TabIndex = 1;
            // 
            // labConfig
            // 
            labConfig.AutoSize = true;
            labConfig.Location = new Point(59, 45);
            labConfig.Name = "labConfig";
            labConfig.Size = new Size(43, 15);
            labConfig.TabIndex = 0;
            labConfig.Text = "設定檔";
            // 
            // tabConfig
            // 
            tabConfig.Controls.Add(btnDelete);
            tabConfig.Controls.Add(btnSetToDefault);
            tabConfig.Controls.Add(btnImport);
            tabConfig.Controls.Add(dgvConfig);
            tabConfig.Location = new Point(4, 24);
            tabConfig.Name = "tabConfig";
            tabConfig.Padding = new Padding(10);
            tabConfig.Size = new Size(516, 442);
            tabConfig.TabIndex = 0;
            tabConfig.Text = "設定檔管理";
            tabConfig.UseVisualStyleBackColor = true;
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
            btnDelete.Visible = false;
            btnDelete.Click += btnDelete_Click;
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
            btnSetToDefault.Visible = false;
            btnSetToDefault.Click += btnSetToDefault_Click;
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
            btnImport.Click += btnImport_Click;
            // 
            // dgvConfig
            // 
            dgvConfig.AllowUserToAddRows = false;
            dgvConfig.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft JhengHei UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvConfig.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvConfig.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvConfig.Columns.AddRange(new DataGridViewColumn[] { IsDeafult, ConfigName, ConfigPath });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft JhengHei UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvConfig.DefaultCellStyle = dataGridViewCellStyle2;
            dgvConfig.Location = new Point(13, 13);
            dgvConfig.Margin = new Padding(3, 3, 3, 10);
            dgvConfig.Name = "dgvConfig";
            dgvConfig.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Microsoft JhengHei UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvConfig.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvConfig.RowHeadersVisible = false;
            dgvConfig.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            ConfigName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ConfigName.DataPropertyName = "ConfigName";
            ConfigName.HeaderText = "Name";
            ConfigName.Name = "ConfigName";
            ConfigName.ReadOnly = true;
            ConfigName.Width = 67;
            // 
            // ConfigPath
            // 
            ConfigPath.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ConfigPath.DataPropertyName = "ConfigPath";
            ConfigPath.HeaderText = "Path";
            ConfigPath.Name = "ConfigPath";
            ConfigPath.ReadOnly = true;
            ConfigPath.Width = 57;
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
            Text = "SGS.HN.Labeler";
            Load += frmMain_Load;
            tabMain.ResumeLayout(false);
            tabPrint.ResumeLayout(false);
            tabPrint.PerformLayout();
            tabConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvConfig).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private TabControl tabMain;
        private TabPage tabConfig;
        private TabPage tabPrint;
        private StatusStrip statusBarMain;
        private ProgressBar progressBarMain;
        private DataGridView dgvConfig;
        private Button btnDelete;
        private Button btnSetToDefault;
        private Button btnImport;
        private TextBox txtOrderNoEnd;
        private TextBox txtOrderNoStart;
        private Label labOrderNo;
        private ComboBox cbbExcelConfig;
        private Label labConfig;
        private RadioButton radPrintOnce;
        private RadioButton radioButton1;
        private TextBox txtOutputMessage;
        private Label labAutoPrint;
        private Button btnPrint;
        private DataGridViewCheckBoxColumn IsDeafult;
        private DataGridViewTextBoxColumn ConfigName;
        private DataGridViewTextBoxColumn ConfigPath;
        private Label labPrinter;
        private ComboBox cbbPrinter;
    }
}
