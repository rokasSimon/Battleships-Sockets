using System.Net;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;
using BattleshipsCoreClient.Observer;

namespace BattleshipsCoreClient
{
    public partial class Start : Form, ISubscriber, IResponseVisitor
    {
        private string? _username;

        public Start()
        {
            InitializeComponent();

            FormClosed += Start_FormClosed;
        }

        public void ShowWindow()
        {
            Facade.SessionForm.Hide();
            Facade.ActiveSessionForm.Hide();
            Facade.PlacementForm.Hide();

            Show();
        }

        private void Start_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private async void ConnectClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                MessageBox.Show("No username provided or it has spaces", "Error");
            }
            else if (string.IsNullOrWhiteSpace(IpTextBox.Text))
            {
                MessageBox.Show("No ip address provided or it has spaces", "Error");
            }
            else
            {
                try
                {
                    _username = UsernameTextBox.Text;
                    
                    if (IPAddress.TryParse(IpTextBox.Text, out var ipAddress))
                    {
                        var clientConnected = await GameClientManager.Instance.EstablishClient(ipAddress);

                        if (!clientConnected)
                        {
                            MessageBox.Show("Could not establish connection to server. Try checking IP address.");
                            return;
                        }

                        GameClientManager.Instance.Client!.Subscribe(this);

                        await GameClientManager.Instance.Client!.SendMessageAsync(new JoinServerRequest(_username));
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not connect to the server.", "Connection Error");
                }
            }
        }

        public async Task UpdateAsync(AcceptableResponse message)
        {
            await message.Accept(this);
        }

        public Task Visit(FailResponse response)
        {
            MessageBox.Show("Could not connect to the server.", "Connection Error");

            return Task.CompletedTask;
        }

        public async Task Visit(JoinedServerResponse response)
        {
            GameClientManager.Instance.PlayerName = _username;

            await Facade.SwitchToSessionListFrom(this);
        }

        public Task Visit(ActiveTurnResponse response) => Task.CompletedTask;
        public Task Visit(InactiveTurnResponse response) => Task.CompletedTask;
        public Task Visit(LostGameResponse response) => Task.CompletedTask;
        public Task Visit(NewSessionsAddedResponse response) => Task.CompletedTask;
        public Task Visit(SendPlayerListResponse response) => Task.CompletedTask;
        public Task Visit(SendSessionDataResponse response) => Task.CompletedTask;
        public Task Visit(SendTextResponse response) => Task.CompletedTask;
        public Task Visit(StartedGameResponse response) => Task.CompletedTask;
        public Task Visit(WonGameResponse response) => Task.CompletedTask;
        public Task Visit(DisconnectResponse response) => Task.CompletedTask;
        public Task Visit(JoinedSessionResponse response) => Task.CompletedTask;
        public Task Visit(SendSessionKeyResponse response) => Task.CompletedTask;
        public Task Visit(SendSessionListResponse response) => Task.CompletedTask;
        public Task Visit(LeftSessionResponse response) => Task.CompletedTask;
        public Task Visit(OkResponse response) => Task.CompletedTask;
        public Task Visit(SendMapDataResponse response) => Task.CompletedTask;
        public Task Visit(SendTileUpdateResponse response) => Task.CompletedTask;
        public Task Visit(StartedBattleResponse response) => Task.CompletedTask;
    }
}
