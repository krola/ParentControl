namespace ParentControl.SetupTool
{
    partial class SetupTool
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpService = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbDeviceName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDevice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbApi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lConnected = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbRegDevice = new System.Windows.Forms.Button();
            this.bApi = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tpAuthorization = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnAuthSave = new System.Windows.Forms.Button();
            this.tpTimesheet = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnAddTimesheet = new System.Windows.Forms.Button();
            this.btnRemoveTimesheet = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbNoToDate = new System.Windows.Forms.CheckBox();
            this.nTime = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tbToken = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tpService.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tpAuthorization.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tpTimesheet.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nTime)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpService);
            this.tabControl1.Controls.Add(this.tpAuthorization);
            this.tabControl1.Controls.Add(this.tpTimesheet);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(465, 462);
            this.tabControl1.TabIndex = 1;
            // 
            // tpService
            // 
            this.tpService.Controls.Add(this.groupBox3);
            this.tpService.Controls.Add(this.groupBox2);
            this.tpService.Location = new System.Drawing.Point(4, 22);
            this.tpService.Name = "tpService";
            this.tpService.Size = new System.Drawing.Size(457, 436);
            this.tpService.TabIndex = 2;
            this.tpService.Text = "Service";
            this.tpService.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbDeviceName);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tbDevice);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tbApi);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lConnected);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(8, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(441, 390);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Information";
            // 
            // tbDeviceName
            // 
            this.tbDeviceName.Location = new System.Drawing.Point(85, 75);
            this.tbDeviceName.Name = "tbDeviceName";
            this.tbDeviceName.Size = new System.Drawing.Size(152, 20);
            this.tbDeviceName.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Device name:";
            // 
            // tbDevice
            // 
            this.tbDevice.Enabled = false;
            this.tbDevice.Location = new System.Drawing.Point(85, 49);
            this.tbDevice.Name = "tbDevice";
            this.tbDevice.Size = new System.Drawing.Size(152, 20);
            this.tbDevice.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Device id:";
            // 
            // tbApi
            // 
            this.tbApi.Location = new System.Drawing.Point(85, 23);
            this.tbApi.Name = "tbApi";
            this.tbApi.Size = new System.Drawing.Size(152, 20);
            this.tbApi.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "API:";
            // 
            // lConnected
            // 
            this.lConnected.AutoSize = true;
            this.lConnected.Location = new System.Drawing.Point(52, 108);
            this.lConnected.Name = "lConnected";
            this.lConnected.Size = new System.Drawing.Size(46, 13);
            this.lConnected.TabIndex = 1;
            this.lConnected.Text = "Installed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbRegDevice);
            this.groupBox2.Controls.Add(this.bApi);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Location = new System.Drawing.Point(8, 399);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 29);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Actions";
            // 
            // tbRegDevice
            // 
            this.tbRegDevice.Location = new System.Drawing.Point(85, 6);
            this.tbRegDevice.Name = "tbRegDevice";
            this.tbRegDevice.Size = new System.Drawing.Size(113, 23);
            this.tbRegDevice.TabIndex = 3;
            this.tbRegDevice.Text = "Register device";
            this.tbRegDevice.UseVisualStyleBackColor = true;
            this.tbRegDevice.Click += new System.EventHandler(this.tbRegDevice_Click);
            // 
            // bApi
            // 
            this.bApi.Location = new System.Drawing.Point(204, 6);
            this.bApi.Name = "bApi";
            this.bApi.Size = new System.Drawing.Size(75, 23);
            this.bApi.TabIndex = 2;
            this.bApi.Text = "Update";
            this.bApi.UseVisualStyleBackColor = true;
            this.bApi.Click += new System.EventHandler(this.bApi_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(285, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Install";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(366, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Remove";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // tpAuthorization
            // 
            this.tpAuthorization.Controls.Add(this.groupBox4);
            this.tpAuthorization.Controls.Add(this.groupBox1);
            this.tpAuthorization.Location = new System.Drawing.Point(4, 22);
            this.tpAuthorization.Name = "tpAuthorization";
            this.tpAuthorization.Padding = new System.Windows.Forms.Padding(3);
            this.tpAuthorization.Size = new System.Drawing.Size(457, 436);
            this.tpAuthorization.TabIndex = 0;
            this.tpAuthorization.Text = "Authorization";
            this.tpAuthorization.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.tableLayoutPanel1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(451, 401);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configuration";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbLogin, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbPassword, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbToken, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(445, 72);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "Username: ";
            // 
            // tbLogin
            // 
            this.tbLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogin.Location = new System.Drawing.Point(109, 3);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(333, 20);
            this.tbLogin.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Password: ";
            // 
            // tbPassword
            // 
            this.tbPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPassword.Location = new System.Drawing.Point(109, 29);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = 'x';
            this.tbPassword.Size = new System.Drawing.Size(333, 20);
            this.tbPassword.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.btnAuthSave);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 404);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 29);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actions";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(295, 6);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnAuthSave
            // 
            this.btnAuthSave.Location = new System.Drawing.Point(376, 6);
            this.btnAuthSave.Name = "btnAuthSave";
            this.btnAuthSave.Size = new System.Drawing.Size(75, 23);
            this.btnAuthSave.TabIndex = 0;
            this.btnAuthSave.Text = "Save";
            this.btnAuthSave.UseVisualStyleBackColor = true;
            this.btnAuthSave.Click += new System.EventHandler(this.btnAuthSave_Click);
            // 
            // tpTimesheet
            // 
            this.tpTimesheet.Controls.Add(this.groupBox6);
            this.tpTimesheet.Controls.Add(this.groupBox5);
            this.tpTimesheet.Location = new System.Drawing.Point(4, 22);
            this.tpTimesheet.Name = "tpTimesheet";
            this.tpTimesheet.Padding = new System.Windows.Forms.Padding(3);
            this.tpTimesheet.Size = new System.Drawing.Size(457, 436);
            this.tpTimesheet.TabIndex = 1;
            this.tpTimesheet.Text = "Timesheets";
            this.tpTimesheet.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnAddTimesheet);
            this.groupBox6.Controls.Add(this.btnRemoveTimesheet);
            this.groupBox6.Location = new System.Drawing.Point(6, 392);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(448, 41);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Actions";
            // 
            // btnAddTimesheet
            // 
            this.btnAddTimesheet.Location = new System.Drawing.Point(287, 12);
            this.btnAddTimesheet.Name = "btnAddTimesheet";
            this.btnAddTimesheet.Size = new System.Drawing.Size(75, 23);
            this.btnAddTimesheet.TabIndex = 1;
            this.btnAddTimesheet.Text = "Add";
            this.btnAddTimesheet.UseVisualStyleBackColor = true;
            this.btnAddTimesheet.Click += new System.EventHandler(this.btnAddTimesheet_Click);
            // 
            // btnRemoveTimesheet
            // 
            this.btnRemoveTimesheet.Location = new System.Drawing.Point(368, 12);
            this.btnRemoveTimesheet.Name = "btnRemoveTimesheet";
            this.btnRemoveTimesheet.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveTimesheet.TabIndex = 0;
            this.btnRemoveTimesheet.Text = "Remove";
            this.btnRemoveTimesheet.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbNoToDate);
            this.groupBox5.Controls.Add(this.nTime);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.listBox1);
            this.groupBox5.Controls.Add(this.dtTo);
            this.groupBox5.Controls.Add(this.dtFrom);
            this.groupBox5.Location = new System.Drawing.Point(8, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(443, 380);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Timesheets";
            // 
            // cbNoToDate
            // 
            this.cbNoToDate.AutoSize = true;
            this.cbNoToDate.Location = new System.Drawing.Point(237, 54);
            this.cbNoToDate.Name = "cbNoToDate";
            this.cbNoToDate.Size = new System.Drawing.Size(82, 17);
            this.cbNoToDate.TabIndex = 6;
            this.cbNoToDate.Text = "No To Date";
            this.cbNoToDate.UseVisualStyleBackColor = true;
            this.cbNoToDate.CheckedChanged += new System.EventHandler(this.cbNoToDate_CheckedChanged);
            // 
            // nTime
            // 
            this.nTime.Location = new System.Drawing.Point(50, 53);
            this.nTime.Name = "nTime";
            this.nTime.Size = new System.Drawing.Size(46, 20);
            this.nTime.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Hours:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 84);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(431, 290);
            this.listBox1.TabIndex = 2;
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(237, 19);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(200, 20);
            this.dtTo.TabIndex = 1;
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(6, 19);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(200, 20);
            this.dtFrom.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Token";
            // 
            // tbToken
            // 
            this.tbToken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbToken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbToken.Enabled = false;
            this.tbToken.Location = new System.Drawing.Point(109, 55);
            this.tbToken.Name = "tbToken";
            this.tbToken.Size = new System.Drawing.Size(333, 20);
            this.tbToken.TabIndex = 5;
            // 
            // SetupTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 462);
            this.Controls.Add(this.tabControl1);
            this.Name = "SetupTool";
            this.Text = "Parent Control Configuration Wizard";
            this.tabControl1.ResumeLayout(false);
            this.tpService.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tpAuthorization.ResumeLayout(false);
            this.tpAuthorization.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tpTimesheet.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpService;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lConnected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tpAuthorization;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnAuthSave;
        private System.Windows.Forms.TabPage tpTimesheet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbApi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bApi;
        private System.Windows.Forms.TextBox tbDeviceName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDevice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button tbRegDevice;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnAddTimesheet;
        private System.Windows.Forms.Button btnRemoveTimesheet;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.NumericUpDown nTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbNoToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbToken;
    }
}

