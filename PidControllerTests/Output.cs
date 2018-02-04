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
        public void CurrentEqualsTargetCorrectionIsPositive()
        {
            double targetValue = 500000;
            double currentValue = 500000;

            var c = GetController(targetValue);
            double correctionValue = c.SetProcessValue(currentValue, 1000);

            Assert.Zero(correctionValue);
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
