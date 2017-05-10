using System;
using System.Linq;
using System.Collections.Generic;

namespace INFOMAA_Assignment
{
    /// <summary>
    /// Player.
    /// </summary>
    public class Player
    {
        private Position _position;
        private readonly Distribution _distribution;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:INFOMAA_Assignment.Player"/> class.
        /// Creates a player with given set of actions and probability distribution.
        /// </summary>
        /// <param name="actionSet">Action set.</param>
        /// <param name="distribution">Distribution.</param>
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

        /// <summary>
        /// Adds the reward to the set of actions
        /// </summary>
        /// <param name="direction">Actions/direction.</param>
        /// <param name="reward">Reward.</param>
        public void AddReward(int direction, int reward)
        {
            ActionSet[direction] += reward;
        }

        public int GetReward(int direction)
        {
            return ActionSet[direction];
        }

        /// <summary>
        /// Gets the action based on the porbability distribution
        /// </summary>
        /// <returns>The action to perform.</returns>
        public int GetAction()
        {
            // Exploit
            if (_distribution.Sample() == ActionType.EXPLOIT)
                return ActionSet.GetBestAction();
            // Explore
            Random randomService = _distribution.GetRandomService();
            int action = randomService.Next(0, ActionSet.Count);
            return ActionSet.Keys.ToArray()[action];
        }
    }
}
