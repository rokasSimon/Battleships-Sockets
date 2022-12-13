using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Requests;
using BattleshipsCore.Responses;
using BattleshipsCoreClient.Observer;

namespace BattleshipsCoreClient
{
    public partial class ActiveSessionForm : Form, ISubscriber, IResponseVisitor
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

                await Facade.SwitchToSessionListFrom(this);
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

                await Facade.SwitchToSessionListFrom(this);
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

        public async Task UpdateAsync(AcceptableResponse message)
        {
            await message.Accept(this);
        }

        public Task Visit(SendSessionDataResponse response)
        {
            PlayerListBox.Invoke(() =>
            {
                ClearData();
                UpdateSessionData(response.SessionData);

                GameClientManager.Instance.ActiveSession = response.SessionData;
            });

            return Task.CompletedTask;
        }

        public async Task Visit(StartedGameResponse response)
        {
            await Facade.SwitchToPlacementFormFromActiveSession(SessionData!.SessionKey, GameClientManager.Instance.PlayerName!);
        }

        public async Task Visit(LeftSessionResponse response)
        {
            await Facade.SwitchToSessionListFrom(this);
        }

        public Task Visit(ActiveTurnResponse response) => Task.CompletedTask;
        public Task Visit(FailResponse response) => Task.CompletedTask;
        public Task Visit(InactiveTurnResponse response) => Task.CompletedTask;
        public Task Visit(JoinedServerResponse response) => Task.CompletedTask;
        public Task Visit(LostGameResponse response) => Task.CompletedTask;
        public Task Visit(NewSessionsAddedResponse response) => Task.CompletedTask;
        public Task Visit(SendPlayerListResponse response) => Task.CompletedTask;
        public Task Visit(SendTextResponse response) => Task.CompletedTask;
        public Task Visit(WonGameResponse response) => Task.CompletedTask;
        public Task Visit(DisconnectResponse response) => Task.CompletedTask;
        public Task Visit(JoinedSessionResponse response) => Task.CompletedTask;
        public Task Visit(SendSessionKeyResponse response) => Task.CompletedTask;
        public Task Visit(SendSessionListResponse response) => Task.CompletedTask;
        public Task Visit(OkResponse response) => Task.CompletedTask;
        public Task Visit(SendMapDataResponse response) => Task.CompletedTask;
        public Task Visit(SendTileUpdateResponse response) => Task.CompletedTask;
        public Task Visit(StartedBattleResponse response) => Task.CompletedTask;
    }
}
