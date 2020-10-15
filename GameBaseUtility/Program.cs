using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebApiTest;
namespace GameBaseUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "";
            while (input != "q")
            {
                Console.WriteLine("GameBase Update Utility: Select Operation\n[1] Insert Game\n[q] Quit");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        InsertGame();
                        break;
                    default:
                        break;
                }
                Console.Clear();
            }

        }

        private static void InsertGame()
        {
            Console.Clear();
            using(  var db = new gamebase1Entities())
            {
                var game = new Game();
                foreach(var items in game.GetType().GetProperties())
                {

                    if (items.PropertyType.Name == "String" || items.PropertyType.Name == "Nullable`1")
                    {
                        Console.Write(items.Name + ": ");
                        if (items.PropertyType.Name == "Nullable`1")
                        {
                            items.SetValue(game, DateTime.Parse(Console.ReadLine()));

                        }
                        else
                        {
                            items.SetValue(game, Console.ReadLine());
                        }

                    }
                    else if(items.PropertyType.Name=="Guid")
                    {
                        items.SetValue(game, Guid.NewGuid());
                    }

                }
                db.Games.Add(game);
                db.SaveChanges();
    
            }
            
            
            
        }
    }
}
