
namespace PharmacyCoursework
{
    internal static class Program
    {
        private static AuthForm auth = new AuthForm(); //fist form to show
        private static MainForm main = new MainForm(); //main form
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Database.Init();
            Application.Run(auth);
        }
    }
}