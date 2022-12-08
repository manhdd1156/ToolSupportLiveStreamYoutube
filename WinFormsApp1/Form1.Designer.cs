
namespace WinFormsApp1
{
	partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbOpen = new System.Windows.Forms.Label();
            this.tbLink = new System.Windows.Forms.TextBox();
            this.btOpen = new System.Windows.Forms.Button();
            this.lbLink = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.rbLive247 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rbSetTime = new System.Windows.Forms.RadioButton();
            this.tbOpen = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lbMess = new System.Windows.Forms.Label();
            this.lbCountDown = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.cbBackup = new System.Windows.Forms.CheckBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.cbRunnow1 = new System.Windows.Forms.CheckBox();
            this.cbRunnow2 = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbOpen
            // 
            this.lbOpen.AutoSize = true;
            this.lbOpen.Location = new System.Drawing.Point(39, 52);
            this.lbOpen.Name = "lbOpen";
            this.lbOpen.Size = new System.Drawing.Size(70, 15);
            this.lbOpen.TabIndex = 0;
            this.lbOpen.Text = "Select video";
            // 
            // tbLink
            // 
            this.tbLink.Location = new System.Drawing.Point(117, 95);
            this.tbLink.Name = "tbLink";
            this.tbLink.PlaceholderText = "Mã luồng";
            this.tbLink.Size = new System.Drawing.Size(468, 23);
            this.tbLink.TabIndex = 1;
            // 
            // btOpen
            // 
            this.btOpen.Location = new System.Drawing.Point(606, 48);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(75, 23);
            this.btOpen.TabIndex = 2;
            this.btOpen.Text = "Open";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // lbLink
            // 
            this.lbLink.AutoSize = true;
            this.lbLink.Location = new System.Drawing.Point(36, 100);
            this.lbLink.Name = "lbLink";
            this.lbLink.Size = new System.Drawing.Size(73, 15);
            this.lbLink.TabIndex = 3;
            this.lbLink.Text = "Stream code";
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(277, 307);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(112, 41);
            this.btStart.TabIndex = 10;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Live 24/7";
            // 
            // rbLive247
            // 
            this.rbLive247.AutoSize = true;
            this.rbLive247.Location = new System.Drawing.Point(98, 52);
            this.rbLive247.Name = "rbLive247";
            this.rbLive247.Size = new System.Drawing.Size(14, 13);
            this.rbLive247.TabIndex = 15;
            this.rbLive247.UseVisualStyleBackColor = true;
            this.rbLive247.CheckedChanged += new System.EventHandler(this.rbLive247_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "set time live";
            // 
            // rbSetTime
            // 
            this.rbSetTime.AutoSize = true;
            this.rbSetTime.Checked = true;
            this.rbSetTime.Location = new System.Drawing.Point(98, 12);
            this.rbSetTime.Name = "rbSetTime";
            this.rbSetTime.Size = new System.Drawing.Size(14, 13);
            this.rbSetTime.TabIndex = 13;
            this.rbSetTime.TabStop = true;
            this.rbSetTime.UseVisualStyleBackColor = true;
            this.rbSetTime.CheckedChanged += new System.EventHandler(this.rbSetTime_CheckedChanged);
            // 
            // tbOpen
            // 
            this.tbOpen.Location = new System.Drawing.Point(117, 49);
            this.tbOpen.Name = "tbOpen";
            this.tbOpen.PlaceholderText = "Đường dẫn video (C:\\Users\\.....)";
            this.tbOpen.Size = new System.Drawing.Size(468, 23);
            this.tbOpen.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.rbLive247);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rbSetTime);
            this.panel1.Location = new System.Drawing.Point(19, 142);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 87);
            this.panel1.TabIndex = 15;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lbMess
            // 
            this.lbMess.AutoSize = true;
            this.lbMess.ForeColor = System.Drawing.Color.Red;
            this.lbMess.Location = new System.Drawing.Point(117, 288);
            this.lbMess.Name = "lbMess";
            this.lbMess.Size = new System.Drawing.Size(0, 15);
            this.lbMess.TabIndex = 17;
            // 
            // lbCountDown
            // 
            this.lbCountDown.AutoSize = true;
            this.lbCountDown.ForeColor = System.Drawing.Color.Red;
            this.lbCountDown.Location = new System.Drawing.Point(390, 288);
            this.lbCountDown.Name = "lbCountDown";
            this.lbCountDown.Size = new System.Drawing.Size(0, 15);
            this.lbCountDown.TabIndex = 19;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(473, 307);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(112, 41);
            this.btCancel.TabIndex = 20;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Visible = false;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(117, 12);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.PlaceholderText = "Tool support upload video livestream Youtube";
            this.tbTitle.Size = new System.Drawing.Size(468, 23);
            this.tbTitle.TabIndex = 22;
            this.tbTitle.TextChanged += new System.EventHandler(this.tbTitle_TextChanged);
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Location = new System.Drawing.Point(51, 15);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(58, 15);
            this.lbTitle.TabIndex = 21;
            this.lbTitle.Text = "Set Name";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(243, 149);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker1.TabIndex = 18;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.timePicker_ValueChanged);
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(162, 149);
            this.maskedTextBox1.Mask = "00:00:00";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(51, 23);
            this.maskedTextBox1.TabIndex = 20;
            this.maskedTextBox1.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.maskedTextBox1_MaskInputRejected);
            this.maskedTextBox1.Leave += new System.EventHandler(this.maskedFocusOut);
            // 
            // cbBackup
            // 
            this.cbBackup.AutoSize = true;
            this.cbBackup.Location = new System.Drawing.Point(606, 99);
            this.cbBackup.Name = "cbBackup";
            this.cbBackup.Size = new System.Drawing.Size(65, 19);
            this.cbBackup.TabIndex = 23;
            this.cbBackup.Text = "Backup";
            this.cbBackup.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(243, 187);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker2.TabIndex = 24;
            this.dateTimePicker2.Visible = false;
            // 
            // cbRunnow1
            // 
            this.cbRunnow1.AutoSize = true;
            this.cbRunnow1.Location = new System.Drawing.Point(473, 154);
            this.cbRunnow1.Name = "cbRunnow1";
            this.cbRunnow1.Size = new System.Drawing.Size(73, 19);
            this.cbRunnow1.TabIndex = 25;
            this.cbRunnow1.Text = "Run now";
            this.cbRunnow1.UseVisualStyleBackColor = true;
            this.cbRunnow1.CheckedChanged += new System.EventHandler(this.cbRunnow1_CheckedChanged);
            // 
            // cbRunnow2
            // 
            this.cbRunnow2.AutoSize = true;
            this.cbRunnow2.Location = new System.Drawing.Point(473, 191);
            this.cbRunnow2.Name = "cbRunnow2";
            this.cbRunnow2.Size = new System.Drawing.Size(73, 19);
            this.cbRunnow2.TabIndex = 26;
            this.cbRunnow2.Text = "Run now";
            this.cbRunnow2.UseVisualStyleBackColor = true;
            this.cbRunnow2.Visible = false;
            this.cbRunnow2.CheckedChanged += new System.EventHandler(this.cbRunnow2_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 429);
            this.Controls.Add(this.cbRunnow2);
            this.Controls.Add(this.cbRunnow1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.cbBackup);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.lbCountDown);
            this.Controls.Add(this.lbMess);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbOpen);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.lbLink);
            this.Controls.Add(this.btOpen);
            this.Controls.Add(this.tbLink);
            this.Controls.Add(this.lbOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tool support upload video livestream Youtube";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.Load += new System.EventHandler(this.Form_load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbOpen;
		private System.Windows.Forms.TextBox tbLink;
		private System.Windows.Forms.Button btOpen;
		private System.Windows.Forms.Label lbLink;
		private System.Windows.Forms.Button btStart;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton rbLive247;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton rbSetTime;
		private System.Windows.Forms.TextBox tbOpen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lbMess;
        private System.Windows.Forms.Label lbCountDown;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.CheckBox cbBackup;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.CheckBox cbRunnow1;
        private System.Windows.Forms.CheckBox cbRunnow2;
    }
}

