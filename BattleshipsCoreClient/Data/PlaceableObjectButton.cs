using BattleshipsCore.Game.PlaceableObjects;

namespace BattleshipsCoreClient.Data
{
    public class PlaceableObjectButton
    {
        public Button Button { get; set; }
        public PlaceableObject PlaceableObject { get; set; }
        public int LeftCount { get; set; }

        public PlaceableObjectButton(Button button, PlaceableObject placeableObject, int leftCount)
        {
            PlaceableObject = placeableObject;
            Button = button;
            LeftCount = leftCount;
        }
    }
}
