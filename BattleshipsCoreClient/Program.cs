#nullable disable

namespace BattleshipsCoreClient
{
    internal static class Program
    {
        public static Start ConnectionForm;
        public static SessionForm SessionForm;
        public static ActiveSessionForm ActiveSessionForm;
        public static PlacementForm PlacementForm;
        public static ShootingForm ShootingForm;

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
            PlacementForm = new PlacementForm();
            ShootingForm = new ShootingForm();

            Application.Run(ConnectionForm);
        }
    }
}