using System.Net;
using BattleshipsCore.Game;
using BattleshipsCore.Responses;
using BattleshipsCoreClient.Observer;
using Message = BattleshipsCore.Interfaces.Message;

namespace BattleshipsCoreClient
{
    public partial class Start : Form, ISubscriber
    {
        private string? _username;

        public Start()
        {
            InitializeComponent();

            FormClosed += Start_FormClosed;
        }

        public void ShowWindow()
        {
            Program.SessionForm.Hide();
            Program.ActiveSessionForm.Hide();
            Program.PlacementForm.Hide();

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

        public async Task UpdateAsync()
        {
            var client = GameClientManager.Instance.Client;
            if (client == null) return;

            var message = client.GetMessage();

            if (message is JoinedServerResponse)
            {
                GameClientManager.Instance.PlayerName = _username;

                await Program.SwitchToSessionListFrom(this);
            }
            else if (message is FailResponse)
            {
                MessageBox.Show("Could not connect to the server.", "Connection Error");
            }
        }
    }
}
