using System;
using System.Collections.Generic;
using System.Linq;

namespace INFOMAA_Assignment
{
    public class Game
    {
        Logger _logger;
        readonly Torus _torus;
        ActionSet _actionSet;
        Player[] _players;

        readonly int _numPlayers;
        readonly int _colissionRadius;
        readonly int _positiveReward;
        readonly int _negativeReward;
        readonly int _speed;

        int _clock;
        int _gameLength;

        public Game(Torus torus, int numPlayers, ActionSet actionSet, int colissionRadius, int positiveReward, int negativeReward, int speed, Distribution distribution, int gameLength)
        {
            // Init parameters
            _clock = 0;
            _gameLength = gameLength;

            _torus = torus;
            _actionSet = actionSet;

            _colissionRadius = colissionRadius;
            _positiveReward = positiveReward;
            _negativeReward = negativeReward;
            _numPlayers = numPlayers;
            _speed = speed;

            // Create players
            _players = new Player[numPlayers];
            for (int i = 0; i < numPlayers; i++)
            {
                _players[i] = new Player(actionSet, distribution);
                Random randomService = distribution.GetRandomService();
                int x = randomService.Next(0, torus.Width);
                int y = randomService.Next(0, torus.Height);
                _players[i].SetPosition(new Position(x, y));
            }

            // Setup logger with parameters
            _logger = new Logger(gameLength, actionSet.CleanCopy(), numPlayers, new[]{
                torus.Width.ToString(),
                torus.Height.ToString(),
                numPlayers.ToString(),
                actionSet.NumActions.ToString(),
                colissionRadius.ToString(),
                speed.ToString(),
                positiveReward.ToString(),
                negativeReward.ToString(),
                $"{distribution.Epsilon:0.000}"
            });
        }

        /// <summary>
        /// Start the game.
        /// </summary>
        public void Start()
        {
            Console.WriteLine("{0}-", _logger._parameters);
            while (_clock < _gameLength)
            {
                Console.Write($"\rGametime: {_clock + 1} of {_gameLength} seconds");
                Step();
            }
            _logger.Dump();
        }

        /// <summary>
        /// Perform a single step in the game.
        /// </summary>
        public void Step()
        {
            Dictionary<int, List<int>> rewardsPerAction = new Dictionary<int, List<int>>();

            // init list of actions to log
            foreach (var action in _actionSet.Keys)
            {
                rewardsPerAction.Add(action, new List<int>());
            }

            for (int i = 0; i < _numPlayers; i++)
            {
                int action = _players[i].GetAction();

                Position next = _torus.NextPosition(_players[i].GetPosition(), _speed, action);
                bool colission = false;

                // Check for colissions
                for (int j = 0; j < _numPlayers; j++)
                {
                    colission |= (i != j && IsCollision(next, _players[j]));
                }

                if (!colission)
                { // if there is NO colission, add the positive reward and move the player
                    _players[i].SetPosition(next);
                    _players[i].AddReward(action, _positiveReward);
                }
                else
                { // if there is a colission, add a negative reward to the action
                    _logger.LogCollision(_clock);
                    _players[i].AddReward(action, _negativeReward);
                }

                // Add score to list
                rewardsPerAction[action].Add(!colission ? _positiveReward : _negativeReward);
            }

            // Log means of scores per action
            _logger.LogActionMeans(_clock, rewardsPerAction);
            _clock++;
        }

        /// <summary>
        /// Checks for a colission based on the position and the position of the other player
        /// </summary>
        /// <returns><c>true</c>, if collision a would occur, <c>false</c> otherwise.</returns>
        /// <param name="pos">Position.</param>
        /// <param name="player">Player.</param>
        private bool IsCollision(Position pos, Player player)
        {
            return Math.Sqrt((player.GetPosition().X - pos.X) * (player.GetPosition().X - pos.X) + (player.GetPosition().Y - pos.Y) * (player.GetPosition().Y - pos.Y)) < _colissionRadius;
        }
    }
}
