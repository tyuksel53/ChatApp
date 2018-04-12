namespace YazlabII_Client
{
    partial class Login
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
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.RegisterNewUser = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(12, 44);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(145, 20);
            this.tbUsername.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username:";
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Location = new System.Drawing.Point(15, 88);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(53, 13);
            this.Password.TabIndex = 2;
            this.Password.Text = "Password";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(12, 104);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(145, 20);
            this.tbPassword.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(82, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RegisterNewUser
            // 
            this.RegisterNewUser.AutoSize = true;
            this.RegisterNewUser.Location = new System.Drawing.Point(9, 196);
            this.RegisterNewUser.Name = "RegisterNewUser";
            this.RegisterNewUser.Size = new System.Drawing.Size(96, 13);
            this.RegisterNewUser.TabIndex = 5;
            this.RegisterNewUser.Text = "Register New User";
            this.RegisterNewUser.Click += new System.EventHandler(this.Register_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(169, 232);
            this.Controls.Add(this.RegisterNewUser);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbUsername);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label RegisterNewUser;
    }
}

