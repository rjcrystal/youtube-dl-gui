using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;


namespace youtube_dl_gui
{
  
    public partial class Form1 : Form
    {
       
      public Form1()
        {
            InitializeComponent();
            //this.Text = a+" "+size+" "+time;
        }

        private void dl_Click(object sender, EventArgs e)
        {
            String t;
            t = textBox1.Text;
            if (String.IsNullOrEmpty(t))
            {
                MessageBox.Show("Enter a video link");
            }
            else 
            {
                Process start = new Process();
                start.StartInfo.FileName = @"youtube-dl.exe"; // Specify exe name.
                start.StartInfo.UseShellExecute = false;
                start.StartInfo.RedirectStandardOutput = true;
                start.StartInfo.Arguments = t;
                start.StartInfo.CreateNoWindow = true;
                //
                // Start the process.
                //
                start.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                start.ErrorDataReceived+=new DataReceivedEventHandler(ErrorDataReceived);
                textBox1.Text = " ";
                textBox2.Text = " ";
                start.Start();
                start.BeginOutputReadLine();
            }
        }
        public void ErrorDataReceived(object sendingprocess, DataReceivedEventArgs error)
        {
            if (!String.IsNullOrEmpty(error.Data))
            {

                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show(error.Data);
                    });
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            
 
        }
        public void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            Regex pattern = new Regex(@"\d{1,3}\%", RegexOptions.None);
                if (!String.IsNullOrEmpty(outLine.Data))
                {
                    
                    try
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            if (pattern.IsMatch(outLine.Data))
                            {
                                textBox2.AppendText(outLine.Data);
                            }
                            else
                            {
                                textBox2.AppendText(outLine.Data + "\n");
                            }
                        });
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
            
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lb1_Click(object sender, EventArgs e)
        {

        }
        private void progressBar1_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process[] killer = Process.GetProcessesByName("youtube-dl");
            try
            {
                killer[0].Kill();
            }
            catch (IndexOutOfRangeException q)
            {
                MessageBox.Show("process exited abnormally");
            }
            MessageBox.Show(killer[0]+"download canceled");
            textBox2.Text = "download cancelled";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

     
       }
}
