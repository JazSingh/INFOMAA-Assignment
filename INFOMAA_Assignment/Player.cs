using System;
using MathNet.Numerics;
using System.Linq;

namespace INFOMAA_Assignment
{
    public class Player
    {
        private Position position;
        private int reward;
        private ActionSet actionSet;
        private readonly Distribution distribution;

        public Player(ActionSet actionSet, Distribution distribution)
        {
            this.actionSet = actionSet.CleanCopy();
            this.distribution = distribution;
        }

        public void SetPosition(Position newPosition)
        {
            position = newPosition;
        }

        public Position GetPosition()
        {
            return position;
        }

        public void AddReward(int direction, int reward)
        {
            actionSet[direction] += reward;
        }

        public int GetReward(int direction)
        {
            return actionSet[direction];
        }

        public int GetAction()
        {
            // Exploit
            if (distribution.Sample() == ActionType.EXPLOIT)
                return actionSet.GetBestAction().Key;
            // Explore
            Random randomService = distribution.GetRandomService();
            int action = randomService.Next(0, actionSet.Count);
            return actionSet.Values.ToArray()[action];
        }
    }
}
