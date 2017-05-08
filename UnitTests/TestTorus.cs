using NUnit.Framework;
using System;
using INFOMAA_Assignment;

namespace UnitTests
{
    [TestFixture()]
    public class TestTorus
    {
        [Test()]
        public void TestTorusMovementHorizontalRight()
        {
            Torus torus = new Torus(100, 100);
            Position pos = new Position(50, 50);
            Position nextPos = torus.NextPosition(pos, 10, 0);
            Assert.AreEqual(pos.X + 10, nextPos.X);
            Assert.AreEqual(pos.Y, nextPos.Y);
        }

        [Test()]
        public void TestTorusMovementHorizontalLeft()
        {
            Torus torus = new Torus(100, 100);
            Position pos = new Position(50, 50);
            Position nextPos = torus.NextPosition(pos, 10, 180);
            Assert.AreEqual(pos.X - 10, nextPos.X);
            Assert.AreEqual(pos.Y, nextPos.Y);
        }

        [Test()]
        public void TestTorusMovementVerticalUp()
        {
            Torus torus = new Torus(100, 100);
            Position pos = new Position(50, 50);
            Position nextPos = torus.NextPosition(pos, 10, 90);
            Assert.AreEqual(pos.X, nextPos.X);
            Assert.AreEqual(pos.Y + 10, nextPos.Y);
        }

        [Test()]
        public void TestTorusMovementVerticalDown()
        {
            Torus torus = new Torus(100, 100);
            Position pos = new Position(50, 50);
            Position nextPos = torus.NextPosition(pos, 10, 270);
            Assert.AreEqual(pos.X, nextPos.X);
            Assert.AreEqual(pos.Y - 10, nextPos.Y);
        }

        [Test()]
        public void TestTorusMovementVerticalWrapRight()
        {
            Torus torus = new Torus(100, 100);
            Position pos = new Position(95, 50);
            Position nextPos = torus.NextPosition(pos, 10, 0);
            Assert.AreEqual(5, nextPos.X);
            Assert.AreEqual(pos.Y, nextPos.Y);
        }

        [Test()]
        public void TestTorusMovementVerticalWrapLeft()
        {
            Torus torus = new Torus(100, 100);
            Position pos = new Position(5, 50);
            Position nextPos = torus.NextPosition(pos, 10, 180);
            Assert.AreEqual(95, nextPos.X);
            Assert.AreEqual(pos.Y, nextPos.Y);
        }

        [Test()]
        public void TestTorusMovementVerticalWrapUp()
        {
            Torus torus = new Torus(100, 100);
            Position pos = new Position(50, 95);
            Position nextPos = torus.NextPosition(pos, 10, 90);
            Assert.AreEqual(pos.X, nextPos.X);
            Assert.AreEqual(5, nextPos.Y);
        }

        [Test()]
        public void TestTorusMovementVerticalWrapDown()
        {
            Torus torus = new Torus(100, 100);
            Position pos = new Position(50, 5);
            Position nextPos = torus.NextPosition(pos, 10, 270);
            Assert.AreEqual(pos.X, nextPos.X);
            Assert.AreEqual(95, nextPos.Y);
        }
    }
}