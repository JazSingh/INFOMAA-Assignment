using System;
using MathNet.Numerics;
using System.Linq;
using System.Collections.Generic;

namespace INFOMAA_Assignment
{
    public class Player
    {
        private Position position;
        private ActionSet actionSet;
        private readonly Distribution distribution;

        public Player(ActionSet actionSet, Distribution distribution)
        {
            this.actionSet = actionSet.CleanCopy();
            this.distribution = distribution;
        }

        public ActionSet ActionSet { get { return actionSet; } }

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

        public int GetAction(List<int> tabooList)
        {
            if (tabooList.Count() == 0)
            {
                // Exploit
                if (distribution.Sample() == ActionType.EXPLOIT)
                    return actionSet.GetBestAction().Key;
                // Explore
                Random randomService = distribution.GetRandomService();
                int action = randomService.Next(0, actionSet.Count);
                return actionSet.Keys.ToArray()[action];
            }
            foreach (KeyValuePair<int, int> kvp in actionSet)
            {
                if (!tabooList.Contains(kvp.Key))
                    return kvp.Key;
            }
            return -1; // No suitable action found
        }
    }
}
