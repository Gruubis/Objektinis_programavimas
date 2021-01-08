
namespace GUI
{
    partial class ItemsAdminControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
/*            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();*/
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
/*            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Size = new System.Drawing.Size(181, 218);
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);*/
            // 
            // TitleLabel
            // 
/*            this.TitleLabel.Location = new System.Drawing.Point(218, 15);*/
            // 
            // PriceLabel
            // 
/*            this.PriceLabel.Location = new System.Drawing.Point(417, 179);
            this.PriceLabel.Click += new System.EventHandler(this.PriceLabel_Click);*/
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(431, 88);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 27);
            this.button3.TabIndex = 7;
            this.button3.Text = "Istrinti preke";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ItemsAdminControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button3);
            this.Name = "ItemsAdminControl";
            this.Size = new System.Drawing.Size(510, 224);
/*            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.TitleLabel, 0);
            this.Controls.SetChildIndex(this.PriceLabel, 0);
            this.Controls.SetChildIndex(this.DescriptionLabel, 0);*/
            this.Controls.SetChildIndex(this.button3, 0);
/*            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();*/
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button button3;
    }
}
