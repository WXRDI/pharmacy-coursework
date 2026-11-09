namespace PharmacyCoursework
{
    public partial class MainForm : Form
    {
        int loggedUserId;
        string loggedUserRole;
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm((int id, string role)? loginInfo)
        {
            InitializeComponent();
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
    }
}
