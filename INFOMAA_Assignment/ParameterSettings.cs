using System;
using System.Collections.Generic;

namespace INFOMAA_Assignment
{
    public struct ParameterSettings
    {
		public int Width;
		public int Height;

        public int NumPlayers;
        public int NumActions;

        public int PositiveReward;
        public int NegativeReward;

        public int CollisionRadius;
        public int Speed;
        public double Epsilon;

        public string ParameterBaselineType;

        public Dictionary<string, string> GetMap()
        {
			Dictionary<string, string> parameters = new Dictionary<string, string>
			{
				{ ParamNameConstants.WIDTH, Width.ToString() },
				{ ParamNameConstants.HEIGHT, Height.ToString() },
				{ ParamNameConstants.NUMPLAYERS, NumPlayers.ToString() },
				{ ParamNameConstants.NUMACTIONS, NumActions.ToString() },
				{ ParamNameConstants.COLLISIONRADIUS, CollisionRadius.ToString() },
				{ ParamNameConstants.SPEED, Speed.ToString() },
				{ ParamNameConstants.POSREWARD, PositiveReward.ToString() },
				{ ParamNameConstants.NEGREWARD, NegativeReward.ToString() },
				{ ParamNameConstants.EPSILON, $"{Epsilon:0.000}" }
			};
            return parameters;
        }

    }
}
