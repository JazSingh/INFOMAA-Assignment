using System;

namespace INFOMAA_Assignment
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // angles to test: All divisors of 360 > 10: 12 15 18 20 24 30 36 40 45 60 72 90
            //                                        k: 30, 24, 20, 18, 12, 10, 9, 8, 6, 5, 4

            int[] ks = { 30, 24, 20, 18, 12, 10, 9, 8, 6, 5, 4 };
            Torus torus = new Torus(100, 100);
            ActionSet actionSet = new ActionSet(30);
            Distribution distribution = new Distribution(0.5, new Random(1));

            Game game = new Game(torus, 10, actionSet, 3, 1, -10, 10, distribution, 1000000);
            game.Start();
            Console.ReadLine();
        }
    }
}
