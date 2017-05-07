using System;
using MathNet.Numerics.Distributions;
namespace INFOMAA_Assignment
{
    public class Distribution
    {
        private ContinuousUniform distribution;
        private double epsilon;

        public Distribution(double epsilon, Random randomService)
        {
            this.distribution = new ContinuousUniform(0, 1, randomService);
            this.epsilon = epsilon;
        }

        public Distribution Clone()
        {
            return new Distribution(epsilon, distribution.RandomSource);
        }

        public ActionType Sample()
        {
            double sample = distribution.Sample();
            if (sample < epsilon)
            {
                return ActionType.EXPLOIT;
            }
            return ActionType.EXPLORE;
        }

        public Random GetRandomService()
        {
            return distribution.RandomSource;
        }
    }
}
