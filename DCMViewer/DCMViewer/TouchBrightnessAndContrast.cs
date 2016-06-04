using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DCMViewer
{
    public partial class TouchBrightnessAndContrast : Form
    {
        bool panel1_mouseDown;
        Bitmap _sourceBitmap;

        public TouchBrightnessAndContrast()
        {
            InitializeComponent();
        }

        private void ComputeFieldValues(int X, int Y, int maxX, int maxY)
        {
            float contrast = 0;
            float brightness = 0;

            contrast = (((float)X / (float)maxX) - 0.5f) * 400;
            brightness = (((float)Y / (float)maxY) - 0.5f) * 200 * -1; // (* -1) to compensate Y coord being backwards.

            label3.Text = X.ToString();
            label4.Text = Y.ToString();
            label7.Text = contrast.ToString();
            label8.Text = brightness.ToString();
            label11.Text = maxX.ToString();
            label12.Text = maxY.ToString();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (panel1_mouseDown)
            {
                int border = 1; // Compensate panel border.
                int maxX = ((Panel)sender).Width - border;
                int maxY = ((Panel)sender).Height - border;

                ComputeFieldValues(e.X, e.Y, maxX, maxY);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1_mouseDown = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            panel1_mouseDown = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofg = new OpenFileDialog();
            if (ofg.ShowDialog() == DialogResult.OK)
            {
                 OpenJPG(ofg.FileName);

            }
        }

        private void OpenJPG(string fileName)
        {
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                _sourceBitmap = new Bitmap(stream);
            }
        }
    }
}
