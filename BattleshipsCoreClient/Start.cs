using System.Net;

namespace BattleshipsCoreClient
{
    public partial class Start : Form
    {
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

        private void ConnectClick(object sender, EventArgs e)
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
                    var username = UsernameTextBox.Text;
                    
                    if (IPAddress.TryParse(IpTextBox.Text, out var ipAddress))
                    {
                        var success = GameClientManager.Instance.Connect(ipAddress, username);

                        if (success)
                        {
                            Program.SessionForm.ShowWindow();
                        }
                        else
                        {
                            MessageBox.Show("Failed to connect to server.", "Error");
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not connect to the server.", "Connection Error");
                }
            }
        }
    }
}
