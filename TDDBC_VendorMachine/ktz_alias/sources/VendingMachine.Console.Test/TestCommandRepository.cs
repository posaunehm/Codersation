using System;
using System.Linq;

using VendingMachine.Console;

using NUnit.Framework;

namespace VendingMachine.Console.Test {
    internal class TestCommandRepository : ICommandRepository {
        public Action FindCommand(string inCommand, params string[] inArgs) {
            return () => {
                Assert.That(inCommand, Is.Not.Null.And.Not.Empty);
                Assert.That(inCommand, Is.EqualTo(this.ExpectedCommand));

                Assert.That(inArgs, Is.Not.Null);
                Assert.That(inArgs.Length, Is.EqualTo(this.ExpectedArgs.Length));

                foreach (var arg in inArgs.Zip(this.ExpectedArgs, (actual, expected) => Tuple.Create(actual, expected))) {
                    Assert.That(arg.Item1, Is.EqualTo(arg.Item2));
                }
            };
        }

        public string ExpectedCommand {get; set; }
        public string[] ExpectedArgs {get;set; }
    }
}

