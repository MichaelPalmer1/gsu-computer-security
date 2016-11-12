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
            this.txtCapturedData = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPacketCount = new System.Windows.Forms.TextBox();
            this.txtGUID = new System.Windows.Forms.TextBox();
            this.resultTable = new System.Windows.Forms.DataGridView();
            this.sourceIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.srcPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.srcMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.protocol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etherType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TTL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.headerLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ack = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.syn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sequenceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ackNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checksum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.validChecksum = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultTable)).BeginInit();
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
            this.cmbDevices.Size = new System.Drawing.Size(417, 21);
            this.cmbDevices.TabIndex = 1;
            this.cmbDevices.SelectedIndexChanged += new System.EventHandler(this.cmbDevices_SelectedIndexChanged);
            // 
            // txtCapturedData
            // 
            this.txtCapturedData.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapturedData.Location = new System.Drawing.Point(12, 150);
            this.txtCapturedData.Multiline = true;
            this.txtCapturedData.Name = "txtCapturedData";
            this.txtCapturedData.ReadOnly = true;
            this.txtCapturedData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCapturedData.Size = new System.Drawing.Size(417, 319);
            this.txtCapturedData.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.screenToolStripMenuItem,
            this.packetsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1564, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // screenToolStripMenuItem
            // 
            this.screenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.screenToolStripMenuItem.Name = "screenToolStripMenuItem";
            this.screenToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.screenToolStripMenuItem.Text = "Screen";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // packetsToolStripMenuItem
            // 
            this.packetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendWindowToolStripMenuItem});
            this.packetsToolStripMenuItem.Name = "packetsToolStripMenuItem";
            this.packetsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.packetsToolStripMenuItem.Text = "Packets";
            // 
            // sendWindowToolStripMenuItem
            // 
            this.sendWindowToolStripMenuItem.Name = "sendWindowToolStripMenuItem";
            this.sendWindowToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.sendWindowToolStripMenuItem.Text = "&Send Window";
            this.sendWindowToolStripMenuItem.Click += new System.EventHandler(this.sendWindowToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Number of Packets";
            // 
            // txtPacketCount
            // 
            this.txtPacketCount.Location = new System.Drawing.Point(320, 57);
            this.txtPacketCount.Name = "txtPacketCount";
            this.txtPacketCount.Size = new System.Drawing.Size(109, 20);
            this.txtPacketCount.TabIndex = 5;
            this.txtPacketCount.Text = "0";
            this.txtPacketCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtGUID
            // 
            this.txtGUID.Location = new System.Drawing.Point(12, 124);
            this.txtGUID.Name = "txtGUID";
            this.txtGUID.ReadOnly = true;
            this.txtGUID.Size = new System.Drawing.Size(417, 20);
            this.txtGUID.TabIndex = 6;
            // 
            // resultTable
            // 
            this.resultTable.AllowUserToAddRows = false;
            this.resultTable.AllowUserToDeleteRows = false;
            this.resultTable.AllowUserToOrderColumns = true;
            this.resultTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sourceIP,
            this.srcPort,
            this.destIP,
            this.destPort,
            this.srcMac,
            this.destMac,
            this.protocol,
            this.etherType,
            this.TTL,
            this.headerLength,
            this.totalLength,
            this.ack,
            this.syn,
            this.sequenceNumber,
            this.ackNum,
            this.checksum,
            this.validChecksum,
            this.data});
            this.resultTable.Location = new System.Drawing.Point(435, 27);
            this.resultTable.Name = "resultTable";
            this.resultTable.ReadOnly = true;
            this.resultTable.Size = new System.Drawing.Size(1117, 590);
            this.resultTable.TabIndex = 7;
            this.resultTable.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.resultTable_CellContentDoubleClick);
            // 
            // sourceIP
            // 
            this.sourceIP.HeaderText = "Source IP";
            this.sourceIP.Name = "sourceIP";
            this.sourceIP.ReadOnly = true;
            // 
            // srcPort
            // 
            this.srcPort.HeaderText = "Source Port";
            this.srcPort.Name = "srcPort";
            this.srcPort.ReadOnly = true;
            // 
            // destIP
            // 
            this.destIP.HeaderText = "Destination IP";
            this.destIP.Name = "destIP";
            this.destIP.ReadOnly = true;
            // 
            // destPort
            // 
            this.destPort.HeaderText = "Destination Port";
            this.destPort.Name = "destPort";
            this.destPort.ReadOnly = true;
            // 
            // srcMac
            // 
            this.srcMac.HeaderText = "Source MAC";
            this.srcMac.Name = "srcMac";
            this.srcMac.ReadOnly = true;
            // 
            // destMac
            // 
            this.destMac.HeaderText = "Destination MAC";
            this.destMac.Name = "destMac";
            this.destMac.ReadOnly = true;
            // 
            // protocol
            // 
            this.protocol.HeaderText = "Protocol";
            this.protocol.Name = "protocol";
            this.protocol.ReadOnly = true;
            // 
            // etherType
            // 
            this.etherType.HeaderText = "EtherType";
            this.etherType.Name = "etherType";
            this.etherType.ReadOnly = true;
            // 
            // TTL
            // 
            this.TTL.HeaderText = "TTL";
            this.TTL.Name = "TTL";
            this.TTL.ReadOnly = true;
            // 
            // headerLength
            // 
            this.headerLength.HeaderText = "HLEN";
            this.headerLength.Name = "headerLength";
            this.headerLength.ReadOnly = true;
            // 
            // totalLength
            // 
            this.totalLength.HeaderText = "TLEN";
            this.totalLength.Name = "totalLength";
            this.totalLength.ReadOnly = true;
            // 
            // ack
            // 
            this.ack.HeaderText = "ACK";
            this.ack.Name = "ack";
            this.ack.ReadOnly = true;
            // 
            // syn
            // 
            this.syn.HeaderText = "SYN";
            this.syn.Name = "syn";
            this.syn.ReadOnly = true;
            // 
            // sequenceNumber
            // 
            this.sequenceNumber.HeaderText = "SEQ #";
            this.sequenceNumber.Name = "sequenceNumber";
            this.sequenceNumber.ReadOnly = true;
            // 
            // ackNum
            // 
            this.ackNum.HeaderText = "ACK #";
            this.ackNum.Name = "ackNum";
            this.ackNum.ReadOnly = true;
            // 
            // checksum
            // 
            this.checksum.HeaderText = "Checksum";
            this.checksum.Name = "checksum";
            this.checksum.ReadOnly = true;
            // 
            // validChecksum
            // 
            this.validChecksum.HeaderText = "Valid Checksum";
            this.validChecksum.Name = "validChecksum";
            this.validChecksum.ReadOnly = true;
            // 
            // data
            // 
            this.data.HeaderText = "Data";
            this.data.Name = "data";
            this.data.ReadOnly = true;
            // 
            // frmCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1564, 691);
            this.Controls.Add(this.resultTable);
            this.Controls.Add(this.txtGUID);
            this.Controls.Add(this.txtPacketCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCapturedData);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.ComboBox cmbDevices;
        private System.Windows.Forms.TextBox txtCapturedData;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem screenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPacketCount;
        private System.Windows.Forms.ToolStripMenuItem packetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendWindowToolStripMenuItem;
        private System.Windows.Forms.TextBox txtGUID;
        private System.Windows.Forms.DataGridView resultTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn srcPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn destIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn destPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn srcMac;
        private System.Windows.Forms.DataGridViewTextBoxColumn destMac;
        private System.Windows.Forms.DataGridViewTextBoxColumn protocol;
        private System.Windows.Forms.DataGridViewTextBoxColumn etherType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TTL;
        private System.Windows.Forms.DataGridViewTextBoxColumn headerLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalLength;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ack;
        private System.Windows.Forms.DataGridViewCheckBoxColumn syn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sequenceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ackNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn checksum;
        private System.Windows.Forms.DataGridViewCheckBoxColumn validChecksum;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
    }
}

