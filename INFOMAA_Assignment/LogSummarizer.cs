using System;
using System.Collections.Generic;
using System.IO;

namespace INFOMAA_Assignment
{
    // Creates useful summaries from a series of logs
    public class LogSummarizer
    {
        readonly string _summaryName;
        readonly string _independentVariable;
        readonly string _sessionHash;

        List<Logger> _logs;

        public LogSummarizer(string independentVariable, string sessionHash)
        {
			_summaryName = $"{independentVariable}";
            _sessionHash = sessionHash;
            _independentVariable = independentVariable;
            _logs = new List<Logger>();
        }

        public void AddLog(Logger log) { _logs.Add(log); }

        public void SetLogs(List<Logger> logs) { _logs = logs; }

		public void FlushAll()
		{
			FlushCollissionSummary();
            FlushScoreSummary();
            FlushNumActionPlayerSummary();
		}

		public void FlushCollissionSummary()
        {
            Console.WriteLine("Flushing collisions...");
            int gameLength = _logs[0].Collisions.Length + 1;

            string[] summary = new string[gameLength];
            summary[0] = CreateCollisionColumnNames();
            for (int i = 1; i < gameLength; i++)
                summary[i] = CreateCollisionEntry(i - 1);

            string directory = Path.Combine(_sessionHash, _independentVariable);
			Directory.CreateDirectory(directory);
            File.WriteAllLines(Path.Combine(directory, $"collisions_summary.csv"), summary);
        }

        string CreateCollisionColumnNames()
        {
            string header = string.Empty;
            foreach (Logger logger in _logs)
                header += $"{_independentVariable} = {logger.GetParameterValue(_independentVariable)};";
            return header.Remove(header.Length - 1);
        }

        string CreateCollisionEntry(int timeStep)
        {
            string entry = string.Empty;
            foreach (Logger logger in _logs)
                entry += $"{logger.Collisions[timeStep]:0.00000};";
            return $"{entry.Remove(entry.Length - 1)}";
        }

        public void FlushScoreSummary()
        {
            Console.WriteLine("Flushing scores...");
            // Write the individual averaged logs
            foreach (Logger log in _logs)
                log.FlushScoresToFile(_independentVariable, _independentVariable);
            // Write the total average if number of actions is not the independent variable
            if (_independentVariable == ParamNameConstants.NUMACTIONS) return;
            LogSquasher squasher = new LogSquasher(_logs, _logs[0].ParameterSettings, _logs[0].MeanScores.GetLength(0), _logs[0].ActionSet);
            Logger squashed = squasher.Squash();
            squashed.FlushScoresToFile(_independentVariable, "summary");
        }

		public void FlushNumActionPlayerSummary()
		{
            Console.WriteLine("Flushing number of times action played per timestep");
			// Write the individual averaged logs
			foreach (Logger log in _logs)
                log.FlushNumActionPlayedToFile(_independentVariable, _independentVariable);
			// Write the total average if number of actions is not the independent variable
			if (_independentVariable == ParamNameConstants.NUMACTIONS) return;
			LogSquasher squasher = new LogSquasher(_logs, _logs[0].ParameterSettings, _logs[0].MeanScores.GetLength(0), _logs[0].ActionSet);
			Logger squashed = squasher.Squash();
			squashed.FlushNumActionPlayedToFile(_independentVariable, "summary");
		}
    }
}
