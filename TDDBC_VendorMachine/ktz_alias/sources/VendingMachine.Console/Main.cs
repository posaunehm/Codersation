using System;

using Ninject;

using VendingMachine.Model;

namespace VendingMachine.Console {
    class MainClass {
        public static void Main(string[] args) {
            var app = new ConsoleAppRunner(
                BuildKernel(args)
            );
            app.Run();
        }

        private static IKernel BuildKernel(string[] args) {
            var kernel = new Ninject.StandardKernel();
            kernel.Bind<ChangePool>().ToMethod(ctx => new ChangePool());
            kernel.Bind<ItemRackPosition>().ToMethod(ctx => new ItemRackPosition(new Tuple<int, ItemRack>[0]));
            kernel.Bind<IUserCoinMeckRole>().ToMethod(ctx => new CoinMeckRole());
            kernel.Bind<IUserPurchaseRole>().ToMethod(ctx => new ItemRackRole());
            kernel.Bind<PurchaseContext>().ToSelf();
            kernel.Bind<IRunnerRepository>().ToMethod(ctx => new CommandRunnerRepository());

            return kernel;
        }
    }
}
