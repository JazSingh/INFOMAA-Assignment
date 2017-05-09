using System;
using NUnit.Framework;
using INFOMAA_Assignment;
namespace UnitTests
{
    [TestFixture()]
    public class TestDistribution
    {
        [Test()]
        public void TestDistributionEpsilon()
        {
            int numExplore = 0;
            Distribution distribution = new Distribution(0.01, new Random(1));
            for (int i = 0; i < 100000; i++)
            {
                if (distribution.Sample() == ActionType.EXPLORE)
                    numExplore++;
            }
            Assert.GreaterOrEqual(numExplore, 100000 * 0.01 * 0.95);
            Assert.LessOrEqual(numExplore, 100000 * 0.01 * 1.05);
        }
    }
}
