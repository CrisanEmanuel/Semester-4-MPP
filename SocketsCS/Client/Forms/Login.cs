using System;
using System.Drawing;
using System.Windows.Forms;
using Client.Controllers;
using Services;

namespace Client.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IService _server;
        public LoginForm(IService server)
        {
            Text = @"Login";
            _server = server;
            InitializeComponent();
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void usernameText_Enter(object sender, EventArgs e)
        {
            if (usernameBox.Text != @" username") return;
            usernameBox.Text = @"";
            usernameBox.ForeColor = Color.Black;
        }

        private void usernameText_Leave(object sender, EventArgs e)
        {
            if (usernameBox.Text != @"") return;
            usernameBox.Text = @" username";
            usernameBox.ForeColor = Color.Silver;
        }

        private void passwordText_Enter(object sender, EventArgs e)
        {
            if (passwordBox.Text != @" password") return;
            passwordBox.Text = @"";
            passwordBox.ForeColor = Color.Black;
        }
        
        private void passwordText_Leave(object sender, EventArgs e)
        {
            if (passwordBox.Text != @"") return;
            passwordBox.Text = @" password";
            passwordBox.ForeColor = Color.Silver;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            var username = usernameBox.Text;
            var password = passwordBox.Text;
            
            if (username == @" username" || password == @" password")
            {
                MessageBox.Show(@"Please fill in the fields");
                return;
            }
            
            try
            {
                var angajatController = new AngajatController(_server);
                angajatController.Login(username, password);
                var angajatForm = new AngajatForm(angajatController, _server, username, this);
                angajatForm.Show();
                Hide();
            } catch (Exception ex)
            {
                MessageBox.Show(this, @"Login Error " + ex.Message/* + ex.StackTrace*/, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void signUpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // nothing yet
        }
    }
}