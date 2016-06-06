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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            testingContrast1 tc1 = new testingContrast1();
            tc1.ShowDialog();
        }

        private void btnMIR_Click(object sender, EventArgs e)
        {
            MultipleImageRepresentation mir = new MultipleImageRepresentation();
            mir.ShowDialog();
        }

        private void btnTouchResponse_Click(object sender, EventArgs e)
        {
            TouchResponse tr = new TouchResponse();
            tr.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TouchBrightnessAndContrast tbc = new TouchBrightnessAndContrast();
            tbc.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GDI_Testing gdit = new GDI_Testing();
            gdit.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            US_Loader_TouchBrightnessContrast ultbc = new US_Loader_TouchBrightnessContrast();
            ultbc.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClickableBorderNumberPic.ClickableBorderNumberPic cbnp = new ClickableBorderNumberPic.ClickableBorderNumberPic();
            cbnp.Parent = flowLayoutPanel1;
            cbnp.borderSize = 2;
            cbnp.Create(200, 130);
            
        }
    }
}
