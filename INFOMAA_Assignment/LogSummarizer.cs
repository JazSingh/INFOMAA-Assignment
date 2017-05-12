﻿using System;
using System.Collections.Generic;
using System.IO;

namespace INFOMAA_Assignment
{
    // Creates useful summaries from a series of logs
    public class LogSummarizer
    {
        string _summaryName;
        List<Logger> _logs;

        public LogSummarizer(string name, string testedParam)
        {
            _summaryName = name;
            _logs = new List<Logger>();
        }

        public void AddLog(Logger log) { _logs.Add(log); }

        public void SetLogs(List<Logger> logs) { _logs = logs; }

        public void DumpCollissionSummary()
        {
            Console.WriteLine("Creating and dumping colission summary");
            int gameLength = _logs[0].Collisions.Length;
            string[] summary = new string[gameLength];
            for (int i = 0; i < gameLength; i++)
                summary[i] = CreateCollisionEntry(i);
            File.WriteAllLines($"{DateTime.Now.ToShortDateString().Replace('/', '-')}-{DateTime.Now.TimeOfDay.ToString("")}-{_summaryName}-.csv", summary);
        }

        private string CreateCollisionEntry(int i)
        {
            string entry = "";
            foreach (Logger logger in _logs)
                entry += $"{logger.Collisions[i]};";
            return $"{entry.Remove(entry.Length - 1)}\n";
        }

        public void CreateScoreSummary()
        {
            //TODO
        }
    }
}
