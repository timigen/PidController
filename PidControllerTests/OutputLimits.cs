using NUnit.Framework;

using Pid;

namespace PidControllerTests
{
    [TestFixture]
    public class OutputLimits
    {
        private double min = -5;
        private double max = 5;

        [Test]
        public void OutputMinimumEnforced()
        {
            var targetValue = -90000000;
            var c = GetController(targetValue);

            double correction = c.GetCorrection(10000000, 1000);

            Assert.GreaterOrEqual(correction, min);
        }

        [Test]
        public void OutputMaximumEnforced()
        {
            var targetValue = 90000000;
            var c = GetController(targetValue);

            double correction = c.GetCorrection(-10000000, 1000);

            Assert.LessOrEqual(correction, max);
        }

        private IController GetController(double targetValue)
        {
            var config = new Config(targetValue, min, max);

            return new Controller(config);
        }
    }
}
