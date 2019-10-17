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
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSettingDoor = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTextNoteType = new System.Windows.Forms.ComboBox();
            this.comboBoxSectionType = new System.Windows.Forms.ComboBox();
            this.listViewChooseParameter = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewFamilyDoor
            // 
            this.listViewFamilyDoor.CheckBoxes = true;
            this.listViewFamilyDoor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewFamilyDoor.Location = new System.Drawing.Point(13, 29);
            this.listViewFamilyDoor.Name = "listViewFamilyDoor";
            this.listViewFamilyDoor.Size = new System.Drawing.Size(264, 186);
            this.listViewFamilyDoor.TabIndex = 0;
            this.listViewFamilyDoor.UseCompatibleStateImageBehavior = false;
            this.listViewFamilyDoor.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Family Name";
            this.columnHeader1.Width = 263;
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
            // btnSettingDoor
            // 
            this.btnSettingDoor.Location = new System.Drawing.Point(485, 221);
            this.btnSettingDoor.Name = "btnSettingDoor";
            this.btnSettingDoor.Size = new System.Drawing.Size(75, 28);
            this.btnSettingDoor.TabIndex = 2;
            this.btnSettingDoor.Text = "Ok";
            this.btnSettingDoor.UseVisualStyleBackColor = true;
            this.btnSettingDoor.Click += new System.EventHandler(this.btnSettingDoor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "TextNote Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Section Type:";
            // 
            // comboBoxTextNoteType
            // 
            this.comboBoxTextNoteType.FormattingEnabled = true;
            this.comboBoxTextNoteType.Location = new System.Drawing.Point(372, 26);
            this.comboBoxTextNoteType.Name = "comboBoxTextNoteType";
            this.comboBoxTextNoteType.Size = new System.Drawing.Size(188, 21);
            this.comboBoxTextNoteType.TabIndex = 4;
            // 
            // comboBoxSectionType
            // 
            this.comboBoxSectionType.FormattingEnabled = true;
            this.comboBoxSectionType.Location = new System.Drawing.Point(372, 52);
            this.comboBoxSectionType.Name = "comboBoxSectionType";
            this.comboBoxSectionType.Size = new System.Drawing.Size(188, 21);
            this.comboBoxSectionType.TabIndex = 4;
            // 
            // listViewChooseParameter
            // 
            this.listViewChooseParameter.Location = new System.Drawing.Point(287, 100);
            this.listViewChooseParameter.Name = "listViewChooseParameter";
            this.listViewChooseParameter.Size = new System.Drawing.Size(273, 115);
            this.listViewChooseParameter.TabIndex = 5;
            this.listViewChooseParameter.UseCompatibleStateImageBehavior = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Choose parameters:";
            // 
            // frmSettingDoor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 252);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listViewChooseParameter);
            this.Controls.Add(this.comboBoxSectionType);
            this.Controls.Add(this.comboBoxTextNoteType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBoxTextNoteType;
        public System.Windows.Forms.ComboBox comboBoxSectionType;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ListView listViewChooseParameter;
    }
}