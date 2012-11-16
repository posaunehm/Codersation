using System;
using System.Linq;

using NUnit.Framework;

namespace VendingMachine.Console.Test {
    [TestFixture]
    public class _Consoleのバッファ制御に関するTestSuite {
        [TestCase("ins\n")] 
        [TestCase("buy\n")]
        [TestCase("show ins\n")]
        [TestCase("show item\n")]
        [TestCase("show amount\n")]
        [TestCase("eject\n")] 
        [TestCase("help\n")]
        [TestCase("help ins\n")] 
        [TestCase("help buy\n")]
        [TestCase("help show ins\n")]
        [TestCase("help show item\n")]
        [TestCase("help show amount\n")]
        [TestCase("help elect\n")]
        public void TestCase(string inExpected) {
            var buf = new FakeConsoleReadBuffer(inExpected) {
                Prompt = "> ",
            };

            Assert.That(buf.Cursor, Is.EqualTo(0));
            Assert.That(buf.History.Any(), Is.False);
            Assert.That(buf.Current, Is.Null.Or.Empty);

            foreach (var e in inExpected.Select((c, i) => Tuple.Create(i, c))) {
                Assert.That(buf.Cursor, Is.EqualTo(e.Item1));
                Assert.That(buf.Current, Is.EqualTo(inExpected.Substring(0, buf.Cursor)));
                Assert.That(buf.History.Any(), Is.False);

                Assert.That(buf.Read(), Is.EqualTo(e.Item2));
            }
            Assert.That(buf.Cursor, Is.EqualTo(0));
            Assert.That(buf.Current, Is.Null.Or.Empty);
            Assert.That(buf.History.Any(), Is.True);
            Assert.That(buf.History.Last(), Is.EqualTo(inExpected.Trim()));
        }
    }

    internal class FakeConsoleReadBuffer : AbstractConsoleReadBuffer {
        private string mExpected;

        public FakeConsoleReadBuffer(string inExpected) {
            mExpected = inExpected;
        }

        protected override char ReadCore() {
            return mExpected[this.Cursor++];
        }
    }
}

