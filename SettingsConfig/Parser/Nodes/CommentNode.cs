using Jammo.ParserTools;
using SettingsConfig.Internal.Lexer;

namespace SettingsConfig.Parser.Nodes;

public class CommentNode : SettingsNode
{
    public SettingsToken Comment { get; }
    public string CommentString => Comment.ToString();
    
    public CommentNode(SettingsToken comment) : base(comment.Context)
    {
        ThrowIfInvalidToken(comment, SettingsTokenId.Comment);
        
        Comment = comment;
    }

    public override string ToFormattedString()
    {
        throw new System.NotImplementedException();
    }
}