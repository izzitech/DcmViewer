namespace DCMViewer
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnMIR = new System.Windows.Forms.Button();
            this.btnTouchResponse = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "testingContrast1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnTouchResponse);
            this.groupBox1.Controls.Add(this.btnMIR);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(581, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 315);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "tools";
            // 
            // btnMIR
            // 
            this.btnMIR.Location = new System.Drawing.Point(6, 64);
            this.btnMIR.Name = "btnMIR";
            this.btnMIR.Size = new System.Drawing.Size(116, 39);
            this.btnMIR.TabIndex = 1;
            this.btnMIR.Text = "Multiple Image Representation";
            this.btnMIR.UseVisualStyleBackColor = true;
            this.btnMIR.Click += new System.EventHandler(this.btnMIR_Click);
            // 
            // btnTouchResponse
            // 
            this.btnTouchResponse.Location = new System.Drawing.Point(6, 109);
            this.btnTouchResponse.Name = "btnTouchResponse";
            this.btnTouchResponse.Size = new System.Drawing.Size(116, 40);
            this.btnTouchResponse.TabIndex = 2;
            this.btnTouchResponse.Text = "Touch Response";
            this.btnTouchResponse.UseVisualStyleBackColor = true;
            this.btnTouchResponse.Click += new System.EventHandler(this.btnTouchResponse_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 155);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 44);
            this.button2.TabIndex = 3;
            this.button2.Text = "Touch Brightness / Contrast";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 339);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnMIR;
        private System.Windows.Forms.Button btnTouchResponse;
        private System.Windows.Forms.Button button2;
    }
}

