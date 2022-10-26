using BattleshipsCore.Game.GameGrid;
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
        public override Product_Level_One_Ships CreateLevel1(int type, string name, int length, int max)
        {
            if(type == 1){
                return new Ship1(length, name, max, type);
            }
            else if(type == 2){
                return new Ship2(length, name, max, type);
            }
            else if(type == 3){
                return new Ship3(length, name, max, type);
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
            if(type == 1){
                return new SuperShip1(length, name, max, type);
            }
            else if(type == 2){
                return new SuperShip2(length, name, max, type);
            }
            else if(type == 3){
                return new SuperShip3(length, name, max, type);
            }
            return null;
        }
    }
    public abstract class Product_Level_One_Ships : Ship
    {
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
    public class Ship1 : Product_Level_One_Ships
    {
        public int ShipType {get;set;}
        public Ship1(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv1 " + Name + " was generated");
        }
    }
    public class Ship2 : Product_Level_One_Ships
    {
        public int ShipType {get;set;}
        public Ship2(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv1 " + Name + " was generated");
        }
    }
    public class Ship3 : Product_Level_One_Ships
    {
        public int ShipType {get;set;}
        public Ship3(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv1 " + Name + " was generated");
        }
    }
    public class SuperShip1 : Product_Level_Two_Ships
    {
        public int ShipType {get;set;}
        public SuperShip1(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv2 " + Name + " was generated");
        }
    }
    public class SuperShip2 : Product_Level_Two_Ships
    {
        public int ShipType {get;set;}
        public SuperShip2(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv2 " + Name + " was generated");
        }
    }
    public class SuperShip3 : Product_Level_Two_Ships
    {
        public int ShipType {get;set;}
        public SuperShip3(int length, string name, int max, int shipType) : base(length, name, max)
        {
            this.ShipType = shipType;
        }
        public override void GenerateShip(){
            Console.WriteLine("Lv2 " + Name + " was generated");
        }
    }
}