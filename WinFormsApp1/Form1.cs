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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{

    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer aTimer;
        // biến đếm ngược khi chọn ngày giờ livestream
        private int startLiveStreamCounter = 0;
        private int endLiveStreamCounter = 0;
        private int totalTimeLiveStream = 0;
        String setTimePattern = "\\d{2}:\\d{2}:\\d{2}";
        String baseUrl = "rtmp://a.rtmp.youtube.com/live2/";
        String baseUrl_backup = "rtmp://b.rtmp.youtube.com/live2?backup=1/";
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
            if (rememberText.Length > 0)
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
                }
                else if (rememberText[0] == "remember :true")
                {

                    if (!rememberText[1].Contains("select video :") || !rememberText[2].Contains("stream code :") || !rememberText[3].Contains("type :"))
                    {
                        return;
                    }
                    //string strSetTime = rememberText[4].ToString().Substring(6);

                    //Match m = Regex.Match(strSetTime, setTimePattern, RegexOptions.IgnoreCase);
                    //if (m.Success)
                    //{
                    //    maskedTextBox1.Text = setTimePattern;
                    //}
                    //else
                    //{
                    //    maskedTextBox1.Text = "";
                    //}
                    tbOpen.Text = rememberText[1].ToString().Substring(14);
                    tbLink.Text = rememberText[2].ToString().Substring(13);
                    string typeCheck = rememberText[3].ToString().Substring(6);
                    if (typeCheck == "set time")
                    {
                        rbSetTime.Checked = true;
                    }
                    else if (typeCheck == "live 247")
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
                string typeCheck = rbSetTime.Checked ? "set time" : "live 247";
                string strSetLoop = rbSetTime.Checked ? tbLoop.Text : "";
                string[] input = { "remember :true", "select video :" + tbOpen.Text, "stream code :" + tbLink.Text, "type :" + typeCheck, "set loop :" + strSetLoop };
                SaveDoubleValue(input);
                e.Cancel = false;
            }
            else
            {
                string[] input = { "remember :false" };
                SaveDoubleValue(input);
            }
        }
        static String[] LoadDoubleValue()
        {
            String[] textRemember = { };
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


        private void rbSetTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = true;
            cbRunnow1.Visible = true;
            tbLoop.Visible = true;  
            //maskedTextBox1.Visible = true;

            // Ẩn lựa chọn live 24/7
            dateTimePicker2.Visible = false;
            cbRunnow2.Visible = false;
        }


        private void rbLive247_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Visible = true;
            cbRunnow2.Visible = true;

            // Ẩn lựa chọn set time live
            dateTimePicker1.Visible = false;
            cbRunnow1.Visible = false;
            tbLoop.Visible = false;
            //maskedTextBox1.Visible = false;
        }

        private void cbRunnow1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRunnow1.Checked)
            {
                dateTimePicker1.Enabled = false;
            }
            else
            {
                dateTimePicker1.Enabled = true;
            }
        }

        private void cbRunnow2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRunnow2.Checked)
            {
                dateTimePicker2.Enabled = false;
            }
            else
            {
                dateTimePicker2.Enabled = true;
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            lbMess.Text = "";
            

            // Tạo 1 timer
            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 1000; // 1 second
            DateTime d = DateTime.Now;
            TimeSpan ts;
            if (string.IsNullOrEmpty(tbOpen.Text))
            {
                Console.WriteLine("herre ");
                lbMess.Text = "Select video is Empty!";
                return;
            }
            else if (string.IsNullOrEmpty(tbLink.Text))
            {
                Console.WriteLine("herre2 ");
                lbMess.Text = "Stream code is Empty!";
                return;
            }
            else if (rbSetTime.Checked)
            {
                //checkTimeLiveStream();
                //Check tbLoop
                if(string.IsNullOrEmpty(tbLoop.Text)) {
                    lbMess.Text = "Please set loop field!";
                    return;
                }
                //Nếu chọn runnow thì sẽ bỏ qua timer ( set TotalSeconds = -2 )
                ts = cbRunnow1.Checked ? d.Subtract(DateTime.Now.AddSeconds(2)) : d.Subtract(dateTimePicker1.Value);
                System.Diagnostics.Debug.WriteLine("We get a total of {0} seconds", (int)ts.TotalSeconds);
                Console.WriteLine("time : " + (int)ts.TotalSeconds);

                if ((int)ts.TotalSeconds >= 0)
                {
                    lbMess.Text = "Please choose a time in the future";
                    return;
                }
                else
                {
                    startLiveStreamCounter = Math.Abs((int)ts.TotalSeconds);
                    aTimer.Start();
                }

            }
            else if (rbLive247.Checked)
            {
                //Nếu chọn runnow thì sẽ bỏ qua timer ( set TotalSeconds = -2 )
                ts = cbRunnow2.Checked ? d.Subtract(DateTime.Now.AddSeconds(2)) : d.Subtract(dateTimePicker2.Value);
                System.Diagnostics.Debug.WriteLine("We get a total of {0} seconds", (int)ts.TotalSeconds);
                Console.WriteLine("time : " + (int)ts.TotalSeconds);
                if ((int)ts.TotalSeconds >= 0)
                {
                    lbMess.Text = "Please choose a time in the future";
                    return;
                }
                else
                {
                    startLiveStreamCounter = Math.Abs((int)ts.TotalSeconds);
                    aTimer.Start();
                }
            }
            //lbMess.Text = (int)ts.TotalSeconds + "";
            lbMess.Text = "Started count down in " + DateTime.Now.ToString();

            // Ẩn các nút trên màn hình
            btStart.Enabled = false;
            btCancel.Visible = true;
            tbOpen.Enabled = false;
            tbLink.Enabled = false;
            tbTitle.Enabled = false;
            btOpen.Enabled = false;
            rbSetTime.Enabled = false;
            rbLive247.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            cbRunnow1.Enabled = false;
            cbRunnow2.Enabled = false;
            cbBackup.Enabled = false;
            tbLoop.Enabled = false; 
            //maskedTextBox1.Enabled = false;
        }


        /**
		 * date : 29/11/2022
		 * Author : DDMANH
		 * Function : Đếm ngược thời gian, live stream sẽ bắt đầu vào thời gian đã chọn trên datetimepiker1
		**/
        private void aTimer_Tick(object sender, EventArgs e)
        {
            //lbMess.Text = DateTime.Now.ToString(); //Hiển thị thời gian hiện tại
            startLiveStreamCounter--;

            if (startLiveStreamCounter == 0)
            {
                aTimer.Stop();
                if (rbSetTime.Checked)
                {
                    start_LiveStream("setTime");
                }
                else if (rbLive247.Checked)
                {
                    start_LiveStream("live247");
                }

                btCancel.Visible = false;

                //Hiển thị các nút trên màn hình
                btStart.Enabled = true;
                tbOpen.Enabled = true;
                tbLink.Enabled = true;
                btOpen.Enabled = true;
                tbTitle.Enabled = true;
                rbSetTime.Enabled = true;
                rbLive247.Enabled = true;
                cbRunnow1.Enabled = true;
                cbRunnow2.Enabled = true;
                cbBackup.Enabled = true;
                tbLoop.Enabled = true;
                //maskedTextBox1.Enabled = true;

                if(!cbRunnow1.Checked)
                {
                    dateTimePicker1.Enabled = true;
                }
                if (!cbRunnow2.Checked)
                {
                    dateTimePicker2.Enabled = true;
                }
                lbCountDown.Text = "";
            }

            lbCountDown.Text = startLiveStreamCounter.ToString() + " seconds left to live stream";
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
            var loop = Int32.Parse(tbLoop.Text);
            String contentLoopFile = "";
            //tạo string loop từ đường dẫn video
            for (int i = 0; i < loop; i++)
            {
                contentLoopFile += tbOpen.Text + ";\n";
            }

            contentLoopFile += tbOpen.Text + ";\n";

            //viết vào file loop
            File.WriteAllText(loopFilePath, contentLoopFile);
            //var fileBatName = "D:\\test\\temp.bat";
            var fileBatName = Directory.GetCurrentDirectory() + "\\" + Guid.NewGuid().ToString() + ".bat";
            var batchPath = Path.Combine(Directory.GetCurrentDirectory() + "\\", fileBatName);
            // Câu lệnh v1
            //var batchCode = type=="live247" ?  "title "+tbTitle.Text + "\ncd lib \n:loop\nfor /F \"delims=;\" %%F in (" + loopFilePath + ") DO ffmpeg -re -y -i \"%%F\" -c copy -f mpegts - | ffmpeg -re -f mpegts -i - -c copy -f flv \"rtmp://a.rtmp.youtube.com/live2/" + tbLink.Text + "\"\ngoto loop"
            //                                    : "title " + tbTitle.Text + "\ncd lib \nfor /F \"delims=;\" %%F in (" + loopFilePath + ") DO ffmpeg -re -y -i \"%%F\" -c copy -f mpegts - | ffmpeg -re -f mpegts -i - -c copy -f flv \"rtmp://a.rtmp.youtube.com/live2/" + tbLink.Text + "\"";
            // Câu lệnh v2 : thay đổi live247
            var batchCode = type == "live247" ? "title " + tbTitle.Text + "\ncd lib \n:loop\nffmpeg -re -y -i \"" + tbOpen.Text + "\" -c copy -f flv -flvflags no_duration_filesize \"" + (cbBackup.Checked ? baseUrl_backup : baseUrl)  + tbLink.Text + "\"\ngoto loop"
                                                //: "title " + tbTitle.Text + "\ncd lib \nffmpeg -re -y -stream_loop -1 -i \"" + tbOpen.Text + "\" -c copy -t " + totalTimeLiveStream + " -f flv -flvflags no_duration_filesize \""+ (cbBackup.Checked ? baseUrl_backup : baseUrl) + tbLink.Text + "\"";
                                                : "title " + tbTitle.Text + "\ncd lib \nfor /F \"delims=;\" %%F in (" + loopFilePath + ") DO ffmpeg -re -y -i \"%%F\" -c copy -f mpegts - | ffmpeg -re -f mpegts -i - -c copy -f flv -flvflags no_duration_filesize \"" + (cbBackup.Checked ? baseUrl_backup : baseUrl) + tbLink.Text + "\"";

            Console.WriteLine(batchCode);
            File.WriteAllLines(batchPath, batchCode.Split('\n'));

            // bắt buộc phải có file ffmpeg.exe ( thư viện ) ở file chạy
            // file batch sẽ được chạy và application sẽ dừng cho đến khi file batch đc đóng.
            lbMess.Text = "Tool is running!";
            var process = Process.Start(batchPath);
            process.EnableRaisingEvents = true;
            process.WaitForExit();
            //Provide some time to process the main_Exited event. 


            string typeCheck = rbSetTime.Checked ? "set time" : "live 247";
            string strSetTime = rbSetTime.Checked ? tbLoop.Text : "";
            string[] input = { "remember :true", "select video :" + tbOpen.Text, "stream code :" + tbLink.Text, "type :" + typeCheck, "set time :" + strSetTime };
            SaveDoubleValue(input);


            lbMess.Text = "";
            //tbTitle.Text = batchPath;
            File.Delete(batchPath);
            File.Delete(loopFilePath);

            //ExecuteCmd(batchCode);
        }
        //protected void test(object sender, EventArgs e)
        //      {
        //	string typeCheck = rbPublicNow.Checked ? "public now" : (rbSetTime.Checked ? "set time" : "live 247");
        //	string strLoop = (typeCheck == "public now" || typeCheck == "set time") ? tbLoop.Text : "0";
        //	string[] input = { "remember :true", "select video :" + tbOpen.Text, "stream code :" + tbLink.Text, "type :" + typeCheck, "loop :" + strLoop };
        //	SaveDoubleValue(input);
        //	//File.Delete(batchPath);
        //	//File.Delete(loopFilePath);
        //}
        // Nút hủy đếm ngược
        private void btCancel_Click(object sender, EventArgs e)
        {
            aTimer.Stop();
            btStart.Enabled = true;
            btCancel.Visible = false;
            tbOpen.Enabled = true;
            tbLink.Enabled = true;
            btOpen.Enabled = true;
            tbLoop.Enabled = true;
            tbTitle.Enabled = true;
            rbSetTime.Enabled = true;
            rbLive247.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            cbRunnow1.Enabled = true;
            cbRunnow2.Enabled = true;
            cbBackup.Enabled = true;
            //maskedTextBox1.Enabled = true;
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



        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            if (tbTitle.Text == "")
            {
                this.Text = "Tool support upload video livestream Youtube";
            }
            else
            {
                this.Text = tbTitle.Text;
            }
        }

        //private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        //{
        //    Console.WriteLine(maskedTextBox1.Text);
        //    if (maskedTextBox1.MaskCompleted)
        //    {
        //        Console.WriteLine("full text");
        //    }
        //}


        //private void maskedFocusOut(object sender, EventArgs e)
        //{
        //    checkTimeLiveStream();
        //}

        //private void checkTimeLiveStream()
        //{
        //    Match m = Regex.Match(maskedTextBox1.Text, setTimePattern, RegexOptions.IgnoreCase);
        //    if (maskedTextBox1.MaskCompleted && m.Success)
        //    {
        //        int hh = Int32.Parse(maskedTextBox1.Text.Substring(0, 2));
        //        int mm = Int32.Parse(maskedTextBox1.Text.Substring(3, 2));
        //        int ss = Int32.Parse(maskedTextBox1.Text.Substring(6, 2));
        //        totalTimeLiveStream = hh * 3600 + mm * 60 + ss;
        //        if (totalTimeLiveStream <= 0)
        //        {
        //            lbMess.Text = "Please set time!";
        //            maskedTextBox1.Focus();
        //        }
        //    }
        //    else
        //    {
        //        lbMess.Text = "Please set time!";
        //        maskedTextBox1.Focus();
        //    }
        //}

    }
}
