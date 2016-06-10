using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DCMViewer
{
    public partial class TouchBrightnessAndContrast : Form
    {
        bool mouseDown;
        Bitmap _sourceBitmap;
        Bitmap _shownBitmap;
        float contrast = 0;
        float brightness = 0;

        public TouchBrightnessAndContrast()
        {
            InitializeComponent();
        }

        private void ComputeFieldValues(int X, int Y, int maxX, int maxY)
        {
            contrast = (((float)X / (float)maxX) - 0.5f) * 400;
            brightness = (((float)Y / (float)maxY) - 0.5f) * 200 * -1; // (* -1) to compensate Y coord being backwards.

            label3.Text = X.ToString();
            label4.Text = Y.ToString();
            label7.Text = contrast.ToString();
            label8.Text = brightness.ToString();
            label11.Text = maxX.ToString();
            label12.Text = maxY.ToString();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofg = new OpenFileDialog();
            if (ofg.ShowDialog() == DialogResult.OK)
            {
                OpenJPG(ofg.FileName);
                pictureBox1.Image = _sourceBitmap;
                label13.Text = _sourceBitmap.Width.ToString();
                label14.Text = _sourceBitmap.Height.ToString();
            }
        }

        private void OpenJPG(string fileName)
        {
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                _sourceBitmap = new Bitmap(stream);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                
                int border = 1; // Compensate panel border.
                int maxX = ((PictureBox)sender).Width - border;
                int maxY = ((PictureBox)sender).Height - border;

                if (e.X < 0 || e.Y < 0 || e.X > maxX || e.Y > maxY)
                {
                    return;
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();
                ComputeFieldValues(e.X, e.Y, maxX, maxY);
                AdjustContrast();
                pictureBox1.Image = _shownBitmap;
                if (_sourceBitmap.PixelFormat == PixelFormat.Format8bppIndexed && checkBox1.Checked == false)
                {
                    pictureBox1.Image.Palette = _sourceBitmap.Palette;
                }
                pictureBox1.Refresh();
                sw.Stop();
                System.Diagnostics.Debug.WriteLine("Showtime: " + sw.ElapsedMilliseconds + "ms.");
            }
        }

        public unsafe void AdjustContrast()
        {
            BitmapData bDataSource = _sourceBitmap.LockBits(new Rectangle(0, 0, _sourceBitmap.Width, _sourceBitmap.Height), ImageLockMode.ReadOnly, _sourceBitmap.PixelFormat);
            _shownBitmap = new Bitmap(_sourceBitmap.Width, _sourceBitmap.Height, _sourceBitmap.PixelFormat);
            BitmapData bDataDest = _shownBitmap.LockBits(new Rectangle(0, 0, _sourceBitmap.Width, _sourceBitmap.Height), ImageLockMode.ReadWrite, _sourceBitmap.PixelFormat);
            byte bitsPerPixel = (byte)Image.GetPixelFormatSize(bDataSource.PixelFormat);
            label17.Text = bitsPerPixel.ToString();

            byte* scan0Source = (byte*)bDataSource.Scan0.ToPointer();
            byte* scan0Dest = (byte*)bDataDest.Scan0.ToPointer();
            byte* colorSource = scan0Source;
            byte* colorDest = scan0Dest;
            float finalContrast = 0; // float 32 vs double 64: double +10ms 
            long imageByteSize = bDataSource.Height * bDataSource.Width * (bitsPerPixel / 8);

            contrast = (200.0f + contrast) / 200.0f;
            contrast *= contrast;

            brightness = (100 + brightness) / 100.0f;

            Stopwatch watchdog = new Stopwatch();
            watchdog.Start();

            for (int i = 0; i < imageByteSize; ++i)
            {
                finalContrast = ((((*colorSource / 255.0f) - 0.5f) * contrast) + 0.5f) * 255;
                finalContrast = finalContrast * brightness;

                if (finalContrast < 0) finalContrast = 0;
                if (finalContrast > 255) finalContrast = 255;

                *colorDest = (byte)finalContrast;
                ++colorSource;
                ++colorDest;
            }

            Console.WriteLine("AdjContrast for-loop time: " + watchdog.ElapsedMilliseconds + " / " + watchdog.ElapsedTicks + " ticks.");
            label19.Text = watchdog.ElapsedMilliseconds.ToString();

            _sourceBitmap.UnlockBits(bDataSource);
            _shownBitmap.UnlockBits(bDataDest);
        }
    }
}
