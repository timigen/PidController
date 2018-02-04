using NUnit.Framework;

using Pid;
using PidController;

namespace PidControllerTests
{
    [TestFixture]
    public class OutputLimits
    {
        private double min = -5;
        private double max = 5;

        [Test]
        public void OutputMinimumEnfroced()
        {
            var targetValue = -90000000;
            var c = GetController(targetValue);

            double correction = c.GetCorrection(10000000,1000);

            Assert.LessOrEqual(correction, min);
        }

        [Test]
        public void OutputMaximumEnfroced()
        {
            var targetValue = 90000000;
            var c = GetController(targetValue);

            double correction = c.GetCorrection(-10000000, 1000);

            Assert.LessOrEqual(correction, max);
        }

        private Controller GetController(double targetValue)
        {
            var config = new Config(targetValue, min, max);

            return new Controller(config);
        }
    }
}
