namespace DCMViewer
{
    partial class MultipleImageRepresentation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultipleImageRepresentation));
            this.image2 = new System.Windows.Forms.PictureBox();
            this.image5 = new System.Windows.Forms.PictureBox();
            this.image3 = new System.Windows.Forms.PictureBox();
            this.image4 = new System.Windows.Forms.PictureBox();
            this.image1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.image2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image1)).BeginInit();
            this.SuspendLayout();
            // 
            // image2
            // 
            this.image2.Location = new System.Drawing.Point(12, 44);
            this.image2.Name = "image2";
            this.image2.Size = new System.Drawing.Size(207, 112);
            this.image2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image2.TabIndex = 0;
            this.image2.TabStop = false;
            // 
            // image5
            // 
            this.image5.Location = new System.Drawing.Point(12, 397);
            this.image5.Name = "image5";
            this.image5.Size = new System.Drawing.Size(207, 112);
            this.image5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image5.TabIndex = 1;
            this.image5.TabStop = false;
            // 
            // image3
            // 
            this.image3.Location = new System.Drawing.Point(12, 161);
            this.image3.Name = "image3";
            this.image3.Size = new System.Drawing.Size(207, 112);
            this.image3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image3.TabIndex = 2;
            this.image3.TabStop = false;
            // 
            // image4
            // 
            this.image4.Location = new System.Drawing.Point(12, 280);
            this.image4.Name = "image4";
            this.image4.Size = new System.Drawing.Size(207, 112);
            this.image4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image4.TabIndex = 3;
            this.image4.TabStop = false;
            // 
            // image1
            // 
            this.image1.Location = new System.Drawing.Point(226, 13);
            this.image1.Name = "image1";
            this.image1.Size = new System.Drawing.Size(733, 496);
            this.image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image1.TabIndex = 4;
            this.image1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(150, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "18000000";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(170, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "->";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MultipleImageRepresentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 521);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.image1);
            this.Controls.Add(this.image4);
            this.Controls.Add(this.image3);
            this.Controls.Add(this.image5);
            this.Controls.Add(this.image2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MultipleImageRepresentation";
            this.Text = "MultipleImageRepresentation";
            ((System.ComponentModel.ISupportInitialize)(this.image2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox image2;
        private System.Windows.Forms.PictureBox image5;
        private System.Windows.Forms.PictureBox image3;
        private System.Windows.Forms.PictureBox image4;
        private System.Windows.Forms.PictureBox image1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}