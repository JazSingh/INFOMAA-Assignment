using System;
using System.Collections.Generic;
using System.IO;

namespace INFOMAA_Assignment
{
    public class Logger
    {
        // Propensities per per t per player
        ActionSet[] _propensitiesPerTimeStep;
        int[] _colissionsPerTimeStep;

        int _numSteps;
        int _numPlayers;

        string _timestamp;

        public Logger(int timeSteps, int numPlayers)
        {
            _timestamp = DateTime.Now.ToString("O");
            _numSteps = timeSteps;
            _numPlayers = numPlayers;
            _propensitiesPerTimeStep = new ActionSet[timeSteps];
            _colissionsPerTimeStep = new int[timeSteps];
        }

        public void LogActionSet(int timeStep, int player, ActionSet actionSet)
        {
            if (_propensitiesPerTimeStep[timeStep] == null)
            {
                _propensitiesPerTimeStep[timeStep] = actionSet.CleanCopy();
            }

            foreach (KeyValuePair<int, int> kvp in actionSet)
            {
                _propensitiesPerTimeStep[timeStep][kvp.Key] += kvp.Value;
            }
        }

        public void LogCollision(int timeStep)
        {
            _colissionsPerTimeStep[timeStep]++;
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
            string[] entries = new string[_numSteps + 1];
            entries[0] = CreateHeader(_propensitiesPerTimeStep[0]);
            for (int i = 1; i < _numSteps + 1; i++)
            {
                entries[i] += CreateEntry(i, _propensitiesPerTimeStep[i - 1]);
            }
            File.WriteAllLines("_scores.csv", entries);
        }

        private string CreateHeader(ActionSet set)
        {
            string header = "time";
            foreach (KeyValuePair<int, int> kvp in set)
            {
                header += $";{kvp.Key} deg";
            }
            return header;
        }

        private string CreateEntry(int timestep, ActionSet set)
        {
            string entry = $"{timestep}";
            foreach (KeyValuePair<int, int> kvp in set)
            {
                entry += $";{((kvp.Value/(double) _numPlayers)):0,000}";
            }
            return entry;
        }


        private void DumpColissions()
        {
            string[] entries = new string[_numSteps + 1];
            entries[0] = "step;numColissions\n";
            for (int i = 1; i < _numSteps + 1; i++)
            {
                entries[i] += $"{i};{_colissionsPerTimeStep[i - 1]}\n";
            }
            File.WriteAllLines("_collisions.csv", entries);
        }
    }
}
