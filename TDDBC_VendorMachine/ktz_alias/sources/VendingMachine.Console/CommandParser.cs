using System;
using System.Collections.Generic;
using System.Linq;

using VendingMachine.Model;

namespace VendingMachine.Console {
    public enum ParseResultStatus {
        Unknown = 0,
        NotSupportedCommand,
        Success,
        InvalidMoney,
    }

    public interface IParseResult {
        ParseResultStatus Status {get; }
    }

    internal class AbstractCommandParseResult : IParseResult {
        public ParseResultStatus Status {get; internal set; }
    }

    public interface IParserRepository {
        Func<IParseResult> FindParser(string inCommand);
    }

    internal class CommandParserRepository : IParserRepository {
        private IDictionary<string, Func<string[], IParseResult>> mParsers;

        public CommandParserRepository() {
            mParsers = new Dictionary<string, Func<string[], IParseResult>> {
                { "ins", (tokens) => {
                        var it = tokens.Skip(1).GetEnumerator();
                        
                        var status = ParseResultStatus.InvalidMoney;
                        var money = Money.Unknown;
                        var count = 1;
                        if (it.MoveNext()) {
                            int v;
                            if (int.TryParse(it.Current, out v)) {
                                var m = MoneyResolver.Resolve(v);

                                if (m.Status == MoneyStatus.Available) {
                                    status = ParseResultStatus.Success;
                                    money = m.Type;
                                }
                            }
                        }
                        if (it.MoveNext()) {
                            int c;
                            if (int.TryParse(it.Current, out c)) {
                                if (c <= 0) {
                                    status = ParseResultStatus.InvalidMoney;
                                }
                                count = c;
                            }
                        }

                        return new MoneyInsertionParseResult {
                            Status = status,
                            Money = money, 
                            Count = count,
                        };
                    }
                },
                { "help", (tokens) => {
                        throw new NotImplementedException();

                        // [TODO:]
                        // ins
                        // buy
                        // show
                        // change
                    }
                },
            };
        }

        public Func<IParseResult> FindParser(string inCommand) {
            if (! string.IsNullOrEmpty(inCommand)) {            
                var tokens = inCommand.Split(' ');

                Func<string[], IParseResult> parser;
                if (mParsers.TryGetValue(tokens[0], out parser)) {
                    return () => {
                        return parser(tokens);
                    };
                }
            }

            return () => {
                return new ParseErrorResult(ParseResultStatus.NotSupportedCommand);
            };
        }

    }

    internal class MoneyInsertionParseResult : AbstractCommandParseResult {
        public Money Money {get; internal set; }
        public int Count {get; internal set; }
    }

    internal class ParseErrorResult : AbstractCommandParseResult {
        public ParseErrorResult(ParseResultStatus inStatus) {
            this.Status = inStatus;
        }
    }
}

