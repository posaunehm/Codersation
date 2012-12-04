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
                yield return new _コマンドパーサに渡すTestFixture.Parameter {
                    Input = "ins 10 100",
                    Expected = new MoneyInsertionParseResult {
                        Status = ParseResultStatus.Success,
                        Money = Money.Coin10,
                        Count = 100,
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

        [Test]
        public void _投入合計金額表示依頼をパースする() {
            var repo = new CommandParserRepository();
            
            var parser = repo.FindParser("show amount");
            var result = parser();            
            
            Assert.That(result.Status, Is.EqualTo(ParseResultStatus.Success));
            Assert.That(result, Is.InstanceOf(typeof(ShowAmountParseResult)));
        }
        
        [Test]
        public void _陳列された商品の表示依頼をパースする() {
            var repo = new CommandParserRepository();
            
            var parser = repo.FindParser("show item");
            var result = parser();            
            
            Assert.That(result.Status, Is.EqualTo(ParseResultStatus.Success));
            Assert.That(result, Is.InstanceOf(typeof(ShowItemParseResult)));
        }

        [TestCase("3")]
        [TestCase("1")]
        [TestCase("4")]
        public void _商品の購入依頼をパースする_一件の場合 (string inPosition) {
            var repo = new CommandParserRepository();
            
            var parser = repo.FindParser("buy " + inPosition);
            var result = parser();            
            
            Assert.That(result.Status, Is.EqualTo(ParseResultStatus.Success));
            Assert.That(result, Is.InstanceOf(typeof(PurchaseParseResult)));
            
            var actual = (PurchaseParseResult)result;
            
            Assert.That(actual.Positions.Length, Is.EqualTo(1));
            Assert.That(actual.Positions[0].ToString(), Is.EqualTo(inPosition));
        }

        [TestCase("1 3")]
        [TestCase("1 1 1 1")]
        public void _商品の購入依頼をパースする_複数券同時の場合(string inPositions) {
            var repo = new CommandParserRepository();
            
            var parser = repo.FindParser("buy " + inPositions);
            var result = parser();            
            
            Assert.That(result.Status, Is.EqualTo(ParseResultStatus.Success));
            Assert.That(result, Is.InstanceOf(typeof(PurchaseParseResult)));
            
            var actual = (PurchaseParseResult)result;
            var expected = inPositions.Split(' ');
            Assert.That(actual.Positions.Length, Is.EqualTo(expected.Length));

            for (var i = 0; i < expected.Length; ++i) {
                Assert.That(actual.Positions[i].ToString(), Is.EqualTo(expected[i]));
            }
        }

        [TestCase("")]
        [TestCase("FF")]
        [TestCase("     ")]
        [TestCase("AA 3")]
        public void _商品の購入依頼をパースする_インデックス未指定の場合(string inPosition) {
            var repo = new CommandParserRepository();
            
            var parser = repo.FindParser("buy " + inPosition);
            var result = parser();            

            Assert.That(result.Status, Is.EqualTo(ParseResultStatus.InvalidArgs));
            Assert.That(result, Is.InstanceOf(typeof(ParseErrorResult)));
        }

        private HashSet<string> ListExpectedHelpContents() {
            return new HashSet<string> {
                "ins", 
                "buy",
                "show item",
                "show amount",
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

            var passed = false;
            var runner = repo.FindRunner(new MoneyEjectParseResult(), (message) => {
                Assert.That(message, Is.EqualTo("money is not inserted."));
                passed = true;
            });
            runner();
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));

            Assert.That(passed, Is.True);
        }

        [Test]
        public void _投入合計金額表示を処理する_未入金の場合() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContext()
                    .BindRunnerRepository()
                    .Get<IRunnerRepository>()
                ;
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));
            
            var passed = false;
            var runner = repo.FindRunner(new ShowAmountParseResult(), (message) => {
                Assert.That(message, Is.EqualTo("Not received."));
                passed = true;
            });
            runner();
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));           
            Assert.That(passed, Is.True);
        }

        [Test]
        public void _投入合計金額表示を処理する_受け付けない金種を投入した場合() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContext()
                    .BindRunnerRepository()
                    .Get<IRunnerRepository>()
                ;

            Action runner;
            var fixtures = new _コマンドパーサに渡すTestFixture().InvalidInsMoneyParams;
            foreach (var param in fixtures.Select(f => f.Expected)) {
                runner = repo.FindRunner(param, null);
                runner();

                var passed = false;
                runner = repo.FindRunner(new ShowAmountParseResult(), (message) => {
                    Assert.That(message, Is.EqualTo("Not received."));
                    passed = true;
                });
                runner();
                
                Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));
                Assert.That(passed, Is.True);
            }
        }

        [Test]
        public void _投入合計金額表示を処理する_お金を投入した場合() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContext()
                    .BindRunnerRepository()
                    .Get<IRunnerRepository>()
                    ;

            Action runner;
            var fixtures = new _コマンドパーサに渡すTestFixture().InsMoneyParams;
            foreach (var param in fixtures.Select(f => f.Expected)) {
                runner = repo.FindRunner(param, null);
                runner();

            }

            var expected = fixtures
                .Select(f => f.Expected)
                    .Cast<MoneyInsertionParseResult>()
                    .Sum(r => r.Money.Value() * r.Count)
            ;

            var passed = false;
            runner = repo.FindRunner(new ShowAmountParseResult(), (message) => {
                Assert.That(message, Is.EqualTo(string.Format("total money is {0}.", expected)));
                Assert.That(message, Is.EqualTo(string.Format("total money is {0}.", repo.PurchaseContext.ReceivedTotal)));
                passed = true;
            });
            runner();
            Assert.That(passed, Is.True);

            runner = repo.FindRunner(new MoneyEjectParseResult(), null);
            runner();

            passed = false;
            runner = repo.FindRunner(new ShowAmountParseResult(), (message) => {
                Assert.That(message, Is.EqualTo("Not received."));
                passed = true;
            });
            runner();
            Assert.That(passed, Is.True);
        }

        [Test]
        public void _陳列された商品の表示依頼を処理する_未入金の場合() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContextContainingSoldout()
                .BindRunnerRepository()
                .Get<IRunnerRepository>()
            ;

            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));

            var expected = new string[] {
                "       # Name                     Price",
                "-----+--+------------------------+------",
                " [ ]   1 Item0...................   300",
                " [ ]   2 Item1...................  1200",
                " [-]   3 Item2...................   900",
                " [ ]   4 Item3...................   600"
            };
 
            var it = expected.GetEnumerator();

            var runner = repo.FindRunner(new ShowItemParseResult(), (message) => {
                Assert.That(it.MoveNext(), Is.True);
                Assert.That(message, Is.EqualTo(it.Current));
            });
            runner();
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));           
            Assert.That(it.MoveNext(), Is.False);
        }

        [Ignore]
        [Test]
        public void _陳列された商品の表示依頼を処理する_未陳列の商品含む場合() {
        }

        [Test]
        public void _陳列された商品の表示依頼を処理する_受け付けない金種を投入した場合() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContextContainingSoldout()
                .BindRunnerRepository()
                .Get<IRunnerRepository>()
            ;
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));

            Action runner;
            var fixtures = new _コマンドパーサに渡すTestFixture().InvalidInsMoneyParams;
            foreach (var param in fixtures.Select(f => f.Expected)) {
                var expected = new string[] {
                    "       # Name                     Price",
                    "-----+--+------------------------+------",
                    " [ ]   1 Item0...................   300",
                    " [ ]   2 Item1...................  1200",
                    " [-]   3 Item2...................   900",
                    " [ ]   4 Item3...................   600"
                };               
                var it = expected.GetEnumerator();

                runner = repo.FindRunner(param, null);
                runner();

                runner = repo.FindRunner(new ShowItemParseResult(), (message) => {
                    Assert.That(it.MoveNext(), Is.True);
                    Assert.That(message, Is.EqualTo(it.Current));
                });
                runner();

                Assert.That(it.MoveNext(), Is.False);
                Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));           
            }
        }

        [Test]
        public void _陳列された商品の表示依頼を処理する_お金を投入した場合() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContextContainingSoldout()
                .BindRunnerRepository()
                .Get<IRunnerRepository>();
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));

            Action runner;
            this.TestShowItemCore(repo, 
                "       # Name                     Price",
                "-----+--+------------------------+------",
                " [ ]   1 Item0...................   300",
                " [ ]   2 Item1...................  1200",
                " [-]   3 Item2...................   900",
                " [ ]   4 Item3...................   600"
            );               

            var fixtures = new _コマンドパーサに渡すTestFixture().InsMoneyParams.Select(f => f.Expected);
            var money = fixtures.GetEnumerator();

            money.MoveNext();
            runner = repo.FindRunner(money.Current, null);
            runner();
            
            this.TestShowItemCore(repo, 
                "       # Name                     Price",
                "-----+--+------------------------+------",
                " [*]   1 Item0...................   300",
                " [ ]   2 Item1...................  1200",
                " [-]   3 Item2...................   900",
                " [ ]   4 Item3...................   600"
            );

            money.MoveNext();
            runner = repo.FindRunner(money.Current, null);
            runner();
            
            this.TestShowItemCore(repo, 
                "       # Name                     Price",
                "-----+--+------------------------+------",
                " [*]   1 Item0...................   300",
                " [ ]   2 Item1...................  1200",
                " [-]   3 Item2...................   900",
                " [*]   4 Item3...................   600"
            );

            money.MoveNext();
            runner = repo.FindRunner(money.Current, null);
            runner();
            
            this.TestShowItemCore(repo, 
                "       # Name                     Price",
                "-----+--+------------------------+------",
                " [*]   1 Item0...................   300",
                " [*]   2 Item1...................  1200",
                " [-]   3 Item2...................   900",
                " [*]   4 Item3...................   600"
            );
        }

        [Test]
        public void _陳列された商品の表示依頼を処理する_釣り銭切れが解消するまでは購入不可となる場合() {
            var repo = new Ninject.StandardKernel()
                .BindNoChangeContext()
                .BindRunnerRepository()
                .Get<IRunnerRepository>()
            ;
        
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));
            
            Action runner;

            runner = repo.FindRunner(new MoneyInsertionParseResult {Money = Money.Coin500, Count=1}, null);
            runner();

            this.TestShowItemCore(repo, 
                "       # Name                     Price",
                "-----+--+------------------------+------",
                " [!]   1 Item0...................   120",
                " [x]   2 ........................     0",
                " [!]   3 Item2...................   250"
            );   

            runner = repo.FindRunner(new MoneyInsertionParseResult {Money = Money.Coin100, Count=1}, null);
            runner();
            runner = repo.FindRunner(new MoneyInsertionParseResult {Money = Money.Coin10, Count=3}, null);
            runner();

            this.TestShowItemCore(repo, 
                "       # Name                     Price",
                "-----+--+------------------------+------",
                " [*]   1 Item0...................   120",
                " [x]   2 ........................     0",
                " [!]   3 Item2...................   250"
            );   

        }

        private void TestShowItemCore(IRunnerRepository inRepo, params string[] inExpected) {
            var it = inExpected.GetEnumerator();

            var runner = inRepo.FindRunner(new ShowItemParseResult(), (message) => {
                Assert.That(it.MoveNext(), Is.True);
                Assert.That(message, Is.EqualTo(it.Current));
            });
            runner();
            
            Assert.That(it.MoveNext(), Is.False);
        }

        [Test]
        public void _商品の購入依頼を処理する_未入金で一件の場合() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContextContainingSoldout()
                .BindRunnerRepository()
                .Get<IRunnerRepository>()
            ;
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));

            var result = new PurchaseParseResult { 
                Positions = new int[] {1}
            };

            var expected = new string[] {
                "Money enough to purchase is not inserted. (left: 300)",
            };
            var it = expected.GetEnumerator();

            var runner = repo.FindRunner(result, (message) => {
                Assert.That(it.MoveNext(), Is.True);
                Assert.That(message, Is.Not.Null.And.Not.Empty);
                Assert.That(message, Is.EqualTo(it.Current));
            });

            runner();

            Assert.That(it.MoveNext(), Is.False);
        }
        
        [Test]
        public void _商品の購入依頼を処理する_未入金で複数件同時の場合() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContextContainingSoldout()
                .BindRunnerRepository()
                .Get<IRunnerRepository>()
            ;
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));
            
            var result = new PurchaseParseResult { 
                Positions = new int[] {4, 1}
            };
            
            var expected = new string[] {
                "Money enough to purchase is not inserted. (left: 600)",
                "Money enough to purchase is not inserted. (left: 300)",
            };
            var it = expected.GetEnumerator();
            
            var runner = repo.FindRunner(result, (message) => {
                Assert.That(it.MoveNext(), Is.True);
                Assert.That(message, Is.Not.Null.And.Not.Empty);
                Assert.That(message, Is.EqualTo(it.Current));
            });
            
            runner();
            
            Assert.That(it.MoveNext(), Is.False);
        }
        
        [Test]
        public void _商品の購入依頼を処理する_未入金で複数件同時_売り切れ選択含む場合() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContextContainingSoldout()
                    .BindRunnerRepository()
                    .Get<IRunnerRepository>()
                    ;
            
            Assert.That(repo.PurchaseContext.ReceivedTotal, Is.EqualTo(0));
            
            var result = new PurchaseParseResult { 
                Positions = new int[] {3, 1}
            };
            
            var expected = new string[] {
                "Sorry, this item has been sold out.",
                "Money enough to purchase is not inserted. (left: 300)",
            };
            var it = expected.GetEnumerator();
            
            var runner = repo.FindRunner(result, (message) => {
                Assert.That(it.MoveNext(), Is.True);
                Assert.That(message, Is.Not.Null.And.Not.Empty);
                Assert.That(message, Is.EqualTo(it.Current));
            });
            
            runner();
            
            Assert.That(it.MoveNext(), Is.False);
        }

        [Test]
        public void _商品の購入依頼を処理する_一件の場合() {
            var repo = new Ninject.StandardKernel()
                .BindPurchaseContextContainingSoldout()
                .BindRunnerRepository()
                .Get<IRunnerRepository>()
            ;

            var fixtures = new _コマンドパーサに渡すTestFixture().InsMoneyParams
                .Select(f => f.Expected)
                .Cast<MoneyInsertionParseResult>()
            ;

            Action runner;
            foreach (var f in fixtures) {
                runner = repo.FindRunner(f, null);

                runner();
            }
            
            var result = new PurchaseParseResult { 
                Positions = new int[] {1}
            };
            
            var expected = new string[] {
                "Purchased !! [Item0]",
            };
            var it = expected.GetEnumerator();

            runner = repo.FindRunner(result, (message) => {
                Assert.That(it.MoveNext(), Is.True);
                Assert.That(message, Is.Not.Null.And.Not.Empty);
                Assert.That(message, Is.EqualTo(it.Current));
            });

            runner();

            Assert.That(it.MoveNext(), Is.False);
        }

        [Ignore]
        [Test]
        public void _商品の購入依頼を処理する_複数件同時の場合 () {
        }
        
        [Ignore]
        [Test]
        public void _商品の購入依頼を処理する_複数件同時_でも後半お金不足の場合 () {
        }

        [Ignore]
        [Test]
        public void _商品の購入依頼を処理する_正しくないインデックスを含む一件の場合() {
        }

        [Ignore]
        [Test]
        public void _商品の購入依頼を処理する_正しくないインデックスを含む複数件同時の場合 () {
        }

        [Ignore]
        [Test]
        public void _商品の購入依頼を処理する_範囲外の一件の場合() {
        }

        [Ignore]
        [Test]
        public void _商品の購入依頼を処理する_範囲外の複数件同時の場合 () {
        }
        
        [Ignore]
        [Test]
        public void _商品の購入依頼を処理する_うっかり未陳列の商品を選択した場合 () {
        }

        [Test]
        public void _お金の排出依頼を処理する() {
            var parameters = new _コマンドパーサに渡すTestFixture().InsMoneyParams
                .Select(p => p.Expected)
                .Cast<MoneyInsertionParseResult>()
            ;
            
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
            var content = ConsoleAppHelper.ListHelpContents()
                .Where(c => c.Command == result.Command).FirstOrDefault();

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

