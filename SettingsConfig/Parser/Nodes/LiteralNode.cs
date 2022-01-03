using SettingsConfig.Internal.Lexer;

namespace SettingsConfig.Parser.Nodes
{
    public abstract class LiteralNode : SettingValueNode
    {
        public SettingsToken LiteralToken { get; }
        
        public LiteralNode(SettingsToken token) : base(token.Context)
        {
            LiteralToken = token;
        }

        public abstract override string ToString();
    }
}