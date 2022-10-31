using BattleshipsCore.Game.PlaceableObjects;

namespace BattleshipsCoreClient.Data
{
    internal class PlaceableObjectData
    {
        public PlaceableObject PlaceableObject { get; set; }
        public int LeftCount { get; set; }

        public PlaceableObjectData(PlaceableObject placeableObject, int leftCount)
        {
            PlaceableObject = placeableObject;
            LeftCount = leftCount;
        }
    }
}
