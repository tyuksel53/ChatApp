namespace YazlabII_Client
{
    partial class AnaSayfa
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
            this.pbUserImg = new System.Windows.Forms.PictureBox();
            this.lbUsername = new System.Windows.Forms.Label();
            this.lbOturumAcmaTarihi = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lvKullanicilar = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserImg)).BeginInit();
            this.SuspendLayout();
            // 
            // pbUserImg
            // 
            this.pbUserImg.Location = new System.Drawing.Point(12, 26);
            this.pbUserImg.Name = "pbUserImg";
            this.pbUserImg.Size = new System.Drawing.Size(94, 67);
            this.pbUserImg.TabIndex = 0;
            this.pbUserImg.TabStop = false;
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(9, 116);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(35, 13);
            this.lbUsername.TabIndex = 1;
            this.lbUsername.Text = "label1";
            // 
            // lbOturumAcmaTarihi
            // 
            this.lbOturumAcmaTarihi.AutoSize = true;
            this.lbOturumAcmaTarihi.Location = new System.Drawing.Point(9, 152);
            this.lbOturumAcmaTarihi.Name = "lbOturumAcmaTarihi";
            this.lbOturumAcmaTarihi.Size = new System.Drawing.Size(35, 13);
            this.lbOturumAcmaTarihi.TabIndex = 2;
            this.lbOturumAcmaTarihi.Text = "label2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(130, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Kayitlı Kullanıcılar";
            // 
            // lvKullanicilar
            // 
            this.lvKullanicilar.Location = new System.Drawing.Point(133, 48);
            this.lvKullanicilar.Name = "lvKullanicilar";
            this.lvKullanicilar.Size = new System.Drawing.Size(221, 251);
            this.lvKullanicilar.TabIndex = 7;
            this.lvKullanicilar.UseCompatibleStateImageBehavior = false;
            this.lvKullanicilar.SelectedIndexChanged += new System.EventHandler(this.lvKullanicilar_SelectedIndexChanged);
            // 
            // AnaSayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 338);
            this.Controls.Add(this.lvKullanicilar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbOturumAcmaTarihi);
            this.Controls.Add(this.lbUsername);
            this.Controls.Add(this.pbUserImg);
            this.Name = "AnaSayfa";
            this.Text = "AnaSayfa";
            ((System.ComponentModel.ISupportInitialize)(this.pbUserImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbUserImg;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.Label lbOturumAcmaTarihi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvKullanicilar;
    }
}