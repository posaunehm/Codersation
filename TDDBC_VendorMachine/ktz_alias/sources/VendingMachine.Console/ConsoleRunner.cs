using System;
using System.Collections.Generic;
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
        private AbstractConsoleReadBuffer mReadBuffer;

        public ConsoleAppRunner(IKernel inKernel, IEnumerable<string> inDictionary) : base (inKernel) {
            mReadBuffer = new ConsoleReadBuffer(inDictionary);
            mReadBuffer.Prompt = "> ";
            mReadBuffer.BufferUpdated += () => {
                var text = mReadBuffer.Prompt + mReadBuffer.Current;

                System.Console.Write(new String('\b', System.Console.CursorLeft));

                System.Console.CursorLeft = 0;
                System.Console.CursorTop -= text.Length / System.Console.WindowWidth;
                System.Console.Write(mReadBuffer.Prompt + mReadBuffer.Current);
            };

            this.LogUpdated += (message) => {
                System.Console.WriteLine(message);
            };

            this.OnLogUpdated("Enter 'Ctrl + C' for exit application.");
            this.OnLogUpdated("Enter 'help' for instructions.");
        }

        protected override string ReadCommand() {
            string command = "";
            do {
                System.Console.Write(mReadBuffer.Prompt);
                command = mReadBuffer.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(command));

            return command.Trim();         
        }
    }

    internal class ConsoleReadBuffer : AbstractConsoleReadBuffer {
        public ConsoleReadBuffer(IEnumerable<string> inDictionary) : base(inDictionary) {
        }

        protected override char ReadCore() {
            var k = System.Console.ReadKey(true);
            var c = k.KeyChar;

            if (! char.IsControl(c) || c == '\n') {
                System.Console.Write(c);
            } else if (k.Key == ConsoleKey.Backspace) {
                if (this.Current.Length > 0) {
                    c = '\b';
                    System.Console.Write(c);
                } else {
                    c = '\0';
                }
            }

            return c;
        }
    }
}

