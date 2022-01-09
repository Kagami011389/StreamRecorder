using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace hello_world
{
    public partial class Form1 : Form
    {
        private TimeStampProcess time = new TimeStampProcess();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        private void button1_Click(object sender, EventArgs e)
        {   
            if(this.textBox1.Text == "")
            {
                MessageBox.Show("請更改直播時間");
            }
            else
            {
                this.textBox1.AppendText(Environment.NewLine);
                this.textBox1.AppendText(time.GetTimeStamp(new DateTime(2021, 12, 19, 12, 00, 00)));
                this.textBox1.Focus();
            }
            
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = $"{DateTime.Now.Year.ToString()}/" +
                $"{DateTime.Now.Month.ToString()}/{DateTime.Now.Day.ToString()}" +
                $" {DateTime.Now.Hour.ToString().PadLeft(2, '0')}:{DateTime.Now.Minute.ToString().PadLeft(2, '0')}" +
                $":{DateTime.Now.Second.ToString().PadLeft(2,'0')}";
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            time.SetStreamDateTime(this.textBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string path = $"{this.textBox2.Text.Replace('/', '_').Replace(':', '_')}.txt";
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(this.textBox1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ "不可使用字元<（小於）>（大於）：（冒號）“（雙引號）/（正斜線）\\（反斜線）|（管線; pipe）？（問號）*（星號）");
            }
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Text;
            string[] textArray;
            string[] messageArray;
            string[] timeArray;
            int shiftTime = 0;
            
            try
            {
                shiftTime = Convert.ToInt32(this.textBox3.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"時間位移錯誤:{ex.Message}");
                //this.textBox1.AppendText(Environment.NewLine);
                //this.textBox1.AppendText($"{ex.Message}");
                return;
            }
            
            Text = this.textBox1.Text;
            textArray = Text.Split(new[] { "\r\n" }, StringSplitOptions.None);
            timeArray = new string[textArray.Length];
            messageArray = new string[textArray.Length];
            this.textBox1.AppendText($"\r\n\r\ntime shift processing...\r\n");

            for (int i=0;i<textArray.Length;i++)
            {
                try 
                {
                    timeArray[i] = textArray[i].Substring(0, 8);
                    timeArray[i] = time.TimeShift(timeArray[i], shiftTime);
                    messageArray[i] = textArray[i].Substring(8);
                    this.textBox1.AppendText($"\r\n{timeArray[i]}{messageArray[i]}");
                    
                }
                catch(Exception ex)
                {
                    if (textArray[i] != "time shift processing...")
                    {
                        this.textBox1.AppendText($"{textArray[i]}");
                    }
                    else
                    {
                        this.textBox1.AppendText(Environment.NewLine);
                        this.textBox1.AppendText(Environment.NewLine);
                        this.textBox1.AppendText($"time shift processing...");
                        this.textBox1.AppendText(Environment.NewLine);
                        continue;
                    }

                    
                }
            }
        }
    }
    public class TimeStampProcess
    {
        private DateTime streamDateTime;
        public String GetTimeStamp(DateTime time)
        {
            time = streamDateTime;
            return DateTime.Now.Subtract(time).ToString().Substring(0, 8);
        }

        public void SetStreamDateTime(string times)
        {
            int[] time_int;
            string[] time_string;
            /*2021/12/19 13:32:05*/
            char[] sep = { '/', ':' ,' '};
            time_string = times.Split(sep,StringSplitOptions.RemoveEmptyEntries);
  
            
            time_int = Array.ConvertAll(time_string,s => int.Parse(s)); 

            streamDateTime = new DateTime(time_int[0], time_int[1], time_int[2], time_int[3], time_int[4], time_int[5]);
        }

        public string TimeShift(string time,int shiftSecond)
        {
            int hour, minute, second;
            hour = Convert.ToInt32(time.Split(':')[0]);
            minute = Convert.ToInt32(time.Split(':')[1]);
            second = Convert.ToInt32(time.Split(':')[2]);
            second += shiftSecond;
            if(second >= 60)
            {
                second -= 60;
                minute++;
            }
            if(second < 0)
            {
                minute--;
                second += 60;
            }
            if(minute >= 60)
            {
                minute -= 60;
                hour++;
            }
            if (minute < 0)
            {
                minute += 60;
                hour--;
            }
            time = $"{Convert.ToString(hour).PadLeft(2,'0')}:{Convert.ToString(minute).PadLeft(2, '0')}:{Convert.ToString(second).PadLeft(2,'0')}";
            return time;
        }

       
    }
}
