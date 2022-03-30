using Jammo.ParserTools;
using YASF.Internal.Lexer;

namespace YASF.Parser.Nodes;

public class UnknownNode : SettingValueNode
{
    public SettingsToken Token { get; }
    
    public UnknownNode(SettingsToken token) : base(token.Context)
    {
        Token = token;
    }

    public override string ToFormattedString()
    {
        return Token.ToString();
    }
}