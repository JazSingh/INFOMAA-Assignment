using System;
using System.Collections.Generic;
using System.IO;

namespace INFOMAA_Assignment
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int[] numActions = { 10, 8, 6, 4, 2 };
            int[] numPlayers = { 10, 50, 65, 75, 100, 150, 250, 500 };
            int[] widths = { 50, 100, 250 };
            int[] heights = { 500, 1000 };
            int[] positiveRewards = { 1, 3, 5, 10 };
            int[] negativeRewards = { 0, -1, -3, -5, -10 };
            int[] speeds = { 3, 5, 8 };
            int[] epsilons = { 0, 1000, 100, 10, 2, 1 }; // 1/e when applying this value
            int[] collisionRadius = { 3, 5, 8 };

            Dictionary<string, int[]> independentVariableRuns = new Dictionary<string, int[]>
            {
                {ParamNameConstants.NUMPLAYERS, numPlayers},
                {ParamNameConstants.NUMACTIONS, numActions},
                {ParamNameConstants.WIDTH, widths},
                {ParamNameConstants.HEIGHT, heights},
                {ParamNameConstants.POSREWARD, positiveRewards},
                {ParamNameConstants.NEGREWARD, negativeRewards},
                {ParamNameConstants.SPEED, speeds},
                {ParamNameConstants.EPSILON, epsilons},
                {ParamNameConstants.COLLISIONRADIUS, collisionRadius}
            };

            string sessionHash = GetSessionHash();
            Directory.CreateDirectory(sessionHash);
            int numRuns = 25;
            int gameLength = 100000;

            foreach (KeyValuePair<string, int[]> kvp in independentVariableRuns)
            {
                string independentVariable = kvp.Key;
                int[] independentVariableValues = kvp.Value;

                LogSummarizer summarizer = new LogSummarizer(independentVariable, sessionHash);
                Console.WriteLine($"\nIndependent variable: {independentVariable}");
                foreach (int independentVariableValue in independentVariableValues)
                {
                    Console.WriteLine($"\n\n{independentVariable}: {independentVariableValue}");
                    List<Logger> logs = new List<Logger>(numRuns);
                    ParameterSettings settings = ParameterBaseline.Default(independentVariable, independentVariableValue);

                    for (int run = 0; run < numRuns; run++)
                    {
                        Console.WriteLine($"\nRun:{run + 1}/{numRuns}");

                        int seed = GenerateSeed(settings, run);
                        Random randomSource = new Random(seed);

                        Game game = new Game(settings, randomSource, gameLength, sessionHash);
                        game.Start();

                        logs.Add(game.Logger);
                    }
                    LogSquasher squasher = new LogSquasher(logs, settings, gameLength, logs[0].ActionSet);
                    summarizer.AddLog(squasher.Squash());
                    Console.Clear();
                }
                summarizer.FlushAll();
            }

            Console.WriteLine("\n\nRun finished, press enter to terminate...");
            Console.Read();
        }

        public static int GenerateSeed(ParameterSettings settings, int iteration)
        {
            int epsilon = (int)(settings.Epsilon * 10000);
            int seed;
            unchecked // 2,3,5,7,11,13,17,19,23,29 prime numbers 
            {
                seed = settings.NumActions * 2
                               + settings.NumPlayers * 3
                               + settings.Width * 7
                               + settings.Height * 11
                               + settings.PositiveReward * 13
                               + settings.NegativeReward * 17
                               + settings.Speed * 19
                               + epsilon * 23
                               + iteration * 29;
            }
            return unchecked(seed * 37);
        }

        static string GetSessionHash()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            return $"Session_{DateTime.Now.ToString("s").Replace(':', '-')}_{DateTime.Now.GetHashCode().ToString("x8")}";
        }
    }
}
