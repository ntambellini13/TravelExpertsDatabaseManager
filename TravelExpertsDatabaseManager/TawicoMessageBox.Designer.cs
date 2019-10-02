namespace TravelExpertsDatabaseManager
{
    partial class TawicoMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TawicoMessageBox));
            this.TawicoButtonConfirm = new System.Windows.Forms.Button();
            this.TawicoLabel = new System.Windows.Forms.Label();
            this.TawicoButtonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TawicoButtonConfirm
            // 
            this.TawicoButtonConfirm.Location = new System.Drawing.Point(64, 94);
            this.TawicoButtonConfirm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TawicoButtonConfirm.Name = "TawicoButtonConfirm";
            this.TawicoButtonConfirm.Size = new System.Drawing.Size(70, 26);
            this.TawicoButtonConfirm.TabIndex = 0;
            this.TawicoButtonConfirm.Text = "OK";
            this.TawicoButtonConfirm.UseVisualStyleBackColor = true;
            this.TawicoButtonConfirm.Click += new System.EventHandler(this.TawicoButtonConfirm_Click);
            // 
            // TawicoLabel
            // 
            this.TawicoLabel.Location = new System.Drawing.Point(39, 6);
            this.TawicoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TawicoLabel.Name = "TawicoLabel";
            this.TawicoLabel.Size = new System.Drawing.Size(200, 66);
            this.TawicoLabel.TabIndex = 2;
            this.TawicoLabel.Text = "label1";
            // 
            // TawicoButtonCancel
            // 
            this.TawicoButtonCancel.Location = new System.Drawing.Point(145, 94);
            this.TawicoButtonCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TawicoButtonCancel.Name = "TawicoButtonCancel";
            this.TawicoButtonCancel.Size = new System.Drawing.Size(70, 26);
            this.TawicoButtonCancel.TabIndex = 3;
            this.TawicoButtonCancel.Text = "OK";
            this.TawicoButtonCancel.UseVisualStyleBackColor = true;
            this.TawicoButtonCancel.Click += new System.EventHandler(this.TawicoButtonCancel_Click);
            // 
            // TawicoMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 127);
            this.Controls.Add(this.TawicoButtonCancel);
            this.Controls.Add(this.TawicoLabel);
            this.Controls.Add(this.TawicoButtonConfirm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "TawicoMessageBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TawicoMessageBox";
            this.Load += new System.EventHandler(this.TawicoMessageBox_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TawicoButtonConfirm;
        private System.Windows.Forms.Label TawicoLabel;
        private System.Windows.Forms.Button TawicoButtonCancel;
    }
}