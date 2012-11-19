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
        NotSupportedHelpCommand,
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
                            if (! int.TryParse(it.Current, out v)) {
                                return new ParseErrorResult(ParseResultStatus.InvalidMoney);
                            }

                            var m = MoneyResolver.Resolve(v);

                            if (m.Status != MoneyStatus.Available) {
                                return new ParseErrorResult(ParseResultStatus.InvalidMoney);
                            }

                            status = ParseResultStatus.Success;
                            money = m.Type;
                        }
                        if (it.MoveNext()) {
                            if (! int.TryParse(it.Current, out count)) {
                                return new ParseErrorResult(ParseResultStatus.InvalidMoney);
                            }
                            if (count <= 0 || count > 100) {
                                return new ParseErrorResult(ParseResultStatus.InvalidMoney);
                            }
                        }

                        return new MoneyInsertionParseResult {
                            Status = status,
                            Money = money, 
                            Count = count,
                        };
                    }
                },
                { "eject", (token) => {
                        return new MoneyEjectParseResult {
                            Status = ParseResultStatus.Success,
                        };
                    }
                },
                { "help", (tokens) => {
                        var it = tokens.Skip(1).GetEnumerator();

                        var result = new HelpParseResult {
                            Status = ParseResultStatus.Success
                        };

                        if (it.MoveNext()) {
                            result.Command = it.Current;
                        }

                        return result;
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

    internal class MoneyEjectParseResult : AbstractCommandParseResult {
    }

    internal class HelpParseResult : AbstractCommandParseResult {
        public string Command { get; internal set; }
    }

    internal class ParseErrorResult : AbstractCommandParseResult {
        public ParseErrorResult(ParseResultStatus inStatus) {
            this.Status = inStatus;
        }
    }
}

