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
                .GroupBy(item => item)
                .Select(item => item.Key)
                .ToList()
            ;
        }

        private void ClearBuffer() {
            mBuf = new StringBuilder();

            this.Cursor = 0;
        }

        public char Read() {
            var c = this.ReadCore();
            if (c == '\t') {
                if (! this.InCompleting) {
                    mGenerator = this.InitCompletement(this.GetCurrentText(false));
                }

                mCompletement = mGenerator();

                c = string.IsNullOrEmpty(mCompletement) ? '\0': mCompletement.Last();

                this.OnBufferUpdated();
            }
            else {
                if (this.InCompleting) {
                    mBuf = new StringBuilder(mCompletement);
                    
                    mGenerator = null;
                    mCompletement = null;
                }

                if (c == '\n') {
                    this.History.Add(this.Current);
                    
                    this.ClearBuffer();
                }  
                else if (c == '\b') {
                    if (mBuf.Length > 0) {
                        mBuf.Remove(mBuf.Length-1, 1);
                    }
                }
                else if (c != '\0') {
                    mBuf.Append(c);
                }
            }

            return c;
        }

        public string ReadLine() {
            while (this.Read() != '\n');

            return this.History.Last();
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
            var indent = inTarget.TakeWhile(ch => ch == ' ').Count();
            var t = inTarget.TrimStart();

            var items = this.FilterCompletion(t)
                .Select(item => item.PadLeft(item.Length+indent))
                .OrderBy(item => item)
                .ToList()
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

        private IEnumerable<string> FilterCompletion(string inTarget) {
            if (string.IsNullOrEmpty(inTarget)) {
                return mDic
                    .Select(item => item.Split(' ').First())
                    .GroupBy(item => item)
                    .Select(item => item.Key)
                ;
            }
            else {
                return mDic
                    .Where(item => item.StartsWith(inTarget))
                ;
            }
        }

        private void OnBufferUpdated() {
            if (this.BufferUpdated != null) {
                this.BufferUpdated();
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

        public event Action BufferUpdated;
    }
}

