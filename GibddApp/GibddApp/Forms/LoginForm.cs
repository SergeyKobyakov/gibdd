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
            LoginInfo.Login = textBoxLogin.Text.ToUpper();
            LoginInfo.Password = textBoxPassword.Text;

            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
