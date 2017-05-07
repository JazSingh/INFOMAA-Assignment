using System;
using System.Collections.Generic;
using System.IO;

namespace INFOMAA_Assignment
{
    public class Logger
    {
        // Propensities per per t per player
        ActionSet[] propensitiesPerTimeStep;
        int[] colissionsPerTimeStep;

        int numSteps;
        int numPlayers;

        string timestamp;

        public Logger(int timeSteps, int numPlayers)
        {
            timestamp = DateTime.Now.ToString("O");
            numSteps = timeSteps;
            this.numPlayers = numPlayers;
            propensitiesPerTimeStep = new ActionSet[timeSteps];
            colissionsPerTimeStep = new int[timeSteps];
        }

        public void LogActionSet(int timeStep, int player, ActionSet actionSet)
        {
            if (propensitiesPerTimeStep[timeStep] == null)
            {
                propensitiesPerTimeStep[timeStep] = actionSet.CleanCopy();
            }

            foreach (KeyValuePair<int, int> kvp in actionSet)
            {
                propensitiesPerTimeStep[timeStep][kvp.Key] += kvp.Value;
            }
        }

        public void LogCollision(int timeStep)
        {
            colissionsPerTimeStep[timeStep]++;
        }

        public void Dump()
        {
            Console.WriteLine("Dumping scores per action per time step");
            DumpScoresPerActionPerTimeStep();
            Console.WriteLine("Dumping collisions");
            DumpColissions();
        }

        private void DumpScoresPerActionPerTimeStep()
        {
            string[] entries = new string[numSteps + 1];
            entries[0] = CreateHeader(propensitiesPerTimeStep[0]);
            for (int i = 1; i < numSteps + 1; i++)
            {
                entries[i] += CreateEntry(i, propensitiesPerTimeStep[i - 1]);
            }
            File.WriteAllLines("_scores.csv", entries);
        }

        private string CreateHeader(ActionSet set)
        {
            string header = "time";
            foreach (KeyValuePair<int, int> kvp in set)
            {
                header += string.Format(";{0} deg", kvp.Key);
            }
            return header;
        }

        private string CreateEntry(int timestep, ActionSet set)
        {
            string entry = string.Format("{0}", timestep);
            foreach (KeyValuePair<int, int> kvp in set)
            {
                entry += string.Format(";{0:0,000}", ((kvp.Value / (double)numPlayers)));
            }
            return entry;
        }


        private void DumpColissions()
        {
            string[] entries = new string[numSteps + 1];
            entries[0] = "step;numColissions\n";
            for (int i = 1; i < numSteps + 1; i++)
            {
                entries[i] += string.Format("{0};{1}\n", i, colissionsPerTimeStep[i - 1]);
            }
            File.WriteAllLines("_collisions.csv", entries);
        }
    }
}
