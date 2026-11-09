namespace PharmacyCoursework
{
    public partial class MainForm : Form
    {
        int? loggedUserId;
        string? loggedUserRole;
        Form authForm;


        public MainForm((int id, string role)? loginInfo, Form authForm)
        {
            InitializeComponent();
            if (loginInfo == null) throw new ArgumentNullException(nameof(loginInfo), "Login info (user id and role) cant be null");
            loggedUserId = loginInfo.Value.id;
            loggedUserRole = loginInfo.Value.role;

            this.authForm = authForm;

        }

        public void ChangeUserInfo((int id, string role)? loginInfo)
        {
            if (loginInfo == null) throw new ArgumentNullException(nameof(loginInfo), "Login info (user id and role) cant be null");
            loggedUserId = loginInfo.Value.id;
            loggedUserRole = loginInfo.Value.role;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (authForm != null)
            {
                authForm.Hide();
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loggedUserId = null;
            loggedUserRole = null;
            PharmacyDbAccess.LogOut();
            authForm.Show();
            this.Hide();
        }
    }
}
