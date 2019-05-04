using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ParkingLot.Tests
{
    [TestClass]
    public class UnitTest1
    {
        FeesCalculator calc = null;
        [TestInitialize()]
        public void Initialize()
        {
            calc = new FeesCalculator();
            calc.Initialize();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            calc = null;
        }


        [TestMethod]
        public void PositiveTest()
        {
            int actualResult = calc.GetParkingFee("09:01", "10:00");
            int excpectedResult = 5;
            Assert.AreEqual(excpectedResult, actualResult);
        }

        [TestMethod]
        public void PositiveTest_SameTime()
        {
            int actualResult = calc.GetParkingFee("09:01", "09:01");
            int excpectedResult = 2;
            Assert.AreEqual(excpectedResult, actualResult);
        }

        [TestMethod]
        public void NegativeTest()
        {
            int actualResult = calc.GetParkingFee("10:30", "12:31");
            int excpectedResult = 14;
            Assert.AreNotEqual(excpectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InputArgumentNullTest()
        {
            int actualResult = calc.GetParkingFee(null, null);
        }
    }
}
