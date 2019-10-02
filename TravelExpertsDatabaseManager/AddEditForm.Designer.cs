namespace TravelExpertsDatabaseManager
{
    partial class AddEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditForm));
            this.addEditCancelButton = new System.Windows.Forms.Button();
            this.addEditButton = new System.Windows.Forms.Button();
            this.addEditLabel = new System.Windows.Forms.Label();
            this.addEditTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addEditCancelButton
            // 
            this.addEditCancelButton.Location = new System.Drawing.Point(234, 112);
            this.addEditCancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addEditCancelButton.Name = "addEditCancelButton";
            this.addEditCancelButton.Size = new System.Drawing.Size(96, 35);
            this.addEditCancelButton.TabIndex = 0;
            this.addEditCancelButton.Text = "Cancel";
            this.addEditCancelButton.UseVisualStyleBackColor = true;
            this.addEditCancelButton.Click += new System.EventHandler(this.addEditCancelButton_Click);
            // 
            // addEditButton
            // 
            this.addEditButton.Location = new System.Drawing.Point(112, 112);
            this.addEditButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addEditButton.Name = "addEditButton";
            this.addEditButton.Size = new System.Drawing.Size(96, 35);
            this.addEditButton.TabIndex = 1;
            this.addEditButton.Text = "OK";
            this.addEditButton.UseVisualStyleBackColor = true;
            this.addEditButton.Click += new System.EventHandler(this.addEditButton_Click);
            // 
            // addEditLabel
            // 
            this.addEditLabel.Location = new System.Drawing.Point(18, 14);
            this.addEditLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.addEditLabel.Name = "addEditLabel";
            this.addEditLabel.Size = new System.Drawing.Size(150, 35);
            this.addEditLabel.TabIndex = 2;
            this.addEditLabel.Text = "label1";
            this.addEditLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // addEditTextBox
            // 
            this.addEditTextBox.Location = new System.Drawing.Point(180, 14);
            this.addEditTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addEditTextBox.Name = "addEditTextBox";
            this.addEditTextBox.Size = new System.Drawing.Size(148, 26);
            this.addEditTextBox.TabIndex = 3;
            this.addEditTextBox.TextChanged += new System.EventHandler(this.addEditTextBox_TextChanged);
            // 
            // AddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 183);
            this.Controls.Add(this.addEditTextBox);
            this.Controls.Add(this.addEditLabel);
            this.Controls.Add(this.addEditButton);
            this.Controls.Add(this.addEditCancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AddEditForm";
            this.Text = "AddEditForm";
            this.Load += new System.EventHandler(this.AddEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addEditCancelButton;
        private System.Windows.Forms.Button addEditButton;
        private System.Windows.Forms.Label addEditLabel;
        private System.Windows.Forms.TextBox addEditTextBox;
    }
}