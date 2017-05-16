using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace INFOMAA_Assignment
{
    public class Logger
    {
        // Propensities per per t per player
        // ActionSet[] _propensitiesPerTimeStep;

        private Dictionary<int, List<double>> _meanPerTimeStep = new Dictionary<int, List<double>>();

        int[] _colissionsPerTimeStep;

        int _numSteps;

        private ActionSet _actionSet;

        string _hashcode;
        private string _parameters;
        private Dictionary<string, string> paramMap;

        public Logger(int timeSteps, ActionSet actionSet, Dictionary<string, string> parameters)
        {
            _numSteps = timeSteps;
            _actionSet = actionSet;
            _colissionsPerTimeStep = new int[timeSteps];

            foreach (int degrees in _actionSet.Keys)
                _meanPerTimeStep.Add(degrees, new List<double>());

            paramMap = parameters;
            _parameters = "";
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                _parameters += $"{parameter.Key}{parameter.Value}";
            }

            _hashcode = DateTime.Now.GetHashCode().ToString("x8");
        }

        public List<int> GetActions()
        {
            return _actionSet.Keys.ToList();
        }

        public List<double> GetMeansOfAction(int actionKey)
        {
            return _meanPerTimeStep[actionKey];
        }

        public int[] Collisions { get { return _colissionsPerTimeStep; } }

        public string Parameters { get { return _parameters; } }



        public string GetParameterValue(string key)
        {
            return paramMap[key];
        }

        public void LogCollision(int timeStep)
        {
            _colissionsPerTimeStep[timeStep]++;
        }

        public void LogActionMeans(int timeStep, Dictionary<int, List<int>> meanRewardPerAction)
        {
            foreach (int action in meanRewardPerAction.Keys)
            {
                double sum = 0;
                foreach (int score in meanRewardPerAction[action])
                    sum += score;

                double mean = 0;

                if (meanRewardPerAction[action].Count > 0)
                    mean = sum / meanRewardPerAction[action].Count;
                _meanPerTimeStep[action].Add(mean);
            }
        }

        public void Dump()
        {
            Console.WriteLine("\nDumping collisions and scores per action per time step");
            DumpAll();
        }

        private void DumpAll()
        {
            string[] entries = new string[_numSteps + 1];
            entries[0] = CreateHeader(_actionSet);

            for (int i = 1; i <= _numSteps; i++)
            {
                List<double> meansPerAction = new List<double>();
                foreach (KeyValuePair<int, List<double>> kvp in _meanPerTimeStep)
                    meansPerAction.Add(kvp.Value[i - 1]);
                entries[i] += CreateEntry(i, meansPerAction);
            }
            File.WriteAllLines(_hashcode + _parameters + "_result.csv", entries);
        }

        private string CreateEntry(int timeStep, List<double> meansPerAction)
        {
            string entry = $"{timeStep}";
            foreach (double mean in meansPerAction)
                entry += $";{mean:0.000}";
            entry += $";{_colissionsPerTimeStep[timeStep - 1]}";
            return entry;
        }

        private string CreateHeader(ActionSet actionSet)
        {
            string header = "time";
            foreach (KeyValuePair<int, int> kvp in actionSet)
                header += $";action{kvp.Key}degrees"; // zonder spaties voor matlab :)
            header += $";collisions";
            return header;
        }
    }
}
