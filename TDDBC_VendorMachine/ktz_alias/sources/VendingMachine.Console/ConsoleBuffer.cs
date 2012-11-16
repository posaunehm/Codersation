using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Console {
    public abstract class AbstractConsoleReadBuffer {
        private StringBuilder mBuf;

        protected AbstractConsoleReadBuffer() {
            this.ClearBuffer();

            this.History = new List<string>();
        }

        private void ClearBuffer() {
            mBuf = new StringBuilder();

            this.Cursor = 0;
        }

        public char Read() {
            var c = this.ReadCore();
            if (c == '\n') {
                this.History.Add(this.Current);

                this.ClearBuffer();
            }
            else {
                mBuf.Append(c);
            }

            return c;
        }

        protected abstract char ReadCore();

        public string Prompt { get; set; }
        public string Current { 
            get {
                return mBuf.ToString();
            } 
        }

        public int Cursor { get; set; }

        public IList<string> History { get; private set; }
    }
}

