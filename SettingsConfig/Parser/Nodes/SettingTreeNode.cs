using System;
using System.Collections.Generic;
using System.Linq;
using Jammo.ParserTools;
using SettingsConfig.Internal.Lexer;

namespace SettingsConfig.Parser.Nodes
{
    public class SettingTreeNode : SettingValueNode
    {
        private readonly SettingAssignmentExpression[] b_assignments;

        public SettingsToken OpenBracket { get; }
        public IEnumerable<SettingAssignmentExpression> Assignments => b_assignments;
        public SettingsToken CloseBracket { get; }
        
        public SettingTreeNode(
            SettingsToken open, IEnumerable<SettingAssignmentExpression> nodes, SettingsToken close) : base(open.Context)
        {
            ThrowIfInvalidToken(open, SettingsTokenId.OpenBracket);
            ThrowIfInvalidToken(close, SettingsTokenId.CloseBracket);
            
            OpenBracket = open;
            CloseBracket = close;
            b_assignments = nodes.ToArray();
        }
    }
}