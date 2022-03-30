using Jammo.ParserTools;
using YASF.Internal.Lexer;

namespace YASF.Parser.Nodes;

public class MethodNode : SettingValueNode
{
    public SettingsToken OpenCurlyBracket { get; }
    public SettingsToken CloseCurlyBracket { get; }
    
    public MethodNode(SettingsToken openCurlyBracket, SettingsToken closeCurlyBracket) : base(openCurlyBracket.Context)
    {
        OpenCurlyBracket = openCurlyBracket;
        CloseCurlyBracket = closeCurlyBracket;
        
        // TODO: Add functionality
    }

    public override string ToFormattedString()
    {
        return $"{OpenCurlyBracket}{CloseCurlyBracket}";
    }
}