using System;
using System.Collections.Generic;
using System.IO;

namespace INFOMAA_Assignment
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int[] numActions = { 10, 9, 8, 6, 5, 4 };
            int[] numPlayers = { 25, 50, 60, 65, 70, 75, 100, 250 };
            int[] widths = { 100, 250, 500, 1000 };
            int[] heights = { 100, 250, 500, 1000 };
            int[] positiveRewards = { 1, 5, 100, 1000 };
            int[] negativeRewards = { -1, -5, -10, -100, -1000 };
            int[] speeds = { 2, 3, 5, 8 };
            int[] epsilons = { 1000, 100, 10, 50 }; // 1/e when applying this value
            int[] collisionRadius = { 3, 4, 5, 6, 8, 10 };

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
            int numRuns = 1;
            int gameLength = 10000;

            foreach (KeyValuePair<string, int[]> kvp in independentVariableRuns)
            {
                string independentVariable = kvp.Key;
                int[] independentVariableValues = kvp.Value;

                LogSummarizer summarizer = new LogSummarizer(independentVariable, sessionHash);
                Console.WriteLine($"\n\nIndependent variable: {independentVariable}");
                foreach (int independentVariableValue in independentVariableValues)
                {
                    Console.WriteLine($"{independentVariable}: {independentVariableValue}");
                    List<Logger> logs = new List<Logger>(numRuns);
                    ParameterSettings settings = ParameterBaseline.Default(independentVariable, independentVariableValue);

                    for (int run = 0; run < numRuns; run++)
                    {
                        Console.WriteLine($"Run:{run + 1}/{numRuns}");
                        int seed = GenerateSeed(settings, run);

                        Console.WriteLine($"Seed: {seed}");
                        Random randomSource = new Random(seed);

                        Game game = new Game(settings, randomSource, gameLength, sessionHash);
                        game.Start();

                        Console.WriteLine("\nDone\n");
                        logs.Add(game.Logger);
                    }
                    LogSquasher squasher = new LogSquasher(logs, settings, gameLength, logs[0].ActionSet);
                    summarizer.AddLog(squasher.Squash());
                }
                summarizer.FlushAll();
            }

            Console.WriteLine("Run finished, press enter to terminate...");
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
