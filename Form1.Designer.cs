namespace ReadTerminals
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axBioBridgeSDK1 = new AxBioBridgeSDKLib.AxBioBridgeSDK();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabData = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnReadSaveSMS = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnClearList = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnReadSave = new System.Windows.Forms.Button();
            this.cbxPlace = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDevice = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddDevice = new System.Windows.Forms.Button();
            this.txtIPAdd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDevID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPlace = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.chkForTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.axBioBridgeSDK1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabData.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabSetting.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevice)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axBioBridgeSDK1
            // 
            this.axBioBridgeSDK1.Enabled = true;
            this.axBioBridgeSDK1.Location = new System.Drawing.Point(454, 343);
            this.axBioBridgeSDK1.Name = "axBioBridgeSDK1";
            this.axBioBridgeSDK1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axBioBridgeSDK1.OcxState")));
            this.axBioBridgeSDK1.Size = new System.Drawing.Size(110, 55);
            this.axBioBridgeSDK1.TabIndex = 0;
            this.axBioBridgeSDK1.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabData);
            this.tabControl1.Controls.Add(this.tabSetting);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(564, 337);
            this.tabControl1.TabIndex = 1;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.groupBox5);
            this.tabData.Controls.Add(this.groupBox4);
            this.tabData.Controls.Add(this.groupBox3);
            this.tabData.Location = new System.Drawing.Point(4, 22);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(3);
            this.tabData.Size = new System.Drawing.Size(556, 311);
            this.tabData.TabIndex = 0;
            this.tabData.Text = "Read & Save";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.btnReadSaveSMS);
            this.groupBox5.Location = new System.Drawing.Point(280, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(270, 118);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Read SMS";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(19, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(197, 46);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "Reading SMS is automated sequentially after 1 hour. To stop the reading please cl" +
    "ick the button bellow.";
            // 
            // btnReadSaveSMS
            // 
            this.btnReadSaveSMS.Location = new System.Drawing.Point(19, 80);
            this.btnReadSaveSMS.Name = "btnReadSaveSMS";
            this.btnReadSaveSMS.Size = new System.Drawing.Size(197, 23);
            this.btnReadSaveSMS.TabIndex = 3;
            this.btnReadSaveSMS.Text = "Stop";
            this.btnReadSaveSMS.UseVisualStyleBackColor = true;
            this.btnReadSaveSMS.Click += new System.EventHandler(this.btnReadSaveSMS_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBox1);
            this.groupBox4.Location = new System.Drawing.Point(8, 132);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(542, 173);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Show";
            // 
            // btnClearList
            // 
            this.btnClearList.Location = new System.Drawing.Point(479, 343);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(75, 23);
            this.btnClearList.TabIndex = 1;
            this.btnClearList.Text = "Clear";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(6, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(530, 147);
            this.listBox1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnReadSave);
            this.groupBox3.Controls.Add(this.cbxPlace);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(8, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(266, 118);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Read Access Log";
            // 
            // btnReadSave
            // 
            this.btnReadSave.Enabled = false;
            this.btnReadSave.Location = new System.Drawing.Point(97, 80);
            this.btnReadSave.Name = "btnReadSave";
            this.btnReadSave.Size = new System.Drawing.Size(157, 23);
            this.btnReadSave.TabIndex = 2;
            this.btnReadSave.Text = "Read && Save Log";
            this.btnReadSave.UseVisualStyleBackColor = true;
            this.btnReadSave.Click += new System.EventHandler(this.btnReadSave_Click);
            // 
            // cbxPlace
            // 
            this.cbxPlace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPlace.Enabled = false;
            this.cbxPlace.FormattingEnabled = true;
            this.cbxPlace.Location = new System.Drawing.Point(97, 19);
            this.cbxPlace.Name = "cbxPlace";
            this.cbxPlace.Size = new System.Drawing.Size(157, 21);
            this.cbxPlace.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Place";
            // 
            // tabSetting
            // 
            this.tabSetting.Controls.Add(this.groupBox2);
            this.tabSetting.Controls.Add(this.groupBox1);
            this.tabSetting.Location = new System.Drawing.Point(4, 22);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetting.Size = new System.Drawing.Size(556, 311);
            this.tabSetting.TabIndex = 1;
            this.tabSetting.Text = "Settings";
            this.tabSetting.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDevice);
            this.groupBox2.Location = new System.Drawing.Point(302, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 182);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Show";
            // 
            // dgvDevice
            // 
            this.dgvDevice.AllowUserToAddRows = false;
            this.dgvDevice.AllowUserToDeleteRows = false;
            this.dgvDevice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDevice.Location = new System.Drawing.Point(6, 19);
            this.dgvDevice.Name = "dgvDevice";
            this.dgvDevice.ReadOnly = true;
            this.dgvDevice.RowHeadersWidth = 25;
            this.dgvDevice.Size = new System.Drawing.Size(236, 157);
            this.dgvDevice.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddDevice);
            this.groupBox1.Controls.Add(this.txtIPAdd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtModel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDevID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPlace);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 182);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add New Device";
            // 
            // btnAddDevice
            // 
            this.btnAddDevice.Location = new System.Drawing.Point(114, 153);
            this.btnAddDevice.Name = "btnAddDevice";
            this.btnAddDevice.Size = new System.Drawing.Size(75, 23);
            this.btnAddDevice.TabIndex = 8;
            this.btnAddDevice.Text = "Add";
            this.btnAddDevice.UseVisualStyleBackColor = true;
            this.btnAddDevice.Click += new System.EventHandler(this.btnAddDevice_Click);
            // 
            // txtIPAdd
            // 
            this.txtIPAdd.Location = new System.Drawing.Point(114, 109);
            this.txtIPAdd.Name = "txtIPAdd";
            this.txtIPAdd.Size = new System.Drawing.Size(144, 20);
            this.txtIPAdd.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "IP Address";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(114, 80);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(144, 20);
            this.txtModel.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Model";
            // 
            // txtDevID
            // 
            this.txtDevID.Location = new System.Drawing.Point(114, 50);
            this.txtDevID.Name = "txtDevID";
            this.txtDevID.Size = new System.Drawing.Size(144, 20);
            this.txtDevID.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Device ID";
            // 
            // txtPlace
            // 
            this.txtPlace.Location = new System.Drawing.Point(114, 20);
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.Size = new System.Drawing.Size(144, 20);
            this.txtPlace.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Place";
            // 
            // chkForTime
            // 
            this.chkForTime.Enabled = true;
            this.chkForTime.Tick += new System.EventHandler(this.chkForTime_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 410);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.axBioBridgeSDK1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Read Access Control & SMS";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axBioBridgeSDK1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabSetting.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevice)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxBioBridgeSDKLib.AxBioBridgeSDK axBioBridgeSDK1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.TabPage tabSetting;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtIPAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDevID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPlace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddDevice;
        private System.Windows.Forms.DataGridView dgvDevice;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbxPlace;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnReadSave;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnReadSaveSMS;
        private System.Windows.Forms.Timer chkForTime;
        private System.Windows.Forms.TextBox textBox1;
    }
}

