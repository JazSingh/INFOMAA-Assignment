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
            ActionSet actionSet = new ActionSet(6);
            Distribution distribution = new Distribution(0.05, new Random(1));

            Game game = new Game(torus, 20, actionSet, 3, 1, -100, 3, distribution, 100000);
            game.Start();
            Console.ReadLine();
        }
    }
}
