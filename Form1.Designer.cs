namespace AutoBrowser
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtID = new System.Windows.Forms.TextBox();
            this.btnCARD = new System.Windows.Forms.Button();
            this.txtPWD1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSET = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMAIL = new System.Windows.Forms.TextBox();
            this.btnSHUTDOWN = new System.Windows.Forms.Button();
            this.btnForm2 = new System.Windows.Forms.Button();
            this.txtPWD2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnSHUTDOWN2 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSHUTDOWN = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSYNC = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labTOKEN = new System.Windows.Forms.Label();
            this.labNAME = new System.Windows.Forms.Label();
            this.lstMsg = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(60, 38);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(123, 22);
            this.txtID.TabIndex = 2;
            // 
            // btnCARD
            // 
            this.btnCARD.Location = new System.Drawing.Point(214, 140);
            this.btnCARD.Name = "btnCARD";
            this.btnCARD.Size = new System.Drawing.Size(161, 40);
            this.btnCARD.TabIndex = 7;
            this.btnCARD.Text = "打卡";
            this.btnCARD.UseVisualStyleBackColor = true;
            this.btnCARD.Click += new System.EventHandler(this.btnCARD_Click);
            // 
            // txtPWD1
            // 
            this.txtPWD1.Location = new System.Drawing.Point(60, 64);
            this.txtPWD1.Name = "txtPWD1";
            this.txtPWD1.Size = new System.Drawing.Size(123, 22);
            this.txtPWD1.TabIndex = 3;
            this.txtPWD1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPWD1_Leave);
            this.txtPWD1.Leave += new System.EventHandler(this.txtPWD1_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "帳號:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "密碼:";
            // 
            // btnSET
            // 
            this.btnSET.Location = new System.Drawing.Point(21, 140);
            this.btnSET.Name = "btnSET";
            this.btnSET.Size = new System.Drawing.Size(161, 40);
            this.btnSET.TabIndex = 5;
            this.btnSET.Text = "設定預設值";
            this.btnSET.UseVisualStyleBackColor = true;
            this.btnSET.Click += new System.EventHandler(this.btnSET_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "MAIL:";
            // 
            // txtMAIL
            // 
            this.txtMAIL.Location = new System.Drawing.Point(61, 114);
            this.txtMAIL.Name = "txtMAIL";
            this.txtMAIL.Size = new System.Drawing.Size(122, 22);
            this.txtMAIL.TabIndex = 4;
            // 
            // btnSHUTDOWN
            // 
            this.btnSHUTDOWN.BackColor = System.Drawing.Color.Red;
            this.btnSHUTDOWN.ForeColor = System.Drawing.Color.White;
            this.btnSHUTDOWN.Location = new System.Drawing.Point(213, 193);
            this.btnSHUTDOWN.Margin = new System.Windows.Forms.Padding(2);
            this.btnSHUTDOWN.Name = "btnSHUTDOWN";
            this.btnSHUTDOWN.Size = new System.Drawing.Size(161, 40);
            this.btnSHUTDOWN.TabIndex = 8;
            this.btnSHUTDOWN.Text = "打卡後關機";
            this.btnSHUTDOWN.UseVisualStyleBackColor = false;
            this.btnSHUTDOWN.Click += new System.EventHandler(this.btnSHUTDOWN_Click);
            // 
            // btnForm2
            // 
            this.btnForm2.Location = new System.Drawing.Point(20, 193);
            this.btnForm2.Margin = new System.Windows.Forms.Padding(2);
            this.btnForm2.Name = "btnForm2";
            this.btnForm2.Size = new System.Drawing.Size(77, 40);
            this.btnForm2.TabIndex = 6;
            this.btnForm2.Text = "名單維護";
            this.btnForm2.UseVisualStyleBackColor = true;
            this.btnForm2.Click += new System.EventHandler(this.btnForm2_Click);
            // 
            // txtPWD2
            // 
            this.txtPWD2.Location = new System.Drawing.Point(60, 87);
            this.txtPWD2.Name = "txtPWD2";
            this.txtPWD2.ReadOnly = true;
            this.txtPWD2.Size = new System.Drawing.Size(123, 22);
            this.txtPWD2.TabIndex = 99;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(26, 10);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(158, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // btnSHUTDOWN2
            // 
            this.btnSHUTDOWN2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSHUTDOWN2.Enabled = false;
            this.btnSHUTDOWN2.Location = new System.Drawing.Point(6, 65);
            this.btnSHUTDOWN2.Margin = new System.Windows.Forms.Padding(2);
            this.btnSHUTDOWN2.Name = "btnSHUTDOWN2";
            this.btnSHUTDOWN2.Size = new System.Drawing.Size(161, 40);
            this.btnSHUTDOWN2.TabIndex = 10;
            this.btnSHUTDOWN2.Text = "排程打卡";
            this.btnSHUTDOWN2.UseVisualStyleBackColor = false;
            this.btnSHUTDOWN2.EnabledChanged += new System.EventHandler(this.btnSHUTDOWN2_EnabledChanged);
            this.btnSHUTDOWN2.Click += new System.EventHandler(this.btnSHUTDOWN2_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(5, 14);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(162, 22);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.Value = new System.DateTime(2016, 5, 4, 18, 0, 0, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSHUTDOWN);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.btnSHUTDOWN2);
            this.groupBox1.Location = new System.Drawing.Point(209, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(180, 124);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "排程";
            // 
            // chkSHUTDOWN
            // 
            this.chkSHUTDOWN.AutoSize = true;
            this.chkSHUTDOWN.Location = new System.Drawing.Point(9, 41);
            this.chkSHUTDOWN.Name = "chkSHUTDOWN";
            this.chkSHUTDOWN.Size = new System.Drawing.Size(48, 16);
            this.chkSHUTDOWN.TabIndex = 11;
            this.chkSHUTDOWN.Text = "關機";
            this.chkSHUTDOWN.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSYNC
            // 
            this.btnSYNC.Location = new System.Drawing.Point(106, 193);
            this.btnSYNC.Name = "btnSYNC";
            this.btnSYNC.Size = new System.Drawing.Size(77, 40);
            this.btnSYNC.TabIndex = 100;
            this.btnSYNC.Text = "名單同步";
            this.btnSYNC.UseVisualStyleBackColor = true;
            this.btnSYNC.Click += new System.EventHandler(this.btnSYNC_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // labTOKEN
            // 
            this.labTOKEN.AutoSize = true;
            this.labTOKEN.Location = new System.Drawing.Point(18, 238);
            this.labTOKEN.Name = "labTOKEN";
            this.labTOKEN.Size = new System.Drawing.Size(43, 12);
            this.labTOKEN.TabIndex = 101;
            this.labTOKEN.Text = "TOKEN";
            this.labTOKEN.Visible = false;
            // 
            // labNAME
            // 
            this.labNAME.AutoSize = true;
            this.labNAME.Location = new System.Drawing.Point(67, 238);
            this.labNAME.Name = "labNAME";
            this.labNAME.Size = new System.Drawing.Size(38, 12);
            this.labNAME.TabIndex = 103;
            this.labNAME.Text = "NAME";
            this.labNAME.Visible = false;
            // 
            // lstMsg
            // 
            this.lstMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstMsg.FormattingEnabled = true;
            this.lstMsg.ItemHeight = 12;
            this.lstMsg.Location = new System.Drawing.Point(0, 251);
            this.lstMsg.Name = "lstMsg";
            this.lstMsg.Size = new System.Drawing.Size(398, 40);
            this.lstMsg.TabIndex = 104;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 291);
            this.Controls.Add(this.lstMsg);
            this.Controls.Add(this.labNAME);
            this.Controls.Add(this.labTOKEN);
            this.Controls.Add(this.btnSYNC);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txtPWD2);
            this.Controls.Add(this.btnForm2);
            this.Controls.Add(this.btnSHUTDOWN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMAIL);
            this.Controls.Add(this.btnSET);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPWD1);
            this.Controls.Add(this.btnCARD);
            this.Controls.Add(this.txtID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "打卡";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnCARD;
        private System.Windows.Forms.TextBox txtPWD1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSET;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMAIL;
        private System.Windows.Forms.Button btnSHUTDOWN;
        private System.Windows.Forms.Button btnForm2;
        public System.Windows.Forms.TextBox txtPWD2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnSHUTDOWN2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnSYNC;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labTOKEN;
        private System.Windows.Forms.Label labNAME;
        private System.Windows.Forms.ListBox lstMsg;
        private System.Windows.Forms.CheckBox chkSHUTDOWN;

    }
}

