using System.Linq;
using Jammo.ParserTools;

namespace SettingsConfig.Parser.Nodes
{
    public class AndStatement : LogicalStatement
    {
        internal AndStatement(BlockNode block, StringContext context) : base(block, context)
        {
            
        }
        
        public override bool IsTruthy()
        {
            return Values.All(t => t);
        }
    }
}