using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
	
	public partial class Form1 : Form
	{
		private System.Windows.Forms.Timer aTimer;
		// biến đếm ngược khi chọn ngày giờ livestream
		private int counter = 0;
		public Form1()
		{
			InitializeComponent();
			
		}

		private void Form_load(object sender, EventArgs e)
		{
			//var loopFileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";
			//var loopFilePath = Path.Combine(Environment.GetEnvironmentVariable("temp"), loopFileName);
			//lbMess.Text = Directory.GetCurrentDirectory() +"\\" + Guid.NewGuid().ToString();
			String[] rememberText;
			rememberText = LoadDoubleValue();
			if(rememberText.Length>0)
            {
				if (rememberText.Length < 5) // Nếu không đủ 5 dòng có nghĩa là sai định dạng => cũng return;
				{
					return;

				}
				else if (rememberText[0].Length == 0 || (rememberText[0].Length > 0 && !rememberText[0].Contains("remember"))) // nếu dòng 1 trong file save.dat không phải remember: thì thôi
				{
					return;
                }
				else if (rememberText[0].Length > 0 && rememberText[0].Contains("remember :false")) // nếu là remember false cũng return, vì không muốn lưu
				{
					return;
                }else if(rememberText[0] == "remember :true")
                {

					if (!rememberText[1].Contains("select video :") || !rememberText[2].Contains("stream code :") || !rememberText[3].Contains("type :") || !rememberText[4].Contains("loop :"))
                    {
						return;
                    }
					string strLoop = rememberText[4].ToString().Substring(6);
					int intLoop = 0;
					if (Int32.TryParse(strLoop,out intLoop)) {
						tbLoop.Text = intLoop + "";
					}else
                    {
						return;
                    }
					tbOpen.Text = rememberText[1].ToString().Substring(14);
					tbLink.Text = rememberText[2].ToString().Substring(13);
					string typeCheck = rememberText[3].ToString().Substring(6);
					if(typeCheck=="set time")
                    {
						rbSetTime.Checked = true;
                    }else if(typeCheck == "live 247")
                    {
						rbLive247.Checked = true;
                    }
				}
			}
			
		}
		private void Form_Closing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Do you want to save changes to your text?", "My Application",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				// Cancel the Closing event from closing the form.
				e.Cancel = true;
				string typeCheck = rbPublicNow.Checked ? "public now" : (rbSetTime.Checked ? "set time" : "live 247");
				string strLoop = (typeCheck == "public now" || typeCheck == "set time") ? tbLoop.Text : "0";
				string[] input = {"remember :true", "select video :"+tbOpen.Text, "stream code :"+tbLink.Text,"type :"+ typeCheck,"loop :"+strLoop};
				SaveDoubleValue(input);
				e.Cancel = false;
			}else
            {
				string[] input = { "remember :false"};
				SaveDoubleValue(input);
			}
		}
		static String[] LoadDoubleValue()
		{
			String[] textRemember = {};
			if (File.Exists("save.dat"))
			{
				return File.ReadAllLines("save.dat");
			}
			return textRemember;
		}

		static void SaveDoubleValue(String[] val)
		{
			File.WriteAllLines("save.dat", val);
		}

		private void tbLoop_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}
		public void SetText(string text)
		{

		}
		private void btOpen_Click(object sender, EventArgs e)
        {
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					var sr = new StreamReader(openFileDialog1.FileName);
					tbOpen.Text = openFileDialog1.FileName;
					//SetText(sr.ReadToEnd());
				}
				catch (SecurityException ex)
				{
					MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
					$"Details:\n\n{ex.StackTrace}");
				}
			}
		}

        private void rbPublicNow_CheckedChanged(object sender, EventArgs e)
        {
			lbLoop.Location = new Point(4, 5);
			tbLoop.Location = new Point(44, 2);
			lbLoop.Visible = true;
			tbLoop.Visible = true;
			dateTimePicker1.Visible = false;
		}

        private void rbSetTime_CheckedChanged(object sender, EventArgs e)
        {
			lbLoop.Location = new Point(7, 53);
			tbLoop.Location = new Point(47, 50);
			lbLoop.Visible = true;
			tbLoop.Visible = true;
			dateTimePicker1.Visible = true;
		}

        private void rbLive247_CheckedChanged(object sender, EventArgs e)
        {
			lbLoop.Visible = false;
			tbLoop.Visible = false;
			dateTimePicker1.Visible = false;
		}
		
		private void btStart_Click(object sender, EventArgs e)
        {
			lbMess.Text = "";
			if(tbOpen.Text=="")
            {
				lbMess.Text = "Select video is Empty!";
            }else if(tbLink.Text == "")
            {
				lbMess.Text = "Stream code is Empty!";
			}else if (rbPublicNow.Checked)
			{
				if(tbLoop.Text == "")
                {
					lbMess.Text = "Loop is Empty!";
					return;
				}
				start_LiveStream("publicNow");

			}
			else if (rbSetTime.Checked)
			{
				if (tbLoop.Text == "")
                {
					lbMess.Text = "Day and Time Pud and Loop is Empty!";
					return;
				}
				DateTime d = DateTime.Now;
				var ts = d.Subtract(dateTimePicker1.Value);
				System.Diagnostics.Debug.WriteLine("We get a total of {0} seconds", (int)ts.TotalSeconds);
				lbMess.Text = (int)ts.TotalSeconds + "";
				if ((int)ts.TotalSeconds>0)
				{
					lbMess.Text = "Please choose a future time";

				}else
				{
					aTimer = new System.Windows.Forms.Timer();

					aTimer.Tick += new EventHandler(aTimer_Tick);

					aTimer.Interval = 1000; // 1 second
					counter = Math.Abs((int)ts.TotalSeconds);
					aTimer.Start();

					lbMess.Text = "Started count down in " + DateTime.Now.ToString();
					btStart.Enabled = false;
					btCancel.Visible = true;
					tbOpen.Enabled = false;
					tbLink.Enabled = false;
					tbLoop.Enabled = false;
					btOpen.Enabled = false;
					rbPublicNow.Enabled = false;
					rbSetTime.Enabled = false;
					rbLive247.Enabled = false;
					dateTimePicker1.Enabled = false;
				}
				
				
			}
			else if(rbLive247.Checked) {
				start_LiveStream("live247");
			}
		}
		public void start_LiveStream(string type)
		{
			DateTime localDate = DateTime.Now;
			// hiển thị ngày giờ hiện tại bắt đầu nhấn nút start
			lbMess.Text = "Started in " + DateTime.Now.ToString("HH:mm:ss tt - dd/MM/yyyy");
			//string ffmpegPath = Directory.GetCurrentDirectory() + "\\ffmpeg.exe";
			//File.Copy("../../../lib/ffmpeg.exe", ffmpegPath, true);
            // tạo file text lưu thông tin cần lặp lại 
            //var loopFileName = "D:\\test\\temp.txt";
			var loopFileName = Directory.GetCurrentDirectory() + "\\" + Guid.NewGuid().ToString() + ".txt";
			//var loopFileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";
			//var loopFilePath = Path.Combine(Environment.GetEnvironmentVariable("temp"), loopFileName);
			var loopFilePath = Path.Combine(Directory.GetCurrentDirectory() + "\\", loopFileName);
			
			// lấy số lượng loop
			var loop = type=="live247" ? 1: Int32.Parse(tbLoop.Text);
			String contentLoopFile = "";
			// tạo string loop từ đường dẫn video
			for (int i = 0; i < loop; i++)
			{
				contentLoopFile += tbOpen.Text + ";";
			}
			// viết vào file loop
			File.WriteAllText(loopFilePath, contentLoopFile);
			//var fileBatName = "D:\\test\\temp.bat";
			var fileBatName = Directory.GetCurrentDirectory() + "\\" + Guid.NewGuid().ToString() + ".bat";
			var batchPath = Path.Combine(Directory.GetCurrentDirectory() + "\\", fileBatName);
			
				var batchCode = type=="live247" ?  ":loop\nfor /F \"delims=;\" %%F in (" + loopFilePath + ") DO ffmpeg -re -y -i \"%%F\" -c copy -f mpegts - | ffmpeg -re -f mpegts -i - -c copy -f flv \"rtmp://a.rtmp.youtube.com/live2/" + tbLink.Text + "\"\ngoto loop"
												: "for /F \"delims=;\" %%F in (" + loopFilePath + ") DO ffmpeg -re -y -i \"%%F\" -c copy -f mpegts - | ffmpeg -re -f mpegts -i - -c copy -f flv \"rtmp://a.rtmp.youtube.com/live2/" + tbLink.Text + "\"";
			File.WriteAllText(batchPath, batchCode);

            // bắt buộc phải có file ffmpeg.exe ( thư viện ) ở file chạy
            // file batch sẽ được chạy và application sẽ dừng cho đến khi file batch đc đóng.
			lbMess.Text = "Tool is running!";
			Process.Start(batchPath).WaitForExit();
			lbMess.Text = "";
            File.Delete(batchPath);
            File.Delete(loopFilePath);

            //ExecuteCmd(batchCode);
        }
		// Nút hủy đếm ngược
		private void btCancel_Click(object sender, EventArgs e)
		{
			aTimer.Stop();
			btStart.Enabled = true;
			btCancel.Visible = false;
			tbOpen.Enabled = true;
			tbLink.Enabled = true;
			tbLoop.Enabled = true;
			btOpen.Enabled = true;
			rbPublicNow.Enabled = true;
			rbSetTime.Enabled = true;
			rbLive247.Enabled = true;
			dateTimePicker1.Enabled = true;
			lbMess.Text = "Cancelled count down!";
			lbCountDown.Text = "";

		}
		
        private void timePicker_ValueChanged(object sender, EventArgs e)
        {
			//var picker = (DateTimePicker)sender;
			//if (picker == null) return;

			//DateTime d = DateTime.Now;
			//var ts = d.Subtract(picker.Value);
			//System.Diagnostics.Debug.WriteLine("We get a total of {0} seconds", (int)ts.TotalSeconds);
			//lbMess.Text = (int)ts.TotalSeconds + "";
		}

        private void aTimer_Tick(object sender, EventArgs e)
        {
			//lbMess.Text = DateTime.Now.ToString(); //Hiển thị thời gian hiện tại
			counter--;

			if (counter == 0)
            {
				aTimer.Stop();
				start_LiveStream("setTime");
				btCancel.Visible = false;
				tbOpen.Enabled = true;
				tbLink.Enabled = true;
				tbLoop.Enabled = true;
				btOpen.Enabled = true;
				rbPublicNow.Enabled = true;
				rbSetTime.Enabled = true;
				rbLive247.Enabled = true;
				dateTimePicker1.Enabled = true;
				lbCountDown.Text = "";
			}

			lbCountDown.Text = counter.ToString() + " seconds left";
		}

        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
			if(tbTitle.Text=="")
            {
				this.Text = "Tool support upload video livestream Youtube";
			}else
            {
				this.Text = tbTitle.Text;
			}
        }
    }
}
