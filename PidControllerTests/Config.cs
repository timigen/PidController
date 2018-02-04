using NUnit.Framework;
using Pid;
using System;

namespace PidControllerTests
{
    [TestFixture]
    public class ConfigTest
    {
        [Test]
        public void ConfigInvalid()
        {
            Assert.Throws<Exception>(() => new Config(100, 0, 0));
            Assert.Throws<Exception>(() => new Config(100, 10, 0) );
        }
    }
}
