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
            this.TawicoButtonConfirm.Location = new System.Drawing.Point(65, 144);
            this.TawicoButtonConfirm.Name = "TawicoButtonConfirm";
            this.TawicoButtonConfirm.Size = new System.Drawing.Size(105, 40);
            this.TawicoButtonConfirm.TabIndex = 0;
            this.TawicoButtonConfirm.Text = "OK";
            this.TawicoButtonConfirm.UseVisualStyleBackColor = true;
            this.TawicoButtonConfirm.Click += new System.EventHandler(this.TawicoButtonConfirm_Click);
            // 
            // TawicoLabel
            // 
            this.TawicoLabel.Location = new System.Drawing.Point(28, 9);
            this.TawicoLabel.Name = "TawicoLabel";
            this.TawicoLabel.Size = new System.Drawing.Size(300, 101);
            this.TawicoLabel.TabIndex = 2;
            this.TawicoLabel.Text = "label1";
            // 
            // TawicoButtonCancel
            // 
            this.TawicoButtonCancel.Location = new System.Drawing.Point(186, 144);
            this.TawicoButtonCancel.Name = "TawicoButtonCancel";
            this.TawicoButtonCancel.Size = new System.Drawing.Size(105, 40);
            this.TawicoButtonCancel.TabIndex = 3;
            this.TawicoButtonCancel.Text = "OK";
            this.TawicoButtonCancel.UseVisualStyleBackColor = true;
            this.TawicoButtonCancel.Click += new System.EventHandler(this.TawicoButtonCancel_Click);
            // 
            // TawicoMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 196);
            this.Controls.Add(this.TawicoButtonCancel);
            this.Controls.Add(this.TawicoLabel);
            this.Controls.Add(this.TawicoButtonConfirm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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