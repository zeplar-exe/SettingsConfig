using System;
using System.Diagnostics;
using NUnit.Framework;
using SettingsConfig.Parser;
using SettingsConfig.Serialization;

namespace SettingsConfig_Tests
{
    [TestFixture]
    public class DeserializeTests
    {
        [Test]
        public void TestDeserializer()
        {
            var document = new SettingsParser("name=\"Bru\" text=\"Matou\"").Parse();
            var type = SettingsDeserializer.Deserialize<TestType>(document);
            
            Console.WriteLine(type.Name);
            Console.WriteLine(type.Text);
            Assert.True(type.Name == "Bru" && type.Text == "Matou");
        }
    }

    public class TestType
    {
        public string Name { get; set; }
        internal string Text { get; set; }
    }
}