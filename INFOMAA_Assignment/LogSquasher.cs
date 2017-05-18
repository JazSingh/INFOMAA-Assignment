using System;
using System.Collections.Generic;

namespace INFOMAA_Assignment
{
    public class LogSquasher
    {
        readonly List<Logger> _logs;
        ParameterSettings _parameters;
        readonly int _timesteps;
        readonly ActionSet _actionSet;

        public LogSquasher(List<Logger> logs, ParameterSettings parameters, int timesteps, ActionSet actionSet)
        {
            _logs = logs;
            _parameters = parameters;
            _timesteps = timesteps;
            _actionSet = actionSet;
        }

        public Logger Squash()
        {
            double divisor = _logs.Count;
            Logger squashed = new Logger(_timesteps, _actionSet, _parameters, _logs[0].SessionHash)
            {
                Collisions = SquashCollisions(divisor),
                MeanScores = SquashScores(divisor),
                NumActionPlayed = SquashNumActionPlayed(divisor)
            };
            return squashed;
        }

        double[] SquashCollisions(double divisor)
        {
            double[] squashedCollisions = new double[_timesteps];
            foreach (Logger log in _logs)
                for (int i = 0; i < _timesteps; i++)
                    squashedCollisions[i] += log.Collisions[i] / divisor;

            return squashedCollisions;
        }

        double[,] SquashScores(double divisor)
        {
            double[,] squashedScores = new double[_timesteps, _actionSet.Keys.Count];
            foreach (Logger log in _logs)
                for (int i = 0; i < _timesteps; i++)
                    for (int j = 0; j < _actionSet.Keys.Count; j++)
                        squashedScores[i, j] += log.MeanScores[i, j] / divisor;

            return squashedScores;
        }

		double[,] SquashNumActionPlayed(double divisor)
		{
			double[,] squashedScores = new double[_timesteps, _actionSet.Keys.Count];
			foreach (Logger log in _logs)
				for (int i = 0; i < _timesteps; i++)
					for (int j = 0; j < _actionSet.Keys.Count; j++)
                        squashedScores[i, j] += log.NumActionPlayed[i, j] / divisor;

			return squashedScores;
		}
    }
}
