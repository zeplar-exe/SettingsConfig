using Jammo.ParserTools;
using SettingsConfig.Internal.Lexer;

namespace SettingsConfig.Parser.Nodes;

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