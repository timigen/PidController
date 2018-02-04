using NUnit.Framework;
using Pid;

namespace PidControllerTests
{
    [TestFixture]
    public class Output
    {
        [Test]
        public void CurrentLtTargetCorrectionIsPositive()
        {
            double targetValue = 500000;
            double currentValue = 0;

            var c = GetController(targetValue);
            double correctionValue = c.SetProcessValue(currentValue, 1000);

            Assert.Positive(correctionValue);
        }

        [Test]
        public void CurrentEqualsTargetCorrectionIsZero()
        {
            double targetValue = 500000;
            double currentValue = 500000;

            var c = GetController(targetValue);
            double correctionValue = c.SetProcessValue(currentValue, 1000);

            Assert.Zero(correctionValue);
        }

        [Test]
        public void CurrentGtTargetCorrectionIsNegative()
        {
            double targetValue = 5000;
            double currentValue = 100000;

            var c = GetController(targetValue);
            double correctionValue = c.SetProcessValue(currentValue, 1000);

            Assert.Negative(correctionValue);
        }

        [Test]
        public void Ascend()
        {
            double targetValue = 1000;
            double[] values = new double[] { 0, 10, 700, 900, targetValue };
            var c = GetController(targetValue);

            foreach (double mV in values)
            {
                var outp = c.SetProcessValue(mV, 1000);

                if (mV == targetValue)
                {
                    Assert.Zero(outp);
                    break;
                }

                Assert.Positive(outp);
            }

        }

        [Test]
        public void Descend()
        {
            double targetValue = 0;
            double[] values = new double[] { 1000, 900, 700, 250, targetValue };
            var c = GetController(targetValue);

            foreach (double mV in values)
            {
                var outp = c.SetProcessValue(mV, 1000);

                if (mV == targetValue)
                {
                    Assert.Zero(outp);
                    break;
                }

                Assert.Negative(outp);
            }

        }

        private Controller GetController(double targetValue)
        {
            double p = 0.001;
            double i = 0.00005;
            double d = 0.0001;

            return new Controller(p, i, d, targetValue);
        }
    }
}
