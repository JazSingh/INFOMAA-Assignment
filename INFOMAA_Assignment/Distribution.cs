﻿using System;
using MathNet.Numerics.Distributions;
namespace INFOMAA_Assignment
{
    public class Distribution
    {
        private ContinuousUniform _distribution;
        private double _epsilon;

        public Distribution(double epsilon, Random randomService)
        {
            _distribution = new ContinuousUniform(0, 1, randomService);
            _epsilon = epsilon;
        }

        public Distribution Clone()
        {
            return new Distribution(_epsilon, _distribution.RandomSource);
        }

        public ActionType Sample()
        {
            double sample = _distribution.Sample();
            if (sample < _epsilon)
            {
                return ActionType.EXPLOIT;
            }
            return ActionType.EXPLORE;
        }

        public Random GetRandomService()
        {
            return _distribution.RandomSource;
        }
    }
}
