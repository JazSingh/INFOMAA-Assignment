using System;
using System.Linq;
using System.Collections.Generic;

namespace INFOMAA_Assignment
{
    public class Player
    {
        private Position _position;
        private readonly Distribution _distribution;

        public Player(ActionSet actionSet, Distribution distribution)
        {
            ActionSet = actionSet.CleanCopy();
            _distribution = distribution;
        }

        public ActionSet ActionSet { get; }

        public void SetPosition(Position newPosition)
        {
            _position = newPosition;
        }

        public Position GetPosition()
        {
            return _position;
        }

        public void AddReward(int direction, int reward)
        {
            ActionSet[direction] += reward;
        }

        public int GetReward(int direction)
        {
            return ActionSet[direction];
        }

        public int GetAction(List<int> tabooList)
        {
            if (!tabooList.Any())
            {
                // Exploit
                if (_distribution.Sample() == ActionType.EXPLOIT)
                    return ActionSet.GetBestAction().Key;
                // Explore
                Random randomService = _distribution.GetRandomService();
                int action = randomService.Next(0, ActionSet.Count);
                return ActionSet.Keys.ToArray()[action];
            }
            foreach (KeyValuePair<int, int> kvp in ActionSet)
            {
                if (!tabooList.Contains(kvp.Key))
                    return kvp.Key;
            }
            return -1; // No suitable action found
        }
    }
}
