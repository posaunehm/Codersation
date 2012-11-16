using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine.Console {
    public abstract class AbstractConsoleReadBuffer {
        private StringBuilder mBuf;
        private IEnumerable<string> mDic;
        private string mCompletement;
        private Func<string> mGenerator;

        protected AbstractConsoleReadBuffer(IEnumerable<string> inPhrases) {
            mDic = this.MakeDictionary(inPhrases);

            this.ClearBuffer();

            this.History = new List<string>();
        }

        private IEnumerable<string> MakeDictionary(IEnumerable<string> inPhrases) {
            return inPhrases
                .Select(p => p.Split(' '))
                .SelectMany(words => words.Select((_, i) => string.Join(" ", words, 0, i+1)))
                .ToList()
            ;
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
            else if (c == '\t') {
                if (! this.InCompleting) {
                    mGenerator = this.InitCompletement(this.GetCurrentText(false));
                }

                mCompletement = mGenerator();

                c = string.IsNullOrEmpty(mCompletement) ? '\0': mCompletement.Last();
            }
            else if (c != '\0') {
                if (this.InCompleting) {
                    mBuf = new StringBuilder(mCompletement);
                    mGenerator = null;
                    mCompletement = null;
                }

                mBuf.Append(c);
            }

            return c;
        }

        protected abstract char ReadCore();

        private Func<string> InitCompletement(string inTarget) {
            var it = InitCompletementCore(inTarget);

            return () => {
                it.MoveNext();

                return it.Current;
            };  
        }

        private IEnumerator<string> InitCompletementCore(string inTarget) {
            var items = mDic
                .Where(phrase => string.IsNullOrWhiteSpace(inTarget) || phrase.StartsWith(inTarget))
            ;

            var noMatch = ! items.Any();
            while (true) {
                if (noMatch) {
                    yield return inTarget;
                }
                else {
                    foreach (var item in items) {
                        yield return item;
                    }
                }
            }
        }

        public bool InCompleting { 
            get {
                return mGenerator != null;
            } 
        }

        public string Prompt { get; set; }
        public string Current { 
            get {
                return this.GetCurrentText(this.InCompleting);
            } 
        }

        private string GetCurrentText(bool inInCompleting) {
            return inInCompleting ? mCompletement : mBuf.ToString();
        }

        public int Cursor { get; set; }

        public IList<string> History { get; private set; }
    }
}

