using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace INFOMAA_Assignment
{
    public class Game
    {
        Logger logger;
        readonly Torus torus;
        ActionSet actionSet;
        Player[] players;

        readonly int numPlayers;
        readonly int colissionRadius;
        readonly int positiveReward;
        readonly int negativeReward;
        readonly int speed;

        int clock;
        int gameLength;

        public Game(Torus torus, int numPlayers, ActionSet actionSet, int colissionRadius, int positiveReward, int negativeReward, int speed, Distribution distribution, int gameLength)
        {
            clock = 0;
            this.gameLength = gameLength;

            this.torus = torus;
            this.actionSet = actionSet;

            this.colissionRadius = colissionRadius;
            this.positiveReward = positiveReward;
            this.negativeReward = negativeReward;
            this.numPlayers = numPlayers;
            this.speed = speed;

            players = new Player[numPlayers];
            for (int i = 0; i < numPlayers; i++)
            {
                players[i] = new Player(actionSet, distribution);
                Random randomService = distribution.GetRandomService();
                int x = randomService.Next(0, torus.Width);
                int y = randomService.Next(0, torus.Height);
                players[i].SetPosition(new Position(x, y));
            }

            logger = new Logger(gameLength, numPlayers);
        }

        public void Start()
        {
            while (clock < gameLength)
            {
                Console.WriteLine(clock);
                for (int i = 0; i < numPlayers; i++)
                {
                    Console.WriteLine(players[i].GetPosition());
                    bool actionDone = false;
                    List<int> tabooList = new List<int>();
                    while (!actionDone)
                    {
                        int action = players[i].GetAction(tabooList);
                        if (action == -1)
                        {
                            throw new Exception("No suitable action found");
                        }
                        tabooList.Add(action);
                        Position next = torus.NextPosition(players[i].GetPosition(), speed, action);
                        bool colission = false;
                        for (int j = 0; j < numPlayers; j++)
                        {
                            colission |= (i != j && IsCollision(next, players[j]));
                        }
                        if (!colission)
                        {
                            actionDone = true;
                            logger.LogActionSet(clock, i, players[i].ActionSet);
                            players[i].SetPosition(next);
                            players[i].AddReward(action, positiveReward);
                        }
                        else
                        {
                            logger.LogCollision(clock);
                            players[i].AddReward(action, negativeReward);
                        }
                    }
                }
                clock++;
            }
            logger.Dump();
        }

        private bool IsCollision(Position pos, Player player)
        {
            return Math.Sqrt((player.GetPosition().X - pos.X) * (player.GetPosition().X - pos.X) + (player.GetPosition().Y - pos.Y) * (player.GetPosition().Y - pos.Y)) < colissionRadius;
        }
    }
}
