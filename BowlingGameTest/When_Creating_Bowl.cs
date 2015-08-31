using System;
using BowlingGameLib.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingGameLib;

namespace BowlingGameTest
{
    [TestClass]
    public class When_Creating_Bowl
    {
        [TestMethod]
        public void create_strike_bowl()
        {
            Bowl bowl = new Bowl(10);
            Assert.AreEqual(bowl.PinsDrop,10);
        }

        [TestMethod]
        [ExpectedException(typeof(GameExceptions))]
        public void should_exception_when_create_with_less_then_zero_point()
        {
            Bowl bowl = new Bowl(-1);
            Assert.Fail("Excepiton not thrown");
        }

        [TestMethod]
        [ExpectedException(typeof(GameExceptions))]
        public void should_exception_when_create_with_more_than_10_point()
        {
            Bowl bowl = new Bowl(11);
            Assert.Fail("Excepiton not thrown");
        }
    }
}
