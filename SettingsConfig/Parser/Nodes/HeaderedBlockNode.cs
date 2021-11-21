using System.Collections.Generic;
using Jammo.ParserTools;

namespace SettingsConfig.Parser.Nodes
{
    public class HeaderedBlockNode : SettingsNode
    {
        public readonly BlockNode Block;
        
        public override IEnumerable<SettingsNode> Nodes
        {
            get
            {
                yield return Block;
            }
        }

        public HeaderedBlockNode(BlockNode block, StringContext context) : base(context)
        {
            Block = block;
        }
    }
}