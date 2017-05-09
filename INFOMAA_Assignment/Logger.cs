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
    int _numPlayers;

    private ActionSet _actionSet;

    string _hashcode;
    public string _parameters;

    public Logger(int timeSteps, ActionSet actionSet, int numPlayers, string[] parameters)
    {
      _numSteps = timeSteps;
      _numPlayers = numPlayers;
      _actionSet = actionSet;
      _colissionsPerTimeStep = new int[timeSteps];

      foreach(int degrees in _actionSet.Keys)
      {
        _meanPerTimeStep.Add(degrees, new List<double>());
      }

      _parameters = "";
      foreach(string parameter in parameters)
      {
        _parameters += $"-{parameter}";
      }

      _hashcode = DateTime.Now.GetHashCode().ToString("x8");
    }

    public void LogCollision(int timeStep)
    {
      _colissionsPerTimeStep[timeStep]++;
    }

    public void LogActionMeans(int timeStep, Dictionary<int, List<int>> meanRewardPerAction)
    {
      foreach(int action in meanRewardPerAction.Keys)
      {
        double sum = 0;
        foreach(int score in meanRewardPerAction[action])
        {
          sum += score;
        }

        double mean = 0;

        if(meanRewardPerAction[action].Count > 0)
        {
          mean = sum / meanRewardPerAction[action].Count;
        }
        _meanPerTimeStep[action].Add(mean);
      }
    }

    public void Dump()
    {
      Console.WriteLine("\nDumping scores per action per time step");
      DumpScoresPerActionPerTimeStep();
      Console.WriteLine("Dumping collisions");
      DumpColissions();
    }

    private void DumpScoresPerActionPerTimeStep()
    {
      string[] entries = new string[_numSteps + 1];
      entries[0] = CreateHeader(_actionSet);

      for(int i = 1; i <= _numSteps; i++)
      {
        List<double> meansPerAction = new List<double>();
        foreach(KeyValuePair<int, List<double>> kvp in _meanPerTimeStep)
        {
          meansPerAction.Add(kvp.Value[i - 1]);
        }
        entries[i] += CreateEntry(i, meansPerAction);
      }
      File.WriteAllLines(_hashcode + _parameters + "_scores.csv", entries);
    }

    private string CreateEntry(int timeStep, List<double> meansPerAction)
    {
      string entry = $"{timeStep}";
      foreach(double mean in meansPerAction)
      {
        entry += $";{mean:0.000}";
      }
      return entry;
    }

    private string CreateHeader(ActionSet actionSet)
    {
      string header = "time";
      foreach(KeyValuePair<int, int> kvp in actionSet)
      {
        header += $";action {kvp.Key} degrees";
      }
      return header;
    }


    private void DumpColissions()
    {
      string[] entries = new string[_numSteps + 1];
      entries[0] = "step;numColissions\n";
      for(int i = 1; i < _numSteps + 1; i++)
      {
        entries[i] += $"{i};{_colissionsPerTimeStep[i - 1]}";
      }
      File.WriteAllLines(_hashcode + _parameters + "_collisions.csv", entries);
    }
  }
}
