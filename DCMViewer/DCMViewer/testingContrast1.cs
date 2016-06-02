using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
            // pictureBox1.Image = AdjustContrastCS(new Bitmap(foto), trackBar1.Value);
            pictureBox1.Image = AdjustContrastCEX(new Bitmap(foto), trackBar1.Value);
            GC.Collect();
            // AdjustContrastC(new Bitmap(foto), trackBar1.Value);
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

        public static Bitmap AdjustContrastCS(Bitmap OriginalImage, float Value)
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

        // This is astonishing faster than chained for() statements.
        public unsafe Bitmap AdjustContrastCEX(Bitmap bitmap, float value)
        {
            BitmapData bData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte bitsPerPixel = (byte)Image.GetPixelFormatSize(bData.PixelFormat);
            byte* scan0 = (byte*)bData.Scan0.ToPointer();
            byte* color = scan0;
            double finalContrast = 0;
            long imageByteSize = bitmap.Height * bitmap.Width * (bitsPerPixel / 8);

            value = (100.0f + value) / 100.0f;
            value *= value;

            for (int i = 0; i < imageByteSize; i++)
            {
                finalContrast = ((((*color / 255.0) - 0.5) * value) + 0.5) * 255;

                if (finalContrast < 0) finalContrast = 0;
                if (finalContrast > 255) finalContrast = 255;

                *color = (byte)finalContrast;
                color++;
            }

            bitmap.UnlockBits(bData);
            return bitmap;
        }

        public unsafe Bitmap AdjustContrastC(Bitmap bitmap, float value)
        {
            BitmapData bData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            byte* scan0 = (byte*)bData.Scan0.ToPointer();
            byte bitsPerPixel = (byte)Image.GetPixelFormatSize(bData.PixelFormat);

            // https://msdn.microsoft.com/en-us/library/system.drawing.image.flags%28v=vs.110%29.aspx
            // ImageFlagsNone               0
            // ImageFlagsScalable           1
            // ImageFlagsHasAlpha           2
            // ImageFlagsHasTranslucent     4
            // ImageFlagsPartiallyScalable  8
            // ImageFlagsColorSpaceRGB      16
            // ImageFlagsColorSpaceCMYK     32
            // ImageFlagsColorSpaceGRAY     64
            // ImageFlagsColorSpaceYCBCR    128
            // ImageFlagsColorSpaceYCCK     256
            // ImageFlagsHasRealDPI         4096
            // ImageFlagsHasRealPixelSize   8192
            // ImageFlagsReadOnly           65536
            // ImageFlagsCaching            131072


            //----
            // Procesamos **RGB** 8 bits por canal.
            // const int RGB = 16; //ImageFlags.ColorSpaceRgb
            //if ((bitmap.Flags & RGB) == RGB)
            //{
                // Set contrast value
                value = (100.0f + value) / 100.0f;
                value *= value;

            //int offset = bData.Stride - bitmap.Width * 3;
            // int widthBytesSize = bitmap.Width * 4;

            byte* pixel = scan0;
            Random ran = new Random();
            double result = 0;

            for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width * 4; x++)
                    {

                    result = *pixel;
                    result = result / 255.0;
                    result -= 0.5;
                    result *= value;
                    result += 0.5;
                    result *= 255;
                    if (result < 0) result = 0;
                    if (result > 255) result = 255;
                    *pixel = (byte)result;
                    pixel++;
                    ////Blue
                    //*pixel = (byte)ran.Next(0, 254);
                    //pixel++;
                    ////Green
                    //pixel++;
                    ////Red
                    //pixel++;
                    ////Alfa
                    //pixel++;

                    //*pixel = (byte)150;

                    //// Color Pixel = bitmap.GetPixel(x, y);
                    //float Red = Pixel.R / 255.0f;
                    //float Green = Pixel.G / 255.0f;
                    //float Blue = Pixel.B / 255.0f;

                    //Red = (((Red - 0.5f) * value) + 0.5f) * 255.0f;
                    //Green = (((Green - 0.5f) * value) + 0.5f) * 255.0f;
                    //Blue = (((Blue - 0.5f) * value) + 0.5f) * 255.0f;

                    //bitmap.SetPixel(x, y, Color.FromArgb(ClampInt((int)Red, 255, 0),
                    //                                      ClampInt((int)Green, 255, 0),
                    //                                      ClampInt((int)Blue, 0, 255)));

                }
                //pixel += offset;
                }

            //}
            //else
            //{
            //    MessageBox.Show(String.Format("Todavía no hay soporte para este formato de color: {0}.", bitmap.Flags.ToString()));
            //}
            bitmap.UnlockBits(bData);
            return bitmap;
        }

        // THIS IS UNCOMPLETE! Doesn't detect where an image is grayscale.
        public static byte GetBitsPerPixel(PixelFormat pixelFormat)
        {
            https://msdn.microsoft.com/en-us/library/system.drawing.imaging.pixelformat%28v=vs.110%29.aspx

            if (pixelFormat == PixelFormat.Alpha) ; // The pixel data contains alpha values that are not premultiplied.
            if (pixelFormat == PixelFormat.Canonical) ; // The default pixel format of 32 bits per pixel.The format specifies 24 - bit color depth and an 8 - bit alpha channel.
            if (pixelFormat == PixelFormat.DontCare) ; //No pixel format is specified.
            if (pixelFormat == PixelFormat.Extended) ; // Reserved.
            if (pixelFormat == PixelFormat.Format16bppArgb1555) return 16; // The pixel format is 16 bits per pixel.The color information specifies 32,768 shades of color, of which 5 bits are red, 5 bits are green, 5 bits are blue, and 1 bit is alpha.
            if (pixelFormat == PixelFormat.Format16bppGrayScale) return 16; // The pixel format is 16 bits per pixel.The color information specifies 65536 shades of gray.
            if (pixelFormat == PixelFormat.Format16bppRgb555) return 16; // Specifies that the format is 16 bits per pixel; 5 bits each are used for the red, green, and blue components.The remaining bit is not used.
            if (pixelFormat == PixelFormat.Format16bppRgb565) return 16; // Specifies that the format is 16 bits per pixel; 5 bits are used for the red component, 6 bits are used for the green component, and 5 bits are used for the blue component.
            if (pixelFormat == PixelFormat.Format1bppIndexed) return 1; // Specifies that the pixel format is 1 bit per pixel and that it uses indexed color.The color table therefore has two colors in it.
            if (pixelFormat == PixelFormat.Format24bppRgb) return 24; // Specifies that the format is 24 bits per pixel; 8 bits each are used for the red, green, and blue components.
            if (pixelFormat == PixelFormat.Format32bppArgb) return 32; //Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components.
            if (pixelFormat == PixelFormat.Format32bppPArgb) return 32; // Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components.The red, green, and blue components are premultiplied, according to the alpha component.
            if (pixelFormat == PixelFormat.Format32bppRgb) return 32; // Specifies that the format is 32 bits per pixel; 8 bits each are used for the red, green, and blue components.The remaining 8 bits are not used.
            if (pixelFormat == PixelFormat.Format48bppRgb) return 48; // Specifies that the format is 48 bits per pixel; 16 bits each are used for the red, green, and blue components.
            if (pixelFormat == PixelFormat.Format4bppIndexed) return 4; // Specifies that the format is 4 bits per pixel, indexed.
            if (pixelFormat == PixelFormat.Format64bppArgb) return 64; // Specifies that the format is 64 bits per pixel; 16 bits each are used for the alpha, red, green, and blue components.
            if (pixelFormat == PixelFormat.Format64bppPArgb) return 64; // Specifies that the format is 64 bits per pixel; 16 bits each are used for the alpha, red, green, and blue components.The red, green, and blue components are premultiplied according to the alpha component.
            if (pixelFormat == PixelFormat.Format8bppIndexed) return 8; // Specifies that the format is 8 bits per pixel, indexed.The color table therefore has 256 colors in it.
            if (pixelFormat == PixelFormat.Gdi) ; // The pixel data contains GDI colors.
            if (pixelFormat == PixelFormat.Indexed) ; // The pixel data contains color - indexed values, which means the values are an index to colors in the system color table, as opposed to individual color values.
            if (pixelFormat == PixelFormat.Max) ; // The maximum value for this enumeration.
            if (pixelFormat == PixelFormat.PAlpha) ; // The pixel format contains premultiplied alpha values.
            if (pixelFormat == PixelFormat.Undefined) ; // The pixel format is undefined.

            return 0;
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

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            pictureBox1.Image = AdjustContrastCEX(new Bitmap(foto), hScrollBar1.Value);
            GC.Collect();
        }
    }
}
