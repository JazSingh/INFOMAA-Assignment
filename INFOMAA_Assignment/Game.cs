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

        //w, h, N, k, δ, r, R1, and R2
        public Game(Torus torus, int numPlayers, ActionSet actionSet, int colissionRadius, int positiveReward, int negativeReward, Distribution distribution)
        {
            this.torus = torus;
            this.actionSet = actionSet;

            this.colissionRadius = colissionRadius;
            this.positiveReward = positiveReward;
            this.negativeReward = negativeReward;

            players = new Player[numPlayers];
            for (int i = 0; i < numPlayers; i++)
            {
                players[i] = new Player(actionSet, distribution);
                Random randomService = distribution.GetRandomService();
                int x = randomService.Next(0, torus.Width);
                int y = randomService.Next(0, torus.Height);
                players[i].SetPosition(new Position(x, y));
                int action = randomService.Next(0, actionSet.Count);
                players[i].SetDirection(actionSet.Values.ToArray()[action]);
            }
        }

        //TODO
        public void Start()
        {

        }
    }
}
