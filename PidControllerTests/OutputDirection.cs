using NUnit.Framework;
using Pid;
using PidController;

namespace PidControllerTests
{
    [TestFixture]
    public class OutputDirection
    {
        [Test]
        public void CurrentLtTargetCorrectionIsPositive()
        {
            double targetValue = 500000;
            double currentValue = 0;

            var c = GetController(targetValue);
            double correctionValue = c.GetCorrection(currentValue, 1000);

            Assert.Positive(correctionValue);
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
            var config = new Config(targetValue, -1000, 1000);

            return new Controller(config);
        }
    }
}
