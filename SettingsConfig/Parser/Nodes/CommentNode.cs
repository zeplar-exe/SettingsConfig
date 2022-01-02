using Jammo.ParserTools;
using SettingsConfig.Internal.Lexer;

namespace SettingsConfig.Parser.Nodes;

public class CommentNode : SettingsNode
{
    public SettingsToken Token { get; }
    
    public CommentNode(SettingsToken token) : base(token.Context)
    {
        Token = token;
    }
}