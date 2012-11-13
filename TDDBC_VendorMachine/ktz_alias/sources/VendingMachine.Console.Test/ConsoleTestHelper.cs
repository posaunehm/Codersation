using System;

using Ninject;

namespace VendingMachine.Console.Test {
    public static class ConsoleVendingMachineDIExtensions {
        public static IKernel BindRunnerRepository(this IKernel inSelf) {
            inSelf.Bind<IRunnerRepository>().ToMethod(ctx => new CommandRunnerRepository());

            return inSelf;
        }
    }
}

