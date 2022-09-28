using BattleshipsCore.Data;
using BattleshipsCore.Requests;
using BattleshipsCore.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public void UpdateSessionData(GameSessionData sessionData)
        {
            SessionData = sessionData;
            JoinedPlayers = sessionData.PlayerNames;

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
                ClearData();

                Program.SessionForm.Show();
                Hide();
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
                MessageBox.Show("Failed to refresh.", "Error");
                return;
            }

            var sessionResponse = GameClientManager.Instance.Client!
                    .SendCommand<GetSessionDataRequest, SendSessionDataResponse>(
                    new GetSessionDataRequest(SessionData.SessionKey));

            if (sessionResponse == null)
            {
                MessageBox.Show("Failed to refresh.", "Error");
                return;
            }

            UpdateSessionData(sessionResponse.SessionData);
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {

        }

        private void ClearData()
        {
            SessionData = null;
            JoinedPlayers.Clear();
        }
    }
}
