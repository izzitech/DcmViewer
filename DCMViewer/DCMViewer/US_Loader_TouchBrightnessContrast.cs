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
    public partial class US_Loader_TouchBrightnessContrast : Form
    {
        List<ClickableBorderNumberPic.ClickableBorderNumberPic> clickablePicBoxList = new List<ClickableBorderNumberPic.ClickableBorderNumberPic>();

        public US_Loader_TouchBrightnessContrast()
        {
            InitializeComponent();
            int howManyPics = 6;

            ClickableBorderNumberPic.ClickableBorderNumberPic cbnp;
            for (int i = 0; i < howManyPics; ++i)
            {
                cbnp = new ClickableBorderNumberPic.ClickableBorderNumberPic();
                cbnp.Parent = flowLayoutPanel1;
                cbnp.id = i + 1;
                cbnp.status = true;
                cbnp.imagePath = @".\us\18000000_00" + (i+1) + ".jpg";
                cbnp.Create();
                clickablePicBoxList.Add(cbnp);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            clickablePicBoxList[2].changeID(clickablePicBoxList[2].id += 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clickablePicBoxList[2].changeID(clickablePicBoxList[2].id -= 1);
        }

        private void picOnClick(object sender, EventArgs e)
        {
            ((ClickableBorderNumberPic.ClickableBorderNumberPic)sender).changeStatus(!((ClickableBorderNumberPic.ClickableBorderNumberPic)sender).status);
        }
    }
}
