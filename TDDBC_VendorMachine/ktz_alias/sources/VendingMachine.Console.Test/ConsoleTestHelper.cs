using System;
using System.Collections.Generic;
using System.Linq;

using Ninject;

namespace VendingMachine.Console.Test {
    public static class ConsoleVendingMachineDIExtensions {
        public static IKernel BindRunnerRepository(this IKernel inSelf) {
            var help = CommandCompletionHelper.ListHelpContents();

            inSelf.Bind<IRunnerRepository>().ToMethod(ctx => new CommandRunnerRepository(help));

            return inSelf;
        }
    }

    public static class ConsoleTestHelper {
        public static string[] ListExpectedHelpContents() {
            var len = CommandCompletionHelper.ListHelpContents()
                .Where(c => !c.Ignored)
                .Max(c => c.Command.Length)+2
            ;

            return new string[] {
                //                "buy",
                "eject".PadRight(len)           + "To eject inserted money is requested.", 
                "help".PadRight(len)            + "This message(s) is displayed.",
                "ins".PadRight(len)             + "To insert money is requested.", 
                "show amount".PadRight(len)     + "To display the total amount of money inserted is requested.",
            };
        }

        public static IEnumerable<string> ListHelpCommands() {
            var commands = CommandCompletionHelper.ListCommands();

            return commands.Concat(commands.Select(c => "help " + c));
        }
    }
}

