namespace YazlabII_Client
{
    partial class KonusmaPenceresi
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
            this.lbHeader = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lvMesajlar = new System.Windows.Forms.ListBox();
            this.pbTargetPicture = new System.Windows.Forms.PictureBox();
            this.lbTargetUsername = new System.Windows.Forms.Label();
            this.lbHostUsername = new System.Windows.Forms.Label();
            this.pbHostPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbTargetPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHostPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.lbHeader.Location = new System.Drawing.Point(12, 9);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(35, 13);
            this.lbHeader.TabIndex = 1;
            this.lbHeader.Text = "label1";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(12, 214);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(436, 29);
            this.tbMessage.TabIndex = 2;
            this.tbMessage.Text = "Mesajınız";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(373, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Gönder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lvMesajlar
            // 
            this.lvMesajlar.FormattingEnabled = true;
            this.lvMesajlar.Location = new System.Drawing.Point(12, 25);
            this.lvMesajlar.Name = "lvMesajlar";
            this.lvMesajlar.Size = new System.Drawing.Size(436, 186);
            this.lvMesajlar.TabIndex = 4;
            // 
            // pbTargetPicture
            // 
            this.pbTargetPicture.Location = new System.Drawing.Point(501, 40);
            this.pbTargetPicture.Name = "pbTargetPicture";
            this.pbTargetPicture.Size = new System.Drawing.Size(140, 77);
            this.pbTargetPicture.TabIndex = 5;
            this.pbTargetPicture.TabStop = false;
            // 
            // lbTargetUsername
            // 
            this.lbTargetUsername.AutoSize = true;
            this.lbTargetUsername.Location = new System.Drawing.Point(501, 21);
            this.lbTargetUsername.Name = "lbTargetUsername";
            this.lbTargetUsername.Size = new System.Drawing.Size(35, 13);
            this.lbTargetUsername.TabIndex = 6;
            this.lbTargetUsername.Text = "label1";
            // 
            // lbHostUsername
            // 
            this.lbHostUsername.AutoSize = true;
            this.lbHostUsername.Location = new System.Drawing.Point(498, 153);
            this.lbHostUsername.Name = "lbHostUsername";
            this.lbHostUsername.Size = new System.Drawing.Size(35, 13);
            this.lbHostUsername.TabIndex = 7;
            this.lbHostUsername.Text = "label2";
            // 
            // pbHostPicture
            // 
            this.pbHostPicture.Location = new System.Drawing.Point(501, 169);
            this.pbHostPicture.Name = "pbHostPicture";
            this.pbHostPicture.Size = new System.Drawing.Size(140, 74);
            this.pbHostPicture.TabIndex = 8;
            this.pbHostPicture.TabStop = false;
            // 
            // KonusmaPenceresi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 302);
            this.Controls.Add(this.pbHostPicture);
            this.Controls.Add(this.lbHostUsername);
            this.Controls.Add(this.lbTargetUsername);
            this.Controls.Add(this.pbTargetPicture);
            this.Controls.Add(this.lvMesajlar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.lbHeader);
            this.Name = "KonusmaPenceresi";
            this.Text = "KonusmaPenceresi";
            ((System.ComponentModel.ISupportInitialize)(this.pbTargetPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHostPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbHeader;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lvMesajlar;
        private System.Windows.Forms.PictureBox pbTargetPicture;
        private System.Windows.Forms.Label lbTargetUsername;
        private System.Windows.Forms.Label lbHostUsername;
        private System.Windows.Forms.PictureBox pbHostPicture;
    }
}