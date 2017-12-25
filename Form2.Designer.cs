namespace AutoBrowser
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtPWD1 = new System.Windows.Forms.TextBox();
            this.txtPWD2 = new System.Windows.Forms.TextBox();
            this.labID = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCLS = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtMAIL = new System.Windows.Forms.TextBox();
            this.txtNAME = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCLS2 = new System.Windows.Forms.Button();
            this.btnNEW = new System.Windows.Forms.Button();
            this.btnDEL = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.進階ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.推播管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.說明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取得TOKENToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gITHUBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(33, 24);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(665, 297);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // txtPWD1
            // 
            this.txtPWD1.Location = new System.Drawing.Point(229, 20);
            this.txtPWD1.Margin = new System.Windows.Forms.Padding(2);
            this.txtPWD1.Name = "txtPWD1";
            this.txtPWD1.Size = new System.Drawing.Size(76, 22);
            this.txtPWD1.TabIndex = 1;
            // 
            // txtPWD2
            // 
            this.txtPWD2.Location = new System.Drawing.Point(229, 45);
            this.txtPWD2.Margin = new System.Windows.Forms.Padding(2);
            this.txtPWD2.Name = "txtPWD2";
            this.txtPWD2.Size = new System.Drawing.Size(76, 22);
            this.txtPWD2.TabIndex = 2;
            // 
            // labID
            // 
            this.labID.AutoSize = true;
            this.labID.BackColor = System.Drawing.Color.Red;
            this.labID.Font = new System.Drawing.Font("新細明體", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labID.ForeColor = System.Drawing.Color.White;
            this.labID.Location = new System.Drawing.Point(33, 22);
            this.labID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labID.Name = "labID";
            this.labID.Size = new System.Drawing.Size(107, 35);
            this.labID.TabIndex = 3;
            this.labID.Text = "NONE";
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(341, 16);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "確定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCLS
            // 
            this.btnCLS.Enabled = false;
            this.btnCLS.Location = new System.Drawing.Point(341, 43);
            this.btnCLS.Margin = new System.Windows.Forms.Padding(2);
            this.btnCLS.Name = "btnCLS";
            this.btnCLS.Size = new System.Drawing.Size(56, 24);
            this.btnCLS.TabIndex = 5;
            this.btnCLS.Text = "取消";
            this.btnCLS.UseVisualStyleBackColor = true;
            this.btnCLS.Click += new System.EventHandler(this.btnCLS_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labID);
            this.groupBox1.Controls.Add(this.btnCLS);
            this.groupBox1.Controls.Add(this.txtPWD1);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.txtPWD2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 387);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(698, 74);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "密碼確認-「若要設定密碼,請點選上方GRID的PWD欄位」";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "再次確認:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "PWD:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "ID:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtID);
            this.groupBox2.Controls.Add(this.txtMAIL);
            this.groupBox2.Controls.Add(this.txtNAME);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnCLS2);
            this.groupBox2.Controls.Add(this.btnNEW);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 321);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(698, 66);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "新增";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(33, 18);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(76, 22);
            this.txtID.TabIndex = 20;
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            // 
            // txtMAIL
            // 
            this.txtMAIL.Location = new System.Drawing.Point(380, 18);
            this.txtMAIL.Margin = new System.Windows.Forms.Padding(2);
            this.txtMAIL.Name = "txtMAIL";
            this.txtMAIL.Size = new System.Drawing.Size(144, 22);
            this.txtMAIL.TabIndex = 19;
            // 
            // txtNAME
            // 
            this.txtNAME.Location = new System.Drawing.Point(205, 18);
            this.txtNAME.Margin = new System.Windows.Forms.Padding(2);
            this.txtNAME.Name = "txtNAME";
            this.txtNAME.Size = new System.Drawing.Size(76, 22);
            this.txtNAME.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(160, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "NAME:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(339, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "MAIL:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 22);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "ID:";
            // 
            // btnCLS2
            // 
            this.btnCLS2.Enabled = false;
            this.btnCLS2.Location = new System.Drawing.Point(598, 16);
            this.btnCLS2.Margin = new System.Windows.Forms.Padding(2);
            this.btnCLS2.Name = "btnCLS2";
            this.btnCLS2.Size = new System.Drawing.Size(56, 24);
            this.btnCLS2.TabIndex = 5;
            this.btnCLS2.Text = "取消";
            this.btnCLS2.UseVisualStyleBackColor = true;
            this.btnCLS2.Click += new System.EventHandler(this.btnCLS2_Click);
            // 
            // btnNEW
            // 
            this.btnNEW.Enabled = false;
            this.btnNEW.Location = new System.Drawing.Point(538, 16);
            this.btnNEW.Margin = new System.Windows.Forms.Padding(2);
            this.btnNEW.Name = "btnNEW";
            this.btnNEW.Size = new System.Drawing.Size(56, 24);
            this.btnNEW.TabIndex = 4;
            this.btnNEW.Text = "新增";
            this.btnNEW.UseVisualStyleBackColor = true;
            this.btnNEW.Click += new System.EventHandler(this.btnNEW_Click);
            // 
            // btnDEL
            // 
            this.btnDEL.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDEL.Enabled = false;
            this.btnDEL.Location = new System.Drawing.Point(0, 24);
            this.btnDEL.Margin = new System.Windows.Forms.Padding(2);
            this.btnDEL.Name = "btnDEL";
            this.btnDEL.Size = new System.Drawing.Size(32, 297);
            this.btnDEL.TabIndex = 21;
            this.btnDEL.Text = "刪";
            this.btnDEL.UseVisualStyleBackColor = true;
            this.btnDEL.Click += new System.EventHandler(this.btnDEL_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.進階ToolStripMenuItem,
            this.說明ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(698, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 進階ToolStripMenuItem
            // 
            this.進階ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.推播管理ToolStripMenuItem});
            this.進階ToolStripMenuItem.Name = "進階ToolStripMenuItem";
            this.進階ToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.進階ToolStripMenuItem.Text = "進階";
            // 
            // 推播管理ToolStripMenuItem
            // 
            this.推播管理ToolStripMenuItem.Name = "推播管理ToolStripMenuItem";
            this.推播管理ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.推播管理ToolStripMenuItem.Text = "推播管理";
            this.推播管理ToolStripMenuItem.Click += new System.EventHandler(this.推播管理ToolStripMenuItem_Click);
            // 
            // 說明ToolStripMenuItem
            // 
            this.說明ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.取得TOKENToolStripMenuItem,
            this.gITHUBToolStripMenuItem,
            this.版本ToolStripMenuItem});
            this.說明ToolStripMenuItem.Name = "說明ToolStripMenuItem";
            this.說明ToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.說明ToolStripMenuItem.Text = "說明";
            // 
            // 取得TOKENToolStripMenuItem
            // 
            this.取得TOKENToolStripMenuItem.Name = "取得TOKENToolStripMenuItem";
            this.取得TOKENToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.取得TOKENToolStripMenuItem.Text = "LINE TOKEN";
            this.取得TOKENToolStripMenuItem.Click += new System.EventHandler(this.取得TOKENToolStripMenuItem_Click);
            // 
            // gITHUBToolStripMenuItem
            // 
            this.gITHUBToolStripMenuItem.Name = "gITHUBToolStripMenuItem";
            this.gITHUBToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gITHUBToolStripMenuItem.Text = "GITHUB";
            this.gITHUBToolStripMenuItem.Click += new System.EventHandler(this.gITHUBToolStripMenuItem_Click);
            // 
            // 版本ToolStripMenuItem
            // 
            this.版本ToolStripMenuItem.Name = "版本ToolStripMenuItem";
            this.版本ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.版本ToolStripMenuItem.Text = "版本";
            this.版本ToolStripMenuItem.Click += new System.EventHandler(this.版本ToolStripMenuItem_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 461);
            this.Controls.Add(this.btnDEL);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.Text = "名單維護";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtPWD1;
        private System.Windows.Forms.TextBox txtPWD2;
        private System.Windows.Forms.Label labID;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCLS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCLS2;
        private System.Windows.Forms.Button btnNEW;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtMAIL;
        private System.Windows.Forms.TextBox txtNAME;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDEL;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 進階ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 推播管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 說明ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取得TOKENToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gITHUBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版本ToolStripMenuItem;
    }
}