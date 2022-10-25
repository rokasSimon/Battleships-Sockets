using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Requests;
using BattleshipsCore.Responses;
using BattleshipsCoreClient.Observer;

namespace BattleshipsCoreClient
{
    public partial class ActiveSessionForm : Form, ISubscriber
    {
        public GameSessionData? SessionData { get; private set; }
        public List<string> JoinedPlayers { get; private set; } = new List<string>();

        public ActiveSessionForm()
        {
            InitializeComponent();

            FormClosed += ActiveSessionForm_FormClosed;
        }

        private void ActiveSessionForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private async void LeaveButton_Click(object sender, EventArgs e)
        {
            await GameClientManager.Instance.Client!
                .SendMessageAsync(
                new LeaveSessionRequest(SessionData!.SessionKey, GameClientManager.Instance.PlayerName!));
        }

        private async void RefreshSessionButton_Click(object sender, EventArgs e)
        {
            if (SessionData == null)
            {
                MessageBox.Show("Session no longer exists.", "Error");

                await Program.SwitchToSessionListFrom(this);
                return;
            }

            await GameClientManager.Instance.Client!
                .SendMessageAsync(new GetSessionDataRequest(SessionData.SessionKey));
        }

        private async void StartGameButton_Click(object sender, EventArgs e)
        {
            if (SessionData == null)
            {
                MessageBox.Show("Session no longer exists.", "Error");

                await Program.SwitchToSessionListFrom(this);
                return;
            }

            await GameClientManager.Instance.Client!
                .SendMessageAsync(new StartGameRequest(SessionData.SessionKey));
        }

        private void UpdateSessionData(GameSessionData sessionData)
        {
            SessionData = sessionData;
            JoinedPlayers = SessionData.PlayerNames;

            PlayerListBox.Items.Clear();
            foreach (var player in JoinedPlayers)
            {
                PlayerListBox.Items.Add(player);
            }
        }

        private void ClearData()
        {
            SessionData = null;
            JoinedPlayers.Clear();
        }

        public async Task UpdateAsync()
        {
            var client = GameClientManager.Instance.Client;
            if (client == null) return;

            var message = client.GetMessage();

            if (message is SendSessionDataResponse ssdr)
            {
                PlayerListBox.Invoke(() =>
                {
                    ClearData();
                    UpdateSessionData(ssdr.SessionData);

                    GameClientManager.Instance.ActiveSession = ssdr.SessionData;
                });
            }
            else if (message is StartedGameResponse sgr)
            {
                await Program.SwitchToPlacementFormFromActiveSession(SessionData!.SessionKey, GameClientManager.Instance.PlayerName);
            }
            else if (message is LeftSessionResponse lsr)
            {
                await Program.SwitchToSessionListFrom(this);
            }
        }
    }
}
