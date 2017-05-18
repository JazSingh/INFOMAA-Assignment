using System;
namespace INFOMAA_Assignment
{
    public static class ParameterBaseline
    {
        public static ParameterSettings Default()
        {
            ParameterSettings settings = new ParameterSettings
            {
                Width = 250,
                Height = 250,

                NumPlayers = 50,
                NumActions = 6,

                PositiveReward = 1,
                NegativeReward = -1,

                Epsilon = 0.01,
                Speed = 3,
                CollisionRadius = 5,

                ParameterBaselineType = ParameterBaselineType.DEFAULT
            };
            return settings;
        }

        public static ParameterSettings Default(string independentVariable, int variableValue)
        {
            ParameterSettings settings = Default();
            switch (independentVariable)
            {
                case ParamNameConstants.NUMPLAYERS: settings.NumPlayers = variableValue; break;
                case ParamNameConstants.NUMACTIONS: settings.NumActions = variableValue; break;
                case ParamNameConstants.HEIGHT: settings.Height = variableValue; break;
                case ParamNameConstants.WIDTH: settings.Width = variableValue; break;
                case ParamNameConstants.POSREWARD: settings.PositiveReward = variableValue; break;
                case ParamNameConstants.NEGREWARD: settings.NegativeReward = variableValue; break;
                case ParamNameConstants.EPSILON: settings.Epsilon = 1 / ((double)variableValue); break;
                case ParamNameConstants.SPEED: settings.Speed = variableValue; break;
                case ParamNameConstants.COLLISIONRADIUS: settings.CollisionRadius = variableValue; break;
                default: throw new Exception($"Variable {independentVariable} not present");
            }

            return settings;
        }
    }
}
