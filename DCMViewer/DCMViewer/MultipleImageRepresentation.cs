using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DCMViewer
{
    public partial class MultipleImageRepresentation : Form
    {
        public MultipleImageRepresentation()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenImages();
        }

        private void OpenImages()
        {
            string[] pics;


            pics = Directory.GetFiles("./pics", textBox1.Text + "*.jpg");
            Console.WriteLine(String.Format("Found: {0} images.", pics.Length));

            image1.Image = new Bitmap(pics[1]);
            image2.Image = new Bitmap(pics[2]);
            image3.Image = new Bitmap(pics[3]);
            image4.Image = new Bitmap(pics[4]);
            image5.Image = new Bitmap(pics[5]);
        }
    }
}
