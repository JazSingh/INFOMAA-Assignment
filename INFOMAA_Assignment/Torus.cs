using System;
namespace INFOMAA_Assignment
{
    public class Torus
    {
        private int width;
        private int height;

        public Torus(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public Position NextPosition(Position currentPosition, int speed, int angle)
        {
            //double radians = angle * (Math.PI / 180);
            int dx = (int)(speed * Math.Cos(angle));
            int dy = (int)(speed * Math.Sin(angle));
            int nextX = (currentPosition.X + dx) % width;
            int nextY = (currentPosition.Y + dy) % height;
            return new Position(nextX, nextY);
        }
    }
}
