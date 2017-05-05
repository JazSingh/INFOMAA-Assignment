using System;
using System.Collections.Generic;
namespace INFOMAA_Assignment
{
    public class DirectionRotator
    {
        int deltaAngle; // k
        int numAngles; // n

        Dictionary<int, int> angleMap;

        public DirectionRotator(int deltaAngle)
        {
            this.deltaAngle = deltaAngle;
            this.numAngles = 360 / this.deltaAngle;

            angleMap = new Dictionary<int, int>(numAngles);

            int i = 0;

            while (i < numAngles)
            {
                angleMap.Add(i * deltaAngle, (i + 1 % numAngles) * deltaAngle);
                i++;
            }
        }

        public int NextAngle(int currentAngle)
        {
            return angleMap[currentAngle];
        }
    }
}
