namespace GibddApp.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLogin.Text)
                || string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                MessageBox.Show("Login and password should not be empty");
            }

            LoginInfo.Login = textBoxLogin.Text;
            LoginInfo.Password = textBoxPassword.Text;

            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
