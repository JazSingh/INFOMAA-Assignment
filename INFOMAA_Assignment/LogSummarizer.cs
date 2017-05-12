using System;
using System.Collections.Generic;

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

        public void AddLog(Logger log)
        {
            _logs.Add(log);
        }

        public void CreateCollissionSummary()
        {

        }

        public void CreateScoreSummary()
        {

        }
    }
}
