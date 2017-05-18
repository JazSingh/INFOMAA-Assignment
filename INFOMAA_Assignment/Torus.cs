using System;
namespace INFOMAA_Assignment
{
    /// <summary>
    /// Torus. 
    /// Class that represents the torus.
    /// Torus is created from given rectangle with height and width.
    /// </summary>
    public class Torus
    {
        readonly int width;
        readonly int height;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:INFOMAA_Assignment.Torus"/> class.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public Torus(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        /// <summary>
        /// Get next position, give the current position, speed and angle of movement.
        /// </summary>
        /// <returns>The new position.</returns>
        /// <param name="currentPosition">Current position.</param>
        /// <param name="speed">Speed.</param>
        /// <param name="angle">Angle.</param>
        public Position NextPosition(Position currentPosition, int speed, int angle)
        {
            // Convert degree to radians
            double radians = angle * (Math.PI / 180);
            // Calculate movement in each direction
            int dx = (int)(speed * Math.Cos(radians));
            int dy = (int)(speed * Math.Sin(radians));
            // Calculate next position, if necessary, wrap
            int nextX = (currentPosition.X + dx) % width;
            if (nextX < 0) nextX = width + nextX;
            int nextY = (currentPosition.Y + dy) % height;
            if (nextY < 0) nextY = height + nextY;
            return new Position(nextX, nextY);
        }
    }
}
