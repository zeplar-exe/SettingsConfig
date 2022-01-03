using System;
using Jammo.ParserTools;
using SettingsConfig.Internal.Lexer;

namespace SettingsConfig.Parser.Nodes
{
    public class SettingAssignmentExpression : SettingsNode
    {
        public IdentifierNode Name { get; }
        public SettingsToken EqualsToken { get; }
        public SettingValueNode ValueNode { get; }
        
        public SettingAssignmentExpression(IdentifierNode name, SettingsToken equals, SettingValueNode value) 
            : base(name.Context)
        {
            ThrowIfInvalidToken(equals, SettingsTokenId.Equals);
            
            Name = name;
            EqualsToken = equals;
            ValueNode = value;
        }

        public override string ToFormattedString()
        {
            return $"{Name}={ValueNode}";
        }
    }
}