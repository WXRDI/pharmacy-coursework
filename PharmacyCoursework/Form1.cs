namespace PharmacyCoursework
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pass = textBoxPass.Text;
            string login = textBoxLogin.Text;

            (int id, string role)? acc = PharmacyDbAccess.Login(pass, login);

            if (acc != null)
            {
                Form g = new MainForm(acc);
                g.Show();
                this.Hide();
            }
        }
    }
}
