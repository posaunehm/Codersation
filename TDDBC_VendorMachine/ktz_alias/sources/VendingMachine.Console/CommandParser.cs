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
                            if (! int.TryParse(it.Current, out count) || count <= 0) {
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
                            if (result.HelpContents.ContainsKey(it.Current)) {
                                result.Command = it.Current;
                            }
                            else {
                                return new ParseErrorResult(ParseResultStatus.NotSupportedHelpCommand);
                            }
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

    internal struct HelpContent {
        public string Command;
        public string Description;
        public string Usage;
        public bool Ignored;
    }

    internal class HelpParseResult : AbstractCommandParseResult {
        public HelpParseResult() {
            var contents = new HelpContent[] {
                new HelpContent {
                    Command = "ins", 
                    Description = "To insert money is requested.",
                    Usage = "ins <money value> [<count>]",
                },
                new HelpContent {
                    Command = "buy",
                    Description = "",
                    Usage = "",
                    Ignored = true
                },
                new HelpContent {
                    Command = "show-item",
                    Description = "",
                    Usage = "",
                    Ignored = true
                },
                new HelpContent {
                    Command = "show-amount",
                    Description = "",
                    Usage = "",
                    Ignored = true
                },
                new HelpContent {
                    Command = "eject", 
                    Description = "To eject inserted money is requested.",
                    Usage = "eject",
                },
                new HelpContent {
                    Command = "help",
                    Description = "This message(s) is displayed.",
                    Usage = "help [<command name>]"
                }
            };

            this.HelpContents = contents.ToDictionary(
                content => content.Command, 
                content => content
            );
        }

        public IDictionary<string, HelpContent> HelpContents {get; private set;}
        public string Command { get; internal set; }
    }

    internal class ParseErrorResult : AbstractCommandParseResult {
        public ParseErrorResult(ParseResultStatus inStatus) {
            this.Status = inStatus;
        }
    }
}

