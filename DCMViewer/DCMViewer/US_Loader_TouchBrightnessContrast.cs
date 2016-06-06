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
            int howManyPics = 5;

            ClickableBorderNumberPic.ClickableBorderNumberPic cbnp;
            for (int i = 0; i < howManyPics; ++i)
            {
                cbnp = new ClickableBorderNumberPic.ClickableBorderNumberPic();
                cbnp.Parent = flowLayoutPanel1;
                cbnp.borderSize = 2;
                cbnp.value = i;
                cbnp.status = true;
                cbnp.Create(200, 130);
                clickablePicBoxList.Add(cbnp);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            clickablePicBoxList[2].changeValue(clickablePicBoxList[2].value += 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clickablePicBoxList[2].changeValue(clickablePicBoxList[2].value -= 1);
        }
    }
}
