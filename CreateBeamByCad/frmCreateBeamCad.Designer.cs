namespace CreateBeamByCad
{
    partial class frmCreateBeamCad
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
            this.btnBeamCreating = new System.Windows.Forms.Button();
            this.btnSelectLine = new System.Windows.Forms.Button();
            this.dropNameBeam = new System.Windows.Forms.ComboBox();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.label1 = new System.Windows.Forms.Label();
            this.goupPosition = new System.Windows.Forms.GroupBox();
            this.radioOriginBeam = new System.Windows.Forms.RadioButton();
            this.radioLeftBeam = new System.Windows.Forms.RadioButton();
            this.radioCenterBeam = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEndOffset = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartOffset = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.goupPosition.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBeamCreating
            // 
            this.btnBeamCreating.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBeamCreating.AutoSize = true;
            this.btnBeamCreating.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBeamCreating.Enabled = false;
            this.btnBeamCreating.Location = new System.Drawing.Point(623, 150);
            this.btnBeamCreating.Name = "btnBeamCreating";
            this.btnBeamCreating.Size = new System.Drawing.Size(96, 38);
            this.btnBeamCreating.TabIndex = 0;
            this.btnBeamCreating.Text = "Create Beam";
            this.btnBeamCreating.UseVisualStyleBackColor = true;
            this.btnBeamCreating.Click += new System.EventHandler(this.bntBeamCreating_Click);
            // 
            // btnSelectLine
            // 
            this.btnSelectLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectLine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectLine.Location = new System.Drawing.Point(507, 150);
            this.btnSelectLine.Name = "btnSelectLine";
            this.btnSelectLine.Size = new System.Drawing.Size(96, 38);
            this.btnSelectLine.TabIndex = 1;
            this.btnSelectLine.Text = "Select Line";
            this.btnSelectLine.UseVisualStyleBackColor = true;
            this.btnSelectLine.Click += new System.EventHandler(this.btnSelectLine_Click);
            // 
            // dropNameBeam
            // 
            this.dropNameBeam.FormattingEnabled = true;
            this.dropNameBeam.Location = new System.Drawing.Point(110, 31);
            this.dropNameBeam.Name = "dropNameBeam";
            this.dropNameBeam.Size = new System.Drawing.Size(238, 21);
            this.dropNameBeam.TabIndex = 3;
            this.dropNameBeam.SelectedIndexChanged += new System.EventHandler(this.dropNameBeam_SelectedIndexChanged);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "BeamName:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // goupPosition
            // 
            this.goupPosition.Controls.Add(this.radioOriginBeam);
            this.goupPosition.Controls.Add(this.radioLeftBeam);
            this.goupPosition.Controls.Add(this.radioCenterBeam);
            this.goupPosition.Location = new System.Drawing.Point(45, 75);
            this.goupPosition.Name = "goupPosition";
            this.goupPosition.Size = new System.Drawing.Size(303, 41);
            this.goupPosition.TabIndex = 9;
            this.goupPosition.TabStop = false;
            this.goupPosition.Text = "Position of beam";
            // 
            // radioOriginBeam
            // 
            this.radioOriginBeam.AutoSize = true;
            this.radioOriginBeam.Location = new System.Drawing.Point(32, 18);
            this.radioOriginBeam.Name = "radioOriginBeam";
            this.radioOriginBeam.Size = new System.Drawing.Size(52, 17);
            this.radioOriginBeam.TabIndex = 2;
            this.radioOriginBeam.TabStop = true;
            this.radioOriginBeam.Text = "Origin";
            this.radioOriginBeam.UseVisualStyleBackColor = true;
            // 
            // radioLeftBeam
            // 
            this.radioLeftBeam.AutoSize = true;
            this.radioLeftBeam.Location = new System.Drawing.Point(134, 18);
            this.radioLeftBeam.Name = "radioLeftBeam";
            this.radioLeftBeam.Size = new System.Drawing.Size(43, 17);
            this.radioLeftBeam.TabIndex = 1;
            this.radioLeftBeam.TabStop = true;
            this.radioLeftBeam.Text = "Left";
            this.radioLeftBeam.UseVisualStyleBackColor = true;
            // 
            // radioCenterBeam
            // 
            this.radioCenterBeam.AutoSize = true;
            this.radioCenterBeam.Location = new System.Drawing.Point(228, 18);
            this.radioCenterBeam.Name = "radioCenterBeam";
            this.radioCenterBeam.Size = new System.Drawing.Size(56, 17);
            this.radioCenterBeam.TabIndex = 0;
            this.radioCenterBeam.TabStop = true;
            this.radioCenterBeam.Text = "Center";
            this.radioCenterBeam.UseVisualStyleBackColor = true;
            this.radioCenterBeam.CheckedChanged += new System.EventHandler(this.radioMiddle_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEndOffset);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStartOffset);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(45, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 60);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Level Offset";
            // 
            // txtEndOffset
            // 
            this.txtEndOffset.Location = new System.Drawing.Point(311, 24);
            this.txtEndOffset.Name = "txtEndOffset";
            this.txtEndOffset.Size = new System.Drawing.Size(100, 20);
            this.txtEndOffset.TabIndex = 3;
            this.txtEndOffset.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "End Offset";
            // 
            // txtStartOffset
            // 
            this.txtStartOffset.Location = new System.Drawing.Point(97, 24);
            this.txtStartOffset.Name = "txtStartOffset";
            this.txtStartOffset.Size = new System.Drawing.Size(100, 20);
            this.txtStartOffset.TabIndex = 1;
            this.txtStartOffset.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Start Offset";
            // 
            // frmCreateBeamCad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(731, 211);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.goupPosition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dropNameBeam);
            this.Controls.Add(this.btnSelectLine);
            this.Controls.Add(this.btnBeamCreating);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateBeamCad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCreateBeamCad";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmCreateBeamCad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.goupPosition.ResumeLayout(false);
            this.goupPosition.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox goupPosition;
        public System.Windows.Forms.ComboBox dropNameBeam;
        public System.Windows.Forms.RadioButton radioLeftBeam;
        public System.Windows.Forms.RadioButton radioCenterBeam;
        private System.Windows.Forms.RadioButton radioOriginBeam;
        public System.Windows.Forms.Button btnBeamCreating;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtEndOffset;
        public System.Windows.Forms.TextBox txtStartOffset;
        public System.Windows.Forms.Button btnSelectLine;
    }
}