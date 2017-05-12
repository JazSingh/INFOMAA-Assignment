using System;
using System.Collections.Generic;
using System.Linq;

namespace INFOMAA_Assignment
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // angles to test: All divisors of 360 > 10: 12 15 18 20 24 30 36 40 45 60 72 90
            //                                        k: 30, 24, 20, 18, 12, 10, 9, 8, 6, 5, 4

            int[] ks = { 10, 9, 8, 6, 5, 4 }; // 6
            int[] numPlayers = { 10, 15, 25, 50, 60, 65, 66, 67, 68, 69, 70, 75, 100, 250 }; // 6
            int[] widths = { 100, 250, 500, 1000 }; // 4
            int[] heights = { 100, 250, 500, 1000 }; // 4
            int[] positiveRewards = { 1, 5, 100, 1000 }; // 4
            int[] negativeRewards = { -1, -5, -10, -100, -1000 }; //5
            int[] speeds = { 2, 3, 5, 8 }; // 4 
            double[] epsilons = { 0.001d, 0.01d, 0.05d, 0.1d }; // 4

            foreach (int np in numPlayers)
            {
                LogSummarizer summarizer = new LogSummarizer("np", np.ToString());
                foreach (int k in ks)
                {
                    int numberOfPlayers = np;
                    int positiveReward = 5;
                    int negativeReward = -10;
                    int speed = 3;
                    int gameLength = 4000;
                    Torus torus = new Torus(250, 250);
                    ActionSet actionSet = new ActionSet(k);
                    Distribution distribution = new Distribution(0.01, new Random(k * (numberOfPlayers + positiveReward + negativeReward + speed + gameLength + torus.Height + torus.Width)));

                    Game game = new Game(torus, numberOfPlayers, actionSet, (int)(2 * speed), positiveReward, negativeReward, speed, distribution, gameLength);
                    game.Start();
                    summarizer.AddLog(game.Logger);
                    Console.WriteLine("Done");
                }
                summarizer.DumpCollissionSummary();
            }
            Console.WriteLine("Run finished, press any key to terminate...");
            Console.ReadLine();
        }
    }
}
