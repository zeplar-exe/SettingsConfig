using System.Collections.Generic;
using Jammo.ParserTools;
using Jammo.ParserTools.Lexing;
using Jammo.ParserTools.Tools;

namespace SettingsConfig.Internal.Lexer
{
    public class SettingsLexer
    {
        private EnumerableNavigator<LexerToken> Navigator { get; }
        
        public SettingsLexer(string text)
        {
            Navigator = new Jammo.ParserTools.Lexing.Lexer(text, new LexerOptions
            {
                IncludeUnderscoreAsAlphabetic = true,
                IncludePeriodAsNumeric = true
            }).ToNavigator();
        }

        public IEnumerable<SettingsToken> Lex()
        {// make abstract lcass for that in ParserTools
            foreach (var token in Navigator.EnumerateFromIndex())
            {
                switch (token.Id)
                {
                    case LexerTokenId.Alphabetic:
                    case LexerTokenId.AlphaNumeric:
                    {
                        switch (token.ToString().ToUpper())
                        {
                            case "IF":
                                yield return CreateToken(SettingsTokenId.IfKeyword);
                                break;
                            case "OR":
                                yield return CreateToken(SettingsTokenId.OrKeyword);
                                break;
                            case "AND":
                                yield return CreateToken(SettingsTokenId.AndKeyword);
                                break;
                            case "NOR":
                                yield return CreateToken(SettingsTokenId.NorKeyword);
                                break;
                            case "NAND":
                                yield return CreateToken(SettingsTokenId.NandKeyword);
                                break;
                            case "XOR":
                                yield return CreateToken(SettingsTokenId.XorKeyword);
                                break;
                            case "XAND":
                                yield return CreateToken(SettingsTokenId.XandKeyword);
                                break;
                            case "TRUE":
                                yield return CreateToken(SettingsTokenId.BooleanTrue);
                                break;
                            case "FALSE":
                                yield return CreateToken(SettingsTokenId.BooleanFalse);
                                break;
                            default:
                                yield return CreateToken(SettingsTokenId.Identifier);
                                break;
                        }
                        
                        break;
                    }
                    case LexerTokenId.DoubleQuote:
                        var fullString = new List<string>();
                        
                        Navigator.Skip();
                        
                        foreach (var stringToken in Navigator.EnumerateFromIndex())
                        {
                            if (stringToken.Is(LexerTokenId.DoubleQuote))
                            {
                                Navigator.TryPeekLast(out var last);
                                
                                if (!last.Is(LexerTokenId.Backslash))
                                    break;
                            }
                            
                            fullString.Add(stringToken.ToString());
                        }

                        yield return new SettingsToken(
                            string.Concat(fullString), token.Context,
                            SettingsTokenId.StringLiteral);
                        
                        break;
                    case LexerTokenId.Octothorpe:
                        var comment = Navigator.TakeWhile(t => !t.Is(LexerTokenId.Newline));
                        yield return CreateToken(string.Concat(comment), SettingsTokenId.Comment);
                        break;
                    case LexerTokenId.Numeric:
                        yield return CreateToken(SettingsTokenId.NumericLiteral);
                        break;
                    case LexerTokenId.LeftParenthesis:
                        yield return CreateToken(SettingsTokenId.OpenParenthesis);
                        break;
                    case LexerTokenId.RightParenthesis:
                        yield return CreateToken(SettingsTokenId.CloseParenthesis);
                        break;
                    case LexerTokenId.OpenBracket:
                        yield return CreateToken(SettingsTokenId.OpenBracket);
                        break;
                    case LexerTokenId.CloseBracket:
                        yield return CreateToken(SettingsTokenId.CloseBracket);
                        break;
                    case LexerTokenId.OpenCurlyBracket:
                        yield return CreateToken(SettingsTokenId.OpenCurlyBracket);
                        break;
                    case LexerTokenId.CloseCurlyBracket:
                        yield return CreateToken(SettingsTokenId.CloseCurlyBracket);
                        break;
                    case LexerTokenId.Period:
                        yield return CreateToken(SettingsTokenId.Period);
                        break;
                    case LexerTokenId.Equals:
                        yield return CreateToken(SettingsTokenId.Equals);
                        break;
                    case LexerTokenId.Newline:
                    case LexerTokenId.Whitespace:
                        continue;
                    default:
                        yield return CreateToken(SettingsTokenId.Unknown);
                        break;
                }
            }

            yield return CreateToken("", SettingsTokenId.EndOfFile);
        }

        private SettingsToken CreateToken(SettingsTokenId id)
        {
            return CreateToken(Navigator.Current.ToString(), id);
        }
        
        private SettingsToken CreateToken(string text, SettingsTokenId id)
        {
            return new SettingsToken(text, Navigator.Current.Context, id);
        }
    }
}