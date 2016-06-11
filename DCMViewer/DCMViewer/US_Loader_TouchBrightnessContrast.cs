using System;
using System.Windows.Forms;
using PictureFrameControl;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace DCMViewer
{
    public partial class US_Loader_TouchBrightnessContrast : Form
    {
        bool mouseDown = false;
        bool easterEggON = false;
        int howManyPics = 0;
        int selectedPic = 0;
        int border = 1; // Compensates Panel control border.
        int maxX;
        int maxY;
        int maxLoadableFiles = 150;
        float contrast = 0;
        float brightness = 0;
        
        string[] fullPath = null;
        Bitmap[] sourceBitmap = null;
        Bitmap[] shownBitmap = null;
        PictureFrame[] pictureFrames = null;

        // For US Psycho.
        Random random = new Random();
        Timer autoPsycho = new Timer();

        public US_Loader_TouchBrightnessContrast()
        {
            InitializeComponent();

            maxX = pictureBoxMain.Width - border;
            maxY = pictureBoxMain.Height - border;
            lbl_pictureBoxMaxX.Text = maxX.ToString();
            lbl_pictureBoxMaxY.Text = maxY.ToString();


        }

        private void US_Loader_TouchBrightnessContrast_Shown(object sender, EventArgs e)
        {
            LoadPics();
        }

        private void LoadPics()
        {
            if (fullPath == null) return;
            if (fullPath.Length == 0) return;

            howManyPics = fullPath.Length;

            sourceBitmap = new Bitmap[howManyPics];
            shownBitmap = new Bitmap[howManyPics];
            pictureFrames = new PictureFrame[howManyPics];

            // Process pics
            progressBar1.Visible = true; // aestethic detail.
            lbl_loading.Visible = true; // aestethic detail.

            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Visible = false;
            for (int i = 0; i < howManyPics; ++i)
            {
                // Load pictures in RAM.
                sourceBitmap[i] = new Bitmap(fullPath[i]);
                progressBar1.Value = progressBar1.Maximum / (howManyPics / (i + 1));

                // Show pictures in PictureFrames custom controls.
                pictureFrames[i] = new PictureFrame();
                pictureFrames[i].id = i + 1;
                pictureFrames[i].borderSize = 3;
                pictureFrames[i].status = true;
                pictureFrames[i].Width = (int)(pictureFrames[i].Height * (float)((float)sourceBitmap[i].Width / (float)sourceBitmap[i].Height));
                pictureFrames[i].pictureBox.Image = sourceBitmap[i];
                pictureFrames[i].pictureBox.MouseDown += pictureFrameMouseDownEvent;
                pictureFrames[i].numberShown.MouseDown += pictureFrameMouseDownEvent;
                pictureFrames[i].MouseDown += pictureFrameMouseDownEvent;
                pictureFrames[i].Create();
                flowLayoutPanel1.Controls.Add(pictureFrames[i]);
                //flowLayoutPanel1.Refresh();
            }
            flowLayoutPanel1.ResumeLayout();
            flowLayoutPanel1.Visible = true;

            // TODO: it should be _shownBitmap, for saved brightness and contrast.
            pictureBoxMain.Image = sourceBitmap[0];

            // aestethic detail.
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            lbl_loading.Visible = false;

            lbl_pictureWidth.Text = sourceBitmap[selectedPic].Width.ToString();
            lbl_pictureHeight.Text = sourceBitmap[selectedPic].Height.ToString();
            lbl_pixelFormat.Text = sourceBitmap[selectedPic].PixelFormat.ToString();
        }


        private void pictureFrameMouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender is PictureFrame)
                {
                    selectedPic = ((PictureFrame)sender).id - 1;
                }
                if (sender is PictureBox)
                {
                    selectedPic = ((PictureFrame)((PictureBox)sender).Parent).id - 1;
                }
                if (sender is Label)
                {
                    selectedPic = ((PictureFrame)((Label)sender).Parent).id - 1;
                }

                // TODO: it should be _shownBitmap, for saved brightness and contrast.
                pictureBoxMain.Image = sourceBitmap[selectedPic];
            }

            lbl_pictureWidth.Text = sourceBitmap[selectedPic].Width.ToString();
            lbl_pictureHeight.Text = sourceBitmap[selectedPic].Height.ToString();
            lbl_pixelFormat.Text = sourceBitmap[selectedPic].PixelFormat.ToString();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                if (e.X < 0 || e.Y < 0 || e.X > maxX || e.Y > maxY)
                {
                    return;
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();
                ComputeFieldValues(e.X, e.Y, maxX, maxY);
                AdjustContrast();
                pictureBoxMain.Image = shownBitmap[selectedPic];
                pictureFrames[selectedPic].pictureBox.Image = shownBitmap[selectedPic];
                if (sourceBitmap[selectedPic].PixelFormat == PixelFormat.Format8bppIndexed && !easterEggON)
                {
                    pictureBoxMain.Image.Palette = sourceBitmap[selectedPic].Palette;
                    pictureFrames[selectedPic].pictureBox.Image.Palette = sourceBitmap[selectedPic].Palette; 
                }
                pictureBoxMain.Refresh();
                pictureFrames[selectedPic].pictureBox.Refresh();
                sw.Stop();
                System.Diagnostics.Debug.WriteLine("Showtime: " + sw.ElapsedMilliseconds + "ms.");
            }
        }

        private void ComputeFieldValues(int X, int Y, int maxX, int maxY)
        {
            contrast = (((float)X / (float)maxX) - 0.5f) * 400;
            brightness = (((float)Y / (float)maxY) - 0.5f) * 200 * -1; // (* -1) to compensate Y coord being backwards.

            lbl_pictureBoxMousePositionX.Text = X.ToString();
            lbl_pictureBoxMousePositionY.Text = Y.ToString();
            lbl_contrastValue.Text = contrast.ToString();
            lbl_brightnessValue.Text = brightness.ToString();
        }

        public unsafe void AdjustContrast()
        {
            BitmapData bDataSource = 
                sourceBitmap[selectedPic].LockBits(
                new Rectangle(0, 0, sourceBitmap[selectedPic].Width, sourceBitmap[selectedPic].Height),
                ImageLockMode.ReadOnly,
                sourceBitmap[selectedPic].PixelFormat
                );

            shownBitmap[selectedPic] = 
                new Bitmap(
                    sourceBitmap[selectedPic].Width,
                    sourceBitmap[selectedPic].Height,
                    sourceBitmap[selectedPic].PixelFormat
                    );

            BitmapData bDataDest =
                shownBitmap[selectedPic].LockBits(
                    new Rectangle(0, 0, sourceBitmap[selectedPic].Width, sourceBitmap[selectedPic].Height),
                    ImageLockMode.ReadWrite,
                    sourceBitmap[selectedPic].PixelFormat
                    );

            byte bitsPerPixel = (byte)Image.GetPixelFormatSize(bDataSource.PixelFormat);
            lbl_bitsPerPixel.Text = bitsPerPixel.ToString();

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

            Console.WriteLine(
                "AdjContrast for-loop time: " + watchdog.ElapsedMilliseconds + " / " + watchdog.ElapsedTicks + " ticks."
                );

            lbl_processingTime.Text = watchdog.ElapsedMilliseconds.ToString();

            sourceBitmap[selectedPic].UnlockBits(bDataSource);
            shownBitmap[selectedPic].UnlockBits(bDataDest);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void pictureBoxMain_Resize(object sender, EventArgs e)
        {
            maxX = ((PictureBox)sender).Width - border;
            maxY = ((PictureBox)sender).Height - border;

            lbl_pictureBoxMaxX.Text = maxX.ToString();
            lbl_pictureBoxMaxY.Text = maxX.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedPic = 0;

                if (pictureFrames != null)
                {
                    flowLayoutPanel1.SuspendLayout();
                    for (int i = 0; i < pictureFrames.Length; ++i)
                    {
                        pictureFrames[i].Dispose();
                    }
                    flowLayoutPanel1.ResumeLayout();
                }

                if (ofd.FileNames.Length > maxLoadableFiles)
                {
                    DialogResult answer;
                    answer = MessageBox.Show(
                        String.Format("To prevent unstable WinForms controls, just first {0} images should be loaded...\nPress \"Yes\" to load all {1} images, or \"No\" to only load {2} files.", maxLoadableFiles, ofd.FileNames.Length, maxLoadableFiles),
                        "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (answer == DialogResult.Yes)
                    {
                        fullPath = new string[ofd.FileNames.Length];
                        fullPath = ofd.FileNames;
                    }
                    else
                    {
                        fullPath = new string[maxLoadableFiles];
                        Array.Copy(ofd.FileNames, fullPath, maxLoadableFiles);
                    }
                    
                }
                else
                {
                    fullPath = new string[ofd.FileNames.Length];
                    fullPath = ofd.FileNames;
                }
                LoadPics();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sourceBitmap != null)
            {
                brightness = 0;
                contrast = 0;
                AdjustContrast();
                pictureBoxMain.Image = shownBitmap[selectedPic];
                if (sourceBitmap[selectedPic].PixelFormat == PixelFormat.Format8bppIndexed && !easterEggON)
                {
                    pictureBoxMain.Image.Palette = sourceBitmap[selectedPic].Palette;
                }
                pictureBoxMain.Refresh();

                lbl_pictureBoxMousePositionX.Text = "0";
                lbl_pictureBoxMousePositionY.Text = "0";
                lbl_brightnessValue.Text = "0";
                lbl_contrastValue.Text = "0";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            easterEggON = ((CheckBox)sender).Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                autoPsycho.Interval = 50;
                autoPsycho.Tick += autoPsychoEvent;
                autoPsycho.Start();
            }
            else
            {
                autoPsycho.Stop();
            }
        }

        private void autoPsychoEvent(object sender, EventArgs e)
        {
            if (sourceBitmap != null)
            {
                brightness = random.Next(-100, 100);
                contrast = random.Next(-200, 200);

                AdjustContrast();
                pictureBoxMain.Image = shownBitmap[selectedPic];
                if (sourceBitmap[selectedPic].PixelFormat == PixelFormat.Format8bppIndexed && !easterEggON)
                {
                    pictureBoxMain.Image.Palette = sourceBitmap[selectedPic].Palette;
                }
                pictureBoxMain.Refresh();

                lbl_pictureBoxMousePositionX.Text = "0";
                lbl_pictureBoxMousePositionY.Text = "0";
                lbl_brightnessValue.Text = brightness.ToString();
                lbl_contrastValue.Text = contrast.ToString();
            }

        }
    }
}
