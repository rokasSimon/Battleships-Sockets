using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;
using BattleshipsCoreClient.Observer;
using Message = BattleshipsCore.Interfaces.Message;

namespace BattleshipsCoreClient
{
    public partial class SessionForm : Form, ISubscriber, IResponseVisitor
    {
        public List<GameSessionData> SessionList { get; set; }

        public SessionForm()
        {
            SessionList = new List<GameSessionData>();

            InitializeComponent();

            FormClosed += SessionForm_FormClosed;
        }

        private void SessionForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private async void SessionRow_Click(object sender, DataGridViewCellEventArgs e)
        {
            var rowClicked = e.RowIndex;

            if (rowClicked < 0 || rowClicked >= SessionList.Count) return;

            var sessionData = SessionList[rowClicked];

            await GameClientManager.Instance.Client!
                .SendMessageAsync(new JoinSessionRequest(sessionData.SessionKey, GameClientManager.Instance.PlayerName!));
        }

        private async void RefreshButton_Click(object sender, EventArgs e)
        {
            await GameClientManager.Instance.Client!.SendMessageAsync(new GetSessionListRequest());
        }

        private async void CreateSessionButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CreateSessionTextBox.Text))
            {
                MessageBox.Show("Could not create session because session name is empty or has spaces.");
            }
            else
            {
                var playerName = GameClientManager.Instance.PlayerName!;
                var sessionName = CreateSessionTextBox.Text;

                await GameClientManager.Instance.Client!.SendMessageAsync(new CreateSessionRequest(playerName, sessionName));
            }
        }

        private async void DisconnectButton_Click(object sender, EventArgs e)
        {
            var _ = await GameClientManager.Instance.DisconnectAsync();

            await Facade.SwitchToConnectionFormFrom(this);
        }

        private void RefreshSessions(SendSessionListResponse sessionListResponse)
        {
            SessionList = sessionListResponse.SessionList;
            SessionListGrid.Rows.Clear();

            foreach (var session in SessionList)
            {
                var row = SessionListGrid.Rows.Add();

                SessionListGrid.Rows[row].Cells[0].Value = session.SessionName;
                SessionListGrid.Rows[row].Cells[1].Value = session.PlayerNames.Count;
            }

        }

        public async Task UpdateAsync(AcceptableResponse message)
        {
            await message.Accept(this);
        }

        public Task Visit(SendSessionListResponse response)
        {
            SessionListGrid.Invoke(() =>
            {
                RefreshSessions(response);
            });

            return Task.CompletedTask;
        }

        public async Task Visit(DisconnectResponse response)
        {
            await Facade.SwitchToConnectionFormFrom(this);
        }

        public async Task Visit(JoinedSessionResponse response)
        {
            await Facade.SwitchToActiveSessionFormFromSessionList(response.SessionId);
        }

        public async Task Visit(SendSessionKeyResponse response)
        {
            await Facade.SwitchToActiveSessionFormFromSessionList(response.SessionKey);
        }

        public Task Visit(ActiveTurnResponse response) => Task.CompletedTask;
        public Task Visit(FailResponse response) => Task.CompletedTask;
        public Task Visit(InactiveTurnResponse response) => Task.CompletedTask;
        public Task Visit(JoinedServerResponse response) => Task.CompletedTask;
        public Task Visit(LeftSessionResponse response) => Task.CompletedTask;
        public Task Visit(LostGameResponse response) => Task.CompletedTask;
        public Task Visit(NewSessionsAddedResponse response) => Task.CompletedTask;
        public Task Visit(OkResponse response) => Task.CompletedTask;
        public Task Visit(SendMapDataResponse response) => Task.CompletedTask;
        public Task Visit(SendPlayerListResponse response) => Task.CompletedTask;
        public Task Visit(SendSessionDataResponse response) => Task.CompletedTask;
        public Task Visit(SendTextResponse response) => Task.CompletedTask;
        public Task Visit(SendTileUpdateResponse response) => Task.CompletedTask;
        public Task Visit(StartedBattleResponse response) => Task.CompletedTask;
        public Task Visit(StartedGameResponse response) => Task.CompletedTask;
        public Task Visit(WonGameResponse response) => Task.CompletedTask;
    }
}
