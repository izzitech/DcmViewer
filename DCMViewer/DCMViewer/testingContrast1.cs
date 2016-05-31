using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCMViewer
{
    public partial class testingContrast1 : Form
    {
        Image foto;

        public testingContrast1()
        {
            InitializeComponent();
            foto = pictureBox1.Image;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ChangeContrast(trackBar1.Value);

        }

        private void ChangeContrast(int value)
        {
            pictureBox1.Image = AdjustContrast(new Bitmap(foto), trackBar1.Value);
        }

        // http://www.gutgames.com/
        // http://www.gutgames.com/post/Adjusting-Contrast-of-an-Image-in-C.aspx
        // http://cul.codeplex.com/

        // Image processing articles
        // http://www.codeproject.com/Articles/1989/Image-Processing-for-Dummies-with-C-and-GDI-Part
        // http://davidthomasbernal.com/blog/2008/03/13/c-image-processing-performance-unsafe-vs-safe-code-part-i/
        // http://davidthomasbernal.com/blog/2008/03/14/c-image-processing-performance-unsafe-vs-safe-code-part-ii/
        // ----
        // Resumen: 
        //          No queda otra. Usar código unsafe y punteros es la mejor solución del mundo y 
        //          la única que debería existir.
        // ----

        public static Bitmap AdjustContrast(Bitmap OriginalImage, float Value)
        {
            Bitmap NewBitmap = new Bitmap(OriginalImage.Width, OriginalImage.Height);
            //Bitmap NewData = new Bitmap(OriginalImage.Width, OriginalImage.Height);
            //Bitmap OldData = new Bitmap(OriginalImage.Width, OriginalImage.Height);

            //int NewPixelSize = Image.GetPixelFormatSize(NewData.PixelFormat);
            //int OldPixelSize = Image.GetPixelFormatSize(OldData.PixelFormat);

            Value = (100.0f + Value) / 100.0f;
            Value *= Value;

            for (int x = 0; x < NewBitmap.Width; ++x)
            {
                for (int y = 0; y < NewBitmap.Height; ++y)
                {
                    Color Pixel = NewBitmap.GetPixel(x, y);
                    float Red = Pixel.R / 255.0f;
                    float Green = Pixel.G / 255.0f;
                    float Blue = Pixel.B / 255.0f;

                    Red = (((Red - 0.5f) * Value) + 0.5f) * 255.0f;
                    Green = (((Green - 0.5f) * Value) + 0.5f) * 255.0f;
                    Blue = (((Blue - 0.5f) * Value) + 0.5f) * 255.0f;

                    NewBitmap.SetPixel(x, y, Color.FromArgb(ClampInt((int)Red, 255, 0),
                                                          ClampInt((int)Green, 255, 0),
                                                          ClampInt((int)Blue, 0, 255)));

                }
            }

            return NewBitmap;
        }

        public static int ClampInt(int value, int min, int max)
        {
            if (max < value)
            {
                return max;
            }

            if (min > value)
            {
                return min;
            }

            return value;
        }

    }
}
