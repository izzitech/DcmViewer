using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DCMViewer
{
    public partial class TouchResponse : Form
    {
        bool panel1_mouseDown;

        public TouchResponse()
        {
            InitializeComponent();
        }
        
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("panel1_MouseDown");

            panel1_mouseDown = true;
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

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("panel1_MouseUp");
            panel1_mouseDown = false;
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
    }
}
