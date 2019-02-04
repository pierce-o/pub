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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBoxDevicesConnected = new System.Windows.Forms.ListBox();
            this.labelDevices = new System.Windows.Forms.Label();
            this.labelDevicesWhitelisted = new System.Windows.Forms.Label();
            this.listBoxWhitelistedDevices = new System.Windows.Forms.ListBox();
            this.buttonWhitelist = new System.Windows.Forms.Button();
            this.buttonRemoveDevice = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelBackupLocation = new System.Windows.Forms.Label();
            this.labelArchiveMethod = new System.Windows.Forms.Label();
            this.comboBoxArchiveMethod = new System.Windows.Forms.ComboBox();
            this.labelFileResolver = new System.Windows.Forms.Label();
            this.comboBoxFileResolver = new System.Windows.Forms.ComboBox();
            this.buttonBackupLocation = new System.Windows.Forms.Button();
            this.textBoxFolderPath = new System.Windows.Forms.TextBox();
            this.listBoxEvents = new System.Windows.Forms.ListBox();
            this.labelEvents = new System.Windows.Forms.Label();
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
            this.listBoxWhitelistedDevices.SelectedIndexChanged += new System.EventHandler(this.listBoxWhitelistedDevices_SelectedIndexChanged);
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
            this.groupBox2.Controls.Add(this.labelEvents);
            this.groupBox2.Controls.Add(this.listBoxEvents);
            this.groupBox2.Controls.Add(this.labelBackupLocation);
            this.groupBox2.Controls.Add(this.labelArchiveMethod);
            this.groupBox2.Controls.Add(this.comboBoxArchiveMethod);
            this.groupBox2.Controls.Add(this.labelFileResolver);
            this.groupBox2.Controls.Add(this.comboBoxFileResolver);
            this.groupBox2.Controls.Add(this.buttonBackupLocation);
            this.groupBox2.Controls.Add(this.textBoxFolderPath);
            this.groupBox2.Location = new System.Drawing.Point(218, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 319);
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
            // labelArchiveMethod
            // 
            this.labelArchiveMethod.AutoSize = true;
            this.labelArchiveMethod.Location = new System.Drawing.Point(6, 106);
            this.labelArchiveMethod.Name = "labelArchiveMethod";
            this.labelArchiveMethod.Size = new System.Drawing.Size(82, 13);
            this.labelArchiveMethod.TabIndex = 5;
            this.labelArchiveMethod.Text = "Archive Method";
            // 
            // comboBoxArchiveMethod
            // 
            this.comboBoxArchiveMethod.FormattingEnabled = true;
            this.comboBoxArchiveMethod.Items.AddRange(new object[] {
            "High",
            "Medium",
            "Low"});
            this.comboBoxArchiveMethod.Location = new System.Drawing.Point(6, 122);
            this.comboBoxArchiveMethod.Name = "comboBoxArchiveMethod";
            this.comboBoxArchiveMethod.Size = new System.Drawing.Size(178, 21);
            this.comboBoxArchiveMethod.TabIndex = 4;
            this.comboBoxArchiveMethod.Text = "Default : Medium";
            this.comboBoxArchiveMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxArchiveMethod_SelectedIndexChanged);
            // 
            // labelFileResolver
            // 
            this.labelFileResolver.AutoSize = true;
            this.labelFileResolver.Location = new System.Drawing.Point(6, 66);
            this.labelFileResolver.Name = "labelFileResolver";
            this.labelFileResolver.Size = new System.Drawing.Size(68, 13);
            this.labelFileResolver.TabIndex = 3;
            this.labelFileResolver.Text = "File Resolver";
            // 
            // comboBoxFileResolver
            // 
            this.comboBoxFileResolver.FormattingEnabled = true;
            this.comboBoxFileResolver.Items.AddRange(new object[] {
            "High",
            "Medium",
            "Low"});
            this.comboBoxFileResolver.Location = new System.Drawing.Point(6, 82);
            this.comboBoxFileResolver.Name = "comboBoxFileResolver";
            this.comboBoxFileResolver.Size = new System.Drawing.Size(178, 21);
            this.comboBoxFileResolver.TabIndex = 2;
            this.comboBoxFileResolver.Text = "Default : Medium";
            this.comboBoxFileResolver.SelectedIndexChanged += new System.EventHandler(this.comboBoxFileResolver_SelectedIndexChanged);
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
            // listBoxEvents
            // 
            this.listBoxEvents.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxEvents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxEvents.FormattingEnabled = true;
            this.listBoxEvents.HorizontalScrollbar = true;
            this.listBoxEvents.Location = new System.Drawing.Point(6, 179);
            this.listBoxEvents.Name = "listBoxEvents";
            this.listBoxEvents.Size = new System.Drawing.Size(178, 130);
            this.listBoxEvents.TabIndex = 9;
            // 
            // labelEvents
            // 
            this.labelEvents.AutoSize = true;
            this.labelEvents.Location = new System.Drawing.Point(6, 163);
            this.labelEvents.Name = "labelEvents";
            this.labelEvents.Size = new System.Drawing.Size(40, 13);
            this.labelEvents.TabIndex = 10;
            this.labelEvents.Text = "Events";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 343);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.Label labelFileResolver;
        private System.Windows.Forms.ComboBox comboBoxFileResolver;
        private System.Windows.Forms.Label labelArchiveMethod;
        private System.Windows.Forms.ComboBox comboBoxArchiveMethod;
        private System.Windows.Forms.Label labelBackupLocation;
        private System.Windows.Forms.ListBox listBoxEvents;
        private System.Windows.Forms.Label labelEvents;
    }
}

