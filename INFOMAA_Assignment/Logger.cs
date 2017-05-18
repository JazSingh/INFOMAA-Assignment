using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace INFOMAA_Assignment
{
    public class Logger
    {
        int _numSteps;
        int _numPlayers;
        string _sessionHash;
        string _parameterString;
        Dictionary<string, string> _paramMap;
        ActionSet _actionSet;
        int[] _actions;
        Dictionary<int, int> _actionMap;
        ParameterSettings _parameters;

        // Logs
        double[] _colissionsPerTimeStep;
        double[,] _meanPayoffPerTimeStep;
        double[,] _numPlayerPlayedActionPerTimeStep;

        public Logger(int timeSteps, ActionSet actionSet, ParameterSettings parameters, string sessionHash)
        {
            _numSteps = timeSteps;
            _actionSet = actionSet;
            _sessionHash = sessionHash;
            _parameters = parameters;

            // init logs
            _colissionsPerTimeStep = new double[timeSteps];
            _meanPayoffPerTimeStep = new double[timeSteps, _actionSet.Keys.Count];
            _numPlayerPlayedActionPerTimeStep = new double[timeSteps, _actionSet.Keys.Count];

            // lookup tables for actions -> indices
            _actionMap = new Dictionary<int, int>();
            _actions = actionSet.Keys.ToArray();
            for (int index = 0; index < _actions.Length; index++)
                _actionMap.Add(_actions[index], index);

            // Parse paramters
            _paramMap = parameters.GetMap();
            _numPlayers = parameters.NumPlayers;

            _parameterString = string.Empty;
            foreach (KeyValuePair<string, string> parameter in _paramMap)
                _parameterString += $"{parameter.Key}{parameter.Value}";
        }

        // GETTERS AND SETTERS
        public double[,] MeanScores
        {
            get => _meanPayoffPerTimeStep;
            set => _meanPayoffPerTimeStep = value;
        }

        public double[] Collisions
        {
            get => _colissionsPerTimeStep;
            set => _colissionsPerTimeStep = value;
        }

        public double[,] NumActionPlayed
        {
            get => _numPlayerPlayedActionPerTimeStep;
            set => _numPlayerPlayedActionPerTimeStep = value;
        }

        public string Parameters => _parameterString;
        public ParameterSettings ParameterSettings => _parameters;
        public string SessionHash => _sessionHash;
        public ActionSet ActionSet => _actionSet;
        public Dictionary<string, string> ParameterMap => _paramMap;

        public int[] GetActions()
        {
            return _actions;
        }

        public string GetParameterValue(string key)
        {
            return _paramMap[key];
        }

        // Logs a collision
        public void LogCollision(int timeStep)
        {
            _colissionsPerTimeStep[timeStep]++;
        }

        // Logs the reward of an action proportional to the number of players.
        public void LogAction(int timeStep, int action, int reward)
        {
            _meanPayoffPerTimeStep[timeStep, _actionMap[action]] += reward;
            _numPlayerPlayedActionPerTimeStep[timeStep, _actionMap[action]] += 1;
        }

        // Flush contents of log to file.
        public void FlushScoresToFile(string subDirectory, string independentVariable)
        {
            //Console.WriteLine("\nFlush scores per action per time step");
            string[] entries = new string[_numSteps + 1];
            entries[0] = CreateHeader(_actionSet);
            for (int i = 1; i <= _numSteps; i++)
                entries[i] += CreateScoreEntry(i - 1);

            string directory = Path.Combine(_sessionHash, subDirectory);
            Directory.CreateDirectory(directory);

            string varVal = independentVariable == "summary" ? "_summary" : _paramMap[independentVariable];
            string fileName = $"{_parameters.ParameterBaselineType}_{independentVariable}{varVal}_scores.csv";

            string outputFile = Path.Combine(directory, fileName);
            File.WriteAllLines(outputFile, entries);
        }

        // A single row in the output file
        string CreateScoreEntry(int timeStep)
        {
            string entry = string.Empty;
            for (int action = 0; action < _meanPayoffPerTimeStep.GetLength(1); action++)
            {
                double avgScore = _numPlayerPlayedActionPerTimeStep[timeStep, action] < double.Epsilon ? 0 : _meanPayoffPerTimeStep[timeStep, action]
                    / _numPlayerPlayedActionPerTimeStep[timeStep, action];
                entry += $"{avgScore:0.0000};";
            }
            return entry.Remove(entry.Length - 1);
        }

        // Top row for output file
        string CreateHeader(ActionSet actionSet)
        {
            string header = string.Empty;
            foreach (KeyValuePair<int, int> kvp in actionSet)
                header += $"action = {kvp.Key};";
            return header.Remove(header.Length - 1);
        }

        // Flush contents of log to file.
        public void FlushNumActionPlayedToFile(string subDirectory, string independentVariable)
        {
            string[] entries = new string[_numSteps + 1];
            entries[0] = CreateHeader(_actionSet);
            for (int i = 1; i <= _numSteps; i++)
                entries[i] += CreateNumActionEntry(i - 1);

            string directory = Path.Combine(_sessionHash, subDirectory);
            Directory.CreateDirectory(directory);

            string varVal = independentVariable == "summary" ? "_summary" : _paramMap[independentVariable];
            string fileName = $"{_parameters.ParameterBaselineType}_{independentVariable}{varVal}_NumActionPlayed.csv";

            string outputFile = Path.Combine(directory, fileName);
            File.WriteAllLines(outputFile, entries);
        }

        // A single row in the output file
        string CreateNumActionEntry(int timeStep)
        {
            string entry = string.Empty;
            for (int action = 0; action < _numPlayerPlayedActionPerTimeStep.GetLength(1); action++)
                entry += $"{_numPlayerPlayedActionPerTimeStep[timeStep, action]};";
            return entry.Remove(entry.Length - 1);
        }
    }
}
