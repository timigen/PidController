using NUnit.Framework;
using Pid;

namespace PidControllerTests
{
    [TestFixture]
    public class OutputDirection
    {
        [Test]
        public void CurrentLtTargetCorrectionIsPositive()
        {
            double currentValue = 0;
            double targetValue = 100000;

            var c = GetController(targetValue);


            double correctionValue = c.GetCorrection(currentValue, 1000);
            Assert.Positive(correctionValue);
        }

        [Test]
        public void CurrentLtTargetCorrectionIsPositiveLoop()
        {
            double currentValue = 0;
            double targetValue = 100000;

            var c = GetController(targetValue);

            while (currentValue != targetValue)
            {
                double correctionValue = c.GetCorrection(currentValue, 1000);
                Assert.Positive(correctionValue);
                currentValue += correctionValue;
            }


        }

        [Test]
        public void CurrentEqualsTargetCorrectionIsZero()
        {
            double targetValue = 500000;
            double currentValue = 500000;

            var c = GetController(targetValue);
            double correctionValue = c.GetCorrection(currentValue, 1000);

            Assert.Zero(correctionValue);
        }

        [Test]
        public void CurrentGtTargetCorrectionIsNegative()
        {
            double targetValue = 5000;
            double currentValue = 100000;

            var c = GetController(targetValue);
            double correctionValue = c.GetCorrection(currentValue, 1000);

            Assert.Negative(correctionValue);
        }

        [Test]
        public void controlFunctionSatisfiedNoCorrection()
        {
            double targetValue = 1000;
            double[] values = new double[] { targetValue, targetValue };
            var c = GetController(targetValue);

            foreach (double mV in values)
            {
                var outp = c.GetCorrection(mV, 1000);

                Assert.Zero(outp);
            }
        }

        private IController GetController(double targetValue)
        {
            var config = new Config(targetValue, -1000, 1000);

            return new Controller(config);
        }
    }
}
