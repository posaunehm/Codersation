using System;

using Ninject;

namespace VendingMachine.Console.Test {
    public static class ConsoleVendingMachineDIExtensions {
        public static IKernel BindRunnerRepository(this IKernel inSelf) {
            inSelf.Bind<IRunnerRepository>().ToMethod(ctx => new CommandRunnerRepository());

            return inSelf;
        }
    }

    public static class ConsoleTestHelper {
        public static string[] ListExpectedHelpContents() {
            return new string[] {
                //                "buy",
                "eject        To eject inserted money is requested.", 
                "help         This message(s) is displayed.",
                "ins          To insert money is requested.", 
                //                "show-amount  ",
                //                "show-item    ",
            };
        }
    }
}

