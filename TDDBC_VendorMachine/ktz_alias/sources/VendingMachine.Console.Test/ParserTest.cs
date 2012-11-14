using System;
using System.Collections.Generic;
using System.Linq;

using VendingMachine.Console;
using VendingMachine.Model;

using Ninject;

using NUnit.Framework;

using TestUtils;

namespace VendingMachine.Console.Test {
    public class _コマンドパーサに渡すTestFixture {
        public IEnumerable<Parameter> InsMoneyParams {
            get {
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "ins 500",
                    Expected = new MoneyInsertionParseResult {
                        Status = ParseResultStatus.Success,
                        Money = Money.Coin500,
                        Count = 1,
                    },
                };
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "ins 100 4",
                    Expected = new MoneyInsertionParseResult {
                        Status = ParseResultStatus.Success,
                        Money = Money.Coin100,
                        Count = 4,
                    },
                };
            }
        }
        public IEnumerable<Parameter> InvalidInsMoneyParams {
            get {
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "ins 256",
                    Expected = new ParseErrorResult(ParseResultStatus.InvalidMoney),
                };
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "ins 128 8",
                    Expected = new ParseErrorResult(ParseResultStatus.InvalidMoney),
                };
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "ins 50 0",
                    Expected = new ParseErrorResult(ParseResultStatus.InvalidMoney),
                };
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "ins 10 -10",
                    Expected = new ParseErrorResult(ParseResultStatus.InvalidMoney),
                };
            }
        }

        public IEnumerable<Parameter> InvalidCommandParams {
            get {
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "",
                    Expected = new ParseErrorResult(ParseResultStatus.NotSupportedCommand),
                };
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "detarame",
                    Expected = new ParseErrorResult(ParseResultStatus.NotSupportedCommand),
                };
            }
        }

        public class Parameter {
            public string Input {get; internal set; }
            public IParseResult Expected {get; internal set; }
        }
    }

    [TestFixture]
    public class _入力をパースしてドメインモデルに渡すところのTestSuite {
        [Test]
        public void _お金投入の入力をパースする_成功の場合(
            [ValueSource(typeof(_コマンドパーサに渡すTestFixture), "InsMoneyParams")] _コマンドパーサに渡すTestFixture.Parameter inParameter) 
        {
            var repo = new CommandParserRepository();

            var parser = repo.FindParser(inParameter.Input);
            var result = parser();
            Assert.That(result.Status, Is.EqualTo(inParameter.Expected.Status));

            Assert.That(result, Is.InstanceOf(inParameter.Expected.GetType()));
            var actual = (MoneyInsertionParseResult)result;
            var expected = (MoneyInsertionParseResult)inParameter.Expected;

            Assert.That(actual.Money, Is.EqualTo(expected.Money));
            Assert.That(actual.Count, Is.EqualTo(expected.Count));
        }

        [Test]
        public void _お金投入の入力をパースする_不正な入力の場合(
            [ValueSource(typeof(_コマンドパーサに渡すTestFixture), "InvalidInsMoneyParams")] _コマンドパーサに渡すTestFixture.Parameter inParameter) 
        {
            var repo = new CommandParserRepository();
            var parser = repo.FindParser(inParameter.Input);
            var result = parser();
            
            Assert.That(result.Status, Is.EqualTo(inParameter.Expected.Status));

            Assert.That(result, Is.InstanceOf(inParameter.Expected.GetType()));
        }

        [Test]
        public void _お金の排出依頼をパースする() {
            var repo = new CommandParserRepository();
            var parser = repo.FindParser("eject");
            var result = parser();            
            
            Assert.That(result.Status, Is.EqualTo(ParseResultStatus.Success));
            Assert.That(result, Is.InstanceOf(typeof(MoneyEjectParseResult)));
        }

        private HashSet<string> ListExpectedHelpContents() {
            return new HashSet<string> {
                "ins", 
                "buy",
                "show-item",
                "show-amount",
                "eject", 
                "help",
            };
        }

        [Test]
        public void _ヘルプ表示依頼をパースする() {
            var repo = new CommandParserRepository();
            var parser = repo.FindParser("help");
            var result = parser();            
            
            Assert.That(result.Status, Is.EqualTo(ParseResultStatus.Success));
            Assert.That(result, Is.InstanceOf(typeof(HelpParseResult)));
            
            var actual = (HelpParseResult)result;
            var expected = this.ListExpectedHelpContents();
            
            Assert.That(actual.HelpContents.Keys.All(command => expected.Contains(command)), Is.True);
            Assert.That(actual.Command, Is.Null.Or.Empty);
        }
        
        [Test]
        public void _ヘルプ表示依頼をパースする_コマンド指定() {
            var repo = new CommandParserRepository();
            var parser = repo.FindParser("help ins");
            var result = parser();            
            
            Assert.That(result.Status, Is.EqualTo(ParseResultStatus.Success));
            Assert.That(result, Is.InstanceOf(typeof(HelpParseResult)));
            
            var actual = (HelpParseResult)result;
            var expected = this.ListExpectedHelpContents();
            
            Assert.That(actual.HelpContents.Keys.All(command => expected.Contains(command)), Is.True);
            Assert.That(actual.Command, Is.EqualTo("ins"));
        }

        [Test]
        public void _未定義のコマンドをパースする(
            [ValueSource(typeof(_コマンドパーサに渡すTestFixture), "InvalidCommandParams")] _コマンドパーサに渡すTestFixture.Parameter inParameter) 
        {
            var repo = new CommandParserRepository();
            var parser = repo.FindParser(inParameter.Input);
            var result = parser();
            
            Assert.That(result.Status, Is.EqualTo(inParameter.Expected.Status));
            
            Assert.That(result, Is.InstanceOf(inParameter.Expected.GetType()));
        }
    }

    [TestFixture]
    public class _ドメインからの応答を整形して返すところのTestSuite {
        [Test]
        public void _パースされた投入金額を処理する(
            [ValueSource(typeof(_コマンドパーサに渡すTestFixture), "InsMoneyParams")] _コマンドパーサに渡すTestFixture.Parameter inParameter) 
        {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContext()
                .BindRunnerRepository()
                .Get<IRunnerRepository>()
            ;

            var runner = repo.FindRunner(inParameter.Expected, null);

            runner();

            var expected = (MoneyInsertionParseResult)inParameter.Expected;
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(expected.Money.Value() * expected.Count));
        }

        [Test]
        public void _パースされた投入金額を処理する_連続投入() {
            var parameters = new _コマンドパーサに渡すTestFixture().InsMoneyParams
                .Select(p => p.Expected)
                .Cast<MoneyInsertionParseResult>();

            var repo = new Ninject.StandardKernel()
                .BindPurchaseContext()
                .BindRunnerRepository()
                .Get<IRunnerRepository>()
            ;

            foreach (var parameter in parameters) {
                var runner = repo.FindRunner(parameter, null);
                       
                runner();
            }

            var totalAmount = parameters
                .Sum(r => (r.Money.Value() * r.Count))
            ;
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(totalAmount));
        }
        
        [Test]
        public void _お金の排出依頼を処理する_入金がない場合() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContext()
                    .BindRunnerRepository()
                    .Get<IRunnerRepository>()
                    ;
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));
            
            var runner = repo.FindRunner(new MoneyEjectParseResult(), null);
            
            runner();
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));
        }
        
        [Test]
        public void _お金の排出依頼を処理する() {
            var parameters = new _コマンドパーサに渡すTestFixture().InsMoneyParams
                .Select(p => p.Expected)
                    .Cast<MoneyInsertionParseResult>();
            
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContext()
                    .BindRunnerRepository()
                    .Get<IRunnerRepository>()
                    ;

            Action runner;
            foreach (var parameter in parameters) {
                runner = repo.FindRunner(parameter, null);               
                runner();
            }

            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.GreaterThan(0));
            
            runner = repo.FindRunner(new MoneyEjectParseResult(), null);            
            runner();
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));
        }

        [Test]
        public void _ヘルプ表示依頼を処理する() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContext()
                .BindRunnerRepository()
                .Get<IRunnerRepository>()
            ;

            var it = ConsoleTestHelper.ListExpectedHelpContents().GetEnumerator();

            var runner = repo.FindRunner(new HelpParseResult(), (message) => {
                Assert.That(it.MoveNext(), Is.True);
                Assert.That(message, Is.Not.Null.And.Not.Empty);
                Assert.That(message, Is.EqualTo(it.Current));
            });
            
            runner();

            Assert.That(it.MoveNext(), Is.False);
        }

        [Test]
        public void _ヘルプ表示依頼を処理する_コマンド指定() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContext()
                .BindRunnerRepository()
                .Get<IRunnerRepository>()
            ;

            var result = new HelpParseResult { Command = "ins"};
            var content = result.HelpContents[result.Command];

            var it = (new string[] {content.Usage, content.Description}).GetEnumerator();

            var runner = repo.FindRunner(result, (message) => {
                Assert.That(it.MoveNext(), Is.True);
                Assert.That(message, Is.Not.Null.And.Not.Empty);
                Assert.That(message, Is.EqualTo(it.Current));
            });
            
            runner();
            
            Assert.That(it.MoveNext(), Is.False);
        }
    }
}

