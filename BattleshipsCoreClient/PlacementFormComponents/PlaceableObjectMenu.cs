using BattleshipsCore.Game.PlaceableObjects;
using BattleshipsCoreClient.Data;

namespace BattleshipsCoreClient.PlacementFormComponents
{
    internal class PlaceableObjectMenu
    {
        private readonly Color _inactiveColor = Color.White;
        private readonly Color _activeColor = Color.LightGray;

        private readonly FlowLayoutPanel _placeableObjectButtonPanel;
        private readonly Dictionary<Guid, PlaceableObjectButton> _placeableObjectButtons;

        private PlaceableObjectButton? _selectedPlaceableObject;

        public Guid? SelectedButtonId
        {
            get
            {
                if (_selectedPlaceableObject == null) return null;

                return Guid.Parse(_selectedPlaceableObject.Button.Name);
            }
        }

        public PlaceableObject? Selection => _selectedPlaceableObject?.PlaceableObject;
        public bool HasSelection => _selectedPlaceableObject != null;
        public bool CanPlace => _selectedPlaceableObject != null && _selectedPlaceableObject.LeftCount > 0;

        public PlaceableObjectMenu(FlowLayoutPanel placeableObjectButtonPanel)
        {
            _placeableObjectButtonPanel = placeableObjectButtonPanel;
            _placeableObjectButtons = new Dictionary<Guid, PlaceableObjectButton>();
        }

        public void AddSelection(
            PlaceableObjectData data,
            EventHandler clickAction)
        {
            var id = Guid.NewGuid();
            var button = new Button
            {
                Name = id.ToString(),
                Text = PlaceableObjectButtonText(data.PlaceableObject.Name, data.PlaceableObject.MaximumCount),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = _inactiveColor,
            };

            button.Click += clickAction;

            _placeableObjectButtonPanel.Controls.Add(button);
            _placeableObjectButtons.Add(id, new PlaceableObjectButton(button, data.PlaceableObject, data.LeftCount));
        }

        public void SelectPlaceableObject(Guid? guid)
        {
            if (_selectedPlaceableObject != null)
            {
                _selectedPlaceableObject.Button.BackColor = _inactiveColor;
            }

            if (guid.HasValue && _placeableObjectButtons.ContainsKey(guid.Value))
            {
                _selectedPlaceableObject = _placeableObjectButtons[guid.Value];
                _selectedPlaceableObject.Button.BackColor = _activeColor;
            }
            else
            {
                _selectedPlaceableObject = null;
            }
        }

        public void UpdateSelection(Guid guid, bool shouldPlace)
        {
            if (_placeableObjectButtons.TryGetValue(guid, out var buttonData))
            {
                var increment = shouldPlace ? -1 : +1;

                buttonData.LeftCount += increment;
                buttonData.Button.Text = PlaceableObjectButtonText(buttonData.PlaceableObject.Name, buttonData.LeftCount);
            }
        }

        private static string PlaceableObjectButtonText(string objectName, int count)
        {
            return $"{objectName} x{count}";
        }

        public void Clear()
        {
            _placeableObjectButtons.Clear();
            _selectedPlaceableObject = null;
            _placeableObjectButtonPanel.Controls.Clear();
        }
    }
}
