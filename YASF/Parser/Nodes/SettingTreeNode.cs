using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jammo.ParserTools;
using YASF.Internal.Lexer;

namespace YASF.Parser.Nodes
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

        public override string ToFormattedString()
        {
            var builder = new StringBuilder();

            builder.AppendLine(OpenBracket.ToString());
            foreach (var assignment in Assignments)
            {
                builder.AppendLine(assignment.ToFormattedString());
            }
            builder.AppendLine(CloseBracket.ToString());

            return builder.ToString();
        }
    }
}