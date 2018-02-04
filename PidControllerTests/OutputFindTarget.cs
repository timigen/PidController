using NUnit.Framework;
using Pid;
using System;

namespace PidControllerTests
{
    [TestFixture]
    public class OutputFindTarget
    {
        [Test]
        public void CurrentLtTargetCorrectionIsPositiveLoop()
        {
            double currentValue = 0;
            double targetValue = 10;

            var c = GetController(targetValue);

            while (currentValue != targetValue)
            {
                double correctionValue = c.GetCorrection(currentValue, 1000);
                currentValue += correctionValue;
            }
        }

        [Test]
        public void FindSuccessfully()
        {
            double measuredValue = 0;
            double targetValue = 100;
            int count = 0;

            var c = GetController(targetValue);

            while (targetValue != measuredValue)
            {
                var outp = c.GetCorrection(measuredValue, 1000);
                measuredValue += outp;
                count++;
            }
            Console.WriteLine("iterations: ", count);
            Assert.True(true);
        }

        [Test]
        public void AscendStopAtTarget()
        {
            double targetValue = 1000;
            double[] values = new double[] { 0, 10, 700, 900, targetValue };
            var c = GetController(targetValue);

            foreach (double mV in values)
            {
                var outp = c.GetCorrection(mV, 1000);

                if (mV == targetValue)
                {
                    Assert.Zero(outp);
                    break;
                }

                Assert.Positive(outp);
            }
        }

        [Test]
        public void DescendStopAtTarget()
        {
            double targetValue = 0;
            double[] values = new double[] { 1000, 900, 700, 250, targetValue };
            var c = GetController(targetValue);

            foreach (double mV in values)
            {
                var outp = c.GetCorrection(mV, 1000);

                if (mV == targetValue)
                {
                    Assert.Zero(outp);
                    break;
                }

                Assert.Negative(outp);
            }

        }

        private IController GetController(double targetValue)
        {
            var config = new Config(targetValue, -10, 10);

            return new Controller(config);
        }
    }
}
