using System;

using NUnit.Framework;

using WBS;

namespace WBSTest
{
    [TestFixture]
    public class EngineerFactoryTester
    {
        [TestCase]
        public void TestEngineerCreate()
        {
            Engineer e1 = EngineerFactory.Create("Test 1", 15);
            Engineer e2 = EngineerFactory.Create("Test 2", -10);
            Engineer e3 = EngineerFactory.Create("Test 3", 25);

            Assert.That(e1.ID, Is.EqualTo(0));
            Assert.That(e2.ID, Is.EqualTo(1));
            Assert.That(e3.ID, Is.EqualTo(2));

            Assert.That(e1.Name, Is.EqualTo("Test 1"));
            Assert.That(e2.Name, Is.EqualTo("Test 2"));
            Assert.That(e3.Name, Is.EqualTo("Test 3"));

            Assert.That(e1.HoursAvailable, Is.EqualTo(15));
            Assert.That(e2.HoursAvailable, Is.EqualTo(0));
            Assert.That(e3.HoursAvailable, Is.EqualTo(24));
        }
    }
}
