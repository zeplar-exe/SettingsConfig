using System.Net.Mime;
using Jammo.ParserTools;
using Jammo.ParserTools.Lexing;

namespace YASF.Internal.Lexer
{
    public class SettingsToken : GenericLexerToken<SettingsTokenId>
    {
        public SettingsToken(string text, StringContext context, SettingsTokenId id) : base(text, context, id)
        {
            
        }

        public override string ToString()
        {
            return RawToken;
        }
    }
}