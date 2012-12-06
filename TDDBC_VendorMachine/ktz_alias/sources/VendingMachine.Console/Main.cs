using System;
using System.Collections.Generic;
using System.Linq;

using Ninject;

using VendingMachine.Model;

namespace VendingMachine.Console {
    class MainClass {
        public static void Main(string[] args) {
            var help = ConsoleAppHelper.ListHelpContents();

            var helpCommands = help.Where(content => ! content.Ignored).Select(content => content.Command);

            var app = new ConsoleAppRunner(
                BuildKernel(args, help),
                helpCommands.Concat(helpCommands.Select(command => "help " + command))
            );
            app.Run();
        }

        private static IKernel BuildKernel(string[] args, IEnumerable<HelpContent> inHelp) {
            var kernel = new Ninject.StandardKernel();
            kernel.Bind<CreditPool>().ToMethod(ctx => ConsoleAppHelper.InitInfinityReservedChange());
            kernel.Bind<ItemRackPosition>().ToMethod(ctx => {
                return new ItemRackPosition(
                    ConsoleAppHelper.ListRacksDefault().Select((rack, i) => Tuple.Create(i, rack)).ToArray()
                );
            });
            kernel.Bind<IUserCoinMeckRole>().ToMethod(ctx => new CoinMeckRole());
            kernel.Bind<IUserPurchaseRole>().ToMethod(ctx => new ItemRackRole());
            kernel.Bind<PurchaseContext>().ToSelf();
            kernel.Bind<IRunnerRepository>().ToMethod(ctx => new CommandRunnerRepository(inHelp));

            return kernel;
        }
    }
}
