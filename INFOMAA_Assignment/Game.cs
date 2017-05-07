using System;
using System.Linq;

namespace INFOMAA_Assignment
{
    public class Game
    {
        Torus torus;
        ActionSet actionSet;
        Player[] players;

        int numPlayers;
        int colissionRadius;
        int positiveReward;
        int negativeReward;

        long clock;

        //w, h, N, k, δ, r, R1, and R2
        public Game(Torus torus, int numPlayers, ActionSet actionSet, int colissionRadius, int positiveReward, int negativeReward, Distribution distribution)
        {
            clock = 0;

            this.torus = torus;
            this.actionSet = actionSet;

            this.colissionRadius = colissionRadius;
            this.positiveReward = positiveReward;
            this.negativeReward = negativeReward;
            this.numPlayers = numPlayers;

            players = new Player[numPlayers];
            for (int i = 0; i < numPlayers; i++)
            {
                players[i] = new Player(actionSet, distribution);
                Random randomService = distribution.GetRandomService();
                int x = randomService.Next(0, torus.Width);
                int y = randomService.Next(0, torus.Height);
                players[i].SetPosition(new Position(x, y));
            }
        }

        public void Start()
        {
            while (true)
            {
                for (int i = 0; i < numPlayers; i++)
                {
                    //TODO
                }
                clock++;
            }
        }

        private bool IsColission(Player p1, Player p2)
        {
            throw new NotImplementedException();
        }
    }
}
