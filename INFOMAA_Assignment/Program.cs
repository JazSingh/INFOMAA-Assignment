using System;

namespace INFOMAA_Assignment
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // angles to test: All divisors of 360 > 10: 12 15 18 20 24 30 36 40 45 60 72 90
            //                                        k: 30, 24, 20, 18, 12, 10, 9, 8, 6, 5, 4

            /*
            int[] ks = { 30, 24, 20, 18, 12, 10, 9, 8, 6, 5, 4, 3, 2 };
            int[] numPlayers = { 2, 3, 5, 7, 8, 10, 15, 25, 50, 100, 250, 500, 1000 };
            int[] widths = { 20, 30, 50, 75, 100, 150, 250, 500, 1000 };
            int[] heights = { 20, 30, 50, 75, 100, 150, 250, 500, 1000 };
            int[] positiveRewards = { 1, 5, 100, 1000 };
            int[] negativeRewards = { -1, -5, -10, -100, -500, -1000 };
            */

            int numberOfPlayers = 400;
            int positiveReward = 1;
            int negativeReward = -10;
            int speed = 3;
            int gameLength = 100;
            Torus torus = new Torus(750, 750);
            ActionSet actionSet = new ActionSet(4);
            Distribution distribution = new Distribution(0.01, new Random(1));

            Game game = new Game(torus, numberOfPlayers, actionSet, 3, positiveReward, negativeReward, speed, distribution, gameLength);
            game.Start();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
