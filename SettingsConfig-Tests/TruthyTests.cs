using NUnit.Framework;
using SettingsConfig.Parser;

namespace SettingsConfig_Tests
{
    [TestFixture]
    public class TruthyTests
    {
        [Test]
        public void TestTruthyHelper()
        {
            Assert.True(true.IsTruthy() && 888.IsTruthy());
        }

        [Test]
        public void TestFalsy()
        {
            Assert.False("".IsTruthy() || new int[] { }.IsTruthy());
        }
    }
}