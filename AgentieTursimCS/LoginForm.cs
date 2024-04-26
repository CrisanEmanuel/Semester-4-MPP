using System;
using System.Drawing;
using System.Windows.Forms;
using AgentieTurismCS.Utils;

namespace AgentieTurismCS
{
    public partial class LoginForm : Form
    {
        private readonly Service.Service _service;
        public LoginForm(Service.Service service)
        {
            Text = @"Login";
            _service = service;
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
            
            var angajat = _service.FindAngajatByUsername(username);
            if (angajat == null)
            {
                MessageBox.Show(@"Username " + username + @" does not exist!");
                return;
            }

            if (!Password.VerifyPassword(password, angajat.Password))
            {
                MessageBox.Show(@"Invalid username or password!");
                return;
            }

            var angajatForm = new AngajatForm(_service, angajat, this);
            angajatForm.Show();
            Hide();
        }

        private void signUpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // nothing yet
        }
    }
}