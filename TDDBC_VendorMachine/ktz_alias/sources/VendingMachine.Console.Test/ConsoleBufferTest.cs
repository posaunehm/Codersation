using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace VendingMachine.Console.Test {
    [TestFixture]
    public class _Consoleのバッファ制御に関するTestSuite {
        [TestCase("ins")] 
        [TestCase("buy")]
        [TestCase("show ins")]
        [TestCase("show item")]
        [TestCase("show amount")]
        [TestCase("eject")] 
        [TestCase("help")]
        [TestCase("help ins")] 
        [TestCase("help buy")]
        [TestCase("help show ins")]
        [TestCase("help show item")]
        [TestCase("help show amount")]
        [TestCase("help elect")]
        public void _標準入力からの取得をシミュレーション(string inExpected) {
            var buf = new FakeConsoleReadBuffer(new string[0], inExpected) {
                Prompt = "> ",
            };

            Assert.That(buf.Cursor, Is.EqualTo(0));
            Assert.That(buf.History.Any(), Is.False);
            Assert.That(buf.Current, Is.Null.Or.Empty);

            foreach (var e in inExpected.Select((c, i) => Tuple.Create(i, c))) {
                Assert.That(buf.Read(), Is.EqualTo(e.Item2));

                Assert.That(buf.Cursor, Is.EqualTo(e.Item1+1));
                Assert.That(buf.Current, Is.EqualTo(inExpected.Substring(0, buf.Cursor)));
                Assert.That(buf.History.Any(), Is.False);
            }

            Assert.That(buf.ReadExtraNewLine(), Is.EqualTo('\n'));

            Assert.That(buf.Cursor, Is.EqualTo(0));
            Assert.That(buf.Current, Is.Null.Or.Empty);
            Assert.That(buf.History.Any(), Is.True);
            Assert.That(buf.History.Last(), Is.EqualTo(inExpected.Trim()));
        }

        [TestCase("in", "ins")] 
        [TestCase("bu", "buy")]
        [TestCase("eje", "eject")] 
        public void _標準入力からの取得をシミュレーション_入力補完あり(string inPartial, string inExpected) {
            var dic = new List<string> {
                "ins",
                "buy",
                "show ins",
                "show item",
                "show amount",
                "eject", 
                "help",
                "help ins", 
                "help buy",
                "help show ins",
                "help show item",
                "help show amount",
                "help elect",
            };

            var buf = new FakeConsoleReadBuffer(dic, inPartial) {
                Prompt = "> ",
            };

            Assert.That(buf.InCompleting, Is.False);

            buf.ReadToEnd();
            Assert.That(buf.Current, Is.EqualTo(inPartial));
            Assert.That(buf.InCompleting, Is.False);

            Assert.That(buf.ReadExtraTab(), Is.EqualTo(inExpected.Last()));
            Assert.That(buf.Current, Is.EqualTo(inExpected));
            Assert.That(buf.InCompleting, Is.True);

            Assert.That(buf.ReadExtraTab(), Is.EqualTo(inExpected.Last()));
            Assert.That(buf.Current, Is.EqualTo(inExpected));
            Assert.That(buf.InCompleting, Is.True);

            Assert.That(buf.ReadExtraSpace(), Is.EqualTo(' '));
            Assert.That(buf.Current, Is.EqualTo(inExpected + ' '));
            Assert.That(buf.InCompleting, Is.False);

        }
    }

    internal class FakeConsoleReadBuffer : AbstractConsoleReadBuffer {
        private string mExpected;

        public FakeConsoleReadBuffer(IEnumerable<string> inDictionary, string inExpected) : base(inDictionary) {
            mExpected = inExpected;
        }

        protected override char ReadCore() {
            return this.Cursor == mExpected.Length ? '\0' : mExpected[this.Cursor++];
        }

        public void ReadToEnd() {
            while (this.Read() != '\0');
        }

        private char ReadExtraChar(char inChar) {
            mExpected += inChar;

            return this.Read();
        }

        public char ReadExtraNewLine() {
            return this.ReadExtraChar('\n');
        }

        public char ReadExtraTab() {
            return this.ReadExtraChar('\t');
        }

        public char ReadExtraSpace() {
            return this.ReadExtraChar(' ');
        }
    }
}

