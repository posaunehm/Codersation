using System;

using Ninject;

namespace VendingMachine.Console {
    public delegate void ConsoleLogEventHandler(string inMessage);

    public abstract class AbstractApplicationRunner {
        public static readonly string TerminateCommand = new string((char)3, 1);

        private CommandParserRepository mParserRepo;
        private IRunnerRepository mRunnerRepo;

        protected AbstractApplicationRunner(IKernel inKernel) {
            mParserRepo = new CommandParserRepository();
            mRunnerRepo = inKernel.Get<IRunnerRepository>();
        }

        public void Run() {
            while (this.ProcessCommand());
        }
        
        private bool ProcessCommand() {
            var command = ReadCommand();
            if (command == TerminateCommand) return false;

            var parser = mParserRepo.FindParser(command);
            var parseResult = parser();

            var runner = mRunnerRepo.FindRunner(parseResult, this.LogUpdated);

            runner();

            return true;
        }

        protected abstract string ReadCommand();

        protected void OnLogUpdated(string inMessage) {
            if (this.LogUpdated != null) {
                this.LogUpdated(inMessage);
            }
        }

        public event ConsoleLogEventHandler LogUpdated;
    }

    public class ConsoleAppRunner : AbstractApplicationRunner {
        public ConsoleAppRunner(IKernel inKernel) : base (inKernel) {
            this.LogUpdated += (message) => {
                System.Console.WriteLine(message);
            };

            this.OnLogUpdated("Enter 'Ctrl + C' for exit application.");
            this.OnLogUpdated("Enter 'help' for instructions.");
        }

        protected override string ReadCommand() {
            string command = "";
            do {
                System.Console.Write("> ");
                command = System.Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(command));

            return command.Trim();         
        }
    }
}

