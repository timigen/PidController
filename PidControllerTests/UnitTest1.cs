using NUnit.Framework;
using Pid;
using System;
using System.Collections.Generic;

namespace PidControllerTests
{

    [TestFixture]
    public class JuryRig
    {
        [Test]
        public void TestMethod1()
        {
            double measuredValue = 0;
            double targetValue = 107;
            uint count = 0;

            var c = GetController(targetValue);
            
            while (targetValue != measuredValue)
            {
                var correction = c.GetCorrection(measuredValue, 1000);
                measuredValue += correction;
                count++;
            }
            Assert.True(true);
        }

        private IController GetController(double targetValue)
        {
            var config = new Config(targetValue, -10, 10);

            return new Controller(config);
        }

    }
}
