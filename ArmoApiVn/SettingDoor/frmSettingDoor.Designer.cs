namespace ArmoApiVn.SettingDoor
{
    partial class frmSettingDoor
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
            this.listViewFamilyDoor = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSettingDoor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewFamilyDoor
            // 
            this.listViewFamilyDoor.CheckBoxes = true;
            this.listViewFamilyDoor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewFamilyDoor.Location = new System.Drawing.Point(13, 29);
            this.listViewFamilyDoor.Name = "listViewFamilyDoor";
            this.listViewFamilyDoor.Size = new System.Drawing.Size(264, 181);
            this.listViewFamilyDoor.TabIndex = 0;
            this.listViewFamilyDoor.UseCompatibleStateImageBehavior = false;
            this.listViewFamilyDoor.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Doors don\'t create section:";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Family Name";
            this.columnHeader1.Width = 263;
            // 
            // btnSettingDoor
            // 
            this.btnSettingDoor.Location = new System.Drawing.Point(201, 216);
            this.btnSettingDoor.Name = "btnSettingDoor";
            this.btnSettingDoor.Size = new System.Drawing.Size(75, 28);
            this.btnSettingDoor.TabIndex = 2;
            this.btnSettingDoor.Text = "Ok";
            this.btnSettingDoor.UseVisualStyleBackColor = true;
            this.btnSettingDoor.Click += new System.EventHandler(this.btnSettingDoor_Click);
            // 
            // frmSettingDoor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 252);
            this.Controls.Add(this.btnSettingDoor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewFamilyDoor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettingDoor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSettingDoor";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListView listViewFamilyDoor;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        public System.Windows.Forms.Button btnSettingDoor;
    }
}