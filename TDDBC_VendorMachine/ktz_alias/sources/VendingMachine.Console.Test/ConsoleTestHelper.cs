using System;
using System.Collections.Generic;
using System.Linq;

using Ninject;

namespace VendingMachine.Console.Test {
    public static class ConsoleVendingMachineDIExtensions {
        public static IKernel BindRunnerRepository(this IKernel inSelf) {
            var help = ConsoleAppHelper.ListHelpContents();

            inSelf.Bind<IRunnerRepository>().ToMethod(ctx => new CommandRunnerRepository(help));

            return inSelf;
        }
    }

    public static class ConsoleTestHelper {
        public static string[] ListExpectedHelpContents() {
            var len = ConsoleAppHelper.ListHelpContents()
                .Where(c => !c.Ignored)
                .Max(c => c.Command.Length)+2
            ;

            return new string[] {
                //                "buy",
                "eject".PadRight(len)           + "To eject inserted money is requested.", 
                "help".PadRight(len)            + "This message(s) is displayed.",
                "ins".PadRight(len)             + "To insert money is requested.", 
                "show amount".PadRight(len)     + "To display the total amount of money inserted is requested.",
                "show item".PadRight(len)       + "To display the layouted items is requested, where blank is unselectable, '*' is selectable, and '-' is soldout.",
            };
        }

        public static IEnumerable<string> ListHelpCommands() {
            var commands = ConsoleAppHelper.ListCommands();

            return commands.Concat(commands.Select(c => "help " + c));
        }
    }
}

