using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Responses;
using BattleshipsCoreClient.Observer;
using Message = BattleshipsCore.Interfaces.Message;

namespace BattleshipsCoreClient
{
    public partial class SessionForm : Form, ISubscriber
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

            await Program.SwitchToConnectionFormFrom(this);
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

        public async Task UpdateAsync(Message message)
        {
            if (message is SendSessionListResponse sessionListResponse)
            {
                // You must use Invoke if you want to modify winforms components from this Update function
                // because this UpdateAsync is running on a background thread
                SessionListGrid.Invoke(() =>
                {
                    RefreshSessions(sessionListResponse);
                });
            }
            else if (message is SendSessionKeyResponse sskr)
            {
                await Program.SwitchToActiveSessionFormFromSessionList(sskr.SessionKey);
            }
            else if (message is JoinedSessionResponse jsr)
            {
                await Program.SwitchToActiveSessionFormFromSessionList(jsr.SessionId);
            }
            else if (message is DisconnectResponse)
            {
                await Program.SwitchToConnectionFormFrom(this);
            }
        }
    }
}
