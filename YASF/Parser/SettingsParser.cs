using System.Collections.Generic;
using System.IO;
using Jammo.ParserTools;
using Jammo.ParserTools.Tools;
using YASF.Settings;
using YASF.Settings.Methods;
using YASF.Internal.Lexer;
using YASF.Parser.Nodes;

namespace YASF.Parser
{
    public class SettingsParser
    {
        private EnumerableNavigator<SettingsToken> Navigator { get; }

        private readonly List<ParserError> b_errors;

        public IEnumerable<ParserError> Errors => b_errors.AsReadOnly();

        public SettingsParser(Stream stream)
        {
            b_errors = new List<ParserError>();
            
            using var reader = new StreamReader(stream);
            
            Navigator = new SettingsLexer(reader.ReadToEnd()).Lex().ToNavigator();
        }
        
        public SettingsParser(string text)
        {
            b_errors = new List<ParserError>();
            
            Navigator = new SettingsLexer(text).Lex().ToNavigator();
        }

        public SettingsDocument ParseDocument()
        {
            return SettingsDocument.FromParser(this);
        }
        
        public IEnumerable<SettingsNode> ParseSyntaxTree()
        {
            foreach (var token in Navigator.EnumerateFromIndex())
            {
                switch (token.Id)
                {
                    case SettingsTokenId.Identifier:
                    {
                        if (TryParseSetting(new IdentifierNode(token), out var setting))
                            yield return setting;
                        
                        break;
                    }
                    case SettingsTokenId.Comment:
                        yield return new CommentNode(token);
                        break;
                    default:
                        ReportError("Unexpected token.");
                        break;
                }
            }
        }

        private bool TryParseSetting(IdentifierNode identifier, out SettingsNode node)
        {
            node = default;
            
            if (TryParseSettingAssignment(identifier, out var token))
            {
                node = token;
                
                return true;
            }

            return false;
        }

        private bool TryParseSettingAssignment(IdentifierNode identifier, out SettingAssignmentExpression expression)
        {
            expression = default;
            
            if (!Navigator.TakeIf(t => t.Is(SettingsTokenId.Equals), out var equalsToken))
            {
                ReportError("Expected a '='");
                
                return false;
            }
            
            SettingAssignmentExpression CreateExpression(SettingValueNode valueNode)
            {
                return new SettingAssignmentExpression(identifier, equalsToken, valueNode);
            }
            
            if (Navigator.TakeIf(t => t.Is(SettingsTokenId.OpenCurlyBracket), out var curlyBracket))
            {
                expression = CreateExpression(ParseMethod(curlyBracket));

                return true;
            }
            
            if (Navigator.TakeIf(t => t.Is(SettingsTokenId.OpenBracket), out var openBracket))
            {
                expression = CreateExpression(ParseTree(openBracket));

                return true;
            }

            if (!Navigator.TryMoveNext(out var literalToken) || !literalToken.IsLiteral())
            {
                ReportError("Expected a value.");

                expression = CreateExpression(new UnknownNode(literalToken));

                return true;
            }

            switch (literalToken.Id)
            {
                case SettingsTokenId.StringLiteral:
                    expression = CreateExpression(new StringLiteralNode(literalToken));
                    return true;
                case SettingsTokenId.NumericLiteral:
                    expression = CreateExpression(new NumericLiteralNode(literalToken));
                    break;
                case SettingsTokenId.BooleanTrue or SettingsTokenId.BooleanFalse:
                    expression = CreateExpression(new BooleanLiteralNode(literalToken));
                    break;
            }

            return true;
        }
        
        private SettingTreeNode ParseTree(SettingsToken open)
        {
            var children = new List<SettingAssignmentExpression>();
            SettingsToken closeBracket = null;

            foreach (var token in Navigator.EnumerateFromIndex())
            {
                switch (token.Id)
                {
                    case SettingsTokenId.Identifier:
                        if (TryParseSettingAssignment(new IdentifierNode(token), out var assignment))
                            children.Add(assignment);
                        break;
                    case SettingsTokenId.CloseBracket:
                        closeBracket = token;
                        goto TreeClosed;
                }
            }
            
            TreeClosed:

            return new SettingTreeNode(open, children, closeBracket);
        }
        
        private MethodNode ParseMethod(SettingsToken openCurlyBracket)
        {
            _ = Navigator.TakeWhile(t => !t.Is(SettingsTokenId.CloseCurlyBracket));
            Navigator.TryMoveNext(out var close);
            
            var node = new MethodNode(openCurlyBracket, close);

            // TODO:
            
            return node;
        }

        private void ReportError(string message)
        {
            b_errors.Add(new ParserError(message, Navigator.Current.Context));
        }
    }
}