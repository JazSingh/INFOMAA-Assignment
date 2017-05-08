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
            int numberOfPlayers = 20;
            int positiveReward = 1;
            int negativeReward = -100;
            int speed = 3;
            int gameLength = 1000;
            Torus torus = new Torus(100, 100);
            ActionSet actionSet = new ActionSet(6);
            Distribution distribution = new Distribution(0.05, new Random(1));

            Game game = new Game(torus, numberOfPlayers, actionSet, 3, positiveReward, negativeReward, speed, distribution, gameLength);
            game.Start();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
