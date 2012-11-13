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
                    Expected = new MoneyInsertionParseResult {
                        Status = ParseResultStatus.InvalidMoney,
                    },
                };
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "ins 128 8",
                    Expected = new MoneyInsertionParseResult {
                        Status = ParseResultStatus.InvalidMoney,
                    },
                };
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "ins 50 0",
                    Expected = new MoneyInsertionParseResult {
                        Status = ParseResultStatus.InvalidMoney,
                    },
                };
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "ins 10 -10",
                    Expected = new MoneyInsertionParseResult {
                        Status = ParseResultStatus.InvalidMoney,
                    },
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
        private IKernel SetUpPurchaseContextKernel() {
            var kernel = new Ninject.StandardKernel();
            kernel.Bind<ChangePool>().ToMethod(ctx => TestHelper.InitInfinityReservedChange());
            kernel.Bind<ItemRackPosition>().ToMethod(ctx => TestHelper.InitInfinityItems(ItemRackState.CanNotPurchase));
            kernel.Bind<IUserCoinMeckRole>().ToMethod(ctx => new CoinMeckRole());
            kernel.Bind<IUserPurchaseRole>().ToMethod(ctx => new ItemRackRole());
            kernel.Bind<PurchaseContext>().ToSelf();
            kernel.Bind<IRunnerRepository>().ToMethod(ctx => new CommandRunnerRepository());

            return kernel;
        }

        [Test]
        public void _パースされた投入金額を処理する(
            [ValueSource(typeof(_コマンドパーサに渡すTestFixture), "InsMoneyParams")] _コマンドパーサに渡すTestFixture.Parameter inParameter) 
        {
            var repo = this.SetUpPurchaseContextKernel().Get<IRunnerRepository>();
            var runner = repo.FindRunner(inParameter.Expected);

            runner();

            var expected = (MoneyInsertionParseResult)inParameter.Expected;
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(expected.Money.Value() * expected.Count));
        }

        [Test]
        public void _パースされた投入金額を処理する_連続投入 () {
            var parameters = new _コマンドパーサに渡すTestFixture().InsMoneyParams
                .Select(p => p.Expected)
                .Cast<MoneyInsertionParseResult>()
            ;

            var repo = this.SetUpPurchaseContextKernel().Get<IRunnerRepository>();

            var totalAmount = parameters
                .Sum(r => (r.Money.Value() * r.Count))
            ;
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(totalAmount));
        }
    }
}

