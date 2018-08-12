using System;
using NUnit.Framework;

namespace Infrastructure.Universal.Tests
{
    [TestFixture]
    public class TypedSwitchTests
    {
        [Test]
        public void StructTest()
        {
            var tswitch = new TypedSwitch<int>()
                .Case((Func<bool, bool> x) => 35)
                .Case((string x) => 5)
                .Case((int x) => 200);

            Assert.That(tswitch.Switch(new Func<bool, bool>(x => !x)), Is.EqualTo(35));
            Assert.That(tswitch.Switch("50"), Is.EqualTo(5));
            Assert.That(tswitch.Switch(25), Is.EqualTo(200));
            Assert.That(tswitch.Switch(12.5), Is.EqualTo(0));
        }

        [Test]
        public void ClassTest()
        {
            var delegate1 = new Action(() =>
            {
                var i = 5 + 10;
            });
            var delegate2 = new Action(() =>
            {
                var i = 20 + 30;
            });

            var tswitch = new TypedSwitch<Delegate>()
                .Case((int x) => delegate1)
                .Case((double y) => delegate2);

            Assert.That(tswitch.Switch(2), Is.EqualTo(delegate1));
            Assert.That(tswitch.Switch(2.5), Is.EqualTo(delegate2));
            Assert.That(tswitch.Switch("str"), Is.EqualTo(null));
        }
    }
}