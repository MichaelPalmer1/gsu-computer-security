namespace PacketSniff
{
    partial class frmCapture
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
            this.btnStartStop = new System.Windows.Forms.Button();
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPacketCount = new System.Windows.Forms.TextBox();
            this.txtGUID = new System.Windows.Forms.TextBox();
            this.resultTable = new System.Windows.Forms.DataGridView();
            this.sourceIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.protocol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPostalCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCountry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ipAddress = new System.Windows.Forms.TextBox();
            this.btnSearchIP = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.chkHideLocal = new System.Windows.Forms.CheckBox();
            this.tblGratuitousArps = new System.Windows.Forms.DataGridView();
            this.colArpSourceMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArpSourceIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArpTargetMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArpTargetIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblGratuitousArps = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblGratuitousArps)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartStop
            // 
            this.btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartStop.Location = new System.Drawing.Point(12, 44);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(88, 39);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // cmbDevices
            // 
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Location = new System.Drawing.Point(12, 97);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(349, 21);
            this.cmbDevices.TabIndex = 1;
            this.cmbDevices.SelectedIndexChanged += new System.EventHandler(this.cmbDevices_SelectedIndexChanged);
            // 
            // txtResults
            // 
            this.txtResults.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.Location = new System.Drawing.Point(12, 150);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResults.Size = new System.Drawing.Size(349, 245);
            this.txtResults.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1239, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Packets";
            // 
            // txtPacketCount
            // 
            this.txtPacketCount.Location = new System.Drawing.Point(268, 57);
            this.txtPacketCount.Name = "txtPacketCount";
            this.txtPacketCount.Size = new System.Drawing.Size(93, 20);
            this.txtPacketCount.TabIndex = 5;
            this.txtPacketCount.Text = "0";
            this.txtPacketCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGUID
            // 
            this.txtGUID.Location = new System.Drawing.Point(12, 124);
            this.txtGUID.Name = "txtGUID";
            this.txtGUID.ReadOnly = true;
            this.txtGUID.Size = new System.Drawing.Size(349, 20);
            this.txtGUID.TabIndex = 6;
            // 
            // resultTable
            // 
            this.resultTable.AllowUserToAddRows = false;
            this.resultTable.AllowUserToDeleteRows = false;
            this.resultTable.AllowUserToOrderColumns = true;
            this.resultTable.AllowUserToResizeRows = false;
            this.resultTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sourceIP,
            this.protocol,
            this.colCity,
            this.colState,
            this.colPostalCode,
            this.colCountry,
            this.colLat,
            this.colLong});
            this.resultTable.Location = new System.Drawing.Point(367, 44);
            this.resultTable.Name = "resultTable";
            this.resultTable.ReadOnly = true;
            this.resultTable.Size = new System.Drawing.Size(844, 326);
            this.resultTable.TabIndex = 7;
            // 
            // sourceIP
            // 
            this.sourceIP.HeaderText = "Source IP";
            this.sourceIP.Name = "sourceIP";
            this.sourceIP.ReadOnly = true;
            // 
            // protocol
            // 
            this.protocol.HeaderText = "Protocol";
            this.protocol.Name = "protocol";
            this.protocol.ReadOnly = true;
            // 
            // colCity
            // 
            this.colCity.HeaderText = "City";
            this.colCity.Name = "colCity";
            this.colCity.ReadOnly = true;
            // 
            // colState
            // 
            this.colState.HeaderText = "State";
            this.colState.Name = "colState";
            this.colState.ReadOnly = true;
            // 
            // colPostalCode
            // 
            this.colPostalCode.HeaderText = "Postal Code";
            this.colPostalCode.Name = "colPostalCode";
            this.colPostalCode.ReadOnly = true;
            // 
            // colCountry
            // 
            this.colCountry.HeaderText = "Country";
            this.colCountry.Name = "colCountry";
            this.colCountry.ReadOnly = true;
            // 
            // colLat
            // 
            this.colLat.HeaderText = "Latitude";
            this.colLat.Name = "colLat";
            this.colLat.ReadOnly = true;
            // 
            // colLong
            // 
            this.colLong.HeaderText = "Longitude";
            this.colLong.Name = "colLong";
            this.colLong.ReadOnly = true;
            // 
            // ipAddress
            // 
            this.ipAddress.Location = new System.Drawing.Point(12, 471);
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.Size = new System.Drawing.Size(195, 20);
            this.ipAddress.TabIndex = 8;
            // 
            // btnSearchIP
            // 
            this.btnSearchIP.Location = new System.Drawing.Point(213, 470);
            this.btnSearchIP.Name = "btnSearchIP";
            this.btnSearchIP.Size = new System.Drawing.Size(75, 20);
            this.btnSearchIP.TabIndex = 9;
            this.btnSearchIP.Text = "Lookup IP";
            this.btnSearchIP.UseVisualStyleBackColor = true;
            this.btnSearchIP.Click += new System.EventHandler(this.btnSearchIP_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(379, 376);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(399, 295);
            this.webBrowser.TabIndex = 10;
            // 
            // chkAutoScroll
            // 
            this.chkAutoScroll.AutoSize = true;
            this.chkAutoScroll.Checked = true;
            this.chkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoScroll.Location = new System.Drawing.Point(12, 557);
            this.chkAutoScroll.Name = "chkAutoScroll";
            this.chkAutoScroll.Size = new System.Drawing.Size(112, 17);
            this.chkAutoScroll.TabIndex = 11;
            this.chkAutoScroll.Text = "Auto Scroll Tables";
            this.chkAutoScroll.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(106, 44);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(88, 39);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // chkHideLocal
            // 
            this.chkHideLocal.AutoSize = true;
            this.chkHideLocal.Location = new System.Drawing.Point(12, 580);
            this.chkHideLocal.Name = "chkHideLocal";
            this.chkHideLocal.Size = new System.Drawing.Size(143, 17);
            this.chkHideLocal.TabIndex = 13;
            this.chkHideLocal.Text = "Stop showing private IPs";
            this.chkHideLocal.UseVisualStyleBackColor = true;
            // 
            // tblGratuitousArps
            // 
            this.tblGratuitousArps.AllowUserToAddRows = false;
            this.tblGratuitousArps.AllowUserToDeleteRows = false;
            this.tblGratuitousArps.AllowUserToOrderColumns = true;
            this.tblGratuitousArps.AllowUserToResizeRows = false;
            this.tblGratuitousArps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tblGratuitousArps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblGratuitousArps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colArpSourceMac,
            this.colArpSourceIp,
            this.colArpTargetMac,
            this.colArpTargetIp});
            this.tblGratuitousArps.Location = new System.Drawing.Point(787, 398);
            this.tblGratuitousArps.Name = "tblGratuitousArps";
            this.tblGratuitousArps.ReadOnly = true;
            this.tblGratuitousArps.Size = new System.Drawing.Size(424, 273);
            this.tblGratuitousArps.TabIndex = 14;
            // 
            // colArpSourceMac
            // 
            this.colArpSourceMac.HeaderText = "Source Mac";
            this.colArpSourceMac.Name = "colArpSourceMac";
            this.colArpSourceMac.ReadOnly = true;
            // 
            // colArpSourceIp
            // 
            this.colArpSourceIp.HeaderText = "Source IP";
            this.colArpSourceIp.Name = "colArpSourceIp";
            this.colArpSourceIp.ReadOnly = true;
            // 
            // colArpTargetMac
            // 
            this.colArpTargetMac.HeaderText = "Target MAC";
            this.colArpTargetMac.Name = "colArpTargetMac";
            this.colArpTargetMac.ReadOnly = true;
            // 
            // colArpTargetIp
            // 
            this.colArpTargetIp.HeaderText = "Target IP";
            this.colArpTargetIp.Name = "colArpTargetIp";
            this.colArpTargetIp.ReadOnly = true;
            // 
            // lblGratuitousArps
            // 
            this.lblGratuitousArps.AutoSize = true;
            this.lblGratuitousArps.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGratuitousArps.Location = new System.Drawing.Point(933, 373);
            this.lblGratuitousArps.Name = "lblGratuitousArps";
            this.lblGratuitousArps.Size = new System.Drawing.Size(162, 22);
            this.lblGratuitousArps.TabIndex = 15;
            this.lblGratuitousArps.Text = "Gratuitous ARPs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 423);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "IP Lookup";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 445);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(326, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Perform a lookup of the IP specified below and display it on the map";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 531);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(204, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Configure how the capture tables function";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 509);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Settings";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(718, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(170, 22);
            this.label6.TabIndex = 20;
            this.label6.Text = "Packet Locations";
            // 
            // frmCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1239, 724);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblGratuitousArps);
            this.Controls.Add(this.tblGratuitousArps);
            this.Controls.Add(this.chkHideLocal);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.chkAutoScroll);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.btnSearchIP);
            this.Controls.Add(this.ipAddress);
            this.Controls.Add(this.resultTable);
            this.Controls.Add(this.txtGUID);
            this.Controls.Add(this.txtPacketCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.cmbDevices);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmCapture";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PacketSniff";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblGratuitousArps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.ComboBox cmbDevices;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPacketCount;
        private System.Windows.Forms.TextBox txtGUID;
        private System.Windows.Forms.DataGridView resultTable;
        private System.Windows.Forms.TextBox ipAddress;
        private System.Windows.Forms.Button btnSearchIP;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.CheckBox chkAutoScroll;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox chkHideLocal;
        private System.Windows.Forms.DataGridView tblGratuitousArps;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArpSourceMac;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArpSourceIp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArpTargetMac;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArpTargetIp;
        private System.Windows.Forms.Label lblGratuitousArps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn protocol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPostalCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCountry;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLong;
    }
}

