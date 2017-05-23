using System;
using System.Collections.Generic;
using System.Linq;

namespace INFOMAA_Assignment
{
    public class Game
    {
        readonly Logger _logger;
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

        public Game(ParameterSettings settings, Random randomSource, int gameLength, string sessionHash)
        {
            _clock = 0;
            _gameLength = gameLength;

            _torus = new Torus(settings.Width, settings.Height);
            _actionSet = new ActionSet(settings.NumActions);
            Distribution distribution = new Distribution(settings.Epsilon, randomSource);

            _colissionRadius = settings.CollisionRadius;
            _positiveReward = settings.PositiveReward;
            _negativeReward = settings.NegativeReward;
            _numPlayers = settings.NumPlayers;
            _speed = settings.Speed;

            // Create players
            _players = new Player[_numPlayers];
            for (int i = 0; i < _numPlayers; i++)
            {
                _players[i] = new Player(_actionSet, distribution);
                Random randomService = distribution.GetRandomService();
                int x = randomService.Next(0, _torus.Width);
                int y = randomService.Next(0, _torus.Height);
                _players[i].SetPosition(new Position(x, y));
            }

            _logger = new Logger(gameLength, _actionSet.CleanCopy(), settings, sessionHash);
        }

        public Logger Logger { get { return _logger; } }

        /// <summary>
        /// Start the game.
        /// </summary>
        public void Start()
        {
            while (_clock < _gameLength)
            {
                Console.Write($"\rGametime: {_clock + 1} of {_gameLength} seconds");
                Step();
            }
        }

        /// <summary>
        /// Perform a single step in the game.
        /// </summary>
        public void Step()
        {
            for (int i = 0; i < _numPlayers; i++)
            {
                int action = _players[i].GetAction();

                Position next = _torus.NextPosition(_players[i].GetPosition(), _speed, action);
                bool colission = false;

                // Check for colissions
                int j = 0;
                while (!colission && j < _numPlayers)
                {
                    colission |= (i != j && IsCollision(next, _players[j]));
                    j++;
                }

                if (!colission)
                { // if there is NO colission, add the positive reward and move the player
                    _players[i].SetPosition(next);
                    _players[i].AddReward(action, _positiveReward);
                    _logger.LogAction(_clock, action, _positiveReward);
                }
                else
                { // if there is a colission, add a negative reward to the action
                    _players[i].AddReward(action, _negativeReward);
                    _logger.LogCollision(_clock);
                    _logger.LogAction(_clock, action, _negativeReward);
                }
            }
            _clock++;
        }

        /// <summary>
        /// Checks for a colission based on the position and the position of the other player
        /// </summary>
        /// <returns><c>true</c>, if collision a would occur, <c>false</c> otherwise.</returns>
        /// <param name="pos">Position.</param>
        /// <param name="player">Player.</param>
        bool IsCollision(Position pos, Player player)
        {
            return Math.Sqrt((player.GetPosition().X - pos.X) * (player.GetPosition().X - pos.X) + (player.GetPosition().Y - pos.Y) * (player.GetPosition().Y - pos.Y)) < _colissionRadius;
        }
    }
}
