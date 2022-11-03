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
            if(type == 1){
                return director.Construct(new OneSailShipBuilder(length, name, max, type));
            }
            else if(type == 2){
                return director.Construct(new TwoSailShipBuilder(length, name, max, type));
            }
            else if(type == 3){
                return director.Construct(new ThreeSailShipBuilder(length, name, max, type));

            }
            return null;
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
            if(type == 4){

                return new OneSailSuperShip(length, name, max, type);
            }
            else if(type == 5){
                return new TwoSailSuperShip(length, name, max, type);
            }
            else if(type == 6){
                return new ThreeSailSuperShip(length, name, max, type);

            }
            return null;
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

    public class OneSailShip : Product_Level_One_Ships
    {
        public int ShipType {get;set;}
        public OneSailShip(int length, string name, int max, int shipType) : base(length, name, max)

        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){

            Console.WriteLine("Lv1 " + Name + " was generated" + " | ShootingRange: " + ShootingRange + " | NavalArtillery: " + NavalArtillery);
        }
    }
    public class TwoSailShip : Product_Level_One_Ships
    {
        public int ShipType {get;set;}
        public TwoSailShip(int length, string name, int max, int shipType) : base(length, name, max)

        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){

            Console.WriteLine("Lv1 " + Name + " was generated" + " | ShootingRange: " + ShootingRange + " | NavalArtillery: " + NavalArtillery);
        }
    }
    public class ThreeSailShip : Product_Level_One_Ships
    {
        public int ShipType {get;set;}
        public ThreeSailShip(int length, string name, int max, int shipType) : base(length, name, max)

        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){

            Console.WriteLine("Lv1 " + Name + " was generated" + " | ShootingRange: " + ShootingRange + " | NavalArtillery: " + NavalArtillery);
        }
    }
    public class OneSailSuperShip : Product_Level_Two_Ships
    {
        public int ShipType {get;set;}
        public OneSailSuperShip(int length, string name, int max, int shipType) : base(length, name, max)

        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv2 " + Name + " was generated");
        }
    }

    public class TwoSailSuperShip : Product_Level_Two_Ships
    {
        public int ShipType {get;set;}
        public TwoSailSuperShip(int length, string name, int max, int shipType) : base(length, name, max)

        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv2 " + Name + " was generated");
        }
    }

    public class ThreeSailSuperShip : Product_Level_Two_Ships
    {
        public int ShipType {get;set;}
        public ThreeSailSuperShip(int length, string name, int max, int shipType) : base(length, name, max)

        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv2 " + Name + " was generated");
        }
    }
}