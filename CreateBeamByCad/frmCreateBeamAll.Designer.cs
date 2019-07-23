namespace CreateBeamByCad
{
    partial class frmCreateBeamAll
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
            this.btnCreateBeamAll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioVertical = new System.Windows.Forms.RadioButton();
            this.radioHorizontal = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dropLineStyle = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dropTextStyle = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMinimun = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOffsetLevelBeam = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateBeamAll
            // 
            this.btnCreateBeamAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateBeamAll.Location = new System.Drawing.Point(558, 115);
            this.btnCreateBeamAll.Name = "btnCreateBeamAll";
            this.btnCreateBeamAll.Size = new System.Drawing.Size(102, 34);
            this.btnCreateBeamAll.TabIndex = 0;
            this.btnCreateBeamAll.Text = "Create All";
            this.btnCreateBeamAll.UseVisualStyleBackColor = true;
            this.btnCreateBeamAll.Click += new System.EventHandler(this.btnCreateBeamAll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioVertical);
            this.groupBox1.Controls.Add(this.radioHorizontal);
            this.groupBox1.Location = new System.Drawing.Point(24, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 46);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direction of Beam";
            // 
            // radioVertical
            // 
            this.radioVertical.AutoSize = true;
            this.radioVertical.Location = new System.Drawing.Point(197, 19);
            this.radioVertical.Name = "radioVertical";
            this.radioVertical.Size = new System.Drawing.Size(60, 17);
            this.radioVertical.TabIndex = 1;
            this.radioVertical.Text = "Vertical";
            this.radioVertical.UseVisualStyleBackColor = true;
            // 
            // radioHorizontal
            // 
            this.radioHorizontal.AutoSize = true;
            this.radioHorizontal.Checked = true;
            this.radioHorizontal.Location = new System.Drawing.Point(63, 19);
            this.radioHorizontal.Name = "radioHorizontal";
            this.radioHorizontal.Size = new System.Drawing.Size(72, 17);
            this.radioHorizontal.TabIndex = 0;
            this.radioHorizontal.TabStop = true;
            this.radioHorizontal.Text = "Horizontal";
            this.radioHorizontal.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Line stype of Beam:";
            // 
            // dropLineStyle
            // 
            this.dropLineStyle.FormattingEnabled = true;
            this.dropLineStyle.Location = new System.Drawing.Point(122, 14);
            this.dropLineStyle.Name = "dropLineStyle";
            this.dropLineStyle.Size = new System.Drawing.Size(183, 21);
            this.dropLineStyle.TabIndex = 3;
            this.dropLineStyle.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Text Style of Beam:";
            // 
            // dropTextStyle
            // 
            this.dropTextStyle.FormattingEnabled = true;
            this.dropTextStyle.Location = new System.Drawing.Point(456, 14);
            this.dropTextStyle.Name = "dropTextStyle";
            this.dropTextStyle.Size = new System.Drawing.Size(204, 21);
            this.dropTextStyle.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(319, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Minimum Length of Beam:";
            // 
            // txtMinimun
            // 
            this.txtMinimun.Location = new System.Drawing.Point(456, 80);
            this.txtMinimun.Name = "txtMinimun";
            this.txtMinimun.Size = new System.Drawing.Size(204, 20);
            this.txtMinimun.TabIndex = 7;
            this.txtMinimun.Text = "1000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(319, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Offset Level Default:";
            // 
            // txtOffsetLevelBeam
            // 
            this.txtOffsetLevelBeam.Location = new System.Drawing.Point(456, 50);
            this.txtOffsetLevelBeam.Name = "txtOffsetLevelBeam";
            this.txtOffsetLevelBeam.Size = new System.Drawing.Size(204, 20);
            this.txtOffsetLevelBeam.TabIndex = 9;
            this.txtOffsetLevelBeam.Text = "0";
            // 
            // frmCreateBeamAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 161);
            this.Controls.Add(this.txtOffsetLevelBeam);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMinimun);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dropTextStyle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dropLineStyle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCreateBeamAll);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateBeamAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCreateBeamAll";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmCreateBeamAll_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton radioVertical;
        public System.Windows.Forms.RadioButton radioHorizontal;
        public System.Windows.Forms.Button btnCreateBeamAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox dropLineStyle;
        public System.Windows.Forms.ComboBox dropTextStyle;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtMinimun;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtOffsetLevelBeam;
    }
}