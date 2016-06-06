using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DCMViewer
{
    public partial class GDI_Testing : Form
    {
        Image _sourceImage;

        public GDI_Testing()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _sourceImage = new Bitmap(@"E:\rami.jpg");

        }

        public void GDI_Test()
        {
            

        }

        private void GDI_Testing_Paint(object sender, PaintEventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Stopwatch sw = new Stopwatch();

            base.OnPaint(e);
            sw.Start();
            Graphics g = e.Graphics;
            //Rectangle rect = new Rectangle(10, 10, this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
            Rectangle panelRect = new Rectangle(panel1.Location.X, panel1.Location.Y, panel1.Width, panel1.Height);
            //LinearGradientBrush lbrush = new LinearGradientBrush(panelRect, Color.Red, Color.Yellow, LinearGradientMode.BackwardDiagonal);
            //g.FillRectangle(lbrush, panelRect);
            //g.DrawImage(_sourceImage, panelRect);
            sw.Stop();
            System.Diagnostics.Debug.WriteLine("OnPaint: " + sw.ElapsedMilliseconds + "ms.");
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("panel1_MouseDown");
        }
    }
}
