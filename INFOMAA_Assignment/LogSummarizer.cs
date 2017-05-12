using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MathNet.Numerics.Distributions;

namespace INFOMAA_Assignment
{
    // Creates useful summaries from a series of logs
    public class LogSummarizer
    {
        readonly string _summaryName;
        readonly string _testedParamName;
        readonly string _testedParamValue;
        readonly string _variatedParamName;

        List<Logger> _logs;

        public LogSummarizer(string testedParamName, string testedParamValue, string variatedParam)
        {
            _testedParamName = testedParamName;
            _testedParamValue = testedParamValue;
            _summaryName = $"{_testedParamName}-{_testedParamValue}";
            _variatedParamName = variatedParam;
            _logs = new List<Logger>();
        }

        public void AddLog(Logger log) { _logs.Add(log); }

        public void SetLogs(List<Logger> logs) { _logs = logs; }

        public void DumpCollissionSummary()
        {
            Console.WriteLine("Creating and dumping colission summary");
            int gameLength = _logs[0].Collisions.Length;

            var summary = new List<string>();
            summary.Add(CreateParameterheader());
            summary.Add(CreateCollisionColumnNames());
            for (int i = 0; i < gameLength; i++)
            {
                summary.Add(CreateCollisionEntry(i));
            }
            File.WriteAllLines($"{DateTime.Now.ToShortDateString().Replace('/', '-')}-{DateTime.Now.TimeOfDay.ToString("g").Replace(':', '-')}-{_summaryName}-collisions.csv", summary);
        }

        public void DumpScoreSummary()
        {
            Console.WriteLine("Creating and dumping score summary");
            int gameLength = _logs[0].Collisions.Length;

            var summary = new List<string>();
            summary.Add(CreateParameterheader());
            summary.Add(CreateScoreColumnNames());
            summary.Add(CreateActionColumnNames());
            for (int timeStep = 0; timeStep < gameLength; timeStep++)
            {
                summary.Add(CreateScoreEntry(timeStep));
            }
            File.WriteAllLines($"{DateTime.Now.ToShortDateString().Replace('/', '-')}-{DateTime.Now.TimeOfDay.ToString("g").Replace(':', '-')}-{_summaryName}-scores.csv", summary);
        }

        private string CreateParameterheader()
        {
            return $"Tested Parameter:{_testedParamName} = {_testedParamValue}, variated Parameter: {_variatedParamName}";
        }

        private string CreateCollisionColumnNames()
        {
            string header = "time;";
            foreach (Logger logger in _logs)
            {
                header += $"{_variatedParamName} = {logger.GetParameterValue(_variatedParamName)};";
            }
            return header;
        }

        private string CreateScoreColumnNames()
        {
            string header = ";";
            foreach (Logger logger in _logs)
            {
                var numOfActions = logger.GetActions().Count;
                header += $"{_variatedParamName} = {logger.GetParameterValue(_variatedParamName)};";
                header += new string(';', numOfActions);
            }
            return header;
        }

        private string CreateActionColumnNames()
        {
            string header = "time;";
            foreach (Logger logger in _logs)
            {
                var actions = logger.GetActions();
                foreach (var action in actions)
                {
                    header += $"{action};";
                }
                header += ";";
            }
            return header;
        }

        private string CreateCollisionEntry(int timeStep)
        {
            string entry = $"{timeStep + 1};";
            foreach (Logger logger in _logs)
            {
                entry += $"{logger.Collisions[timeStep]};";
            }
            return $"{entry.Remove(entry.Length - 1)}";
        }

        private string CreateScoreEntry(int timeStep)
        {
            string entry = $"{timeStep + 1};";
            foreach (Logger logger in _logs)
            {
                var actions = logger.GetActions();
                foreach (var action in actions)
                {
                    var means = logger.GetMeansOfAction(action);
                    var meanOfThisTimeStep = means[timeStep];
                    entry += $"{meanOfThisTimeStep};";

                }
                entry += ";";
            }
            return $"{entry.Remove(entry.Length - 1)}";
        }
    }
}
