namespace BattleshipsCoreClient
{
    internal static class Program
    {
        public static volatile bool isThreadRunning = false;

        public static Start ConnectionForm;
        public static SessionForm SessionForm;
        public static ActiveSessionForm ActiveSessionForm;
        public static object PlacementPanel;
        public static object PlayPanel;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConnectionForm = new Start();
            SessionForm = new SessionForm();
            ActiveSessionForm = new ActiveSessionForm();

            Application.Run(ConnectionForm);
        }

        public static void ReturnToConnectionPanel()
        {
            SessionForm.Hide();
            ActiveSessionForm.Hide();

            ConnectionForm.Show();
        }
    }
}