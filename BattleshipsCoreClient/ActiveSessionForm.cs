using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Requests;
using BattleshipsCore.Responses;

namespace BattleshipsCoreClient
{
    public partial class ActiveSessionForm : Form
    {
        public GameSessionData? SessionData { get; private set; }
        public List<string> JoinedPlayers { get; private set; }

        public ActiveSessionForm()
        {
            JoinedPlayers = new List<string>();

            InitializeComponent();

            FormClosed += ActiveSessionForm_FormClosed;
        }

        public void ShowWindow(GameSessionData sessionData)
        {
            Program.ConnectionForm.Hide();
            Program.SessionForm.Hide();
            Program.PlacementForm.Hide();
            Program.ShootingForm.Hide();

            ClearData();
            UpdateSessionData(sessionData);
            Show();
        }

        private void UpdateSessionData(GameSessionData sessionData)
        {
            var freshSessionData = GameClientManager.Instance
                .Client!
                .SendCommand<GetSessionDataRequest, SendSessionDataResponse>(
                new GetSessionDataRequest(sessionData.SessionKey));

            if (freshSessionData == null)
            {
                MessageBox.Show("Could not update session list.", "Error");
                return;
            }

            SessionData = freshSessionData.SessionData;
            JoinedPlayers = SessionData.PlayerNames;

            PlayerListBox.Items.Clear();
            foreach (var player in JoinedPlayers)
            {
                PlayerListBox.Items.Add(player);
            }
        }

        private void ActiveSessionForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LeaveButton_Click(object sender, EventArgs e)
        {
            var success = GameClientManager.Instance.LeaveSession();

            if (success)
            {
                Program.SessionForm.ShowWindow();
            }
            else
            {
                MessageBox.Show("Failed to leave session.", "Error");
            }
        }

        private void RefreshSessionButton_Click(object sender, EventArgs e)
        {
            if (SessionData == null)
            {
                MessageBox.Show("Session does not exist.", "Error");

                Program.SessionForm.ShowWindow();
                return;
            }

            UpdateSessionData(SessionData);

            if (SessionData.Active)
            {
                StartGame();
            }
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            if (SessionData == null)
            {
                MessageBox.Show("Session does not exist.", "Error");

                Program.SessionForm.ShowWindow();
                return;
            }

            var okResponse = GameClientManager.Instance
                .Client!
                .SendCommand<StartGameRequest, OkResponse>(
                new StartGameRequest(SessionData.SessionKey));

            if (okResponse == null)
            {
                MessageBox.Show("Could not start game. You need exactly 2 players to be in the session.", "Error");
                return;
            }

            StartGame();
        }

        private void StartGame()
        {
            var myMap = GameClientManager.Instance
                .Client!
                .SendCommand<GetMapDataRequest, SendMapDataResponse>(
                new GetMapDataRequest(SessionData!.SessionKey, GameClientManager.Instance.PlayerName!));

            if (myMap == null)
            {
                MessageBox.Show("Could not get into game. Exiting into session lobby.", "Error");

                Program.SessionForm.ShowWindow();
                GameClientManager.Instance.LeaveSession();

                return;
            }

            Program.PlacementForm.ShowWindow(myMap.MapData);
        }

        private void ClearData()
        {
            SessionData = null;
            JoinedPlayers.Clear();
        }
    }
}
