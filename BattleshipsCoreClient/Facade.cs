using BattleshipsCore.Game;
using BattleshipsCore.Requests;
using BattleshipsCoreClient.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient
{
    public class Facade
    {
        public Facade()
        {

        }
        public static Start ConnectionForm;
        public static SessionForm SessionForm;
        public static ActiveSessionForm ActiveSessionForm;
        public static PlacementForm PlacementForm;
        public static ShootingForm ShootingForm;

        public static GameClientManager _gm;

        public void StartGame()
        {
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConnectionForm = new Start();
            SessionForm = new SessionForm();
            ActiveSessionForm = new ActiveSessionForm();
            PlacementForm = new PlacementForm(1);
            ShootingForm = new ShootingForm();
            _gm = GameClientManager.Instance;

            Application.Run(ConnectionForm);
        }
            
        public static async Task SwitchToConnectionFormFrom<TGameForm>(TGameForm form)
            where TGameForm : Form, ISubscriber
        {
            if (_gm.Client != null)
            {
                _gm.Client.Unsubscribe(form);
                _gm.Client.Subscribe(ConnectionForm);

                await _gm.DisconnectAsync();
            }

            form.Invoke(() =>
            {
                ConnectionForm.Show();
                SessionForm.Hide();
                ActiveSessionForm.Hide();
                PlacementForm.Hide();
                ShootingForm.Hide();
            });
        }

        public static async Task SwitchToSessionListFrom<TGameForm>(TGameForm form)
            where TGameForm : Form, ISubscriber
        {
            _gm.Client.Unsubscribe(form);
            _gm.Client.Subscribe(SessionForm);

            await _gm.Client.SendMessageAsync(new GetSessionListRequest());

            form.Invoke(() =>
            {
                SessionForm.Show();
                ConnectionForm.Hide();
                ActiveSessionForm.Hide();
                PlacementForm.Hide();
                ShootingForm.Hide();
            });
        }

        public static async Task SwitchToActiveSessionFormFromSessionList(Guid sessionKey)
        {
            _gm.Client.Unsubscribe(SessionForm);
            _gm.Client.Subscribe(ActiveSessionForm);

            await _gm.Client.SendMessageAsync(new GetSessionDataRequest(sessionKey));

            SessionForm.Invoke(() =>
            {
                ActiveSessionForm.Show();
                ConnectionForm.Hide();
                SessionForm.Hide();
                PlacementForm.Hide();
                ShootingForm.Hide();
            });
        }

        public static async Task SwitchToPlacementFormFromActiveSession(Guid sessionKey, string playerName)
        {
            _gm.Client.Unsubscribe(ActiveSessionForm);
            _gm.Client.Subscribe(PlacementForm);

            await _gm.Client.SendMessageAsync(new GetMapDataRequest(sessionKey, playerName));

            ActiveSessionForm.Invoke(() =>
            {
                PlacementForm.Show();
                ConnectionForm.Hide();
                SessionForm.Hide();
                ActiveSessionForm.Hide();
                ShootingForm.Hide();
            });
        }

        public static async Task EnableShootingForm()
        {
            _gm.Client.Subscribe(ShootingForm);

            await _gm.Client.SendMessageAsync(new GetOpponentMapRequest(_gm.PlayerName!));
            await _gm.Client.SendMessageAsync(new GetMyTurnRequest(_gm.PlayerName!));

            PlacementForm.Invoke(() =>
            {
                ShootingForm.Show();
                ConnectionForm.Hide();
                SessionForm.Hide();
                ActiveSessionForm.Hide();
            });
        }

        public static async Task LeaveShootingForm()
        {
            _gm.Client.Unsubscribe(PlacementForm);
            _gm.Client.Unsubscribe(ShootingForm);
            _gm.Client.Subscribe(SessionForm);

            await _gm.Client.SendMessageAsync(new GetSessionListRequest());

            ShootingForm.Invoke(() =>
            {
                SessionForm.Show();
                ConnectionForm.Hide();
                ActiveSessionForm.Hide();
                PlacementForm.Hide();
                PlacementForm.ClearData();
                ShootingForm.Hide();
            });
        }
    }
}
