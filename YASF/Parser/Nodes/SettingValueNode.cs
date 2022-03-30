using System.Collections.Generic;
using Jammo.ParserTools;

namespace YASF.Parser.Nodes
{
    public abstract class SettingValueNode : SettingsNode
    {
        protected SettingValueNode(StringContext context) : base(context)
        {
            
        }
    }
}