using System;
using SettingsConfig.Internal.Lexer;

namespace SettingsConfig.Parser.Nodes
{
    public class StringLiteralNode : LiteralNode
    {
        public string Value { get; }
        
        public StringLiteralNode(SettingsToken token) : base(token)
        {
            ThrowIfInvalidToken(token, SettingsTokenId.StringLiteral);

            Value = token.ToString();
        }

        public override string ToFormattedString()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}