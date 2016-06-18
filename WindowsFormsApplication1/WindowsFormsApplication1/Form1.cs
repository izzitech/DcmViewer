using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                PictureBox pb = null;
                for (int i = 0; i < fileDialog.FileNames.Length; ++i) {
                    pb = new PictureBox();
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.ImageLocation = fileDialog.FileNames[i];
                    flowLayoutPanel1.Controls.Add(pb);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.LoadReportDefinition(new StreamReader(@"D:\izzitech\DcmViewer\WindowsFormsApplication1\WindowsFormsApplication1\Report1.rdlc"));
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            // We set margins, the idiots use Inches instead of metrics.
            PageSettings pageSettings = new PageSettings();
            pageSettings.Margins.Left = 39;
            pageSettings.Margins.Right = 39;
            pageSettings.Margins.Top = 39;
            pageSettings.Margins.Bottom = 39;
            this.reportViewer1.SetPageSettings(pageSettings);
            this.reportViewer1.RefreshReport();
        }
    }
}
