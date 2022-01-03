using System;
using SettingsConfig.Internal.Lexer;

namespace SettingsConfig.Parser.Nodes
{
    public class IdentifierNode : SettingsNode
    {
        public SettingsToken IdentifierToken { get; }
        public string Identifier => IdentifierToken.ToString();

        public IdentifierNode(SettingsToken identifier) : base(identifier.Context)
        {
            ThrowIfInvalidToken(identifier, SettingsTokenId.Identifier);
            
            IdentifierToken = identifier;
        }

        public override string ToString()
        {
            return Identifier;
        }

        public override string ToFormattedString()
        {
            throw new NotImplementedException();
        }
    }
}