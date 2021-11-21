using System.Linq;
using Jammo.ParserTools;

namespace SettingsConfig.Parser.Nodes
{
    public class NorStatement : LogicalStatement
    {
        internal NorStatement(BlockNode block, StringContext context) : base(block, context)
        {
            
        }
        
        public override bool IsTruthy()
        {
            return !Values.Any();
        }
    }
}