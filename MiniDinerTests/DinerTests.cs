using System;
using NUnit.Framework;

namespace MiniDinerTests
{
    using MiniDinerApp;

    [TestFixture]
    public class DinerTests
    {
         private OrderManager _orderManager;

         [SetUp]
         public void Initialize()
         {
             this._orderManager = new OrderManager("DinerXYZ");
         }

        [Test]
        public void TestMorning123()
        {
            var input = "morning, 1, 2, 3";
            var output = _orderManager.Process(input);

            Assert.AreEqual(output, "eggs, Toast, coffee");
        }

        [Test]
        public void TestMorning213()
        {
            var input = "morning, 2, 1, 3";
            var output = _orderManager.Process(input);

            Assert.AreEqual(output, "eggs, Toast, coffee");
        }

        [Test]
        public void TestMorning1234()
        {
            var input = "morning,  1, 2, 3, 4";
            var output = _orderManager.Process(input);

            Assert.AreEqual(output, "eggs, Toast, coffee, error");
        }

        [Test]
        public void TestMorning12333()
        {
            var input = "morning,  1, 2, 3, 3,3";
            var output = _orderManager.Process(input);

            Assert.AreEqual(output, "eggs, Toast, coffee(x3)");
        }

        [Test]
        public void TestNight1234()
        {
            var input = "night,  1, 2, 3, 4";
            var output = _orderManager.Process(input);

            Assert.AreEqual(output, "steak, potato, wine, cake");
        }

        [Test]
        public void TestNight1224()
        {
            var input = "night,  1, 2, 2, 4";
            var output = _orderManager.Process(input);

            Assert.AreEqual(output, "steak, potato(x2), cake");
        }


        [Test]
        public void TestNight1235()
        {
            var input = "night,  1, 2, 3, 5";
            var output = _orderManager.Process(input);

            Assert.AreEqual(output, "steak, potato, wine, error");
        }


        [Test]
        public void TestNight11235()
        {
            var input = "night,  1, 1, 2, 3, 5";
            var output = _orderManager.Process(input);

            Assert.AreEqual(output, "steak, error");
        }

        [Test]
        public void TestNightMixedCase1224()
        {
            var input = "nIgHt,  1, 2, 2, 4";
            var output = _orderManager.Process(input);

            Assert.AreEqual(output, "steak, potato(x2), cake");
        }

        [Test]
        public void TestNoInputException()
        {
            var input = "";
            bool caughtException = false;
            try
            {
                var output = _orderManager.Process(input);
            }
            catch (Exception)
            {
                caughtException = true;
            }

            Assert.AreEqual(caughtException, true);
        }


        [Test]
        public void TestInvalidInputException()
        {
            var input = "xyz";
            bool caughtException = false;
            try
            {
                var output = _orderManager.Process(input);
            }
            catch (Exception)
            {
                caughtException = true;
            }

            Assert.AreEqual(caughtException, true);
        }

        [Test]
        public void TestMorningInputException()
        {
            var input = "morning 1,2,4";
            bool caughtException = false;
            try
            {
                var output = _orderManager.Process(input);
            }
            catch (Exception)
            {
                caughtException = true;
            }

            Assert.AreEqual(caughtException, true);
        }
    }
}
