using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Game.SessionObserver;
using System;

namespace BattleshipsCoreClient
{
    public partial class SessionForm : Form, ISessionObserver
    {
        public List<GameSessionData> SessionList { get; set; }

        public SessionForm()
        {
            SessionList = new List<GameSessionData>();

            InitializeComponent();

            FormClosed += SessionForm_FormClosed;
        }

        public void ShowWindow()
        {
            Program.ConnectionForm.Hide();
            Program.ActiveSessionForm.Hide();
            Program.PlacementForm.Hide();
            Program.ShootingForm.Hide();

            RefreshSessions();
            Show();
            if (GameClientManager.Instance.Client != null) GameClientManager.Instance.Client.Attach(this);
        }

        private void SessionForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void SessionRow_Click(object sender, DataGridViewCellEventArgs e)
        {
            var rowClicked = e.RowIndex;

            if (rowClicked < 0 || rowClicked >= SessionList.Count) return;

            var sessionData = SessionList[rowClicked];

            JoinSession(sessionData);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshSessions();
        }

        private void CreateSessionButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CreateSessionTextBox.Text))
            {
                MessageBox.Show("Could not create session because session name is empty or has spaces.");
            }
            else
            {
                var createdSessionData = GameClientManager.Instance.CreateSession(CreateSessionTextBox.Text);
                
                if (createdSessionData == null)
                {
                    MessageBox.Show("Could not create new session.", "Error");

                    return;
                }

               //Program.ActiveSessionForm.ShowWindow(createdSessionData);
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            var success = GameClientManager.Instance.Disconnect();

            if (!success)
            {
                MessageBox.Show("Already disconnected", "Error");
            }

            Program.ConnectionForm.ShowWindow();
        }

        private void RefreshSessions()
        {
            var sessionListResponse = GameClientManager.Instance.Client!
                .SendCommand<GetSessionListRequest, SendSessionListResponse>(
                new GetSessionListRequest());

            if (sessionListResponse == null)
            {
                MessageBox.Show("Could not refresh session list.", "Error");

                return;
            }

            SessionList = sessionListResponse.SessionList;
            SessionListGrid.Rows.Clear();

            foreach (var session in SessionList)
            {
                var row = SessionListGrid.Rows.Add();

                SessionListGrid.Rows[row].Cells[0].Value = session.SessionName;
                SessionListGrid.Rows[row].Cells[1].Value = session.PlayerNames.Count;
            }

        }

        private void JoinSession(GameSessionData session)
        {
            var success = GameClientManager.Instance.JoinSession(session);

            if (!success)
            {
                MessageBox.Show("Could not join session.", "Error");

                return;
            }

            Program.ActiveSessionForm.ShowWindow(session);
        }

        public void Update(SessionSubject sessionSubject)
        {
            MessageBox.Show("New sessions were added" );
        }
    }
}
