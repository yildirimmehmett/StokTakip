namespace stok_Takip
{
    partial class Şifremi_Unuttum
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
            this.btnsifre = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnsifre
            // 
            this.btnsifre.BackColor = System.Drawing.Color.Black;
            this.btnsifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnsifre.ForeColor = System.Drawing.Color.White;
            this.btnsifre.Location = new System.Drawing.Point(-7, 92);
            this.btnsifre.Name = "btnsifre";
            this.btnsifre.Size = new System.Drawing.Size(420, 43);
            this.btnsifre.TabIndex = 71;
            this.btnsifre.Text = "Yanıtla";
            this.btnsifre.UseVisualStyleBackColor = false;
            this.btnsifre.Click += new System.EventHandler(this.btnsifre_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(11, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(186, 25);
            this.label9.TabIndex = 70;
            this.label9.Text = "Sorunuzun Cevabı :";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox1.Location = new System.Drawing.Point(203, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(179, 29);
            this.textBox1.TabIndex = 69;
            // 
            // Şifremi_Unuttum
            // 
            this.AcceptButton = this.btnsifre;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(412, 131);
            this.Controls.Add(this.btnsifre);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox1);
            this.MaximumSize = new System.Drawing.Size(428, 170);
            this.MinimumSize = new System.Drawing.Size(428, 170);
            this.Name = "Şifremi_Unuttum";
            this.Text = "                                            ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnsifre;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
    }
}