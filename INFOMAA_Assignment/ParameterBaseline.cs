using System;
namespace INFOMAA_Assignment
{
    public static class ParameterBaseline
    {
        /// <summary>
        /// This set of parameters are used as a baseline for our simulations.
        /// </summary>
        /// <returns></returns>
        public static ParameterSettings Default()
        {
            ParameterSettings settings = new ParameterSettings
            {
                Width = 250,
                Height = 250,

                NumPlayers = 100,
                NumActions = 6,

                PositiveReward = 1,
                NegativeReward = -10,

                Epsilon = 0.01,
                Speed = 3,
                CollisionRadius = 5,

                ParameterBaselineType = ParameterBaselineType.DEFAULT
            };
            return settings;
        }

        /// <summary>
        /// Sets the value of the parameter which is changed in this experiment.
        /// </summary>
        /// <param name="independentVariable"></param>
        /// <param name="variableValue"></param>
        /// <returns></returns>
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
                case ParamNameConstants.EPSILON: settings.Epsilon = variableValue == 0 ? 0 : 1 / ((double)variableValue); break;
                case ParamNameConstants.SPEED: settings.Speed = variableValue; break;
                case ParamNameConstants.COLLISIONRADIUS: settings.CollisionRadius = variableValue; break;
                default: throw new Exception($"Variable {independentVariable} not present");
            }

            return settings;
        }
    }
}
