using System;

namespace VendingMachine.Console {
    public delegate void ConsoleLogEventHandler(string inMessage);

    public abstract class AbstractApplicationRunner {
        public static readonly string TerminateCommand = new string((char)3, 1);

        private CommandParserRepository mParserRepo;
        private IRunnerRepository mRunnerRepo;

        protected AbstractApplicationRunner() {

        }

        public void Run() {
            while (this.ProcessCommand());
        }
        
        private bool ProcessCommand() {
            var command = ReadCommand();

            // if request terninating app ...

            var parser = mParserRepo.FindParser(command);
            var parseResult = parser();

            // if parse error was reported ...

            var runner = mRunnerRepo.FindRunner(parseResult);

            return true;
        }
        
        protected abstract string ReadCommand();
        
        public event ConsoleLogEventHandler LogUpdated;
    }
}

