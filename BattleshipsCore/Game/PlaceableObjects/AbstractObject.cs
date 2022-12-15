using BattleshipsCore.Game.ChainOfResponse;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects.Builder;
using Newtonsoft.Json;
namespace BattleshipsCore.Game.PlaceableObjects
{
    public abstract class Abstract_Level
    {
        public abstract Product_Level_One_Ships CreateLevel1(int type, string name, int length, int max);
        public abstract Product_Level_Two_Ships CreateLevel2(int type, string name, int length, int max);
    }

    public class Level1Factory : Abstract_Level
    {
        ShipDirector director = new ShipDirector();

        public override Product_Level_One_Ships CreateLevel1(int type, string name, int length, int max)
        {
            return (Product_Level_One_Ships)Chain.ChainOfShips(type, name, length, max);
        }
        public override Product_Level_Two_Ships CreateLevel2(int type, string name, int length, int max)
        {
            return null;
        }
    }
    public class Level2Factory : Abstract_Level
    {
        public override Product_Level_One_Ships CreateLevel1(int type, string name, int length, int max)
        {
            return null;
        }

        public override Product_Level_Two_Ships CreateLevel2(int type, string name, int length, int max)
        {
            return (Product_Level_Two_Ships)Chain.ChainOfShips(type, name, length, max);
        }
    }
    public abstract class Product_Level_One_Ships : Ship
    {
        public int ShootingRange { get; set; }
        public int NavalArtillery { get; set; }

        public abstract void GenerateShip();
        public Product_Level_One_Ships(int length, string name, int max): base(name, max, length)
        {
        }
    }
    public abstract class Product_Level_Two_Ships : Ship
    {  
        public abstract void GenerateShip();      
        public Product_Level_Two_Ships(int length, string name, int max): base(name, max, length)
        {
        }     
    }

    public class Boat : Product_Level_One_Ships
    {
        public override TileType Type => TileType.Ship;
        public int ShipType {get;set;}

        public Boat(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv1 " + Name + " was generated" + " | ShootingRange: " + ShootingRange + " | NavalArtillery: " + NavalArtillery);
        }
    }

    public class SailBoat : Product_Level_One_Ships
    {
        public override TileType Type => TileType.Ship;
        public int ShipType {get;set;}

        public SailBoat(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv1 " + Name + " was generated" + " | ShootingRange: " + ShootingRange + " | NavalArtillery: " + NavalArtillery);
        }
    }
    public class Brig : Product_Level_One_Ships
    {
        public override TileType Type => TileType.Ship;
        public int ShipType {get;set;}

        public Brig(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv1 " + Name + " was generated" + " | ShootingRange: " + ShootingRange + " | NavalArtillery: " + NavalArtillery);
        }
    }

    public class NarrowBoat : Product_Level_Two_Ships
    {
        public override TileType Type => TileType.NarrowBoat;
        public int ShipType {get;set;}

        public NarrowBoat(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv2 " + Name + " was generated");
        }
    }

    public class Cruise : Product_Level_Two_Ships
    {
        public override TileType Type => TileType.Cruise;
        public int ShipType {get;set;}

        public Cruise(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv2 " + Name + " was generated");
        }
    }

    public class Tanker : Product_Level_Two_Ships
    {
        public override TileType Type => TileType.Tanker;
        public int ShipType {get;set;}

        public Tanker(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv2 " + Name + " was generated");
        }
    }
}