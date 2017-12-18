using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace camera
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		private FilterInfoCollection webcam;
		private VideoCaptureDevice cam;

		private void Form1_Load(object sender, EventArgs e)
		{
			webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			foreach (FilterInfo VideoCaptureDevice in webcam)
			{
				comboBox1.Items.Add(VideoCaptureDevice.Name);
			}
			comboBox1.SelectedIndex = 0;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
			cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
			cam.Start();
		}

		void cam_NewFrame(object sender, NewFrameEventArgs eventargs)
		{
			Bitmap bit = (Bitmap)eventargs.Frame.Clone();
			pictureBox1.Image = bit;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (cam.IsRunning)
			{
				cam.Stop();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			saveFileDialog1.InitialDirectory = "C:/Users/amr/Desktop";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				pictureBox1.Image.Save(saveFileDialog1.FileName);
			}
		}
	}
}
