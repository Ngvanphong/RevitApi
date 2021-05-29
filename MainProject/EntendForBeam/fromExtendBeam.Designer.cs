namespace MainProject.EntendForBeam
{
    partial class fromExtendBeam
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioVerticalExtend = new System.Windows.Forms.RadioButton();
            this.radioHorizontalExtend = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaximunExtend = new System.Windows.Forms.TextBox();
            this.btnExtendBeam = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioVerticalExtend);
            this.groupBox1.Controls.Add(this.radioHorizontalExtend);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 37);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direction";
            // 
            // radioVerticalExtend
            // 
            this.radioVerticalExtend.AutoSize = true;
            this.radioVerticalExtend.Location = new System.Drawing.Point(188, 14);
            this.radioVerticalExtend.Name = "radioVerticalExtend";
            this.radioVerticalExtend.Size = new System.Drawing.Size(60, 17);
            this.radioVerticalExtend.TabIndex = 1;
            this.radioVerticalExtend.Text = "Vertical";
            this.radioVerticalExtend.UseVisualStyleBackColor = true;
            // 
            // radioHorizontalExtend
            // 
            this.radioHorizontalExtend.AutoSize = true;
            this.radioHorizontalExtend.Checked = true;
            this.radioHorizontalExtend.Location = new System.Drawing.Point(44, 14);
            this.radioHorizontalExtend.Name = "radioHorizontalExtend";
            this.radioHorizontalExtend.Size = new System.Drawing.Size(72, 17);
            this.radioHorizontalExtend.TabIndex = 0;
            this.radioHorizontalExtend.TabStop = true;
            this.radioHorizontalExtend.Text = "Horizontal";
            this.radioHorizontalExtend.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Maximun distance:";
            // 
            // txtMaximunExtend
            // 
            this.txtMaximunExtend.Location = new System.Drawing.Point(111, 61);
            this.txtMaximunExtend.Name = "txtMaximunExtend";
            this.txtMaximunExtend.Size = new System.Drawing.Size(119, 20);
            this.txtMaximunExtend.TabIndex = 3;
            this.txtMaximunExtend.Text = "500";
            // 
            // btnExtendBeam
            // 
            this.btnExtendBeam.Location = new System.Drawing.Point(243, 56);
            this.btnExtendBeam.Name = "btnExtendBeam";
            this.btnExtendBeam.Size = new System.Drawing.Size(99, 28);
            this.btnExtendBeam.TabIndex = 4;
            this.btnExtendBeam.Text = "Extend";
            this.btnExtendBeam.UseVisualStyleBackColor = true;
            this.btnExtendBeam.Click += new System.EventHandler(this.btnExtendBeam_Click);
            // 
            // fromExtendBeam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 100);
            this.Controls.Add(this.btnExtendBeam);
            this.Controls.Add(this.txtMaximunExtend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fromExtendBeam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmExtendBeam";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.fromExtendBeam_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExtendBeam;
        public System.Windows.Forms.RadioButton radioVerticalExtend;
        public System.Windows.Forms.RadioButton radioHorizontalExtend;
        public System.Windows.Forms.TextBox txtMaximunExtend;
    }
}