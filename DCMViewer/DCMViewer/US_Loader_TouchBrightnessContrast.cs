using System;
using System.Windows.Forms;
using PictureFrameControl;
using System.IO;
using System.Drawing;

namespace DCMViewer
{
    public partial class US_Loader_TouchBrightnessContrast : Form
    {
        int howManyPics = 6;
        Bitmap[] bmp = null;

        public US_Loader_TouchBrightnessContrast()
        {
            InitializeComponent();
        }

        private void US_Loader_TouchBrightnessContrast_Shown(object sender, EventArgs e)
        {
            bmp = new Bitmap[howManyPics];
            PictureFrame[] pictureFrames = new PictureFrame[howManyPics]; 
            string filenameTemplate = @".\us\18000000_00{0}.jpg";
            string fullPath = "";

            progressBar1.Visible = true; // aestethic detail.
            for (int i = 0; i < howManyPics; ++i)
            {
                // Load pictures in RAM.
                fullPath = String.Format(filenameTemplate, (i + 1).ToString());
                bmp[i] = new Bitmap(fullPath);
                progressBar1.Value = progressBar1.Maximum * (i + 1) / howManyPics;

                // Show pictures in PictureFrames custom controls.
                pictureFrames[i] = new PictureFrame();
                pictureFrames[i].id = i + 1;
                pictureFrames[i].borderSize = 3;
                pictureFrames[i].status = true;
                pictureFrames[i].pictureBox.Image = bmp[i];
                pictureFrames[i].pictureBox.MouseDown += MouseDownEvent;
                pictureFrames[i].numberShown.MouseDown += MouseDownEvent;
                pictureFrames[i].MouseDown += MouseDownEvent;
                pictureFrames[i].Create();
                flowLayoutPanel1.Controls.Add(pictureFrames[i]);
                this.Refresh();
            }

            pictureBox1.Image = bmp[0];

            // aestethic detail.
            progressBar1.Value = 0;
            progressBar1.Visible = false; 
        }

        private void MouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender is PictureFrame)
                {
                    pictureBox1.Image = bmp[((PictureFrame)sender).id - 1];
                }
                if (sender is PictureBox)
                {
                    pictureBox1.Image = bmp[((PictureFrame)((PictureBox)sender).Parent).id - 1];
                }
                if (sender is Label)
                {
                    pictureBox1.Image = bmp[((PictureFrame)((Label)sender).Parent).id - 1];
                }
            }
        }
    }
}
