namespace pub
{
    partial class Form1
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
            this.listBoxDevicesConnected = new System.Windows.Forms.ListBox();
            this.labelDevices = new System.Windows.Forms.Label();
            this.labelDevicesWhitelisted = new System.Windows.Forms.Label();
            this.listBoxWhitelistedDevices = new System.Windows.Forms.ListBox();
            this.buttonWhitelist = new System.Windows.Forms.Button();
            this.buttonRemoveDevice = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelBackupLocation = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonBackupLocation = new System.Windows.Forms.Button();
            this.textBoxFolderPath = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxDevicesConnected
            // 
            this.listBoxDevicesConnected.FormattingEnabled = true;
            this.listBoxDevicesConnected.Location = new System.Drawing.Point(6, 34);
            this.listBoxDevicesConnected.Name = "listBoxDevicesConnected";
            this.listBoxDevicesConnected.Size = new System.Drawing.Size(184, 95);
            this.listBoxDevicesConnected.TabIndex = 0;
            // 
            // labelDevices
            // 
            this.labelDevices.AutoSize = true;
            this.labelDevices.Location = new System.Drawing.Point(5, 16);
            this.labelDevices.Name = "labelDevices";
            this.labelDevices.Size = new System.Drawing.Size(101, 13);
            this.labelDevices.TabIndex = 1;
            this.labelDevices.Text = "Devices Connected";
            // 
            // labelDevicesWhitelisted
            // 
            this.labelDevicesWhitelisted.AutoSize = true;
            this.labelDevicesWhitelisted.Location = new System.Drawing.Point(5, 170);
            this.labelDevicesWhitelisted.Name = "labelDevicesWhitelisted";
            this.labelDevicesWhitelisted.Size = new System.Drawing.Size(101, 13);
            this.labelDevicesWhitelisted.TabIndex = 3;
            this.labelDevicesWhitelisted.Text = "Devices Whitelisted";
            // 
            // listBoxWhitelistedDevices
            // 
            this.listBoxWhitelistedDevices.FormattingEnabled = true;
            this.listBoxWhitelistedDevices.Location = new System.Drawing.Point(6, 188);
            this.listBoxWhitelistedDevices.Name = "listBoxWhitelistedDevices";
            this.listBoxWhitelistedDevices.Size = new System.Drawing.Size(184, 95);
            this.listBoxWhitelistedDevices.TabIndex = 2;
            // 
            // buttonWhitelist
            // 
            this.buttonWhitelist.Location = new System.Drawing.Point(6, 135);
            this.buttonWhitelist.Name = "buttonWhitelist";
            this.buttonWhitelist.Size = new System.Drawing.Size(184, 23);
            this.buttonWhitelist.TabIndex = 4;
            this.buttonWhitelist.Text = "Whitelist";
            this.buttonWhitelist.UseVisualStyleBackColor = true;
            this.buttonWhitelist.Click += new System.EventHandler(this.buttonWhitelist_Click);
            // 
            // buttonRemoveDevice
            // 
            this.buttonRemoveDevice.Location = new System.Drawing.Point(6, 289);
            this.buttonRemoveDevice.Name = "buttonRemoveDevice";
            this.buttonRemoveDevice.Size = new System.Drawing.Size(184, 23);
            this.buttonRemoveDevice.TabIndex = 5;
            this.buttonRemoveDevice.Text = "Remove";
            this.buttonRemoveDevice.UseVisualStyleBackColor = true;
            this.buttonRemoveDevice.Click += new System.EventHandler(this.buttonRemoveDevice_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxDevicesConnected);
            this.groupBox1.Controls.Add(this.buttonRemoveDevice);
            this.groupBox1.Controls.Add(this.labelDevices);
            this.groupBox1.Controls.Add(this.buttonWhitelist);
            this.groupBox1.Controls.Add(this.listBoxWhitelistedDevices);
            this.groupBox1.Controls.Add(this.labelDevicesWhitelisted);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 319);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Device Management";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelBackupLocation);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comboBox3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.buttonBackupLocation);
            this.groupBox2.Controls.Add(this.textBoxFolderPath);
            this.groupBox2.Location = new System.Drawing.Point(218, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 190);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Backup Settings";
            // 
            // labelBackupLocation
            // 
            this.labelBackupLocation.AutoSize = true;
            this.labelBackupLocation.Location = new System.Drawing.Point(6, 16);
            this.labelBackupLocation.Name = "labelBackupLocation";
            this.labelBackupLocation.Size = new System.Drawing.Size(88, 13);
            this.labelBackupLocation.TabIndex = 8;
            this.labelBackupLocation.Text = "Backup Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "label3";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "High",
            "Medium",
            "Low"});
            this.comboBox3.Location = new System.Drawing.Point(6, 162);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(178, 21);
            this.comboBox3.TabIndex = 6;
            this.comboBox3.Text = "Default : Medium";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "High",
            "Medium",
            "Low"});
            this.comboBox2.Location = new System.Drawing.Point(6, 122);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(178, 21);
            this.comboBox2.TabIndex = 4;
            this.comboBox2.Text = "Default : Medium";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "High",
            "Medium",
            "Low"});
            this.comboBox1.Location = new System.Drawing.Point(6, 82);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(178, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "Default : Medium";
            // 
            // buttonBackupLocation
            // 
            this.buttonBackupLocation.Location = new System.Drawing.Point(128, 32);
            this.buttonBackupLocation.Name = "buttonBackupLocation";
            this.buttonBackupLocation.Size = new System.Drawing.Size(56, 24);
            this.buttonBackupLocation.TabIndex = 1;
            this.buttonBackupLocation.Text = "Open";
            this.buttonBackupLocation.UseVisualStyleBackColor = true;
            this.buttonBackupLocation.Click += new System.EventHandler(this.buttonBackupLocation_Click);
            // 
            // textBoxFolderPath
            // 
            this.textBoxFolderPath.Location = new System.Drawing.Point(6, 34);
            this.textBoxFolderPath.Name = "textBoxFolderPath";
            this.textBoxFolderPath.ReadOnly = true;
            this.textBoxFolderPath.Size = new System.Drawing.Size(116, 20);
            this.textBoxFolderPath.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 343);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "pub";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxDevicesConnected;
        private System.Windows.Forms.Label labelDevices;
        private System.Windows.Forms.Label labelDevicesWhitelisted;
        private System.Windows.Forms.ListBox listBoxWhitelistedDevices;
        private System.Windows.Forms.Button buttonWhitelist;
        private System.Windows.Forms.Button buttonRemoveDevice;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonBackupLocation;
        private System.Windows.Forms.TextBox textBoxFolderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label labelBackupLocation;
    }
}

