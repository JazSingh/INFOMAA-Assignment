using System;
namespace INFOMAA_Assignment
{
    public class Player
    {
        private Position position;
        private int direction;
        private int reward;

        public Player(Position position, int direction)
        {
            this.position = position;
            this.direction = direction;
        }

        public void SetPosition(Position newPosition)
        {
            position = newPosition;
        }

        public void SetDirection(int newDirection)
        {
            direction = newDirection;
        }

        public Position GetPosition()
        {
            return position;
        }

        public int GetDirection()
        {
            return direction;
        }

        public void AddReward(int reward)
        {
            this.reward += reward;
        }

        public int GetReward()
        {
            return reward;
        }
    }
}
