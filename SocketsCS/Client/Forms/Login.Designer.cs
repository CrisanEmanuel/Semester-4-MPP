using System.ComponentModel;

namespace Client.Forms
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.loginButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.signUpLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(298, 320);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(213, 29);
            this.loginButton.TabIndex = 0;
            this.loginButton.Text = "Log in";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(332, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 51);
            this.label1.TabIndex = 1;
            this.label1.Text = "Log in";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // usernameBox
            // 
            this.usernameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameBox.ForeColor = System.Drawing.Color.Silver;
            this.usernameBox.Location = new System.Drawing.Point(298, 191);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(213, 27);
            this.usernameBox.TabIndex = 2;
            this.usernameBox.Text = " username";
            this.usernameBox.Enter += new System.EventHandler(this.usernameText_Enter);
            this.usernameBox.Leave += new System.EventHandler(this.usernameText_Leave);
            // 
            // passwordBox
            // 
            this.passwordBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordBox.ForeColor = System.Drawing.Color.Silver;
            this.passwordBox.Location = new System.Drawing.Point(298, 253);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(213, 27);
            this.passwordBox.TabIndex = 3;
            this.passwordBox.Text = " password";
            this.passwordBox.UseSystemPasswordChar = true;
            this.passwordBox.Enter += new System.EventHandler(this.passwordText_Enter);
            this.passwordBox.Leave += new System.EventHandler(this.passwordText_Leave);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(298, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Don\'t have an account?";
            // 
            // signUpLink
            // 
            this.signUpLink.Location = new System.Drawing.Point(456, 352);
            this.signUpLink.Name = "signUpLink";
            this.signUpLink.Size = new System.Drawing.Size(92, 28);
            this.signUpLink.TabIndex = 5;
            this.signUpLink.TabStop = true;
            this.signUpLink.Text = "Sign up!";
            this.signUpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.signUpLink_LinkClicked);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(806, 502);
            this.Controls.Add(this.signUpLink);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loginButton);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "LoginForm";
            this.Leave += new System.EventHandler(this.usernameText_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel signUpLink;

        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.TextBox passwordBox;

        private System.Windows.Forms.TextBox textBox2;

        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label label1;


        #endregion
    }
}