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

        string _hashcode;
        string _parameters;

        public Logger(int timeSteps, int numPlayers, string[] parameters)
        {
            _numSteps = timeSteps;
            this._numPlayers = numPlayers;
            _propensitiesPerTimeStep = new ActionSet[timeSteps];
            _colissionsPerTimeStep = new int[timeSteps];

            _parameters = "";
            foreach (string parameter in parameters)
            {
                _parameters += string.Format("-{0}", parameter);
            }

            _hashcode = GetHashCode().ToString("x8");
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
            File.WriteAllLines(_hashcode + _parameters + "_scores.csv", entries);
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
                entry += string.Format(";{0:0.000}", ((kvp.Value / (double)_numPlayers)));
            }
            return entry;
        }


        private void DumpColissions()
        {
            string[] entries = new string[_numSteps + 1];
            entries[0] = "step;numColissions\n";
            for (int i = 1; i < _numSteps + 1; i++)
            {
                entries[i] += string.Format("{0};{1}\n", i, _colissionsPerTimeStep[i - 1]);
            }
            File.WriteAllLines(_hashcode + _parameters + "_collisions.csv", entries);
        }
    }
}
