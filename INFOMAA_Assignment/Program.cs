using System;

namespace INFOMAA_Assignment
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Torus torus = new Torus(100, 100);
            ActionSet actionSet = new ActionSet(6);
            Distribution distribution = new Distribution(0.5, new Random(1));

            Game game = new Game(torus, 5, actionSet, 3, 1, -1, 10, distribution, 3600);
            game.Start();
            Console.ReadLine();
        }
    }
}
