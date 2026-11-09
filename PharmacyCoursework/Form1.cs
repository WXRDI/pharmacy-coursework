namespace PharmacyCoursework
{
    public partial class AuthForm : Form
    {
        private bool isFirstStartup { get; set; } = true;
        private MainForm? mainForm;
        public AuthForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (PharmacyDbAccess.AutoLogIn())
            {
                (int id, string role) loginInfo = PharmacyDbAccess.FindRoleFromToken();
                Form g = new MainForm(loginInfo, this);
                g.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string pass = textBoxPass.Text;
            string login = textBoxLogin.Text;

            (int id, string role)? acc = PharmacyDbAccess.Login(pass, login);

            if (acc != null)
            {
                if (isFirstStartup)
                {
                    PharmacyDbAccess.OnLoginSuccess(acc.Value.id.ToString(), checkBox1.Checked);
                    MainForm g = new MainForm(acc, this);
                    g.Show();
                    mainForm = g;
                    isFirstStartup = false;
                    textBoxLogin.Text = "";
                    textBoxPass.Text = "";
                    this.Hide();
                }
                else
                {
                    PharmacyDbAccess.OnLoginSuccess(acc.Value.id.ToString(), checkBox1.Checked);
                    if (mainForm != null)
                    {
                        mainForm.Show();
                        mainForm.ChangeUserInfo(acc);
                        this.Hide();
                        textBoxLogin.Text = "";
                        textBoxPass.Text = "";
                    }
                    else
                    {
                        throw new ArgumentNullException(nameof(mainForm), "Cant be null");
                    }
                }
            }

        }
    }
}
