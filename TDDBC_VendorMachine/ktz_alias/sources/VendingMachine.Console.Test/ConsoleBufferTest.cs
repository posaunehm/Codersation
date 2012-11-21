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
            Assert.That(buf.History.Last(), Is.EqualTo(inExpected));
        }

        [TestCase("in", "ins")] 
        [TestCase("bu", "buy")]
        [TestCase("eje", "eject")] 
        [TestCase("   eje", "   eject")] 
        [TestCase("dummy", "dummy")] 
        public void _標準入力からの取得をシミュレーション_入力補完あり(string inPartial, string inExpected) {
            var dic = CommandCompletionHelper.ListCommands();
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

        [TestCase("in", "ins")] 
        [TestCase("bu", "buy")]
        [TestCase("eje", "eject")] 
        [TestCase("   eje", "   eject")] 
        [TestCase("dummy", "dummy")] 
        public void _標準入力からの取得をシミュレーション_入力補完あり2(string inPartial, string inExpected) {
            var dic = CommandCompletionHelper.ListCommands();
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
            
            Assert.That(buf.ReadExtraNewLine(), Is.EqualTo('\n'));
            Assert.That(buf.Current, Is.Null.Or.Empty);
            Assert.That(buf.History.Any(), Is.True);
            Assert.That(buf.History.Last(), Is.EqualTo(inExpected));
        }

        public class _複数候補の入力補完Fixture {
            public IEnumerable<Tuple<string, string[]>> Params {
                get {
                    yield return Tuple.Create(
                        "sho", 
                        TestUtils.TestHelper.AsArray("show", "show amount", "show item", "show")
                    );
                    yield return Tuple.Create(
                        "he", 
                        TestUtils.TestHelper.AsArray(
                            "help", "help buy", "help eject", "help help", "help ins", 
                            "help show", "help show amount", "help show item",
                            "help"
                    ));
                    yield return Tuple.Create(
                        "",
                        TestUtils.TestHelper.AsArray("buy", "eject", "help", "ins", "show", "buy")
                    );
                }
            }
        }

        [Test]
        public void _標準入力からの取得をシミュレーション_入力補完あり_複数候補あり(
            [ValueSource(typeof(_複数候補の入力補完Fixture), "Params")] Tuple<string, string[]> inParameter) {
            var dic = ConsoleTestHelper.ListHelpCommands();
            var buf = new FakeConsoleReadBuffer(dic, inParameter.Item1) {
                Prompt = "> ",
            };
            
            Assert.That(buf.InCompleting, Is.False);
            
            buf.ReadToEnd();
            Assert.That(buf.Current, Is.EqualTo(inParameter.Item1));
            Assert.That(buf.InCompleting, Is.False);

            foreach (var expected in inParameter.Item2) {
                Assert.That(buf.ReadExtraTab(), Is.EqualTo(expected.Last()));
                Assert.That(buf.Current, Is.EqualTo(expected));
                Assert.That(buf.InCompleting, Is.True);
            }

            Assert.That(buf.ReadExtraSpace(), Is.EqualTo(' '));
            Assert.That(buf.Current, Is.EqualTo(inParameter.Item2.Last() + ' '));
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

