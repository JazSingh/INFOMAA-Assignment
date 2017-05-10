using System;
using MathNet.Numerics.Distributions;
namespace INFOMAA_Assignment
{
    public class Distribution
    {
        private ContinuousUniform _distribution;
        private double _epsilon;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:INFOMAA_Assignment.Distribution"/> class.
        /// </summary>
        /// <param name="epsilon">Epsilon.</param>
        /// <param name="randomService">Random service.</param>
        public Distribution(double epsilon, Random randomService)
        {
            // These bounds are inclusive
            _distribution = new ContinuousUniform(0, 100, randomService);
            _epsilon = epsilon;
        }

        public double Epsilon { get { return _epsilon; } }

        // Get a fresh clone of the distribution
        public Distribution Clone()
        {
            return new Distribution(_epsilon, _distribution.RandomSource);
        }

        // Sample and return the action to perform.
        public ActionType Sample()
        {
            double sample = _distribution.Sample();
            if (sample < _epsilon * 100)
            {
                return ActionType.EXPLORE;
            }
            return ActionType.EXPLOIT;
        }

        public Random GetRandomService()
        {
            return _distribution.RandomSource;
        }
    }
}
